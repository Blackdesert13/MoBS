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
		public class FahrstrassenKAnzeige {
			private List<FahrstrasseK> _fahrstrassen;

			public List<FahrstrasseK> Fahrstrassen {
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
		private FahrstrassenKAnzeige _fahrstrassenKAnzeige = new FahrstrassenKAnzeige();
		private DataTable _tabelle;
		private DataTable _tabelleK;
		private int _ausgewählteFahrstrasse = -1;
    private int _heightMenuStrip = 24;
		private int _ausgewählteKombiFahrstrasse = -1;

		public FahrstrassenEditor(Model model) {
			InitializeComponent();

            _heightMenuStrip = menuStrip1.Height;
			_model = model;
			_beenden = false;
			dataGridView1.AutoGenerateColumns = false;
			dataGridView2.AutoGenerateColumns = false;
			//tabControl1_SelectedIndexChanged(tabControl1, new EventArgs());

			this.dataGridView2.SelectionChanged -= dataGridView2_SelectionChanged;
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
			_fahrstrassenKAnzeige.Fahrstrassen = _model.ZeichnenElemente.FahrstrassenKElemente.Elemente;

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

			_tabelleK = new DataTable();
			_tabelleK.Columns.Add("ID", typeof(int));
			_tabelleK.Columns.Add("Start", typeof(int));
			_tabelleK.Columns.Add("Ziel", typeof(int));
			_tabelleK.Columns.Add("Fahrstraßen", typeof(string));

			//DataGridViewRowCollection rowCollection = dataGridView1.Rows;
			//rowCollection.Clear();
			foreach (FahrstrasseK fahrStrasseK in _fahrstrassenKAnzeige.Fahrstrassen) {
				_tabelleK.Rows.Add(
					fahrStrasseK.ID,
					fahrStrasseK.StartSignal.ID,
					fahrStrasseK.EndSignal.ID,
					fahrStrasseK.FahrstrassenString.Trim().Replace("\t", "; ")
					);

			}
			
			dataGridView2.DataSource = _tabelleK;
			comboBox2.SelectedIndex = 0;
			comboBox2_SelectedIndexChanged(comboBox2, null);
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

			List<int> kIds = new List<int>();
			foreach (DataRow row in _tabelleK.Rows) {
				FahrstrasseK fs = null;
				if((string)row[3] == "") {
					continue;
				}

				if (row[0].ToString() != "") {
					int id = (int)row[0];//Convert.ToInt32(row[0]);
					kIds.Add(id);

					foreach (FahrstrasseK el in _fahrstrassenKAnzeige.Fahrstrassen) {
						if (el.ID == id) {
							fs = el;
							break;
						}
					}
					fs.FahrstrassenString = (string)row[3];
				}
				else {
					int id = _model.ZeichnenElemente.FahrstrassenKElemente.SucheFreieNummer();
					fs = new FahrstrasseK(
							_model.ZeichnenElemente,
							_model.ZeichnenElemente.Zoom,
							MoBaSteuerung.Anlagenkomponenten.Enum.AnzeigeTyp.Bearbeiten,
							new string[] { "",  id.ToString()},
							 " FahrstrassenListe\t" + ((string)row[3]).Trim().Replace(";", "\t")
						);
					if(fs.StartSignal != null) {
						row[0] = id;
						row[1] = fs.StartSignal.ID;
						row[2] = fs.EndSignal.ID;

						kIds.Add(id);
					}
					
				}
			}

			for (int i = 0; i < _fahrstrassenKAnzeige.Fahrstrassen.Count;) {
				if (kIds.Contains(_fahrstrassenKAnzeige.Fahrstrassen[i].ID)) {
					i++;
				}
				else {
					_fahrstrassenKAnzeige.Fahrstrassen.RemoveAt(i);
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) {
            if(tabControl1.SelectedTab == this.tabPageKombiFs) {
				//this.menuStrip1.Enabled = false;
				this.dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;
				this.dataGridView2.SelectionChanged += dataGridView2_SelectionChanged;
				this.fahrstraßenSuchenToolStripMenuItem.Enabled = false;
				this.alleFahrstraßenSuchenToolStripMenuItem.Enabled = false;
				foreach (FahrstrasseN el in _model.ZeichnenElemente.FahrstrassenElemente.AuswahlFahrstrassen) {
					el.Selektiert = false;
				}
				_model.ZeichnenElemente.FahrstrassenElemente.AuswahlFahrstrassen.Clear();
				//this.dataGridView2_SelectionChanged(this.dataGridView2, null);
				_model.OnAnlageNeuZeichnen();
			}
            else {
				//this.menuStrip1.Enabled = true;
				this.dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
				this.dataGridView2.SelectionChanged -= dataGridView2_SelectionChanged;
				this.fahrstraßenSuchenToolStripMenuItem.Enabled = true;
				this.alleFahrstraßenSuchenToolStripMenuItem.Enabled = true;
				FahrstrasseK fskAlt = _model.ZeichnenElemente.FahrstrassenKElemente.Element(_ausgewählteKombiFahrstrasse);
				if (fskAlt != null) {
					fskAlt.Selektiert = false;
				}
				//this.dataGridView2_SelectionChanged(this.dataGridView2, null);
				_model.OnAnlageNeuZeichnen();
			}
        }

		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) {
			string rowFilter = string.Empty;
			switch (comboBox2.SelectedIndex) {
				case 0:
					break;
				case 1:
					rowFilter = string.Format("[{0}] = '{1}'", "Start", (int)numericUpDown2.Value);
					break;
				case 2:
					rowFilter = string.Format("[{0}] = '{1}'", "Ziel", (int)numericUpDown2.Value);
					break;
			}
			(dataGridView2.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
		}

		private void numericUpDown2_ValueChanged(object sender, EventArgs e) {
			comboBox2_SelectedIndexChanged(comboBox2, e);
		}

		private void dataGridView2_SelectionChanged(object sender, EventArgs e) {
			DataGridViewRow row = dataGridView2.CurrentRow;
			if (row != null) {
				int id = (int)row.Cells[0].Value;
				if (id != _ausgewählteKombiFahrstrasse) {
					FahrstrasseK fskAlt = _model.ZeichnenElemente.FahrstrassenKElemente.Element(_ausgewählteKombiFahrstrasse);
					if (fskAlt != null) {
						fskAlt.Selektiert = false;
					}
					_ausgewählteKombiFahrstrasse = id;
					FahrstrasseK fskNeu = _model.ZeichnenElemente.FahrstrassenKElemente.Element(id);
					fskNeu.Selektiert = true;
					_model.OnAnlageNeuZeichnen();
				}
			}
		}

		private void dataGridView2_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) {

		}

		private void dataGridView2_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) {

		}

		private void neueKombiFahrstrasse_Click(object sender, EventArgs e) {
			frmKombiFahrstrasse frm = new frmKombiFahrstrasse();
			if (frm.ShowDialog(this) == DialogResult.OK) {

			}
			else {

			}
		}
	}
}
