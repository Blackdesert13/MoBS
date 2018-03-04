using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using MoBaSteuerung.Elemente;

namespace MoBaSteuerung.ToolBox
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ToolBoxElemente : Form
    {
        private bool beenden;
        private Model _model;
        /* /// <summary>
         /// 
         /// </summary>
         public ToolBoxElemente()
         {
             InitializeComponent();
             this.beenden = false;
         }*/
        public ToolBoxElemente(Model model)
        {
            _model = model;
            InitializeComponent();
            this.weiche.Model = _model;
            this.gleis.Model = _model;
            this.schalter.Model = _model;
            this.fss.Model = _model;
            this.entkuppler.Model = _model;
            this.relaise.Model = _model;
            this.rueckMeldePlatine.Model = _model;
            this.beenden = false;


        }

        private void frmElemente_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !this.beenden;
            this.Hide();
            //Application.ExitThread();
        }

        private void ToolBoxElemente_Shown(object sender, EventArgs e)
        {
            Debug.Print("ToolBoxElemente_Shown");
            this.Hide();
            this.Opacity = 1;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Beenden()
        {
            this.beenden = true;
            this.Close();
        }

        private void Elemente_SizeChanged(object sender, EventArgs e)
        {
            // Workeround wegen Windoof

            //this.panel.ResumeLayout(false);
            //this.panel.PerformLayout();
            this.Padding = new Padding(5, 5, 1, 5);
            Application.DoEvents();
            this.Padding = new Padding(5, 5, 0, 5);
            //this.panel.SuspendLayout();
        }

        public void AktualisierenSelektierteElemente(List<AnlagenElement> auswahlElemente)
        {
            foreach(AnlagenElement x in auswahlElemente)
            {
                switch (x.GetType().Name)
                {
                    case "Knoten":
                        knoten.AktuellerKnoten = (Knoten)x;
                        break;
                    case "Gleis":
                        gleis.AktuellesGleis = (Gleis)x;
                        break;
                    case "Weiche":
                        weiche.AktuelleWeiche = (Weiche)x;
                        break;
                    case "FSS":
                        fss.AktuellerFSS = (FSS)x;
                        break;
                    case "Schalter":
                        break;
                    case "Entkuppler":
                        entkuppler.AktuellerEntkuppler = (Entkuppler)x;
                        break;
                }
            }
           
        }
    }
}