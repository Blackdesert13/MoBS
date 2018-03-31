using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Delegates;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using MoBaSteuerung.Anlagenkomponenten.MCSpeicher;
using MoBaSteuerung;
using MoBaSteuerung.Elemente;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading;
using MoBaSteuerung.ZeichnenElemente;
//using MoBa.Anlagenkomponenten.ZeichnenElemente;

namespace MoBaSteuerung {
    /// <summary>
    /// Anlagenlogik
    /// </summary>
    public class Model : Control {





        #region Private Felder

        private AnzeigeTyp anzeigeTyp;
        private AnlagenElemente zeichnenElemente;
        private ArduinoController _ardController;
        private bool _master = false;
        private AnlagenElement _neuesElement;
        private List<AnlagenElement> _auswahlElemente;
        private int _fahrstraßenStartVerzögerung;

        /// <summary>
        /// Dateiname.
        /// </summary>
        private string anlageDateiName;

        /// <summary>
        /// Dateipfad+name.
        /// </summary>
        private string anlageDateiPfadName;

        /// <summary>
        /// 
        /// </summary>
        private bool istAnlageSpeichernErforderlich;

        #endregion

        #region Öffentliche Eigenschaften (Properties)

        /// <summary>
        /// alle AnlagenElementListen
        /// </summary>
        public AnlagenElemente ZeichnenElemente { get { return zeichnenElemente; } }

        /// <summary>
        /// Speichern erforderlich.
        /// </summary>
        public bool IstAnlageSpeichernErforderlich
        {
            set
            {
                this.istAnlageSpeichernErforderlich = value;
            }
            get
            {
                return this.istAnlageSpeichernErforderlich;
            }
        }

        /// <summary>
        /// Anlagen Datei Name
        /// </summary>
        public string AnlageDateiName
        {
            get
            {
                return this.anlageDateiName;
            }
        }

        /// <summary>
        /// Anlage Datei Pfad Name
        /// </summary>
        public string AnlageDateiPfadName
        {
            get
            {
                return this.anlageDateiPfadName;
            }

            set
            {
                this.anlageDateiPfadName = value;
                // Dateiname übergeben
                if (string.IsNullOrEmpty(this.anlageDateiPfadName))
                {
                    this.anlageDateiName = Constanten.STANDARDFILENAME;
                }
                else {
                    this.anlageDateiName = Path.GetFileNameWithoutExtension(this.anlageDateiPfadName);
                }
            }
        }

