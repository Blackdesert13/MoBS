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
  public partial class Signal : UserControl
  {
    private bool offen;
		private Model _model;
		private MoBaSteuerung.Elemente.Signal _signal;

		/// <summary>
		/// 
		/// </summary>
		public Signal()
    {
      InitializeComponent();
      this.offen = true;
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

		private void signalDatenLaden()
		{
			if (_signal != null)
			{
				textBoxSignal.Text = Convert.ToString(_signal.ID);
				textBoxAusgang.Text = _signal.Ausgang.SpeicherString;
				//.Text = _signal.Bezeichnung;
				
				
			}
			else
			{
				textBoxGleis.Text = "";
				textBoxAusgang.Text = "";
				
			}
		}

		private void signalLaden(int ID)
		{
			_signal = _model.ZeichnenElemente.SignalElemente.Element(ID);
			_model.BearbeitenSelektieren(_signal);
			signalDatenLaden();
		}
		private void buttonLaden_Click(object sender, EventArgs e)
		{
			int id;
			if (int.TryParse(textBoxSignal.Text, out id))
				signalLaden(id);
			else
			{
				textBoxAusgang.Text = "";
			}
		}

		private void buttonKoppelung_Click(object sender, EventArgs e)
		{
			if (_signal != null)
			{
				FrmBefehlsliste frmBefehlsliste = new FrmBefehlsliste(_signal.Koppelung, _signal.KurzBezeichnung + "-Koppelung");
				if (frmBefehlsliste.ShowDialog(this) == DialogResult.OK)
				{
					// string[][] t =  frm.auslesen();
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (_signal != null)
			{
				frmProperties frm = new frmProperties(_signal);
				if (frm.ShowDialog(this) == DialogResult.OK) ;
			}
		}
	}
}
