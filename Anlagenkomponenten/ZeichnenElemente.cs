using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using MoBaSteuerung.Elemente;
using MoBaSteuerung.Anlagenkomponenten.MCSpeicher;
//using MoBa.Anlagenkomponenten.ZeichnenElemente;
using System.Drawing;
using MoBaSteuerung.ZeichnenElemente;
using MoBa.Elemente;

namespace MoBaSteuerung.Anlagenkomponenten {


   /// <summary>
   /// enthält alle Elemente einer Anlage
   /// </summary>
    public class AnlagenElemente 
    {
        private ElementListe<Regler> reglerElemente;
        private ElementListe<Anschluss> anschlussElemente;
        private ElementListe<Servo> servoElemente;
        private ElementListe<FSS> fssElemente;
        private ElementListe<Gleis> gleisElemente;
        private ElementListe<Weiche> weicheElemente;
        private ElementListe<Signal> signalElemente;
        private ElementListe<Schalter> schalterElemente;
        private ElementListe<Knoten> knotenElemente;
        private ElementListe<Entkuppler> entkupplerElemente;
        private ElementListe<MCSpeicher.MCSpeicher> listeMCSpeicher;
        private ElementListe<InfoFenster> infoElemente;
        private ElementListe<Haltestelle> haltestellenElemente;//für Straßenbahn
        private FahrstrassenNElemente fahrstrassenElemente;
        private ElementListe<Zug> zugElemente;
        private Anlagenzustand anlagenZustand;
        

        private bool _rückmeldungAnzeigen;
        private bool _rückmeldungAktiv;
        private int _entkupplerAbschaltAutoWert;
        private bool _entkupplerAbschaltAutoAktiv;

        private Servo _aktiverServo = null;
        private ServoAction _aktiverServoRichtung = ServoAction.None;
        private int _zoom;

        /// <summary>
        /// enthält alle Elemente der Anlage
        /// </summary>
        public AnlagenElemente() {

            this.reglerElemente   = new ElementListe<Regler>();
            this.anschlussElemente = new ElementListe<Anschluss>();
            this.servoElemente    = new ElementListe<Servo>();
            this.knotenElemente   = new ElementListe<Knoten>();
            this.gleisElemente    = new ElementListe<Gleis>();
            this.weicheElemente   = new ElementListe<Weiche>();
            this.signalElemente   = new ElementListe<Signal>();
            this.schalterElemente = new ElementListe<Schalter>();
            this.fssElemente      = new ElementListe<FSS>();           
            this.entkupplerElemente = new ElementListe<Entkuppler>();
            this.infoElemente       = new ElementListe<InfoFenster>();
            this.haltestellenElemente = new ElementListe<Haltestelle>();

            this.listeMCSpeicher = new ElementListe<MCSpeicher.MCSpeicher>();
            this.fahrstrassenElemente = new FahrstrassenNElemente();
            this.zugElemente = new ElementListe<Zug>();
            this.anlagenZustand = new Anlagenzustand();

            this.RückmeldungAnzeigen = false;
            this.RückmeldungAktiv = false;
            this.EntkupplerAbschaltAutoAktiv = true;
            this.EntkupplerAbschaltAutoWert = 3;
        }

