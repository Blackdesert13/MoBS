using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MoBaSteuerung.Dialoge {
    public partial class frmArduino : Form {
        private DialogResult dialogResult;
        private string portName;

        public string PortName {
            get {
                return portName;
            }
        }

        public frmArduino():this(null) {
        }

        public frmArduino(string[] portnames){
            InitializeComponent();
            this.dialogResult = DialogResult.Cancel;
            this.portName = String.Empty;
            this.comboBoxPort.Items.AddRange(portnames);
            if(portnames.Length > 0)
                this.comboBoxPort.SelectedIndex = 0;
        }

        private void frmArduino_FormClosed(object sender, FormClosedEventArgs e) {
            this.DialogResult = this.dialogResult;
        }

        private void buttonOK_Click(object sender, EventArgs e) {
            this.dialogResult = DialogResult.OK;
            this.Close();
        }

        private void comboBoxPort_SelectedIndexChanged(object sender, EventArgs e) {
            this.portName = this.comboBoxPort.SelectedItem.ToString();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
