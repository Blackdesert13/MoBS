using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Timers;
using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using MoBaSteuerung.Anlagenkomponenten.MCSpeicher;
using System.Text.RegularExpressions;
using System.Linq;
using MoBaSteuerung.ZeichnenElemente;

namespace MoBaSteuerung.Elemente {
	
	/// <summary>
	/// aktuell verwendete Fahrstrasse
	/// </summary>
	public class FahrstrasseK : AnlagenElement {
		private GraphicsPath _fahrstrLinie = new GraphicsPath();
		private AnlagenElement[] _listeElemente;
		private Signal _startSignal;
		private Signal _endSignal;
		private bool _isAktiv = false;
		private List<Befehl> _startBefehle;
		private List<Befehl> _startGleise;
		private List<Befehl> _endBefehle;
		private List<Adresse> _streckenRM;
		private Gleis _zielGleis;

		#region Properties
		/// <summary>
		/// 
		/// </summary>
		public override string SpeicherString {
			get {
				//int zielGlNr = 0;
				//if (_zielGleis != null) { zielGlNr = _zielGleis.ID; }
				string txt = "FahrstrasseK"
										+ "\t" + ID
										+ "\t" + StartSignal.ID
										+ "\t" + EndSignal.ID
										+ "\tFalse"
										+ "\tFalse";
				txt += Environment.NewLine + " BefehlStart" + StartBefehleString;

				txt += Environment.NewLine + " BefehlZiel" + this.AuslesenBefehlsliste(_endBefehle);

				txt += Environment.NewLine + " KnotenListe";
				foreach (var item in _listeElemente) {
					if (item.GetType().Name == "Knoten")
						txt += "\t" + item.ID;
				}
				return txt;
			}
		}
		/// <summary>
		/// gibt an ob FS aktiv ist
		/// </summary>
		public bool IsAktiv {
			get {
				return _isAktiv;
			}
			set {
				_isAktiv = value;
				this.Ausgang.Stellung = value;
			}
		}

		public Signal EndSignal {
			get {
				return _endSignal;
			}
		}

		public Signal StartSignal {
			get {
				return _startSignal;
			}
		}

		public string StartBefehleString {
			get {
				string value = "";
				if (_startGleise != null) { value += this.AuslesenBefehlsliste(_startGleise); }
				value += this.AuslesenBefehlsliste(_startBefehle);
				return value;
			}
			set {
				string[] spString = value.Trim().Split(';');
				_startBefehle = new List<Befehl>();
				_startGleise = null;
				this.EinlesenBefehlsliste(_startBefehle, spString, true);
				StartGleiseAktualisieren();
			}
		}

		public string EndBefehleString {
			get {
				return this.AuslesenBefehlsliste(_endBefehle);
			}
			set {
				string[] spString = value.Trim().Split(';');
				_endBefehle = new List<Befehl>();
				this.EinlesenBefehlsliste(_endBefehle, spString, true);
			}
		}

		public AnlagenElement[] ListeElemente {
			get {
				return _listeElemente;
			}
			set {
				_listeElemente = value;
			}
		}

		public List<Befehl> StartBefehle {
			get {
				return _startBefehle;
			}

			set {
				_startBefehle = value;
			}
		}

		public List<Befehl> EndBefehle {
			get {
				return _endBefehle;
			}

			set {
				_endBefehle = value;
			}
		}
		#endregion//Properties
		#region Konstuktoren
		/// <summary>
		/// erstellt FS aus Serialisierung (für Slave)
		/// </summary>
		/// <param name="elementListe">Serialisierung</param>
		public FahrstrasseK(AnlagenElement[] elementListe)
		: base(elementListe[0].Parent, elementListe[0].Parent.FahrstrassenElemente.SucheFreieNummer(), elementListe[0].Zoom, elementListe[0].AnzeigenTyp) {
			_listeElemente = elementListe;
			_startSignal = (Signal)elementListe[0];
			_endSignal = (Signal)elementListe[elementListe.Length - 1];
			SucheAdressen();
			Berechnung();
		}

