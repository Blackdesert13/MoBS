using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ModellBahnSteuerung.FahrstrassenEditor {
	public partial class frmKombiFahrstrasse : Form {
		public frmKombiFahrstrasse() {
			InitializeComponent();
		}

		private DialogResult dialogResult;
		

		private void frmArduino_FormClosed(object sender, FormClosedEventArgs e) {
			this.DialogResult = this.dialogResult;
		}

		private void buttonOK_Click(object sender, EventArgs e) {
			this.dialogResult = DialogResult.OK;
			this.Close();
		}
		
		private void buttonCancel_Click(object sender, EventArgs e) {
			this.Close();
		}
	}
}
