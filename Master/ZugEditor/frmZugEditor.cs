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
        private ElementListe<Zug> zugElemente;
        private List<Zug> zugListe;
        private AnlagenElemente pa;

       // public frmZugEditor(ElementListe<Zug> ZugElemente, AnlagenElemente parent)
        public frmZugEditor( AnlagenElemente parent)
        {
            InitializeComponent();
            pa = parent;
            zugElemente = pa.ZugElemente;
            zugListe = zugElemente.Elemente;
            foreach (Zug x in this.zugListe)
            {
                string[] zeile = {
                    Convert.ToString(x.ID),
                    Convert.ToString(x.SignalNummer),
                    x.Lok,
                    x.ZugTyp,
                    x.Geschwindigkeit,
                    x.Bezeichnung };
                dataGridView1.Rows.Add(zeile);
            }
        }

        /// <summary>
        /// erstellt aus dem Formolar eine neue Liste
        /// </summary>
        private void zugListeNeu()
        {
            zugListe.Clear();
            foreach(DataGridViewRow zeile in this.dataGridView1.Rows)
            {
                string[] elem = new string[7];
                elem[0] = "Zug";
                elem[1] = (string)zeile.Cells[0].Value;
                elem[2] = (string)zeile.Cells[1].Value;
                elem[3] = (string)zeile.Cells[2].Value;
                elem[4] = (string)zeile.Cells[3].Value;
                elem[5] = (string)zeile.Cells[4].Value;
                elem[6] = (string)zeile.Cells[5].Value;
                if (elem[1] != null) { Zug zug = new Zug(pa, 0, AnzeigeTyp.Bedienen, elem); }               
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        /// <summary>
        /// übernehmen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            zugListeNeu();
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        /// <summary>
        /// Abbruch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
