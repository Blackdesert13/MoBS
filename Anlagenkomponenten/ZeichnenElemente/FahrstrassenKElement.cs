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
using MoBaSteuerung.ZeichnenElemente;

namespace MoBaSteuerung.Elemente {
	
	/// <summary>
	/// aktuell verwendete Fahrstrasse
	/// </summary>
	public class FahrstrasseK : AnlagenElement {
		private GraphicsPath _fahrstrLinie = new GraphicsPath();
		private Signal _startSignal;
		private bool _isAktiv = false;
        private List<FahrstrasseN> _fahrStrassen;

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public override string SpeicherString {
			get {
                string txt = "FahrstrasseK"
                                        + "\t" + ID
                                        + "\t" + StartSignal.ID;
				txt += Environment.NewLine + " FahrstrassenListe";
				foreach (var item in _fahrStrassen) {
					txt += "\t" + item.ID;
				}
				return txt;
			}
		}
		/// <summary>
		/// gibt an ob FS aktiv ist
		/// </summary>
		public bool IsAktiv {
			get {
				return _isAktiv;
			}
			set {
				_isAktiv = value;
				this.Ausgang.Stellung = value;
			}
		}
        
		public Signal StartSignal {
			get {
				return _startSignal;
			}
		}
        
        
        
		#endregion//Properties
		#region Konstuktoren
		/// <summary>
		/// erstellt FS aus Serialisierung (für Slave)
		/// </summary>
		/// <param name="elementListe">Serialisierung</param>
		public FahrstrasseK(AnlagenElement[] elementListe)
		: base(elementListe[0].Parent, elementListe[0].Parent.FahrstrassenElemente.SucheFreieNummer(), elementListe[0].Zoom, elementListe[0].AnzeigenTyp) {
			
			_startSignal = (Signal)elementListe[0];
			Berechnung();
		}

		public FahrstrasseK(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, string[] elem)
				: base(parent, Convert.ToInt32(elem[1]), zoom, anzeigeTyp) {


            Parent.FahrstrassenKElemente.Hinzufügen(this);
			Berechnung();
		}
		#endregion//Konstruktoren
		private string AuslesenBefehlsliste(List<Befehl> list) {
			string spString = String.Empty;
			foreach (Befehl befehl in list) {
				spString += "\t";
				AnlagenElement el = befehl.Element;
				if (el is Gleis) {
					spString += "Gl" + el.ID + ":" + (befehl.SchaltZustand ? "An" : "Aus");
				}
				else if (el is Signal) {
					spString += "Sn" + el.ID + ":" + (befehl.SchaltZustand ? "An" : "Aus");
				}
				else if (el is Weiche) {
					spString += "We" + el.ID + ":" + (befehl.SchaltZustand ? "An" : "Aus");
				}
				else if (el is FSS) {
					spString += "Fss" + el.ID + ":" + (befehl.SchaltZustand ? "An" : "Aus");
				}
			}
			return spString;
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
        }

		

		/// <summary>
		/// prüft die Verfügbarkeit der FS (Adr.-Block., Zielsignal-Belegung, Zug-Länge)
		/// soll AdressenFrei ersetzen
		/// </summary>
		/// <returns></returns>
		public bool Verfuegbarkeit()//Verfuegbarkeit()
		{
			foreach(FahrstrasseN fs in _fahrStrassen) {
                if (!fs.Verfuegbarkeit()) {
                    return false;
                }
            }
			return true;
		}
        
		public bool AusgangToggeln(FahrstrassenSignalTyp signalTyp, bool verlaengern) {
			if (Verfuegbarkeit() || IsAktiv) {
				return true;
			}
			return false;
		}
        
	}


}
