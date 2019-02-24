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

		public FahrstrassenEditor(Model model) {
			InitializeComponent();

			_model = model;
			_beenden = false;
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
			_model.FahrstrassenSuchen();
			_fahrstrassenAnzeige.Fahrstrassen = _model.ZeichnenElemente.FahrstarssenElemente.GespeicherteFahrstrassen;
			this.propertyGrid1.SelectedObject = _fahrstrassenAnzeige;
		}
	}
}
