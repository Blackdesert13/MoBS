﻿using System;
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
    public partial class FrmKnoten : UserControl
    {
        private bool offen;
        private Knoten _knoten;
        private Model _model;
        /// <summary>
        /// 
        /// </summary>
        public FrmKnoten()
        {
            this.InitializeComponent();
            this.offen = true;
        }

        public Model Model { set { _model = value; } }

        public Knoten AktuellerKnoten { set { _knoten = value; knotenDatenLaden(); } }

        private void PictureBoxMenü_MouseEnter(object sender, EventArgs e)
        {
          if(this.offen)
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
            if(this.offen)
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
        private void knotenDatenLaden()
        {
            textBoxKnoten.Text = Convert.ToString(_knoten.ID);
            textBoxLageX.Text = Convert.ToString(_knoten.PositionRaster.X);
            textBoxLageY.Text = Convert.ToString(_knoten.PositionRaster.Y);
        }

        private void knotenSpeichern()
        {
            Point p = new Point();
            p.X = Convert.ToInt32(textBoxLageX.Text);
            p.Y = Convert.ToInt32(textBoxLageY.Text);
            _knoten.PositionRaster = p;
        }

        private void buttonSpeichern_Click(object sender, EventArgs e)
        {
            knotenSpeichern();
        }
    }
}