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
    public partial class frmProperties : Form
    {
        public frmProperties()
        {
            InitializeComponent();
        }
        public frmProperties(AnlagenElement AElement)
        {
            InitializeComponent();
            propertyGrid1.SelectedObject = AElement;
        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }
    }
}
