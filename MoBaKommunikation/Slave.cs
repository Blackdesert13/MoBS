using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Lifetime;
using System.Threading;
using System.Threading.Tasks;

namespace MoBaKommunikation {
	/// <summary>
	/// 
	/// </summary>
	public class Slave : IDisposable {
		private TcpChannel tcpEmpfangChannel;
		private InterfaceMoBaMaster sendenZumMaster;
		private string remoteID;
		private Int32 port;

		/// <summary>
		/// 
		/// </summary>
		public Slave(string SystemName) {
			RemotingConfiguration.ApplicationName = SystemName;
			LifetimeServices.LeaseTime = TimeSpan.FromSeconds(5);
			LifetimeServices.RenewOnCallTime = TimeSpan.FromSeconds(5);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="masterDNS">DNS</param>
		/// <param name="port">Port</param>
		/// <param name="remoteID">Remote Unique</param>
		/// <param name="clientName"></param>
		public void Start(string masterDNS, Int32 port, string remoteID, string clientName) {
			#region SlaveRemoteServer

			this.tcpEmpfangChannel = new TcpChannel(port + 1);
			ChannelServices.RegisterChannel(this.tcpEmpfangChannel, false);
			RemotingConfiguration.RegisterWellKnownServiceType(typeof(ISlave), remoteID + "Slave", WellKnownObjectMode.Singleton);

			#endregion

			#region SlaveRemoteClient

			this.sendenZumMaster = Activator.GetObject(typeof(InterfaceMoBaMaster), "tcp://" + masterDNS + ":" + port + "/" + remoteID + "Master") as InterfaceMoBaMaster;

			#endregion

			// An Master anmelden
			if (this.sendenZumMaster != null) {
				this.remoteID = remoteID;
				this.port = port;
				new Task(() => this.sendenZumMaster.Anmelden(/*Environment.MachineName*/masterDNS, this.port + 1, this.remoteID + "Slave", clientName + Environment.MachineName)).Start();
			}

			ISlave.MasterAnlageDaten = this.MasterAnlageDaten;
			ISlave.MasterAnlagenZustandsDaten = this.MasterAnlagenZustandsDaten;
			ISlave.MasterZugListenDaten = this.MasterZugListenDaten;
		}

		private void MasterAnlagenZustandsDaten(byte[] masterAnlagenZustandsDaten) {
			this.OnMasterAnlagenZustandsDaten(masterAnlagenZustandsDaten);
		}

		private void MasterZugListenDaten(byte[] masterZugListenDaten) {
			this.OnMasterZugListenDaten(masterZugListenDaten);
		}

		private void MasterAnlageDaten(byte[] masterAnlageDaten) {
			this.OnMasterAnlageDaten(masterAnlageDaten);
		}

		/// <summary>
		/// 
		/// </summary>
		public void Stop() {
			// Server beenden
			ChannelServices.UnregisterChannel(this.tcpEmpfangChannel);
			this.tcpEmpfangChannel = null;

			// Vom Master abmelden
			if (this.sendenZumMaster != null) {
				this.sendenZumMaster.Abmelden(Environment.MachineName, this.port + 1, this.remoteID + "Slave");
			}
		}


		public void SlaveAnMasterMouseClick(string elementType, int id) {
			new Task(() => this.SlaveAnMasterMouseClickTask(elementType, id)).Start();
		}

		private void SlaveAnMasterMouseClickTask(string elementType, int id) {
			try {
				if (this.sendenZumMaster != null) {
					this.sendenZumMaster.MouseClick(elementType, id);
				}
			}
			catch (Exception ex) {
				Logging.Log.Schreibe("Error: " + ex.Message, LogLevel.Trace);
			}
		}

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
					//this.slaveClients.SendenBeenden();
					//this.slaveClients.Dispose();
					//this.slaveClients = null;

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

		#region Event AnlageDaten

		public delegate void ByteEventhandler(byte[] e);

		public event ByteEventhandler MasterAnlageDatenEventHandler;

		public event ByteEventhandler MasterAnlagenZustandsDatenEventHandler;

		public event ByteEventhandler MasterZugListenDatenEventHandler;


		public virtual void OnMasterAnlageDaten(byte[] e) {
			if (this.MasterAnlageDatenEventHandler != null) {
				SynchronizationContext context = SynchronizationContext.Current ?? new SynchronizationContext();
				context.Send(s => { this.MasterAnlageDatenEventHandler(e); }, null);
			}
		}

		public virtual void OnMasterAnlagenZustandsDaten(byte[] e) {
			if (this.MasterAnlagenZustandsDatenEventHandler != null) {
				SynchronizationContext context = SynchronizationContext.Current ?? new SynchronizationContext();
				context.Send(s => { this.MasterAnlagenZustandsDatenEventHandler(e); }, null);
			}
		}

		public virtual void OnMasterZugListenDaten(byte[] e) {
			if (this.MasterZugListenDatenEventHandler != null) {
				SynchronizationContext context = SynchronizationContext.Current ?? new SynchronizationContext();
				context.Send(s => { this.MasterZugListenDatenEventHandler(e); }, null);
			}
		}

		#endregion

	}
}