        /// <summary>
        /// Art der Anzeige (Bedienen, Bearbeiten)
        /// </summary>
        public AnzeigeTyp Anzeigetyp {
            set {
                this.anzeigeTyp = value;
                this.zeichnenElemente.AnzeigeTyp = this.anzeigeTyp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Int32 Zoom {
            set {
                this.zeichnenElemente.Zoom = value;
            }
        }

        /// <summary>
        /// Server
        /// </summary>
        public bool Master {
            get {
                return _master;
            }
            set {
                _master = value;
            }
        }


        #endregion

        #region Konstruktor(en)

        /// <summary>
        /// 
        /// </summary>
        public Model() {
            this.zeichnenElemente = new AnlagenElemente();
            this._ardController = new ArduinoController();
            this.AuswahlElemente = new List<AnlagenElement>();

            this._ardController.BefehlReceived += _ardController_BefehlReceived;
            this.AnlagenzustandAdresseChanged += Model_AnlagenzustandAdresseChanged;

            this.FahrstraßenStartVerzögerung = 1000;
        }

        /// <summary>
        /// Liest ankommende Befehle vom ArduinoController und verarbeitet diese weiters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="a"></param>
        private void _ardController_BefehlReceived(object sender, BefehlEventArgs a) {
            byte[] befehl = a.Befehl;
            string befehlS = String.Empty;
            foreach (byte b in befehl)
                befehlS += b + " ";
            Debug.Print("Befehl von Arduino: " + befehlS);

            if (befehl.Length == 5) {
                Arduino arduino = this.zeichnenElemente.AnlagenZustand.GetArduino(befehl[0]);
                if (arduino != null) {
                    switch (befehl[1]) {
                        case 10:
                            arduino.Rueckmeldung[0] = (ushort)(befehl[2] + befehl[3] * 256);
                            OnArduinoRueckmeldungReceived();
                            break;
                        case 11:
                            arduino.Rueckmeldung[1] = (ushort)(befehl[2] + befehl[3] * 256);
                            OnArduinoRueckmeldungReceived();
                            break;
                        case 40:
                            arduino.Ausgaenge[0] = (ushort)(befehl[2] + befehl[3] * 256);
                            OnArduinoRueckmeldungReceived();
                            break;
                        case 41:
                            arduino.Ausgaenge[1] = (ushort)(befehl[2] + befehl[3] * 256);
                            OnArduinoRueckmeldungReceived();
                            break;
                        case 42:
                            arduino.Ausgaenge[2] = (ushort)(befehl[2] + befehl[3] * 256);
                            OnArduinoRueckmeldungReceived();
                            break;
                        case 99:
                            InfoFenster sysZeit = this.zeichnenElemente.InfoElemente.Element(99);
                            int zeit = befehl[2] + befehl[3] * 256;
                            string txt = "Ard-Zeit: ";
                            txt = txt + zeit + "_";// Convert.ToString( zeit);
                            try {
                                if (sysZeit != null) { sysZeit.Text = txt; }
                            }
                            catch (Exception e)
                            {
                                Debug.Print(e.Message);
                            }
                            OnArduinoRueckmeldungReceived();
                            break;
                    }
                    
                    if (befehl[1] > 99 && befehl[1] < 122)
                    {
                        int id = befehl[1] - 100;
                        Haltestelle haltestelle = this.zeichnenElemente.HaltestellenElemente.Element(id);
                        try {
                            if (haltestelle != null) {
                                haltestelle.InfoBefehl(befehl);
                            }
                        }
                        catch(Exception e) {
                            Debug.Print(e.Message);
                        }
                        OnArduinoRueckmeldungReceived();
                    }
                }
            }
        }



        /// <summary>
        /// Wenn Zustand in einer Adresse geändert wurde, wird dieser über den ArduinoController gesendet.
        /// Wird keine Adresse an die Methode übergeben, werden alle Adressen ausgegeben.
        /// </summary>
        /// <param name="adresse">Adresse in der sich ein Zustand geändert hat</param>
        private void Model_AnlagenzustandAdresseChanged(Adresse adresse) {
            if (adresse != null && adresse.ArdNr > 1) {
                byte[] befehl = this.zeichnenElemente.AnlagenZustand.GetBefehl(adresse.ArdNr, adresse.AdressenNr);
                if (befehl != null) {
                    Debug.Print("Sende an Arduino Befehl: " + befehl[0] + " " + befehl[1]
                                    + " " + befehl[2] + " " + befehl[3] + " " + befehl[4] + " ");
                    this._ardController.SendData(befehl);
                }
            }
            else {
                foreach (Arduino arduino in zeichnenElemente.AnlagenZustand.ArduinoListe) {
                    for (int i = 0; i < arduino.Ausgaenge.Length; i++)
                        this._ardController.SendData(this.zeichnenElemente.AnlagenZustand.GetBefehl(arduino.Nr, i));
                }
            }
        }

        public int BedienenMouseDoubleClick(Point location)
        {
            //TODO:
            //Zeichenelemente auf punkt suchen (Methode: SucheElementAufPunkt)
            //Zugnummer/Signalnummer auslesen, zurückgeben
            return 0;
        }

        #endregion

        #region Öffentliche Methoden
        /// <summary>
        /// Sucht nach Anlagenelementen, auf welche geclickt wurde
        /// </summary>
        /// <param name="punkt">Position des Clicks</param>
        /// <returns></returns>
        private List<AnlagenElement> SucheElementAufPunkt(Point punkt) {
            List<AnlagenElement> elemList = new List<AnlagenElement> { };
            foreach (Entkuppler el in zeichnenElemente.EntkupplerElemente.Elemente)
                if (el.MouseClick(punkt))
                    elemList.Add(el);
            foreach (Schalter el in zeichnenElemente.SchalterElemente.Elemente)
                if (el.MouseClick(punkt))
                    elemList.Add(el);
            foreach (FSS el in zeichnenElemente.FssElemente.Elemente)
                if (el.MouseClick(punkt))
                {
                    elemList.Add(el);
                }
            foreach (Signal el in zeichnenElemente.SignalElemente.Elemente)
                if (el.MouseClick(punkt))
                    elemList.Add(el);
            foreach (Weiche el in zeichnenElemente.WeicheElemente.Elemente)
                if (el.MouseClick(punkt))
                    elemList.Add(el);
            foreach (Gleis el in zeichnenElemente.GleisElemente.Elemente)
                if (el.MouseClick(punkt))
                    elemList.Add(el);

            return elemList;
        }


        /// <summary>
        /// Behandeln eines MausClicks beim Bedienen
        /// </summary>
        /// <param name="p">Position des Mausklicks</param>
        /// <returns></returns>
        public List<AnlagenElement> BedienenMouseClick(Point p) {
            zeichnenElemente.FahrstarssenElemente.AuswahlFahrstrassen.Clear();
            List<AnlagenElement> elemList = SucheElementAufPunkt(p);
            return elemList;
        }

        /// <summary>
        /// Umschalten eines Elementes
        /// </summary>
        /// <param name="elementName">Elementtyp</param>
        /// <param name="nr">ID des Elementes, welches geschaltet werden soll</param>
        /// <returns></returns>
        public bool ElementToggeln(string elementName, int nr) {
            AnlagenElement el = null;
            switch (elementName) {
                case "Signal":
                    el = zeichnenElemente.SignalElemente.Element(nr);
                    break;
                case "Gleis":
                    //el = zeichnenElemente.GleisElemente.Element(nr);
                    break;
                case "Schalter":
                    el = zeichnenElemente.SchalterElemente.Element(nr);
                    break;
                case "FSS":
                    el = zeichnenElemente.FssElemente.Element(nr);
                    
                    break;
                case "Entkuppler":
                    el = zeichnenElemente.EntkupplerElemente.Element(nr);
                    break;
                case "Weiche":
                    el = zeichnenElemente.WeicheElemente.Element(nr);
                    break;
                case "Fahrstrasse":
                    el = zeichnenElemente.FahrstarssenElemente.Fahrstrasse(nr);
                    if (!((Fahrstrasse)el).IsAktiv) {
                        Thread fahrstraßenStartThread = new Thread(this.FahrstraßeStarten);
                        fahrstraßenStartThread.Start(el);
                    }
                    break;
            }

            if (el != null) {
                bool action = el.AusgangToggeln();
                if(elementName == "FSS")
                    zeichnenElemente.FSSAktualisieren();
                if (action && _ardController.IsPortOpen())
                    OnAnlagenZustandAdresseChanged(el.Ausgang);
                return action;
            }

            return false;
        }

		public bool FahrstrasseSchalten(FahrstrasseN el, FahrstrassenSignalTyp signalTyp){
			if (el != null) {
				if (!el.IsAktiv) {
					Thread fahrstraßenStartThread = new Thread(this.FahrstraßeStarten);
					fahrstraßenStartThread.Start(el);
				}
				bool action = el.AusgangToggeln(signalTyp);
				if (action && _ardController.IsPortOpen())
					OnAnlagenZustandAdresseChanged(el.Ausgang);
				return action;
			}
			return false;
		}

		private void FahrstraßeStarten(object fahrstraße) {
            FahrstrasseN fs = (FahrstrasseN)fahrstraße;
            Adresse adrStartGleis = fs.adrStartGleis;
            Thread.Sleep(this.FahrstraßenStartVerzögerung);
            if (adrStartGleis != null && fs.IsAktiv) {
                adrStartGleis.AusgangSchalten();
                //adrStartGleis.IsLocked = true;

                this.OnAnlageNeuZeichnen();
                this.OnAnlagenzustandChanged();
                if (_ardController.IsPortOpen())
                    this.OnAnlagenZustandAdresseChanged(adrStartGleis);
            }
        }

        /// <summary>
        /// Behandeln eine Rechtsklicks bein Bedienen 
        /// (Fahrstrassen suchen/aktivieren)
        /// </summary>
        /// <param name="p">Position des Klicks</param>
        /// <returns></returns>
        public List<AnlagenElement> BedienenMouseRightClick(Point p) {
            List<AnlagenElement> elemList = SucheElementAufPunkt(p);
			return elemList;
            if (elemList.Count > 0)
                if (elemList[0].GetType().Name == "Signal") {
                    return FahrstrassenSignalSchalten((Signal)elemList[0]);
                }
            return null;
        }

        /// <summary>
        /// Schalten einer Fahrstraße über Tastatur
        /// </summary>
        /// <param name="signalNummer"></param>
        /// <returns></returns>
        public List<AnlagenElement> FahrstrassenSignal(int signalNummer) {
            Signal sn = zeichnenElemente.SignalElemente.Element(signalNummer);
            if (sn != null) {
                return FahrstrassenSignalSchalten(sn);
            }
            return null;
        }


        /// <summary>
        /// Fahrstrasse über Signal schalten.<para/>
        /// Wenn Fahrstraßen in der Vorauswahl sind, wird überprüft ob das ausgewählte Signal ein gültiges Zielsignal ist <para/>
        /// und die Fahrstrasse zurück gegeben und die Vorauswahl gelöscht.<para/>
        /// Ist keine Fahrstraße in der Vorauswahl wird überprüft ob das Signal zu einer aktiven Fahrstrasse gehört und <para/>
        /// diese zurückgegeben. Ist dies nicht der Fall werden alle verfügbaren Fahrstrassen, welche von diesem <para/>
        /// Signal ausgehen zurückgegeben.
        /// </summary>
        /// <param name="signal">Startsignal</param>
        /// <returns></returns>
        public List<AnlagenElement> FahrstrassenSignalSchalten(Signal signal) {
            List<AnlagenElement> el = new List<AnlagenElement>();
            if (zeichnenElemente.FahrstarssenElemente.AuswahlFahrstrassen.Count == 0) {
                foreach (FahrstrasseN fs in zeichnenElemente.FahrstarssenElemente.AktiveFahrstrassen)
                    if (signal == fs.StartSignal || signal == fs.EndSignal) {
                        el.Add(fs);
                        return el;
                    }
                foreach (FahrstrasseN fs in zeichnenElemente.FahrstarssenElemente.GespeicherteFahrstrassen) {
                    if (fs.StartSignal == signal && fs.AdressenFrei())
                        el.Add(fs);
                }
                //zeichnenElemente.FahrstarssenElemente.SucheFahrstrassen((Signal)elemList[0]);
                return el;
            }
            else {
                foreach (FahrstrasseN fs in zeichnenElemente.FahrstarssenElemente.AuswahlFahrstrassen)
                    if (fs.EndSignal == signal) {
                        zeichnenElemente.FahrstarssenElemente.AlleLöschenAuswahl();
                        el.Add(fs);
                        //zeichnenElemente.FahrstarssenElemente.HinzufügenAktiv(fs);
                        return el;
                    }
                zeichnenElemente.FahrstarssenElemente.AlleLöschenAuswahl();
                OnAnlageNeuZeichnen();
            }
            return null;
        }

        /// <summary>
        /// Anlagenzustand wird eingelesen und Deserialisiert
        /// </summary>
        /// <param name="anlagenZustandsDaten"></param>
        public void AnlagenZustandsDatenEinlesen(byte[] anlagenZustandsDaten) {
            XmlSerializer formatter = new XmlSerializer(typeof(Anlagenzustand));
            MemoryStream stream = new MemoryStream(anlagenZustandsDaten);
            this.zeichnenElemente.AnlagenZustand = (Anlagenzustand)formatter.Deserialize(stream);

            this.zeichnenElemente.FahrstarssenElemente.AktiveFahrstrassenAktualisieren(this.zeichnenElemente.AnlagenZustand);
        }

        /// <summary>
        /// Der Anlagenzustand wird Serialisiert zurückgegeben
        /// </summary>
        /// <returns></returns>
        public byte[] AnlagenZustandsDatenAuslesen() {
            XmlSerializer serializer = new XmlSerializer(typeof(Anlagenzustand));

            //BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            //formatter.Serialize(stream, this.zeichnenElemente.AnlagenZustand);
            serializer.Serialize(stream, this.zeichnenElemente.AnlagenZustand);

            return stream.GetBuffer();
        }


        /// <summary>
        /// löscht alle gegenwärtige Elemente und 
        /// lädt eine neue Anlage
        /// </summary>
        public void AnlageLaden(string anlageDateiPfadName) {
            this.AnlageLaden(this.AnlageDatenEinlesen(anlageDateiPfadName));
        }


        /// <summary>
        /// Lädt die Anlagedatei ein.
        /// </summary>
        /// <param name="anlageDateiPfadName"></param>
        /// <returns></returns>
        public byte[] AnlageDatenEinlesen(string anlageDateiPfadName) {
            if (!string.IsNullOrEmpty(anlageDateiPfadName)) {
                FileStream anlageFileStream = new FileStream(anlageDateiPfadName, FileMode.Open, FileAccess.Read, FileShare.Read);

                byte[] anlageBytes = new byte[anlageFileStream.Length];
                anlageFileStream.Read(anlageBytes, 0, anlageBytes.Length);

                anlageFileStream.Close();
                anlageFileStream.Dispose();

                return anlageBytes;
            }
            return null;
        }


        /// <summary>
        /// löscht alle gegenwärtige Elemente und 
        /// lädt eine neue Anlage
        /// </summary>
        public void AnlageLaden(byte[] anlageDaten) {
            // this.AnlageZurücksetzen();
            zeichnenElemente = new AnlagenElemente();
            StreamReader anlageDatenStreamReader = new StreamReader(new MemoryStream(anlageDaten), System.Text.Encoding.UTF8);

            string zeile = "";
            while ((zeile = anlageDatenStreamReader.ReadLine()) != null) {
                string[] elem = zeile.Split('\t');

                if (elem[0] == "MCSpeicher") {
                    new MCSpeicher(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem);
                }

                if (elem[0] == "Regler")
                {
                    new Regler(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem);
                }

                if (elem[0] == "Knot") {
                    new Knoten(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem);
                }

                if (elem[0] == "Gleis") {
                    new Gleis(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem);
                }

                if (elem[0] == "Weiche") {
                    string[] knot = elem[2].Split(' ');
                    Knoten kn = zeichnenElemente.KnotenElemente.Element(Convert.ToInt32(knot[0]));
                    if (kn != null) {
                        Weiche we = kn.Weichen[Convert.ToInt32(knot[1])];
                        if (we != null) {
                            we.Grundstellung = Convert.ToBoolean(elem[3]);
                            we.ID = Convert.ToInt32(elem[1]);
                            we.Ausgang.SpeicherString = elem[4];
                            we.Bezeichnung = elem[5];
                            if (elem.Length > 6)
                                we.Stecker = elem[6];
                        }
                    }
                }

                if (elem[0] == "Signal") {
                    new Signal(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem);
                }

                if (elem[0] == "FSS")    {
                    new FSS(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem);
                }
                if (elem[0] == "Schalter") {
                    new Schalter(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem);
                }

                if (elem[0] == "Entkuppler") {
                    new Entkuppler(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem);
                }

                if (elem[0] == "Info") {
                    new InfoFenster(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem);
                }

                if (elem[0] == "Zug")
                {
                    new Zug(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem);
                }

                if (elem[0] == "HS")
                {
                    new Haltestelle(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem);
                }

                if (elem[0] == "FahrstrasseV") {
                    string befehleStart = anlageDatenStreamReader.ReadLine();
                    string befehleZiel = anlageDatenStreamReader.ReadLine();
                    string knotenListe = anlageDatenStreamReader.ReadLine();
                    Fahrstrasse neueFahrstrasse = new Fahrstrasse(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem, befehleStart, befehleZiel, knotenListe);
                }
				if (elem[0] == "FahrstrasseN") {
					string befehleStart = anlageDatenStreamReader.ReadLine();
					string befehleZiel = anlageDatenStreamReader.ReadLine();
					string knotenListe = anlageDatenStreamReader.ReadLine();
					new FahrstrasseN(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem, befehleStart, befehleZiel, knotenListe);
				}
			}      
            anlageDatenStreamReader.Dispose();
            this.zeichnenElemente.FSSLaden();
            zeichnenElemente.FSSAktualisieren();
            this.OnAnlageGrößeInRasterChanged(new Size(65, 35));
        }

        public void FahrstrassenAuswahl(List<AnlagenElement> el) {
            this.zeichnenElemente.FahrstarssenElemente.HinzufügenAuswahl(el);
        }


        /// <summary>
        /// Neue Anlage
        /// </summary>
        public void AnlageNeu() {
            this.AnlageZurücksetzen();
        }


        /// <summary>
        /// speichert die gegenwärtige Anlage in einer Datei
        /// </summary>
        /// <param name="zugDateiPfadName">Der Pfad zu der zu speichernden Zugdatei</param>
        public void zugDateiSpeichern(string zugDateiPfadName)
        {
            StreamWriter zugStreamWriter = new StreamWriter(zugDateiPfadName, false, System.Text.Encoding.UTF8);

            zugStreamWriter.WriteLine(Environment.NewLine + "Züge\tNr.\tSignal\tLok\tTyp\tGeschw\tBez"
                                           + this.zeichnenElemente.ZugElemente.SpeicherString);
            zugStreamWriter.Flush();
            zugStreamWriter.Dispose();
        }

        /// <summary>
        /// speichert die gegenwärtige Anlage in einer Datei
        /// </summary>
        /// <param name="anlageDateiPfadName">Der Pfad zu der zu speichernden Anlagendatei</param>
        public void AnlageSpeichern(string anlageDateiPfadName) {
            string trennung = "--------------------------------------------------------------------------------------------";

            StreamWriter anlageStreamWriter = new StreamWriter(anlageDateiPfadName, false, System.Text.Encoding.UTF8);
            anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Arduinos\tNr.\tLageX\tLageY\tAnl.-Teil"
                                            + this.zeichnenElemente.ListeMCSpeicher.SpeicherString);
            anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "FahrReg\tNr.\tLageX\tLageY\tFarbe\tFarbeZ\tBez\tStecker"
                                            + this.zeichnenElemente.ReglerElemente.SpeicherString);
            anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Knoten\tNr.\tLageX\tLageY"
                                            + this.zeichnenElemente.KnotenElemente.SpeicherString);
            anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Gleise\tNr.\tStartKn\tEndKn\tRegler\tAusgang\tEingang\tBez.\tStecker" 
                                            + this.zeichnenElemente.GleisElemente.SpeicherString);
            anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Weichen\tNr.\tKnoten\tGru-Ste\tAusgang\tBez.\tStecker"
                                            + this.zeichnenElemente.WeicheElemente.SpeicherString);
            anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "GleisSchalter\tNr.\tGleis\tBez."
                                            + this.zeichnenElemente.SchalterElemente.SpeicherString);
            anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "FSSer\tNr.\tGleis\tRegler1\tRegler2\tAusgang\tBez.\tStecker"
                                            + this.zeichnenElemente.FssElemente.SpeicherString);
            anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Entkuppler_\tNr.\tGleis\tAusgang\tBez.\tStecker"
                                            + this.zeichnenElemente.EntkupplerElemente.SpeicherString);
            anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Infos\tNr.\tGleis\tLage\tBez"
                                            + this.zeichnenElemente.InfoElemente.SpeicherString);
            anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Signale\tNr.\tGleis\tRichtu.\tInfoF\tAusgang\tBez.\tStecker"
                                            + this.zeichnenElemente.SignalElemente.SpeicherString);     
            anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "HStelln\tNr.\tInfFeld\tBez"
                                            + this.zeichnenElemente.HaltestellenElemente.SpeicherString);

            anlageStreamWriter.WriteLine(trennung + Environment.NewLine + this.zeichnenElemente.FahrstarssenElemente.SpeicherString);
            
            anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Züge\tNr.\tSignal\tLok\tTyp\tGeschw\tBez"
                                           + this.zeichnenElemente.ZugElemente.SpeicherString);
            anlageStreamWriter.Flush();
            anlageStreamWriter.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        public void AnlageZurücksetzen() {
            NeuesElementVorschauReset();
            this.zeichnenElemente.AlleLöschen();
        }

        /// <summary>
        /// Alles neu zeichnen im Bedienmodus.
        /// </summary>
        /// <param name="graphics"></param>
        public void ZeichneElementeBedienen(Graphics graphics) {
            graphics.SmoothingMode = SmoothingMode.HighQuality;

            this.zeichnenElemente.GleisElemente.ElementeZeichnen1(graphics);
            this.zeichnenElemente.GleisElemente.ElementeZeichnen(graphics);
            this.zeichnenElemente.WeicheElemente.ElementeZeichnen(graphics);
            this.zeichnenElemente.FahrstarssenElemente.ElementeZeichnen(graphics);
            //this.zeichnenElemente.KnotenElemente.ElementeZeichnen(graphics);
            this.zeichnenElemente.SchalterElemente.ElementeZeichnen(graphics);
            this.zeichnenElemente.FssElemente.ElementeZeichnen(graphics);
            this.zeichnenElemente.EntkupplerElemente.ElementeZeichnen(graphics);
            this.zeichnenElemente.SignalElemente.ElementeZeichnen(graphics);
            this.zeichnenElemente.InfoElemente.ElementeZeichnen(graphics);
            this.zeichnenElemente.ReglerElemente.ElementeZeichnen(graphics);
            graphics.SmoothingMode = SmoothingMode.Default;
        }

        /// <summary>
        /// Alles neu zeichnen im Bearbeitungsmodus.
        /// </summary>
        /// <param name="graphics"></param>
        public void ZeichneElementeBearbeiten(Graphics graphics) {
            graphics.SmoothingMode = SmoothingMode.HighQuality;

            this.zeichnenElemente.FahrstarssenElemente.ElementeZeichnen(graphics);
            this.zeichnenElemente.GleisElemente.ElementeZeichnen(graphics);
            this.zeichnenElemente.WeicheElemente.ElementeZeichnen(graphics);
            this.zeichnenElemente.KnotenElemente.ElementeZeichnen(graphics);
            this.zeichnenElemente.SchalterElemente.ElementeZeichnen(graphics);
            this.zeichnenElemente.EntkupplerElemente.ElementeZeichnen(graphics);
            this.zeichnenElemente.SignalElemente.ElementeZeichnen(graphics);
            this.zeichnenElemente.FssElemente.ElementeZeichnen(graphics);

            if (this._neuesElement != null)
                this._neuesElement.ElementZeichnen(graphics);

            graphics.SmoothingMode = SmoothingMode.Default;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphicsPath"></param>
        /// <returns></returns>
        public bool AuswahlRechteckElemente(GraphicsPath graphicsPath) {
            // ToDo sind Elemente im Auswahlrechteck


            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphicsPath"></param>
        public void AuswahlRechteckVerschieben(GraphicsPath graphicsPath) {
            //To Do Verschieben.

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphicsPath"></param>
        public void AuswahlRechteckElementeLöschen(GraphicsPath graphicsPath) {
            //To Do löschen.

        }


        public string StringBereinigen(string EingangsString)
        {
            string startString = EingangsString;// null;//"  AA A";

            string ergebnis = "";
            if (startString != null)
            {
                string[] arbeitsArray = startString.Split(' ');
                for (int a = 0; a < arbeitsArray.Length; a++)
                {
                    for (int b = a + 1; b < arbeitsArray.Length; b++)
                    {
                        if (arbeitsArray[a] == arbeitsArray[b]) { arbeitsArray[b] = ""; }
                    }
                    if (arbeitsArray[a] != "")
                    {
                        if (ergebnis != "") ergebnis = ergebnis + " ";
                        ergebnis = ergebnis + arbeitsArray[a];
                    }
                }
            }

            return ergebnis;
        }
        #endregion

        #region Events

        #region EeventHandler

        /// <summary>
        /// 
        /// </summary>
        public event ViewNeuZeichnenEventHandler AnlageNeuZeichnen;


        /// <summary>
        /// 
        /// </summary>
        public event SizeEventHandler AnlageGrößeInRasterChanged;

        /// <summary>
        /// 
        /// </summary>
        public event ArduinoRueckmeldungEventHandler ArduinoRueckmeldungReceived;

        /// <summary>
        /// 
        /// </summary>
        public event ArduinoSendEventHandler AnlagenzustandAdresseChanged;

        /// <summary>
        /// 
        /// </summary>
        public event AnlagenZustandEventHandler AnlagenzustandChanged;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnAnlageNeuZeichnen() {
            if (this.AnlageNeuZeichnen != null) {
                this.AnlageNeuZeichnen();
            }
        }

        protected virtual void OnArduinoRueckmeldungReceived() {
            if (this.ArduinoRueckmeldungReceived != null) {
                this.ArduinoRueckmeldungReceived();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnAnlagenZustandAdresseChanged(Adresse adresse) {
            if (this.AnlagenzustandAdresseChanged != null) {
                this.AnlagenzustandAdresseChanged(adresse);
            }
        }

        /// <summary>
        /// Wird ausgelösst, wenn die Größe geändert wurde.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnAnlageGrößeInRasterChanged(Size e) {
            if (this.AnlageGrößeInRasterChanged != null) {
                this.AnlageGrößeInRasterChanged(e);
            }
        }

        protected virtual void OnAnlagenzustandChanged() {
            if (this.AnlagenzustandChanged != null) {
                this.AnlagenzustandChanged();
            }
        }
      
        /// <summary>
        /// liefert die Zugliste
        /// </summary>
        /// <returns></returns>
        public ElementListe<Zug> GetZugListe()
        {
            return zeichnenElemente.ZugElemente;
        }
      
        /// <summary>
        /// liefert alle verfügbaren Portnamen
        /// </summary>
        /// <returns></returns>
        public string[] GetSerialPortNames() {
            return _ardController.GetSerialPortNames();
        }

        public bool OpenComPort(string portName) {
            if (this._ardController.OpenComPort(portName)) {
                if (zeichnenElemente.RückmeldungAktiv)
                    _ardController.SendData(new byte[] { 1, 1, 0, 0, 2 });
                else
                    _ardController.SendData(new byte[] { 1, 2, 0, 0, 3 });
                return true;
            }
            return false;
        }

        public bool CloseComPort() {
            return this._ardController.CloseComPort();
        }

        public void FahrstrassenSuchen() {
            zeichnenElemente.FahrstarssenElemente.GespeicherteFahrstrassen.Clear();
            zeichnenElemente.FahrstarssenElemente.GespeicherteFahrstrassen = new List<FahrstrasseN>();
            foreach (Signal sn in zeichnenElemente.SignalElemente.Elemente) {
                zeichnenElemente.FahrstarssenElemente.SucheFahrstrassen(sn);
            }
        }

        /// <summary>
        /// Löscht die gegenwärtige Auswahl (Selektion) im Bedienmodus, gegenwärtig können dies nur Fahrstraßen sein
        /// </summary>
        /// <returns>Gibt TRUE zurück, wenn eine Auswahl gelöscht wurde</returns>
        public bool BedienenAuswahlLöschen() {
            if (zeichnenElemente.FahrstarssenElemente.AuswahlFahrstrassen.Count > 0) {
                zeichnenElemente.FahrstarssenElemente.AlleLöschenAuswahl();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gibt an ob Rückmeldung mit dargestellt werden soll
        /// </summary>
        public bool RückmeldungAnzeigen {
            set {
                this.zeichnenElemente.RückmeldungAnzeigen = value;
                OnAnlageNeuZeichnen();
            }
            get {
                return this.zeichnenElemente.RückmeldungAnzeigen;
            }
        }

        public bool RückmeldungAktiv {
            set {
                if (value != this.zeichnenElemente.RückmeldungAktiv) {
                    if (value)
                        _ardController.SendData(new byte[] { 1, 1, 0, 0, 2 });
                    else
                        _ardController.SendData(new byte[] { 1, 2, 0, 0, 3 });
                }
                this.zeichnenElemente.RückmeldungAktiv = value;
            }
            get {
                return this.zeichnenElemente.RückmeldungAktiv;
            }
        }

        public int FahrstraßenStartVerzögerung {
            get {
                return _fahrstraßenStartVerzögerung;
            }

            set {
                _fahrstraßenStartVerzögerung = value;
            }
        }

        public List<AnlagenElement> AuswahlElemente
        {
            get
            {
                return _auswahlElemente;
            }

            set
            {
                _auswahlElemente = value;
            }
        }



        public bool NeuesElementVorschau(BearbeitungsModus bearbeitungsModus, Point letzterRasterpunkt, int zoom) {
            if (this._neuesElement == null) {
                switch (bearbeitungsModus) {
                    case BearbeitungsModus.Gleis:
                        this._neuesElement = new Knoten(this.zeichnenElemente, zoom, this.anzeigeTyp, letzterRasterpunkt);
                        break;
                    case BearbeitungsModus.Schalter:
                        this._neuesElement = new Schalter(this.zeichnenElemente, zoom, this.anzeigeTyp, letzterRasterpunkt);
                        break;
                    case BearbeitungsModus.Signal:
                        this._neuesElement = new Signal(this.zeichnenElemente, zoom, this.anzeigeTyp, letzterRasterpunkt, false);
                        break;
                    case BearbeitungsModus.Entkuppler:
                        this._neuesElement = new Entkuppler(this.zeichnenElemente, zoom, this.anzeigeTyp, letzterRasterpunkt);
                        break;
                    case BearbeitungsModus.Fss:
                        this._neuesElement = new FSS(this.zeichnenElemente, zoom, this.anzeigeTyp, letzterRasterpunkt);
                        break;
                }
            }
            else {
                switch (bearbeitungsModus) {
                    case BearbeitungsModus.Gleis:
                        if (this._neuesElement.GetType().Name == "Knoten") {
                            ((RasterAnlagenElement)this._neuesElement).PositionRaster = letzterRasterpunkt;
                            this._neuesElement.BearbeitenAktualisierenNeuZeichnen();
                        }
                        else {
                            Gleis gl = (Gleis)this._neuesElement;
                            gl.EndKn = new Knoten(this.zeichnenElemente, zoom, this.anzeigeTyp, letzterRasterpunkt);
                            gl.BearbeitenAktualisierenNeuZeichnen();
                        }
                        break;
                    case BearbeitungsModus.Signal:
                    case BearbeitungsModus.Entkuppler:
                    case BearbeitungsModus.Schalter:
                    case BearbeitungsModus.Fss:
                        ((RasterAnlagenElement)this._neuesElement).PositionRaster = letzterRasterpunkt;
                        this._neuesElement.BearbeitenAktualisierenNeuZeichnen();
                        break;
                }
            }
            return true;
        }

        /// <summary>
        /// Setzt das Vorschau Element zurück (weist diesem NULL zu)
        /// </summary>
        public void NeuesElementVorschauReset() {
            if (this._neuesElement != null) {
                this._neuesElement = null;
                OnAnlageNeuZeichnen();
            }
        }


        public bool BearbeitenNeuZeichnen(BearbeitungsModus bearbeitungsModus, MouseButtons button, Point rasterpunkt) {
            if (this._neuesElement != null) {
                switch (bearbeitungsModus) {
                    case BearbeitungsModus.Gleis:
                        if (this._neuesElement.GetType().Name == "Knoten") {
                            Gleis gl = new Gleis(this.zeichnenElemente, this._neuesElement.Zoom, this.anzeigeTyp, (Knoten)this._neuesElement, (Knoten)this._neuesElement);
                            this._neuesElement = gl;
                        }
                        else {
                            Gleis gl = (Gleis)this._neuesElement;
                            Knoten startKnoten = this.zeichnenElemente.SucheKnoten(gl.StartKn.PositionRaster);
                            Knoten endKnoten = this.zeichnenElemente.SucheKnoten(gl.EndKn.PositionRaster);
                            if (startKnoten == null)
                                startKnoten = new Knoten(this.zeichnenElemente, this.zeichnenElemente.KnotenElemente.SucheFreieNummer(), gl.StartKn.Zoom,
                                                                this.anzeigeTyp, gl.StartKn.PositionRaster);
                            if (endKnoten == null)
                                endKnoten = new Knoten(this.zeichnenElemente, this.zeichnenElemente.KnotenElemente.SucheFreieNummer(), gl.EndKn.Zoom,
                                                                this.anzeigeTyp, gl.EndKn.PositionRaster);
                            new Gleis(this.zeichnenElemente, this.zeichnenElemente.GleisElemente.SucheFreieNummer(), _neuesElement.Zoom, this.anzeigeTyp,
                                        startKnoten, endKnoten);
                            NeuesElementVorschauReset();
                            return true;
                        }
                        break;
                    case BearbeitungsModus.Signal:
                        _neuesElement.GleisElementAustragen();
                        new Signal(this.zeichnenElemente, this.zeichnenElemente.SignalElemente.SucheFreieNummer(), _neuesElement.Zoom,
                                    _neuesElement.AnzeigenTyp, ((RasterAnlagenElement)_neuesElement).PositionRaster, ((Signal)_neuesElement).InZeichenRichtung);
                        NeuesElementVorschauReset();
                        return true;
                    case BearbeitungsModus.Entkuppler:
                        _neuesElement.GleisElementAustragen();
                        new Entkuppler(this.zeichnenElemente, this.zeichnenElemente.EntkupplerElemente.SucheFreieNummer(), _neuesElement.Zoom,
                                        _neuesElement.AnzeigenTyp, ((RasterAnlagenElement)_neuesElement).PositionRaster);
                        NeuesElementVorschauReset();
                        return true;
                    case BearbeitungsModus.Schalter:
                        _neuesElement.GleisElementAustragen();
                        new Schalter(this.zeichnenElemente, this.zeichnenElemente.SchalterElemente.SucheFreieNummer(), _neuesElement.Zoom,
                                      _neuesElement.AnzeigenTyp, ((RasterAnlagenElement)_neuesElement).PositionRaster);
                        NeuesElementVorschauReset();
                        return true;
                    case BearbeitungsModus.Fss:
                        _neuesElement.GleisElementAustragen();
                        new FSS(this.zeichnenElemente, this.zeichnenElemente.FssElemente.SucheFreieNummer(), _neuesElement.Zoom,
                                      _neuesElement.AnzeigenTyp, ((RasterAnlagenElement)_neuesElement).PositionRaster);
                        NeuesElementVorschauReset();
                        return true;
                }
            }
            return false;
        }

        private List<AnlagenElement> SucheElementSelektieren(Point punkt) {
            List<AnlagenElement> elemList = new List<AnlagenElement> { };

            foreach (Knoten el in zeichnenElemente.KnotenElemente.Elemente)
                if (el.MouseClick(punkt))
                    elemList.Add(el);
            foreach (Entkuppler el in zeichnenElemente.EntkupplerElemente.Elemente)
                if (el.MouseClick(punkt))
                    elemList.Add(el);
            foreach (Schalter el in zeichnenElemente.SchalterElemente.Elemente)
                if (el.MouseClick(punkt))
                    elemList.Add(el);
            foreach (Signal el in zeichnenElemente.SignalElemente.Elemente)
                if (el.MouseClick(punkt))
                    elemList.Add(el);
            foreach (Weiche el in zeichnenElemente.WeicheElemente.Elemente)
                if (el.MouseClick(punkt))
                    elemList.Add(el);
            foreach (FSS el in zeichnenElemente.FssElemente.Elemente)
                if (el.MouseClick(punkt))
                    elemList.Add(el);
            foreach (Gleis el in zeichnenElemente.GleisElemente.Elemente)
                if (el.MouseClick(punkt))
                    elemList.Add(el);
            return elemList;
        }


        public bool StartOnSelElement(Point location) {
            foreach (AnlagenElement el in this.AuswahlElemente)
                if (el.MouseClick(location))
                    return true;
            return false;
        }


        public string BearbeitenAnlagenElementInfoText(Point punkt) {
            List<AnlagenElement> elListe = this.SucheElementAufPunkt(punkt);
            if(elListe.Count > 0) {
                return elListe[0].InfoString;
            }
            return String.Empty;
        }

        public void BearbeitenSelektionLöschen() {
            foreach (AnlagenElement el in this.AuswahlElemente)
                el.Selektiert = false;
            this.AuswahlElemente.Clear();
        }

        public bool BearbeitenSelektieren(MouseButtons button, bool ctrlPressed, Point punkt) {
            List<AnlagenElement> elListe = this.SucheElementSelektieren(punkt);
            if (ctrlPressed) {
                //this._auswahlElemente.AddRange(elListe);
            }
            else {
                BearbeitenSelektionLöschen();
                this.AuswahlElemente = elListe;
            }

            foreach (AnlagenElement el in this.AuswahlElemente)
                el.Selektiert = true;

            return true;
        }

        public void BearbeitenSelektieren(AnlagenElement element) {
            BearbeitenSelektionLöschen();
            if (element == null) {
                OnAnlageNeuZeichnen();
                return;
            }
            this.AuswahlElemente.Add(element);
            element.Selektiert = true;
            OnAnlageNeuZeichnen();
        }

        public void BearbeitenDragDrop(Point deltaRaster, DragDropEffects effect) {
            foreach (AnlagenElement el in this.AuswahlElemente)
                ((RasterAnlagenElement)el).DragDropPositionVerschieben(deltaRaster);
        }

        public void BearbeitenDragDropAbschließen(DragDropEffects effect) {
            foreach (AnlagenElement el in this.AuswahlElemente)
                ((RasterAnlagenElement)el).DragDropAbschließen();

            BearbeitenSelektieren(MouseButtons.Left, false, new Point(-1, -1));
        }

        public bool SignalDrehen() {
            if (_neuesElement.GetType().Name == "Signal") {
                Signal sig = (Signal)_neuesElement;
                sig.InZeichenRichtung = !sig.InZeichenRichtung;
                sig.Berechnung();
                return true;
            }
            return false;
        }
        #endregion
    }
}