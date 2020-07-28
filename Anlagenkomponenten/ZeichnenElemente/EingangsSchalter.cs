using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using MoBaSteuerung.Anlagenkomponenten.MCSpeicher;
using MoBaSteuerung.Elemente;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace MoBaSteuerung.Elemente {
	/// <summary>
	/// schaltet den Fahrstrom zwischen zwei Reglern bzw. anderen FSS um
	/// </summary>
	public class EingangsSchalter : GleisRasterAnlagenElement {
		//private Color füllFarbe;
		private GraphicsPath _graphicsPath;
		private GraphicsPath _graphicsPathKreis;
		private Adresse _eingang;

		[Description("die Zeile in der Anlagendatei")]
		/// <summary>
		/// zum Speichern in der Anlagen-Datei
		/// </summary>
		public override string SpeicherString {
			get {
				string erg =
								"EingSchalter"
								+ "\t" + ID
								+ "\t" + AnschlussGleis.ID + " " + Gleisposition
								+ "\t" + Ausgang.SpeicherString
								+ "\t" + Bezeichnung
								+ "\t" + Stecker;
				if (Koppelung == null) { erg = erg + ""; }
				else { erg = erg + Koppelung.ListenString; }//KoppelungsString;
				return erg;
			}
		}

		/// <summary>
		/// Adresse der Rückmeldung
		/// </summary>
		[TypeConverter(typeof(AdresseTypeConverter))]
		new public Adresse Ausgang {
			get {
				if (_eingang == null) {
					_eingang = new Adresse(this.Parent, 0, 0, 0);
				}
				return _eingang;
			}

			set {
				_eingang = value;
			}
		}

		[Description("Kurzbezeichnung des Elements")]
		public override string InfoString {
			get {
				return "EingangsSchalter " + this.ID;
			}
		}

		public EingangsSchalter(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, Point rasterPosition)
		: base(parent, 0, zoom, anzeigeTyp) {
			KurzBezeichnung = "EingSchalter";
			this._graphicsPath = new GraphicsPath();
			this._graphicsPathKreis = new GraphicsPath();
			PositionRaster = rasterPosition;
			Position = new Point(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
			this.Berechnung();
		}

		public EingangsSchalter(AnlagenElemente parent, Int32 iD, Int32 zoom, AnzeigeTyp anzeigeTyp, Point rasterPosition)
		 : base(parent, iD, zoom, anzeigeTyp) {
			KurzBezeichnung = "EingSchalter";
			_graphicsPath = new GraphicsPath();
			this._graphicsPathKreis = new GraphicsPath();
			PositionRaster = rasterPosition;
			Ausgang = new Adresse(parent);
			Position = new Point(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
			foreach (Gleis gl in Parent.GleisElemente.Elemente) {
				if (gl.GleisElementAnschluss(this)) {
					AnschlussGleis = gl;
					Parent.EingSchalterElemente.Hinzufügen(this);
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
		public EingangsSchalter(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, string[] elem)
		: base(parent, Convert.ToInt32(elem[1]), zoom, anzeigeTyp) {
			KurzBezeichnung = "Eing";
			string[] glAnschl = elem[2].Split(' ');

			Ausgang.SpeicherString = elem[3];
			Bezeichnung = elem[4];
			Stecker = elem[5];

			Gleis gl = Parent.GleisElemente.Element(Convert.ToInt32(glAnschl[0]));
			Gleisposition = Convert.ToInt32(glAnschl[1]);
			if (gl != null) {
				PositionRaster = gl.GetRasterPosition(this, Convert.ToInt32(glAnschl[1]));
				Position = new Point(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
				{
					AnschlussGleis = gl;
					Parent.EingSchalterElemente.Hinzufügen(this);
					_graphicsPath = new GraphicsPath();
					_graphicsPathKreis = new GraphicsPath();
					this.Berechnung();
				}
			}
		}
	
		

		public override bool MouseClick(Point punkt) {
			return this._graphicsPath.IsVisible(punkt);
		}

		public override void Berechnung() {
			Matrix matrix = new Matrix();
			matrix.Translate(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
			matrix.Scale(Zoom, Zoom);

			this._graphicsPath.Reset();
			this._graphicsPath.AddRectangle(new RectangleF(-0.45f, -0.45f, 0.9f, 0.9f));
			this._graphicsPath.Transform(matrix);
			this._graphicsPathKreis.Reset();
			this._graphicsPathKreis.AddEllipse(new RectangleF(-0.4f, -0.4f, 0.8f, 0.8f));
			this._graphicsPathKreis.Transform(matrix);
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
			Color farbePinsel = Color.Transparent;
			Color farbeStift = Color.Transparent;

			switch (this.AnzeigenTyp) {
				case AnzeigeTyp.Bearbeiten:
					if (AnschlussGleis == null) {
						transpanz = 128;
					}
					farbeStift = Color.FromArgb(transpanz, Color.Black);
					farbePinsel = Color.FromArgb(transpanz, Color.DarkGray);
					if (this.ElementZustand == Elementzustand.Selektiert)
						farbePinsel = Color.FromArgb(transpanz, Color.Yellow);
					break;
				case AnzeigeTyp.Bedienen:
					if (Passiv) {
						transpanz = 128;
					}
					if (this.Ausgang.EingangAbfragen())
						farbePinsel = Color.FromArgb(transpanz, Color.Yellow);
					else
						farbePinsel = Color.FromArgb(transpanz, Color.DarkGray);
					farbeStift = Color.FromArgb(transpanz, Color.Black);
					break;
			}

			SolidBrush pinsel = new SolidBrush(farbePinsel);
			Pen stift = new Pen(farbeStift, 1);

			graphics.FillPath(pinsel, this._graphicsPath);
			graphics.DrawPath(stift, this._graphicsPath);
		}
	}
}


