using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MoBaSteuerung.Anlagenkomponenten;

namespace MoBaSteuerung.Dialoge
{
  /// <summary>
  /// 
  /// </summary>
  public partial class frmLog : Form
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logPath"></param>
    public frmLog()
    {
      this.InitializeComponent();
      if (!string.IsNullOrEmpty(Logging.Log.LogDateiPfad))
      {
        this.LogLaden();
        this.fileSystemWatcherLog.Path = Logging.Log.LogDateiPfad;
        this.fileSystemWatcherLog.EnableRaisingEvents = true;
      }
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      this.fileSystemWatcherLog.EnableRaisingEvents = false;
      this.Close();
    }

    private void fileSystemWatcherLog_Changed(object sender, System.IO.FileSystemEventArgs e)
    {
      this.LogLaden();
    }

    private void fileSystemWatcherLog_Created(object sender, System.IO.FileSystemEventArgs e)
    {
      this.LogLaden();
    }

    private void LogLaden()
    {
      this.richTextBoxLog.Text = File.ReadAllText(Logging.Log.LogDateiPfad);
      this.richTextBoxLog.ScrollToCaret();
    }
  }
}
