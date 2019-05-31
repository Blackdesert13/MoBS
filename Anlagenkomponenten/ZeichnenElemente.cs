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
using System.IO;
using ModellBahnSteuerung.Elemente;

namespace MoBaSteuerung.Anlagenkomponenten {


	/// <summary>
	/// enthält alle Elemente einer Anlage
	/// </summary>
	public class AnlagenElemente {
		#region private Felder
		private ElementListe<Regler> _reglerElemente;
        private ElementListe<StartSignalGruppe> _ssgElemente;
        private ElementListe<Anschluss> _anschlussElemente;
		private ElementListe<Servo> servoElemente;
		private ElementListe<FSS> _fssElemente;
		private ElementListe<Gleis> _gleisElemente;
		private ElementListe<Weiche> _weicheElemente;
		private ElementListe<Signal> signalElemente;
		private ElementListe<Schalter> schalterElemente;
		private ElementListe<Knoten> _knotenElemente;
		private ElementListe<Entkuppler> _entkupplerElemente;
		private ElementListe<MCSpeicher.MCSpeicher> _listeMCSpeicher;
		private ElementListe<InfoFenster> infoElemente;
		private ElementListe<Haltestelle> haltestellenElemente;//für Straßenbahn
		private FahrstrassenNElemente _fahrstrassenElemente;
		private ElementListe<Zug> _zugElemente;
		private Anlagenzustand _anlagenZustand;
		private bool _rückmeldungAnzeigen;
		private bool _rückmeldungAktiv;
		private int _entkupplerAbschaltAutoWert;
		private bool _entkupplerAbschaltAutoAktiv;
		private Servo _aktiverServo = null;
		private ServoAction _aktiverServoRichtung = ServoAction.None;
		private int _zoom;
		private string zugDateiPfadName;
        #endregion //private Felder
        
        #region Properties
        public string ZugDateiPfadName { set { zugDateiPfadName = value; } }
    
