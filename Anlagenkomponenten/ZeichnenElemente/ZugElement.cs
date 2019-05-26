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
        #region PrivateFelder
        private int _signalNr = 0;
        private string _lok = "";
        private string _typ = "";
        private int _geschwindigkeit = 0;
        private int _laenge;
        private DateTime _ankunftZeit = new DateTime();
        private int _digitalAdresse = 0;
        #endregion//Private Felder

        public Zug(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, string[] elem)
            : base (parent, Convert.ToInt32(elem[1]), zoom, anzeigeTyp)
        {
            //infoFenster = null; //parent.InfoElemente.Element(Convert.ToInt32(elem[2]));
            //signalNr = Convert.ToInt32( elem[2] );
            Parent.ZugElemente.Hinzufügen(this);
            _signalNr = Convert.ToInt32(elem[2]);
            _lok = elem[3];
            _typ = elem[4];
            if(elem[5]!="")_geschwindigkeit = Convert.ToInt16( elem[5]);
            Bezeichnung = elem[6];            
            if (elem.Length > 7) _laenge = Convert.ToInt32(elem[7]);
            if (elem.Length > 8) _digitalAdresse = Convert.ToInt16(elem[8]);
            if (elem.Length > 9) _ankunftZeit = Convert.ToDateTime(elem[9]);
        }
        #region Properties
        /// <summary>
        /// zum Speichern in der Anlagen-Datei
        /// </summary>
        public override string SpeicherString
        {
            get
            {//1=ID, 2=Signal, 3=Lok, 4=Typ, 5=Speed, 6=Bezeichnung, 7=Länge, 8=DigitalAdresse, 9=AnkunftsZeit
                return "Zug"
                  + "\t" + ID                //1
                  + "\t" + _signalNr         //2
                  + "\t" + _lok              //3
                  + "\t" + _typ              //4
                  + "\t" + _geschwindigkeit  //5
                  + "\t" +  Bezeichnung      //6
                  + "\t" + _laenge           //7
                  + "\t" + _digitalAdresse   //8
                  + "\t" + _ankunftZeit      //9
                  ;        
            }
        }

        /// <summary>
        /// die aktuelle Position auf der Anlage
        /// </summary>
        public int SignalNummer
        {
            set
            {
                if (_signalNr > 0)
                {
                    Signal signalAlt = Parent.SignalElemente.Element(_signalNr);
                    signalAlt.ZugNr = 0;
                }
                _signalNr = value;
                Signal signal = Parent.SignalElemente.Element(_signalNr);
                if (signal == null)
                { _signalNr = 0; }
                else { signal.ZugNr = ID; }
            }
            get
            {
                return _signalNr;
            }
        }
       
        /// <summary>
        /// Zuglänge
        /// </summary>
        public int Laenge
        {
            set { _laenge = value; }
            get { return _laenge; }
        }
        
        /// <summary>
        /// die Art des Zuges, zB: P, G, D, ICE
        /// </summary>
        public string ZugTyp
        {
            set { _typ = value; }
            get { return _typ; }
        }

        /// <summary>
        /// Triebfahrzeug
        /// </summary>
        public string Lok {
            set { _lok = value; }
            get { return _lok; }
        }

        /// <summary>
        /// Maximal-Geschwindigkeit
        /// </summary>
        public Int32 Geschwindigkeit
        {
            set { _geschwindigkeit = value; }
            get { return _geschwindigkeit; }
        }
       
        /// <summary>
        /// Ankunfts-Zeit am Signal
        /// </summary>
        public DateTime AnkunftsZeit
        {
            get {  return _ankunftZeit; }
            set { _ankunftZeit = value; }
        }

        public int DigitalAdresse
        {
            get { return _digitalAdresse;  }
            set { _digitalAdresse = value; }
        }

        /// <summary>
        /// Text für InfoFeld
        /// </summary>
        public string Anzeige {
            get
            { return ID + " " + _lok + " " + Bezeichnung; } }
        #endregion //Properties
    }
}
