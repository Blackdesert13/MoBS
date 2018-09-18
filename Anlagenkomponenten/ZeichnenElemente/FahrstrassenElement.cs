using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Timers;
using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using MoBaSteuerung.Anlagenkomponenten.MCSpeicher;
using System.Text.RegularExpressions;
using System.Linq;

namespace MoBaSteuerung.Elemente {

    #region Fahrstraßenliste  alt

    /// <summary>
    /// 
    /// </summary>
    public class FahrstrassenElemente {
        private List<Fahrstrasse> _aktiveFahrstrassen;
        private List<Fahrstrasse> _auswahlFahrstrassen;
        private List<Fahrstrasse> _gespeicherteFahrstrassen;

        /// <summary>
        /// 
        /// </summary>
        public List<Fahrstrasse> AktiveFahrstrassen {
            get {
                return this._aktiveFahrstrassen;
            }
        }

        public List<Fahrstrasse> AuswahlFahrstrassen {
            get {
                return _auswahlFahrstrassen;
            }

            set {
                _auswahlFahrstrassen = value;
            }
        }

        public List<Fahrstrasse> GespeicherteFahrstrassen {
            get {
                return _gespeicherteFahrstrassen;
            }

            set {
                _gespeicherteFahrstrassen = value;
            }
        }

