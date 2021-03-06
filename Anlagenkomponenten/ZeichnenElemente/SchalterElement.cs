﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Enum;

namespace MoBaSteuerung.Elemente
{

	/// <summary>
	/// Schalter für Gleise
	/// </summary>
	public class Schalter : GleisRasterAnlagenElement
	{
		private Color _füllFarbe;
		private GraphicsPath _graphicsPath;
		
		#region Properties
		/// <summary>
		/// zum Speichern in der Anlagen-Datei
		/// </summary>
		public override string SpeicherString
		{
			get {
				return "Schalter"
						+ "\t" + ID
						+ "\t" + AnschlussGleis.ID + " " + Gleisposition
						+ "\t" + Bezeichnung
						+ "\t" + KoppelungsString;
			}
		}

		public override string InfoString
		{
			get {
				return "Schalter " + this.ID;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool An
		{
			set
			{
				if (value)
				{
					this._füllFarbe = Color.Green;
				}
				else
				{
					this._füllFarbe = Color.Red;
				}
			}
		}
		#endregion //Properties

		#region Konstruktoren
		public Schalter(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, Point rasterPosition)
		 : base(parent, 0, zoom, anzeigeTyp)
		{
			_graphicsPath = new GraphicsPath();
			PositionRaster = rasterPosition;
			Position = new Point(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
			this.Berechnung();
		}

		public Schalter(AnlagenElemente parent, Int32 iD, Int32 zoom, AnzeigeTyp anzeigeTyp, Point rasterPosition)
		 : base(parent, iD, zoom, anzeigeTyp)
		{
			_graphicsPath = new GraphicsPath();
			PositionRaster = rasterPosition;
			Position = new Point(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
			foreach (Gleis gl in Parent.GleisElemente.Elemente) {
				if (gl.GleisElementAnschluss(this)) {
					AnschlussGleis = gl;
					Ausgang = AnschlussGleis.Ausgang;
					Parent.SchalterElemente.Hinzufügen(this);
					this.Berechnung();
					break;
				}
			}
		}

		public Schalter(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, string[] elem)
				: base(parent, Convert.ToInt32(elem[1]), zoom, anzeigeTyp)
		{
			string[] glAnschl = elem[2].Split(' ');
			Gleis gl = Parent.GleisElemente.Element(Convert.ToInt32(glAnschl[0]));
			if (elem.Length > 4) KoppelungsString = elem[4];
			if (gl != null) {
				PositionRaster = gl.GetRasterPosition(this, Convert.ToInt32(glAnschl[1]));
				Position = new Point(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);

				if (gl.GleisElementAnschluss(this)) {
					AnschlussGleis = gl;
					Ausgang = AnschlussGleis.Ausgang;

					Parent.SchalterElemente.Hinzufügen(this);
					_graphicsPath = new GraphicsPath();

					this.Berechnung();
				}
			}
		}
		#endregion //Konstruktoren

		/// <summary>
		/// 
		/// </summary>
		/// <param name="graphics"></param>
		public override void ElementZeichnen(Graphics graphics)
		{
			int transpanz = 255;
			Color farbePinsel = Color.Transparent;
			Color farbeStift = Color.Transparent;

			switch (this.AnzeigenTyp) {
				case AnzeigeTyp.Bearbeiten:
					if (AnschlussGleis == null) {
						transpanz = 128;
					}
					farbeStift = Color.FromArgb(transpanz, Color.Black);
					farbePinsel = Color.FromArgb(transpanz, Color.Red);
					if (this.AnzeigenTyp == AnzeigeTyp.Bearbeiten && this.ElementZustand == Elementzustand.Selektiert)
						farbePinsel = Color.FromArgb(transpanz, Color.Yellow);
					break;
				case AnzeigeTyp.Bedienen:
					if (Passiv) {
						transpanz = 128;
					}
					if (this.ElementZustand == Elementzustand.An)
						farbePinsel = Color.FromArgb(transpanz, Color.Green);
					else
						farbePinsel = Color.FromArgb(transpanz, Color.Red);
					farbeStift = Color.FromArgb(transpanz, Color.Black);
					if (Ausgang.IsLocked) {
						farbeStift = Color.FromArgb(transpanz, Color.Red);
					}
					break;
			}

			SolidBrush pinsel = new SolidBrush(farbePinsel);
			Pen stift = new Pen(farbeStift, 1);

			graphics.FillPath(pinsel, this._graphicsPath);
			graphics.DrawPath(stift, this._graphicsPath);
		}

		/// <summary>
		/// berechnet die Grafik zum Zeichnen
		/// </summary>
		public override void Berechnung()
		{
			if (AnschlussGleis != null)
			{ PositionRaster = AnschlussGleis.GetRasterPosition(this, Gleisposition); }
			Matrix matrix = new Matrix();
			matrix.Translate(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
			matrix.Scale(Zoom, Zoom);
			this._graphicsPath.Reset();
			this._graphicsPath.AddRectangle(new RectangleF(-0.4f, -0.4f, 0.8f, 0.8f));
			this._graphicsPath.Transform(matrix);
		}


		public override bool MouseClick(Point punkt)
		{
			return this._graphicsPath.IsVisible(punkt);
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

        public override bool AusgangToggeln()
        {
            return AnschlussGleis.AusgangToggeln();
        }
    }
}