using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MoBaSteuerung.Anlagenkomponenten
{
  /// <summary>
  /// Projekt Constanten
  /// </summary>
  public static class Constanten
  {
    /// <summary>
    /// Festgelegter Standardraster einer neuen Anlage.
    /// 20 Pixel
    /// </summary>
    public const Int32 STANDARDRASTER = 20;

    /// <summary>
    /// Festgelegter Standardname einer neuen 
    /// Unbenannt
    /// </summary>
    public const String STANDARDFILENAME = "Unbenannt";

    /// <summary>
    /// Festgelegte Standardgröße einer neuen Anlage
    /// 20 x 15 Raster
    /// </summary>
    public static Size STANDARDANLAGENGRÖßEINRASTER = new Size(20, 15);

    /// <summary>
    /// 
    /// </summary>
    public static string ProgrammName = "MoBaSt";
  }
}