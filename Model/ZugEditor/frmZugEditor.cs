using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using MoBaSteuerung.Elemente;
using MoBaSteuerung.ZeichnenElemente;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ModellBahnSteuerung.ZugEditor
{
    public partial class frmZugEditor : Form
    {
			private ElementListe<Zug> _zugElemente;
			private List<Zug> _zugListe;
			private AnlagenElemente _pa;
		  private int _signalNummer;
		
		public frmZugEditor(AnlagenElemente parent, int ZugNummer)
		{
			Konstruktor(parent,ZugNummer);
			foreach (DataGridViewRow zeile in this.dataGridView1.Rows)
			{

			}
		}
		public frmZugEditor( AnlagenElemente parent)
    {
			Konstruktor(parent,11);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="ZugNummer"></param>
		/// <returns></returns>
		private int Konstruktor(AnlagenElemente parent,int ZugNummer)
		{
			InitializeComponent();
			_pa = parent;
			_zugElemente = _pa.ZugElemente;
			_zugListe = _zugElemente.Elemente;
			int aktiveZeile = -1;
			foreach (Zug x in this._zugListe)
			{
				string[] zeile = {
										Convert.ToString(x.ID),
										Convert.ToString(x.SignalNummer),
										x.ZugTyp,
										x.Lok,
										x.Bezeichnung,
										Convert.ToString( x.AnkunftsZeit),//A-Zeit
                    Convert.ToString( x.Geschwindigkeit),
										Convert.ToString( x.DigitalAdresse),
										};
				dataGridView1.Rows.Add(zeile);
			}
			zugSuchen(ZugNummer);
			//this.dataGridView1.CurrentCell = this.dataGridView1[1, 3];
			return aktiveZeile;
		}

		private void zugSuchen(int Signal)
		{
			String searchValue = "somestring";
				int rowIndex = -1;
			/*foreach(DataGridViewRow row in dataGridView1.Rows)
			{
					if(row.Cells[1].Value.ToString().Equals(searchValue))
					{
							rowIndex = row.Index;
							break;
					}
			}*/
			string zn = Convert.ToString(Signal);
			foreach (DataGridViewRow zeile in dataGridView1.Rows)
			{
				if (zeile.Cells[1].Value.ToString().Equals(zn))
				{
					int z =zeile.Index;
					this.dataGridView1.CurrentCell = this.dataGridView1[1, z];
					break;
				}
			}

		}
		/// <summary>
		/// erstellt aus dem Formolar eine neue Liste
		/// </summary>
        private void zugListeNeu()
        {
			_zugListe.Clear();
            foreach(DataGridViewRow zeile in this.dataGridView1.Rows)
            {
                string[] elem = new string[10];
				elem[0] = "Zug";
				elem[1] = (string)zeile.Cells[0].Value; //ID
				elem[2] = (string)zeile.Cells[1].Value; //Signal
				elem[3] = (string)zeile.Cells[3].Value; //Lok
				elem[4] = (string)zeile.Cells[2].Value; //Typ
				elem[5] = (string)zeile.Cells[6].Value; //Geschwindigkeit
				elem[6] = (string)zeile.Cells[4].Value; //Bezeichnung
				elem[7] = "0"; //Länge
				elem[8] = (string)zeile.Cells[7].Value; //Digitale Addresse
				elem[9] = (string)zeile.Cells[5].Value; //Ankunftszeit
				if (elem[1] != null) { Zug zug = new Zug(_pa, 0, AnzeigeTyp.Bedienen, elem); }
			}
			_pa.ZugDateiSpeichern();
		}

		private void textBox1_TextChanged(object sender, EventArgs e) {

		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {

		}
		/// <summary>
		/// übernehmen
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button1_Click(object sender, EventArgs e) {
			zugListeNeu();
			this.DialogResult = DialogResult.OK;
			this.Close();

		}

		/// <summary>
		/// Abbruch
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button2_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
