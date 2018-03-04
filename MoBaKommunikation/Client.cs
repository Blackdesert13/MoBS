using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoBaKommunikation
{
  /// <summary>
  /// 
  /// </summary>
  public class SlaveClients : IDisposable
  {
    private List<SlaveClient> slaveClients;

    /// <summary>
    /// 
    /// </summary>
    public SlaveClients()
    {
      this.slaveClients = new List<MoBaKommunikation.SlaveClient>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slaveDNS"></param>
    /// <param name="slavePort"></param>
    /// <param name="slaveRemoteID"></param>
    /// <param name="name"></param>
    internal SlaveClient Add(string slaveDNS, Int32 slavePort, string slaveRemoteID, string name)
    {
      foreach (SlaveClient itemSlaveClient in this.slaveClients)
      {
        if (itemSlaveClient.SlaveDNS.ToLower() == slaveDNS.ToLower() && itemSlaveClient.SlavePort == slavePort && itemSlaveClient.SlaveRemoteID.ToLower() == slaveRemoteID.ToLower())
        {
          return itemSlaveClient;
        }
      }
      // wenn nicht vorhanden, dann hinzufügen
      SlaveClient slaveClient = new MoBaKommunikation.SlaveClient(slaveDNS, slavePort, slaveRemoteID, name);
      this.slaveClients.Add(slaveClient);
      return slaveClient;
    }

    /// <summary>1
    /// 
    /// </summary>
    /// <param name="slaveDNS"></param>
    /// <param name="slavePort"></param>
    /// <param name="slaveRemoteID"></param>
    /// <returns></returns>
    internal SlaveClient Remove(string slaveDNS, Int32 slavePort, string slaveRemoteID)
    {
      foreach (SlaveClient itemSlaveClient in this.slaveClients)
      {
        if (itemSlaveClient.SlaveDNS.ToLower() == slaveDNS.ToLower() && itemSlaveClient.SlavePort == slavePort && itemSlaveClient.SlaveRemoteID.ToLower() == slaveRemoteID.ToLower())
        {
          this.slaveClients.Remove(itemSlaveClient);
          return itemSlaveClient;
        }
      }
      return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slaveDNS"></param>
    /// <returns></returns>
    internal SlaveClient Item(string slaveDNS)
    {
      foreach (SlaveClient itemSlaveClient in this.slaveClients)
      {
        if (itemSlaveClient.SlaveDNS.ToLower() == slaveDNS.ToLower()) return itemSlaveClient;
      }
      throw new IndexOutOfRangeException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="anlageDaten"></param>
    internal void SendenAnlageZustandsDatenAnAlle(byte[] anlageDaten)
    {
      // ToDo Parallel senden mit Exception händling
      foreach (SlaveClient itemSlaveClient in this.slaveClients)
      {
        itemSlaveClient.SendenZumSlave.AnlageZustandsDaten(anlageDaten);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slaveDNS"></param>
    /// <param name="anlageDaten"></param>
    internal void SendenAnlage(string slaveDNS, byte[] anlageDaten)
    {
      // ToDo
      new Task(() => this.Item(slaveDNS).SendenZumSlave.AnlageDaten(anlageDaten)).Start();
    }

    /// <summary>
    /// 
    /// </summary>
    internal void SendenBeenden()
    {
      // Parallel Schleife und auf ende warten.
      foreach (SlaveClient itemSlaveClient in this.slaveClients)
      {
        try
        {
          itemSlaveClient.SendenZumSlave.MasterBeendet();
        }
        catch (Exception ex)
        {
          // 
        }
      }


    }

    #region IDisposable

    private bool disposed = false;

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      // This object will be cleaned up by the Dispose method.
      // Therefore, you should call GC.SupressFinalize to
      // take this object off the finalization queue
      // and prevent finalization code for this object
      // from executing a second time.
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
      // Check to see if Dispose has already been called.
      if (!this.disposed)
      {
        // If disposing equals true, dispose all managed
        // and unmanaged resources.
        if (disposing)
        {
          // Dispose managed resources.
          foreach (SlaveClient itemSlaveClient in this.slaveClients)
          {
            itemSlaveClient.Dispose();
          }
          this.slaveClients.Clear();
        }

        // Call the appropriate methods to clean up
        // unmanaged resources here.
        // If disposing is false,
        // only the following code is executed.


        // Note disposing has been done.
        disposed = true;

      }
    }

    #endregion
  }

  /// <summary>
  /// 
  /// </summary>
  public class SlaveClient : IDisposable
  {
    private string slaveDNS;
    private Int32 slavePort;
    private string slaveRemoteID;
    private string name;
    private InterfaceMoBaSlave sendenZumSlave;

    /// <summary>
    /// 
    /// </summary>
    public string SlaveDNS
    {
      get
      {
        return this.slaveDNS;
      }

      set
      {
        this.slaveDNS = value;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public int SlavePort
    {
      get
      {
        return this.slavePort;
      }

      set
      {
        this.slavePort = value;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public string SlaveRemoteID
    {
      get
      {
        return this.slaveRemoteID;
      }

      set
      {
        this.slaveRemoteID = value;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public string Name
    {
      get
      {
        return this.name;
      }

      set
      {
        this.name = value;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public InterfaceMoBaSlave SendenZumSlave
    {
      get
      {
        if (this.sendenZumSlave == null)
        {
          this.sendenZumSlave = Activator.GetObject(typeof(InterfaceMoBaSlave), "tcp://" + this.SlaveDNS + ":" + this.SlavePort + "/" + this.SlaveRemoteID) as InterfaceMoBaSlave;
        }
        return this.sendenZumSlave;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public SlaveClient()
    {
      this.slaveDNS = "";
      this.slavePort = 0;
      this.slaveRemoteID = "";
      this.name = "";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dnsName"></param>
    /// <param name="port"></param>
    /// <param name="remoteID"></param>
    /// <param name="name"></param>
    public SlaveClient(string dnsName, Int32 port, string remoteID, string name)
    {
      this.slaveDNS = dnsName;
      this.slavePort = port;
      this.slaveRemoteID = remoteID;
      this.name = name;
    }

    #region IDisposable

    private bool disposed = false;

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      // This object will be cleaned up by the Dispose method.
      // Therefore, you should call GC.SupressFinalize to
      // take this object off the finalization queue
      // and prevent finalization code for this object
      // from executing a second time.
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
      // Check to see if Dispose has already been called.
      if (!this.disposed)
      {
        // If disposing equals true, dispose all managed
        // and unmanaged resources.
        if (disposing)
        {
          // Dispose managed resources.
          this.sendenZumSlave = null;
        }

        // Call the appropriate methods to clean up
        // unmanaged resources here.
        // If disposing is false,
        // only the following code is executed.


        // Note disposing has been done.
        disposed = true;

      }
    }

    #endregion
  }
}