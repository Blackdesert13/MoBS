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
    public class StartSignalGruppe: RasterAnlagenElement
    {
        #region privateFelder
        private GraphicsPath _graphicsPath = new GraphicsPath();
        private GraphicsPath _graphicsPathText = new GraphicsPath();
        private StringFormat _stringFormat;
        private List<Signal> _signaleListe;
        private List<string> _typListe;
        #endregion//private Felder
        
        #region Konstruktoren
        public StartSignalGruppe(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, string[] elem)
        : base(parent, Convert.ToInt32(elem[1]), zoom, anzeigeTyp)
        {
            Parent.SsgElemente.Hinzufügen(this);
            this._stringFormat = new StringFormat();
            PositionRaster = new Point(Convert.ToInt32(elem[2]), Convert.ToInt32(elem[3]));
            TypeListenString = elem[4];
            Bezeichnung = elem[5];
            SignalString = elem[6];
        }
        #endregion //Konstruktoren

        #region Properties
        public override string SpeicherString
        {
            get
            {
                return "SSG"
                    + "\t" + ID
                    + "\t" + PositionRaster.X
                    + "\t" + PositionRaster.Y
                    + "\t" + TypeListenString
                    + "\t" + Bezeichnung
                    + "\t" + SignalString;
            }
        }
		
        public string SignalString
        {
            get {
                string snString = "";
                foreach( Signal x in _signaleListe)
                { snString += x.ID + " "; }
                return snString.Trim();
            }
            set
            {
                string[] signale = value.Split(' ');
                List<Signal> signalListe = new List<Signal>();
                foreach (string x in signale)
                {
                    Signal sig = Parent.SignalElemente.Element(Convert.ToInt32(x));
                    if (sig != null) { signalListe.Add(sig); }
                }
                _signaleListe = signalListe; 
            }
        }

        public string TypeListenString
        {
            get {
                string lString = "";
                foreach(string x in _typListe) { lString += x + " "; }
                return lString.Trim() ;
            }
            set {
                string[] liste = value.Split(' ');
                _typListe = new List<string>(liste);
            }
        }
        #endregion//Eigenschaften

        #region oeffentlicheMethoden
        public override bool MouseClick(Point punkt)
        {
            return this._graphicsPath.IsVisible(punkt);
        }
        /// <summary>
        /// gibt die ID des ausgewählten Signal zurück
        /// </summary>
        /// <returns></returns>
        public int FSAuswahl()
        {  //suche nach Zug-Typen
            List< Signal> sgAuswahl = new List<Signal>();
            foreach (Signal x in _signaleListe)
            {
				if (x.IsLocked)
				{
					continue;
				}
                foreach(string s in _typListe)
                {
                    if((x.Zug != null)&& (x.Zug.ZugTyp == s) )
                    {
                        sgAuswahl.Add(x);
                        break;
                    }
                }
            }
//Auswahl nach ankunftszeit
            Signal ergSn = null;
            foreach(Signal x in sgAuswahl)
            {
                if (ergSn == null) { ergSn = x; }
                else
                {
                    if(ergSn.Zug.AnkunftsZeit > x.Zug.AnkunftsZeit) { ergSn = x; }
                }
            }
            if (ergSn != null) { return ergSn.ID; }
            else { return 0; }
           
        }

        public override bool AusgangToggeln()
        {
            
            return true;
        }

        /// <summary>
        /// Berechnet die Grafik
        /// </summary>
        public override void Berechnung()
        {
            Position = new Point(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
            Matrix matrix = new Matrix();
            matrix.Translate(PositionRaster.X * Zoom, PositionRaster.Y * Zoom);
            matrix.Scale(Zoom, Zoom);
            this._graphicsPath.Reset();
            this._graphicsPath.AddRectangle(new RectangleF(-0.5f, -0.5f, 1f, 1f));
            Matrix matrixText = new Matrix();
            matrixText.Translate(this.PositionRaster.X * this.Zoom, this.PositionRaster.Y * this.Zoom);
            matrixText.Scale(this.Zoom, this.Zoom);
            this._graphicsPathText.Reset();
            this._graphicsPathText.AddString( this.Bezeichnung, new FontFamily("Arial"), 0, 0.5f, new PointF(0, -0.3f), this._stringFormat);
            this._graphicsPathText.Transform(matrixText);
            this._graphicsPath.Transform(matrix);
        }
        public override void ElementZeichnen(Graphics graphics)
        {
            //int transpanz = 255;
            Color farbePinsel = Color.White;// Color.Transparent;
            Color farbeStift = Color.Black;//Transparent;
            SolidBrush pinsel = new SolidBrush(farbePinsel);
            Pen stift = new Pen(farbeStift, 1);
            graphics.FillPath(pinsel, this._graphicsPath);
            graphics.DrawPath(stift, this._graphicsPath);
            graphics.DrawPath(Pens.Black, this._graphicsPathText);
        }
        #endregion
        #region privateMethoden

        #endregion //privateMethoden
    }
}
