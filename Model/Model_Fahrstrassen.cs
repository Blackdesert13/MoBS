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
//using MoBa.Anlagenkomponenten.ZeichnenElemente;

namespace MoBaSteuerung {
	//hier nur Methoden zum Anlage laden und Speichern

	/// <summary>
	/// Anlagenlogik
	/// </summary>
	public partial class Model : Control {

		public void FahrstrassenAuswahl(List<AnlagenElement> el) {
			this._zeichnenElemente.FahrstrassenElemente.HinzufügenAuswahl(el);
		}

		private void FahrstraßeStarten(object fahrstraße) {
			FahrstrasseN fs = (FahrstrasseN)fahrstraße;
			//Adresse adrStartGleis = fs.adrStartGleis;
			Thread.Sleep(this.FahrstraßenStartVerzögerung);
			if (/*adrStartGleis != null && */fs.IsAktiv) {
				fs.StartGleisEinschalten();
				//adrStartGleis.IsLocked = true;

				this.OnAnlageNeuZeichnen();
				this.OnAnlagenzustandChanged(null);
			}
		}

		/// <summary>
		/// Behandeln eine Rechtsklicks beim Bedienen 
		/// (Fahrstrassen suchen/aktivieren)
		/// </summary>
		/// <param name="p">Position des Klicks</param>
		/// <returns></returns>
		public List<AnlagenElement> BedienenMouseRightClick(Point p) {
			List<AnlagenElement> elemList = SucheElementAufPunkt(p);
			return elemList;
			if (elemList.Count > 0)
				if (elemList[0].GetType().Name == "Signal") {
					return FahrstrassenSignalSchalten((Signal)elemList[0]);
				}
			return null;
		}

		public bool FahrstrasseSchalten(FahrstrasseN el, FahrstrassenSignalTyp signalTyp) {
			if (el != null) {
				bool verlaengern = el.StartSignal.IsLocked;
				if (!el.IsAktiv && !verlaengern) {
					//Thread fahrstraßenStartThread = new Thread(this.FahrstraßeStarten);
					//fahrstraßenStartThread.Start(el);
				}
				bool action = el.AusgangToggeln(signalTyp, verlaengern && !el.StartSignal.AutoStart);
				//if (action && _ardController.IsPortOpen())
				//	OnAnlagenzustandChanged(el.Ausgang);
				if (signalTyp == FahrstrassenSignalTyp.ZielSignal) {
					this.ZeichnenElemente.ZugDateiSpeichern();
					OnZugListeChanged();
				}
				return action;
			}
			return false;
		}

		/// <summary>
		/// Schalten einer Fahrstraße über Tastatur
		/// </summary>
		/// <param name="signalNummer"></param>
		/// <returns></returns>
		public List<AnlagenElement> FahrstrassenSignal(int signalNummer, bool verlaengern) {
			Signal sn = _zeichnenElemente.SignalElemente.Element(signalNummer);
			if (sn != null) {
				return FahrstrassenSignalSchalten(sn, verlaengern);
			}
			return null;
		}


		public bool FahrstrassenSignalClick(int signalNummer, bool shift) {
			List<Elemente.AnlagenElement> el = FahrstrassenSignal(signalNummer, shift);
			if (el != null) {
				if (el.Count == 1) {
					if (((FahrstrasseN)el[0]).StartSignal.ID == signalNummer)
						return FahrstrasseSchalten((FahrstrasseN)el[0], FahrstrassenSignalTyp.StartSignal);
					else if (((FahrstrasseN)el[0]).EndSignal.ID == signalNummer)
						return FahrstrasseSchalten((FahrstrasseN)el[0], FahrstrassenSignalTyp.ZielSignal);
				}
				else {
					this.FahrstrassenAuswahl(el);
					return true;
				}
			}
			return false;
		}


