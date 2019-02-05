using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.MCSpeicher;

namespace MoBaSteuerung.Elemente {


    /// <summary>
    /// Gleis
    /// </summary>
    public class Gleis : LinienAnlagenElement {

        private int reglerNr = 0;
        //private int reglerNrAktuell = 0;
        private Regler regler;
        private FSS fss;
       // private Point[] linienPositionen;
        private Color farbeLinien;
        //private Rectangle rechteckText;
        private StringFormat stringFormat;
        //private Font stringFont;

        private int _direction;
        private int _length;
        private Signal[] _signale = new Signal[2];
        private List<Entkuppler> _entkuppler = new List<Entkuppler> { };
       // private List<InfoFenster>
        private Schalter _schalter = null;
        private Adresse _eingang;
        private GraphicsPath graphicsPath;

        /// <summary>
        /// zum Speichern in der Anlagen-Datei
        /// </summary>
        public override string SpeicherString {
            get {
                return "Gleis"
                    + "\t" + ID
                    + "\t" + StartKn.ID + " " + StartKn.GetGleisAnschlussNr(this)
                    + "\t" + EndKn.ID + " " + EndKn.GetGleisAnschlussNr(this)
                    + "\t" + reglerNr
                    + "\t" + Ausgang.SpeicherString
                    + "\t" + Eingang.SpeicherString
                    + "\t" + Bezeichnung
                    + "\t" + Stecker;
            }
        }

        public override string InfoString {
            get {
                return "Gleis " + this.ID;
            }
        }

        public Signal[] Signale {
            get {
                return _signale;
            }

            set {
                _signale = value;
            }
        }

        public Adresse Eingang {
            get {
                if (_eingang == null) {
                    _eingang = new Adresse(this.Parent, 0, 0, 0);
                }
                return _eingang;
            }

            set {
                _eingang = value;
            }
        }

        public Schalter Schalter {
            get {
                return _schalter;
            }

            set {
                _schalter = value;
            }
        }

        public List<Entkuppler> Entkuppler {
            get {
                return _entkuppler;
            }

            set {
                _entkuppler = value;
            }
        }
        public int ReglerNr {  get { return reglerNr; }  set { reglerNr = value; }  }

        public Gleis(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, Knoten startKnoten, Knoten endKnoten)
        : base(parent, 0, zoom, anzeigeTyp) {
            graphicsPath = new GraphicsPath();
            KurzBezeichnung = "Gl";
            StartKn = startKnoten;
            EndKn = endKnoten;

            this.Berechnung();
        }

