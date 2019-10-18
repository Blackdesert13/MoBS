using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Delegates;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using MoBaSteuerung.Anlagenkomponenten.MCSpeicher;
using MoBaSteuerung;
using MoBaSteuerung.Elemente;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading;
using MoBaSteuerung.ZeichnenElemente;
using MoBa.Elemente;
using System.Collections;
//using MoBa.Anlagenkomponenten.ZeichnenElemente;

namespace MoBaSteuerung {
	//hier nur Methoden zum Anlage laden und Speichern

	/// <summary>
	/// Anlagenlogik
	/// </summary>
	public partial class Model : Control {

		private ArduinoController _ardController;
		private string _connectedComPort = "";
		//volatile Queue<> _arduinoSendeListe = new Queue<Anlagenzustand>();
		volatile Queue<Tuple<String,int>> _schaltBefehle = new Queue<Tuple<String, int>>();
		private Thread _Daemon_SendenAnArduino = null;
		private Thread _Daemon_ElementToggeln = null;

		public bool RückmeldungAktiv {
			set {
				if (value != this._zeichnenElemente.RückmeldungAktiv) {
					if (value)
						_ardController.SendData(new byte[] { 1, 1, 0, 0, 2 });
					else
						_ardController.SendData(new byte[] { 1, 2, 0, 0, 3 });
				}
				this._zeichnenElemente.RückmeldungAktiv = value;
			}
			get {
				return this._zeichnenElemente.RückmeldungAktiv;
			}
		}

		public string UpdateComPorts() {
			string result = "";
			if (_connectedComPort != "") {
				string[] ports = _ardController.GetSerialPortNames();

				bool contain = false;
				foreach (string item in ports) {
					if (_connectedComPort == item) {
						contain = true;
						break;
					}
				}
				if (!contain) {
					try {
						if (!_ardController.CloseComPort()) {
							_ardController.BefehlReceived -= _ardController_BefehlReceived;
							_ardController.Dispose();
							_ardController = new ArduinoController();
							_ardController.BefehlReceived += _ardController_BefehlReceived;
						}
					}
					catch (Exception e) {

					}
					result = "ComPort " + _connectedComPort + " getrennt";
				}
				else {
					if (!_ardController.CloseComPort()) {
						_ardController.BefehlReceived -= _ardController_BefehlReceived;
						_ardController.Dispose();
						_ardController = new ArduinoController();
						_ardController.BefehlReceived += _ardController_BefehlReceived;
					}
					if (_ardController.OpenComPort(_connectedComPort, false)) {
						result = "ComPort " + _connectedComPort + " neu verbunden";
						AlleAusgaengeSenden();
						_ardController.SendData(new byte[] { 1, 19, 0, 0, 20 });
					}
					else {
						result = "ComPort " + _connectedComPort + " getrennt";
					}

				}
			}
			return result;
		}

		/// <summary>
		/// Sendet alle Ausgänge über den Comport an die Anlage
		/// </summary>
		private void AlleAusgaengeSenden() {
			foreach (Arduino arduino in _zeichnenElemente.AnlagenZustand.ArduinoListe) {
				for (int i = 0; i < arduino.Ausgaenge.Length; i++) {
					this._ardController.SendData(this._zeichnenElemente.AnlagenZustand.GetBefehl(arduino.Nr, i));
					arduino.Ausgaenge.Changed[i] = false;
				}
			}
		}

		/// <summary>
		/// liefert alle verfügbaren Portnamen
		/// </summary>
		/// <returns></returns>
		public string[] GetSerialPortNames() {
			return _ardController.GetSerialPortNames();
		}

		public bool OpenComPort(string portName) {
			if (this._ardController.OpenComPort(portName)) {
				_connectedComPort = portName;
				AlleAusgaengeSenden();
				if (_zeichnenElemente.RückmeldungAktiv) {
					_ardController.SendData(new byte[] { 1, 1, 0, 0, 2 });
				}
				else {
					_ardController.SendData(new byte[] { 1, 2, 0, 0, 3 });
				}
				_ardController.SendData(new byte[] { 1, 19, 0, 0, 20 });
				return true;
			}
			return false;
		}

		public bool CloseComPort() {
			bool result = this._ardController.CloseComPort();
			if (!result) {
				_ardController.BefehlReceived -= _ardController_BefehlReceived;
				_ardController.Dispose();
				_ardController = new ArduinoController();
				_ardController.BefehlReceived += _ardController_BefehlReceived;
			}
			_connectedComPort = "";
			return true;
		}

		//nicht aktiv jetzt
		//private void Daemon_ArduinoSenden() {
		//	while (true) {
		//		if((_arduinoSendeListe.Count > 0) && _ardController.IsPortOpen()) {
		//			Anlagenzustand anlagenZustand = _arduinoSendeListe.Dequeue();
		//			foreach (Arduino arduino in anlagenZustand.ArduinoListe) {
		//				for (int i = 0; i < arduino.Ausgaenge.Length; i++) {
		//					this._ardController.SendData(this._zeichnenElemente.AnlagenZustand.GetBefehl(arduino.Nr, i));
		//				}
		//			}
		//		}
		//	}
		//}

		DateTime _zeitpunktLetzterBefehl;
		private void Daemon_ElementToggeln() {
			while (true) {
				if (_schaltBefehle.Count > 0) {
					_zeitpunktLetzterBefehl = DateTime.Now;
					Tuple<string, int> schaltBefehl = _schaltBefehle.Dequeue();
					if(this.ElementToggelnAusfuehren(schaltBefehl.Item1, schaltBefehl.Item2)) {
						AnlagenzustandSpeichern();
						OnAnlagenzustandChanged(null);
						OnAnlageNeuZeichnen();
					}
				}
				else {
					TimeSpan diff = DateTime.Now.Subtract(_zeitpunktLetzterBefehl);
					if(diff.Milliseconds > 500) {
						_Daemon_ElementToggeln.Suspend();
					}
				}
			}
		}
		
