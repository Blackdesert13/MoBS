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
		private GraphicsPath _graphicsPathHintergrund = new GraphicsPath();
		private Signal _startSignal;
		private Signal _endSignal;
		private List<Knoten> _knotenListe = new List<Knoten>();
		private List<Befehl> _befehle;
		private bool _isAktiv = false;

		private List<FahrstrasseN> _fahrStrassen;

		#region Properties
		/// <summary>
		/// 
		/// </summary>
		public override string SpeicherString {
			get {
				string txt = "FahrstrasseK"
							+ "\t" + ID;
				txt += Environment.NewLine + " FahrstrassenListe";
				txt += "\t" + FahrstrassenString;
				txt += Environment.NewLine + " Befehle" + BefehleString;

				return txt;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string FahrstrassenString {
			get {
				string txt = "";
				foreach (var item in _fahrStrassen) {
					txt += item.ID + "\t";
				}
				return txt.Trim();
			}
			set {
				_fahrStrassen.Clear();
				this.EinlesenFahrstrassenListe(_fahrStrassen, value.Replace(";", "\t"));
				Berechnung();
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

		public Signal StartSignal {
			get {
				return _startSignal;
			}
		}
		public Signal EndSignal {
			get {
				return _endSignal;
			}
		}

		public List<Befehl> Befehle {
			get {
				return _befehle;
			}

			set {
				_befehle = value;
			}
		}

		public override bool Selektiert {
			get {
				return base.Selektiert;
			}

			set {
				base.Selektiert = value;
				if(_endSignal != null) {
					_endSignal.Selektiert = value;
				}
			}
		}

		public string BefehleString {
			get {
				return this.AuslesenBefehlsliste(_befehle);
			}
			set {
				string[] spString = value.Trim().Split(';');
				_befehle = new List<Befehl>();
				this.EinlesenBefehlsliste(_befehle, spString, true);
			}
		}

		#endregion//Properties

		#region Konstuktoren
		///// <summary>
		///// 
		///// </summary>
		///// <param name="elementListe"></param>
		//public FahrstrasseK(AnlagenElement[] elementListe)
		//: base(elementListe[0].Parent, elementListe[0].Parent.FahrstrassenElemente.SucheFreieNummer(), elementListe[0].Zoom, elementListe[0].AnzeigenTyp) {
		//	Berechnung();
		//}

		public FahrstrasseK(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, List<FahrstrasseN> fsListe, string befehlsString) 
			: base(parent, parent.FahrstrassenKElemente.SucheFreieNummer(), zoom, anzeigeTyp) {
			_fahrStrassen = fsListe;
			this.BefehleString = befehlsString;
			this._startSignal = _fahrStrassen.First().StartSignal;
			this._endSignal = _fahrStrassen.Last().EndSignal;

			Parent.FahrstrassenKElemente.Hinzufügen(this);
			Berechnung();
		}

		public FahrstrasseK(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, string[] elem, string fsListe, string befehle)
				: base(parent, Convert.ToInt32(elem[1]), zoom, anzeigeTyp) {
			_fahrStrassen = new List<FahrstrasseN>();
			if (!EinlesenFahrstrassenListe(_fahrStrassen, fsListe)) {
				return;
			}

			this._startSignal = _fahrStrassen.First().StartSignal;
			this._endSignal = _fahrStrassen.Last().EndSignal;

			string[] spString;
			spString = befehle.Trim().Split('\t');
			if (spString[0] == "Befehle") {
				_befehle = new List<Befehl>();
				if (!this.EinlesenBefehlsliste(_befehle, spString))
					return;
			}
			else
				return;

			Parent.FahrstrassenKElemente.Hinzufügen(this);
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
				if(elName.Length != 2 && befehl.Length != 2) {
					continue;
				}
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

		private bool EinlesenFahrstrassenListe(List<FahrstrasseN> list, string fahrstrassenListe) {
			string[] spString = fahrstrassenListe.Split('\t');
			if (spString[0] == " FahrstrassenListe") {
				try {
					for (int i = 1; i < spString.Length; i++) {
						Int16 id = Convert.ToInt16(spString[i].Trim());
						bool found = false;
						foreach (FahrstrasseN fs in Parent.FahrstrassenElemente.GespeicherteFahrstrassen) {
							if (fs.ID == id) {
								found = true;
								if (i > 1) {
									if (fs.StartSignal == list.ElementAt(i - 2).EndSignal) {
										list.Add(fs);
									}
									else {
										return true;
									}
								}
								else {
									list.Add(fs);
									break;
								}
							}
						}
						if (!found) {
							break;
						}
					}
				}
				catch (Exception e) {
					return false;
				}
			}
			if (list.Count > 0) {
				return true;
			}
			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="graphics"></param>
		public override void ElementZeichnen(Graphics graphics) {
			if (this.AnzeigenTyp == AnzeigeTyp.Bearbeiten) {
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
			else if (this.AnzeigenTyp == AnzeigeTyp.Bedienen) {

				switch (this.ElementZustand) {
					case Elementzustand.An:
						return;
					case Elementzustand.Aus:
						return;
					case Elementzustand.Selektiert:
						break;
				}

				Color farbePinsel = Color.FromArgb(128, Color.Yellow);
				SolidBrush pinsel = new SolidBrush(farbePinsel);
				//graphics.FillPath(pinsel, this._graphicsPathHintergrund);
			}

		}

		public override bool AusgangToggeln() {
			if (Verfuegbarkeit()) {
				for(int i = 0; i < this._fahrStrassen.Count; i++) {
					bool verlaengern = _fahrStrassen[i].StartSignal.IsLocked;
					_fahrStrassen[i].AusgangToggeln(FahrstrassenSignalTyp.ZielSignal, verlaengern);
				}
				foreach(Befehl b in _befehle) {
					b.Element.IsLocked = false;
					b.BefehlAusfuehren();
					b.Element.IsLocked = true;
				}

				return true;
			}
			return false;
		}

		public override void Berechnung() {
			_fahrstrLinie.Reset();

			this._knotenListe.Clear();
			foreach (FahrstrasseN item in _fahrStrassen) {
				foreach (AnlagenElement anlElement in item.ListeElemente) {
					if (anlElement is Knoten) {
						this._knotenListe.Add((Knoten)anlElement);
					}
				}
			}

			List<Point> punkte = new List<Point> { };
			punkte.Add(_startSignal.PositionRaster);
			foreach (Knoten el in _knotenListe) {
				punkte.Add(((RasterAnlagenElement)el).PositionRaster);
			}
			punkte.Add(_endSignal.PositionRaster);

			_fahrstrLinie.AddLines(punkte.ToArray());
			Matrix matrix = new Matrix();
			matrix.Scale(Zoom, Zoom);
			_fahrstrLinie.Transform(matrix);

			Matrix matrix2 = new Matrix();
			matrix2.Translate(this._endSignal.PositionRaster.X * this.Zoom, this._endSignal.PositionRaster.Y * this.Zoom);
			matrix2.Scale(this.Zoom, this.Zoom);
			this._graphicsPathHintergrund.Reset();
			this._graphicsPathHintergrund.AddEllipse(new RectangleF(-0.5f, -0.5f, 1f, 1f));
			this._graphicsPathHintergrund.Transform(matrix2);
		}
		
		/// <summary>
		/// prüft die Verfügbarkeit der FS (Adr.-Block., Zielsignal-Belegung, Zug-Länge)
		/// soll AdressenFrei ersetzen
		/// </summary>
		/// <returns></returns>
		public bool Verfuegbarkeit()//Verfuegbarkeit()
		{
			foreach (FahrstrasseN fs in _fahrStrassen) {
				if (!fs.Verfuegbarkeit()) {
					return false;
				}
			}
			return true;
		}

		public bool IstGleich(List<FahrstrasseN> fsKNeu) {
			if (this._fahrStrassen.Count != fsKNeu.Count) {
				return false;
			}
			for (int i = 0, j = 0; i < this._fahrStrassen.Count; i++, j++) {
				if (this._fahrStrassen.ElementAt(i) != fsKNeu.ElementAt(j)) {
					return false;
				}
			}
			return true;
		}

		public bool AusgangToggeln(FahrstrassenSignalTyp signalTyp, bool verlaengern) {
			if (Verfuegbarkeit() || IsAktiv) {
				return true;
			}
			return false;
		}

	}


}