        /// <summary>
        /// 
        /// </summary>
        public Int32 Zoom {
            set {
                this.reglerElemente.Zoom = value;
                this.knotenElemente.Zoom = value;
                this.gleisElemente.Zoom = value;
                this.weicheElemente.Zoom = value;
                this.signalElemente.Zoom = value;
                this.schalterElemente.Zoom = value;
                this.fssElemente.Zoom = value;
                this.infoElemente.Zoom = value;
                this.entkupplerElemente.Zoom = value;
                this.fahrstrassenElemente.Zoom = value;
                this.servoElemente.Zoom = value;
                this._zoom = value;
            }
            get
            {
                return this._zoom;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public AnzeigeTyp AnzeigeTyp {
            set {
                this.gleisElemente.AnzeigeTyp = value;
                this.weicheElemente.AnzeigeTyp = value;
                this.signalElemente.AnzeigeTyp = value;
                this.schalterElemente.AnzeigeTyp = value;
                this.fssElemente.AnzeigeTyp = value;
                this.knotenElemente.AnzeigeTyp = value;
                this.entkupplerElemente.AnzeigeTyp = value;
                this.fahrstrassenElemente.AnzeigeTyp = value;
                this.infoElemente.AnzeigeTyp = value;
                this.servoElemente.AnzeigeTyp = value;
            }
        }

        public ElementListe<Regler> ReglerElemente
        {
            get { return this.reglerElemente; }
        }
        public ElementListe<Anschluss> AnschlussElemente
        {
            get { return this.anschlussElemente; }
        }

        public ElementListe<Servo> ServoElemente
        {
            get { return this.servoElemente; }
        }

        /// <summary>
        /// Liste aller Gleise
        /// </summary>
        public ElementListe<Gleis> GleisElemente {
            get {
                return this.gleisElemente;
            }
        }

        /// <summary>
        /// schaltet alle Gleise ein, für Digitalbetrieb
        /// </summary>
        public void GleiseEinschalten()
        {
            List<Gleis> items;
            items = gleisElemente.Elemente;
            foreach (Gleis item in items)
            {
                item.AusgangSchalten(true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ElementListe<Weiche> WeicheElemente {
            get {
                return this.weicheElemente;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ElementListe<Signal> SignalElemente {
            get {
                return this.signalElemente;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ElementListe<Schalter> SchalterElemente {
            get {
                return this.schalterElemente;
            }
        }

        /// <summary>
        /// liefert die Fahrstromschalter-Liste
        /// </summary>
        public ElementListe<FSS> FssElemente
        {
            get
            {
                return this.fssElemente;
            }
        }

        /// <summary>
        /// liefert die Infofenster-Liste
        /// </summary>
        public ElementListe<InfoFenster> InfoElemente
        {
            get
            {
                return this.infoElemente;
            }
        }

        /// <summary>
        /// liefert die Infofenster-Liste
        /// </summary>
        public ElementListe<Haltestelle> HaltestellenElemente
        {
            get
            {
                return this.haltestellenElemente;
            }
        }

        /// <summary>
        /// liefert die Knoten-Liste
        /// </summary>
        public ElementListe<Knoten> KnotenElemente {
            get {
                return this.knotenElemente;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ElementListe<Entkuppler> EntkupplerElemente {
            get {
                return this.entkupplerElemente;
            }
        }


        public ElementListe<MCSpeicher.MCSpeicher> ListeMCSpeicher {
            get {
                return listeMCSpeicher;
            }
        }

        public ElementListe<Zug> ZugElemente {
            get { return zugElemente; }
        }

        public FahrstrassenNElemente FahrstrassenElemente {
            get {
                return fahrstrassenElemente;
            }
        }

        public Anlagenzustand AnlagenZustand {
            get {
                return anlagenZustand;
            }
            set {
                anlagenZustand = value;
            }
        }

        public bool RückmeldungAnzeigen {
            get {
                return this._rückmeldungAnzeigen;
            }
            set {
                this._rückmeldungAnzeigen = value;
            }
        }

        public bool RückmeldungAktiv {
            get {
                return _rückmeldungAktiv;
            }

            set {
                _rückmeldungAktiv = value;
            }
        }

        /// <summary>
        /// aktiviert die Koppelung nach dem Laden aller Elemente
        /// </summary>
        public void KoppelungenAktivieren()
        {
            weicheElemente.KoppelungenAktivieren();
            fssElemente.KoppelungenAktivieren();
            gleisElemente.KoppelungenAktivieren();
            schalterElemente.KoppelungenAktivieren();
        }

        public Servo AktiverServo {
            get {
                return _aktiverServo;
            }
            set {
                if(_aktiverServo != null && value != null) {
                    _aktiverServo.AusgangToggeln();
                }
                _aktiverServo = value;
            }
        }

        public ServoAction AktiverServoAction {
            get {
                return _aktiverServoRichtung;
            }

            set {
                _aktiverServoRichtung = value;
            }
        }
        public int EntkupplerAbschaltAutoWert {
            get {
                return _entkupplerAbschaltAutoWert;
            }

            set {
                _entkupplerAbschaltAutoWert = value;
            }
        }

        public bool EntkupplerAbschaltAutoAktiv {
            get {
                return _entkupplerAbschaltAutoAktiv;
            }

            set {
                _entkupplerAbschaltAutoAktiv = value;
            }
        }

        public void FSSAktualisieren()
        {
            //Logging.Log.Schreibe("123", LogLevel.Info);
            int undefRegler = 0;
            int undefRegleralt = 0;
            string logString = "";
            //Logging.Log.Schreibe("", LogLevel.Info);
            foreach (FSS x in FssElemente.Elemente)
            {
                logString = logString + x.ID + "\t";
            }
            // Logging.Log.Schreibe(logString, LogLevel.Info);
            //logString.Insert(Environment.NewLine);
            foreach (FSS x in fssElemente.Elemente)
            {//reset aller FSS, Regler 1 und 2 auf null
                undefRegler= undefRegler + x.reset();
                logString = logString + x.ReglerNummer1 + " "+ x.ReglerNummer2 + " " + x.AktiverReglerNr + "\t";
            }
           /* foreach(FSS x in fssElemente.Elemente)
            {

            }*/

            while((undefRegler!=0)&&(undefRegler != undefRegleralt))
            {
                undefRegleralt = undefRegler;
                undefRegler = 0;
                foreach (FSS x in fssElemente.Elemente)
                {
                    undefRegler += x.aktualisieren();
                    //if (x.AktiverReglerNr == 0) undefRegler++;
                }
            }
            Logging.Log.Schreibe(logString, LogLevel.Info);
        }

        /*public List<AnlagenElement> RMSuchen(int ArduinoNr)
        {
            List<AnlagenElement> ergebnis = new List<AnlagenElement>();
            return ergebnis;
        }*/
        
        public List<Gleis> RueckmeldungSuchen(int ArduinoNr)
        {
           // List<Gleis> ergebnis = new List<Gleis>();
            List<Gleis> gListe = gleisElemente.Elemente;
            return gListe.FindAll(x => x.Eingang.ArdNr == ArduinoNr);
            //return ergebnis;
        }
        
        /// <summary>
        /// liefert alle Anlagenelemente auf einer Relais-Platine
        /// </summary>
        /// <param name="ArduinoNr"></param>
        /// <param name="PlatinenNr"></param>
        /// <returns></returns>
        public List<AnlagenElement> RelaisAdresseSuchen(int ArduinoNr, int PlatinenNr)
        {
            List<AnlagenElement> ergebnis = new List<AnlagenElement>();
            ergebnis.AddRange(weicheElemente.AdresseSuchen(ArduinoNr, PlatinenNr));
            ergebnis.AddRange(gleisElemente.AdresseSuchen(ArduinoNr, PlatinenNr));
            ergebnis.AddRange(fssElemente.AdresseSuchen(ArduinoNr, PlatinenNr));
            ergebnis.AddRange(entkupplerElemente.AdresseSuchen(ArduinoNr, PlatinenNr));
            ergebnis.AddRange(signalElemente.AdresseSuchen(ArduinoNr, PlatinenNr));
            return ergebnis;
        }

        /// <summary>
        /// liefert alle Gleise einer RM-Platine
        /// </summary>
        /// <param name="ArduinoNr"></param>
        /// <param name="PlatinenNr"></param>
        /// <returns></returns>
        public List<Gleis>  RMAdresseSuchen(int ArduinoNr, int PlatinenNr)
        {
            return gleisElemente.Elemente.FindAll(x => (x.Eingang.ArdNr == ArduinoNr) && (x.Eingang.AdressenNr == PlatinenNr));
        }

        /// <summary>
        /// liefert alle Anlagenelemente mit dem gleichen Stecker
        /// </summary>
        /// <param name="SteckerBezeichnung"></param>
        /// <returns></returns>
        public List<AnlagenElement> Steckersuchen(string SteckerBezeichnung)
        {
            string stecker = SteckerBezeichnung;
            List<AnlagenElement> ergebnis = new List<AnlagenElement>();
            ergebnis.AddRange(ReglerElemente.SteckerSuchen(stecker));
            ergebnis.AddRange(weicheElemente.SteckerSuchen(stecker));
            ergebnis.AddRange(gleisElemente.SteckerSuchen(stecker));
            ergebnis.AddRange(fssElemente.SteckerSuchen(stecker));
            ergebnis.AddRange(entkupplerElemente.SteckerSuchen(stecker));
            ergebnis.AddRange(signalElemente.SteckerSuchen(stecker));
            ergebnis.AddRange(anschlussElemente.SteckerSuchen(stecker));
            return ergebnis;
        }

        /// <summary>
        /// belegt alle Signale mit den Zügen neu(in Arbeit)
        /// </summary>
        public void ZuegeAktualisieren()
        {
            foreach(Signal s in signalElemente.Elemente)
            {
                s.Zug = 0;
            }
            foreach(Zug z in zugElemente.Elemente)
            {
                if(z.SignalNummer != 0) { signalElemente.Element(z.SignalNummer).Zug = z.ID; }
            }

        }
       
        /// <summary>
        /// verknüpft FSS untereinander
        /// </summary>
        public void FSSLaden()
        {
            foreach(FSS x in fssElemente.Elemente) { x.FSSLaden(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public void AlleLöschen() {
            this.gleisElemente.AlleLöschen();
            this.weicheElemente.AlleLöschen();
            this.signalElemente.AlleLöschen();
            this.schalterElemente.AlleLöschen();
            this.knotenElemente.AlleLöschen();
            this.entkupplerElemente.AlleLöschen();
            this.fahrstrassenElemente.AlleLöschen();
            this.listeMCSpeicher.AlleLöschen();
            this.AnlagenZustand = new Anlagenzustand();
        }
        
        /// <summary>
        /// prüft ob auf einem Rasterpunkt schon ein Knoten existiert
        /// </summary>
        /// <param name="rasterPunkt"></param>
        /// <returns></returns>
        public Knoten SucheKnoten(Point rasterPunkt) {
            foreach (Knoten item in this.KnotenElemente.Elemente) {
                if (item.PositionRaster == rasterPunkt) {
                    return item;
                }
            }
            return null;
        }

    }
}