		/// <summary>
		/// Umschalten eines Elementes
		/// </summary>
		/// <param name="elementName">Elementtyp</param>
		/// <param name="nr">ID des Elementes, welches geschaltet werden soll</param>
		/// <returns></returns>
		public void ElementToggeln(string elementName, int nr) {
			this._schaltBefehle.Enqueue(new Tuple<string, int>(elementName, nr));
			if (_Daemon_ElementToggeln.ThreadState == (System.Threading.ThreadState.Suspended | System.Threading.ThreadState.Background )) {
				_Daemon_ElementToggeln.Resume();
			}

		}

		/// <summary>
		/// Umschalten eines Elementes
		/// </summary>
		/// <param name="elementName">Elementtyp</param>
		/// <param name="nr">ID des Elementes, welches geschaltet werden soll</param>
		/// <returns></returns>
		private bool ElementToggelnAusfuehren(string elementName, int nr) {
			AnlagenElement el = null;
			switch (elementName) {
				case "StartSignalGruppe":
					//StartSignalGruppe ssg = _zeichnenElemente.SsgElemente.Element(nr);
					//int signalId = ssg.FSAuswahl();
					//if (signalId != 0) {
					//	Signal sn = _zeichnenElemente.SignalElemente.Element(signalId);
					//	if (sn != null) {
					//		return FahrstrassenSignalSchalten(sn);
					//	}
					//	FahrstrassenSignalSchalten(signalId);
					//	//el = _zeichnenElemente.FahrstrassenElemente.Fahrstrasse(signalId);
					//	//FahrstrasseSchalten((FahrstrasseN)el, FahrstrassenSignalTyp.StartSignal);
					//	_zeichnenElemente.FSSAktualisieren();
					//	return true;
					//}
					return false;
				case "Servo":
					el = _zeichnenElemente.ServoElemente.Element(nr);
					break;
				case "Signal":
					el = _zeichnenElemente.SignalElemente.Element(nr);
					break;
				case "Gleis":
					//el = zeichnenElemente.GleisElemente.Element(nr);
					break;
				case "Schalter":
					el = _zeichnenElemente.SchalterElemente.Element(nr);
					break;
				case "FSS":
					el = _zeichnenElemente.FssElemente.Element(nr);
					break;
				case "Entkuppler":
					el = _zeichnenElemente.EntkupplerElemente.Element(nr);
					break;
				case "Weiche":
					el = _zeichnenElemente.WeicheElemente.Element(nr);
					break;
				case "FahrstrasseN_Ziel":
					el = _zeichnenElemente.FahrstrassenElemente.Fahrstrasse(nr);
					FahrstrasseN fs = (FahrstrasseN)el;
					FahrstrasseSchalten(fs, FahrstrassenSignalTyp.ZielSignal);

					if (fs.IsAktiv && fs.EndSignal.AutoStart) {
						List<AnlagenElement> fsListe = FahrstrassenSignalSchalten(fs.EndSignal, true);
						if(fsListe.Count > 0) {
							FahrstrasseSchalten((FahrstrasseN)fsListe[0], FahrstrassenSignalTyp.ZielSignal);
						}
					}
					_zeichnenElemente.FSSAktualisieren();
					return true;
					break;
				case "FahrstrasseN_Start":
					el = _zeichnenElemente.FahrstrassenElemente.Fahrstrasse(nr);

					FahrstrasseSchalten((FahrstrasseN)el, FahrstrassenSignalTyp.StartSignal);
					_zeichnenElemente.FSSAktualisieren();
					return true;
					break;
			}

			if (el != null) {
				bool action = el.AusgangToggeln();
				//if (elementName == "FSS")
				_zeichnenElemente.FSSAktualisieren();
				if (elementName == "Entkuppler") {
					if (el.ElementZustand == Elementzustand.An && EntkupplerAbschaltAutoAktiv) {
						Thread entkupplerAbschalt = new Thread(this.EntkupplerAbschaltung);
						entkupplerAbschalt.Start(el);
					}
				}
				//if (action && _ardController.IsPortOpen())
				//	OnAnlagenzustandChanged(null);
				return action;
			}

			return false;
		}

		private void EntkupplerAbschaltung(object entkuppler) {
			Entkuppler el = (Entkuppler)entkuppler;
			Thread.Sleep(this.EntkupplerAbschaltAutoWert * 1000);
			if (el.ElementZustand == Elementzustand.An) {
				el.AusgangToggeln();

				this.OnAnlageNeuZeichnen();
				this.OnAnlagenzustandChanged(el.Ausgang);
			}
		}

		public void BedienenServoManuell(ServoAction action) {
			Logging.Log.Schreibe("Servo Action " + action);
			if (this._zeichnenElemente.AktiverServo != null && _ardController.IsPortOpen()) {
				OnZubehoerServoAction(this.ZeichnenElemente.AktiverServo.ID, action);
				//if (keyData == Keys.Left) {
				//    OnZubehoerServoAction(this.ZeichnenElemente.AktiverServo.ID, ServoAction.LinksClick);
				//}
				//else if(keyData == Keys.Right) {
				//    OnZubehoerServoAction(this.ZeichnenElemente.AktiverServo.ID, ServoAction.RechtsClick);
				//}
			}
		}

	}
}