		/// <summary>
		/// Fahrstrasse über Signal schalten.<para/>
		/// Wenn Fahrstraßen in der Vorauswahl sind, wird überprüft ob das ausgewählte Signal ein gültiges Zielsignal ist <para/>
		/// und die Fahrstrasse zurück gegeben und die Vorauswahl gelöscht.<para/>
		/// Ist keine Fahrstraße in der Vorauswahl wird überprüft ob das Signal zu einer aktiven Fahrstrasse gehört und <para/>
		/// diese zurückgegeben. Ist dies nicht der Fall werden alle verfügbaren Fahrstrassen, welche von diesem <para/>
		/// Signal ausgehen zurückgegeben.
		/// </summary>
		/// <param name="signal">Startsignal</param>
		/// <returns></returns>
		public List<AnlagenElement> FahrstrassenSignalSchalten(Signal signal, bool verlaengern) {
			List<AnlagenElement> el = new List<AnlagenElement>();
			if (_zeichnenElemente.FahrstrassenElemente.AuswahlFahrstrassen.Count == 0) {
				//prüft ob das Signal an einer aktiven FS beteiligt ist
				foreach (FahrstrasseN fs in _zeichnenElemente.FahrstrassenElemente.AktiveFahrstrassen) {
					if (signal == fs.StartSignal || signal == fs.EndSignal) {
						el.Add(fs);
					}
				}

				if (el.Count > 0) {
					if (el.Count != 1) {
						for (int i = 0; i < el.Count;) {
							if (((FahrstrasseN)el[i]).StartSignal == signal) {
								el.RemoveAt(i);
							}
							else {
								i++;
							}
						}
						while (el.Count != 1) {
							el.RemoveAt(1);
						}
						return el;
					}
					if (!verlaengern) {
						return el;
					}
					if (((FahrstrasseN)el[0]).StartSignal == signal) {
						return el;
					}
					el.Clear();
				}

				foreach (FahrstrasseN fs in _zeichnenElemente.FahrstrassenElemente.GespeicherteFahrstrassen) {
					if (fs.StartSignal == signal && fs.Verfuegbarkeit())
						el.Add(fs);
				}
				//zeichnenElemente.FahrstarssenElemente.SucheFahrstrassen((Signal)elemList[0]);
				return el;
			}
			else {
				foreach (FahrstrasseN fs in _zeichnenElemente.FahrstrassenElemente.AuswahlFahrstrassen)
					if (fs.EndSignal == signal) {
						_zeichnenElemente.FahrstrassenElemente.AlleLöschenAuswahl();
						el.Add(fs);
						//zeichnenElemente.FahrstarssenElemente.HinzufügenAktiv(fs);
						return el;
					}
				_zeichnenElemente.FahrstrassenElemente.AlleLöschenAuswahl();
				OnAnlageNeuZeichnen();
			}
			return null;
		}

		/// <summary>
		/// Schalten einer Fahrstraße über Tastatur
		/// </summary>
		/// <param name="signalNummer"></param>
		/// <returns></returns>
		public List<AnlagenElement> FahrstrassenSignal(int signalNummer) {
			Signal sn = _zeichnenElemente.SignalElemente.Element(signalNummer);
			if (sn != null) {
				return FahrstrassenSignalSchalten(sn);
			}
			return null;
		}


		/// <summary>
		/// Fahrstrasse über Signal schalten.<para/>
		/// Wenn Fahrstraßen in der Vorauswahl sind, wird überprüft ob das ausgewählte Signal ein gültiges Zielsignal ist <para/>
		/// und die Fahrstrasse zurück gegeben und die Vorauswahl gelöscht.<para/>
		/// Ist keine Fahrstraße in der Vorauswahl wird überprüft ob das Signal zu einer aktiven Fahrstrasse gehört und <para/>
		/// diese zurückgegeben. Ist dies nicht der Fall werden alle verfügbaren Fahrstrassen, welche von diesem <para/>
		/// Signal ausgehen zurückgegeben.
		/// </summary>
		/// <param name="signal">Startsignal</param>
		/// <returns></returns>
		public List<AnlagenElement> FahrstrassenSignalSchalten(Signal signal) {
			List<AnlagenElement> el = new List<AnlagenElement>();
			if (_zeichnenElemente.FahrstrassenElemente.AuswahlFahrstrassen.Count == 0) {
				foreach (FahrstrasseN fs in _zeichnenElemente.FahrstrassenElemente.AktiveFahrstrassen)
					if (signal == fs.StartSignal || signal == fs.EndSignal) {
						el.Add(fs);
						return el;
					}
				foreach (FahrstrasseN fs in _zeichnenElemente.FahrstrassenElemente.GespeicherteFahrstrassen) {
					if (fs.StartSignal == signal && fs.Verfuegbarkeit())
						el.Add(fs);
				}
				return el;
			}
			else {
				foreach (FahrstrasseN fs in _zeichnenElemente.FahrstrassenElemente.AuswahlFahrstrassen)
					if (fs.EndSignal == signal) {
						_zeichnenElemente.FahrstrassenElemente.AlleLöschenAuswahl();
						el.Add(fs);
						//zeichnenElemente.FahrstarssenElemente.HinzufügenAktiv(fs);
						return el;
					}
				_zeichnenElemente.FahrstrassenElemente.AlleLöschenAuswahl();
				OnAnlageNeuZeichnen();
			}
			return null;
		}
	}
}
