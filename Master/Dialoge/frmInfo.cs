using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MoBaSteuerung.Dialoge
{
  public partial class frmInfo : Form
  {
    public frmInfo()
    {
      InitializeComponent();
    }

    private void frmInfo_Load(object sender, EventArgs e)
    {
      this.labelVersion.Text = Environment.Version.ToString();
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      this.Close();
    }

  }
}
