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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="anlageDaten"></param>
        public void AnlageDaten(byte[] anlageDaten) {
            Debug.Print(anlageDaten.Length.ToString());
            MasterAnlageDaten(anlageDaten);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="anlageZustandDaten"></param>
        public void AnlageZustandsDaten(byte[] anlageZustandDaten) {
            Debug.Print(anlageZustandDaten.Length.ToString());
            MasterAnlagenZustandsDaten(anlageZustandDaten);
        }

        /// <summary>
        /// 
        /// </summary>
        public void MasterBeendet() {
            Debug.Print("MasterBeendet");
        }

    }
}