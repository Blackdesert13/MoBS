﻿using System;
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
				txt += FahrstrassenString;

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
				this.EinlesenFahrstrassenListe(_fahrStrassen, value.Trim().Replace(";", "\t"));
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



		#endregion//Properties
		#region Konstuktoren
		/// <summary>
		/// 
		/// </summary>
		/// <param name="elementListe"></param>
		public FahrstrasseK(AnlagenElement[] elementListe)
		: base(elementListe[0].Parent, elementListe[0].Parent.FahrstrassenElemente.SucheFreieNummer(), elementListe[0].Zoom, elementListe[0].AnzeigenTyp) {


			Berechnung();
		}

		public FahrstrasseK(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, string[] elem, string fsListe)
				: base(parent, Convert.ToInt32(elem[1]), zoom, anzeigeTyp) {
			_fahrStrassen = new List<FahrstrasseN>();
			if (!EinlesenFahrstrassenListe(_fahrStrassen, fsListe)) {
				return;
			}

			this._startSignal = _fahrStrassen.First().StartSignal;
			this._endSignal = _fahrStrassen.Last().EndSignal;
			
			Parent.FahrstrassenKElemente.Hinzufügen(this);
			Berechnung();
		}
		#endregion//Konstruktoren

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
			if(this.AnzeigenTyp == AnzeigeTyp.Bearbeiten) {
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
			else if(this.AnzeigenTyp == AnzeigeTyp.Bedienen){
				
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
				graphics.FillPath(pinsel, this._graphicsPathHintergrund);
			}
			
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
			this._graphicsPathHintergrund.Transform(matrix);
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

		public bool IstGleich(FahrstrasseK fssKNeu) {
			if(this._fahrStrassen.Count != fssKNeu._fahrStrassen.Count) {
				return false;
			}
			for(int i=0, j = 0; i < this._fahrStrassen.Count; i++, j++) {
				if (this._fahrStrassen.ElementAt(i) != fssKNeu._fahrStrassen.ElementAt(j)) {
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
