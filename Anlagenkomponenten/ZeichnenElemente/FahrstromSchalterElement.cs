using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using MoBaSteuerung.Anlagenkomponenten.MCSpeicher;
using MoBaSteuerung.Elemente;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace MoBaSteuerung.Elemente {
	/// <summary>
	/// schaltet den Fahrstrom zwischen zwei Reglern bzw. anderen FSS um
	/// </summary>
	public class FSS : GleisRasterAnlagenElement {
		//private Color füllFarbe;
		private GraphicsPath graphicsPath;
		private GraphicsPath graphicsPathKreis;
		private Regler[] _regler = new Regler[2];//der 3. Regler ist der aktive Regler
		private FSS[] _fss = new FSS[2];
		private int[] _reglerNr = new int[2];
		private int[] _reglerNrQuelle = new int[2];
		/*die 3. Nummer ist die aktuelle Reglernummer ew. FSS
          die 4. Nummer ist die aktuelle Reglernummer,
          ist die 3.Nummer ein FSS ist die 4. Nummer dessen aktueller Regler!
        */
		//private bool stellung = false;

		/// <summary>
		/// zum Speichern in der Anlagen-Datei
		/// </summary>
		public override string SpeicherString {
			get {
				return "FSS"
						+ "\t" + ID
						+ "\t" + AnschlussGleis.ID + " " + Gleisposition
						+ "\t" + _reglerNr[0]
						+ "\t" + _reglerNr[1]
						+ "\t" + Ausgang.SpeicherString
						+ "\t" + Bezeichnung
						+ "\t" + Stecker
            + "\t" + Koppelung.ListenString;//KoppelungsString;
			}
		}

		public override string InfoString {
			get {
				return "FSS " + this.ID;
			}
		}

		public FSS(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, Point rasterPosition)
		: base(parent, 0, zoom, anzeigeTyp) {
			KurzBezeichnung = "Fss";
			this.graphicsPath = new GraphicsPath();
			this.graphicsPathKreis = new GraphicsPath();
			PositionRaster = rasterPosition;
			Position = new Point(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
			this.Berechnung();
		}
		public FSS(AnlagenElemente parent, Int32 iD, Int32 zoom, AnzeigeTyp anzeigeTyp, Point rasterPosition)
		 : base(parent, iD, zoom, anzeigeTyp) {
			KurzBezeichnung = "Fss";
			graphicsPath = new GraphicsPath();
			this.graphicsPathKreis = new GraphicsPath();
			PositionRaster = rasterPosition;
			Ausgang = new Adresse(parent);
			Position = new Point(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
			this.ReglerNummer1 = 1; this.ReglerNummer2 = 1;
			foreach (Gleis gl in Parent.GleisElemente.Elemente) {
				if (gl.GleisElementAnschluss(this)) {
					AnschlussGleis = gl;
					Parent.FssElemente.Hinzufügen(this);
					this.Berechnung();
				}
			}
		}

		/// <summary>
		/// zum Laden aus der Text-Datei
		/// </summary>
		/// <param name="parent">AnlagenElemente</param>
		/// <param name="zoom"></param>
		/// <param name="anzeigeTyp"></param>
		/// <param name="elem">Zeile aus der Text-Datei</param>
		public FSS(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, string[] elem)
		: base(parent, Convert.ToInt32(elem[1]), zoom, anzeigeTyp) {
			KurzBezeichnung = "Fss";
			string[] glAnschl = elem[2].Split(' ');
			_reglerNrQuelle[0] = Convert.ToInt32(elem[3]);
			_reglerNrQuelle[1] = Convert.ToInt32(elem[4]);
			_reglerNr[0] = Convert.ToInt32(elem[3]);
			_reglerNr[1] = Convert.ToInt32(elem[4]);
			_fss[0] = null;
			_regler[0] = null;
			if (_reglerNr[0] > 0) _regler[0] = Parent.ReglerElemente.Element(_reglerNr[0]);
			_fss[1] = null;
			_regler[1] = null;
			if (_reglerNr[1] > 0) _regler[1] = Parent.ReglerElemente.Element(_reglerNr[1]);
			Ausgang.SpeicherString = elem[5];
			if (elem.Length > 7) {
				Bezeichnung = elem[6];
				Stecker = elem[7];
			}
			if (elem.Length > 8) {
				KoppelungsString = elem[8];
			}


			Gleis gl = Parent.GleisElemente.Element(Convert.ToInt32(glAnschl[0]));
			Gleisposition = Convert.ToInt32(glAnschl[1]);
			if (gl != null) {
				PositionRaster = gl.GetRasterPosition(this, Convert.ToInt32(glAnschl[1]));
				Position = new Point(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
				{
					AnschlussGleis = gl;
					Parent.FssElemente.Hinzufügen(this);
					graphicsPath = new GraphicsPath();
					graphicsPathKreis = new GraphicsPath();
					this.Berechnung();
				}
			}
		}



		public int ReglerNummer1 {
			get { return _reglerNr[0]; }
			set {
				_reglerNr[0] = value;
				if (_reglerNr[0] > -1) {
					_regler[0] = this.Parent.ReglerElemente.Element(_reglerNr[0]);
					_fss[0] = null;
				}
				else _fss[0] = this.Parent.FssElemente.Element(-_reglerNr[0]);
			}
		}
		public int ReglerNummer2 {
			get { return _reglerNr[1]; }
			set {
				_reglerNr[1] = value;
				if (_reglerNr[0] > -1) {
					_regler[1] = this.Parent.ReglerElemente.Element(_reglerNr[1]);
					_fss[1] = null;
				}
				else _fss[1] = this.Parent.FssElemente.Element(-_reglerNr[1]);
			}
		}

		public void FSSLaden() {
			if (_reglerNr[0] < 0) _fss[0] = Parent.FssElemente.Element(-_reglerNr[0]);
			else _fss[0] = null;
			if (_reglerNr[1] < 0) _fss[1] = Parent.FssElemente.Element(-_reglerNr[1]);
			else _fss[1] = null;
		}

		/// <summary>
		/// 
		/// </summary>
		private void reglerAktualisieren() {

			if (_reglerNr[0] < 0) {
				if (_fss[0] == null) _fss[0] = Parent.FssElemente.Element(-_reglerNr[0]);
				_regler[0] = _fss[0].AktiverRegler();
			}
			if (_reglerNr[1] < 0) {
				if (_fss[1] == null) _fss[1] = Parent.FssElemente.Element(-_reglerNr[1]);
				_regler[1] = _fss[1].AktiverRegler();
			}

		}

		/// <summary>
		/// liefert den aktiven Regler,
		/// die aktuelle Stellung eines anderen FSS wird nicht überprüft
		/// </summary>
		/// <returns></returns>
		public Regler AktiverRegler() {
			if (this.ElementZustand == Elementzustand.An) return _regler[1];
			else return _regler[0];
		}

		/// <summary>
		/// liefert die aktive Reglernummer
		/// </summary>
		/// <returns></returns>
		public int AktiverReglerNr {
			get {
				if (this.ElementZustand == Elementzustand.An) { if (_regler[1] != null) return _regler[1].ID; }
				else { if (_regler[0] != null) return _regler[0].ID; }
				return 0;
			}
		}

		/// <summary>
		/// zum reseten von verketteten FSS
		/// und liefert die derzeitige aktive ReglerNummer
		/// </summary>
		/// <returns></returns>
		public int reset() {
			int ergebnis = 0;
			if (_reglerNr[0] < 0) {
				_regler[0] = null;
				ergebnis++;
			}
			if (_reglerNr[1] < 0) {
				_regler[1] = null;
				ergebnis++;
			}
			return ergebnis;//this.aktualisieren();
		}
		/// <summary>
		/// aktualisiert _regler[0] und _regler[1]
		/// die Anzahl der undefinierten Regler wird zurückgeliefert
		/// </summary>
		/// <returns>Anzahl der undefinierten Regler</returns>
		public int aktualisieren() {
			int ergebnis = 0;
			if (_regler[0] == null) {
				if (_fss[0] != null) {
					int rn = _fss[0].AktiverReglerNr;
					if (rn > 0)
						_regler[0] = this.Parent.ReglerElemente.Element(rn);
					else ergebnis++;
				}
			}
			if (_regler[1] == null) {
				if (_fss[1] != null) {
					int rn = _fss[1].AktiverReglerNr;
					if (rn > 0) _regler[1] = this.Parent.ReglerElemente.Element(rn);
					else ergebnis++;
				}
			}
			return ergebnis;
		}
		/// <summary>
		/// liefert die aktive Reglernummer
		/// </summary>
		/// <returns></returns>
		public int reglerCheck() { return _reglerNr[3]; }

		public override bool MouseClick(Point punkt) {
			return this.graphicsPath.IsVisible(punkt);
		}

		public override void Berechnung() {
			Matrix matrix = new Matrix();
			matrix.Translate(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
			matrix.Scale(Zoom, Zoom);

			this.graphicsPath.Reset();
			this.graphicsPath.AddRectangle(new RectangleF(-0.45f, -0.45f, 0.9f, 0.9f));
			this.graphicsPath.Transform(matrix);
			this.graphicsPathKreis.Reset();
			this.graphicsPathKreis.AddEllipse(new RectangleF(-0.4f, -0.4f, 0.8f, 0.8f));
			this.graphicsPathKreis.Transform(matrix);
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



		public override void ElementZeichnen(Graphics graphics) {
			int transpanz = 255;
			Color farbePinsel = Color.LightGray;
			Color farbePinselInnen = Color.LightGray;
			Color farbeStift = Color.Black;

			if (this.ElementZustand == Elementzustand.Selektiert) {
				farbePinsel = Color.Yellow;
				farbePinselInnen = Color.Yellow;
			}
			else {
				if (_regler[0] != null) {
					switch (this.ElementZustand) {
						case Elementzustand.An:
							farbePinsel = _regler[0].Farbe;
							break;
						case Elementzustand.Aus:
							farbePinselInnen = _regler[0].Farbe;
							break;
					}
				}
				if (_regler[1] != null) {
					switch (this.ElementZustand) {
						case Elementzustand.An:
							farbePinselInnen = _regler[1].Farbe;
							break;
						case Elementzustand.Aus:
							farbePinsel = _regler[1].Farbe;
							break;
					}
				}
			}

			if(this.AnschlussGleis == null) {
				transpanz = 128;
			}

			farbePinselInnen = Color.FromArgb(transpanz, farbePinselInnen);
			farbePinsel = Color.FromArgb(transpanz, farbePinsel);
			farbeStift = Color.FromArgb(transpanz, farbeStift);

			SolidBrush pinsel = new SolidBrush(farbePinsel);
			Pen stift = new Pen(farbeStift, 1);
			graphics.FillPath(pinsel, this.graphicsPath);
			graphics.DrawPath(stift, this.graphicsPath);
			// SolidBrush pinsel1 = new SolidBrush(farbePinselInnen);
			pinsel.Color = farbePinselInnen;
			graphics.FillPath(pinsel, this.graphicsPathKreis);
		}
	}
}

