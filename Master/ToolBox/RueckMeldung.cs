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
    public partial class RueckMeldung: UserControl
    {
        private bool offen;
        private Model _model;
        //private TextBox[] textBoxRM0 = new TextBox[16];
        public RueckMeldung()
        {
            InitializeComponent();
            this.offen = false;
          /*  for(int i= 0; i < 16; i++)
            {
                textBoxRM0[i] = new TextBox();
            }
            //textBoxRM0 = new TextBox[16];
            int t = textBoxRM0[0].Location.X;
           // textBoxRM0[0].Location.X =10;
            t++;
            //textBoxRM0[0].Location.X = 10;*/
        }

        public RueckMeldung(Model Model) : this()
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

        private void RMladen()
        {
            dataGridView1.Rows.Clear();
            int platine; int arduino;
            if ((int.TryParse(textBoxPlatine.Text, out platine)) & (int.TryParse(textBoxArduino.Text, out arduino)))
            {
                List<Gleis> rmListeArdr0 = _model.ZeichnenElemente.RMAdresseSuchen(arduino, platine);
                string[] kurzBezeichnungsArray = new string[16];
                string[] anlagenBezeichnungsArray = new string[16];
                string[] steckerArray = new string[16];
                string[] relaisArray = new string[16];
                foreach (Gleis g in rmListeArdr0)
                {
                    int b = g.Eingang.BitNr;
                    kurzBezeichnungsArray[b] = kurzBezeichnungsArray[b] + " " + g.KurzBezeichnung;
                    anlagenBezeichnungsArray[b] = anlagenBezeichnungsArray[b] + " " + g.Bezeichnung;
                    steckerArray[b] = steckerArray[b] + " " + g.Stecker;
                    relaisArray[b] = relaisArray[b] + " " + g.Ausgang.SpeicherString;
                }

                for (int i = 0; i < 16; i++)
                {  
                    anlagenBezeichnungsArray[i] = _model.StringBereinigen(anlagenBezeichnungsArray[i]);
                    steckerArray[i] = _model.StringBereinigen(steckerArray[i]);
                    relaisArray[i] = _model.StringBereinigen(relaisArray[i]);
                    string[] zeile = {
                    Convert.ToString(i),
                    kurzBezeichnungsArray[i],
                    anlagenBezeichnungsArray[i],
                    steckerArray[i],
                    relaisArray[i]
                };
                    dataGridView1.Rows.Add(zeile);
                }
            }
        }

        private void buttonLaden_Click(object sender, EventArgs e)
        {
            RMladen();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
