using System;
using System.Drawing;

namespace MoBaKommunikation
{
  /// <summary>
  /// 
  /// </summary>
  public class IMaster : MarshalByRefObject, InterfaceMoBaMaster
  {
    /// <summary>
    /// 
    /// </summary>
    public static Action<string, Int32, string, string> SlaveAnmelden;
    /// <summary>
    /// 
    /// </summary>
    public static Action<string, Int32, string> SlaveAbmelden;
    /// <summary>
    /// 
    /// </summary>
    public static Action<string, Int32> SlaveMouseClick;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slaveDNS"></param>
    /// <param name="slavePort"></param>
    /// <param name="slaveRemoteID"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public void Anmelden(string slaveDNS, Int32 slavePort, string slaveRemoteID, string name)
    {
      SlaveAnmelden(slaveDNS, slavePort, slaveRemoteID, name);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slaveDNS"></param>
    /// <param name="slavePort"></param>
    /// <param name="slaveRemoteID"></param>
    public void Abmelden(string slaveDNS, Int32 slavePort, string slaveRemoteID)
    {
      SlaveAbmelden(slaveDNS, slavePort, slaveRemoteID);
    }

    public void MouseClick(string elementType, int id)
    {
      SlaveMouseClick(elementType, id);
    }
  }
}