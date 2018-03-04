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
    public partial class FrmGleis : UserControl
    {
        private bool offen;
        private Model _model;
        private Gleis _gleis;
        //private MoBaSteuerung.Elemente.Weiche _weiche;
        /// <summary>
        /// 
        /// </summary>
        public FrmGleis()
        {
          InitializeComponent();
          this.offen = true;
        }

        public Model Model { set { _model = value; } }

        public Gleis AktuellesGleis
        {
            set
            {
                _gleis = value;
                gleisDatenLaden();
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
            this.Height = 112;
            this.offen = true;
          }
        }

        private void ButtonLaden_Click(object sender, EventArgs e)
        {
            int id;
            if (int.TryParse(textBoxGleis.Text, out id))
                gleisLaden(id);
            else
            {
                textBoxAusgang.Text = "";
            }
        }

        private void gleisDatenLaden() {
            //_gleis = gleis;
            if (_gleis != null) {
                textBoxGleis.Text = Convert.ToString( _gleis.ID);
                textBoxAusgang.Text = _gleis.Ausgang.SpeicherString;
                textBoxRM.Text = _gleis.Eingang.SpeicherString;
                TextBoxBezeichnung.Text = _gleis.Bezeichnung;
                if (_gleis.ReglerNr != 0) textBoxRegler.Text = Convert.ToString(_gleis.ReglerNr);
                else textBoxRegler.Text = "";
                textBoxStartKnoten.Text = Convert.ToString(_gleis.StartKn.ID);
                textBoxEndKnoten.Text = Convert.ToString(_gleis.EndKn.ID);
                textBoxStecker.Text = _gleis.Stecker;
            }
            else {
                textBoxGleis.Text = "";
                textBoxAusgang.Text = "";
                textBoxRM.Text = "";
                textBoxRegler.Text = "";
                TextBoxBezeichnung.Text = "";
                textBoxStartKnoten.Text = "";
                textBoxEndKnoten.Text = "";
            }
        }

        private void gleisLaden(int ID)
        {
            _gleis =_model.ZeichnenElemente.GleisElemente.Element(ID);
            _model.BearbeitenSelektieren(_gleis);
            gleisDatenLaden();
        }

        private void gleisSpeichern()
        {
            if(_gleis != null)
            {
                int id;
                if (int.TryParse(textBoxGleis.Text, out id))
                    if (_model.ZeichnenElemente.WeicheElemente.IDFrei(id)) _gleis.ID = id;
                if (int.TryParse(textBoxRegler.Text, out id)) _gleis.ReglerNr = id;
                else
                {
                    _gleis.ReglerNr = 0;
                    textBoxRegler.Text = "";
                }  
                //  _gleis.ReglerNr = Convert
                _gleis.Bezeichnung = TextBoxBezeichnung.Text;
                _gleis.Ausgang.SpeicherString = textBoxAusgang.Text;
                _gleis.Eingang.SpeicherString = textBoxRM.Text;
                _gleis.Stecker                = textBoxStecker.Text;
            }
        }

        private void buttonSpeichern_Click(object sender, EventArgs e)
        {
            gleisSpeichern();
        }

        private void textBoxGleis_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBoxAusgang_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