		public FahrstrasseK(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, string[] elem, string befehleStart, string befehleZiel, string knotenListe)
				: base(parent, Convert.ToInt32(elem[1]), zoom, anzeigeTyp) {
			/*	int zielGleisNr = 0;
				//bool result =
				int.TryParse(elem[4], out zielGleisNr);
				if(zielGleisNr!=0){
					_zielGleis = parent.GleisElemente.Element(Convert.ToInt32(elem[4]));}*/

			int startS = Convert.ToInt16(elem[2]);
			int zielS = Convert.ToInt16(elem[3]);
			if (startS > 0) {
				_startSignal = parent.SignalElemente.Element(startS);
			}
			else
				return;
			if (zielS > 0) {
				_endSignal = parent.SignalElemente.Element(zielS);
				if (_endSignal != null) {
					if (_endSignal.ZielGleisNr > 0) { _zielGleis = parent.GleisElemente.Element(_endSignal.ZielGleisNr); }
				}
			}
			else
				return;

			string[] spString;
			spString = befehleStart.Trim().Split('\t');
			if (spString[0] == "BefehlStart") {
				_startBefehle = new List<Befehl>();
				if (!this.EinlesenBefehlsliste(_startBefehle, spString))
					return;
			}
			else
				return;

			spString = befehleZiel.Trim().Split('\t');
			if (spString[0] == "BefehlZiel") {
				_endBefehle = new List<Befehl>();
				if (!this.EinlesenBefehlsliste(_endBefehle, spString))
					return;
			}
			else
				return;

			spString = knotenListe.Split('\t');
			if (spString[0] == " KnotenListe") {
				List<AnlagenElement> knoten = new List<AnlagenElement>();
				for (int i = 1; i < spString.Length; i++) {
					knoten.Add(parent.KnotenElemente.Element(Convert.ToInt16(spString[i])));
				}
				_listeElemente = knoten.ToArray();
			}
			else
				return;

			//parent.FahrstrassenElemente.GespeicherteFahrstrassen.Add(this);
			Berechnung();
		}
		#endregion//Konstruktoren
		private string AuslesenBefehlsliste(List<Befehl> list) {
			string spString = String.Empty;
			foreach (Befehl befehl in list) {
				spString += "\t";
				AnlagenElement el = befehl.Element;
				if (el is Gleis) {
					spString += "Gl" + el.ID + ":" + (befehl.SchaltZustand ? "An" : "Aus");
				}
				else if (el is Signal) {
					spString += "Sn" + el.ID + ":" + (befehl.SchaltZustand ? "An" : "Aus");
				}
				else if (el is Weiche) {
					spString += "We" + el.ID + ":" + (befehl.SchaltZustand ? "An" : "Aus");
				}
				else if (el is FSS) {
					spString += "Fss" + el.ID + ":" + (befehl.SchaltZustand ? "An" : "Aus");
				}
			}
			return spString;
		}

		private bool EinlesenBefehlsliste(List<Befehl> list, string[] spString, bool all = false) {
			int i = 1;
			if (all = true) {
				i = 0;
			}

			for (; i < spString.Length; i++) {
				string[] befehl = spString[i].Split(':');
				string[] elName = Regex.Matches(befehl[0], @"[a-zA-Z]+|\d+").Cast<Match>().Select(m => m.Value).ToArray();
				AnlagenElement el = null;
				switch (elName[0]) {
					case "Gl":
						el = this.Parent.GleisElemente.Element(Convert.ToInt16(elName[1]));
						break;
					case "Sn":
						el = this.Parent.SignalElemente.Element(Convert.ToInt16(elName[1]));
						break;
					case "We":
						el = this.Parent.WeicheElemente.Element(Convert.ToInt16(elName[1]));
						break;
					case "Fss":
						el = this.Parent.FssElemente.Element(Convert.ToInt16(elName[1]));
						break;
					default:
						break;
				}
				if (el != null) {
					bool zustand = false;
					if (befehl[1] == "An")
						zustand = true;
					else if (befehl[1] != "Aus")
						return false;
					list.Add(new Befehl(el, zustand));
				}

			}
			return true;
		}

