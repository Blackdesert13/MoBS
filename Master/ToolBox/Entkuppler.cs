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
    public partial class FrmEntkuppler : UserControl
    {
        private bool offen;
        private Model _model;
        private Entkuppler _entkuppler;
        /// <summary>
        /// 
        /// </summary>
        public FrmEntkuppler()
        {
            InitializeComponent();
            this.offen = true;
        }

        public Model Model { set { _model = value; } }

        public Entkuppler AktuellerEntkuppler { set { _entkuppler = value; entkupplerDatenLaden(); } }

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
            this.Height = 93;
            this.offen = true;
            }
        }

        private void entkupplerDatenLaden()
        {
            textBoxEntkuppler.Text = Convert.ToString( _entkuppler.ID);
            textBoxAdresse.Text = _entkuppler.Ausgang.SpeicherString;
            textBoxBezeichnung.Text = _entkuppler.Bezeichnung;
            textBoxStecker.Text = _entkuppler.Stecker;          
        }

        //private void entkupplerLaden()
        //{
        //    _entkuppler.Ausgang.SpeicherString = textBoxAdresse.Text;
        //    _entkuppler.Bezeichnung = textBoxBezeichnung.Text;
        //    _entkuppler.Stecker = textBoxStecker.Text;
        //}

        private void buttonLaden_Click(object sender, EventArgs e)
        {

        }


    }
}
