﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.MCSpeicher;


namespace MoBaSteuerung.Elemente {

    /// <summary>
    /// Weiche
    /// </summary>
    public class Weiche : AnlagenElement {
        //private Rectangle rechteckFillEllipse;
        private Color farbeFillEllipse;
        //private Rectangle rechteckDrawEllipse;
        //private Point[] linienPositionen;
        private Color farbeLinien;
       // private Rectangle rechteckText;
        private StringFormat stringFormat;
        //private Font stringFont;


        private Gleis[] _gleise = new Gleis[2] { null, null };
        private Knoten _knoten;
        private GraphicsPath[] graphicsPathLinien = new GraphicsPath[2] { new GraphicsPath(), new GraphicsPath() };
        private bool _grundstellung = false;

        /// <summary>
        /// zum Speichern in der Anlagen-Datei
        /// </summary>
        public override string SpeicherString {
            get {
                return "Weiche"
                 + "\t" + ID
                 + "\t" + _knoten.ID + " " + _knoten.GetWeichenAnschlussNr(this)
                 + "\t" + Grundstellung.ToString()
                 + "\t" + Ausgang.SpeicherString
                 + "\t" + Bezeichnung 
                 + "\t" + Stecker; 
            }
        }

        public override string InfoString {
            get {
                return "Weiche " + this.ID;
            }
        }

        public bool Grundstellung {
            get {
                return _grundstellung;
            }

            set {
                _grundstellung = value;
            }
        }



        public Weiche(AnlagenElemente parent, Int32 iD, Int32 zoom, AnzeigeTyp anzeigeTyp, Knoten node, Gleis gleis1, Gleis gleis2)
         : base(parent, iD, zoom, anzeigeTyp) {
            Ausgang = new Adresse(parent);
            KurzBezeichnung = "We";
            _gleise[0] = gleis1;
            _gleise[1] = gleis2;
            _knoten = node;
            Parent.WeicheElemente.Hinzufügen(this);
            this.stringFormat = new StringFormat();
            this.stringFormat.Alignment = StringAlignment.Far;
            this.stringFormat.LineAlignment = StringAlignment.Far;

            this.farbeFillEllipse = Color.Red;
            this.farbeLinien = Color.Black;
            this.Berechnung();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        public override void ElementZeichnen(Graphics graphics) {

            Pen stift = new Pen(Color.Black, (Single)(this.Zoom * 0.4));
            if (this.AnzeigenTyp == AnzeigeTyp.Bearbeiten && this.ElementZustand == Elementzustand.Selektiert)
                stift.Color = Color.Yellow;
            stift.EndCap = LineCap.Flat;
            stift.StartCap = LineCap.Round;

            bool inv = false;
            if (this.ElementZustand == Elementzustand.An)
                inv = true;
            if (Grundstellung)
                inv = !inv;

            if (inv)
                graphics.DrawPath(stift, graphicsPathLinien[0]);
            else
                graphics.DrawPath(stift, graphicsPathLinien[1]);
        }

        public override void Berechnung() {
            for (int i = 0; i < 2; i++) {
                Matrix matrix = new Matrix();
                matrix.Translate(_knoten.Position.X, _knoten.Position.Y);
                matrix.Rotate(-_gleise[i].GetDirection(_knoten) * 45);

                graphicsPathLinien[i].Reset();
                graphicsPathLinien[i].AddLine(0, 0, (int)(0.7 * Zoom), 0);
                graphicsPathLinien[i].Transform(matrix);
            }
        }

        public override bool MouseClick(Point punkt) {
            RectangleF t0 = this.graphicsPathLinien[0].GetBounds();
            RectangleF t1 = this.graphicsPathLinien[1].GetBounds();
            if (t0.Width == 0) {
                t0.Inflate((Single)(this.Zoom * 0.2), 0);
            }
            else if(t0.Height == 0) {
                t0.Inflate(0, (Single)(this.Zoom * 0.2));
            }
            if (t1.Width == 0) {
                t1.Inflate((Single)(this.Zoom * 0.2), 0);
            }
            else if (t1.Height == 0) {
                t1.Inflate(0, (Single)(this.Zoom * 0.2));
            }
            return t0.Contains(punkt) || t1.Contains(punkt);
        }
    }
}