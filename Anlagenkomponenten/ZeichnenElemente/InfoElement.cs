using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using MoBaSteuerung.Elemente;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace MoBaSteuerung.Elemente
{
	/// <summary>
	/// zur Anzeige von Infos (zB. über Haltestellen oder Zügen an Signalen)
	/// </summary>
	public class InfoFenster : GleisRasterAnlagenElement
	{
		private string txt = "";// = "1234567890";
		private StringFormat stringFormat;
		private GraphicsPath graphicsPathHintergrund;
		private GraphicsPath graphicsPathText;

		/// <summary>
		/// zum Speichern in der Anlagen-Datei
		/// </summary>
		public override string SpeicherString
		{
			get {
				return "Info"
					+ "\t" + ID
					+ "\t" + AnschlussGleis.ID + " " + Gleisposition
					+ "\t" + 0
					+ "\t" + " ";
			}
		}

		public override string InfoString
		{
			get {
				return "InfoElement " + this.ID;
			}
		}

		/// <summary>
		/// Konstruktor für Element Vorschau beim neuzeichnen
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="zoom"></param>
		/// <param name="anzeigeTyp"></param>
		/// <param name="rasterPosition"></param>
		public InfoFenster(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, Point rasterPosition)
				 : base(parent, 0, zoom, anzeigeTyp)
		{
			PositionRaster = rasterPosition;
			Position = new Point(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
			txt = "Infofeld";
			this.Berechnung();
		}

		/// <summary>
		/// Konstruktor für neuzeichnen abschließen und in Liste eintragen
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="iD"></param>
		/// <param name="zoom"></param>
		/// <param name="anzeigeTyp"></param>
		/// <param name="rasterPosition"></param>
		public InfoFenster(AnlagenElemente parent, Int32 iD, Int32 zoom, AnzeigeTyp anzeigeTyp, Point rasterPosition)
		 : base(parent, iD, zoom, anzeigeTyp)
		{
			PositionRaster = rasterPosition;
			Position = new Point(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
			foreach (Gleis gl in Parent.GleisElemente.Elemente) {
				if (gl.GleisElementAnschluss(this)) {
					AnschlussGleis = gl;
					Parent.InfoElemente.Hinzufügen(this);

					this.Berechnung();
					break;
				}
			}
		}

		/// <summary>
		/// Konstruktor zum laden aus der .anl-Datei
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="zoom"></param>
		/// <param name="anzeigeTyp"></param>
		/// <param name="elem"></param>
		public InfoFenster(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, string[] elem)
				: base(parent, Convert.ToInt32(elem[1]), zoom, anzeigeTyp)
		{
			string[] glAnschl = elem[2].Split(' ');
			Gleis gl = Parent.GleisElemente.Element(Convert.ToInt32(glAnschl[0]));
			Gleisposition = Convert.ToInt32(glAnschl[1]);
			if (gl != null) {
				PositionRaster = gl.GetRasterPosition(this, Convert.ToInt32(glAnschl[1]));
				Position = new Point(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
				AnschlussGleis = gl;

				Parent.InfoElemente.Hinzufügen(this);
				this.Text = "";//InfoString;
				this.Berechnung();
			}
		}

		/// <summary>
		/// Text zur Anzeige
		/// </summary>
		public string Text
		{
			set {
                txt = value; Berechnung();
            }
		}

		/// <summary>
		/// Diese Methode sucht in der Gleisliste nach einem Gleis, welches an der Position dieses Elementes liegt
		/// und wenn diese Position noch frei ist, wird dieses Element mit dem Gleis verknüpft.
		/// </summary>
		/// <returns>Gibt TRUE zurück, wenn dieses Objekt in einem Gleis eingetragen werden konnte</returns>
		public override bool AnschlussGleisSuchen()
		{
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
		public override bool BearbeitenAktualisierenNeuZeichnen()
		{
			GleisElementAustragen();
			bool aktualisierungNotwendig = AnschlussGleisSuchen();
			base.BearbeitenAktualisierenNeuZeichnen();
			return aktualisierungNotwendig;
		}

		public override bool GleisElementAustragen()
		{
			if (AnschlussGleis != null) {
				AnschlussGleis.GleisElementAustragen(this);
				this.AnschlussGleis = null;
				return true;
			}
			return false;
		}

		public override void Berechnung()
		{
			Matrix matrix = new Matrix();
			matrix.Translate(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
			matrix.Scale(Zoom, Zoom);
			//this.graphicsPathHintergrund.Reset();
			this.graphicsPathText = new GraphicsPath();
			this.graphicsPathText.AddString(txt, new FontFamily("Arial"), 0, 0.6f, new PointF(-0.5f, -0.36f), this.stringFormat);
			RectangleF rechteck = graphicsPathText.GetBounds();
			//rechteck.Inflate(0.1f,0.1f);
			rechteck.Inflate(0.1f, 0f);

			this.graphicsPathText.Transform(matrix);
			this.graphicsPathHintergrund = new GraphicsPath();
			this.graphicsPathHintergrund.AddRectangle(rechteck);
			//this.graphicsPathText.Reset();
			this.graphicsPathHintergrund.Transform(matrix);

		}

		public override bool MouseClick(Point punkt)
		{
			return this.graphicsPathHintergrund.IsVisible(punkt);
		}

		public override void ElementZeichnen(Graphics graphics)
		{
			//Color farbeStift = Color.Black;
			if (this.AnzeigenTyp == AnzeigeTyp.Bedienen) {
				Pen stift = new Pen(Color.Black, 1);
				SolidBrush pinsel = new SolidBrush(Color.White);
				SolidBrush pinsel1 = new SolidBrush(Color.Black);
				try {
					graphics.FillPath(pinsel, this.graphicsPathHintergrund);
					graphics.FillPath(pinsel1, this.graphicsPathText);
				}
				catch (Exception e) {
					Debug.Print(e.Message);
				}
			}
			if (this.AnzeigenTyp == AnzeigeTyp.Bearbeiten) {
				if(Parent.InfoElemente.Elemente.Contains(this))
					Text = " " + Convert.ToString(ID) + " ";

				int transpanz = 255;
				if (AnschlussGleis == null) {
					transpanz = 128;
				}

				Pen stift = new Pen(Color.FromArgb(transpanz, Color.Black), 1);
				SolidBrush pinsel = new SolidBrush(Color.FromArgb(transpanz, Color.White));
				if(this.ElementZustand == Elementzustand.Selektiert) {
					pinsel.Color = Color.FromArgb(transpanz, Color.Yellow);
				}
				SolidBrush pinsel1 = new SolidBrush(Color.FromArgb(transpanz, Color.Black));
				try {
					graphics.DrawPath(stift, this.graphicsPathHintergrund);
					graphics.FillPath(pinsel, this.graphicsPathHintergrund);
					graphics.FillPath(pinsel1, this.graphicsPathText);
				}
				catch (Exception e) {
					Debug.Print(e.Message);
				}
			}
			//graphics.DrawPath(stift, this.graphicsPathText);
		}
	}
}
