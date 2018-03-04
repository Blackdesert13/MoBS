using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MoBaSteuerung.Anlagenkomponenten;

namespace MoBaSteuerung
{
  static class Program
  {
    /// <summary>
    /// Der Haupteinstiegspunkt für die Anwendung.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.ThreadException += Application_ThreadException;
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MoBaStForm());
    }

    private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
    {
      Logging.Log.SchreibeException(e.Exception);
      if (MsgBox.Show(e.Exception.Message, Constanten.ProgrammName, MessageBoxButtons.OKCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) == DialogResult.Cancel)
      {
        Application.Exit();
      }
    }
  }
}
