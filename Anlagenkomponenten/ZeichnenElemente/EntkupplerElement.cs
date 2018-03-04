using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using MoBaSteuerung.Anlagenkomponenten.MCSpeicher;

namespace MoBaSteuerung.Elemente {
    
    /// <summary>
    /// Entkuppler
    /// </summary>
    public class Entkuppler : GleisRasterAnlagenElement {
        //private Rectangle _mausRechteck = new Rectangle();
        private GraphicsPath _passivZeichen;
        private GraphicsPath _aktivZeichen;

        /// <summary>
        /// zum Speichern in der Anlagen-Datei
        /// </summary>
        public override string SpeicherString {
            get {
                return "Entkuppler"
                       + "\t" + ID
                       + "\t" + AnschlussGleis.ID + " " + Gleisposition
                       + "\t" + Ausgang.SpeicherString
                       + "\t" + Bezeichnung
                       + "\t" + Stecker; ;
            }
        }

        /// <summary>
        /// zum Speichern in der Anlagen-Datei
        /// </summary>
        public override string InfoString {
            get {
                return "Entkuppler " + this.ID;
            }
        }

        public Entkuppler(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, Point rasterPosition)
         : base(parent, 0, zoom, anzeigeTyp) {
            PositionRaster = rasterPosition;
            Position = new Point(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
            KurzBezeichnung = "Ek";
            this.Berechnung();
        }

        public Entkuppler(AnlagenElemente parent, Int32 iD, Int32 zoom, AnzeigeTyp anzeigeTyp, Point rasterPosition)
         : base(parent, iD, zoom, anzeigeTyp) {
            Ausgang = new Adresse(parent);
            PositionRaster = rasterPosition;
            Position = new Point(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
            KurzBezeichnung = "Ek";
            foreach (Gleis gl in Parent.GleisElemente.Elemente) {
                if (gl.GleisElementAnschluss(this)) {
                    AnschlussGleis = gl;
                    Parent.EntkupplerElemente.Hinzufügen(this);
                    this.Berechnung();
                    break;
                }
            }
        }
        
        /// <summary>
        /// Zum laden aus der Anlagen-Datei
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="zoom"></param>
        /// <param name="anzeigeTyp"></param>
        /// <param name="elem"></param>
        public Entkuppler(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, string[] elem)
            : base(parent, Convert.ToInt32(elem[1]), zoom, anzeigeTyp) {
            Ausgang = new Adresse(parent);
            KurzBezeichnung = "Ek";
            string[] glAnschl = elem[2].Split(' ');
            Gleis gl = Parent.GleisElemente.Element(Convert.ToInt32(glAnschl[0]));
            if (gl != null) {
                PositionRaster = gl.GetRasterPosition(this, Convert.ToInt32(glAnschl[1]));
                Position = new Point(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
                if (gl.GleisElementAnschluss(this)) {
                    AnschlussGleis = gl;
                    Parent.EntkupplerElemente.Hinzufügen(this);
                    Ausgang.SpeicherString = elem[3];
                    Bezeichnung = elem[4];
                    this.Berechnung();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        public override void ElementZeichnen(Graphics graphics) {
            int transpanz = 255;
            SolidBrush pinsel = new SolidBrush(Color.FromArgb(transpanz, Color.Yellow));
            Pen stift = new Pen(Color.FromArgb(transpanz, Color.Black), 1);
            switch (this.AnzeigenTyp)
            {
                case AnzeigeTyp.Bearbeiten:
                    if (AnschlussGleis == null) {transpanz = 128;}
                    break;
                case AnzeigeTyp.Bedienen:
                    if (Passiv) { transpanz = 128;}
                    break;
            }
            switch (this.ElementZustand) {
                case Elementzustand.An:
                    graphics.DrawPath(stift, _aktivZeichen);
                    graphics.FillPath(pinsel, _aktivZeichen);
                    break;
                case Elementzustand.Selektiert:
                    float zoom = this.Zoom;
                    graphics.FillEllipse(pinsel, this.PositionRaster.X * zoom - 0.45f * zoom, this.PositionRaster.Y * zoom - 0.45f * zoom, 0.9f * zoom, 0.9f * zoom);
                    break;
                case Elementzustand.Aus:
                    graphics.DrawPath(stift, _passivZeichen);
                    graphics.FillPath(pinsel, _passivZeichen);
                    break;
            }
        }

        public override void Berechnung() {
            if (AnschlussGleis != null)
                PositionRaster = AnschlussGleis.GetRasterPosition(this, Gleisposition);
            Matrix matrix = new Matrix();
            matrix.Translate(this.PositionRaster.X * this.Zoom, this.PositionRaster.Y * this.Zoom);
            matrix.Scale(this.Zoom, this.Zoom);
            this._aktivZeichen = new GraphicsPath();
            this._aktivZeichen.AddLines(new PointF[] { new PointF(-0.45f, 0), new PointF(0.45f, 0), new PointF(0, -0.45f) });
            this._aktivZeichen.CloseFigure();
            this._passivZeichen = (GraphicsPath)this._aktivZeichen.Clone();

            this._aktivZeichen.Transform(matrix);
            matrix.Rotate(180);
            this._passivZeichen.Transform(matrix);
        }

        public override bool MouseClick(Point punkt) {
            return (_passivZeichen.IsVisible(punkt) || _aktivZeichen.IsVisible(punkt));
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
    }
}