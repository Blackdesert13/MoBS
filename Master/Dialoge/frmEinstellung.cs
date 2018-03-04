using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MoBaSteuerung.Dialoge {
    /// <summary>
    /// 
    /// </summary>
    public partial class frmEinstellung : Form {
        /// <summary>
        /// 
        /// </summary>
        public frmEinstellung() {
            this.InitializeComponent();
        }

        #region Member/Properties
        private bool _adminAktiviert = false;
        private string _pwd = "";

        public bool AnlageBearbeitenAktiviert
        {
            get
            {
                return this.aktivierungAnlageBearbeiten.Checked;
            }
            set
            {
                this.aktivierungAnlageBearbeiten.Checked = value;
            }
        }

        

        public bool RückmeldungAnzeigen {
            get {
                return this.rückmeldungAnzeigen.Checked;
            }
            set {
                this.rückmeldungAnzeigen.Checked = value;
            }
        }

        public bool RückmeldungAktiv {
            get {
                return this.rückmeldungAktiv.Checked;
            }
            set {
                this.rückmeldungAktiv.Checked = value;
            }
        }

        public int FahrstraßenStartVerzögerung {
            get {
                return (int)this.fahrstraßeStartVerzögerung.Value;
            }
            set {
                this.fahrstraßeStartVerzögerung.Value = value;
            }
        }

        public bool AdminAktiviert {
            get
            {
                return _adminAktiviert;
            }
            internal set
            {
                _adminAktiviert = value;
                this.aktivierungAnlageBearbeiten.Enabled = _adminAktiviert;
                if (_adminAktiviert) {
                    this.passwort.Text = _pwd;
                }
            }
        }

        public string Pwd
        {
            set
            {
                _pwd = value;
            }
        }

        #endregion

        private void buttonSpeichern_Click(object sender, EventArgs e) {
            // ToDo speichern
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonAbbrechen_Click(object sender, EventArgs e) {
            // DoTo abbrechen
            this.Close();
        }

        private void passwort_TextChanged(object sender, EventArgs e)
        {
            this.aktivierungAnlageBearbeiten.Enabled = (passwort.Text == _pwd);
            if (this.aktivierungAnlageBearbeiten.Enabled)
            {
                if (!_adminAktiviert)
                    AdminAktiviert = true;
            }

        }
    }
}