using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MoBaSteuerung;
using MoBaSteuerung.Elemente;
using System.IO;
using MoBaSteuerung.Anlagenkomponenten.MCSpeicher;

namespace ModellBahnSteuerung.ToolBox
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Relaise : UserControl
    {
        private bool offen;
        private Model _model;
        private string[] _kurzBezeichnungArray = new string[16];
        private string[] _anlagenBezeichnungArray = new string[16];
        private int _RelaisBeschriftung = 0;//0 -> Relais-Nr; 1 -> PC-Adresse; 2 -> Arduino-Ausgang
        private int _textBoxBeschriftung = 0;//0-> PC-Bezeichnung; 1 -> Anlagen-Bezeichnung                                   
        private List<AnlagenElement> _anlagenElemente;
        private int _arduino = 0;
        private int _platine = -1;


        public Relaise()
        {
            InitializeComponent();
            this.offen = true;
        }

        public Relaise(Model Model) : this()
        {
            _model = Model;
        }
        public Model Model { set { _model = value; } }

        private void PictureBoxMenü_MouseEnter(object sender, EventArgs e)
        {
            if (this.offen)
            {
                this.PictureBoxMenü.Image = global::ModellBahnSteuerung.Properties.Resources.OpenMouseEnter;
            }
            else
            {
                this.PictureBoxMenü.Image = global::ModellBahnSteuerung.Properties.Resources.CloseMouseEnter;
            }
        }

        private void PictureBoxMenü_MouseLeave(object sender, EventArgs e)
        {
            if (this.offen)
            {
                this.PictureBoxMenü.Image = global::ModellBahnSteuerung.Properties.Resources.Open;
            }
            else
            {
                this.PictureBoxMenü.Image = global::ModellBahnSteuerung.Properties.Resources.Close;
            }
        }

        private void PictureBoxMenü_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.offen)
            {
                this.PictureBoxMenü.Image = global::ModellBahnSteuerung.Properties.Resources.OpenMouseDown;
            }
            else
            {
                this.PictureBoxMenü.Image = global::ModellBahnSteuerung.Properties.Resources.CloseMouseDown;
            }
        }

        private void PictureBoxMenü_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.offen)
            {
                this.PictureBoxMenü.Image = global::ModellBahnSteuerung.Properties.Resources.OpenMouseEnter;
            }
            else
            {
                this.PictureBoxMenü.Image = global::ModellBahnSteuerung.Properties.Resources.CloseMouseEnter;
            }
        }

        private void PictureBoxMenü_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
            if (this.offen)
            {
                this.Height = 26;
                this.offen = false;
            }
            else
            {
                this.Height = 237;
                this.offen = true;
            }
        }

        private void Aktualisieren_Click(object sender, EventArgs e)
        {
            textBoxBezeichnung.Text = "";
            for (int i = 0; i < _kurzBezeichnungArray.Length; i++)
            {
                _kurzBezeichnungArray[i] = "";
                _anlagenBezeichnungArray[i] = "";
            }
            if ((int.TryParse(textBoxRelaise.Text, out _platine)) & (int.TryParse(textBoxArduino.Text, out _arduino)))
            {
                textBoxBezeichnung.Text =  _model.ZeichnenElemente.ListeMCSpeicher.Element(_arduino).Bezeichnung;// (_arduino);
                _anlagenElemente = _model.ZeichnenElemente.RelaisAdresseSuchen(_arduino, _platine);
                foreach (AnlagenElement a in _anlagenElemente)
                {
                    int b = a.Ausgang.BitNr;
                    _kurzBezeichnungArray[b] = _kurzBezeichnungArray[b] + a.KurzBezeichnung + " ";
                    string[] anlBez = _anlagenBezeichnungArray[b].Split(' ');
                    bool neu = true;
                    foreach (string elem in anlBez)
                    { if (a.Bezeichnung == elem) neu = false; }
                    if (neu)
                    {
                        if (_anlagenBezeichnungArray[b] != "") _anlagenBezeichnungArray[b] = _anlagenBezeichnungArray[b] + " ";
                        _anlagenBezeichnungArray[b] = _anlagenBezeichnungArray[b] + a.Bezeichnung;
                    }
                }
            }
            textBoxFuellen();
        }

        private void textBoxFuellen()
        {
            switch (_textBoxBeschriftung)
            {
                case 0:
                    textBoxFuellenKurzBezeichnung();
                    break;
                case 1:
                    textBoxFuellenAnlagenBezeichnung();
                    break;
            }
        }

        private void textBoxFuellenAnlagenBezeichnung()
        {
            buttonBezeichung.Text = "Anlagenbezeichnung";
            switch (_platine)
            {
                case 1:
                case 2:
                    textBoxBit0.Text = _anlagenBezeichnungArray[1]; //_adressenArray
                    textBoxBit1.Text = _anlagenBezeichnungArray[0];
                    textBoxBit2.Text = _anlagenBezeichnungArray[3];
                    textBoxBit3.Text = _anlagenBezeichnungArray[2];
                    textBoxBit4.Text = _anlagenBezeichnungArray[5];
                    textBoxBit5.Text = _anlagenBezeichnungArray[4];
                    textBoxBit6.Text = _anlagenBezeichnungArray[7];
                    textBoxBit7.Text = _anlagenBezeichnungArray[6];
                    textBoxBit8.Text = _anlagenBezeichnungArray[14];
                    textBoxBit9.Text = _anlagenBezeichnungArray[15];
                    textBoxBit10.Text = _anlagenBezeichnungArray[12];
                    textBoxBit11.Text = _anlagenBezeichnungArray[13];
                    textBoxBit12.Text = _anlagenBezeichnungArray[10];
                    textBoxBit13.Text = _anlagenBezeichnungArray[11];
                    textBoxBit14.Text = _anlagenBezeichnungArray[8];
                    textBoxBit15.Text = _anlagenBezeichnungArray[9];
                    break;
                case 0:
                    textBoxBit0.Text = _anlagenBezeichnungArray[15];
                    textBoxBit1.Text = _anlagenBezeichnungArray[14];
                    textBoxBit2.Text = _anlagenBezeichnungArray[13];
                    textBoxBit3.Text = _anlagenBezeichnungArray[12];
                    textBoxBit4.Text = _anlagenBezeichnungArray[0];
                    textBoxBit5.Text = _anlagenBezeichnungArray[1];
                    textBoxBit6.Text = _anlagenBezeichnungArray[2];
                    textBoxBit7.Text = _anlagenBezeichnungArray[3];
                    textBoxBit8.Text = _anlagenBezeichnungArray[11];
                    textBoxBit9.Text = _anlagenBezeichnungArray[10];
                    textBoxBit10.Text = _anlagenBezeichnungArray[9];
                    textBoxBit11.Text = _anlagenBezeichnungArray[8];
                    textBoxBit12.Text = _anlagenBezeichnungArray[7];
                    textBoxBit13.Text = _anlagenBezeichnungArray[6];
                    textBoxBit14.Text = _anlagenBezeichnungArray[5];
                    textBoxBit15.Text = _anlagenBezeichnungArray[4];
                    break;
            }
        }

        private void textBoxFuellenKurzBezeichnung()
        {
            buttonBezeichung.Text = "PC-ID";
            switch (_platine)
            {
                case 1:
                case 2:
                    textBoxBit0.Text = _kurzBezeichnungArray[1]; //_adressenArray
                    textBoxBit1.Text = _kurzBezeichnungArray[0];
                    textBoxBit2.Text = _kurzBezeichnungArray[3];
                    textBoxBit3.Text = _kurzBezeichnungArray[2];
                    textBoxBit4.Text = _kurzBezeichnungArray[5];
                    textBoxBit5.Text = _kurzBezeichnungArray[4];
                    textBoxBit6.Text = _kurzBezeichnungArray[7];
                    textBoxBit7.Text = _kurzBezeichnungArray[6];
                    textBoxBit8.Text = _kurzBezeichnungArray[14];
                    textBoxBit9.Text = _kurzBezeichnungArray[15];
                    textBoxBit10.Text = _kurzBezeichnungArray[12];
                    textBoxBit11.Text = _kurzBezeichnungArray[13];
                    textBoxBit12.Text = _kurzBezeichnungArray[10];
                    textBoxBit13.Text = _kurzBezeichnungArray[11];
                    textBoxBit14.Text = _kurzBezeichnungArray[8];
                    textBoxBit15.Text = _kurzBezeichnungArray[9];
                    break;
                case 0:
                    textBoxBit0.Text = _kurzBezeichnungArray[15];
                    textBoxBit1.Text = _kurzBezeichnungArray[14];
                    textBoxBit2.Text = _kurzBezeichnungArray[13];
                    textBoxBit3.Text = _kurzBezeichnungArray[12];
                    textBoxBit4.Text = _kurzBezeichnungArray[0];
                    textBoxBit5.Text = _kurzBezeichnungArray[1];
                    textBoxBit6.Text = _kurzBezeichnungArray[2];
                    textBoxBit7.Text = _kurzBezeichnungArray[3];
                    textBoxBit8.Text = _kurzBezeichnungArray[11];
                    textBoxBit9.Text = _kurzBezeichnungArray[10];
                    textBoxBit10.Text = _kurzBezeichnungArray[9];
                    textBoxBit11.Text = _kurzBezeichnungArray[8];
                    textBoxBit12.Text = _kurzBezeichnungArray[7];
                    textBoxBit13.Text = _kurzBezeichnungArray[6];
                    textBoxBit14.Text = _kurzBezeichnungArray[5];
                    textBoxBit15.Text = _kurzBezeichnungArray[4];
                    break;
            }
        }

        private void relaisIDAnzeigen()
        {
            labelR0.Text = "ID1"; labelR1.Text = "ID2"; labelR2.Text = "ID3"; labelR3.Text = "ID4";
            labelR4.Text = "ID5"; labelR5.Text = "ID6"; labelR6.Text = "ID7"; labelR7.Text = "ID8";
            labelR8.Text = "ID16"; labelR9.Text = "ID15"; labelR10.Text = "ID14"; labelR11.Text = "ID13";
            labelR12.Text = "ID12"; labelR13.Text = "ID11"; labelR14.Text = "ID10"; labelR15.Text = "ID9";
        }
        private void adressenAnzeigen()
        {
            string adr = _arduino + " " + _platine + " ";
            switch (_platine)
            {
                case 0:
                    labelR0.Text = adr + "15"; labelR1.Text = adr + "14"; labelR2.Text = adr + "13"; labelR3.Text = adr + "12";
                    labelR4.Text = adr + "0"; labelR5.Text = adr + "1"; labelR6.Text = adr + "2"; labelR7.Text = adr + "3";
                    labelR8.Text = adr + "11"; labelR9.Text = adr + "10"; labelR10.Text = adr + "9"; labelR11.Text = adr + "8";
                    labelR12.Text = adr + "7"; labelR13.Text = adr + "6"; labelR14.Text = adr + "5"; labelR15.Text = adr + "4";
                    break;
                case 1:
                case 2:
                    labelR0.Text = adr + "1"; labelR1.Text = adr + "0"; labelR2.Text = adr + "3"; labelR3.Text = adr + "2";
                    labelR4.Text = adr + "5"; labelR5.Text = adr + "4"; labelR6.Text = adr + "7"; labelR7.Text = adr + "6";
                    labelR8.Text = adr + "14"; labelR9.Text = adr + "15"; labelR10.Text = adr + "12"; labelR11.Text = adr + "13";
                    labelR12.Text = adr + "10"; labelR13.Text = adr + "11"; labelR14.Text = adr + "8"; labelR15.Text = adr + "9";
                    break;
            }
        }
        private void arduinoAusgangAnzeigen()
        {
            switch (_platine)
            {
                case 0:
                    labelR0.Text = "17"; labelR1.Text = "16"; labelR2.Text = "15"; labelR3.Text = "14";
                    labelR4.Text = "2"; labelR5.Text = "3"; labelR6.Text = "4"; labelR7.Text = "5";
                    labelR8.Text = "13"; labelR9.Text = "12"; labelR10.Text = "11"; labelR11.Text = "10";
                    labelR12.Text = "9"; labelR13.Text = "8"; labelR14.Text = "7"; labelR15.Text = "6";
                    break;
                case 1:
                    labelR0.Text = "23"; labelR1.Text = "22"; labelR2.Text = "25"; labelR3.Text = "24";
                    labelR4.Text = "27"; labelR5.Text = "26"; labelR6.Text = "29"; labelR7.Text = "28";
                    labelR8.Text = "36"; labelR9.Text = "37"; labelR10.Text = "34"; labelR11.Text = "35";
                    labelR12.Text = "32"; labelR13.Text = "33"; labelR14.Text = "30"; labelR15.Text = "31";
                    break;
                case 2:
                    labelR0.Text = "39"; labelR1.Text = "38"; labelR2.Text = "41"; labelR3.Text = "40";
                    labelR4.Text = "43"; labelR5.Text = "42"; labelR6.Text = "45"; labelR7.Text = "44";
                    labelR8.Text = "52"; labelR9.Text = "53"; labelR10.Text = "50"; labelR11.Text = "51";
                    labelR12.Text = "48"; labelR13.Text = "49"; labelR14.Text = "46"; labelR15.Text = "47";
                    break;
            }
        }

        private void buttonRelaisbeschriftung_Click(object sender, EventArgs e)
        {
            _RelaisBeschriftung++;
            if (_RelaisBeschriftung > 2) _RelaisBeschriftung = 0;
            switch (_RelaisBeschriftung)
            {
                case 0:
                    buttonRelaisbeschriftung.Text = "Relais-Nr";
                    relaisIDAnzeigen();
                    break;
                case 1:
                    buttonRelaisbeschriftung.Text = "PC-Adresse";
                    adressenAnzeigen();
                    break;
                case 2:
                    buttonRelaisbeschriftung.Text = "Arduino-Ausgang";
                    arduinoAusgangAnzeigen();
                    break;
            }

        }

        private void buttonBezeichung_Click(object sender, EventArgs e)
        {
            _textBoxBeschriftung++;
            if (_textBoxBeschriftung > 1) _textBoxBeschriftung = 0;
            textBoxFuellen();
        }

        private string StringBereinigen(string EingangsString)
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

        private void buttonArduinoExport_Click(object sender, EventArgs e)
        {
            StreamWriter arduinoStreamWriter = new StreamWriter("ArduinoDaten.txt", false, Encoding.UTF8);
            foreach (MCSpeicher ard in _model.ZeichnenElemente.ListeMCSpeicher.Elemente)
            {
                arduinoStreamWriter.WriteLine("Arduino "+ ard.ID);
                for (int i = 0; i < 3; i++)
                {//Relaisplatine
                    arduinoStreamWriter.WriteLine("Relais-Platine " + ard.ID + "-" + i);
                    string[] kurzBezeichnungArray = new string[17];
                    kurzBezeichnungArray[0] = "PC-Bezeichnung";
                    string[] anlagenBezeichnungArray = new string[17];
                    anlagenBezeichnungArray[0] = "Anlagen-Bezeichnung";
                    string[] steckerArray = new string[17];
                    steckerArray[0] = "Stecker";
                    string[] rmArray = new string[17];
                    rmArray[0] = "Rückmeldung";
                    string[] reglerArray = new string[17];
                    reglerArray[0] = "Regler";
                    List<AnlagenElement> anlagenElemente = _model.ZeichnenElemente.RelaisAdresseSuchen(ard.ID, i);
                    foreach (AnlagenElement a in anlagenElemente)
                    {
                        int b = a.Ausgang.BitNr + 1;
                        kurzBezeichnungArray[b] = kurzBezeichnungArray[b] + a.KurzBezeichnung + " ";
                        anlagenBezeichnungArray[b] = anlagenBezeichnungArray[b] + a.Bezeichnung + " ";
                        steckerArray[b] = steckerArray[b] + a.Stecker + " ";
                        if (a.GetType().Name == "Gleis")
                        {
                            Gleis g = (Gleis)a;
                            rmArray[b] = rmArray[b] + "RM" + g.Eingang.SpeicherString + " ";
                        }
                        if (a.GetType().Name == "FSS")
                        {
                            FSS f = (FSS)a;
                            int r = f.ReglerNummer1;
                            if (r > 0) rmArray[b] = rmArray[b] + "Regler" + r + " ";
                            if (r < 0)
                            {
                                FSS f1 = _model.ZeichnenElemente.FssElemente.Element(-r);
                                rmArray[b] = rmArray[b] + //f1.KurzBezeichnung + "_" + 
                                    "Re" + f1.Ausgang.SpeicherString + " ";
                            }
                            r = f.ReglerNummer2;
                            if (r > 0) reglerArray[b] = reglerArray[b] + "Regler" + r + " ";
                            if (r < 0)
                            {
                                FSS f2 = _model.ZeichnenElemente.FssElemente.Element(-r);
                                reglerArray[b] = reglerArray[b] + //f2.KurzBezeichnung + "_" + 
                                    "Re" + f2.Ausgang.SpeicherString + " ";
                            }
                        }
                    }

                    for (int a = 0; a < 17; a++) arduinoStreamWriter.Write(StringBereinigen(kurzBezeichnungArray[a]) + "\t");
                    arduinoStreamWriter.WriteLine("");
                    for (int a = 0; a < 17; a++) arduinoStreamWriter.Write(StringBereinigen(anlagenBezeichnungArray[a]) + "\t");
                    arduinoStreamWriter.WriteLine(" ");
                    for (int a = 0; a < 17; a++) arduinoStreamWriter.Write(StringBereinigen(steckerArray[a]) + "\t");
                    arduinoStreamWriter.WriteLine("");
                    for (int a = 0; a < 17; a++) arduinoStreamWriter.Write(StringBereinigen(rmArray[a]) + "\t");
                    arduinoStreamWriter.WriteLine("");
                    for (int a = 0; a < 17; a++) arduinoStreamWriter.Write(StringBereinigen(reglerArray[a]) + "\t");
                    arduinoStreamWriter.WriteLine(" ");
                }

                for (int i = 0; i < 2; i++)
                {//Rückmelde-Platinen
                    arduinoStreamWriter.WriteLine("RM-Platine " + ard.ID + "-" + i);
                    string[] kurzBezeichnungArray = new string[17];
                    kurzBezeichnungArray[0] = "PC-Bezeichnung";
                    string[] anlagenBezeichnungArray = new string[17];
                    anlagenBezeichnungArray[0] = "Anlagen-Bezeichnung";
                    string[] steckerArray = new string[17];
                    steckerArray[0] = "Stecker";
                    string[] rmArray = new string[17];
                    rmArray[0] = "Rückmeldung";
                    string[] relaisArray = new string[17];
                    relaisArray[0] = "Relais";
                    List<Gleis> gleisElemente = _model.ZeichnenElemente.RMAdresseSuchen(ard.ID, i); //_model.ZeichnenElemente.RMSuchen(ard.ID, i);
                    foreach (Gleis g in gleisElemente)
                    {
                        int b = g.Eingang.BitNr + 1;
                        kurzBezeichnungArray[b] = kurzBezeichnungArray[b] + g.KurzBezeichnung + " ";
                        anlagenBezeichnungArray[b] = anlagenBezeichnungArray[b] + g.Bezeichnung + " ";
                        steckerArray[b] = steckerArray[b] + g.Stecker + " ";
                        relaisArray[b] = relaisArray[b] + "Re"
                            + g.Ausgang.SpeicherString + " ";
                    }
                    for (int a = 0; a < kurzBezeichnungArray.Length; a++)
                        arduinoStreamWriter.Write(StringBereinigen(kurzBezeichnungArray[a]) + "\t");
                    arduinoStreamWriter.WriteLine("");
                    for (int a = 0; a < anlagenBezeichnungArray.Length; a++)
                        arduinoStreamWriter.Write(StringBereinigen(anlagenBezeichnungArray[a]) + "\t");
                    arduinoStreamWriter.WriteLine("");
                    for (int a = 0; a < steckerArray.Length; a++)
                        arduinoStreamWriter.Write(StringBereinigen(steckerArray[a]) + "\t");
                    arduinoStreamWriter.WriteLine("");
                    for (int a = 0; a < relaisArray.Length; a++)
                        arduinoStreamWriter.Write(StringBereinigen(relaisArray[a]) + "\t");
                    arduinoStreamWriter.WriteLine("");
                }
            }
            arduinoStreamWriter.Flush();
            arduinoStreamWriter.Dispose();
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            StreamWriter steckerStreamWriter = new StreamWriter("SteckerDaten.txt", false, Encoding.UTF8);
            List<AnlagenElement> anlagenElemente = _model.ZeichnenElemente.Steckersuchen("32-Vk2/3_");


            steckerStreamWriter.Flush();
            steckerStreamWriter.Dispose();
        }
    }
}
