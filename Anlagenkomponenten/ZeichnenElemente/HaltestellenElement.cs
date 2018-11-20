using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoBaSteuerung;
using System.ComponentModel;
using System.Diagnostics;

namespace MoBaSteuerung.Elemente
{
    

    /// <summary>
    /// zur Anzeige von Schaltzuständen in Straßenbahn-Steuerungen
    /// </summary>
    public class Haltestelle :AnlagenElement
    {
        InfoFenster infoFenster;
        string text = "";

        /// <summary>
        /// zum Speichern in der Anlagen-Datei
        /// </summary>
        public override string SpeicherString
        {
            get
            {
                return "HS"
                    + "\t" + ID
                    + "\t" + infoFenster.ID
                    + "\t";
            }
        }

        public Haltestelle(AnlagenElemente parent, Int32 zoom, AnzeigeTyp anzeigeTyp, string[] elem)
            : base (parent, Convert.ToInt32(elem[1]), zoom, anzeigeTyp)
        {
            infoFenster = parent.InfoElemente.Element(Convert.ToInt32(elem[2]));
            text = "HS " + ID;
            infoFenster.Text = text;
            Parent.HaltestellenElemente.Hinzufügen(this);
        }


/* InfoBefehl für Debug

        public void InfoBefehl(byte[] befehl)
        {
            if(befehl[1] - 100 == ID)
            {
                string txt = "HS" + ID + "-" ;
                int infos = befehl[2];
                int test;
                int zeit = befehl[3];
                for (int i = 0; i < 3; i++)
                {
                    test = infos % 2;
                    txt = txt + " " + test;//befehl[2] + "-" + befehl[3];
                    infos = infos >> 1;
                }
                test = infos % 2;
                zeit = zeit + (test * 256);
                infos = infos >> 1;
                test = infos % 2;
                zeit = zeit + (test * 512);
                infos = infos >> 1;
                test = infos % 2;
                zeit = zeit + (test * 1024);
                infos = infos >> 1;
                test = infos % 2;
                zeit = zeit + (test * 2048);
                infos = infos >> 1;
                test = infos % 2;
                zeit = zeit + (test * 4096);
                infos = infos >> 1;
                txt = txt + " " + zeit;// Convert.ToString( zeit);// befehl[3];
                //Event.OnEvent(this, new HaltestellenEventArgs(txt), HaltestellenChanged);
                infoFenster.Text = txt;
            }

        }
*/

        public void InfoBefehl(byte[] befehl)
        {
            if(befehl[1] - 100 == ID)
            {
                string txt = "HS" + ID + "-" ;
                int infos = befehl[2];
                int test;
                int zeit = befehl[3];
                int[] ausgabe = new int[3];
                for (int i = 0; i < 3; i++)
                {
                    ausgabe[i] = infos % 2;
                    //txt = txt + " " + test;//befehl[2] + "-" + befehl[3];
                    infos = infos >> 1;
                }
                if (ausgabe[0] == 1) {
                    txt += " belegt\n";
                }
                else if(ausgabe[1] == 1) {
                    txt += " blockiert\n";
                } 
                else {
                    txt += " frei\n";
                }
                
                if(ausgabe[2] == 1) {
                    txt += " Abzweig\n";
                }

                test = infos % 2;
                zeit = zeit + (test * 256);
                infos = infos >> 1;
                test = infos % 2;
                zeit = zeit + (test * 512);
                infos = infos >> 1;
                test = infos % 2;
                zeit = zeit + (test * 1024);
                infos = infos >> 1;
                test = infos % 2;
                zeit = zeit + (test * 2048);
                infos = infos >> 1;
                test = infos % 2;
                zeit = zeit + (test * 4096);
                infos = infos >> 1;
                //txt += "Abfahrt";
                txt = txt + " " + zeit;// Convert.ToString( zeit);// befehl[3];
                //Event.OnEvent(this, new HaltestellenEventArgs(txt), HaltestellenChanged);
                infoFenster.Text = txt;
            }

        }
    }
}
