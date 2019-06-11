using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using MoBaSteuerung.Elemente;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace MoBaSteuerung.Elemente
{
    /// <summary>
    /// Fahrregler
    /// </summary>
    public class Regler : RasterAnlagenElement
    {
        #region privateFelder

        private Color fuellFarbe;
        private GraphicsPath _graphicsPath;
        private GraphicsPath _graphicsPathText;
        private StringFormat _stringFormat;

        #endregion  //privateFelder

        #region Properties

        public Color Farbe
        {
            get { return fuellFarbe; }
        }
        /// <summary>
        /// zum Speichern in der Anlagen-Datei
        /// </summary>
        public override string SpeicherString
        {
            get
            {
                string spString = "Regler"
                    + "\t" + ID
                    + "\t" + PositionRaster.X
                    + "\t" + PositionRaster.Y
                    + "\t" + fuellFarbe.Name
                    + "\t" + fuellFarbe.ToArgb()
                    + "\t" + Bezeichnung
                    + "\t" + Stecker;
                return spString;
            }
        }

        public override string InfoString {
            get {
                return "Regler " + this.ID;
            }
        }

        public string FuellFarbe
        {
            set { }
        }
        
        #endregion//Properties


        #region Konstruktoren

        public Regler(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, string[] elem)
        : base(parent, Convert.ToInt32(elem[1]), zoom, anzeigeTyp)
        {
            this._stringFormat = new StringFormat();
            PositionRaster = new Point(Convert.ToInt32(elem[2]), Convert.ToInt32(elem[3]));
            Parent.ReglerElemente.Hinzufügen(this);
            Position = new Point(PositionRaster.X * zoom, PositionRaster.Y * zoom);
            this._graphicsPath = new GraphicsPath();
            this._graphicsPathText = new GraphicsPath();
            this.fuellFarbe= Color.FromName(elem[4]);
            if (elem.Length > 6) this.Bezeichnung = elem[6];
            if (elem.Length > 7) this.Stecker = elem[7];
            KurzBezeichnung = "Reg";
            this.Berechnung();
        }

		#endregion

		#region öffentliche Methoden

		public override bool MouseClick(Point punkt)
		{
			return this._graphicsPath.IsVisible(punkt);
		}

		/// <summary>
		/// berechnet die Grafik
		/// </summary>
		public override void Berechnung()
        {
            Position = new Point(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
            Matrix matrix = new Matrix();
            matrix.Translate(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
            matrix.Scale(Zoom, Zoom);
            this._graphicsPath.Reset();
            this._graphicsPath.AddRectangle(new RectangleF(-0.45f, -0.45f, 0.9f, 0.9f));
            Matrix matrixText = new Matrix();
            matrixText.Translate(this.PositionRaster.X * this.Zoom, this.PositionRaster.Y * this.Zoom);
            matrixText.Scale(this.Zoom, this.Zoom);
            this._graphicsPathText.Reset();
            this._graphicsPathText.AddString(ID.ToString() + " " + this.Bezeichnung, new FontFamily("Arial"), 0, 0.5f, new PointF(0, -0.3f), this._stringFormat);
                                                            //(ID.ToString() + " " + this.Bezeichnung, new FontFamily("Arial"), 0, 0.5f, this.DrehenUmPunkt(new PointF(0, 0), new PointF(0, -0.25f), winkel), this.stringFormat);
                            this._graphicsPathText.Transform(matrixText); 
            this._graphicsPath.Transform(matrix);
        }

        public override void ElementZeichnen(Graphics graphics)
        {
            //int transpanz = 255;
            Color farbePinsel =  fuellFarbe;// Color.Transparent;
            Color farbeStift = Color.Black;//Transparent;
            SolidBrush pinsel = new SolidBrush(farbePinsel);
            Pen stift = new Pen(farbeStift,1 );
            graphics.FillPath(pinsel, this._graphicsPath);
            graphics.DrawPath(stift, this._graphicsPath);
            graphics.DrawPath(Pens.Black, this._graphicsPathText);
        }
		#endregion //öffentliche Methoden
	}
}