		private void SucheAdressen() {
			_endBefehle = new List<Befehl>();
			_startBefehle = new List<Befehl>();
			Adresse snAdr;
			Adresse adr;
			bool stellungZielElemente = true;
			for (int i = 0; i < _listeElemente.Length; i++) {
				stellungZielElemente = (i < _listeElemente.Length - 2);
				switch (_listeElemente[i].GetType().Name) {
					case "Signal":
						_startBefehle.Add(new Befehl(_listeElemente[i], stellungZielElemente));
						_endBefehle.Add(new Befehl(_listeElemente[i], false));
						break;
					case "Knoten":
						Knoten kn = (Knoten)_listeElemente[i];
						Gleis vergl = (Gleis)_listeElemente[i - 1];
						Weiche we = null;
						for (int j = 0; j < 4; j++) {
							if (vergl == (Gleis)kn.Gleise[j]) {
								if (j < 2)
									we = kn.Weichen[0];
								else
									we = kn.Weichen[1];

								if (we != null) {
									_startBefehle.Add(new Befehl(we, ((j % 2) == 0) ^ we.Grundstellung));
								}
								break;
							}
						}
						if (_listeElemente[i + 1] is Gleis) {
							vergl = (Gleis)_listeElemente[i + 1];
							for (int j = 0; j < 4; j++) {
								if (vergl == (Gleis)kn.Gleise[j]) {
									if (j < 2)
										we = kn.Weichen[0];
									else
										we = kn.Weichen[1];

									if (we != null) {
										_startBefehle.Add(new Befehl(we, ((j % 2) == 0) ^ we.Grundstellung));
									}
									break;
								}
							}
						}
						break;
					case "Gleis":
						snAdr = ((Gleis)_listeElemente[i]).Ausgang;
						if (snAdr.ArdNr > 0) {
							_startBefehle.Add(new Befehl(_listeElemente[i], stellungZielElemente));
							adr = new Adresse(Parent, snAdr.ArdNr, snAdr.AdressenNr, snAdr.BitNr);
							adr.Stellung = false;
							_endBefehle.Add(new Befehl(_listeElemente[i], false));
						}
						break;
				}
			}
			//AdressListebereinigen(_endBefehle);
			//AdressListebereinigen(_startBefehle);
		}

