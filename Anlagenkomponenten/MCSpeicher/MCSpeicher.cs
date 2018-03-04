using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoBaSteuerung.Elemente;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using System.Drawing;

namespace MoBaSteuerung.Anlagenkomponenten.MCSpeicher {
    
    
    public class MCSpeicher : RasterAnlagenElement {

        private const int rmL = 2;//Anzahl der Adressen
        private const int rmB = 16;//Breite der RückmeldeByts
        private const int poL = 3;//Anzahl der Adressen der permanentAusgange(Länge) bzw. Leiterplatten
        private const int poB = 16;//Breite der permanentAusgangsByts

        private byte[] rmByte;
        private byte[] ausgangsByte;
        private bool[] ausgabeAktuell;// = new bool[3];
        private BitArray _outBitArray = new BitArray(0);
        private BitArray rmBitArray = new BitArray(0);//Rückmeldungs-Bits

        public override string SpeicherString {
            get {
                return "MCSpeicher"
                   + "\t" + ID
                   + "\t" + PositionRaster.X
                   + "\t" + PositionRaster.Y
                   + "\t" + Bezeichnung;
            }
        }

        public MCSpeicher(AnlagenElemente parent, int zoom, AnzeigeTyp anzeigeTyp, string[] elem)
          : base(parent, Convert.ToInt32(elem[1]), zoom, anzeigeTyp) {
            //rmBitArray.Length = rmB * rmL;
            //rmByte = new Byte[rmL * 2];

            PositionRaster = new Point(Convert.ToInt32(elem[2]), Convert.ToInt32(elem[3]));
            _outBitArray.Length = poB * poL;
            ausgabeAktuell = new bool[poL];
            ausgangsByte = new byte[poL * 2];
            Parent.ListeMCSpeicher.Hinzufügen(this);
            Arduino ard = new Arduino();
            ard.Nr = this.ID;
            if(elem.Length>4) this.Bezeichnung = elem[4];
            parent.AnlagenZustand.ArduinoListe.Add(ard);
            Berechnung();
        }

        /// <summary>
        /// liefert die Ausgangsbefele an die Arduinos als ein ByteArray 
        /// </summary>
        /// <param name="Alles">ist true wenn alle Ausgänge ausgegeben werden</param>
        /// <returns></returns>
        public List<byte> SendeByteListe(bool Alles) {

            if (Alles) {
                for (int i = 0; i < ausgabeAktuell.Length; i++) {
                    ausgabeAktuell[i] = false;
                }
            }
            _outBitArray.CopyTo(ausgangsByte, 0);
            byte nr = Convert.ToByte(ID);
            List<byte> ergebnis = new List<byte>();
            int b = 0;
            byte bef = 39;
            for (byte i = 0; i < poL; i++) //zur Zeit wird immer alles ausgegeben
            {
                bef++;
                if ((Alles) || (!ausgabeAktuell[i])) {
                    b = i * 2;
                    ergebnis.Add(nr);
                    ergebnis.Add(bef);
                    ergebnis.Add(ausgangsByte[b + 1]);
                    ergebnis.Add(ausgangsByte[b]);
                    int summe = nr + bef + ausgangsByte[b + 1] + ausgangsByte[b];
                    while (summe > 255) { summe = summe - 256; }
                    ergebnis.Add(Convert.ToByte(summe));
                }
            }
            return ergebnis;
        }

        public void AusgangSetzen(Adresse Ausgang, bool Stellung) {
            //int stelle = Ausgang.AdressenNr * poB + Ausgang.BitNr;
            /*if( Ausgang.Zustand == outBitArray.Get(stelle) )
            {
                ausgabeAktuell[Ausgang.AdressenNr]=false;
            }*/
            _outBitArray.Set(Ausgang.AdressenNr * poB + Ausgang.BitNr, Stellung);
        }

        public void AusgangToggeln(Adresse Ausgang) {
            int posBit = Ausgang.AdressenNr * poB + Ausgang.BitNr;
            _outBitArray.Set(posBit, !_outBitArray.Get(posBit));
        }

        public bool AusgangAbfragen(Adresse Ausgang) {
            return _outBitArray.Get(Ausgang.AdressenNr * poB + Ausgang.BitNr);
        }

        public bool RueckmeldungAbfragen(Adresse Eingang) {
            if (Eingang.MCNr == ID) {
                return rmBitArray.Get(Eingang.AdressenNr * rmB + Eingang.BitNr);
            }
            else { return false; }
        }