        public string SpeicherString {
            get {
                string spString = "Fahrstraßen"
                                + "\t" + "Nr."
                                + "\t" + "Start"
                                + "\t" + "Ziel";
                foreach (Fahrstrasse x in this.GespeicherteFahrstrassen) {
                    spString += Environment.NewLine + x.SpeicherString;
                }
                return spString;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Int32 Zoom {
            set {
                foreach (Fahrstrasse item in this._gespeicherteFahrstrassen) {
                    item.Zoom = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public AnzeigeTyp AnzeigeTyp {
            set {
                foreach (Fahrstrasse item in this._aktiveFahrstrassen) {
                    item.AnzeigenTyp = value;
                }
                foreach (Fahrstrasse item in this._auswahlFahrstrassen) {
                    item.AnzeigenTyp = value;
                }
            }
        }



        /// <summary>
        /// 
        /// </summary>
        public FahrstrassenElemente() {
            this._aktiveFahrstrassen = new List<Fahrstrasse>();
            this._auswahlFahrstrassen = new List<Fahrstrasse>();
            this._gespeicherteFahrstrassen = new List<Fahrstrasse>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fahrstrasse"></param>
        public void HinzufügenAktiv(Fahrstrasse fahrstrasse) {
            this._aktiveFahrstrassen.Add(fahrstrasse);
            fahrstrasse.IsAktiv = true;
        }

        public void HinzufügenAktiv(List<Fahrstrasse> fahrstrassen) {
            foreach (Fahrstrasse fs in fahrstrassen) {
                HinzufügenAktiv(fs);
            }
        }

        public void HinzufügenAuswahl(List<AnlagenElement> fahrstrassen) {
            foreach (Fahrstrasse fs in fahrstrassen) {
                _auswahlFahrstrassen.Add(fs);
                fs.Selektiert = true;
            }
        }

        public void HinzufügenSpeicher(Fahrstrasse fahrstrasse) {
            this._gespeicherteFahrstrassen.Add(fahrstrasse);
        }


        public void LöschenAktiv(Fahrstrasse aktiveFahrstrasse) {
            this._aktiveFahrstrassen.Remove(aktiveFahrstrasse);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="iD"></param>
        /// <returns></returns>
        public Fahrstrasse Fahrstrasse(Int32 iD) {
            foreach (Fahrstrasse item in this._gespeicherteFahrstrassen) {
                if (item.ID == iD) {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public void AlleLöschen() {
            this._aktiveFahrstrassen = new List<Fahrstrasse>();
            this._auswahlFahrstrassen = new List<Fahrstrasse>();
            this._gespeicherteFahrstrassen = new List<Fahrstrasse>();
        }

        public void AlleLöschenAuswahl() {
            this._auswahlFahrstrassen = new List<Fahrstrasse>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public void ElementeZeichnen(Graphics e) {
            foreach (Fahrstrasse item in this._auswahlFahrstrassen) {
                item.ElementZeichnen(e);
            }
            foreach (Fahrstrasse item in this._aktiveFahrstrassen) {
                item.ElementZeichnen(e);
            }
        }


        public int SucheFahrstrassen(Signal startsignal) {
            List<AnlagenElement> el = new List<AnlagenElement> { startsignal, startsignal.AnschlussGleis };
            if (startsignal.InZeichenRichtung)
                el.Add(startsignal.AnschlussGleis.EndKn);
            else
                el.Add(startsignal.AnschlussGleis.StartKn);
            SucheZielSignale(el, startsignal.InZeichenRichtung);
            return 0;
        }

        private bool KnotenDoppeltInFahrstrasse(List<AnlagenElement> pfad) {
            int anz = pfad.Count;
            for (int j = anz - 1; j >= 5; j = j - 2) {
                int knNr = ((Knoten)pfad[j - 2]).ID;
                for (int i = j - 2; i >= 3; i = i - 2) {
                    if (((Knoten)pfad[j]).ID == ((Knoten)pfad[i]).ID)
                        return true;
                }
            }
            return false;
        }

        private void SucheZielSignale(List<AnlagenElement> pfad, bool dirStart) {
            if (!KnotenDoppeltInFahrstrasse(pfad)) {
                int anz = pfad.Count;
                Knoten lastKn = (Knoten)pfad[anz - 1];
                Gleis lastGl = (Gleis)pfad[anz - 2];
                int anschl = lastKn.GetGleisAnschlussNr(lastGl);
                int offs = 0;
                if (anschl >= 0) {
                    if (anschl < 2)
                        offs = 2;
                    Gleis nextGl;
                    bool vergl;
                    for (int i = 0; i < 2; i++) {
                        if (lastKn.Gleise[offs + i] != null) {
                            nextGl = lastKn.Gleise[offs + i];
                            if ((dirStart && nextGl.StartKn == lastKn) || (!dirStart && nextGl.EndKn == lastKn))
                                vergl = false;
                            else
                                vergl = true;
                            foreach (Signal sn in nextGl.Signale)
                                if (sn != null)
                                    if ((sn.InZeichenRichtung ^ vergl) == dirStart) {
                                        pfad.Add(nextGl);
                                        pfad.Add(sn);
                                        _gespeicherteFahrstrassen.Add(new Fahrstrasse(pfad.ToArray()));
                                        goto escape;
                                    }
                            pfad.Add(nextGl);
                            if (vergl)
                                if (!dirStart)
                                    pfad.Add(nextGl.EndKn);
                                else
                                    pfad.Add(nextGl.StartKn);
                            else
                                if (dirStart)
                                pfad.Add(nextGl.EndKn);
                            else
                                pfad.Add(nextGl.StartKn);
                            SucheZielSignale(pfad, dirStart);

                            escape:
                            pfad.RemoveRange(pfad.Count - 2, 2);
                        }
                    }
                }

                return;
            }
        }

        public void AktiveFahrstrassenAktualisieren(Anlagenzustand anlZust) {
            foreach (Fahrstrasse fs in _aktiveFahrstrassen) {
                fs.IsAktiv = false;
            }
            _aktiveFahrstrassen.Clear();
            foreach (WeichenStrasse weSt in anlZust.AktiveFahrstrassen) {
                Fahrstrasse fs = Fahrstrasse(weSt.Id);
                if (fs != null) {
                    _aktiveFahrstrassen.Add(fs);
                    fs.IsAktiv = true;
                    fs.AdressenSperren();
                }
            }
        }


    }

    #endregion
    #region Fahrstraße alt

    /// <summary>
    /// 
    /// </summary>
    public class Fahrstrasse : AnlagenElement {
        private GraphicsPath _fahrstrLinie = new GraphicsPath();
        private AnlagenElement[] _listeElemente;
        private Signal _startSignal;
        private Signal _endSignal;
        private bool _isAktiv = false;

        private List<Adresse> _startBefehle;
        private List<Adresse> _endBefehle;

        public override string SpeicherString {
            get {
                string txt = "FahrstrasseV"
                            + "\t" + ID
                            + "\t" + StartSignal.ID
                            + "\t" + EndSignal.ID
                            + "\tFalse"
                            + "\tFalse";
                txt += Environment.NewLine + " BefehlStart";
                foreach (var befehl in _startBefehle) {
                    txt += "\t" + befehl.Stellung + "_" + befehl.SpeicherString;
                }
                txt += Environment.NewLine + " BefehlZiel";
                foreach (var befehl in _endBefehle) {
                    txt += "\t" + befehl.Stellung + "_" + befehl.SpeicherString;
                }
                txt += Environment.NewLine + " KnotenListe";
                foreach (var item in _listeElemente) {
                    if (item.GetType().Name == "Knoten")
                        txt += "\t" + item.ID;
                }
                return txt;
            }
        }

        public bool IsAktiv {
            get {
                return _isAktiv;
            }
            set {
                _isAktiv = value;
            }
        }



        public Signal EndSignal {
            get {
                return _endSignal;
            }
        }

        public Signal StartSignal {
            get {
                return _startSignal;
            }
        }

        public Adresse adrStartGleis {
            get {
                if (_startBefehle.Count < 2)
                    return null;
                return _startBefehle[1];
            }
        }



        //public Fahrstrasse(ZeichnenElemente parent, Int32 iD, Int32 zoom, AnzeigeTyp anzeigeTyp, Signal startSignal)
        // : base(parent, iD, zoom, anzeigeTyp) {

        //}


        public Fahrstrasse(AnlagenElement[] elementListe)
        : base(elementListe[0].Parent, elementListe[0].Parent.FahrstarssenElemente.GespeicherteFahrstrassen.Count + 1, elementListe[0].Zoom, elementListe[0].AnzeigenTyp) {
            _listeElemente = elementListe;
            _startSignal = (Signal)elementListe[0];
            _endSignal = (Signal)elementListe[elementListe.Length - 1];
            SucheAdressen();
            Berechnung();
        }

        public Fahrstrasse(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, string[] elem, string befehleStart, string befehleZiel, string knotenListe)
            : base(parent, Convert.ToInt32(elem[1]), zoom, anzeigeTyp) {
            int startS = Convert.ToInt16(elem[2]);
            int zielS = Convert.ToInt16(elem[3]);
            if (startS > 0) {
                _startSignal = parent.SignalElemente.Element(startS);
            }
            else
                return;
            if (zielS > 0) {
                _endSignal = parent.SignalElemente.Element(zielS);
            }
            else
                return;

            string[] spString;
            spString = befehleStart.Trim().Split('\t');
            if (spString[0] == "BefehlStart") {
                _startBefehle = new List<Adresse>();
                for (int i = 1; i < spString.Length; i++) {
                    Adresse neuerBefehl = new Adresse(parent);
                    // neuerBefehl.SpeicherString = spString[i];
                    string[] befehlString = spString[i].Split('_');
                    neuerBefehl.Stellung = Convert.ToBoolean(befehlString[0]);
                    neuerBefehl.SpeicherString = befehlString[1];
                    //neuerBefehl.MC = ANLAGE.MCListe.MC(neuerBefehl.MCNr);
                    _startBefehle.Add(neuerBefehl);
                }
            }
            else
                return;

            spString = befehleZiel.Trim().Split('\t');
            if (spString[0] == "BefehlZiel") {
                _endBefehle = new List<Adresse>();
                for (int i = 1; i < spString.Length; i++) {
                    Adresse neuerBefehl = new Adresse(parent);
                    // neuerBefehl.SpeicherString = spString[i];
                    string[] befehlString = spString[i].Split('_');
                    neuerBefehl.Stellung = Convert.ToBoolean(befehlString[0]);
                    neuerBefehl.SpeicherString = befehlString[1];
                    //neuerBefehl.MC = ANLAGE.MCListe.MC(neuerBefehl.MCNr);
                    _endBefehle.Add(neuerBefehl);
                }
            }
            else
                return;

            spString = knotenListe.Split('\t');
            if (spString[0] == " KnotenListe") {
                List<AnlagenElement> knoten = new List<AnlagenElement>();
                for (int i = 1; i < spString.Length; i++) {
                    knoten.Add(parent.KnotenElemente.Element(Convert.ToInt16(spString[i])));
                }
                _listeElemente = knoten.ToArray();
            }
            else
                return;

            //parent.FahrstarssenElemente.GespeicherteFahrstrassen.Add(this);
            Berechnung();
        }

        private void SucheAdressen() {
            _endBefehle = new List<Adresse>();
            _startBefehle = new List<Adresse>();
            Adresse snAdr;
            Adresse adr;
            bool stellungZielElemente = true;
            for (int i = 0; i < _listeElemente.Length; i++) {
                stellungZielElemente = (i < _listeElemente.Length - 2);
                switch (_listeElemente[i].GetType().Name) {
                    case "Signal":
                        snAdr = ((Signal)_listeElemente[i]).Ausgang;
                        adr = new Adresse(Parent, snAdr.MCNr, snAdr.AdressenNr, snAdr.BitNr);
                        adr.Stellung = stellungZielElemente;
                        _startBefehle.Add(adr);
                        adr = new Adresse(Parent, snAdr.MCNr, snAdr.AdressenNr, snAdr.BitNr);
                        adr.Stellung = false;
                        _endBefehle.Add(adr);
                        break;
                    case "Knoten":
                        Knoten kn = (Knoten)_listeElemente[i];
                        Gleis vergl = (Gleis)_listeElemente[i - 1];
                        Weiche we = null;
                        for (int j = 0; j < 4; j++) {
                            if (vergl == (Gleis)kn.Gleise[j]) {
                                if (j < 2)
                                    we = kn.Weichen[0];
                                else
                                    we = kn.Weichen[1];

                                if (we != null) {
                                    snAdr = we.Ausgang;
                                    adr = new Adresse(Parent, snAdr.MCNr, snAdr.AdressenNr, snAdr.BitNr);
                                    adr.Stellung = ((j % 2) == 0) ^ we.Grundstellung;
                                    _startBefehle.Add(adr);
                                }
                                break;
                            }
                        }
                        vergl = (Gleis)_listeElemente[i + 1];
                        for (int j = 0; j < 4; j++) {
                            if (vergl == (Gleis)kn.Gleise[j]) {
                                if (j < 2)
                                    we = kn.Weichen[0];
                                else
                                    we = kn.Weichen[1];

                                if (we != null) {
                                    snAdr = we.Ausgang;
                                    adr = new Adresse(Parent, snAdr.MCNr, snAdr.AdressenNr, snAdr.BitNr);
                                    adr.Stellung = ((j % 2) == 0) ^ we.Grundstellung;
                                    _startBefehle.Add(adr);
                                }
                                break;
                            }
                        }
                        break;
                    case "Gleis":
                        snAdr = ((Gleis)_listeElemente[i]).Ausgang;
                        if (snAdr.ArdNr > 0) {
                            adr = new Adresse(Parent, snAdr.ArdNr, snAdr.AdressenNr, snAdr.BitNr);
                            adr.Stellung = stellungZielElemente;
                            _startBefehle.Add(adr);
                            adr = new Adresse(Parent, snAdr.ArdNr, snAdr.AdressenNr, snAdr.BitNr);
                            adr.Stellung = false;
                            _endBefehle.Add(adr);
                        }
                        break;
                }
            }
            AdressListebereinigen(_endBefehle);
            AdressListebereinigen(_startBefehle);
        }

        private void AdressListebereinigen(List<Adresse> adrListe) {
            int i = adrListe.Count - 1;
            while (i > 0) {
                int anz = 1;
                for (int j = i - 1; j >= 0; j--) {
                    if (adrListe[j].GleicheAdresse(adrListe[i - anz + 1])) {
                        adrListe.RemoveAt(j);
                        anz++;
                    }
                }
                i = i - anz;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        public override void ElementZeichnen(Graphics graphics) {
            Pen stift = new Pen(Color.Yellow,(Single)(this.Zoom * 0.25));
            switch (this.ElementZustand) {
                case Elementzustand.An:
                    stift.Color = Color.Red;
                    break;
                case Elementzustand.Aus:
                    return;
                case Elementzustand.Selektiert:
                    stift.Color = Color.Yellow;
                    break;
            }
                
            stift.EndCap = LineCap.Round;
            stift.LineJoin = LineJoin.Round;
            stift.StartCap = LineCap.Round;

            graphics.DrawPath(stift, _fahrstrLinie);
        }


        public override void Berechnung() {
            _fahrstrLinie.Reset();
            List<Point> punkte = new List<Point> { };
            punkte.Add(_startSignal.PositionRaster);
            foreach (AnlagenElement el in _listeElemente)
                if (el.GetType().Name != "Gleis")
                    punkte.Add(((RasterAnlagenElement)el).PositionRaster);
            punkte.Add(_endSignal.PositionRaster);

            _fahrstrLinie.AddLines(punkte.ToArray());
            Matrix matrix = new Matrix();
            matrix.Scale(Zoom, Zoom);
            _fahrstrLinie.Transform(matrix);
        }

        public void AdressenSperren() {
            for (int i = 0; i < _startBefehle.Count; i++) {
                if (i != 1) {
                    _startBefehle[i].IsLocked = true;
                }
            }
        }

        public bool AdressenFrei() {
            foreach (Adresse adr in _startBefehle) {
                if (adr.IsLocked) {
                    if (adr.Stellung != adr.AdresseAbfragen())
                        return false;
                }
            }
            return true;
        }

        public override bool AusgangToggeln() {
            if (AdressenFrei() || IsAktiv) {
                if (Parent.AnlagenZustand.FahrstrasseSchalten(this.ID)) {
                    for (int i = 0; i < _startBefehle.Count; i++) {
                        if (i != 1) {
                            this._startBefehle[i].AusgangSchalten();
                            this._startBefehle[i].IsLocked = true;
                        }
                    }
                }
                else {
                    foreach (Adresse adr in _startBefehle) {
                        adr.IsLocked = false;
                    }
                    foreach (Adresse adr in _endBefehle) {
                        adr.AusgangSchalten();
                        adr.IsLocked = false;
                    }
                }
                this.Selektiert = false;
                this.Ausgang.AusgangToggeln();
                Parent.FahrstarssenElemente.AktiveFahrstrassenAktualisieren(Parent.AnlagenZustand);
                return true;
            }
            return false;
        }

    }

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public class FahrstrassenNElemente {
        private List<FahrstrasseN> _aktiveFahrstrassen;
        private List<FahrstrasseN> _auswahlFahrstrassen;
        private List<FahrstrasseN> _gespeicherteFahrstrassen;

        /// <summary>
        /// 
        /// </summary>
        public List<FahrstrasseN> AktiveFahrstrassen {
            get {
                return this._aktiveFahrstrassen;
            }
        }

        public List<FahrstrasseN> AuswahlFahrstrassen {
            get {
                return _auswahlFahrstrassen;
            }

            set {
                _auswahlFahrstrassen = value;
            }
        }

        public List<FahrstrasseN> GespeicherteFahrstrassen {
            get {
                return _gespeicherteFahrstrassen;
            }

            set {
                _gespeicherteFahrstrassen = value;
            }
        }

        /// <summary>
        /// zum Speichern in der Anlagen-Datei
        /// </summary>
        public string SpeicherString {
            get {
                string spString = "FahrstraßenN"
                                + "\t" + "Nr."
                                + "\t" + "Start"
                                + "\t" + "Ziel";
                foreach (FahrstrasseN x in this.GespeicherteFahrstrassen) {
                    spString += Environment.NewLine + x.SpeicherString;
                }
                return spString;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Int32 Zoom {
            set {
                foreach (FahrstrasseN item in this._gespeicherteFahrstrassen) {
                    item.Zoom = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public AnzeigeTyp AnzeigeTyp {
            set {
                foreach (FahrstrasseN item in this._aktiveFahrstrassen) {
                    item.AnzeigenTyp = value;
                }
                foreach (FahrstrasseN item in this._auswahlFahrstrassen) {
                    item.AnzeigenTyp = value;
                }
            }
        }



        /// <summary>
        /// 
        /// </summary>
        public FahrstrassenNElemente() {
            this._aktiveFahrstrassen = new List<FahrstrasseN>();
            this._auswahlFahrstrassen = new List<FahrstrasseN>();
            this._gespeicherteFahrstrassen = new List<FahrstrasseN>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fahrstrasse"></param>
        public void HinzufügenAktiv(FahrstrasseN fahrstrasse) {
            this._aktiveFahrstrassen.Add(fahrstrasse);
            fahrstrasse.IsAktiv = true;
        }

        public void HinzufügenAktiv(List<FahrstrasseN> fahrstrassen) {
            foreach (FahrstrasseN fs in fahrstrassen) {
                HinzufügenAktiv(fs);
            }
        }

        public void HinzufügenAuswahl(List<AnlagenElement> fahrstrassen) {
            foreach (FahrstrasseN fs in fahrstrassen) {
                _auswahlFahrstrassen.Add(fs);
                fs.Selektiert = true;
            }
        }

        public void HinzufügenSpeicher(FahrstrasseN fahrstrasse) {
            this._gespeicherteFahrstrassen.Add(fahrstrasse);
        }

        public void LöschenAktiv(FahrstrasseN aktiveFahrstrasse) {
            this._aktiveFahrstrassen.Remove(aktiveFahrstrasse);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="iD"></param>
        /// <returns></returns>
        public FahrstrasseN Fahrstrasse(Int32 iD) {
            foreach (FahrstrasseN item in this._gespeicherteFahrstrassen) {
                if (item.ID == iD) {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public void AlleLöschen() {
            this._aktiveFahrstrassen = new List<FahrstrasseN>();
            this._auswahlFahrstrassen = new List<FahrstrasseN>();
            this._gespeicherteFahrstrassen = new List<FahrstrasseN>();
        }

        public void AlleLöschenAuswahl() {
            this._auswahlFahrstrassen = new List<FahrstrasseN>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public void ElementeZeichnen(Graphics e) {
            foreach (FahrstrasseN item in this._auswahlFahrstrassen) {
                item.ElementZeichnen(e);
            }
            foreach (FahrstrasseN item in this._aktiveFahrstrassen) {
                item.ElementZeichnen(e);
            }
        }


        public int SucheFahrstrassen(Signal startsignal) {
            List<AnlagenElement> el = new List<AnlagenElement> { startsignal, startsignal.AnschlussGleis };
            if (startsignal.InZeichenRichtung)
                el.Add(startsignal.AnschlussGleis.EndKn);
            else
                el.Add(startsignal.AnschlussGleis.StartKn);
            SucheZielSignale(el, startsignal.InZeichenRichtung);
            return 0;
        }

        private bool KnotenDoppeltInFahrstrasse(List<AnlagenElement> pfad) {
            int anz = pfad.Count;
            for (int j = anz - 1; j >= 5; j = j - 2) {
                int knNr = ((Knoten)pfad[j - 2]).ID;
                for (int i = j - 2; i >= 3; i = i - 2) {
                    if (((Knoten)pfad[j]).ID == ((Knoten)pfad[i]).ID)
                        return true;
                }
            }
            return false;
        }

        private void SucheZielSignale(List<AnlagenElement> pfad, bool dirStart) {
            if (!KnotenDoppeltInFahrstrasse(pfad)) {
                int anz = pfad.Count;
                Knoten lastKn = (Knoten)pfad[anz - 1];
                Gleis lastGl = (Gleis)pfad[anz - 2];
                int anschl = lastKn.GetGleisAnschlussNr(lastGl);
                int offs = 0;
                if (anschl >= 0) {
                    if (anschl < 2)
                        offs = 2;
                    Gleis nextGl;
                    bool vergl;
                    for (int i = 0; i < 2; i++) {
                        if (lastKn.Gleise[offs + i] != null) {
                            nextGl = lastKn.Gleise[offs + i];
                            if ((dirStart && nextGl.StartKn == lastKn) || (!dirStart && nextGl.EndKn == lastKn))
                                vergl = false;
                            else
                                vergl = true;
                            foreach (Signal sn in nextGl.Signale)
                                if (sn != null)
                                    if ((sn.InZeichenRichtung ^ vergl) == dirStart) {
                                        pfad.Add(nextGl);
                                        pfad.Add(sn);
                                        _gespeicherteFahrstrassen.Add(new FahrstrasseN(pfad.ToArray()));
                                        goto escape;
                                    }
                            pfad.Add(nextGl);
                            if (vergl)
                                if (!dirStart)
                                    pfad.Add(nextGl.EndKn);
                                else
                                    pfad.Add(nextGl.StartKn);
                            else
                                if (dirStart)
                                pfad.Add(nextGl.EndKn);
                            else
                                pfad.Add(nextGl.StartKn);
                            SucheZielSignale(pfad, dirStart);

                            escape:
                            pfad.RemoveRange(pfad.Count - 2, 2);
                        }
                    }
                }

                return;
            }
        }

        public void AktiveFahrstrassenAktualisieren(Anlagenzustand anlZust) {
            foreach (FahrstrasseN fs in _aktiveFahrstrassen) {
                fs.IsAktiv = false;
            }
            _aktiveFahrstrassen.Clear();
            foreach (WeichenStrasse weSt in anlZust.AktiveFahrstrassen) {
                FahrstrasseN fs = Fahrstrasse(weSt.Id);
                if (fs != null) {
                    _aktiveFahrstrassen.Add(fs);
                    fs.IsAktiv = true;
                    fs.AdressenSperren();
                }
            }
        }


    }

    public class FahrstrasseN : AnlagenElement {
        private GraphicsPath _fahrstrLinie = new GraphicsPath();
        private AnlagenElement[] _listeElemente;
        private Signal _startSignal;
        private Signal _endSignal;

        private bool _isAktiv = false;

        private List<Befehl> _startBefehle;
        private List<Befehl> _endBefehle;

        public override string SpeicherString {
            get {
                string txt = "FahrstrasseN"
                            + "\t" + ID
                            + "\t" + StartSignal.ID
                            + "\t" + EndSignal.ID
                            + "\tFalse"
                            + "\tFalse";
                txt += Environment.NewLine + " BefehlStart" + this.AuslesenBefehlsliste(_startBefehle);

                txt += Environment.NewLine + " BefehlZiel" + this.AuslesenBefehlsliste(_endBefehle);

                txt += Environment.NewLine + " KnotenListe";
                foreach (var item in _listeElemente) {
                    if (item.GetType().Name == "Knoten")
                        txt += "\t" + item.ID;
                }
                return txt;
            }
        }

        public bool IsAktiv {
            get {
                return _isAktiv;
            }
            set {
                _isAktiv = value;
            }
        }

        public Signal EndSignal {
            get {
                return _endSignal;
            }
        }

        public Signal StartSignal {
            get {
                return _startSignal;
            }
        }

        public Adresse adrStartGleis {
            get {
                if (_startBefehle.Count < 2)
                    return null;
                Befehl bef = _startBefehle[1];
                Adresse adr=new Adresse(this.Parent,bef.Element.Ausgang.ArdNr, bef.Element.Ausgang.AdressenNr, bef.Element.Ausgang.BitNr);
                adr.Stellung = true;
                return adr;
            }
        }



        //public Fahrstrasse(ZeichnenElemente parent, Int32 iD, Int32 zoom, AnzeigeTyp anzeigeTyp, Signal startSignal)
        // : base(parent, iD, zoom, anzeigeTyp) {

        //}


        public FahrstrasseN(AnlagenElement[] elementListe)
        : base(elementListe[0].Parent, elementListe[0].Parent.FahrstarssenElemente.GespeicherteFahrstrassen.Count + 1, elementListe[0].Zoom, elementListe[0].AnzeigenTyp) {
            _listeElemente = elementListe;
            _startSignal = (Signal)elementListe[0];
            _endSignal = (Signal)elementListe[elementListe.Length - 1];
            SucheAdressen();
            Berechnung();
        }

        public FahrstrasseN(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, string[] elem, string befehleStart, string befehleZiel, string knotenListe)
            : base(parent, Convert.ToInt32(elem[1]), zoom, anzeigeTyp) {
            int startS = Convert.ToInt16(elem[2]);
            int zielS = Convert.ToInt16(elem[3]);
            if (startS > 0) {
                _startSignal = parent.SignalElemente.Element(startS);
            }
            else
                return;
            if (zielS > 0) {
                _endSignal = parent.SignalElemente.Element(zielS);
            }
            else
                return;

            string[] spString;
            spString = befehleStart.Trim().Split('\t');
            if (spString[0] == "BefehlStart") {
                _startBefehle = new List<Befehl>();
                if(!this.EinlesenBefehlsliste(_startBefehle, spString))
                    return;
            }
            else
                return;

            spString = befehleZiel.Trim().Split('\t');
            if (spString[0] == "BefehlZiel") {
                _endBefehle = new List<Befehl>();
                if(!this.EinlesenBefehlsliste(_endBefehle, spString))
                    return;
            }
            else
                return;

            spString = knotenListe.Split('\t');
            if (spString[0] == " KnotenListe") {
                List<AnlagenElement> knoten = new List<AnlagenElement>();
                for (int i = 1; i < spString.Length; i++) {
                    knoten.Add(parent.KnotenElemente.Element(Convert.ToInt16(spString[i])));
                }
                _listeElemente = knoten.ToArray();
            }
            else
                return;

            parent.FahrstarssenElemente.GespeicherteFahrstrassen.Add(this);
            Berechnung();
        }

        private string AuslesenBefehlsliste(List<Befehl> list) {
            string spString = String.Empty;
            foreach (Befehl befehl in list) {
				spString += "\t";
                AnlagenElement el = befehl.Element;
				if(el is Gleis) {
					spString += "Gl" + el.ID + ":" + (befehl.SchaltZustand ? "An" : "Aus");
				}
				else if (el is Signal) {
					spString += "Sn" + el.ID + ":" + (befehl.SchaltZustand ? "An" : "Aus");
				}
				else if (el is Weiche) {
					spString += "We" + el.ID + ":" + (befehl.SchaltZustand ? "An" : "Aus");
				}
                else if (el is FSS)
                {
                    spString += "Fss" + el.ID + ":" + (befehl.SchaltZustand ? "An" : "Aus");
                }
            }
            return spString;
        }

        private bool EinlesenBefehlsliste(List<Befehl>list, string[] spString) {
            for (int i = 1; i < spString.Length; i++) {
                string[] befehl = spString[i].Split(':');
                string[] elName = Regex.Matches(befehl[0], @"[a-zA-Z]+|\d+").Cast<Match>().Select(m => m.Value).ToArray();
                AnlagenElement el = null;
                switch (elName[0]) {
                    case "Gl":
                        el = this.Parent.GleisElemente.Element(Convert.ToInt16(elName[1]));
                        break;
                    case "Sn":
                        el = this.Parent.SignalElemente.Element(Convert.ToInt16(elName[1]));
                        break;
                    case "We":
                        el = this.Parent.WeicheElemente.Element(Convert.ToInt16(elName[1]));
                        break;
                    case "Fss":
                        el = this.Parent.FssElemente.Element(Convert.ToInt16(elName[1]));
                        break;
                    default:
                        break;
                }
                if (el != null) {
                    bool zustand=false;
                    if (befehl[1] == "An")
                        zustand = true;
                    else if (befehl[1] != "Aus")
                        return false;
                    list.Add(new Befehl(el,zustand));
                }

            }
            return true;
        }

        private void SucheAdressen() {
            _endBefehle = new List<Befehl>();
            _startBefehle = new List<Befehl>();
            Adresse snAdr;
            Adresse adr;
            bool stellungZielElemente = true;
            for (int i = 0; i < _listeElemente.Length; i++) {
                stellungZielElemente = (i < _listeElemente.Length - 2);
                switch (_listeElemente[i].GetType().Name) {
                    case "Signal":
                        _startBefehle.Add(new Befehl(_listeElemente[i], stellungZielElemente));
                        _endBefehle.Add(new Befehl(_listeElemente[i], false));
                        break;
                    case "Knoten":
                        Knoten kn = (Knoten)_listeElemente[i];
                        Gleis vergl = (Gleis)_listeElemente[i - 1];
                        Weiche we = null;
                        for (int j = 0; j < 4; j++) {
                            if (vergl == (Gleis)kn.Gleise[j]) {
                                if (j < 2)
                                    we = kn.Weichen[0];
                                else
                                    we = kn.Weichen[1];

                                if (we != null) {
                                    _startBefehle.Add(new Befehl(we,((j % 2) == 0) ^ we.Grundstellung));
                                }
                                break;
                            }
                        }
                        vergl = (Gleis)_listeElemente[i + 1];
                        for (int j = 0; j < 4; j++) {
                            if (vergl == (Gleis)kn.Gleise[j]) {
                                if (j < 2)
                                    we = kn.Weichen[0];
                                else
                                    we = kn.Weichen[1];

                                if (we != null) {
                                    _startBefehle.Add(new Befehl(we, ((j % 2) == 0) ^ we.Grundstellung));
                                }
                                break;
                            }
                        }
                        break;
                    case "Gleis":
                        snAdr = ((Gleis)_listeElemente[i]).Ausgang;
                        if (snAdr.ArdNr > 0) {
                            _startBefehle.Add(new Befehl(_listeElemente[i], stellungZielElemente));
                            adr = new Adresse(Parent, snAdr.ArdNr, snAdr.AdressenNr, snAdr.BitNr);
                            adr.Stellung = false;
                            _endBefehle.Add(new Befehl(_listeElemente[i], false));
                        }
                        break;
                }
            }
            //AdressListebereinigen(_endBefehle);
            //AdressListebereinigen(_startBefehle);
        }

        private void AdressListebereinigen(List<Befehl> adrListe) {
            int i = adrListe.Count - 1;
            while (i > 0) {
                int anz = 1;
                for (int j = i - 1; j >= 0; j--) {
                    if (adrListe[j].Element.Ausgang.GleicheAdresse(adrListe[i - anz + 1].Element.Ausgang)) {
                        adrListe.RemoveAt(j);
                        anz++;
                    }
                }
                i = i - anz;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        public override void ElementZeichnen(Graphics graphics) {
            Pen stift = new Pen(Color.Yellow, (Single)(this.Zoom * 0.2));
            switch (this.ElementZustand) {
                case Elementzustand.An:
                    stift.Color = Color.Red;
                    break;
                case Elementzustand.Aus:
                    return;
                case Elementzustand.Selektiert:
                    stift.Color = Color.Yellow;
                    break;
            }

            stift.EndCap = LineCap.Round;
            stift.LineJoin = LineJoin.Round;
            stift.StartCap = LineCap.Round;

            graphics.DrawPath(stift, _fahrstrLinie);
        }


        public override void Berechnung() {
            _fahrstrLinie.Reset();
            List<Point> punkte = new List<Point> { };
            punkte.Add(_startSignal.PositionRaster);
            foreach (AnlagenElement el in _listeElemente)
                if (el.GetType().Name != "Gleis")
                    punkte.Add(((RasterAnlagenElement)el).PositionRaster);
            punkte.Add(_endSignal.PositionRaster);

            _fahrstrLinie.AddLines(punkte.ToArray());
            Matrix matrix = new Matrix();
            matrix.Scale(Zoom, Zoom);
            _fahrstrLinie.Transform(matrix);
        }

        public void AdressenSperren() {
            for (int i = 0; i < _startBefehle.Count; i++) {
                if (i != 1) {
                    _startBefehle[i].Element.IsLocked = true;
                }
            }
        }

        /// <summary>
        /// prüft die Verfügbarkeit der FS (Adr.-Block., Zielsignal-Belegung
        /// </summary>
        /// <returns></returns>
        public bool AdressenFrei() {
           // if (_endSignal.Zug > 0)
             //   return false;
            foreach (Befehl adr in _startBefehle) {
                if (adr.Element.IsLocked) {
                    if (adr.SchaltZustand != adr.Element.Ausgang.AdresseAbfragen())
                        return false;
                }
            }
            return true;
        }

        public bool AusgangToggeln(FahrstrassenSignalTyp signalTyp) {
            if (AdressenFrei() || IsAktiv) {
                if (Parent.AnlagenZustand.FahrstrasseSchalten(this.ID)) {
                    for (int i = 0; i < _startBefehle.Count; i++) {
                        if (i != 1) {
                            this._startBefehle[i].BefehlAusfuehren();
                            this._startBefehle[i].Element.IsLocked = true;
                        }
                    }
                }
                else {
                    foreach (Befehl adr in _startBefehle) {
                        adr.Element.IsLocked = false;
                    }
                    foreach (Befehl adr in _endBefehle) {
                        adr.BefehlAusfuehren();
                        adr.Element.IsLocked = false;
                    }
					if(signalTyp == FahrstrassenSignalTyp.ZielSignal) {
                        //	this._startSignal.Zug = 0;
                        this._startSignal.ZugWechsel(_endSignal.ID);
					}
                }
                this.Selektiert = false;
                this.Ausgang.AusgangToggeln();
                Parent.FahrstarssenElemente.AktiveFahrstrassenAktualisieren(Parent.AnlagenZustand);
                return true;
            }
            return false;
        }

    }


}