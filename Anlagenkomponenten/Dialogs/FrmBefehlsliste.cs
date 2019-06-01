using MoBaSteuerung.Elemente;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ModellBahnSteuerung
{
    public partial class FrmBefehlsliste : Form
    {
        BefehlsListe _bl;
        public FrmBefehlsliste(BefehlsListe BefehlsList,string Name)
        {
            InitializeComponent();
            _bl = BefehlsList;
            labelName.Text = Name;
            byte zeile = 0;
            foreach (Befehl x in _bl.BefListe)
            {             
                dataGridView1.Rows.Add();
                dataGridView1[0, zeile].Value = x.Element.KurzBezeichnung;
                dataGridView1[1, zeile].Value = x.Attribut;
                zeile++;
            }
        }

        private void buttonUebernahme_Click(object sender, EventArgs e)
        {
            string bLString = "";
           // foreach(Row in dataGridView1)
           for(int i=0; i<dataGridView1.RowCount-1; i++)
           {
                if (bLString != "") bLString += " ";
                bLString = bLString + dataGridView1[0, i].Value + ":" + dataGridView1[1, i].Value;
           }
            _bl.ListenString = bLString;
            //_bl.ListeNeu ( bLString);
            _bl.ListeAktivieren();
        }
    }
}
