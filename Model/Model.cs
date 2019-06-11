﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Windows.Forms;
using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Delegates;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using MoBaSteuerung.Anlagenkomponenten.MCSpeicher;
using ModellBahnSteuerung.ZugEditor;
using MoBaSteuerung;
using MoBaSteuerung.Elemente;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading;
using MoBaSteuerung.ZeichnenElemente;
using MoBa.Elemente;
using ModellBahnSteuerung;
//using MoBa.Anlagenkomponenten.ZeichnenElemente;

namespace MoBaSteuerung
{
	/// <summary>
	/// Anlagenlogik
	/// </summary>
	public partial class Model : Control
	{

		#region Private Felder

		private AnzeigeTyp _anzeigeTyp;
		private AnlagenElemente _zeichnenElemente;
		private ArduinoController _ardController;
		private bool _master = false;
		private int _fahrstraßenStartVerzögerung;
		private int _zubehörServoSchrittweite;
		private string _connectedComPort = "";
		private string _anlageDateiName;

		/// <summary>
		/// Dateipfad+name.
		/// </summary>
		private string anlageDateiPfadName;

		/// <summary>
		/// 
		/// </summary>
		private bool _istAnlageSpeichernErforderlich;

		#endregion

		#region Öffentliche Eigenschaften (Properties)
		public int FahrstraßenStartVerzögerung
		{
			get
			{
				return _fahrstraßenStartVerzögerung;
			}

			set
			{
				_fahrstraßenStartVerzögerung = value;
			}
		}

		public int EntkupplerAbschaltAutoWert
		{
			get { return this.ZeichnenElemente.EntkupplerAbschaltAutoWert; }
			set { this.ZeichnenElemente.EntkupplerAbschaltAutoWert = value; }
		}

		public bool EntkupplerAbschaltAutoAktiv
		{
			get { return this.ZeichnenElemente.EntkupplerAbschaltAutoAktiv; }
			set { this.ZeichnenElemente.EntkupplerAbschaltAutoAktiv = value; }
		}

		/// <summary>
		/// alle AnlagenElementListen
		/// </summary>
		public AnlagenElemente ZeichnenElemente { get { return _zeichnenElemente; } }

		/// <summary>
		/// Speichern erforderlich.
		/// </summary>
		public bool IstAnlageSpeichernErforderlich
		{
			set
			{
				this._istAnlageSpeichernErforderlich = value;
			}
			get
			{
				return this._istAnlageSpeichernErforderlich;
			}
		}

		/// <summary>
		/// Anlagen Datei Name
		/// </summary>
		public string AnlageDateiName
		{
			get
			{
				return this._anlageDateiName;
			}
		}

		/// <summary>
		/// Anlage Datei Pfad Name
		/// </summary>
		public string AnlageDateiPfadName
		{
			get
			{
				return this.anlageDateiPfadName;
			}

			set
			{
				this.anlageDateiPfadName = value;
				// Dateiname übergeben
				if (string.IsNullOrEmpty(this.anlageDateiPfadName))
				{
					this._anlageDateiName = Constanten.STANDARDFILENAME;
				}
				else
				{
					this._anlageDateiName = Path.GetFileNameWithoutExtension(this.anlageDateiPfadName);
				}
			}
		}