		private void AdressListebereinigen(List<Befehl> adrListe) {
			int i = adrListe.Count - 1;
			while (i > 0) {
				int anz = 1;
				for (int j = i - 1; j >= 0; j--) {
					if (adrListe[j].Element.Ausgang.GleicheAdresse(adrListe[i - anz + 1].Element.Ausgang)) {
						adrListe.RemoveAt(j);
						anz++;
					}
				}
				i = i - anz;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="graphics"></param>
		public override void ElementZeichnen(Graphics graphics) {
			Pen stift = new Pen(Color.Yellow, (Single)(this.Zoom * 0.2));
			switch (this.ElementZustand) {
				case Elementzustand.An:
					stift.Color = Color.Red;
					break;
				case Elementzustand.Aus:
					return;
				case Elementzustand.Selektiert:
					stift.Color = Color.Yellow;
					break;
			}

			stift.EndCap = LineCap.Round;
			stift.LineJoin = LineJoin.Round;
			stift.StartCap = LineCap.Round;

			graphics.DrawPath(stift, _fahrstrLinie);
		}

		/// <summary>
		/// sucht Start-Gleise aus der Startbefehlsliste
		/// </summary>
		private void StartGleiseAktualisieren() {
			if (_startGleise == null) {
				_startGleise = new List<Befehl>();
				for (int i = 0; i < _startBefehle.Count;) {
					Befehl befehl = _startBefehle[0];
					if (befehl.Element is Signal) {
						break;
					}
					_startBefehle.Remove(befehl);
					_startGleise.Add(befehl);
				}
				if (_startGleise.Count == 0) {
					Befehl befehl = null;
					foreach (Befehl element in _startBefehle) {
						if (element.Element is Gleis) {
							befehl = element;
							break;
						}
					}
					if (befehl != null) {
						_startBefehle.Remove(befehl);
						_startGleise.Add(befehl);
					}
				}
			}
		}

		public override void Berechnung() {
			_fahrstrLinie.Reset();

			StartGleiseAktualisieren();

			List<Point> punkte = new List<Point> { };
			punkte.Add(_startSignal.PositionRaster);
			foreach (AnlagenElement el in _listeElemente)
				if (el.GetType().Name != "Gleis")
					punkte.Add(((RasterAnlagenElement)el).PositionRaster);
			punkte.Add(_endSignal.PositionRaster);

			_fahrstrLinie.AddLines(punkte.ToArray());
			Matrix matrix = new Matrix();
			matrix.Scale(Zoom, Zoom);
			_fahrstrLinie.Transform(matrix);
		}

		public void AdressenSperren() {
			for (int i = 0; i < _startBefehle.Count; i++) {
				if (i != 1) {
					_startBefehle[i].Element.IsLocked = true;
				}
			}
			_startSignal.IsLocked = false;
		}

		/// <summary>
		/// prüft die Verfügbarkeit der FS (Adr.-Block., Zielsignal-Belegung, Zug-Länge)
		/// soll AdressenFrei ersetzen
		/// </summary>
		/// <returns></returns>
		public bool Verfuegbarkeit()//Verfuegbarkeit()
		{
			if (this._endSignal.ID == 4) {
				int a = 10;
			}
			if (_startSignal.Zug != null) {
				if (_startSignal.Zug != null) {
					if (!_endSignal.ZugPruefung(_startSignal.Zug)) { return false; }
				}
			}
			//Prüfung nach Blockaden
			foreach (Befehl adr in _startBefehle) {
				if (adr.Element is Signal) {
					if (adr.Element == this._endSignal && adr.Element.IsLocked) {
						return false;
					}
					continue;
				}

				if (adr.Element is Knoten) {
					continue;
				}
				if (adr.Element is Gleis) {
					Gleis gl = (Gleis)adr.Element;
					if (gl.GleisBelegung())
						return false;
				}
				if (adr.Element.IsLocked) {
					if (adr.SchaltZustand != adr.Element.Ausgang.AusgangAbfragen())
						return false;
				}
			}
			return true;
		}

		/// <summary>
		/// prüft die Verfügbarkeit der FS (Adr.-Block., Zielsignal-Belegung, Zug-Länge)
		/// </summary>
		/// <returns></returns>
		public bool AdressenFreiAlt()//AdressenFrei
		{
			//Prüfung auf Zugtypen am Zielsignal
			if (_startSignal.Zug != null && _startSignal.Zug.ZugTyp != "") {
				if (!_endSignal.ZugTypPruefung(_startSignal.Zug.ZugTyp)) { return false; }
			}
			//Prüfung der Zuglänge am Zielsignal
			if (_endSignal.ZugLaengeMax > 0) {
				if (_startSignal.Zug.Laenge > _endSignal.ZugLaengeMax)
					return false;
			}
			//Prüfung nach Blockaden
			foreach (Befehl adr in _startBefehle) {
				if (adr.Element is Signal) {
					if (adr.Element == this._endSignal && adr.Element.IsLocked) {
						return false;
					}
					continue;
				}

				if (adr.Element is Knoten) {
					continue;
				}
				if (adr.Element is Gleis) {
					Gleis gl = (Gleis)adr.Element;
					if (gl.GleisBelegung())
						return false;
				}
				if (adr.Element.IsLocked) {
					if (adr.SchaltZustand != adr.Element.Ausgang.AusgangAbfragen())
						return false;
				}

			}
			return true;
		}
		/// <summary>
		/// soll die Startgleise mit dem Startsignal vorrübergehend Koppeln
		/// </summary>
		public void StartgleiseInSignalKoppeln() {
			//BefehlsListe startSignal
			//_startSignal.Koppelung.BefListe = _startGleise;
		}


		public bool AusgangToggeln(FahrstrassenSignalTyp signalTyp, bool verlaengern) {
			if (Verfuegbarkeit() || IsAktiv) {
				//StartSignal.Koppelung.Aktiv = true;//StartgleiseInSignalKoppeln();
				if (Parent.AnlagenZustand.FahrstrasseSchalten(this.ID)) {

					for (int i = 0; i < _startBefehle.Count; i++) {
						//if (_startBefehle[i].Element.GetType() != typeof(Signal))
						if (_startBefehle[i].Element is Signal) { }
						else {
							_startBefehle[i].BefehlAusfuehren();
							this._startBefehle[i].Element.IsLocked = true;
						}
					}
					if (verlaengern) {
						foreach (Befehl befehl in this._startGleise) {
							befehl.Element.IsLocked = false;
							befehl.BefehlAusfuehren();
							befehl.Element.IsLocked = true;
						}
					}
					if (_startSignal.Zug == null) {
						if (_startSignal.GegenSignal != null && _startSignal.GegenSignal.Zug != null) {
							_startSignal.GegenSignal.ZugWechsel(_startSignal.ID);
						}
					}
					_startSignal.IsLocked = false;
				}
				else {
					//_startSignal.Koppelung.Aktiv = false;
					if (signalTyp == FahrstrassenSignalTyp.ZielSignal) {
						FahrstrasseN fsAnkommend = null;
						foreach (FahrstrasseN el in Parent.FahrstrassenElemente.AktiveFahrstrassen) {
							if (el.EndSignal == this.StartSignal) {
								fsAnkommend = el;
								break;
							}
						}
						if (fsAnkommend != null) {
							fsAnkommend.AusgangToggeln(FahrstrassenSignalTyp.ZielSignal, verlaengern);
						}
					}

					if (signalTyp == FahrstrassenSignalTyp.StartSignal && this.EndSignal.AutoStart) {
						FahrstrasseN fsAutostart = null;
						foreach (FahrstrasseN el in Parent.FahrstrassenElemente.AktiveFahrstrassen) {
							if (el.StartSignal == this.EndSignal) {
								fsAutostart = el;
								break;
							}
						}
						if (fsAutostart != null) {
							fsAutostart.AusgangToggeln(FahrstrassenSignalTyp.StartSignal, verlaengern);
						}
					}

					foreach (Befehl adr in _startBefehle) {
						adr.Element.IsLocked = false;
					}
					foreach (Befehl adr in _endBefehle) {
						adr.Element.IsLocked = false;
						adr.BefehlAusfuehren();
					}
					if (signalTyp == FahrstrassenSignalTyp.ZielSignal) {
						//	this._startSignal.Zug = 0;
						this._startSignal.ZugWechsel(_endSignal.ID);
					}
				}
				this.Selektiert = false;
				this.Ausgang.AusgangToggeln();
				Parent.FahrstrassenElemente.AktiveFahrstrassenAktualisieren(Parent.AnlagenZustand);
				return true;
			}
			return false;
		}

		public bool IstGleich(FahrstrasseN fssNeu) {
			if (this.StartSignal.ID != fssNeu.StartSignal.ID) {
				return false;
			}

			if (this.EndSignal.ID != fssNeu.EndSignal.ID) {
				return false;
			}

			int i = 0;
			int j = 0;
			if (this.ListeElemente[0] is Signal) {
				i++;
			}
			if (fssNeu.ListeElemente[0] is Signal) {
				j++;
			}
			for (; i < this.ListeElemente.Length; i++) {
				if (this.ListeElemente[i] is Signal) {
					break;
				}
				if (!(this.ListeElemente[i] is Gleis)) {
					for (; j < fssNeu.ListeElemente.Length; j++) {
						if (fssNeu.ListeElemente[j] is Signal) {
							return true;
						}
						if (!(fssNeu.ListeElemente[j] is Gleis)) {
							if (fssNeu.ListeElemente[j] == this.ListeElemente[i]) {
								j++;
								break;
							}
							else {
								return false;
							}
						}
					}
				}
			}

			for (; j < fssNeu.ListeElemente.Length; j++) {
				if (!(fssNeu.ListeElemente[j] is Gleis)) {
					if (!(fssNeu.ListeElemente[j] is Signal)) {
						return false;
					}
				}
			}

			return true;
		}

		public bool IstGleich(AnlagenElement[] fssNeu) {

			//Init Vergleich Knotenliste; Vergleich Start- und Zielsignal
			int i = 0;
			int j = 0;
			if (this.ListeElemente[0] is Signal) {
				i++;
			}
			if (fssNeu[0] is Signal) {
				if (this.StartSignal.ID != ((Signal)fssNeu[0]).ID) {
					return false;
				}
				j++;
			}
			if (fssNeu[fssNeu.Length - 1] is Signal) {
				if (this.EndSignal.ID != fssNeu[fssNeu.Length - 1].ID) {
					return false;
				}
			}



			//Vergleich der Knotenliste
			for (; i < this.ListeElemente.Length; i++) {
				if (this.ListeElemente[i] is Knoten) {
					while (!(fssNeu[j] is Knoten) && j < fssNeu.Length) {
						j++;
					}

					if (j < fssNeu.Length) {
						if (fssNeu[j].ID != this.ListeElemente[i].ID) {
							return false;
						}
						j++;
					}
					else {
						//this ist längere Fahrstraße?
						for (; i < this.ListeElemente.Length; i++) {
							if (this.ListeElemente[i] is Knoten) {
								return false;
							}
						}
						return true;
					}
				}

			}
			//this ist kürzere Fahrstraße?
			for (; j < this.ListeElemente.Length; j++) {
				if (fssNeu[j] is Knoten) {
					return false;
				}
			}
			return true;
		}

		public void StartGleisEinschalten() {
			if (_startGleise != null) {
				foreach (Befehl befehl in _startGleise) {
					befehl.BefehlAusfuehren();
				}
			}
		}
		/// <summary>
		/// prüft ob am Zielgleis die FS aufgelöst werden kann
		/// </summary>
		/// <returns></returns>
		public bool ZielPruefung() {
			if (_zielGleis != null) {
				return _zielGleis.Eingang.RueckmeldungAbfragen();
			}
			return false;
		}
		/// <summary>
		/// prüft ob die Strecke wieder frei ist
		/// </summary>
		/// <returns>true wenn die Strecke belegt ist</returns>
		private bool streckePruefung() {
			bool erg = false;
			foreach (Adresse rm in _streckenRM) {
				if (rm.RueckmeldungAbfragen()) { return true; }
			}
			return erg;
		}
		private void streckenRMErmitteln() {
			foreach (Befehl bef in _startBefehle) {
				Gleis gl;
				Adresse ad;
				_streckenRM = new List<Adresse>();
				if (bef.Element is Gleis) {
					gl = (Gleis)bef.Element;
					if (gl.Eingang.SpeicherString != "0-0-0") {
						ad = gl.Eingang;
						foreach (Adresse x in _streckenRM) {
							if (!x.GleicheAdresse(gl.Eingang)) { _streckenRM.Add(gl.Eingang); }
						}
					}

				}
			}
		}
	}


}
