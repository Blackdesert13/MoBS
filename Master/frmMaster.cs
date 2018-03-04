using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UCfM.Components.Dialogs;

namespace UCfM
{
  /// <summary>
  /// 
  /// </summary>
  public partial class frmMaster : Form
  {
    private Controller controller;

    /// <summary>
    /// 
    /// </summary>
    public frmMaster()
    {
      this.InitializeComponent();
      this.toolStripBearbeiten.Visible = false;
      this.controller = new Controller(this.viewMaster);
    }

    private void toolStripSplitButtonRaster_ButtonClick(object sender, EventArgs e)
    {
      this.toolStripSplitButtonRaster.Checked = !this.toolStripSplitButtonRaster.Checked;
    }

    private void farbeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ColorDialogExtension colorDialog = new ColorDialogExtension(this.Location.X + ((this.Width - 225) / 2), this.Location.Y + ((this.Height - 330) / 2));
      colorDialog.Color = Master.Properties.Settings.Default.RasterFarbe;
      if (colorDialog.ShowDialog() == DialogResult.OK)
      {
        Master.Properties.Settings.Default.RasterFarbe = colorDialog.Color;
        Master.Properties.Settings.Default.Save();
      }
    }

    private void toolStripMenuItemBearbeiten_Click(object sender, EventArgs e)
    {
      this.toolStripBearbeiten.Visible = toolStripMenuItemBearbeiten.Checked;
    }

    private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
    {
      this.viewMaster.Focus();
    }
  }
}