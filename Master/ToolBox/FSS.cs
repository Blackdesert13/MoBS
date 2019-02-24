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

namespace ModellBahnSteuerung.ToolBox
{
  /// <summary>
  /// 
  /// </summary>
  public partial class FrmFSS : UserControl
  {
        private bool offen;
        private Model _model;
        private FSS _fss;  
     
 

        public FrmFSS()
        {
            InitializeComponent();
            this.offen = true;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public FrmFSS(Model Model) : this()
        {
            _model = Model;
        }
        public Model Model  { set { _model = value; } }
        public FSS AktuellerFSS
        {
            set
            {
                _fss = value;
                fssDatenLaden();
            }
        }

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
            if (this.offen)
            {
                this.Height = 26;
                this.offen = false;
            }
            else
            {
                this.Height = 115;
                this.offen = true;
            }
        }



        private void fssLaden(int ID)
        {
            _fss = _model.ZeichnenElemente.FssElemente.Element(ID);
            _model.BearbeitenSelektieren(_fss);
            fssDatenLaden();
        }

        private void  fssDatenLaden()
        {    if (_fss != null)
            {
                textBoxFSS.Text = Convert.ToString(_fss.ID);
                textBoxAusgang.Text = _fss.Ausgang.SpeicherString;
                textBoxRegler1.Text = Convert.ToString(_fss.ReglerNummer1);
                textBoxRegler2.Text = Convert.ToString(_fss.ReglerNummer2);
                textBoxBezeichnung.Text = _fss.Bezeichnung;
                textBoxStecker.Text = _fss.Stecker;
            }
            else
            {
                textBoxFSS.Text = "";
                textBoxAusgang.Text = "";
                textBoxRegler1.Text = "";
                textBoxRegler2.Text = "";
                textBoxBezeichnung.Text = "";
                textBoxStecker.Text = "";
                // TextBoxBezeichnung.Text = "";
                // textBoxStartKnoten.Text = "";
                // textBoxEndKnoten.Text = "";
            }
        }

        private void fssSpeichern()
        {
            if (_fss != null)
            {
                int id;
                if (int.TryParse(textBoxFSS.Text, out id))
                {
                    _fss.Bezeichnung = textBoxBezeichnung.Text;
                    _fss.Ausgang.SpeicherString = textBoxAusgang.Text;
                    if(int.TryParse(textBoxRegler1.Text,out id))
                    { _fss.ReglerNummer1 = id; }
                    if (int.TryParse(textBoxRegler2.Text, out id))
                    { _fss.ReglerNummer2 = id; }
                    _fss.Stecker = textBoxStecker.Text;
                    _fss.Bezeichnung = textBoxBezeichnung.Text;

                }
            }
        }

        private void buttonLaden_Click(object sender, EventArgs e)
        {
            int id;
            if (int.TryParse(textBoxFSS.Text, out id)) fssLaden(id);
            else
            {
                textBoxAusgang.Text = "";
            }
        }

        private void buttonSpeichern_Click(object sender, EventArgs e)
        {
            fssSpeichern();
        }

        private void buttonKoppelung_Click(object sender, EventArgs e)
        {
            if (_fss != null)
            {
                FrmBefehlsliste frmBefehlsliste = new FrmBefehlsliste(_fss.Koppelung, _fss.KurzBezeichnung + "-Koppelung");

            }
        }
    }
}
