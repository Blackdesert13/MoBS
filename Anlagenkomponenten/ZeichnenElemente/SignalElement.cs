using System;
using System.Collections.Generic;
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
        private GraphicsPath graphicsPathHintergrund;
        private GraphicsPath graphicsPathKreis;
        private GraphicsPath graphicsPathLinien;
        private GraphicsPath graphicsPathText;

        private Color farbe;
        private StringFormat stringFormat;
 
        private bool _inZeichenRichtung = false; 
        private InfoFenster infoFenster = null;
        //private int zugNr = 0;
        private Zug zug ;

        /// <summary>
        /// zum Speichern in der Anlagen-Datei
        /// </summary>
        public override string SpeicherString {
            get {
                int infoNr = 0;
                if ( infoFenster != null) { infoNr = infoFenster.ID; }
                return "Signal"
                     + "\t" + ID
                     + "\t" + AnschlussGleis.ID + " " + Gleisposition
                     + "\t" + InZeichenRichtung.ToString()
                     + "\t" + infoNr
                     + "\t" + Ausgang.SpeicherString
                     + "\t" + Bezeichnung
                     + "\t" + Stecker;
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

        public Signal(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, Point rasterPosition, bool inZeichenRichtung)
         : base(parent, 0, zoom, anzeigeTyp) {
            KurzBezeichnung = "Sn";
            PositionRaster = rasterPosition;
            Position = new Point(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
            this._inZeichenRichtung = inZeichenRichtung;

            this.graphicsPathHintergrund = new GraphicsPath();
            this.graphicsPathKreis = new GraphicsPath();
            this.graphicsPathLinien = new GraphicsPath();

            this.farbe = Color.Red;

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

                    this.stringFormat = new StringFormat();
                    this.stringFormat.Alignment = StringAlignment.Center;
                    this.stringFormat.LineAlignment = StringAlignment.Center;

                    this.graphicsPathHintergrund = new GraphicsPath();
                    this.graphicsPathKreis = new GraphicsPath();
                    this.graphicsPathLinien = new GraphicsPath();
                    this.graphicsPathText = new GraphicsPath();

                    this.farbe = Color.Red;

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

            Gleis gl = Parent.GleisElemente.Element(Convert.ToInt32(glAnschl[0]));
            if (gl != null) {
                PositionRaster = gl.GetRasterPosition(this, Convert.ToInt32(glAnschl[1]));
                Position = new Point(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
                InZeichenRichtung = Convert.ToBoolean(elem[3]);

                if (gl.GleisElementAnschluss(this)) {
                    Ausgang.SpeicherString = elem[5];
                    AnschlussGleis = gl;
                    Parent.SignalElemente.Hinzufügen(this);

                    this.stringFormat = new StringFormat();
                    this.stringFormat.Alignment = StringAlignment.Center;
                    this.stringFormat.LineAlignment = StringAlignment.Center;

                    this.graphicsPathHintergrund = new GraphicsPath();
                    this.graphicsPathKreis = new GraphicsPath();
                    this.graphicsPathLinien = new GraphicsPath();
                    this.graphicsPathText = new GraphicsPath();

                    this.farbe = Color.Red;

                    this.Berechnung();
                }
            }
            infoFensterLaden(Convert.ToInt16(elem[4]));
        }

        private void infoFensterLaden(int Nummer)
        {
            infoFenster = this.Parent.InfoElemente.Element(Nummer);
        }

        public string Anzeige {
            set
            {
                if(infoFenster != null)
                {
                    infoFenster.Text = value;
                }
            }
        }

        /*public void ZugErmitteln()
        {
        }*/

        public int Zug {
            set
            {
                int nZug = value;
                if(nZug == 0)
                {
                    zug = null;
                    infoFenster.Text = "";//"Sn" + ID;
                }
                else
                {
                    zug = Parent.ZugElemente.Element(nZug);
                    if(infoFenster != null)
                        infoFenster.Text = zug.Anzeige;
                }
            }
			get
            {
                if (zug != null)
                { return zug.ID; }
                else { return 0; }
			}
        }

        /// <summary>
        /// wechselt die Farbe grün-rot
        /// </summary>
        public bool Durchfahrt {
            set {
                if (value) {
                    this.farbe = Color.Green;
                }
                else {
                    this.farbe = Color.Red;
                }
            }
        }


        public bool InZeichenRichtung {
            get {
                return _inZeichenRichtung;
            }

            set {
                _inZeichenRichtung = value;
            }
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
                    if(this.ElementZustand == Elementzustand.Selektiert)
                        graphics.FillPath(Brushes.Yellow, this.graphicsPathHintergrund);
                    break;
                case AnzeigeTyp.Bedienen:
                    //if (infoFenster != null)
                     //   infoFenster.Text = zug.Anzeige;

                    graphics.FillPath(Brushes.White, this.graphicsPathHintergrund);
                    if (Passiv) {
                        transpanz = 128;
                    }
                    if (this.ElementZustand == Elementzustand.An)
                        farbePinsel = Color.FromArgb(transpanz, Color.Green);
                    else
                        farbePinsel = Color.FromArgb(transpanz, Color.Red);
                    farbeStift = Color.FromArgb(transpanz, Color.Black);
                    break;
            }

            SolidBrush pinsel = new SolidBrush(farbePinsel);
            Pen stift = new Pen(farbeStift, 1);



            graphics.FillPath(pinsel, this.graphicsPathKreis);
            graphics.DrawPath(Pens.Black, this.graphicsPathKreis);
            graphics.DrawPath(Pens.Black, this.graphicsPathLinien);

            if (this.AnzeigenTyp == AnzeigeTyp.Bedienen)
                graphics.DrawPath(Pens.Black, this.graphicsPathText);
        }

        /// <summary>
        /// berechnet die Grafik
        /// </summary>
        public override void Berechnung() {
            //Stopwatch watch = new Stopwatch();
            //watch.Start();
            if (AnschlussGleis != null)
                PositionRaster = AnschlussGleis.GetRasterPosition(this, Gleisposition);

            int winkel = 0;
            if (AnschlussGleis != null)
                winkel = AnschlussGleis.GetDirection(InZeichenRichtung) * 45;

            Matrix matrix = new Matrix();
            matrix.Translate(this.PositionRaster.X * this.Zoom, this.PositionRaster.Y * this.Zoom);
            matrix.Scale(this.Zoom, this.Zoom);
            matrix.Rotate(-winkel);

            this.graphicsPathHintergrund.Reset();
            this.graphicsPathHintergrund.AddEllipse(new RectangleF(-0.5f, -0.5f, 1f, 1f));
            this.graphicsPathHintergrund.Transform(matrix);

            this.graphicsPathKreis.Reset();
            this.graphicsPathKreis.AddEllipse(new RectangleF(0f, 0f, 0.5f, 0.5f));
            this.graphicsPathKreis.Transform(matrix);

            this.graphicsPathLinien.Reset();
            this.graphicsPathLinien.AddLines(new PointF[] { new PointF(0.0f, 0.25f), new PointF(-0.5f, 0.25f), new PointF(-0.5f, 0.5f), new PointF(-0.5f, 0.0f) });
            this.graphicsPathLinien.Transform(matrix);

            if (this.AnzeigenTyp == AnzeigeTyp.Bedienen) {
                Matrix matrixText = new Matrix();
                matrixText.Translate(this.PositionRaster.X * this.Zoom, this.PositionRaster.Y * this.Zoom);
                matrixText.Scale(this.Zoom, this.Zoom);
                this.graphicsPathText.Reset();
                this.graphicsPathText.AddString(ID.ToString(), new FontFamily("Arial"), 0, 0.5f, this.DrehenUmPunkt(new PointF(0, 0), new PointF(0, -0.25f), winkel), this.stringFormat);
                this.graphicsPathText.Transform(matrixText);
            }

            //watch.Stop();
            //double diff = watch.Elapsed.TotalMilliseconds;

        }


        public override bool MouseClick(Point punkt) {
            return this.graphicsPathHintergrund.GetBounds().Contains(punkt);
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
        public void ZugWechsel(int NeuesSignal)
        {
            int neueSignalNr = NeuesSignal;
            if(zug!=null)
                zug.SignalNummer = neueSignalNr;
        }
    }
}