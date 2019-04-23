using MoBaSteuerung;
using MoBaSteuerung.Elemente;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ModellBahnSteuerung.FahrstrassenEditor {
	
	public partial class FahrstrassenEditor : Form {
		public class FahrstrassenAnzeige {
			private List<FahrstrasseN> _fahrstrassen;

			public List<FahrstrasseN> Fahrstrassen {
				get {
					return _fahrstrassen;
				}

				set {
					_fahrstrassen = value;
				}
			}
		}
		private bool _beenden;
		private Model _model;
		private FahrstrassenAnzeige _fahrstrassenAnzeige = new FahrstrassenAnzeige();
		private DataTable _tabelle;
		private int _ausgewählteFahrstrasse = -1;

		public FahrstrassenEditor(Model model) {
			InitializeComponent();

			_model = model;
			_beenden = false;
			dataGridView1.AutoGenerateColumns = false;
		}


		private void FahrstrassenEditor_FormClosing(object sender, FormClosingEventArgs e) {
			e.Cancel = !this._beenden;
			this.Hide();
		}

		/// <summary>
		/// 
		/// </summary>
		public void Beenden() {
			this._beenden = true;
			this.Close();
		}

		private void FahrstrassenEditor_Shown(object sender, EventArgs e) {
			this.Hide();
			this.Opacity = 1;
		}
		
		private void alleFahrstraßenSuchenToolStripMenuItem_Click(object sender, EventArgs e) {
			DialogResult result = MessageBox.Show("Alle Fahrstraßen automatisch neu suchen?", "Caption", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result == DialogResult.Yes) {
				_model.FahrstrassenSuchen();
				AktualisierenTabelle();
				this.propertyGrid1.SelectedObject = _fahrstrassenAnzeige;
			}
		}


		public void AktualisierenTabelle(){
			_fahrstrassenAnzeige.Fahrstrassen = _model.ZeichnenElemente.FahrstrassenElemente.GespeicherteFahrstrassen;

			_tabelle = new DataTable();
			_tabelle.Columns.Add("ID", typeof(int));
			_tabelle.Columns.Add("Start", typeof(int));
			_tabelle.Columns.Add("Ziel", typeof(int));
			_tabelle.Columns.Add("Start-Befehle", typeof(string));
			_tabelle.Columns.Add("End-Befehle", typeof(string));

			//DataGridViewRowCollection rowCollection = dataGridView1.Rows;
			//rowCollection.Clear();
			foreach (FahrstrasseN fahrStrasse in _fahrstrassenAnzeige.Fahrstrassen) {
				_tabelle.Rows.Add(
					fahrStrasse.ID,
					fahrStrasse.StartSignal.ID,
					fahrStrasse.EndSignal.ID,
					fahrStrasse.StartBefehleString.Trim().Replace("\t", "; "),
					fahrStrasse.EndBefehleString.Trim().Replace("\t", "; ")
					);

			}


			dataGridView1.DataSource = _tabelle;
			comboBox1.SelectedIndex = 0;
			comboBox1_SelectedIndexChanged(comboBox1, null);
		}

		private void fahrstraßenSuchenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			List<int> stopGleise = new List<int>();
			string[] gleise = this.textBox1.Text.Split(',');
			foreach(string gleis in gleise) {
				try {
					int num = Convert.ToInt32(gleis);
					stopGleise.Add(num);
				}
				catch (Exception exception) {

				}
				
			}

			_model.FahrstrassenSuchenVonSignal(stopGleise);
			AktualisierenTabelle();
			this.propertyGrid1.SelectedObject = _fahrstrassenAnzeige;
		}

		private void dataGridView1_SelectionChanged(object sender, EventArgs e)
		{
			DataGridViewRow row = dataGridView1.CurrentRow;
			if (row != null) {
				int id = (int)row.Cells[0].Value;
				if(id != _ausgewählteFahrstrasse) {
					_ausgewählteFahrstrasse = id;
					foreach(FahrstrasseN el in _model.ZeichnenElemente.FahrstrassenElemente.AuswahlFahrstrassen) {
						el.Selektiert = false;
					}
					_model.ZeichnenElemente.FahrstrassenElemente.AuswahlFahrstrassen.Clear();
					FahrstrasseN fahrstrasse = _model.ZeichnenElemente.FahrstrassenElemente.Fahrstrasse(_ausgewählteFahrstrasse);
					if (fahrstrasse != null) {
						fahrstrasse.Selektiert = true;
						_model.ZeichnenElemente.FahrstrassenElemente.AuswahlFahrstrassen.Add(fahrstrasse);
						_model.OnAnlageNeuZeichnen();
					}

				}
			}
			else {

			}
		}

		private void FahrstrassenEditor_VisibleChanged(object sender, EventArgs e) {
			if (this.Visible) {
				AktualisierenTabelle();
			}
			else {
				foreach (FahrstrasseN el in _model.ZeichnenElemente.FahrstrassenElemente.AuswahlFahrstrassen) {
					el.Selektiert = false;
				}
				_model.ZeichnenElemente.FahrstrassenElemente.AuswahlFahrstrassen.Clear();
				_model.OnAnlageNeuZeichnen();
				_ausgewählteFahrstrasse = -1;
			}
		}

		private void fahrstraßenSpeichernToolStripMenuItem_Click(object sender, EventArgs e) {
			List<int> ids = new List<int>();
			foreach (DataRow row in _tabelle.Rows) {
				FahrstrasseN fs = null;
				int id = (int)row[0];//Convert.ToInt32(row[0]);
				ids.Add(id);

				foreach (FahrstrasseN el in _fahrstrassenAnzeige.Fahrstrassen) {
					if(el.ID == id) {
						fs = el;
						break;
					}
				}

				fs.StartBefehleString = (string)row[3];
				fs.EndBefehleString = (string)row[4];
			}


			for(int i = 0; i < _fahrstrassenAnzeige.Fahrstrassen.Count;) {
				if (ids.Contains(_fahrstrassenAnzeige.Fahrstrassen[i].ID)) {
					i++;
				}
				else {
					_fahrstrassenAnzeige.Fahrstrassen.RemoveAt(i);
				}
			}

		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			string rowFilter = string.Empty;
			switch(comboBox1.SelectedIndex){
				case 0:
					break;
				case 1:
					rowFilter = string.Format("[{0}] = '{1}'", "Start", (int)numericUpDown1.Value);
					break;
				case 2:
					rowFilter = string.Format("[{0}] = '{1}'", "Ziel", (int)numericUpDown1.Value);
					break;
			}
			(dataGridView1.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			comboBox1_SelectedIndexChanged(comboBox1, e);
		}

		private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
			int column = e.ColumnIndex;
			if(column > 2) {
				DataGridViewRow row = ((DataGridView)sender).Rows[e.RowIndex];
				DataGridViewCell idCell = row.Cells[0];
				int id = (int)idCell.Value;

				FahrstrasseN fs = null;
				foreach(FahrstrasseN el in _fahrstrassenAnzeige.Fahrstrassen) {
					if(el.ID == id) {
						fs = el;
						break;
					}
				}

				if (fs != null) {
					BefehlsListe bListe = null;
					Form frm = null;
					if (column == 3) {
						bListe = new BefehlsListe(_model.ZeichnenElemente, false, (string)row.Cells[3].Value);
						frm = new FrmBefehlsliste(bListe, "Start Befehle");
					}
					else if (column == 4) {
						bListe = new BefehlsListe(_model.ZeichnenElemente, false, (string)row.Cells[4].Value);
						frm = new FrmBefehlsliste(bListe, "End Befehle");
					}

					if(bListe != null && frm != null) {
						frm.ShowDialog();
						string newValue = bListe.ListenString.Trim().Replace(";", "");
						if (column == 3) {
							row.Cells[3].Value = newValue.Replace(" ", "; ");
						}
						else if (column == 4) {
							row.Cells[4].Value = newValue.Replace(" ", "; "); ;
						}
					}
				}
				
			}
		}
	}
}
