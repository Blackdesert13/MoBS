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

			foreach (FahrstrasseN fs in _anlagenElemente.FahrstrassenElemente.GespeicherteFahrstrassen) {
				if (!_startSignalComboBoxItems.Contains(fs.StartSignal.ID)) {
					_startSignalComboBoxItems.Add(fs.StartSignal.ID);
				}
				if (!_zielSignalComboBoxItems.Contains(fs.EndSignal.ID)) {
					_zielSignalComboBoxItems.Add(fs.EndSignal.ID);
				}
			}
			int a = 10;
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

		private void comboBoxStartSignal_SelectedValueChanged(object sender, EventArgs e) {
			if (comboBoxStartSignal.SelectedIndex != -1) {
				if (this._startPunkt == StartPunkt.NichtGewählt) {
					this._startPunkt = StartPunkt.StartSignal;
				}
				else if (this._startPunkt == StartPunkt.StartSignal) {
					//Todo: ResetEndeFahrstrassen();
					//Todo: DisableEndeFahrstrassen();
				}
				//Todo: ResetBeginnFahrstrassen();
				if (this._startPunkt != StartPunkt.ZielSignal) {
					//Todo: DisableBeginnFahrstrassen();
					_zielSignalComboBoxItems.Clear();
					int startSignalId = (int)comboBoxStartSignal.SelectedItem;
					foreach (FahrstrasseN fs in _anlagenElemente.FahrstrassenElemente.GespeicherteFahrstrassen) {
						if (startSignalId == fs.StartSignal.ID) {
							if (!_zielSignalComboBoxItems.Contains(fs.EndSignal.ID)) {
								_zielSignalComboBoxItems.Add(fs.EndSignal.ID);
							}
						}
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

					checkedComboBox2.Items.Clear();
					int startSignalId = (int)comboBoxZielSignal.SelectedItem;
					foreach (FahrstrasseN fs in _anlagenElemente.FahrstrassenElemente.GespeicherteFahrstrassen) {
						if (startSignalId == fs.StartSignal.ID) {
							if (!checkedComboBox2.Items.Contains(fs.EndSignal.ID)) {
								checkedComboBox2.Items.Add(fs.EndSignal.ID);
							}
						}
					}

					checkedComboBox1.Items.Clear();
					int zielSignalId = (int)comboBoxStartSignal.SelectedItem;
					foreach (FahrstrasseN fs in _anlagenElemente.FahrstrassenElemente.GespeicherteFahrstrassen) {
						if (zielSignalId == fs.EndSignal.ID) {
							if (!checkedComboBox1.Items.Contains(fs.StartSignal.ID)) {
								checkedComboBox1.Items.Add(fs.StartSignal.ID);
							}
						}
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
					//Todo: ResetBeginnFahrstrassen();
					//Todo: DisableBeginnFahrstrassen();
				}
				//Todo: ResetEndeFahrstrassen();
				if (this._startPunkt != StartPunkt.StartSignal) {
					//Todo: DisableEndeFahrstrassen();
					_startSignalComboBoxItems.Clear();
					int zielSignalId = (int)comboBoxZielSignal.SelectedItem;
					foreach (FahrstrasseN fs in _anlagenElemente.FahrstrassenElemente.GespeicherteFahrstrassen) {
						if (zielSignalId == fs.EndSignal.ID) {
							if (!_startSignalComboBoxItems.Contains(fs.StartSignal.ID)) {
								_startSignalComboBoxItems.Add(fs.StartSignal.ID);
							}
						}
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

					checkedComboBox2.Items.Clear();
					int startSignalId = (int)comboBoxZielSignal.SelectedItem;
					foreach (FahrstrasseN fs in _anlagenElemente.FahrstrassenElemente.GespeicherteFahrstrassen) {
						if (startSignalId == fs.StartSignal.ID) {
							if (!checkedComboBox2.Items.Contains(fs.EndSignal.ID)) {
								checkedComboBox2.Items.Add(fs.EndSignal.ID);
							}
						}
					}
					
					checkedComboBox1.Items.Clear();
					int zielSignalId = (int)comboBoxStartSignal.SelectedItem;
					foreach (FahrstrasseN fs in _anlagenElemente.FahrstrassenElemente.GespeicherteFahrstrassen) {
						if (zielSignalId == fs.EndSignal.ID) {
							if (!checkedComboBox1.Items.Contains(fs.StartSignal.ID)) {
								checkedComboBox1.Items.Add(fs.StartSignal.ID);
							}
						}
					}
				}
			}
		}

		private void checkedComboBox1_SelectedIndexChanged(object sender, EventArgs e) {
			
		}

		private void checkedComboBox2_SelectedIndexChanged(object sender, EventArgs e) {

		}
	}
}
