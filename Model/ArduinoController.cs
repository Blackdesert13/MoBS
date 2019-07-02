using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Threading;
using System.ComponentModel;
using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Enum;

namespace MoBaSteuerung {
    public class BefehlEventArgs : EventArgs {
        byte[] _befehl;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="befehl"></param>
        public BefehlEventArgs(byte[] befehl) {
            this._befehl = befehl;
        }

        /// <summary>
        /// 
        /// </summary>
        public byte[] Befehl {
            get { return this._befehl; }
            set { this._befehl = value; }
        }
    }

    public class Event {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="event"></param>
        internal static void OnEvent(object sender, EventArgs e, System.Delegate @event) {
            bool eventFired = false;
            if (@event != null) {
                foreach (System.Delegate singleCast in @event.GetInvocationList()) {
                    eventFired = false;
                    try {
                        ISynchronizeInvoke syncInvoke = (ISynchronizeInvoke)singleCast.Target;
                        if (syncInvoke != null && syncInvoke.InvokeRequired) {
                            eventFired = true;
                            syncInvoke.BeginInvoke(singleCast, new object[] { sender, e });
                        }
                        else {
                            eventFired = true;
                            singleCast.DynamicInvoke(new object[] { sender, e });
                        }
                    }
                    catch (System.Reflection.TargetInvocationException ex) {
                        if (!eventFired) {
                            singleCast.DynamicInvoke(new object[] { sender, e });
                        }
                        Debug.Print("OnEvent: " + ex.InnerException.Message);
                    }
                }
            }
        }
    }

    public class ArduinoController : Form {

        #region events

        public event EventHandler<BefehlEventArgs> BefehlReceived;

        #endregion

        private SerialPort _comPort = null;
        private List<byte> _receivedBytes = null;

        public List<byte> ReceivedBytes {
            get { return _receivedBytes; }
            set { _receivedBytes = value; }
        }

        public SerialPort ComPort {
            get { return _comPort; }
            set { _comPort = value; }
        }


        public bool OpenComPort(string name, bool showErrorDialog = true) {
            try {
                ComPort = new SerialPort(name, 9600);
                _receivedBytes = new List<byte>();
                ComPort.DataReceived += ComPort_DataReceived;
                if(!ComPort.IsOpen)
                    ComPort.Open();
                return true;
            }
            catch (Exception e) {
                Logging.Log.Schreibe(e.Message);
                if(showErrorDialog)
                    MessageBox.Show("Der ComPort konnte nicht geöffnet werden.");
            }
            return false;
        }


        void ComPort_DataReceived(object sender, SerialDataReceivedEventArgs e) {
            try {
                int anz = ComPort.BytesToRead;
                if (anz > 0) {
                    byte[] bytes = new byte[anz];
                    ComPort.Read(bytes, 0, anz);
                    _receivedBytes.AddRange(bytes);
                    GetBefehl();
                }
            }
            catch (Exception ex) {
                Logging.Log.Schreibe(ex.Message);
            }
        }

        private void GetBefehl() {
            while (this._receivedBytes.Count >= 5) {
                if (_receivedBytes[4] == (byte)((_receivedBytes[0] + _receivedBytes[1] + _receivedBytes[2] + _receivedBytes[3]) % 256)) {
                    byte[] befehl = new byte[5];
                    for (int i = 0; i < 5; i++)
                        befehl[i] = _receivedBytes[i];

                    Debug.Print("Befehl von Arduino: " + befehl[0] + " " + befehl[1]
                                + " " + befehl[2] + " " + befehl[3] + " " + befehl[4]);


                    _receivedBytes.RemoveRange(0, 5);
                    Event.OnEvent(this,new BefehlEventArgs(befehl),BefehlReceived);
                }
                else {
                    this._receivedBytes.RemoveAt(0);
                }
            }
        }


        public void SendData(byte[] data) {
            if (IsPortOpen()) {
                if (data != null)
                    try {
                        ComPort.Write(data, 0, data.Length);
                        string msg = "";
                        foreach (byte b in data)
                            msg += b.ToString() + " ";
                        Debug.Print(msg + "gesendet");
                    }
                    catch(Exception e) {
                        Logging.Log.Schreibe(e.Message);
                    }
            }
            else {
                Debug.Write("COM Port not open");
            }
        }



        public bool CloseComPort() {
            if (ComPort != null) {
                try {
                    ComPort.DataReceived -= ComPort_DataReceived;
                    if (ComPort.IsOpen) {
                        ComPort.Close();
                        return true;
                    }
                }
                catch (Exception) {
                    Logging.Log.Schreibe("Fehler beim Schließen des Ports");
                }
                finally {
                    ComPort = null;
                }
            }
            return false;
        }

        #region ToolStrip

        public string COMPortName {
            get {
                if (IsPortOpen()) {
                    return ComPort.PortName;
                }
                return String.Empty;
            }
        }

        public bool IsPortOpen() {
            if (ComPort != null)
                return ComPort.IsOpen;
            return false;
        }

        public string[] GetSerialPortNames() {
            return SerialPort.GetPortNames();
        }

        #endregion

    }
}

