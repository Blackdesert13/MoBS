using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ModellBahnSteuerung.ToolBox
{
  /// <summary>
  /// 
  /// </summary>
  public partial class Signal : UserControl
  {
    private bool offen;

    /// <summary>
    /// 
    /// </summary>
    public Signal()
    {
      InitializeComponent();
      this.offen = true;
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
        this.Height = 93;
        this.offen = true;
      }
    }

  }
}
