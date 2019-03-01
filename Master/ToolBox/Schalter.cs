using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MoBaSteuerung;

namespace ModellBahnSteuerung.ToolBox
{
  /// <summary>
  /// 
  /// </summary>
    public partial class FrmSchalter : UserControl
    {
        private bool offen;
        private Model _model;
        private MoBaSteuerung.Elemente.Schalter _schalter;
        /// <summary>
        /// 
        /// </summary>
        public FrmSchalter()
        {
            InitializeComponent();
            this.offen = true;
        }

        public Model Model { set { _model = value; } }

        public MoBaSteuerung.Elemente.Schalter AktuellerSchalter
        {
            set
            {
                _schalter = value;
                schalterDatenLaden();
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
            this.Height = 92;
            this.offen = true;
          }
        }
        private void schalterDatenLaden()
        {
            throw new NotImplementedException();
        }

        private void buttonKoppelung_Click(object sender, EventArgs e)
        {
            if (_schalter != null)
            {
                FrmBefehlsliste frmBefehlsliste = new FrmBefehlsliste(_schalter.Koppelung, _schalter.KurzBezeichnung + "-Koppelung");
                if (frmBefehlsliste.ShowDialog(this) == DialogResult.OK)
                {
                    // string[][] t =  frm.auslesen();
                }
                else
                {
                }

            }
        }
    }
}
