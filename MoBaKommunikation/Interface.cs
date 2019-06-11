using System;
using System.Drawing;

namespace MoBaKommunikation
{
  /// <summary>
  /// 
  /// </summary>
  public interface InterfaceMoBaMaster
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="slaveDNS"></param>
    /// <param name="port"></param>
    /// <param name="remoteID"></param>
    /// <param name="clientName"></param>
    /// <returns></returns>
    void Anmelden(string slaveDNS, Int32 port, string remoteID, string clientName);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slaveDNS"></param>
    /// <param name="port"></param>
    /// <param name="remoteID"></param>
    void Abmelden(string slaveDNS, Int32 port, string remoteID);

    void MouseClick(string elementType, int id);
  }

  /// <summary>
  /// 
  /// </summary>
  public interface InterfaceMoBaSlave
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="anlagenDaten"></param>
    void AnlageDaten(byte[] anlagenDaten);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="anlagenZustandsDaten"></param>
    void AnlageZustandsDaten(byte[] anlagenZustandsDaten);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="zugListenDaten"></param>
		void ZugListenDaten(byte[] zugListenDaten);

		/// <summary>
		/// 
		/// </summary>
		void MasterBeendet();
  }
}