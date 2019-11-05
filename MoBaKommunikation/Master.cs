using System;
using System.Drawing;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Lifetime;
using System.Threading;

namespace MoBaKommunikation {
	/// <summary>
	/// 
	/// </summary>
	public class Master : IDisposable {
		private TcpChannel tcpEmpfangChannel;
		private SlaveClients slaveClients;

		/// <summary>
		/// 
		/// </summary>
		public Master(string SystemName) {
			RemotingConfiguration.ApplicationName = SystemName;
			LifetimeServices.LeaseTime = TimeSpan.FromSeconds(5);
			LifetimeServices.RenewOnCallTime = TimeSpan.FromSeconds(5);

			this.slaveClients = new MoBaKommunikation.SlaveClients();
		}

		#region Anmelden

		/// <summary>
		/// 
		/// </summary>
		/// <param name="slaveClient"></param>
		public delegate void AnmeldenEventHandler(SlaveClient slaveClient);
		/// <summary>
		/// 
		/// </summary>
		public event AnmeldenEventHandler SlaveAnmeldenClickEventHandler;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="slaveClient"></param>
		public virtual void OnSlaveAnmelden(SlaveClient slaveClient) {
			if (this.SlaveAnmeldenClickEventHandler != null) {
				SynchronizationContext context = SynchronizationContext.Current ?? new SynchronizationContext();
				context.Send(s => { this.SlaveAnmeldenClickEventHandler(slaveClient); }, null);
			}
		}

		#endregion

		#region Abmelden

		/// <summary>
		/// 
		/// </summary>
		/// <param name="slaveClient"></param>
		public delegate void AbmeldenEventHandler(SlaveClient slaveClient);
		/// <summary>
		/// 
		/// </summary>
		public event AbmeldenEventHandler SlaveAbmeldenClickEventHandler;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="slaveClient"></param>
		public virtual void OnSlaveAbmelden(SlaveClient slaveClient) {
			if (this.SlaveAbmeldenClickEventHandler != null) {
				SynchronizationContext context = SynchronizationContext.Current ?? new SynchronizationContext();
				context.Send(s => { this.SlaveAbmeldenClickEventHandler(slaveClient); }, null);
			}
		}

		#endregion

		#region MouseClick


		public delegate void PointEventHandler(string elementType, int id);
		/// <summary>
		/// 
		/// </summary>
		public event PointEventHandler SlaveMouseClickEventHandler;

		public virtual void OnSlaveMouseClick(string elementType, int id) {
			if (this.SlaveMouseClickEventHandler != null) {
				SynchronizationContext context = SynchronizationContext.Current ?? new SynchronizationContext();
				context.Send(s => { this.SlaveMouseClickEventHandler(elementType, id); }, null);
			}
		}

		#endregion

		#region Methoden

		#region Public

		/// <summary>
		/// 
		/// </summary>
		/// <param name="remoteID"></param>
		/// <param name="port"></param>
		public void Start(string remoteID, Int32 port) {
			#region MasterRemoteServer

			this.tcpEmpfangChannel = new TcpChannel(port);
			ChannelServices.RegisterChannel(this.tcpEmpfangChannel, false);
			RemotingConfiguration.RegisterWellKnownServiceType(typeof(IMaster), remoteID + "Master", WellKnownObjectMode.Singleton);

			#endregion

			IMaster.SlaveAnmelden = this.SlaveAnmelden;
			IMaster.SlaveAbmelden = this.SlaveAbmelden;
			IMaster.SlaveMouseClick = this.SlaveMouseClick;
		}

		/// <summary>
		/// 
		/// </summary>
		public void Stop() {
			if (this.slaveClients != null) {
				this.slaveClients.SendenBeenden();
				this.slaveClients.Dispose();
				this.slaveClients = null;
			}
			if (this.tcpEmpfangChannel != null) {
				ChannelServices.UnregisterChannel(this.tcpEmpfangChannel);
				this.tcpEmpfangChannel = null;
			}
		}

		/// <summary>
		/// Sendet die Anlage zu dem angegebenen Slave.
		/// </summary>
		/// <param name="slaveDNS">Slave Rechner Name</param>
		/// <param name="anlage">Anlagedaten</param>
		public void SendeAnlageZuSlave(string slaveDNS, byte[] anlage) {
			// Anlage an Slave senden
			try {
				this.slaveClients.SendenAnlage(slaveDNS, anlage);
			}
			catch (Exception e) {

			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="anlageZustandsDaten"></param>
		public void SendeAnlageZustandsDatenAnAlle(byte[] anlageZustandsDaten) {
			// Anlagedaten an alle Slaves senden
			try {
				this.slaveClients.SendenAnlageZustandsDatenAnAlle(anlageZustandsDaten);
			}
			catch (Exception e) {

			}
		}

		public void SendeZugListeAnAlle(byte[] zugListe) {

			try {
				this.slaveClients.SendenZugListeAnAlle(zugListe);
			}
			catch (Exception e) {

			}
		}

		#endregion

		#region Private

		private void SlaveAnmelden(string slaveDNS, Int32 port, string remoteSlaveID, string name) {
			// Slave Anmelden
			SlaveClient slaveClients = this.slaveClients.Add(slaveDNS, port, remoteSlaveID, name);
			// Slaveanmeldung Event auslösen.
			this.OnSlaveAnmelden(slaveClients);
		}

		private void SlaveAbmelden(string slaveDNS, Int32 port, string remoteSlaveID) {
			// Slave Abmelden
			SlaveClient slaveClients = this.slaveClients.Remove(slaveDNS, port, remoteSlaveID);
			if (slaveClients != null) {
				this.OnSlaveAbmelden(slaveClients);
			}
		}

		private void SlaveMouseClick(string elementType, int id) {
			this.OnSlaveMouseClick(elementType, id);
		}

		#endregion

		#endregion

		#region IDisposable

		private bool disposed = false;

		/// <summary>
		/// 
		/// </summary>
		public void Dispose() {
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
		protected virtual void Dispose(bool disposing) {
			// Check to see if Dispose has already been called.
			if (!this.disposed) {
				// If disposing equals true, dispose all managed
				// and unmanaged resources.
				if (disposing) {
					// Dispose managed resources.
					this.slaveClients.SendenBeenden();
					this.slaveClients.Dispose();
					this.slaveClients = null;

					ChannelServices.UnregisterChannel(this.tcpEmpfangChannel);
					this.tcpEmpfangChannel = null;
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