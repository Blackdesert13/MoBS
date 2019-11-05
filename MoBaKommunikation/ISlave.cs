using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using System;
using System.Diagnostics;

namespace MoBaKommunikation {
	/// <summary>
	/// 
	/// </summary>
	public class ISlave : MarshalByRefObject, InterfaceMoBaSlave {
		/// <summary>
		/// 
		/// </summary>
		public static Action<byte[]> MasterAnlageDaten;

		public static Action<byte[]> MasterAnlagenZustandsDaten;

		public static Action<byte[]> MasterZugListenDaten;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="anlageDaten"></param>
		public void AnlageDaten(byte[] anlageDaten) {
			//Logging.Log.Schreibe(anlageDaten.Length.ToString(),LogLevel.Trace);
			MasterAnlageDaten(anlageDaten);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="zugListenDaten"></param>
		public void ZugListenDaten(byte[] zugListenDaten) {
			//Logging.Log.Schreibe(zugListenDaten.Length.ToString(), LogLevel.Trace);
			MasterZugListenDaten(zugListenDaten);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="anlageZustandDaten"></param>
		public void AnlageZustandsDaten(byte[] anlageZustandDaten) {
			//Logging.Log.Schreibe(anlageZustandDaten.Length.ToString());
			MasterAnlagenZustandsDaten(anlageZustandDaten);
		}

		/// <summary>
		/// 
		/// </summary>
		public void MasterBeendet() {
			Logging.Log.Schreibe("MasterBeendet");
		}

	}
}