        /// <summary>
        /// erzeugt im MCSpeicher intern ein ByteArray 
        /// als Spiegelbild des gegenwärtigen Zustandes der Rückmeldung
        /// </summary>
        public void RMBytsAusBitArray() {
            rmBitArray.CopyTo(rmByte, 0);
        }

        /// <summary>
        /// setzt die Bits einer RückmeldeAdresse(in Arbeit)
        /// </summary>
        /// <param name="ByteL">das niederwertige Byte</param>
        /// <param name="ByteH">das höherwetige Byte</param>
        /// <param name="AdresseLP">Adresse der Rückmeldeplatiene</param>
        /// <returns></returns>
        public void RueckmeldungSetzen(byte ByteL, byte ByteH, byte AdresseLP) {
            int intbyte = ByteL;
            int stelle = AdresseLP * rmB;//stelle zum eintragen in das Bitarray
            for (int i = 0; i < 8; i++) {
                if (intbyte % 2 == 0) { rmBitArray.Set(stelle, false); }
                else { rmBitArray.Set(stelle, true); }
                intbyte = intbyte >> 1;
                stelle++;
            }
            intbyte = ByteH;

            RMBytsAusBitArray();
            for (int i = 0; i < 8; i++) {
                if (intbyte % 2 == 0) { rmBitArray.Set(stelle, false); }
                else { rmBitArray.Set(stelle, true); }
                intbyte = intbyte >> 1;
                stelle++;
            }
            RMBytsAusBitArray();
        }

        /// <summary>
        /// alte Methode aus Version 2013
        /// </summary>
        /// <param name="Adresse"></param>
        /// <param name="Byte"></param>
        /// <returns></returns>
        public bool RmBitsSetzenAlt(int Adresse, int Byte) {
            bool change = false;
            int byt = Byte;
            int stelle = Adresse * rmB;
            for (int i = 0; i < rmL; i++) {
                if (byt % 2 == 0)//ermittelt den Rest nach Division durch 2
                {
                    if (rmBitArray.Get(stelle)) { change = true; }
                    rmBitArray.Set(stelle, false);
                }
                else {
                    if (!rmBitArray.Get(stelle)) { change = true; }
                    rmBitArray.Set(stelle, true);
                }
                byt = byt >> 1;
                stelle++;
            }
            return change;
        }
        /*
        public void RueckmeldungsBefehl(Befehl EingangsBefehl)
        {
            Befehl eingang = EingangsBefehl;

            if (eingang.ByteAbfragen(0) == Nr)
            {
                int befehl = eingang.ByteAbfragen(1);
                if ((befehl > 9) && (befehl < 12))//Befehl 10 oder 11
                {
                    int rm = eingang.ByteAbfragen(3) * 256;
                    rm = rm + eingang.ByteAbfragen(2);
                    int stelle = (befehl - 10) * rmB;//befehl-10 --> Adresse
                    for (int i = 0; i < rmL; i++)
                    {
                        if (rm % 2 == 0)//ermittelt den Rest nach Division durch 2
                        {
                            if (rmBitArray.Get(stelle)) { aenderungRM = true; }
                            rmBitArray.Set(stelle, false);
                        }
                        else
                        {
                            if (!rmBitArray.Get(stelle)) { aenderungRM = true; }
                            rmBitArray.Set(stelle, true);
                        }
                        rm = rm >> 1;
                        stelle++;
                    }
                }
            }
        }*/
        public string StatusRM {
            get {
                string txt = "";
                foreach (bool bit in rmBitArray) { if (bit) { txt = "1" + txt; } else { txt = "0" + txt; } }
                return txt;
            }
        }
        public string StatusAusgang {
            get {
                string txt = "";
                foreach (bool bit in _outBitArray) { if (bit) { txt = "1" + txt; } else { txt = "0" + txt; } }
                return txt;
            }
        }

        
        public override void Berechnung() {
            //Mausrechteck = BerechneRechteck(PositionRaster, 2 * Zoom, 1 * Zoom);
        }

        public override void ElementZeichnen(Graphics graphics) {
            //graphics.DrawRectangles(new Pen(Color.Black, 1), new RectangleF[] { Mausrechteck });
        }

        public bool Pruefen() {
            throw new NotImplementedException();
        }
    }
}
