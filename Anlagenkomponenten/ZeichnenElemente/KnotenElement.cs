using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Enum;

namespace MoBaSteuerung.Elemente {
    

    /// <summary>
    /// Knoten, definiert Gleis-Endpunkte
    /// </summary>
    public class Knoten : RasterAnlagenElement {
        private Color füllFarbe;
        private GraphicsPath graphicsPath;
        private int _attachedTracks = 0;
        private Gleis[] _gleise = new Gleis[4];
        private Weiche[] _weichen = new Weiche[2];

        /// <summary>
        /// zum Speichern in der Anlagen-Datei
        /// </summary>
        public override string SpeicherString {
            get {
                return "Knot"
                    + "\t" + ID
                    + "\t" + PositionRaster.X
                    + "\t" + PositionRaster.Y; 
            }
        }

        public override string InfoString {
            get {
                return "Knoten " + this.ID;
            }
        }


        public Knoten(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, Point positionRaster)
        : base(parent, 0, zoom, anzeigeTyp) {
            PositionRaster = positionRaster;
            Position = new Point(positionRaster.X * zoom, positionRaster.Y * zoom);
            this.graphicsPath = new GraphicsPath();

            this.füllFarbe = Color.White;
            this.Berechnung();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iD"></param>
        /// <param name="zoom"></param>
        /// <param name="anzeigeTyp"></param>
        /// <param name="positionRaster"></param>
        public Knoten(AnlagenElemente parent, Int32 iD, Int32 zoom, AnzeigeTyp anzeigeTyp, Point positionRaster)
         : base(parent, iD, zoom, anzeigeTyp) {
            PositionRaster = positionRaster;
            Position = new Point(positionRaster.X * zoom, positionRaster.Y * zoom);
            this.graphicsPath = new GraphicsPath();
            Parent.KnotenElemente.Hinzufügen(this);

            this.füllFarbe = Color.White;
            this.Berechnung();
        }

        public Knoten(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, string[] elem)
          : base(parent, Convert.ToInt32(elem[1]), zoom, anzeigeTyp) {
            PositionRaster = new Point(Convert.ToInt32(elem[2]), Convert.ToInt32(elem[3]));
            Parent.KnotenElemente.Hinzufügen(this);

            Position = new Point(PositionRaster.X * zoom, PositionRaster.Y * zoom);
            this.graphicsPath = new GraphicsPath();
            this.füllFarbe = Color.White;
            this.Berechnung();
        }


        /// <summary>
        /// 
        /// </summary>
        public bool MouseEnter {
            set {
                if (value) {
                    this.füllFarbe = Color.Red;
                }
                else {
                    this.füllFarbe = Color.White;
                }
            }
        }

        public Weiche[] Weichen {
            get {
                return _weichen;
            }

            set {
                _weichen = value;
            }
        }

        public Gleis[] Gleise {
            get {
                return _gleise;
            }

            set {
                _gleise = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        public override void ElementZeichnen(Graphics graphics) {
            switch (this.AnzeigenTyp) {
                case AnzeigeTyp.Bedienen:
                    //graphics.DrawPath(Pens.Black, this.graphicsPath);
                    //graphics.DrawEllipse(Pens.Black, new Rectangle(90, 90, 20, 20));
                    break;
                case AnzeigeTyp.Bearbeiten:
                    //graphics.FillPath(new SolidBrush(this.füllFarbe), this.graphicsPath);
                    if (this.ElementZustand == Elementzustand.Selektiert)
                        graphics.FillPath(Brushes.Yellow, this.graphicsPath);
                    graphics.DrawPath(Pens.Black, this.graphicsPath);
                    
                    break;
            }
        }
        /// <summary>
        /// berechnet die Zeichnen-Graphik
        /// </summary>
        public override void Berechnung() {
            Position = new Point(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
            this.graphicsPath.Reset();
            this.graphicsPath.AddEllipse(new RectangleF(Position.X - 0.225f * Zoom, Position.Y - 0.225f * Zoom, 0.45f * Zoom, 0.45f * Zoom));
        }

        public override bool MouseClick(Point punkt) {
            return this.graphicsPath.GetBounds().Contains(punkt);
        }

        /// <summary>
        /// fügt ein Gleis an einer bestimmten Position ein (beim Gleis neu erstellen) und
        /// erzeugt neue Weichen falls nötig
        /// </summary>
        /// <param name="track"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool AttachTrack(Gleis track, int position) {
            if (_gleise[position] == null) {
                _gleise[position] = track;
                _attachedTracks++;
                if (_gleise[0] != null && _gleise[1] != null && Weichen[0] == null)
                    Weichen[0] = new Weiche(Parent, 10 * ID + 0, Zoom, AnzeigenTyp, this, _gleise[0], _gleise[1]);
                if (_gleise[2] != null && _gleise[3] != null && Weichen[1] == null)
                    Weichen[1] = new Weiche(Parent, 10 * ID + 1, Zoom, AnzeigenTyp, this, _gleise[2], _gleise[3]);
                return true;
            }
            return false;
        }

        public bool AttachTrack(Gleis track) {
            int angle = track.GetDirection(this);
            switch (_attachedTracks) {
                case 0:
                    _gleise[0] = track;
                    _attachedTracks++;
                    return true;
                case 4:
                    break;
                default:
                    bool success = false;
                    for (int i = 0; i < 4; i++) {
                        if (_gleise[i] != null) {
                            int diff = Math.Abs((_gleise[i].GetDirection(this) - angle));
                            if (diff == 1 || diff == 7) {
                                if (i == 0 || i == 2)
                                    success = AttachTrack(track, i + 1);
                                else
                                    success = AttachTrack(track, i - 1);
                            }
                            else if (diff > 2 && diff < 6) {
                                if (i > 1)
                                    i = 0;
                                else
                                    i = 2;

                                success = AttachTrack(track, i);
                                if (!success)
                                    success = AttachTrack(track, i + 1);
                            }
                        }
                        if (success)
                            return true;

                    }
                    break;
            }
            return false;
        }

        public int GetGleisAnschlussNr(Gleis gleis) {
            for (int i = 0; i < 4; i++)
                if (gleis == _gleise[i])
                    return i;
            return -1;
        }

        public int GetWeichenAnschlussNr(Weiche weiche) {
            for (int i = 0; i < 2; i++)
                if (weiche == _weichen[i])
                    return i;
            return -1;
        }

        //public override void DragDropElementVerknüpfungenAktualisieren() {
        //    foreach (Gleis gl in this._gleise)
        //        if(gl != null) { 
        //            gl.Berechnung();
        //            foreach (Entkuppler entk in gl.Entkuppler)
        //                entk.Berechnung();
        //            if (gl.Schalter != null)
        //                gl.Schalter.Berechnung();
        //            foreach (Signal sn in gl.Signale)
        //                if (sn != null)
        //                    sn.Berechnung();
        //        }
        //    foreach (Weiche we in this._weichen)
        //        if (we != null)
        //            we.Berechnung();
        //}
    }
}