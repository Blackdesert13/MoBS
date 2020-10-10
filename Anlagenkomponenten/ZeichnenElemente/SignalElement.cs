using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using MoBaSteuerung.Anlagenkomponenten.MCSpeicher;
using MoBaSteuerung.ZeichnenElemente;

namespace MoBaSteuerung.Elemente {

	/// <summary>
	/// Signal
	/// </summary>
	public class Signal : GleisRasterAnlagenElement {
		#region private Felder
		private GraphicsPath _graphicsPathHintergrund;
		private GraphicsPath _graphicsPathKreis;
		private GraphicsPath _graphicsPathLinien;
		private GraphicsPath _graphicsPathText;

		private Color _farbe;
		private StringFormat _stringFormat;

		private bool _inZeichenRichtung = false;
		private InfoFenster _infoFenster = null;
		private Zug zug;
		private Signal _gegenSignal = null;
		private bool _autoStart = false;
		private List<FahrstrasseN> _autoStartFSGruppe;
		private int _zugLaengeMax;
		private List<string> _zugTyp = new List<string>();
		private Gleis _zielGleis;
		//private DateTime _zeitAnkunft;

		#endregion //private Felder

		#region Properties Eigenschaften
		/// <summary>
		/// zum Speichern in der Anlagen-Datei
		/// </summary>
		public override string SpeicherString {
			get {
				int infoNr = 0;
				if (_infoFenster != null) { infoNr = _infoFenster.ID; }
				return "Signal"
						 + "\t" + ID
						 + "\t" + AnschlussGleis.ID + " " + Gleisposition
						 + "\t" + InZeichenRichtung.ToString()
						 + "\t" + infoNr
						 + "\t" + Ausgang.SpeicherString
						 + "\t" + Bezeichnung
						 + "\t" + Stecker
						 + "\t" + _autoStart
						 + "\t" + _zugLaengeMax
						 + "\t" + ZugTypString
						 + "\t" + (Koppelung != null ? Koppelung.ListenString : "")// KoppelungsString; ;
						 + "\t" + Convert.ToString(ZielGleisNr);
			}
		}
		/// <summary>
		/// InfoString zur Anzeige
		/// </summary>
		public override string InfoString {
			get {
				return "Signal " + this.ID;
			}
		}
		/// <summary>
		/// bei aktiver Rückmeldung sollen Fahrstrassen diesem Zielsiegnal aufgelöst werden
		/// </summary>
		public int ZielGleisNr {
			set {
				if (value > 0) {
					_zielGleis = Parent.GleisElemente.Element(value);
				}
				else { _zielGleis = null; }
			}
			get {
				if (_zielGleis == null) { return 0; }
				else { return _zielGleis.ID; }
			}
		}

		public string Anzeige {
			set {
				if (_infoFenster != null) {
					_infoFenster.Text = value;
				}
			}
		}
		/// <summary>
		/// Aufzählung der zulässigen Zug-Typen
		/// </summary>
		[Description("Aufzählung der zulässigen Zug-Typen an diesem Signal\nohne Angaben sind alle Zug-Typen zulässig")]
		public string ZugTypString {
			get {
				string ergebnis = "";
				foreach (string x in _zugTyp) { ergebnis += x + " "; }
				return ergebnis.Trim();
			}
			set {
				_zugTyp = new List<string>();
				string[] ztyp = value.Split(' ');
				for (int i = 0; i < ztyp.Length; i++) {
					if (ztyp[i] != "")
						_zugTyp.Add(ztyp[i]);
				}
			}
		}
		/// <summary>
		/// max. Zugänge am Signal
		/// </summary>
		public int ZugLaengeMax {
			set { _zugLaengeMax = value; }
			get { return _zugLaengeMax; }
		}

		public Zug Zug { get { return zug; } }


		public int ZugNr {
			set {
				int nZug = value;
				if (nZug == 0) {
					zug = null;
					if (_infoFenster != null) {
						_infoFenster.Text = "";//"Sn" + ID;
					}
				}
				else {
					zug = Parent.ZugElemente.Element(nZug);
					if (_infoFenster != null)
						_infoFenster.Text = zug.Anzeige;
				}
			}
			get {
				if (zug != null) { return zug.ID; }
				else { return 0; }
			}
		}