		/// <summary>
		/// Art der Anzeige (Bedienen, Bearbeiten)
		/// </summary>
		public AnzeigeTyp Anzeigetyp
		{
			set
			{
				this._anzeigeTyp = value;
				this._zeichnenElemente.AnzeigeTyp = this._anzeigeTyp;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public Int32 Zoom { set { this._zeichnenElemente.Zoom = value; } }

		/// <summary>
		/// Server
		/// </summary>
		public bool Master
		{
			get { return _master; }
			set { _master = value; }
		}

		/// <summary>
		/// Gibt an ob Rückmeldung mit dargestellt werden soll
		/// </summary>
		public bool RückmeldungAnzeigen
		{
			set
			{
				this._zeichnenElemente.RückmeldungAnzeigen = value;
				OnAnlageNeuZeichnen();
			}
			get
			{
				return this._zeichnenElemente.RückmeldungAnzeigen;
			}
		}

		public bool RückmeldungAktiv
		{
			set
			{
				if (value != this._zeichnenElemente.RückmeldungAktiv)
				{
					if (value)
						_ardController.SendData(new byte[] { 1, 1, 0, 0, 2 });
					else
						_ardController.SendData(new byte[] { 1, 2, 0, 0, 3 });
				}
				this._zeichnenElemente.RückmeldungAktiv = value;
			}
			get
			{
				return this._zeichnenElemente.RückmeldungAktiv;
			}
		}

		public List<AnlagenElement> AuswahlElemente
		{
			get
			{
				return _auswahlElemente;
			}

			set
			{
				_auswahlElemente = value;
			}
		}

		public int ZubehörServoSchrittweite
		{
			get
			{
				return _zubehörServoSchrittweite;
			}

			set
			{
				_zubehörServoSchrittweite = value;
			}
		}
		#endregion

		#region Konstruktor(en)

		/// <summary>
		/// 
		/// </summary>
		public Model()
		{
			this._zeichnenElemente = new AnlagenElemente();
			this._ardController = new ArduinoController();
			this.AuswahlElemente = new List<AnlagenElement>();

			this._ardController.BefehlReceived += _ardController_BefehlReceived;
			this.AnlagenzustandAdresseChanged += Model_AnlagenzustandAdresseChanged;

			this.FahrstraßenStartVerzögerung = 1000;
			this.ZubehörServoSchrittweite = 1;
		}
		#endregion//Konstruktor(en)

		#region Mausclick-Befehle
		public int BearbeitenMouseDoubleClick(Point location)
		{
			List<AnlagenElement> elements = SucheElementAufPunkt(location);
			foreach (AnlagenElement x in elements)
			{
				frmProperties frm = new frmProperties(x);
				if (frm.ShowDialog(this) == DialogResult.OK) ;
				/*if (x.GetType().Name == "InfoFenster")
				{
					InfoFenster info = (InfoFenster)x;
					frmProperties frm = new frmProperties(info);
					if (frm.ShowDialog(this) == DialogResult.OK) ;
				}*/
			}
					return 0;
		}

		public int BedienenMouseDoubleClick(Point location)
		{
			List<AnlagenElement> elements = SucheElementAufPunkt(location);
			foreach(AnlagenElement x in elements)
			{
				if(x.GetType().Name == "Signal")
				{//
					Signal sn = (Signal)x;
					if (sn.ZugNr > 0)
					{ 				
						frmZugEditor frm = new frmZugEditor(this.ZeichnenElemente,sn.ZugNr);
						if (frm.ShowDialog(this) == DialogResult.OK)
						{
							//model.
							// string[][] t =  frm.auslesen();
						}
						else
						{
						}
					}
				}
			}
			return 0;
		}

		/// <summary>
		/// Behandeln eines MausClicks beim Bedienen
		/// </summary>
		/// <param name="p">Position des Mausklicks</param>
		/// <returns></returns>
		public List<AnlagenElement> BedienenMouseClick(Point p)
		{
			_zeichnenElemente.FahrstrassenElemente.AuswahlFahrstrassen.Clear();
			List<AnlagenElement> elemList = SucheElementAufPunkt(p);
			return elemList;
		}
		/// <summary>
		/// Sucht nach Anlagenelementen, auf welche geclickt wurde
		/// </summary>
		/// <param name="punkt">Position des Clicks</param>
		/// <returns></returns>
		private List<AnlagenElement> SucheElementAufPunkt(Point punkt)
		{
			List<AnlagenElement> elemList = new List<AnlagenElement> { };
			foreach (Entkuppler el in _zeichnenElemente.EntkupplerElemente.Elemente)
				if (el.MouseClick(punkt))
					elemList.Add(el);
			foreach (Schalter el in _zeichnenElemente.SchalterElemente.Elemente)
				if (el.MouseClick(punkt))
					elemList.Add(el);
			foreach (FSS el in _zeichnenElemente.FssElemente.Elemente)
				if (el.MouseClick(punkt))
				{
					elemList.Add(el);
				}
			foreach (Signal el in _zeichnenElemente.SignalElemente.Elemente)
				if (el.MouseClick(punkt))
					elemList.Add(el);
			foreach (Servo el in _zeichnenElemente.ServoElemente.Elemente)
				if (el.MouseClick(punkt))
					elemList.Add(el);
			foreach (Weiche el in _zeichnenElemente.WeicheElemente.Elemente)
				if (el.MouseClick(punkt))
					elemList.Add(el);
			foreach (Gleis el in _zeichnenElemente.GleisElemente.Elemente)
				if (el.MouseClick(punkt))
					elemList.Add(el);
			foreach (StartSignalGruppe el in _zeichnenElemente.SsgElemente.Elemente)
				if (el.MouseClick(punkt))
					elemList.Add(el);
			foreach (InfoFenster el in _zeichnenElemente.InfoElemente.Elemente)
				if (el.MouseClick(punkt))
					elemList.Add(el);
			if (this.ZeichnenElemente.AktiverServoAction != ServoAction.None && this.ZeichnenElemente.AktiverServo != null)
			{
				OnZubehoerServoAction(this.ZeichnenElemente.AktiverServo.ID, this.ZeichnenElemente.AktiverServoAction);
				this.ZeichnenElemente.AktiverServoAction = ServoAction.None;
			}
			return elemList;
		}
		#endregion//Mausclick-Befehle


		#region Servo
		public bool ServoBewegungManuell(int servo, ServoAction action)
		{
			Servo s = this.ZeichnenElemente.ServoElemente.Element(servo);
			if (s != null)
			{
				byte[] befehl = new byte[5];
				befehl[0] = (byte)s.Ausgang.ArdNr;
				befehl[1] = 72;
				befehl[2] = (byte)(s.Ausgang.AdressenNr * 2 + s.Ausgang.BitNr);
				if (action == ServoAction.LinksClick || action == ServoAction.RechtsClick)
				{
					befehl[2] |= 0x080;
					befehl[3] = (byte)ZubehörServoSchrittweite;
					if (action == ServoAction.LinksClick)
					{
						befehl[3] = (byte)-befehl[3];
					}
				}
				else if (action == ServoAction.RechtsHold || action == ServoAction.LinksHold || action == ServoAction.HoldStop)
				{
					befehl[2] |= 0x40;
					switch (action)
					{
						case ServoAction.RechtsHold:
							befehl[3] = 255;
							break;
						case ServoAction.LinksHold:
							befehl[3] = 1;
							break;
						case ServoAction.HoldStop:
							befehl[3] = 0;
							break;
					}
				}
				befehl[4] = (byte)((befehl[0] + befehl[1] + befehl[2] + befehl[3]) % 256);
				_ardController.SendData(befehl);
			}
			return false;
		}
		#endregion//Servo

		#region Öffentliche Methoden

		/// <summary>
		/// Liest ankommende Befehle vom ArduinoController und verarbeitet diese weiters
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="a"></param>
		private void _ardController_BefehlReceived(object sender, BefehlEventArgs a)
		{
			byte[] befehl = a.Befehl;
			string befehlS = String.Empty;
			foreach (byte b in befehl)
				befehlS += b + " ";
			Debug.Print("Befehl von Arduino: " + befehlS);

			if (befehl.Length == 5)
			{
				Arduino arduino = this._zeichnenElemente.AnlagenZustand.GetArduino(befehl[0]);
				if (arduino != null)
				{
					switch (befehl[1])
					{
						case 10:
							arduino.Rueckmeldung[0] = (ushort)(befehl[2] + befehl[3] * 256);
							OnArduinoRueckmeldungReceived();
							break;
						case 11:
							arduino.Rueckmeldung[1] = (ushort)(befehl[2] + befehl[3] * 256);
							OnArduinoRueckmeldungReceived();
							break;
						case 40:
							arduino.Ausgaenge[0] = (ushort)(befehl[2] + befehl[3] * 256);
							OnArduinoRueckmeldungReceived();
							break;
						case 41:
							arduino.Ausgaenge[1] = (ushort)(befehl[2] + befehl[3] * 256);
							OnArduinoRueckmeldungReceived();
							break;
						case 42:
							arduino.Ausgaenge[2] = (ushort)(befehl[2] + befehl[3] * 256);
							OnArduinoRueckmeldungReceived();
							break;
						case 99:
							InfoFenster sysZeit = this._zeichnenElemente.InfoElemente.Element(99);
							int zeit = befehl[2] + befehl[3] * 256;
							string txt = "Ard-Zeit: ";
							txt = txt + zeit + "_";// Convert.ToString( zeit);
							try
							{
								if (sysZeit != null) { sysZeit.Text = txt; }
							}
							catch (Exception e)
							{
								Debug.Print(e.Message);
							}
							OnArduinoRueckmeldungReceived();
							break;
					}

					if (befehl[1] > 99 && befehl[1] < 122)
					{
						int id = befehl[1] - 100;
						Haltestelle haltestelle = this._zeichnenElemente.HaltestellenElemente.Element(id);
						try
						{
							if (haltestelle != null)
							{
								haltestelle.InfoBefehl(befehl);
							}
						}
						catch (Exception e)
						{
							Debug.Print(e.Message);
						}
						OnArduinoRueckmeldungReceived();
					}
				}
			}
		}
		#endregion

		/// <summary>
		/// Wenn Zustand in einer Adresse geändert wurde, wird dieser über den ArduinoController gesendet.
		/// Wird keine Adresse an die Methode übergeben, werden alle Adressen ausgegeben.
		/// </summary>
		/// <param name="adresse">Adresse in der sich ein Zustand geändert hat</param>
		private void Model_AnlagenzustandAdresseChanged(Adresse adresse)
		{
			if (adresse != null && adresse.ArdNr > 1)
			{
				byte[] befehl = this._zeichnenElemente.AnlagenZustand.GetBefehl(adresse.ArdNr, adresse.AdressenNr);
				if (befehl != null)
				{
					Debug.Print("Sende an Arduino Befehl: " + befehl[0] + " " + befehl[1]
													+ " " + befehl[2] + " " + befehl[3] + " " + befehl[4] + " ");
					this._ardController.SendData(befehl);
				}
			}
			else
			{
				foreach (Arduino arduino in _zeichnenElemente.AnlagenZustand.ArduinoListe)
				{
					for (int i = 0; i < arduino.Ausgaenge.Length; i++)
						this._ardController.SendData(this._zeichnenElemente.AnlagenZustand.GetBefehl(arduino.Nr, i));
				}
			}
		}

		/// <summary>
		/// Umschalten eines Elementes
		/// </summary>
		/// <param name="elementName">Elementtyp</param>
		/// <param name="nr">ID des Elementes, welches geschaltet werden soll</param>
		/// <returns></returns>
		public bool ElementToggeln(string elementName, int nr)
		{
			AnlagenElement el = null;
			switch (elementName)
			{
				case "StartSignalGruppe":
					StartSignalGruppe ssg = _zeichnenElemente.SsgElemente.Element(nr);
					int signalId = ssg.FSAuswahl();
					if(signalId != 0) {
						FahrstrassenSignalClick(signalId, false);
						return true;
					}
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

						FahrstrasseSchalten((FahrstrasseN)el, FahrstrassenSignalTyp.ZielSignal);
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

			if (el != null)
			{
				bool action = el.AusgangToggeln();
				//if (elementName == "FSS")
				_zeichnenElemente.FSSAktualisieren();
				if (elementName == "Entkuppler")
				{
					if (el.ElementZustand == Elementzustand.An && EntkupplerAbschaltAutoAktiv)
					{
						Thread entkupplerAbschalt = new Thread(this.EntkupplerAbschaltung);
						entkupplerAbschalt.Start(el);
					}
				}
				if (action && _ardController.IsPortOpen())
					OnAnlagenZustandAdresseChanged(null);
				return action;
			}

			return false;
		}

		/// <summary>
		/// Alles neu zeichnen im Bedienmodus
		/// </summary>
		/// <param name="graphics"></param>
		public void ZeichneElementeBedienen(Graphics graphics)
		{
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			this._zeichnenElemente.FSSAktualisieren();
			this._zeichnenElemente.ZuegeAktualisieren();
			this._zeichnenElemente.GleisElemente.ElementeZeichnen1(graphics);
			this._zeichnenElemente.GleisElemente.ElementeZeichnen(graphics);
			this._zeichnenElemente.WeicheElemente.ElementeZeichnen(graphics);
			this._zeichnenElemente.FahrstrassenElemente.ElementeZeichnen(graphics);
			this._zeichnenElemente.SsgElemente.ElementeZeichnen(graphics);
			this._zeichnenElemente.SchalterElemente.ElementeZeichnen(graphics);
			this._zeichnenElemente.FssElemente.ElementeZeichnen(graphics);
			this._zeichnenElemente.EntkupplerElemente.ElementeZeichnen(graphics);
			this._zeichnenElemente.SignalElemente.ElementeZeichnen(graphics);
			this._zeichnenElemente.InfoElemente.ElementeZeichnen(graphics);
			this._zeichnenElemente.ReglerElemente.ElementeZeichnen(graphics);
			this._zeichnenElemente.ServoElemente.ElementeZeichnen(graphics);
			graphics.SmoothingMode = SmoothingMode.Default;
		}

		/// <summary>
		/// Alles neu zeichnen im Bearbeitungsmodus.
		/// </summary>
		/// <param name="graphics"></param>
		public void ZeichneElementeBearbeiten(Graphics graphics)
		{
			graphics.SmoothingMode = SmoothingMode.HighQuality;

			this._zeichnenElemente.GleisElemente.ElementeZeichnen(graphics);
			this._zeichnenElemente.WeicheElemente.ElementeZeichnen(graphics);
			this._zeichnenElemente.FahrstrassenElemente.ElementeZeichnen(graphics);
			this._zeichnenElemente.KnotenElemente.ElementeZeichnen(graphics);
			this._zeichnenElemente.SchalterElemente.ElementeZeichnen(graphics);
			this._zeichnenElemente.EntkupplerElemente.ElementeZeichnen(graphics);
			this._zeichnenElemente.SignalElemente.ElementeZeichnen(graphics);
			this._zeichnenElemente.InfoElemente.ElementeZeichnen(graphics);
			this._zeichnenElemente.FssElemente.ElementeZeichnen(graphics);
			this._zeichnenElemente.ReglerElemente.ElementeZeichnen(graphics);
			this._zeichnenElemente.ServoElemente.ElementeZeichnen(graphics);

			if (this._neuesElement != null)
				this._neuesElement.ElementZeichnen(graphics);

			graphics.SmoothingMode = SmoothingMode.Default;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="graphicsPath"></param>
		/// <returns></returns>
		public bool AuswahlRechteckElemente(GraphicsPath graphicsPath)
		{
			// ToDo sind Elemente im Auswahlrechteck


			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="graphicsPath"></param>
		public void AuswahlRechteckVerschieben(GraphicsPath graphicsPath)
		{
			//To Do Verschieben.

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="graphicsPath"></param>
		public void AuswahlRechteckElementeLöschen(GraphicsPath graphicsPath)
		{
			//To Do löschen.

		}


		public string StringBereinigen(string EingangsString)
		{
			string startString = EingangsString;// null;//"  AA A";

			string ergebnis = "";
			if (startString != null)
			{
				string[] arbeitsArray = startString.Split(' ');
				for (int a = 0; a < arbeitsArray.Length; a++)
				{
					for (int b = a + 1; b < arbeitsArray.Length; b++)
					{
						if (arbeitsArray[a] == arbeitsArray[b]) { arbeitsArray[b] = ""; }
					}
					if (arbeitsArray[a] != "")
					{
						if (ergebnis != "") ergebnis = ergebnis + " ";
						ergebnis = ergebnis + arbeitsArray[a];
					}
				}
			}

			return ergebnis;
		}

		#region ComPort
		public string UpdateComPorts()
		{
			string result = "";
			if (_connectedComPort != "")
			{
				string[] ports = _ardController.GetSerialPortNames();

				bool contain = false;
				foreach (string item in ports)
				{
					if (_connectedComPort == item)
					{
						contain = true;
						break;
					}
				}
				if (!contain)
				{
					try
					{
						if (!_ardController.CloseComPort())
						{
							_ardController.BefehlReceived -= _ardController_BefehlReceived;
							_ardController.Dispose();
							_ardController = new ArduinoController();
							_ardController.BefehlReceived += _ardController_BefehlReceived;
						}
					}
					catch (Exception e)
					{

					}
					result = "ComPort " + _connectedComPort + " getrennt";
				}
				else
				{
					if (!_ardController.CloseComPort())
					{
						_ardController.BefehlReceived -= _ardController_BefehlReceived;
						_ardController.Dispose();
						_ardController = new ArduinoController();
						_ardController.BefehlReceived += _ardController_BefehlReceived;
					}
					if (_ardController.OpenComPort(_connectedComPort, false))
					{
						result = "ComPort " + _connectedComPort + " neu verbunden";
					}
					else
					{
						result = "ComPort " + _connectedComPort + " getrennt";
					}

				}
			}
			return result;
		}
		#endregion//ComPort
		#region Events
		#region EeventHandler

		/// <summary>
		/// 
		/// </summary>
		public event ViewNeuZeichnenEventHandler AnlageNeuZeichnen;


		/// <summary>
		/// 
		/// </summary>
		public event SizeEventHandler AnlageGrößeInRasterChanged;

		/// <summary>
		/// 
		/// </summary>
		public event ArduinoRueckmeldungEventHandler ArduinoRueckmeldungReceived;

		/// <summary>
		/// 
		/// </summary>
		public event ArduinoSendEventHandler AnlagenzustandAdresseChanged;

		/// <summary>
		/// 
		/// </summary>
		public event ZugListeChangedEventHandler ZugListeChanged;

		/// <summary>
		/// 
		/// </summary>
		public event AnlagenZustandEventHandler AnlagenzustandChanged;

		public event ServoBewegungEventHandler ZubehoerServoAction;

		#endregion



		/// <summary>
		/// 
		/// </summary>
		public virtual void OnAnlageNeuZeichnen()
		{
			if (this.AnlageNeuZeichnen != null)
			{
				this.AnlageNeuZeichnen();
			}
		}

		protected virtual void OnArduinoRueckmeldungReceived()
		{
			if (this.ArduinoRueckmeldungReceived != null)
			{
				this.ArduinoRueckmeldungReceived();
			}
		}

		protected virtual void OnZubehoerServoAction(int servo, ServoAction richtung)
		{
			if (this.ZubehoerServoAction != null)
			{
				this.ZubehoerServoAction(servo, richtung);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		protected virtual void OnAnlagenZustandAdresseChanged(Adresse adresse)
		{
			if (this.AnlagenzustandAdresseChanged != null)
			{
				this.AnlagenzustandAdresseChanged(adresse);
			}
		}

		/// <summary>
		/// Wird ausgelösst, wenn die Größe geändert wurde.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnAnlageGrößeInRasterChanged(Size e)
		{
			if (this.AnlageGrößeInRasterChanged != null)
			{
				this.AnlageGrößeInRasterChanged(e);
			}
		}

		protected virtual void OnAnlagenzustandChanged()
		{
			if (this.AnlagenzustandChanged != null)
			{
				this.AnlagenzustandChanged();
			}
		}

		protected virtual void OnZugListeChanged() {
			if (this.ZugListeChanged != null) {
				this.ZugListeChanged();
			}
		}

		/// <summary>
		/// liefert die Zugliste
		/// </summary>
		/// <returns></returns>
		public ElementListe<Zug> GetZugListe()
		{
			return _zeichnenElemente.ZugElemente;
		}

		/// <summary>
		/// liefert alle verfügbaren Portnamen
		/// </summary>
		/// <returns></returns>
		public string[] GetSerialPortNames()
		{
			return _ardController.GetSerialPortNames();
		}

		public bool OpenComPort(string portName)
		{
			if (this._ardController.OpenComPort(portName))
			{
				_connectedComPort = portName;
				if (_zeichnenElemente.RückmeldungAktiv)
					_ardController.SendData(new byte[] { 1, 1, 0, 0, 2 });
				else
					_ardController.SendData(new byte[] { 1, 2, 0, 0, 3 });
				return true;
			}
			return false;
		}

		public bool CloseComPort()
		{
			bool result = this._ardController.CloseComPort();
			if (!result)
			{
				_ardController.BefehlReceived -= _ardController_BefehlReceived;
				_ardController.Dispose();
				_ardController = new ArduinoController();
				_ardController.BefehlReceived += _ardController_BefehlReceived;
			}
			_connectedComPort = "";
			return true;
		}



		/// <summary>
		/// Löscht die gegenwärtige Auswahl (Selektion) im Bedienmodus, gegenwärtig können dies nur Fahrstraßen sein
		/// </summary>
		/// <returns>Gibt TRUE zurück, wenn eine Auswahl gelöscht wurde</returns>
		public bool BedienenAuswahlLöschen()
		{
			if (_zeichnenElemente.FahrstrassenElemente.AuswahlFahrstrassen.Count > 0)
			{
				_zeichnenElemente.FahrstrassenElemente.AlleLöschenAuswahl();
				return true;
			}
			return false;
		}


		public bool NeuesElementVorschau(BearbeitungsModus bearbeitungsModus, Point letzterRasterpunkt, int zoom)
		{
			if (this._neuesElement == null)
			{
				switch (bearbeitungsModus)
				{
					case BearbeitungsModus.Gleis:
						this._neuesElement = new Knoten(this._zeichnenElemente, zoom, this._anzeigeTyp, letzterRasterpunkt);
						break;
					case BearbeitungsModus.Schalter:
						this._neuesElement = new Schalter(this._zeichnenElemente, zoom, this._anzeigeTyp, letzterRasterpunkt);
						break;
					case BearbeitungsModus.Signal:
						this._neuesElement = new Signal(this._zeichnenElemente, zoom, this._anzeigeTyp, letzterRasterpunkt, false);
						break;
					case BearbeitungsModus.Entkuppler:
						this._neuesElement = new Entkuppler(this._zeichnenElemente, zoom, this._anzeigeTyp, letzterRasterpunkt);
						break;
					case BearbeitungsModus.Fss:
						this._neuesElement = new FSS(this._zeichnenElemente, zoom, this._anzeigeTyp, letzterRasterpunkt);
						break;
					case BearbeitungsModus.InfoElement:
						this._neuesElement = new InfoFenster(this._zeichnenElemente, zoom, this._anzeigeTyp, letzterRasterpunkt);
						break;
				}
			}
			else
			{
				switch (bearbeitungsModus)
				{
					case BearbeitungsModus.Gleis:
						if (this._neuesElement.GetType().Name == "Knoten")
						{
							((RasterAnlagenElement)this._neuesElement).PositionRaster = letzterRasterpunkt;
							this._neuesElement.BearbeitenAktualisierenNeuZeichnen();
						}
						else
						{
							Gleis gl = (Gleis)this._neuesElement;
							gl.EndKn = new Knoten(this._zeichnenElemente, zoom, this._anzeigeTyp, letzterRasterpunkt);
							gl.BearbeitenAktualisierenNeuZeichnen();
						}
						break;
					case BearbeitungsModus.InfoElement:
					case BearbeitungsModus.Signal:
					case BearbeitungsModus.Entkuppler:
					case BearbeitungsModus.Schalter:
					case BearbeitungsModus.Fss:
						((RasterAnlagenElement)this._neuesElement).PositionRaster = letzterRasterpunkt;
						this._neuesElement.BearbeitenAktualisierenNeuZeichnen();
						break;
				}
			}
			return true;
		}



		#endregion


		public void BedienenServoManuell(ServoAction action)
		{
			Debug.Print("Servo Action " + action);
			if (this._zeichnenElemente.AktiverServo != null && _ardController.IsPortOpen())
			{
				OnZubehoerServoAction(this.ZeichnenElemente.AktiverServo.ID, action);
				//if (keyData == Keys.Left) {
				//    OnZubehoerServoAction(this.ZeichnenElemente.AktiverServo.ID, ServoAction.LinksClick);
				//}
				//else if(keyData == Keys.Right) {
				//    OnZubehoerServoAction(this.ZeichnenElemente.AktiverServo.ID, ServoAction.RechtsClick);
				//}
			}
		}
		private void EntkupplerAbschaltung(object entkuppler)
		{
			Entkuppler el = (Entkuppler)entkuppler;
			Thread.Sleep(this.EntkupplerAbschaltAutoWert * 1000);
			if (el.ElementZustand == Elementzustand.An)
			{
				el.AusgangToggeln();

				this.OnAnlageNeuZeichnen();
				this.OnAnlagenzustandChanged();
				if (_ardController.IsPortOpen())
					this.OnAnlagenZustandAdresseChanged(el.Ausgang);
			}
		}
	}
}