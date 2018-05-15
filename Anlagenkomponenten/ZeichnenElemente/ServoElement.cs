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
    public class Servo : RasterAnlagenElement
    {//Entwurf von Jürgen
        private int _winkelEin = 135;
        private int _winkelAus = 45;
        private int _speed = 0;//0 -> speed max
        private string _beschriftung = "";
        private GraphicsPath graphicsPath;
        private StringFormat stringFormat;
        private GraphicsPath graphicsPathHintergrund;
        private GraphicsPath graphicsPathText;



        private bool _winkelRegelung = false;//true -> Steuerung nicht über Ein und Aus,
                                           // winkelEin und -Aus sind dann die maximalen Endlagen

        public Servo(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp)
            : base(parent, 0, zoom, anzeigeTyp)
        { }

        public Servo(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, string[] elem)
        : base(parent, Convert.ToInt32(elem[1]), zoom, anzeigeTyp)
        {
            PositionRaster = new Point(Convert.ToInt32(elem[2]), Convert.ToInt32(elem[3]));
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

        public override void Berechnung()
        {
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

        }

        public override void ElementZeichnen(Graphics graphics)
        {
            if ( PositionRaster.X!=0 && PositionRaster.Y != 0)
            {
                Pen stift = new Pen(Color.Black, 1);
                SolidBrush pinsel = new SolidBrush(Color.White);
                SolidBrush pinsel1 = new SolidBrush(Color.Black);
                graphics.DrawPath(stift, graphicsPathHintergrund);
                graphics.FillPath(pinsel, graphicsPathHintergrund);
                graphics.FillPath(pinsel1, this.graphicsPathText);
            }
        }
        public override bool MouseClick(Point punkt)
        {
            return this.graphicsPathHintergrund.GetBounds().Contains(punkt);
        }
    }
}
