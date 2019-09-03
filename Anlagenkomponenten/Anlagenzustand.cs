using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MoBaSteuerung.Anlagenkomponenten {
    public class BoolArray {
        private List<UInt16> _value = new List<UInt16>();

        public List<UInt16> Value {
            get {
                return _value;
            }

            set {
                _value = value;
            }
        }

        private int GetInternalIndex(int index) {
            return index / 16;
        }

        public BoolArray() {

        }

        public BoolArray(int anzahlWord) {
            _value.InsertRange(0, new UInt16[anzahlWord]);

        }

        public void Toggle(int adresse, int bit) {
            _value[adresse] ^= (UInt16)(1 << bit);
        }

        //public void Toggle(int index) {
        //    int intIndex = GetInternalIndex(index);
        //    _value[intIndex] = (UInt16)(_value[intIndex] ^ (1 << (index % 16)));
        //}


        public bool this[int adresse, int bit] {
            get {
                return (_value[adresse] & (1 << bit)) != 0;
            }
            set {
                if (value) {
                    //left-shift 1, then bitwise OR
                    _value[adresse] |= (UInt16)(1 << bit);
                }
                else {
                    //left-shift 1, then take complement, then bitwise AND
                    _value[adresse] &= (UInt16)(~(1 << bit));
                }
            }
        }

        public UInt16 this[int index] {
            get {
                return _value[index];
            }
            set {
                _value[index] = value;
            }
        }

        public int Length {
            get {
                return _value.Count;
            }
        }

    }

    public class Arduino {
        private bool _istVirtuell = false;
        private int _nr = 0;
        private volatile BoolArray _ausg = new BoolArray(3);
        private volatile BoolArray _lockedAusg = new BoolArray(3);
        private volatile BoolArray _eing = new BoolArray(2);

        public int Nr {
            get {
                return _nr;
            }

            set {
                _nr = value;
            }
        }

        public BoolArray Ausgaenge {
            get {
                return _ausg;
            }

            set {
                _ausg = value;
            }
        }

        public BoolArray Rueckmeldung {
            get {
                return _eing;
            }

            set {
                _eing = value;
            }
        }

        public BoolArray LockedAusg {
            get {
                return _lockedAusg;
            }

            set {
                _lockedAusg = value;
            }
        }

        public bool IstVirtuell {
            get {
                return _istVirtuell;
            }

            set {
                _istVirtuell = value;
            }
        }
    }

    public class WeichenStrasse {
        private int _id = -1;
        //private int _fahrbereich = -1;

        public int Id {
            get {
                return _id;
            }
            set {
                _id = value;
            }
        }
    }


    public class Anlagenzustand {
        private List<Arduino> _arduinoListe = new List<Arduino>();
        private List<WeichenStrasse> _weichenFahrstrassen = new List<WeichenStrasse>();

        public List<Arduino> ArduinoListe {
            get {
                return _arduinoListe;
            }
            set {
                _arduinoListe = value;
            }
        }

        public List<WeichenStrasse> AktiveFahrstrassen {
            get {
                return _weichenFahrstrassen;
            }
            set {
                _weichenFahrstrassen = value;
            }
        }


        public bool FahrstrasseSchalten(int id) {
            foreach(WeichenStrasse wS in _weichenFahrstrassen) {
                if(wS.Id == id) {
                    _weichenFahrstrassen.Remove(wS);
                    return false;
                }
            }
            WeichenStrasse weStr = new WeichenStrasse();
            weStr.Id = id;
            _weichenFahrstrassen.Add(weStr);
            return true;
        }

        public Arduino GetArduino(int arduinoNr) {
            foreach (Arduino ard in _arduinoListe)
                if (ard.Nr == arduinoNr)
                    return ard;
            return null;
        }

        public bool IsFahrstrasseAktiv(int id) {
            foreach(WeichenStrasse wS in _weichenFahrstrassen) {
                if (wS.Id == id)
                    return true;
            }
            return false;
        }

        public byte[] GetBefehl(int arduinoNr, int adressenNr) {
            Arduino ard = GetArduino(arduinoNr);
            if (ard != null) {
                byte[] daten = BitConverter.GetBytes(ard.Ausgaenge[adressenNr]);
                byte[] befehl = new byte[5];
                befehl[0] = (byte)arduinoNr;
                befehl[1] = (byte)(40 + adressenNr);
                befehl[2] = daten[0];
                befehl[3] = daten[1];
                befehl[4] = (byte)((befehl[0] + befehl[1] + befehl[2] + befehl[3]) % 256);
                return befehl;
            }
            return null;
        }
    }
}
 