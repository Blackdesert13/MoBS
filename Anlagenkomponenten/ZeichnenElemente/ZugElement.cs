using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using MoBaSteuerung.Elemente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoBaSteuerung.ZeichnenElemente
{
    /// <summary>
    /// Zug
    /// </summary>
    public class Zug : AnlagenElement
    {
        //InfoFenster infoFenster;
        private int signalNr = 0;
        //private Signal signal;
        private string lok = "";
        private string typ = "";
        private string geschwindigkeit = "";

        public Zug(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, string[] elem)
            : base (parent, Convert.ToInt32(elem[1]), zoom, anzeigeTyp)
        {
            //infoFenster = null; //parent.InfoElemente.Element(Convert.ToInt32(elem[2]));
            //signalNr = Convert.ToInt32( elem[2] );
            Parent.ZugElemente.Hinzufügen(this);
            //SignalNummer = Convert.ToInt32(elem[2]);
            lok = elem[3];
            typ = elem[4];
            Bezeichnung = elem[6];
            SignalNummer = Convert.ToInt32(elem[2]);
            // SignalNummer = signalNr;

        }

        /// <summary>
        /// zum Speichern in der Anlagen-Datei
        /// </summary>
        public override string SpeicherString
        {
            get
            {
                return "Zug"
                  + "\t" + ID
                  + "\t" + signalNr
                  + "\t" + lok
                  + "\t" + typ
                  + "\t" + geschwindigkeit
                  + "\t" + Bezeichnung ; 
            }
        }

        /// <summary>
        /// die aktuelle Position auf der Anlage
        /// </summary>
        public int SignalNummer
        {
            set
            {
                if (signalNr > 0)
                {
                    Signal signalAlt = Parent.SignalElemente.Element(signalNr);
                    signalAlt.Zug = 0;
                }
                signalNr = value;
                Signal signal = Parent.SignalElemente.Element(signalNr);
                if (signal == null)
                { signalNr = 0; }
                else { signal.Zug = ID; }
            }
            get
            {
                return signalNr;
            }
        }
        /// <summary>
        /// die Art des Zuges, zB: P, G, D, ICE
        /// </summary>
        public string ZugTyp
        {
            set { typ = value; }
            get { return typ; }
        }

        /// <summary>
        /// Triebfahrzeug
        /// </summary>
        public string Lok {
            set { lok = value; }
            get { return lok; }
        }

        /// <summary>
        /// Maximal-Geschwindigkeit
        /// </summary>
        public string Geschwindigkeit
        {
            set { geschwindigkeit = value; }
            get { return geschwindigkeit; }
        }


        /// <summary>
        /// Text für InfoFeld
        /// </summary>
        public string Anzeige {
            get
            { return ID + " " + lok + " " + Bezeichnung; } }
    }
}