		/// <summary>
		/// wechselt die Farbe grün-rot
		/// </summary>
		public bool Durchfahrt {
			set {
				if (value) {
					this._farbe = Color.Green;
				}
				else {
					this._farbe = Color.Red;
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool InZeichenRichtung {
			get {
				return _inZeichenRichtung;
			}

			set {
				_inZeichenRichtung = value;
			}
		}

		public bool AutoStart {
			get { return _autoStart; }
			set {
				_autoStart = value;
				/*if (_autoStart)
				{
					//_autoStartFSGruppe = Parent.FahrstrassenElemente.StartSignalGruppe(ID);
				}
				else { _autoStartFSGruppe = null; }*/
			}
		}

		public Signal GegenSignal {
			get {
				return _gegenSignal;
			}

			set {
				_gegenSignal = value;
			}
		}

		#endregion //Properties

		#region Konstruktoren
		public Signal(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, Point rasterPosition, bool inZeichenRichtung)
		 : base(parent, 0, zoom, anzeigeTyp) {
			KurzBezeichnung = "Sn";
			PositionRaster = rasterPosition;
			Position = new Point(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
			this._inZeichenRichtung = inZeichenRichtung;

			this._graphicsPathHintergrund = new GraphicsPath();
			this._graphicsPathKreis = new GraphicsPath();
			this._graphicsPathLinien = new GraphicsPath();

			this._farbe = Color.Red;

			this.Berechnung();
		}

		public Signal(AnlagenElemente parent, Int32 iD, Int32 zoom, AnzeigeTyp anzeigeTyp, Point rasterPosition, bool inZeichenRichtung)
		 : base(parent, iD, zoom, anzeigeTyp) {
			KurzBezeichnung = "Sn";
			PositionRaster = rasterPosition;
			Position = new Point(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
			Ausgang = new Adresse(parent);
			this._inZeichenRichtung = inZeichenRichtung;

			foreach (Gleis gl in Parent.GleisElemente.Elemente) {
				if (gl.GleisElementAnschluss(this)) {
					AnschlussGleis = gl;
					Parent.SignalElemente.Hinzufügen(this);

					this._stringFormat = new StringFormat();
					this._stringFormat.Alignment = StringAlignment.Center;
					this._stringFormat.LineAlignment = StringAlignment.Center;

					this._graphicsPathHintergrund = new GraphicsPath();
					this._graphicsPathKreis = new GraphicsPath();
					this._graphicsPathLinien = new GraphicsPath();
					this._graphicsPathText = new GraphicsPath();

					this._farbe = Color.Red;

					this.Berechnung();

					break;
				}
			}
		}

		public Signal(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, string[] elem)
				: base(parent, Convert.ToInt32(elem[1]), zoom, anzeigeTyp) {
			string[] glAnschl = elem[2].Split(' ');
			KurzBezeichnung = "Sn";
			Bezeichnung = elem[6];
			if (elem.Length > 7)
				Stecker = elem[7];
			Ausgang = new Adresse(parent);

			if (elem.Length > 8) _autoStart = Convert.ToBoolean(elem[8]);
			if (elem.Length > 9) _zugLaengeMax = Convert.ToInt32(elem[9]);
			if (elem.Length > 10) ZugTypString = elem[10];
			if (elem.Length > 11) KoppelungsString = elem[11];
			if (elem.Length > 12) { ZielGleisNr = Convert.ToInt16(elem[12]); }
			Gleis gl = Parent.GleisElemente.Element(Convert.ToInt32(glAnschl[0]));
			if (gl != null) {
				PositionRaster = gl.GetRasterPosition(this, Convert.ToInt32(glAnschl[1]));
				Position = new Point(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
				InZeichenRichtung = Convert.ToBoolean(elem[3]);

				if (gl.GleisElementAnschluss(this)) {
					Ausgang.SpeicherString = elem[5];
					AnschlussGleis = gl;
					Parent.SignalElemente.Hinzufügen(this);

					this._stringFormat = new StringFormat();
					this._stringFormat.Alignment = StringAlignment.Center;
					this._stringFormat.LineAlignment = StringAlignment.Center;

					this._graphicsPathHintergrund = new GraphicsPath();
					this._graphicsPathKreis = new GraphicsPath();
					this._graphicsPathLinien = new GraphicsPath();
					this._graphicsPathText = new GraphicsPath();

					this._farbe = Color.Red;

					this.Berechnung();
				}
				Signal gegenSig = GegenSignalSuchen();
				if (gegenSig != null) {
					GegenSignal = gegenSig;
					gegenSig.GegenSignal = this;
				}
			}
			infoFensterLaden(Convert.ToInt16(elem[4]));
		}
		#endregion //Konstruktoren

		private void infoFensterLaden(int Nummer) {
			_infoFenster = this.Parent.InfoElemente.Element(Nummer);
		}

		private Signal GegenSignalSuchen() {

			bool richtung = this.InZeichenRichtung;
			Gleis gl = this.AnschlussGleis;
			if (richtung) {
				if (gl.Signale[1] != null) {
					return gl.Signale[1];
				}
			}
			else {
				if (gl.Signale[0] != null) {
					return gl.Signale[0];
				}
			}
			Knoten kn = (richtung ? gl.StartKn : gl.EndKn);
			while (kn.Weichen[0] == null && kn.Weichen[1] == null) {
				bool stumpfGleis = true;
				foreach (Gleis g in kn.Gleise) {
					if (g != null && g != gl) {
						gl = g;
						stumpfGleis = false;
						break;
					}
				}
				if (stumpfGleis) {
					break;
				}
				richtung = (gl.StartKn == kn);

				if (richtung) {
					if (gl.Signale[0] != null) {
						return gl.Signale[0];
					}
				}
				else {
					if (gl.Signale[1] != null) {
						return gl.Signale[1];
					}
				}
				kn = (richtung ? gl.EndKn : gl.StartKn);
			}

			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="graphics"></param>
		public override void ElementZeichnen(Graphics graphics) {
			int transpanz = 255;
			Color farbePinsel = Color.Transparent;
			Color farbeStift = Color.Transparent;

			switch (this.AnzeigenTyp) {
				case AnzeigeTyp.Bearbeiten:
					if (AnschlussGleis == null) {
						transpanz = 128;
					}
					farbePinsel = Color.FromArgb(transpanz, Color.Red);
					farbeStift = Color.FromArgb(transpanz, Color.Black);
					if (this.ElementZustand == Elementzustand.Selektiert)
						graphics.FillPath(Brushes.Yellow, this._graphicsPathHintergrund);
					break;
				case AnzeigeTyp.Bedienen:
					if (_infoFenster != null) {
						if (zug == null) { _infoFenster.Text = ""; }
						else { _infoFenster.Text = zug.Anzeige; }
					}
					if (this.Selektiert) {
						graphics.FillPath(Brushes.Yellow, this._graphicsPathHintergrund);
					}
					else {
						graphics.FillPath(Brushes.White, this._graphicsPathHintergrund);
					}
					//if (Passiv) {
					//	transpanz = 128;
					//}
					if (this.ElementZustand == Elementzustand.An) {
						farbePinsel = Color.FromArgb(transpanz, Color.Green);
					}
					else {
						farbePinsel = Color.FromArgb(transpanz, Color.Red);
					}
					
					farbeStift = Color.FromArgb(transpanz, Color.Black);
					if (Ausgang.IsLocked) {
						farbeStift = Color.FromArgb(transpanz, Color.Red);
					}
					break;
			}

			SolidBrush pinsel = new SolidBrush(farbePinsel);
			Pen stift = new Pen(farbeStift, 1);

			graphics.FillPath(pinsel, this._graphicsPathKreis);
			graphics.DrawPath(Pens.Black, this._graphicsPathKreis);
			graphics.DrawPath(Pens.Black, this._graphicsPathLinien);

			//if (this.AnzeigenTyp == AnzeigeTyp.Bedienen)
			graphics.DrawPath(Pens.Black, this._graphicsPathText);
		}

		/// <summary>
		/// berechnet die Grafik
		/// </summary>
		public override void Berechnung() {
			//Stopwatch watch = new Stopwatch();
			//watch.Start();
			if (AnschlussGleis != null) {
				PositionRaster = AnschlussGleis.GetRasterPosition(this, Gleisposition);
			}

			int winkel = 0;
			if (AnschlussGleis != null)
				winkel = AnschlussGleis.GetDirection(InZeichenRichtung) * 45;

			Matrix matrix = new Matrix();
			matrix.Translate(this.PositionRaster.X * this.Zoom, this.PositionRaster.Y * this.Zoom);
			matrix.Scale(this.Zoom, this.Zoom);
			matrix.Rotate(-winkel);

			this._graphicsPathHintergrund.Reset();
			this._graphicsPathHintergrund.AddEllipse(new RectangleF(-0.5f, -0.5f, 1f, 1f));
			this._graphicsPathHintergrund.Transform(matrix);

			this._graphicsPathKreis.Reset();
			this._graphicsPathKreis.AddEllipse(new RectangleF(0f, 0f, 0.5f, 0.5f));
			this._graphicsPathKreis.Transform(matrix);

			this._graphicsPathLinien.Reset();
			this._graphicsPathLinien.AddLines(new PointF[] { new PointF(0.0f, 0.25f), new PointF(-0.5f, 0.25f), new PointF(-0.5f, 0.5f), new PointF(-0.5f, 0.0f) });
			this._graphicsPathLinien.Transform(matrix);

			if (this.AnzeigenTyp == AnzeigeTyp.Bedienen) {
				Matrix matrixText = new Matrix();
				matrixText.Translate(this.PositionRaster.X * this.Zoom, this.PositionRaster.Y * this.Zoom);
				matrixText.Scale(this.Zoom, this.Zoom);
				this._graphicsPathText.Reset();
				this._graphicsPathText.AddString(ID.ToString(), new FontFamily("Arial"), 0, 0.5f, this.DrehenUmPunkt(new PointF(0, 0), new PointF(0, -0.25f), winkel), this._stringFormat);
				this._graphicsPathText.Transform(matrixText);
			}

			//watch.Stop();
			//double diff = watch.Elapsed.TotalMilliseconds;

		}


		public override bool MouseClick(Point punkt) {
			return this._graphicsPathHintergrund.GetBounds().Contains(punkt);
		}

		/// <summary>
		/// Diese Methode sucht in der Gleisliste nach einem Gleis, welches an der Position dieses Elementes liegt
		/// und wenn diese Position noch frei ist, wird dieses Element mit dem Gleis verknüpft.
		/// </summary>
		/// <returns>Gibt TRUE zurück, wenn dieses Objekt in einem Gleis eingetragen werden konnte</returns>
		public override bool AnschlussGleisSuchen() {
			foreach (Gleis gl in Parent.GleisElemente.Elemente) {
				if (gl.GleisElementAnschluss(this)) {
					this.AnschlussGleis = gl;
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override bool BearbeitenAktualisierenNeuZeichnen() {
			GleisElementAustragen();
			bool aktualisierungNotwendig = AnschlussGleisSuchen();
			base.BearbeitenAktualisierenNeuZeichnen();
			return aktualisierungNotwendig;
		}


		public override bool GleisElementAustragen() {
			if (AnschlussGleis != null) {
				AnschlussGleis.GleisElementAustragen(this);
				this.AnschlussGleis = null;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Überträgt den Zug auf das neue Signal
		/// </summary>
		/// <param name="NeuesSignal"></param>
		public void ZugWechsel(int NeuesSignal) {
			int neueSignalNr = NeuesSignal;
			if (zug != null) {
				zug.AnkunftsZeit = DateTime.Now;
				zug.SignalNummer = neueSignalNr;
			}
		}
		/// <summary>
		/// prüft ob der Zug-Typ an diesem Signal zulässig ist
		/// </summary>
		/// <param name="ZugTyp">der zu prüfende Zugtyp</param>
		/// <returns>gibt true wenn der Zugtyp zugelässig ist</returns>
		public bool ZugTypPruefung(string ZugTyp) {
			if (_zugTyp.Count == 0) { return true; }
			foreach (string x in _zugTyp) {
				if (x.Equals(ZugTyp)) { return true; }
			}
			return false;
		}
		/// <summary>
		/// prüft ob der Zug-Typ und die Zug-Länge an diesem Signal zulässig ist
		/// </summary>
		/// <param name="PruefZug">der zu prüfende Zug</param>
		/// <returns>gibt true wenn der Zug am Signal zugelässig ist</returns>
		public bool ZugPruefung(Zug PruefZug) {
			if (_zugLaengeMax > 0) { }
			if (_zugTyp.Count != 0) {
				foreach (string x in _zugTyp) {
					if (!x.Equals(PruefZug.ZugTyp)) { return false; }
				}
			}
			return true;
		}
	}
}