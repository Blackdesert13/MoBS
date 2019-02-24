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
        }
    }
}
