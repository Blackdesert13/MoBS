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
  public partial class FrmWeiche : UserControl
  {
        private bool offen;
        private Model _model;
        private MoBaSteuerung.Elemente.Weiche _weiche;
        
        


        public FrmWeiche()
        {
            InitializeComponent();
            this.offen = true;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public FrmWeiche(Model Model) : this()
        {
            _model = Model;
        }
        public Model Model  { set { _model = value; } }
        public MoBaSteuerung.Elemente.Weiche AktuelleWeiche
        {
            set
            {
                _weiche = value;
                weichenDatenLaden();
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

        private void buttonAuswaehlen_Click(object sender, EventArgs e)
        {
            int id;
            if (int.TryParse(textBoxWeiche.Text, out id)) weicheLaden(id);
            else
            {
                textBoxAusgang.Text = "";
                textBoxGrundstellung.Text = "";
            }
        }

        private void weicheLaden(int ID)
        {
            _weiche = _model.ZeichnenElemente.WeicheElemente.Element(ID);
            _model.BearbeitenSelektieren(_weiche);
            weichenDatenLaden();
        }
        private void weichenDatenLaden() { 

            if (_weiche != null)
            {
                textBoxWeiche.Text = Convert.ToString(_weiche.ID);
                textBoxAusgang.Text = _weiche.Ausgang.SpeicherString;
                textBoxGrundstellung.Text = Convert.ToString(_weiche.Grundstellung);
                textBoxBezeichnung.Text = _weiche.Bezeichnung;
                textBoxStecker.Text = _weiche.Stecker;
            }
            else
            { 
                textBoxAusgang.Text = "";
                textBoxGrundstellung.Text = "";
                textBoxBezeichnung.Text = "";
                textBoxStecker.Text = "";      
            }
        }

        private void weicheSpeichern()
        {
            if (_weiche != null)
            {
                int id;
                if (textBoxWeiche.Text == "")
                {
                    id = _model.ZeichnenElemente.WeicheElemente.FreieID();
                    if (_weiche.ID != id)
                    {
                        _weiche.ID = id;
                        textBoxWeiche.Text = Convert.ToString( _weiche.ID);
                    }//textBoxWeiche.
                }
                if (int.TryParse(textBoxWeiche.Text, out id))
                {
                    if ((_model.ZeichnenElemente.WeicheElemente.IDFrei(id))||(_weiche.ID==id))
                    {
                        _weiche.ID = id;
                        _weiche.Ausgang.SpeicherString = textBoxAusgang.Text;
                        _weiche.Bezeichnung = textBoxBezeichnung.Text;
                        _weiche.Stecker = textBoxStecker.Text;
                        weicheLaden(id);
                    }
                }
            }
        }

        private void buttonSpeichern_Click(object sender, EventArgs e)
        {
            weicheSpeichern();
        }

        private void textBoxGrundstellung_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void textBoxGrundstellung_DoubleClick(object sender, EventArgs e)
        {
            _weiche.Grundstellung = !_weiche.Grundstellung;
        }

        private void buttonKoppelung_Click(object sender, EventArgs e)
        {
            if (_weiche != null)
            {
                FrmBefehlsliste frmBefehlsliste = new FrmBefehlsliste(_weiche.Koppelung, _weiche.KurzBezeichnung + "-Koppelung");
                if (frmBefehlsliste.ShowDialog(this) == DialogResult.OK)
                {
                    // string[][] t =  frm.auslesen();
                }
                else
                {
                }

            }
        }
        /// <summary>
        /// propertygrid öffnen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (_weiche != null)
            {
                frmProperties frm = new frmProperties(_weiche);
                if (frm.ShowDialog(this) == DialogResult.OK) ;
            }
        }
    }
}
