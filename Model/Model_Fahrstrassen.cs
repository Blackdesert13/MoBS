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

		/*   /// <summary>
		/// Überträgt den Zug auf das neue Signal
		/// </summary>
		/// <param name="NeuesSignal"></param>
		public void ZugWechsel(int NeuesSignal)
		{
				int neueSignalNr = NeuesSignal;
				if (zug != null)
						zug.SignalNummer = neueSignalNr;
		}*/
		private void FahrstraßeStarten(object fahrstraße) {
			FahrstrasseN fs = (FahrstrasseN)fahrstraße;
			//Adresse adrStartGleis = fs.adrStartGleis;
			Thread.Sleep(this.FahrstraßenStartVerzögerung);
			if (/*adrStartGleis != null && */fs.IsAktiv) {
				fs.StartGleisEinschalten();
				//adrStartGleis.IsLocked = true;

				this.OnAnlageNeuZeichnen();
				this.OnAnlagenzustandChanged();
				if (_ardController.IsPortOpen())
					this.OnAnlagenZustandAdresseChanged(null);
			}
		}

		/// <summary>
		/// Behandeln eine Rechtsklicks bein Bedienen 
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
					if (fs.StartSignal == signal && fs.AdressenFrei())
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
	}
}