        /// <summary>
        /// 
        /// </summary>
        public Int32 Zoom
        {
            set
            {
                this._reglerElemente.Zoom = value;
                this._ssgElemente.Zoom = value;
                this._knotenElemente.Zoom = value;
                this._gleisElemente.Zoom = value;
                this._weicheElemente.Zoom = value;
                this.signalElemente.Zoom = value;
                this.schalterElemente.Zoom = value;
                this._fssElemente.Zoom = value;
                this.infoElemente.Zoom = value;
                this._entkupplerElemente.Zoom = value;
                this._fahrstrassenElemente.Zoom = value;
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
        public AnzeigeTyp AnzeigeTyp
        {
            set
            {
                this._ssgElemente.AnzeigeTyp = value;
                this._gleisElemente.AnzeigeTyp = value;
                this._weicheElemente.AnzeigeTyp = value;
                this.signalElemente.AnzeigeTyp = value;
                this.schalterElemente.AnzeigeTyp = value;
                this._fssElemente.AnzeigeTyp = value;
                this._knotenElemente.AnzeigeTyp = value;
                this._entkupplerElemente.AnzeigeTyp = value;
                this._fahrstrassenElemente.AnzeigeTyp = value;
                this.infoElemente.AnzeigeTyp = value;
                this.servoElemente.AnzeigeTyp = value;
            }
        }
        public ElementListe<Regler> ReglerElemente { get { return this._reglerElemente; } }
        public ElementListe<StartSignalGruppe> SsgElemente { get { return this._ssgElemente; } }
        public ElementListe<Anschluss> AnschlussElemente { get { return this._anschlussElemente; } }
        public ElementListe<Servo> ServoElemente { get { return this.servoElemente; } }
        /// <summary>
        /// Liste aller Gleise
        /// </summary>
        public ElementListe<Gleis> GleisElemente { get { return this._gleisElemente; } }
        public ElementListe<MCSpeicher.MCSpeicher> ListeMCSpeicher
        {
            get
            {
                return _listeMCSpeicher;
            }
        }

        public ElementListe<Zug> ZugElemente
        {
            get { return _zugElemente; }
        }
        /// <summary>
        /// 
        /// </summary>
        public ElementListe<Weiche> WeicheElemente
        {
            get
            {
                return this._weicheElemente;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ElementListe<Signal> SignalElemente
        {
            get
            {
                return this.signalElemente;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ElementListe<Schalter> SchalterElemente
        {
            get
            {
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
                return this._fssElemente;
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
        /// liefert die Haltestellen-Liste
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
        public ElementListe<Knoten> KnotenElemente
        {
            get
            {
                return this._knotenElemente;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ElementListe<Entkuppler> EntkupplerElemente
        {
            get
            {
                return this._entkupplerElemente;
            }
        }




        public FahrstrassenNElemente FahrstrassenElemente
        {
            get
            {
                return _fahrstrassenElemente;
            }
        }

        public Anlagenzustand AnlagenZustand
        {
            get
            {
                return _anlagenZustand;
            }
            set
            {
                _anlagenZustand = value;
            }
        }

        public bool RückmeldungAnzeigen
        {
            get
            {
                return this._rückmeldungAnzeigen;
            }
            set
            {
                this._rückmeldungAnzeigen = value;
            }
        }

        public bool RückmeldungAktiv
        {
            get
            {
                return _rückmeldungAktiv;
            }

            set
            {
                _rückmeldungAktiv = value;
            }
        }
        #endregion //Properties
        #region Konstruktoren
        /// <summary>
        /// enthält alle Elemente der Anlage
        /// </summary>
        public AnlagenElemente() {
			this._reglerElemente = new ElementListe<Regler>();
            this._ssgElemente = new ElementListe<StartSignalGruppe>();
            this._anschlussElemente = new ElementListe<Anschluss>();
			this.servoElemente = new ElementListe<Servo>();
			this._knotenElemente = new ElementListe<Knoten>();
			this._gleisElemente = new ElementListe<Gleis>();
			this._weicheElemente = new ElementListe<Weiche>();
			this.signalElemente = new ElementListe<Signal>();
			this.schalterElemente = new ElementListe<Schalter>();
			this._fssElemente = new ElementListe<FSS>();
			this._entkupplerElemente = new ElementListe<Entkuppler>();
			this.infoElemente = new ElementListe<InfoFenster>();
			this.haltestellenElemente = new ElementListe<Haltestelle>();

			this._listeMCSpeicher = new ElementListe<MCSpeicher.MCSpeicher>();
			this._fahrstrassenElemente = new FahrstrassenNElemente();
			this._zugElemente = new ElementListe<Zug>();
			this._anlagenZustand = new Anlagenzustand();

			this.RückmeldungAnzeigen = false;
			this.RückmeldungAktiv = false;
			this.EntkupplerAbschaltAutoAktiv = true;
			this.EntkupplerAbschaltAutoWert = 3;
		}
        #endregion //Konstruktoren



		/// <summary>
		/// schaltet alle Gleise ein, für Digitalbetrieb
		/// </summary>
		public void GleiseEinschalten() {
			List<Gleis> items;
			items = _gleisElemente.Elemente;
			foreach (Gleis item in items) {
				item.AusgangSchalten(true);
			}
		}



		/// <summary>
		/// aktiviert die Koppelung nach dem Laden aller Elemente
		/// </summary>
		public void KoppelungenAktivieren() {
			_weicheElemente.KoppelungenAktivieren();
			_fssElemente.KoppelungenAktivieren();
			_gleisElemente.KoppelungenAktivieren();
			schalterElemente.KoppelungenAktivieren();
		}

		public Servo AktiverServo {
			get {
				return _aktiverServo;
			}
			set {
				if (_aktiverServo != null && value != null) {
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

		/// <summary>
		/// speichert die gegenwärtige Zug-Liste der Anlage in einer Datei
		/// </summary>
		/// <param name="zugDateiPfadName">Der Pfad zu der zu speichernden Zugdatei</param>
		public void ZugDateiSpeichern()//string zugDateiPfadName)
		{
			StreamWriter zugStreamWriter = new StreamWriter(zugDateiPfadName + ".zug", false, System.Text.Encoding.UTF8);

			zugStreamWriter.WriteLine(Environment.NewLine + "Züge\tNr.\tSignal\tLok\tTyp\tGeschw\tBez"
																																	 + this.ZugElemente.SpeicherString);
			zugStreamWriter.Flush();
			zugStreamWriter.Dispose();
		}

		public void FSSAktualisieren() {
			//Logging.Log.Schreibe("123", LogLevel.Info);
			int undefRegler = 0;
			int undefRegleralt = 0;
			string logString = "";
			//Logging.Log.Schreibe("", LogLevel.Info);
			foreach (FSS x in FssElemente.Elemente) {
				logString = logString + x.ID + "\t";
			}
			// Logging.Log.Schreibe(logString, LogLevel.Info);
			//logString.Insert(Environment.NewLine);
			foreach (FSS x in _fssElemente.Elemente) {//reset aller FSS, Regler 1 und 2 auf null
				undefRegler = undefRegler + x.reset();
				logString = logString + x.ReglerNummer1 + " " + x.ReglerNummer2 + " " + x.AktiverReglerNr + "\t";
			}
			/* foreach(FSS x in fssElemente.Elemente)
			 {

			 }*/

			while ((undefRegler != 0) && (undefRegler != undefRegleralt)) {
				undefRegleralt = undefRegler;
				undefRegler = 0;
				foreach (FSS x in _fssElemente.Elemente) {
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

		public List<Gleis> RueckmeldungSuchen(int ArduinoNr) {
			// List<Gleis> ergebnis = new List<Gleis>();
			List<Gleis> gListe = _gleisElemente.Elemente;
			return gListe.FindAll(x => x.Eingang.ArdNr == ArduinoNr);
			//return ergebnis;
		}

		/// <summary>
		/// liefert alle Anlagenelemente auf einer Relais-Platine
		/// </summary>
		/// <param name="ArduinoNr"></param>
		/// <param name="PlatinenNr"></param>
		/// <returns></returns>
		public List<AnlagenElement> RelaisAdresseSuchen(int ArduinoNr, int PlatinenNr) {
			List<AnlagenElement> ergebnis = new List<AnlagenElement>();
			ergebnis.AddRange(_weicheElemente.AdresseSuchen(ArduinoNr, PlatinenNr));
			ergebnis.AddRange(_gleisElemente.AdresseSuchen(ArduinoNr, PlatinenNr));
			ergebnis.AddRange(_fssElemente.AdresseSuchen(ArduinoNr, PlatinenNr));
			ergebnis.AddRange(_entkupplerElemente.AdresseSuchen(ArduinoNr, PlatinenNr));
			ergebnis.AddRange(signalElemente.AdresseSuchen(ArduinoNr, PlatinenNr));
			return ergebnis;
		}

		/// <summary>
		/// liefert alle Gleise einer RM-Platine
		/// </summary>
		/// <param name="ArduinoNr"></param>
		/// <param name="PlatinenNr"></param>
		/// <returns></returns>
		public List<Gleis> RMAdresseSuchen(int ArduinoNr, int PlatinenNr) {
			return _gleisElemente.Elemente.FindAll(x => (x.Eingang.ArdNr == ArduinoNr) && (x.Eingang.AdressenNr == PlatinenNr));
		}

		/// <summary>
		/// liefert alle Anlagenelemente mit dem gleichen Stecker
		/// </summary>
		/// <param name="SteckerBezeichnung"></param>
		/// <returns></returns>
		public List<AnlagenElement> Steckersuchen(string SteckerBezeichnung) {
			string stecker = SteckerBezeichnung;
			List<AnlagenElement> ergebnis = new List<AnlagenElement>();
			ergebnis.AddRange(ReglerElemente.SteckerSuchen(stecker));
			ergebnis.AddRange(_weicheElemente.SteckerSuchen(stecker));
			ergebnis.AddRange(_gleisElemente.SteckerSuchen(stecker));
			ergebnis.AddRange(_fssElemente.SteckerSuchen(stecker));
			ergebnis.AddRange(_entkupplerElemente.SteckerSuchen(stecker));
			ergebnis.AddRange(signalElemente.SteckerSuchen(stecker));
			ergebnis.AddRange(_anschlussElemente.SteckerSuchen(stecker));
			return ergebnis;
		}

		/// <summary>
		/// Züge werden in die Signale neu eingetragen
		/// </summary>
		public void ZuegeAktualisieren() {
			foreach (Signal x in signalElemente.Elemente) x.ZugNr = 0;
			foreach (Zug x in _zugElemente.Elemente) {
				if (x.SignalNummer != 0) {
					signalElemente.Element(x.SignalNummer).ZugNr = x.ID;
				}
			}
		}



		/// <summary>
		/// verknüpft FSS untereinander
		/// </summary>
		public void FSSLaden() {
			foreach (FSS x in _fssElemente.Elemente) { x.FSSLaden(); }
		}

		/// <summary>
		/// 
		/// </summary>
		public void AlleLöschen() {
			this._gleisElemente.AlleLöschen();
			this._weicheElemente.AlleLöschen();
			this.signalElemente.AlleLöschen();
			this.schalterElemente.AlleLöschen();
			this._knotenElemente.AlleLöschen();
            this._entkupplerElemente.AlleLöschen();
			this._fahrstrassenElemente.AlleLöschen();
			this._listeMCSpeicher.AlleLöschen();
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