        public Gleis(AnlagenElemente parent, Int32 iD, Int32 zoom, AnzeigeTyp anzeigeTyp, Knoten startKnoten, Knoten endKnoten)
            : base(parent, iD, zoom, anzeigeTyp) {
            graphicsPath = new GraphicsPath();
            KurzBezeichnung = "Gl";
            this.stringFormat = new StringFormat();
            this.stringFormat.Alignment = StringAlignment.Far;
            this.stringFormat.LineAlignment = StringAlignment.Far;

            StartKn = startKnoten;
            EndKn = endKnoten;

            //this.farbeFillEllipse = Color.Red;
            this.farbeLinien = Color.Gray;
            this.Berechnung();


            StartKn.AttachTrack(this);
            EndKn.AttachTrack(this);
            Parent.GleisElemente.Hinzufügen(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="zoom"></param>
        /// <param name="anzeigeTyp"></param>
        /// <param name="elem">gesplittete Speicherstring aus der Anlagendatei (anl)</param>
        public Gleis(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, string[] elem)
            : base(parent, Convert.ToInt32(elem[1]), zoom, anzeigeTyp) {
            Ausgang = new Adresse(Parent);
            Eingang = new Adresse(Parent);
            graphicsPath = new GraphicsPath();
            KurzBezeichnung = "Gl";
            string[] start = elem[2].Split(' ');
            string[] end = elem[3].Split(' ');
            Ausgang.SpeicherString = elem[5];
            Eingang.SpeicherString = elem[6];
            Bezeichnung = elem[7];
            if (elem.Length > 8)
                Stecker = elem[8];
            reglerNr = Convert.ToInt32(elem[4]);
            if (reglerNr > 0)
            { regler = parent.ReglerElemente.Element(Convert.ToInt32(elem[4])); }
            StartKn = Parent.KnotenElemente.Element(Convert.ToInt32(start[0]));
            EndKn = Parent.KnotenElemente.Element(Convert.ToInt32(end[0]));

            Berechnung();

            if (StartKn.AttachTrack(this, Convert.ToInt32(start[1])) &&
                EndKn.AttachTrack(this, Convert.ToInt32(end[1]))) {
                Parent.GleisElemente.Hinzufügen(this);
            }
        }
        private void reglerLaden()
        {
            if (fss != null)
            {
                if (fss.AktiverReglerNr != regler.ID) regler = Parent.ReglerElemente.Element(fss.AktiverReglerNr);
            }
        }
        /// <summary>
        /// zeichnet die Regler-Farbe des Gleises
        /// </summary>
        /// <param name="graphics"></param>
        public override void ElementZeichnen1(Graphics graphics)
        {
            if (this.AnzeigenTyp == AnzeigeTyp.Bedienen)
            {
              //int transpanz = 255;
                if (reglerNr < 0) { regler = Parent.FssElemente.Element(-reglerNr).AktiverRegler(); }
                if (reglerNr > 0)
                    if (reglerNr != regler.ID) regler = Parent.ReglerElemente.Element(reglerNr);
                if (regler != null)
                {
                    if (fss != null) regler = fss.AktiverRegler();
                    Color farbeStift = Color.YellowGreen;
                    farbeStift = regler.Farbe;//farbeStift = Color.FromArgb(transpanz, Color.Azure);
                    Pen stiftGleis = new Pen(farbeStift, Convert.ToSingle(this.Zoom * 0.75));
                    stiftGleis.EndCap = LineCap.Round;
                    stiftGleis.StartCap = LineCap.Round;
                    graphics.DrawPath(stiftGleis, this.graphicsPath);
                }
            }
        }
        public void ElementZeichnenR(Graphics graphics)
        {
            if(this.AnzeigenTyp == AnzeigeTyp.Bedienen)
            {
                Color farbeStift = Color.Transparent;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        public override void ElementZeichnen(Graphics graphics) {
            int transpanz = 255;
            Color farbeStift = Color.Transparent;

            switch (this.AnzeigenTyp) {
                case AnzeigeTyp.Bearbeiten:
                    switch (this.ElementZustand) {
                        case Elementzustand.Selektiert:
                            farbeStift = Color.FromArgb(transpanz, Color.Yellow);
                            break;
                        default:
                            farbeStift = Color.FromArgb(transpanz, Color.Gray);
                            break;
                    }

                    break;
                case AnzeigeTyp.Bedienen:
                    if (Passiv) {
                        transpanz = 128;
                    }
                    switch (this.ElementZustand) {
                        case Elementzustand.An:
                            farbeStift = Color.FromArgb(transpanz, Color.Blue);
                            break;
                        case Elementzustand.Aus:
                            if (this.Ausgang.SpeicherString == "0-0-0")
                            { farbeStift = Color.FromArgb(transpanz, Color.LightBlue); }//BlueViolet CadetBlue LightBlue
                            else
                            { farbeStift = Color.FromArgb(transpanz, Color.Gray); }
                            break;
                    }
                    break;
            }



            Pen stiftGleis = new Pen(farbeStift, 1);
            stiftGleis.EndCap = LineCap.Round;
            stiftGleis.StartCap = LineCap.Round;
            stiftGleis.Width = Convert.ToSingle(this.Zoom * 0.5);

            graphics.DrawPath(stiftGleis, this.graphicsPath);

            if (Parent.RückmeldungAnzeigen && Eingang.RueckmeldungAbfragen()) {
                this.farbeLinien = Color.Orange;
                Pen stift = new Pen(this.farbeLinien);
                stift.EndCap = LineCap.Round;
                stift.StartCap = LineCap.Round;
                stift.DashStyle = DashStyle.Dash;
                stift.DashPattern = new float[2] { 2, 2 };
                stift.Width = Convert.ToSingle(this.Zoom * 0.3);
                graphics.DrawPath(stift, this.graphicsPath);
            }
        }


        public override void Berechnung() {
            double d = (Math.Atan2(StartKn.Position.Y - EndKn.Position.Y, EndKn.Position.X - StartKn.Position.X) * 4 / Math.PI);
            if ((d % 1.0) == 0)
                _direction = (int)((d + 8) % 8.0);

            _length = RasterLengthFromStartkn(EndKn.PositionRaster) * Zoom;
            if ((_direction % 2) == 1)
                _length = (int)(_length * Math.Sqrt(2));

            //Mausrechteck = new Rectangle(_startKn.Position.X, _startKn.Position.Y - (int)(0.15 * Zoom), _length, (int)(0.3 * Zoom));

            this.graphicsPath.Reset();
            this.graphicsPath.AddLine(this.StartKn.PositionRaster.X * this.Zoom, this.StartKn.PositionRaster.Y * this.Zoom,
                                      this.EndKn.PositionRaster.X * this.Zoom, this.EndKn.PositionRaster.Y * this.Zoom);       
        }

        public int GetDirection(Knoten middlePoint) {
            if (middlePoint == StartKn)
                return _direction;
            if (middlePoint == EndKn)
                return (_direction + 4) % 8;
            return -1;
        }
        public int GetDirection(bool inZeichenRichtung) {
            if (inZeichenRichtung)
                return _direction;
            else
                return (_direction + 4) % 8;
        }

        private int RasterLengthFromStartkn(Point rasterPunkt) {
            if ((_direction % 4) == 2)
                return Math.Abs(rasterPunkt.Y - StartKn.PositionRaster.Y);
            else
                return Math.Abs(rasterPunkt.X - StartKn.PositionRaster.X);
        }

        private bool PunktAufGleis(Point punkt) {
            return this.graphicsPath.IsOutlineVisible(punkt, new Pen(Color.Black, this.Zoom * 0.3f));
        }

        private bool RasterPositionFrei(RasterAnlagenElement element) {
            foreach (RasterAnlagenElement el in _signale) {
                if (el != null)
                    if (el.PositionRaster == element.PositionRaster)
                        return false;
            }
            foreach (RasterAnlagenElement el in Entkuppler) {
                if (el != null)
                    if (el.PositionRaster == element.PositionRaster)
                        return false;
            }
            if (Schalter != null)
                if (Schalter.PositionRaster == element.PositionRaster)
                    return false;

            return true;
        }

        public Point GetRasterPosition(AnlagenElement element, int glPosition) {
            double abst = glPosition;
            if ((_direction % 2) == 1)
                abst *= Math.Sqrt(2);
            double winkelRad = _direction * Math.PI / 4;
            return new Point(StartKn.PositionRaster.X + (int)Math.Round(abst * Math.Cos(winkelRad))
                            , StartKn.PositionRaster.Y - (int)Math.Round(abst * Math.Sin(winkelRad))
                            );
        }

        public bool GleisElementAnschluss(FSS fss) {
            if (PunktAufGleis(fss.Position)) {
                if (RasterPositionFrei(fss)) {
                    int glPos = RasterLengthFromStartkn(fss.PositionRaster);
                    if ((glPos > 0) && (glPos < _length)) {
                        this.fss = fss;

                        fss.Gleisposition = glPos;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool GleisElementAnschluss(Signal signal) {
            if (PunktAufGleis(signal.Position)) {
                if (RasterPositionFrei(signal)) {
                    int glPos = RasterLengthFromStartkn(signal.PositionRaster);
                    if ((glPos > 0) && (glPos < _length)) {
                        if (signal.InZeichenRichtung)
                            _signale[0] = signal;
                        else
                            _signale[1] = signal;

                        signal.Gleisposition = glPos;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool GleisElementAnschluss(Entkuppler entkuppler) {
            if (PunktAufGleis(entkuppler.Position)) {
                if (RasterPositionFrei(entkuppler)) {
                    int glPos = RasterLengthFromStartkn(entkuppler.PositionRaster);
                    if ((glPos > 0) && (glPos < _length)) {
                        Entkuppler.Add(entkuppler);

                        entkuppler.Gleisposition = glPos;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schalter"></param>
        /// <returns></returns>
        public bool GleisElementAnschluss(Schalter schalter) {
            if (PunktAufGleis(schalter.Position)) {
                if (RasterPositionFrei(schalter)) {
                    int glPos = RasterLengthFromStartkn(schalter.PositionRaster);
                    if (((glPos * this.Zoom) > 0) && ((glPos * this.Zoom) < _length)) {
                        Schalter = schalter;

                        schalter.Gleisposition = glPos;
                        return true;
                    }
                }
            }
            return false;
        }


        /// <summary>
        /// Durch diese Methode wird der Schalter, welcher diesem Gleis zugeordnet ist ausgetragen.
        /// </summary>
        /// <param name="schalter">zu löschender Schalter</param>
        /// <returns>Gibt TRUE zurück, wenn der übergeben Schalter gleich dem Schalter ist des Gleises ist und dieser ausgetragen wurde.</returns>
        public bool GleisElementAustragen(Schalter schalter) {
            if (this.Schalter == schalter) {
                this.Schalter = null;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Durch diese Methode wird ein Entkuppler, welcher diesem Gleis zugeordnet ist entfernt.
        /// </summary>
        /// <param name="entkuppler">zu entfernender Entkuppler</param>
        /// <returns>Gibt TRUE zurück, wenn der übergeben Entkuppler erfolgreich entfernt wurde, andernfalls FALSE. Wenn der Entkuppler nicht gefunden wurde ebenfalls FALSE</returns>
        public bool GleisElementAustragen(Entkuppler entkuppler) {
            return this.Entkuppler.Remove(entkuppler);
        }

        /// <summary>
        /// Durch diese Methode wird ein Signal, welches diesem Gleis zugeordnet ist entfernt.
        /// </summary>
        /// <param name="signal">zu entfernendes Signal</param>
        /// <returns>Gibt TRUE zurück, wenn das übergebene Signal erfolgreich entfernt wurde, andernfalls FALSE. Wenn das Signal nicht gefunden wurde ebenfalls FALSE.</returns>
        public bool GleisElementAustragen(Signal signal) {
            for (int i = 0; i < _length; i++) {
                if (this._signale[i] == signal) {
                    this._signale[i] = null;
                    return true;
                }
            }
            return false;
        }

        public override bool MouseClick(Point p) {
            return this.PunktAufGleis(p);
        }
        
    }
}