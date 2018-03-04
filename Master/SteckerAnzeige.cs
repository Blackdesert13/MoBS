using MoBaSteuerung;
using MoBaSteuerung.Elemente;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ModellBahnSteuerung
{
    public partial class FormStecker : Form
    {
        private Model _model;
        private string _steckerName;
        private int _steckerTyp;//0-> 30pol. a1-c10
                                //1-> 25pol. 1-25
        private string[] _steckerTypBezeichnung = new string[3];
        private string[] _listeAnschluss;
        private string[] _listePCBez;
        private string[] _listeAnlagenBez;
        private string[] _listeRelaisRM;
        public FormStecker()
        {
            InitializeComponent();
            _steckerTypBezeichnung[0] = "30pol. a1-c10";
            _steckerTypBezeichnung[1] = "D-Sub 25pol. 1-25";
            _steckerTypBezeichnung[2] = "D-Sub 37pol. 1-37";
        }

        public Model Model { set { _model = value; } }

        private void listenLaenge(int ListenLaenge)
        {
            
        }

        private void steckerEinreinreihig(int Anschluesse)
        {
            for(int i = 0; i < Anschluesse; i++)
            {
                string[] zeile = { Convert.ToString(i + 1) };
                dataGridView1.Rows.Add(zeile);
            }
        }

        private void steckerMehrreihig(int Reihen, int Anschluesse)
        {
            int laenge = Reihen * Anschluesse;
            dataGridView1.Rows.Clear();
            _listeAnschluss = new string[laenge];
            _listePCBez = new string[laenge];
            _listeAnlagenBez = new string[laenge];
            _listeRelaisRM = new string[laenge];
            for (int i = 0; i<Anschluesse; i++)
            {
                if (Reihen == 1) _listeAnschluss[i] = Convert.ToString(i + 1);
                else
                {
                    if (Reihen > 0) _listeAnschluss[i] = "a" + Convert.ToString(i + 1);
                    if (Reihen > 1) _listeAnschluss[i + Anschluesse] = "b" + Convert.ToString(i + 1);
                    if (Reihen > 2) _listeAnschluss[i + Anschluesse * 2] = "c" + Convert.ToString(i + 1);
                    if (Reihen > 3) _listeAnschluss[i + Anschluesse * 3] = "d" + Convert.ToString(i + 1);
                }
            }
            foreach(string an in _listeAnschluss)
            {
                string[] zeile = { an };
                dataGridView1.Rows.Add(zeile);
            }
        }

        private void steckerTyp0(int TypNummer)
        {
            labelTyp.Text = _steckerTypBezeichnung[TypNummer];
            switch (TypNummer)
            {
                case 0:
                    //listenLaenge(30);
                    steckerMehrreihig(3, 10);
                    break;
                case 1:
                    //listenLaenge(25);
                    steckerMehrreihig(1,25);
                    break;
                case 2:
                   // listenLaenge(37);
                    steckerMehrreihig(1,37);
                    break;
            }
        }


        private void elementEintragen(AnlagenElement Element, string Anschluss)
        {
            for(int i = 0; i < dataGridView1.RowCount; i++)
            {
                string z = Convert.ToString( dataGridView1[0, 1].Value);
                 if(z == Anschluss)

                   // if (string.Compare(z, Anschluss,true))
                
                {
                    dataGridView1[1, i].Value = dataGridView1[1, i].Value + " " + Element.Bezeichnung;
                }
            }
           // dataGridView1
        }

        private void buttonLaden_Click(object sender, EventArgs e)
        {
            steckerTyp0(_steckerTyp);
            listenFuellen();
            comboBox1.Items.Add("abc");
            comboBox1.Items.Add("123");

            StreamWriter steckerStreamWriter = new StreamWriter("SteckerDaten.txt", false, Encoding.UTF8);
            /*List<AnlagenElement> anlagenElemente = _model.ZeichnenElemente.Steckersuchen(textBoxStecker.Text);
            foreach(AnlagenElement a in anlagenElemente)
            {
                string[] st = a.Stecker.Split(' ');
                foreach(string an in st)
                {
                    if (an.StartsWith(textBoxStecker.Text))
                    {
                        string[] ant = an.Split('_');
                        for(int i =0; i<_listeAnschluss.Length;i++)
                        {
                            if (_listeAnschluss[i] == ant[1])
                            {
                                if (a.Ausgang.MCNr > 0)
                                {
                                    if (_listeRelaisRM[i] != null) _listeRelaisRM[i] += " ";
                                    _listeRelaisRM[i] = "Re" + a.Ausgang.SpeicherString;
                                }
                                if (a.GetType().Name == "Gleis")
                                {
                                    Gleis ag = (Gleis)a;
                                    if (ag.Eingang.MCNr > 0)
                                    {
                                        if (_listeRelaisRM[i] != null) _listeRelaisRM[i] += " ";
                                        _listeRelaisRM[i] += "RM" + ag.Eingang.SpeicherString;
                                    }
                                }
                                if (_listePCBez[i] != null) _listePCBez[i] += " ";
                                _listePCBez[i] += a.KurzBezeichnung;
                                if (_listeAnlagenBez[i] != null) _listeAnlagenBez[i] += " ";
                                _listeAnlagenBez[i] += a.Bezeichnung;
                            }
                        }
                    }
                }
            }*/
            for(int i= 0; i < _listeAnschluss.Length; i++)
            {
                if(_listeAnlagenBez!= null) dataGridView1[1, i].Value = _model.StringBereinigen( _listeAnlagenBez[i]);
                if (_listePCBez != null) dataGridView1[2, i].Value = _model.StringBereinigen(_listePCBez[i]);
                dataGridView1[3, i].Value = _model.StringBereinigen(_listeRelaisRM[i]);
            }
            steckerStreamWriter.Flush();
            steckerStreamWriter.Dispose();
        }
        private void listenFuellen()
        {
            _steckerName = textBoxStecker.Text;
            List<AnlagenElement> anlagenElemente = _model.ZeichnenElemente.Steckersuchen(_steckerName);
            foreach (AnlagenElement a in anlagenElemente)
            {
                string[] st = a.Stecker.Split(' ');
                foreach (string an in st)
                {
                    if (an.StartsWith(textBoxStecker.Text))
                    {
                        string[] ant = an.Split('_');
                        for (int i = 0; i < _listeAnschluss.Length; i++)
                        {
                            if (_listeAnschluss[i] == ant[1])
                            {
                                if (a.Ausgang.MCNr > 0)
                                {
                                    if (_listeRelaisRM[i] != null) _listeRelaisRM[i] += " ";
                                    _listeRelaisRM[i] = "Re" + a.Ausgang.SpeicherString;
                                }
                                if (a.GetType().Name == "Gleis")
                                {
                                    Gleis ag = (Gleis)a;
                                    if (ag.Eingang.MCNr > 0)
                                    {
                                        if (_listeRelaisRM[i] != null) _listeRelaisRM[i] += " ";
                                        _listeRelaisRM[i] += "RM" + ag.Eingang.SpeicherString;
                                    }
                                }
                                if (_listePCBez[i] != null) _listePCBez[i] += " ";
                                _listePCBez[i] += a.KurzBezeichnung;
                                if (_listeAnlagenBez[i] != null) _listeAnlagenBez[i] += " ";
                                _listeAnlagenBez[i] += a.Bezeichnung;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < _listeAnschluss.Length; i++)
            {
                _listeAnlagenBez[i] = _model.StringBereinigen(_listeAnlagenBez[i]);
                _listePCBez[i] = _model.StringBereinigen(_listePCBez[i]);
                _listeRelaisRM[i] = _model.StringBereinigen(_listeRelaisRM[i]);
            }
        }

        private void buttonTyp_Click(object sender, EventArgs e)
        {
            _steckerTyp++;
            if (_steckerTyp >= _steckerTypBezeichnung.Length) _steckerTyp = 0;
            steckerTyp0(_steckerTyp);
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            _steckerName = textBoxStecker.Text;
            string[] steckerName = _steckerName.Split('/');
            string dateiName = "SteckerDaten_" + steckerName[0];
            for (int i = 1; i<steckerName.Length; i++) { dateiName += "-" + steckerName[i]; }
            //dateiName += ".txt";
            //string dateiName = "SteckerDaten_" + _steckerName + ".txt";//"SteckerDaten.txt"
            StreamWriter steckerStreamWriter = new StreamWriter(dateiName+".txt", false, Encoding.UTF8);
            for(int i=0; i < _listeAnschluss.Length; i++)
            {
                steckerStreamWriter.WriteLine(_listeAnschluss[i]
                    + "\t" + _listeAnlagenBez[i]
                    + "\t" + _listePCBez[i]
                    +" \t" + _listeRelaisRM[i]);
            }
            steckerStreamWriter.Flush();
            steckerStreamWriter.Dispose();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
