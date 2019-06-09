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
	/// <summary>
	/// zur Bedienung von Zubehörservos
	/// </summary>
	public class Servo : RasterAnlagenElement {
		#region privateFelder
		private bool _sichtbar = true;
    private int _winkelEin = 135;
    private int _winkelAus = 45;
    private int _speed = 0;//0 -> speed max
    private string _beschriftung = "";
    //private GraphicsPath graphicsPath;
    private StringFormat _stringFormat;
    private GraphicsPath _graphicsPathHintergrund;
    private GraphicsPath _graphicsPathText;
    private GraphicsPath _graphicsPathLeftButton;
    private GraphicsPath _graphicsPathRightButton;
    private bool _winkelRegelung = false;//true -> Steuerung nicht über Ein und Aus,
																				 // winkelEin und -Aus sind dann die maximalen Endlagen
		#endregion// privateFelder

		#region Properties
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
		#endregion //Properties
		#region Konstruktoren
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
		#endregion //Konstruktoren

		#region oeffentlicheMethoden
		public override void Berechnung() {
            Position = new Point(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
            Matrix matrix = new Matrix();
            matrix.Translate(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
            matrix.Scale(Zoom, Zoom);
            //this.graphicsPathText.Reset();
            this._graphicsPathText = new GraphicsPath();
            this._graphicsPathText.AddString(_beschriftung, new FontFamily("Arial"), 0, 0.6f, new PointF(-0.5f, -0.36f), this._stringFormat);
            RectangleF rechteck = _graphicsPathText.GetBounds();
            rechteck.Inflate(0.1f,0.1f);
            this._graphicsPathText.Transform(matrix);
            this._graphicsPathHintergrund = new GraphicsPath();
            this._graphicsPathHintergrund.AddRectangle(rechteck);
            this._graphicsPathHintergrund.Transform(matrix);
            this._graphicsPathLeftButton = new GraphicsPath();
            this._graphicsPathLeftButton.AddRectangle(new RectangleF(rechteck.Left, rechteck.Bottom, rechteck.Width / 2, rechteck.Height));
            this._graphicsPathLeftButton.Transform(matrix);
            this._graphicsPathRightButton = new GraphicsPath();
            this._graphicsPathRightButton.AddRectangle(new RectangleF(rechteck.Left+ rechteck.Width / 2, rechteck.Bottom, rechteck.Width / 2, rechteck.Height));
            this._graphicsPathRightButton.Transform(matrix);
        }

        public override void ElementZeichnen(Graphics graphics) {
            if ( _sichtbar)
            {
                Pen stift = new Pen(Color.Black, 1);
                SolidBrush pinselHintergrund = new SolidBrush(Color.White);
                SolidBrush pinselText = new SolidBrush(Color.Black);
                if(this.ElementZustand == Elementzustand.An) {
                    graphics.DrawPath(stift, _graphicsPathLeftButton);
                    graphics.FillPath(pinselHintergrund, _graphicsPathLeftButton);
                    graphics.DrawPath(stift, _graphicsPathRightButton);
                    graphics.FillPath(pinselHintergrund, _graphicsPathRightButton);

                    pinselHintergrund.Color = Color.Blue;
                    //stift.Color = Color.White;
                    pinselText.Color = Color.White;

                }
                graphics.DrawPath(stift, _graphicsPathHintergrund);
                graphics.FillPath(pinselHintergrund, _graphicsPathHintergrund);
                graphics.FillPath(pinselText, this._graphicsPathText);
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
                if (this._graphicsPathLeftButton.GetBounds().Contains(punkt)) {
                    Parent.AktiverServoAction = ServoAction.LinksClick;
                }
                if (this._graphicsPathRightButton.GetBounds().Contains(punkt)) {
                    Parent.AktiverServoAction = ServoAction.RechtsClick;
                }
            }
            return this._graphicsPathHintergrund.GetBounds().Contains(punkt);
        }
		#endregion //oeffentlicheMethoden
	}
}
