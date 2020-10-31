using MoBaSteuerung;
using MoBaSteuerung.Elemente;
using MoBaSteuerung.Anlagenkomponenten;
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
		internal enum StartPunkt {
			NichtGewählt = 0,
			StartSignal,
			ZielSignal
		}

		private Model _model;
		private AnlagenElemente _anlagenElemente;
		private DialogResult _dialogResult;
		private StartPunkt _startPunkt = StartPunkt.NichtGewählt;

		private System.Windows.Forms.ComboBox.ObjectCollection _startSignalComboBoxItems;
		private System.Windows.Forms.ComboBox.ObjectCollection _zielSignalComboBoxItems;

		private List<CheckComboBoxTest.CheckedComboBox> _startFahrstrassen
			= new List<CheckComboBoxTest.CheckedComboBox>();
		private List<CheckComboBoxTest.CheckedComboBox> _endFahrstrassen
			= new List<CheckComboBoxTest.CheckedComboBox>();

		public frmKombiFahrstrasse(Model model) {
			InitializeComponent();
			_model = model;
			_anlagenElemente = _model.ZeichnenElemente;
			_startSignalComboBoxItems = comboBoxStartSignal.Items;
			_zielSignalComboBoxItems = comboBoxZielSignal.Items;
			_startSignalComboBoxItems.Clear();
			_zielSignalComboBoxItems.Clear();
			_startFahrstrassen.Add(checkedComboBox1);
			_endFahrstrassen.Add(checkedComboBox2);


			List<int> signaleStart = new List<int>();
			List<int> signaleZiel = new List<int>();
			foreach (FahrstrasseN fs in _anlagenElemente.FahrstrassenElemente.GespeicherteFahrstrassen) {
				if (!signaleStart.Contains(fs.StartSignal.ID)) {
					signaleStart.Add(fs.StartSignal.ID);
				}
				if (!signaleZiel.Contains(fs.EndSignal.ID)) {
					signaleZiel.Add(fs.EndSignal.ID);
				}
			}
			signaleStart.Sort();
			signaleZiel.Sort();
			foreach (int sig in signaleStart) {
				_startSignalComboBoxItems.Add(sig);
			}
			foreach (int sig in signaleZiel) {
				_zielSignalComboBoxItems.Add(sig);
			}
			PositionierenControls();
		}

		private void frmArduino_FormClosed(object sender, FormClosedEventArgs e) {
			this.DialogResult = this._dialogResult;
		}

		private void buttonOK_Click(object sender, EventArgs e) {
			this._dialogResult = DialogResult.OK;
			this.Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e) {
			this._dialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void ResetStartFahrstrassen() {
			int i = 1;
			for (; i < _startFahrstrassen.Count;) {
				CheckComboBoxTest.CheckedComboBox altesControl = _startFahrstrassen[_startFahrstrassen.Count - 1];
				_startFahrstrassen.RemoveAt(_startFahrstrassen.Count - 1);
				altesControl.DropDownClosed -= new System.EventHandler(this.checkedComboBox1_DropDownClosed);
				this.panel1.Controls.Remove(altesControl);
				altesControl.Dispose();
			}
			_startFahrstrassen[0].Text = "";
			PositionierenControls();
		}
		
		private void ResetEndeFahrstrassen() {
			int i = 1;
			for (; i < _endFahrstrassen.Count;) {
				CheckComboBoxTest.CheckedComboBox altesControl = _endFahrstrassen[_endFahrstrassen.Count - 1];
				_endFahrstrassen.RemoveAt(_endFahrstrassen.Count - 1);
				altesControl.DropDownClosed -= new System.EventHandler(this.checkedComboBox2_DropDownClosed);
				this.panel1.Controls.Remove(altesControl);
				altesControl.Dispose();
			}
			_endFahrstrassen[0].Text = "";
		}

		private void PositionierenControls() {
			if (_startFahrstrassen.Last().Location.Y != 10) {
				int differenz = 10 - _startFahrstrassen.Last().Location.Y;

				foreach(Control ctrl in _startFahrstrassen) {
					ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y + differenz);
				}

				foreach (Control ctrl in _endFahrstrassen) {
					ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y + differenz);
				}

				label1.Location = new Point(label1.Location.X, label1.Location.Y + differenz);
				label2.Location = new Point(label2.Location.X, label2.Location.Y + differenz);
				comboBoxStartSignal.Location = new Point(comboBoxStartSignal.Location.X, comboBoxStartSignal.Location.Y + differenz);
				comboBoxZielSignal.Location = new Point(comboBoxZielSignal.Location.X, comboBoxZielSignal.Location.Y + differenz);
			}
		}

		private void comboBoxStartSignal_SelectedValueChanged(object sender, EventArgs e) {
			if (comboBoxStartSignal.SelectedIndex != -1) {
				if (this._startPunkt == StartPunkt.NichtGewählt) {
					this._startPunkt = StartPunkt.StartSignal;
				}
				else if (this._startPunkt == StartPunkt.StartSignal) {
					ResetEndeFahrstrassen();
					//Todo: DisableEndeFahrstrassen();
				}
				ResetStartFahrstrassen();
				if (this._startPunkt != StartPunkt.ZielSignal) {
					//Todo: DisableBeginnFahrstrassen();
					_zielSignalComboBoxItems.Clear();
					int startSignalId = (int)comboBoxStartSignal.SelectedItem;
					List<int> signale = new List<int>();
					foreach (FahrstrasseN fs in _anlagenElemente.FahrstrassenElemente.GespeicherteFahrstrassen) {
						if (startSignalId == fs.StartSignal.ID) {
							if (!signale.Contains(fs.EndSignal.ID)) {
								signale.Add(fs.EndSignal.ID);
							}
						}
					}
					signale.Sort();
					foreach (int sig in signale) {
						_zielSignalComboBoxItems.Add(sig);
					}
					comboBoxZielSignal.SelectedIndex = -1;
					comboBoxZielSignal.Text = "";

					checkedComboBox1.Enabled = false;
					checkedComboBox2.Enabled = false;
					this.checkedComboBox1.Text = "Start-Ziel-Signal auswählen";
					this.checkedComboBox2.Text = "Start-Ziel-Signal auswählen";
				}
				else {
					checkedComboBox1.Enabled = true;
					checkedComboBox2.Enabled = true;
					checkedComboBox2.Text = "";
					checkedComboBox1.Text = "";

					checkedComboBox2.Items.Clear();
					int startSignalId = (int)comboBoxZielSignal.SelectedItem;
					List<int> signale = new List<int>();
					foreach (FahrstrasseN fs in _anlagenElemente.FahrstrassenElemente.GespeicherteFahrstrassen) {
						if (startSignalId == fs.StartSignal.ID) {
							if (!signale.Contains(fs.EndSignal.ID)) {
								signale.Add(fs.EndSignal.ID);
							}
						}
					}
					signale.Sort();
					foreach(int sig in signale) {
						checkedComboBox2.Items.Add(sig);
					}
					signale.Clear();

					checkedComboBox1.Items.Clear();
					int zielSignalId = (int)comboBoxStartSignal.SelectedItem;
					foreach (FahrstrasseN fs in _anlagenElemente.FahrstrassenElemente.GespeicherteFahrstrassen) {
						if (zielSignalId == fs.EndSignal.ID) {
							if (!signale.Contains(fs.StartSignal.ID)) {
								signale.Add(fs.StartSignal.ID);
							}
						}
					}
					signale.Sort();
					foreach (int sig in signale) {
						checkedComboBox1.Items.Add(sig);
					}
				}
			}
		}

		private void comboBoxZielSignal_SelectedValueChanged(object sender, EventArgs e) {
			if (comboBoxZielSignal.SelectedIndex != -1) {
				if (this._startPunkt == StartPunkt.NichtGewählt) {
					this._startPunkt = StartPunkt.ZielSignal;
				}
				else if (this._startPunkt == StartPunkt.ZielSignal) {
					ResetStartFahrstrassen();
					//Todo: DisableBeginnFahrstrassen();
				}
				ResetEndeFahrstrassen();
				if (this._startPunkt != StartPunkt.StartSignal) {
					//Todo: DisableEndeFahrstrassen();
					_startSignalComboBoxItems.Clear();
					int zielSignalId = (int)comboBoxZielSignal.SelectedItem;
					List<int> signale = new List<int>();
					foreach (FahrstrasseN fs in _anlagenElemente.FahrstrassenElemente.GespeicherteFahrstrassen) {
						if (zielSignalId == fs.EndSignal.ID) {
							if (!signale.Contains(fs.StartSignal.ID)) {
								signale.Add(fs.StartSignal.ID);
							}
						}
					}
					signale.Sort();
					foreach (int sig in signale) {
						_startSignalComboBoxItems.Add(sig);
					}
					comboBoxStartSignal.SelectedIndex = -1;
					comboBoxStartSignal.Text = "";

					checkedComboBox1.Enabled = false;
					checkedComboBox2.Enabled = false;
					this.checkedComboBox1.Text = "Start-Ziel-Signal auswählen";
					this.checkedComboBox2.Text = "Start-Ziel-Signal auswählen";
				}
				else {
					checkedComboBox1.Enabled = true;
					checkedComboBox2.Enabled = true;
					checkedComboBox2.Text = "";
					checkedComboBox1.Text = "";

					checkedComboBox2.Items.Clear();
					int startSignalId = (int)comboBoxZielSignal.SelectedItem;
					List<int> signale = new List<int>();
					foreach (FahrstrasseN fs in _anlagenElemente.FahrstrassenElemente.GespeicherteFahrstrassen) {
						if (startSignalId == fs.StartSignal.ID) {
							if (!signale.Contains(fs.EndSignal.ID)) {
								signale.Add(fs.EndSignal.ID);
							}
						}
					}
					signale.Sort();
					foreach(int sig in signale) {
						checkedComboBox2.Items.Add(sig);
					}
					signale.Clear();

					checkedComboBox1.Items.Clear();
					int zielSignalId = (int)comboBoxStartSignal.SelectedItem;
					foreach (FahrstrasseN fs in _anlagenElemente.FahrstrassenElemente.GespeicherteFahrstrassen) {
						if (zielSignalId == fs.EndSignal.ID) {
							if (!signale.Contains(fs.StartSignal.ID)) {
								signale.Add(fs.StartSignal.ID);
							}
						}
					}
					signale.Sort();
					foreach (int sig in signale) {
						checkedComboBox1.Items.Add(sig);
					}
				}
			}
		}

		private void checkedComboBox2_DropDownClosed(object sender, EventArgs e) {
			CheckComboBoxTest.CheckedComboBox control = (CheckComboBoxTest.CheckedComboBox)sender;
			if (!control.ValueChanged) {
				return;
			}
			if (control.CheckedItems.Count == 0) {
				int i = 0;
				for (; i < _endFahrstrassen.Count; i++) {
					if (_endFahrstrassen[i] == control) {
						i++;
						break;
					}
				}
				for (; i < _endFahrstrassen.Count;) {
					CheckComboBoxTest.CheckedComboBox altesControl = _endFahrstrassen[_endFahrstrassen.Count - 1];
					_endFahrstrassen.RemoveAt(_endFahrstrassen.Count - 1);
					altesControl.DropDownClosed -= new System.EventHandler(this.checkedComboBox2_DropDownClosed);
					this.panel1.Controls.Remove(altesControl);
					altesControl.Dispose();
				}
			}
			else if (control.CheckedItems.Count == 1) {
				int i = 0;
				for (; i < _endFahrstrassen.Count; i++) {
					if (_endFahrstrassen[i] == control) {
						i++;
						break;
					}
				}
				CheckComboBoxTest.CheckedComboBox neuesControl = null;
				if (i == _endFahrstrassen.Count) {
					neuesControl = new CheckComboBoxTest.CheckedComboBox();
					this.panel1.Controls.Add(neuesControl);
					neuesControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
					neuesControl.CheckOnClick = true;
					neuesControl.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
					neuesControl.DropDownHeight = 1;
					neuesControl.FormattingEnabled = true;
					neuesControl.IntegralHeight = false;
					neuesControl.Location = new System.Drawing.Point(control.Location.X, control.Location.Y + control.Size.Height + 5);
					neuesControl.Name = "checkedComboBox2_" + i;
					neuesControl.Size = new System.Drawing.Size(151, 21);
					neuesControl.TabIndex = 0;
					neuesControl.Text = "";
					neuesControl.ValueSeparator = ", ";
					neuesControl.DropDownClosed += new System.EventHandler(this.checkedComboBox2_DropDownClosed);
					neuesControl.Enabled = true;

					_endFahrstrassen.Add(neuesControl);
				}
				else {
					neuesControl = _endFahrstrassen[i];

					i++;
					for (; i < _endFahrstrassen.Count;) {
						CheckComboBoxTest.CheckedComboBox altesControl = _endFahrstrassen[_endFahrstrassen.Count - 1];
						_endFahrstrassen.RemoveAt(_endFahrstrassen.Count - 1);
						altesControl.DropDownClosed -= new System.EventHandler(this.checkedComboBox2_DropDownClosed);
						this.panel1.Controls.Remove(altesControl);
						altesControl.Dispose();
					}
				}

				neuesControl.Items.Clear();
				neuesControl.Text = "";
				int startSignalId = (int)control.CheckedItems[0];
				List<int> signale = new List<int>();
				foreach (FahrstrasseN fs in _anlagenElemente.FahrstrassenElemente.GespeicherteFahrstrassen) {
					if (startSignalId == fs.StartSignal.ID) {
						if (!signale.Contains(fs.EndSignal.ID)) {
							signale.Add(fs.EndSignal.ID);
						}
					}
				}
				signale.Sort();
				foreach (int sig in signale) {
					neuesControl.Items.Add(sig);
				}
			}
			else {
				int i = 0;
				for (; i < _endFahrstrassen.Count; i++) {
					if (_endFahrstrassen[i] == control) {
						i++;
						break;
					}
				}
				for (; i < _endFahrstrassen.Count;) {
					CheckComboBoxTest.CheckedComboBox altesControl = _endFahrstrassen[_endFahrstrassen.Count - 1];
					_endFahrstrassen.RemoveAt(_endFahrstrassen.Count - 1);
					altesControl.DropDownClosed -= new System.EventHandler(this.checkedComboBox2_DropDownClosed);
					this.panel1.Controls.Remove(altesControl);
					altesControl.Dispose();
				}
			}
		}

		private void checkedComboBox1_DropDownClosed(object sender, EventArgs e) {
			CheckComboBoxTest.CheckedComboBox control = (CheckComboBoxTest.CheckedComboBox)sender;
			if (!control.ValueChanged) {
				return;
			}
			if (control.CheckedItems.Count == 0) {
				int i = 0;
				for (; i < _startFahrstrassen.Count; i++) {
					if (_startFahrstrassen[i] == control) {
						i++;
						break;
					}
				}
				for (; i < _startFahrstrassen.Count;) {
					CheckComboBoxTest.CheckedComboBox altesControl = _startFahrstrassen[_startFahrstrassen.Count - 1];
					_startFahrstrassen.RemoveAt(_startFahrstrassen.Count - 1);
					altesControl.DropDownClosed -= new System.EventHandler(this.checkedComboBox1_DropDownClosed);
					this.panel1.Controls.Remove(altesControl);
					altesControl.Dispose();
				}
				PositionierenControls();
			}
			else if (control.CheckedItems.Count == 1) {
				int i = 0;
				for (; i < _startFahrstrassen.Count; i++) {
					if (_startFahrstrassen[i] == control) {
						i++;
						break;
					}
				}
				CheckComboBoxTest.CheckedComboBox neuesControl = null;
				if (i == _startFahrstrassen.Count) {
					neuesControl = new CheckComboBoxTest.CheckedComboBox();
					this.panel1.Controls.Add(neuesControl);
					neuesControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
					neuesControl.CheckOnClick = true;
					neuesControl.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
					neuesControl.DropDownHeight = 1;
					neuesControl.FormattingEnabled = true;
					neuesControl.IntegralHeight = false;
					neuesControl.Size = new System.Drawing.Size(151, 21);
					neuesControl.Location = new System.Drawing.Point(control.Location.X, control.Location.Y - neuesControl.Size.Height - 5);
					neuesControl.Name = "checkedComboBox1_" + i;
					neuesControl.TabIndex = 0;
					neuesControl.Text = "";
					neuesControl.ValueSeparator = ", ";
					neuesControl.DropDownClosed += new System.EventHandler(this.checkedComboBox1_DropDownClosed);
					neuesControl.Enabled = true;

					_startFahrstrassen.Add(neuesControl);
					PositionierenControls();
				}
				else {
					neuesControl = _startFahrstrassen[i];

					i++;
					for (; i < _startFahrstrassen.Count;) {
						CheckComboBoxTest.CheckedComboBox altesControl = _startFahrstrassen[_startFahrstrassen.Count - 1];
						_startFahrstrassen.RemoveAt(_startFahrstrassen.Count - 1);
						altesControl.DropDownClosed -= new System.EventHandler(this.checkedComboBox1_DropDownClosed);
						this.panel1.Controls.Remove(altesControl);
						altesControl.Dispose();
					}
					PositionierenControls();
				}

				neuesControl.Items.Clear();
				neuesControl.Text = "";
				int zielSignalId = (int)control.CheckedItems[0];
				List<int> signale = new List<int>();
				foreach (FahrstrasseN fs in _anlagenElemente.FahrstrassenElemente.GespeicherteFahrstrassen) {
					if (zielSignalId == fs.EndSignal.ID) {
						if (!signale.Contains(fs.StartSignal.ID)) {
							signale.Add(fs.StartSignal.ID);
						}
					}
				}
				signale.Sort();
				foreach(int sig in signale) {
					neuesControl.Items.Add(sig);
				}
			}
			else {
				int i = 0;
				for (; i < _startFahrstrassen.Count; i++) {
					if (_startFahrstrassen[i] == control) {
						i++;
						break;
					}
				}
				for (; i < _startFahrstrassen.Count;) {
					CheckComboBoxTest.CheckedComboBox altesControl = _startFahrstrassen[_startFahrstrassen.Count - 1];
					_startFahrstrassen.RemoveAt(_startFahrstrassen.Count - 1);
					altesControl.DropDownClosed -= new System.EventHandler(this.checkedComboBox1_DropDownClosed);
					this.panel1.Controls.Remove(altesControl);
					altesControl.Dispose();
				}
				PositionierenControls();
			}

		}

		public string GetBefehlsString() {
			string bLString = "";
			for (int i = 0; i < dataGridView1.RowCount - 1; i++) {
				if (bLString != "") bLString += " ";
				bLString = bLString + dataGridView1[0, i].Value + ":" + dataGridView1[1, i].Value;
			}
			return bLString;
		}

		public List<List<FahrstrasseN>> GetFahrstrassenListen() {
			List<List<FahrstrasseN>> fahrstrassenListe = new List<List<FahrstrasseN>>();
			if((comboBoxStartSignal.SelectedItem) == null || (comboBoxZielSignal== null)) {
				return fahrstrassenListe;
			}
			{
				List<FahrstrasseN> fahrstrassen = new List<FahrstrasseN>();
				int startSignalId = (int)comboBoxStartSignal.SelectedItem;
				int zielSignalId = (int)comboBoxZielSignal.SelectedItem;
				foreach (FahrstrasseN fs in _anlagenElemente.FahrstrassenElemente.GespeicherteFahrstrassen) {
					if (startSignalId == fs.StartSignal.ID) {
						if (zielSignalId == fs.EndSignal.ID) {
							fahrstrassen.Add(fs);
						}
					}
				}
				fahrstrassenListe.Add(fahrstrassen);
			}

			foreach (CheckComboBoxTest.CheckedComboBox comboBox in _startFahrstrassen) {
				if (comboBox.CheckedItems.Count == 0) {
					continue;
				}
				if (comboBox.CheckedItems.Count == 1) {
					int startSignalId = (int)comboBox.CheckedItems[0];
					foreach (List<FahrstrasseN> fahrstrassenL in fahrstrassenListe) {
						int zielSignalId = (int)fahrstrassenL[0].StartSignal.ID;
						foreach (FahrstrasseN fs in _anlagenElemente.FahrstrassenElemente.GespeicherteFahrstrassen) {
							if (startSignalId == fs.StartSignal.ID) {
								if (zielSignalId == fs.EndSignal.ID) {
									fahrstrassenL.Insert(0, fs);
								}
							}
						}
					}
				}
				else {
					int length = fahrstrassenListe.Count;
					for (int i = 0; i < length; i++) {
						int zielSignalId = (int)fahrstrassenListe[0][0].StartSignal.ID;
						foreach (FahrstrasseN fs in _anlagenElemente.FahrstrassenElemente.GespeicherteFahrstrassen) {
							if (zielSignalId == fs.EndSignal.ID) {
								if (comboBox.CheckedItems.Contains(fs.StartSignal.ID)) {
									List<FahrstrasseN> fahrstrassen = fahrstrassenListe[0].ToList();
									fahrstrassen.Insert(0, fs);
									fahrstrassenListe.Add(fahrstrassen);
								}
							}
						}
						fahrstrassenListe.RemoveAt(0);
					}
				}
			}

			foreach (CheckComboBoxTest.CheckedComboBox comboBox in _endFahrstrassen) {
				if (comboBox.CheckedItems.Count == 0) {
					continue;
				}
				if (comboBox.CheckedItems.Count == 1) {
					int zielSignalId = (int)comboBox.CheckedItems[0];
					foreach (List<FahrstrasseN> fahrstrassenL in fahrstrassenListe) {
						int startSignalId = (int)fahrstrassenL.Last().EndSignal.ID;
						foreach (FahrstrasseN fs in _anlagenElemente.FahrstrassenElemente.GespeicherteFahrstrassen) {
							if (startSignalId == fs.StartSignal.ID) {
								if (zielSignalId == fs.EndSignal.ID) {
									fahrstrassenL.Add(fs);
								}
							}
						}
					}
				}
				else {
					int length = fahrstrassenListe.Count;
					for (int i = 0; i < length; i++) {
						int startSignalId = (int)fahrstrassenListe[0].Last().EndSignal.ID;
						foreach (FahrstrasseN fs in _anlagenElemente.FahrstrassenElemente.GespeicherteFahrstrassen) {
							if (startSignalId == fs.StartSignal.ID) {
								if (comboBox.CheckedItems.Contains(fs.EndSignal.ID)) {
									List<FahrstrasseN> fahrstrassen = fahrstrassenListe[0].ToList();
									fahrstrassen.Add(fs);
									fahrstrassenListe.Add(fahrstrassen);
								}
							}
						}
						fahrstrassenListe.RemoveAt(0);
					}
				}
			}

			return fahrstrassenListe;
		}
	}
}
