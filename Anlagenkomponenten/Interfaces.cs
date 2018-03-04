using System;
using System.Drawing;
using MoBaSteuerung.Anlagenkomponenten.Delegates;

namespace MoBaSteuerung.Anlagenkomponenten.Interfaces
{
  /// <summary>
  /// Schnittstelle der Komunikation von Slave zum Master
  /// </summary>
  public interface IZyan
  {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="callback1"></param>
    /// <returns></returns>
    byte[] SlaveAnmelden(string name, Action<string, string> callback1);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    void SlaveAbmelden(string name);

    /// <summary>
    /// Slave Klick-Punkt auf RasterDefault umgerechnet.
    /// </summary>
    /// <param name="SlaveMousePoint"></param>
    void SlaveMouseClick(Point SlaveMousePoint);

    #region MasterEvents

    /// <summary>
    /// Sendet ein AnlagenObject zum zeichnen der Anlage.
    /// </summary>
    event AnlageDatenEventHandler MasterZumClient_AnlageZustandsDaten;

    /// <summary>
    /// Server sendet, das er beendet wird.
    /// </summary>
    event EventHandler MasterZumClient_MasterDisconnected;

    #endregion

  }
}