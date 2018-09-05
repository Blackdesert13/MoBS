using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using MoBaSteuerung.Elemente;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace MoBa.Elemente
{
    public class Servo : RasterAnlagenElement {
        private bool _sichtbar = true;

        private int _winkelEin = 135;
        private int _winkelAus = 45;
        private int _speed = 0;//0 -> speed max
        private string _beschriftung = "";
        private GraphicsPath graphicsPath;
        private StringFormat stringFormat;
        private GraphicsPath graphicsPathHintergrund;
        private GraphicsPath graphicsPathText;
        private GraphicsPath graphicsPathLeftButton;
        private GraphicsPath graphicsPathRightButton;

        private bool _winkelRegelung = false;//true -> Steuerung nicht über Ein und Aus,
                                             // winkelEin und -Aus sind dann die maximalen Endlagen

        /// <summary>
        /// zum Speichern in der Anlagen-Datei
        /// </summary>
        public override string SpeicherString {
            get {
                return "Servo"
                       + "\t" + ID
                       + "\t" + (_sichtbar?(PositionRaster.X + "\t" + PositionRaster.Y):"-1\t-1")
                       + "\t" + _winkelEin
                       + "\t" + _winkelAus
                       + "\t" + _speed
                       + "\t" + _winkelRegelung
                       + "\t" + Ausgang.SpeicherString
                       + "\t" + _beschriftung
                       + "\t" + Bezeichnung
                       + "\t" + Stecker;
            }
        }


        public Servo(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp)
            : base(parent, 0, zoom, anzeigeTyp) {
        }

        public Servo(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, string[] elem)
        : base(parent, Convert.ToInt32(elem[1]), zoom, anzeigeTyp) {
            PositionRaster = new Point(Convert.ToInt32(elem[2]), Convert.ToInt32(elem[3]));
            if(PositionRaster.X < 0 || PositionRaster.Y < 0) {
                _sichtbar = false;
            }
            _winkelEin = Convert.ToInt16(elem[4]);
            _winkelAus = Convert.ToInt16(elem[5]);
            _speed     = Convert.ToInt16(elem[6]);
            _winkelRegelung = Convert.ToBoolean(elem[7]);
            Ausgang.SpeicherString = elem[8];
            _beschriftung          = elem[9];
            Bezeichnung           = elem[10];
            Stecker               = elem[11];
            this.Parent.ServoElemente.Hinzufügen(this);
            this.Berechnung();
        }

        public override void Berechnung() {
            Position = new Point(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
            Matrix matrix = new Matrix();
            matrix.Translate(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
            matrix.Scale(Zoom, Zoom);
            //this.graphicsPathText.Reset();
            this.graphicsPathText = new GraphicsPath();
            this.graphicsPathText.AddString(_beschriftung, new FontFamily("Arial"), 0, 0.6f, new PointF(-0.5f, -0.36f), this.stringFormat);
            RectangleF rechteck = graphicsPathText.GetBounds();
            rechteck.Inflate(0.1f,0.1f);
            this.graphicsPathText.Transform(matrix);
            this.graphicsPathHintergrund = new GraphicsPath();
            this.graphicsPathHintergrund.AddRectangle(rechteck);
            this.graphicsPathHintergrund.Transform(matrix);
            this.graphicsPathLeftButton = new GraphicsPath();
            this.graphicsPathLeftButton.AddRectangle(new RectangleF(rechteck.Left, rechteck.Bottom, rechteck.Width / 2, rechteck.Height));
            this.graphicsPathLeftButton.Transform(matrix);
            this.graphicsPathRightButton = new GraphicsPath();
            this.graphicsPathRightButton.AddRectangle(new RectangleF(rechteck.Left+ rechteck.Width / 2, rechteck.Bottom, rechteck.Width / 2, rechteck.Height));
            this.graphicsPathRightButton.Transform(matrix);
        }

        public override void ElementZeichnen(Graphics graphics) {
            if ( _sichtbar)
            {
                Pen stift = new Pen(Color.Black, 1);
                SolidBrush pinselHintergrund = new SolidBrush(Color.White);
                SolidBrush pinselText = new SolidBrush(Color.Black);
                if(this.ElementZustand == Elementzustand.An) {
                    graphics.DrawPath(stift, graphicsPathLeftButton);
                    graphics.FillPath(pinselHintergrund, graphicsPathLeftButton);
                    graphics.DrawPath(stift, graphicsPathRightButton);
                    graphics.FillPath(pinselHintergrund, graphicsPathRightButton);

                    pinselHintergrund.Color = Color.Blue;
                    //stift.Color = Color.White;
                    pinselText.Color = Color.White;

                }
                graphics.DrawPath(stift, graphicsPathHintergrund);
                graphics.FillPath(pinselHintergrund, graphicsPathHintergrund);
                graphics.FillPath(pinselText, this.graphicsPathText);
            }
        }

        public override bool AusgangToggeln() {
            bool returnValue = base.AusgangToggeln();
            if (this.ElementZustand == Elementzustand.An) {
                Parent.AktiverServo = this;
            }
            else if(this.ElementZustand == Elementzustand.Aus) {
                Parent.AktiverServo = null;
            }
            return false;
        }

        public override bool MouseClick(Point punkt) {
            if(this.ElementZustand == Elementzustand.An && this.AnzeigenTyp == AnzeigeTyp.Bedienen) {
                if (this.graphicsPathLeftButton.GetBounds().Contains(punkt)) {
                    Parent.AktiverServoAction = ServoAction.LinksClick;
                }
                if (this.graphicsPathRightButton.GetBounds().Contains(punkt)) {
                    Parent.AktiverServoAction = ServoAction.RechtsClick;
                }
            }
            return this.graphicsPathHintergrund.GetBounds().Contains(punkt);
        }
    }
}
