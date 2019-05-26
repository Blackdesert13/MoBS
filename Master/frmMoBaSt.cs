using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Dialogs;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using MoBaSteuerung.Dialoge;
using ModellBahnSteuerung.ZugEditor;
using MoBaSteuerung.ToolBox;
using MoBaSteuerung.Anlagenkomponenten.MCSpeicher;
using MoBaSteuerung.Elemente;
using System.Collections.Generic;
using ModellBahnSteuerung;
using System.Diagnostics;
using ModellBahnSteuerung.FahrstrassenEditor;

namespace MoBaSteuerung {
	/// <summary>
	/// Hauptform View
	/// </summary>
	public partial class MoBaStForm : Form {

		public enum WM : uint {
			DEVICE_CHANGE = 0x0219,             // device change event
		}

		protected override void WndProc(ref Message m) {
			switch ((WM)m.Msg) {
				case WM.DEVICE_CHANGE:
					if (_model != null) {
						string result = _model.UpdateComPorts();
						if (result != "") {
							toolStripStatusLabelInfo.Text = result;
						}
					}
					break;
			}

			base.WndProc(ref m);
		}

		#region Private Felder

		private Controller _controller;
		private Model _model;
		private ToolBoxElemente _toolBoxElemente;
		private FahrstrassenEditor _fahrStraßenEditor;
		private int _signalNummer = -1;
		private DateTime timeZeit;
		private string _adminPwd = "MoBS_Mek49";
		private bool _adminAktiviert = false;
		private bool _anlageBearbeitenAktiviert = true;
		private ServoAction _servoKeyAction = ServoAction.None;

		#endregion

		#region Konstruktor(en)

		/// <summary>
		/// 
		/// </summary>
		public MoBaStForm() {
			this.InitializeComponent();
			// Model erzeugen einbinden
			this.InizializeModel();
			// Controller erzeugen einbinden
			this.InitializeController();
			((Control)this.pictureBoxView).AllowDrop = true;
			this.MouseWheel += MoBaStForm_MouseWheel;
			// erstellen der Menü Raster Bilder
			this.InitializeMenüGitterImages();
			// erstellen der Menü Fadenkreuz Bilder
			this.InitializeMenüFadenkreuzImages();
			// Zuletzt geladene Anlagen
			this.InitializeZuletztGeladenAnlagen();
			// Zuletzt verbunden Master
			this.InitializeZuletztVerbundeneMaster();

			this.fadenkreuzStift = new Pen(this.fadenkreuzFarbe, 1);

			this.auswahlRechteck = new GraphicsPath();
			this.auswahlRechteckStift = new Pen(new SolidBrush(Color.Blue));
			this.auswahlRechteckStift.DashPattern = new float[] { 3, 3 };

			this.bearbeitungsModus = BearbeitungsModus.Selektieren;


			this.timerZeit.Enabled = true;
			this.timeZeit = this.Controller.AktuelleZeit();
			this.toolStripStatusLabelZeit.Text = DateTime.Now.ToString("HH:mm:ss");
			//this._adminAktiviert = false;
			this.toolStripMenuItemBearbeiten.Enabled = false;
		}

		#endregion

		#region Private Methoden

		#region Initialize

		private void InizializeModel() {
			this.Model = new Model();
			if (Controller.AppTyp == AppTyp.Master)
				this.Model.Master = true;
		}

		private void InitializeController() {
			this.Controller = new Controller(Model);
			this.Controller.FileLoaded += Controller_AnlageGeladen;
			this.Controller.FileSave += Controller_FileSave;
			this.Controller.FileSaved += Controller_FileSaved;
			this.Controller.FileNew += Controller_FileNew;
			this.Controller.MasterConnected += Controller_MasterVerbunden;
			this.Controller.ZoomChanged += this.Controller_ZoomChange;
			this.Controller.AnlageGrößeInRasterChanged += Controller_AnlageGrößeInRasterChanged;
			this.Controller.AppTypChanged += Controller_AppTypChanged;
			this.Controller.ViewTypChanged += Controller_ViewTypChanged;
			this.Controller.ViewNeuZeichnen += Controller_ViewNeuZeichnen;
		}

		private void InitializeMenüGitterImages() {
			Image image;
			Graphics graphics;

			// Raster Linie
			image = new Bitmap(24, 24);
			graphics = Graphics.FromImage(image);
			graphics.DrawRectangle(Pens.Black, 2, 2, 18, 18);
			this.toolStripMenuItemGitterGitterLinie.Image = image;

			// Raster Linie Versetzt
			image = new Bitmap(24, 24);
			graphics = Graphics.FromImage(image);
			graphics.DrawLine(Pens.Black, 11, 2, 11, 19);
			graphics.DrawLine(Pens.Black, 2, 11, 19, 11);
			this.toolStripMenuItemGitterGitterLinieVersetzt.Image = image;

			// Raster Punkt
			image = new Bitmap(24, 24);
			graphics = Graphics.FromImage(image);
			graphics.FillRectangle(Brushes.Black, 2, 2, 2, 2);
			graphics.FillRectangle(Brushes.Black, 2, 18, 2, 2);
			graphics.FillRectangle(Brushes.Black, 18, 2, 2, 2);
			graphics.FillRectangle(Brushes.Black, 18, 18, 2, 2);
			this.toolStripMenuItemGitterGitterPunkt.Image = image;

			// Raster Punkt Versetzt
			image = new Bitmap(24, 24);
			graphics = Graphics.FromImage(image);
			graphics.FillRectangle(Brushes.Black, 10, 10, 3, 3);
			this.toolStripMenuItemGitterGitterPunktVersetzt.Image = image;

			// Raster Farbe   
			this.toolStripMenuItemGitterGitterFarbe.Image = this.MenüFarbe(global::ModellBahnSteuerung.Properties.Settings.Default.ViewGitterFarbe);

			// Typ zuweisen
			switch (global::ModellBahnSteuerung.Properties.Settings.Default.ViewGitterTyp.ToGitterTyp()) {
				case GitterTyp.Linie:
					this.toolStripSplitButtonCheckableGitter.Image = this.toolStripMenuItemGitterGitterLinie.Image;
					break;
				case GitterTyp.LinieVersetzt:
					this.toolStripSplitButtonCheckableGitter.Image = this.toolStripMenuItemGitterGitterLinieVersetzt.Image;
					break;
				case GitterTyp.Punkt:
					this.toolStripSplitButtonCheckableGitter.Image = this.toolStripMenuItemGitterGitterPunkt.Image;
					break;
				case GitterTyp.PunktVersetzt:
					this.toolStripSplitButtonCheckableGitter.Image = this.toolStripMenuItemGitterGitterPunktVersetzt.Image;
					break;
			}

			graphics.Dispose();
		}

		private void InitializeMenüFadenkreuzImages() {
			Image image;
			Graphics graphics;

			// Fadenkreuz Linie
			image = new Bitmap(24, 24);
			graphics = Graphics.FromImage(image);
			graphics.DrawLine(Pens.Black, 11, 2, 11, 19);
			graphics.DrawLine(Pens.Black, 2, 11, 19, 11);
			this.toolStripMenuItemFadenkreuzFadenkreuzLinie.Image = image;

			// Fadenkreuz Punkte
			image = new Bitmap(24, 24);
			graphics = Graphics.FromImage(image);
			graphics.DrawLine(Pens.Black, 11, 2, 11, 3);
			graphics.DrawLine(Pens.Black, 11, 6, 11, 7);
			graphics.DrawLine(Pens.Black, 11, 14, 11, 15);
			graphics.DrawLine(Pens.Black, 11, 18, 11, 19);

			graphics.DrawLine(Pens.Black, 2, 11, 3, 11);
			graphics.DrawLine(Pens.Black, 6, 11, 7, 11);
			graphics.DrawLine(Pens.Black, 14, 11, 15, 11);
			graphics.DrawLine(Pens.Black, 18, 11, 19, 11);
			this.toolStripMenuItemFadenkreuzFadenkreuzPunkt.Image = image;

			// Fadenkreuz Farbe 
			this.toolStripMenuItemFadenkreuzFadenkreuzFarbe.Image = this.MenüFarbe(global::ModellBahnSteuerung.Properties.Settings.Default.ViewFadenkreuzFarbe);

			// Typ zuweisen
			switch (global::ModellBahnSteuerung.Properties.Settings.Default.ViewFadenkreuzTyp.ToFadenkreuzTyp()) {
				case FadenkreuzTyp.Linie:
					this.toolStripSplitButtonCheckableFadenkreuz.Image = this.toolStripMenuItemFadenkreuzFadenkreuzLinie.Image;
					break;
				case FadenkreuzTyp.Punkte:
					this.toolStripSplitButtonCheckableFadenkreuz.Image = this.toolStripMenuItemFadenkreuzFadenkreuzPunkt.Image;
					break;
			}

			graphics.Dispose();
		}

		private void InitializeZuletztGeladenAnlagen() {
			int count = 0;
			foreach (var item in global::ModellBahnSteuerung.Properties.Settings.Default.ZuletztGeladenAnlagen.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)) {
				count++;
				string s = DateipfadEinkürzen(item, 120, this.toolStripMenuItemDateiZuletztGeöfnetteAnlage.Font);
				this.toolStripMenuItemDateiZuletztGeöfnetteAnlage.DropDownItems.Add(new ToolStripMenuItem(count.ToString() + " " + s, null, this.toolStripMenuItemDateiZuletztGeladenAnlage_Click, "ZuletztGeöfnetteAnlage" + count.ToString()));
				this.toolStripMenuItemDateiZuletztGeöfnetteAnlage.DropDownItems[this.toolStripMenuItemDateiZuletztGeöfnetteAnlage.DropDownItems.Count - 1].Tag = item;

				// wenn nicht gekürzt wurde, dann kein ToolTip anzeigen.
				if (item != s) {
					this.toolStripMenuItemDateiZuletztGeöfnetteAnlage.DropDownItems[this.toolStripMenuItemDateiZuletztGeöfnetteAnlage.DropDownItems.Count - 1].ToolTipText = item;
				}

				// max 9 anzeigen.
				if (count == 9) {
					continue;
				}
			}

			if (this.toolStripMenuItemDateiZuletztGeöfnetteAnlage.DropDownItems.Count > 0) {
				this.toolStripMenuItemDateiZuletztGeöfnetteAnlage.Enabled = true;
			}
		}

		private void InitializeZuletztVerbundeneMaster() {
			int count = 0;
			foreach (var item in global::ModellBahnSteuerung.Properties.Settings.Default.ZuletztVerbundeneMaster.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)) {
				count++;
				this.toolStripMenuItemDateiZuletztVerbundeneMaster.DropDownItems.Add(new ToolStripMenuItem(count.ToString() + " " + item, null, this.toolStripMenuItemDateiZuletztVerbundeneMaster_Click, "ZuletztVerbundeneMaster" + count.ToString()));
				this.toolStripMenuItemDateiZuletztVerbundeneMaster.DropDownItems[this.toolStripMenuItemDateiZuletztVerbundeneMaster.DropDownItems.Count - 1].Tag = item;

				// max 9 anzeigen.
				if (count == 9) {
					continue;
				}
			}

			if (this.toolStripMenuItemDateiZuletztVerbundeneMaster.DropDownItems.Count > 0) {
				this.toolStripMenuItemDateiZuletztVerbundeneMaster.Enabled = true;
			}
		}

		private void InitializeFahrstrassenEditor() {
			this._fahrStraßenEditor = new FahrstrassenEditor(Model);
			this._fahrStraßenEditor.FormClosing += FahrStraßenEditor_FormClosing; ;
			this._fahrStraßenEditor.Location = this.Location;
			this._fahrStraßenEditor.Opacity = 0;
			this._fahrStraßenEditor.Show(this);
		}


		private void InitializeToolBoxElemente() {
			this._toolBoxElemente = new ToolBox.ToolBoxElemente(Model);
			this._toolBoxElemente.FormClosing += ToolBoxElemente_FormClosing;
			this._toolBoxElemente.Location = this.Location;
			this._toolBoxElemente.Opacity = 0;
			this._toolBoxElemente.Show(this);
		}

		#endregion

		#region frmMoBaSt Events

		private void MoBaStForm_Load(object sender, EventArgs e) {
			// ToolBox für die Eigenschaften der Elemente
			this.InitializeToolBoxElemente();
			this.InitializeFahrstrassenEditor();
		}

		private void frmMoBaSt_FormClosing(object sender, FormClosingEventArgs e) {
			trennenToolStripMenuItem_Click(null, null);
			e.Cancel = (this.MasterAnlageSpeichern() || this.SlaveVerbindungTrennen());
		}

		private void TimerZeit_Tick(object sender, EventArgs e) {
			// ToDo zeit muß vom Controller kommen, damit alle synron Laufen
			this.timeZeit = this.Controller.AktuelleZeit();
			this.toolStripStatusLabelZeit.Text = this.timeZeit.ToString("HH:mm:ss");

		}

		#endregion

		#region Controller Events

		private void Controller_FileNew(string e) {
			this.Text = "Modellbahnsteuerung Master [" + e + "]";
		}

		private void Controller_AnlageGeladen(string e) {
			this.ZuleztGeladeneAnlagenHinzufügen(e);
		}

		private void Controller_FileSave(bool e) {
			// wenn gespeichert werden sollte, true
			if (e) {
				// * anhängen
				this.Text = this.Text.TrimEnd('*');
				this.Text += "*";
			}
			else {
				// * am Ende löschen
				this.Text = this.Text.TrimEnd('*');
			}
		}

		private void Controller_FileSaved(string e) {
			this.ZuleztGeladeneAnlagenHinzufügen(e);
		}

		private void Controller_MasterVerbunden(string e) {
			this.ZuleztVerbundeMasterHinzufügen(e);
		}

		private void Controller_ZoomChange(int e) {
			this.toolStripStatusLabelZoomInfo.Text = e.ToString();
			this.Aktualisieren();
		}

		private void Controller_AnlageGrößeInRasterChanged(Size e) {
			this.Aktualisieren();
		}

		private void Controller_AppTypChanged(AppTyp e) {
			this.Aktualisieren();
		}

		private void Controller_ViewTypChanged(AnzeigeTyp e) {
			this.Aktualisieren();
		}

		private void Controller_ViewNeuZeichnen() {
			this.pictureBoxView.Invalidate();
		}

		#endregion

		#region ToolBoxElemente Events

		private void ToolBoxElemente_FormClosing(object sender, FormClosingEventArgs e) {
			this.toolStripButtonElementeEigenschaften.Checked = false;
		}

		#endregion

		private void FahrStraßenEditor_FormClosing(object sender, FormClosingEventArgs e) {
			this.toolStripButtonFahrstrassenEditor.Checked = false;
		}

		#region Menü Events

		#region Datei

		private void toolStripMenuItemDateiNeu_Click(object sender, EventArgs e) {
			// Controller fragen, ob gespeichert werden müsste.  
			if (!this.MasterAnlageSpeichern()) {
				// 
				this.Controller.AnlageNeu();

				this.toolStripMenuItemDateiNeu.Enabled = true;
				this.toolStripMenuItemDateiLaden.Enabled = true;
				this.toolStripMenuItemDateiSchließen.Enabled = true;
				this.toolStripMenuItemDateiSpeichern.Enabled = true;
				this.toolStripMenuItemDateiSpeichernUnter.Enabled = true;
				this.toolStripMenuItemDateiVerbinden.Enabled = false;
				this.toolStripMenuItemDateiTrennen.Enabled = false;

				this.ZoomEnabled(true);
				this.toolStripMenuItemBearbeiten.Enabled = _anlageBearbeitenAktiviert;
				this.toolStripMenuItemBearbeiten.Checked = false;
				this.BearbeitungsModusZeichnenZurücksetzen();
				this.MenüBearbeitenDisabled();


				this.toolStripMenuItemExtrasServer.Enabled = true;
				this.toolStripMenuItemExtraArduino.Enabled = true;

				this.toolStripMenuItemDateiZuletztGeöfnetteAnlage.Enabled = this.toolStripMenuItemDateiZuletztGeöfnetteAnlage.DropDownItems.Count > 0;
				this.toolStripMenuItemDateiZuletztVerbundeneMaster.Enabled = false;

				this.panelView.Visible = true;
			}
		}

		private void toolStripMenuItemDateiLaden_Click(object sender, EventArgs e) {
			// Anlage laden.
			// Controller fragen, ob gespeichert werden müsste.  
			if (!this.MasterAnlageSpeichern()) {
				// Dialog      
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Title = "Anlage laden ...";
				openFileDialog.Filter = "Anlage Datei (*.anl)|*.anl|Alle Dateien (*.*)|*.*";
				openFileDialog.FilterIndex = 1;

				if (openFileDialog.ShowDialog() == DialogResult.OK) {
					this.Controller.AnlageLaden(openFileDialog.FileName);

					this.toolStripMenuItemDateiNeu.Enabled = true;
					this.toolStripMenuItemDateiLaden.Enabled = true;
					this.toolStripMenuItemDateiSchließen.Enabled = true;
					this.toolStripMenuItemDateiSpeichern.Enabled = true;
					this.toolStripMenuItemDateiSpeichernUnter.Enabled = true;
					this.toolStripMenuItemDateiVerbinden.Enabled = false;
					this.toolStripMenuItemDateiTrennen.Enabled = false;

					this.ZoomEnabled(true);
					this.toolStripMenuItemBearbeiten.Enabled = _anlageBearbeitenAktiviert;
					this.toolStripMenuItemBearbeiten.Checked = false;
					this.BearbeitungsModusZeichnenZurücksetzen();

					this.MenüBearbeitenDisabled();

					this.toolStripMenuItemExtrasServer.Enabled = true;
					this.toolStripMenuItemExtraArduino.Enabled = true;

					this.toolStripMenuItemDateiZuletztGeöfnetteAnlage.Enabled = this.toolStripMenuItemDateiZuletztGeöfnetteAnlage.DropDownItems.Count > 0;
					this.toolStripMenuItemDateiZuletztVerbundeneMaster.Enabled = false;

					this.panelView.Visible = true;
				}
			}
		}

		private void toolStripMenuItemDateiSchließen_Click(object sender, EventArgs e) {
			// Anlage schliessen.
			// Controller fragen, ob gespeichert werden müsste.  
			if (!this.MasterAnlageSpeichern()) {
				this.Controller.AnlageSchließen();

				this.toolStripMenuItemDateiNeu.Enabled = true;
				this.toolStripMenuItemDateiLaden.Enabled = true;
				this.toolStripMenuItemDateiSchließen.Enabled = false;
				this.toolStripMenuItemDateiSpeichern.Enabled = false;
				this.toolStripMenuItemDateiSpeichernUnter.Enabled = false;
				this.toolStripMenuItemDateiVerbinden.Enabled = true;
				this.toolStripMenuItemDateiTrennen.Enabled = false;

				this.ZoomEnabled(false);
				this.toolStripMenuItemBearbeiten.Enabled = false;
				this.toolStripMenuItemBearbeiten.Checked = false;
				this.BearbeitungsModusZeichnenZurücksetzen();
				this.MenüBearbeitenDisabled();

				this.toolStripMenuItemExtrasServer.Enabled = false;
				this.toolStripMenuItemExtraArduino.Enabled = false;

				this.toolStripMenuItemDateiZuletztGeöfnetteAnlage.Enabled = this.toolStripMenuItemDateiZuletztGeöfnetteAnlage.DropDownItems.Count > 0;
				this.toolStripMenuItemDateiZuletztVerbundeneMaster.Enabled = this.toolStripMenuItemDateiZuletztVerbundeneMaster.DropDownItems.Count > 0;

				this.panelView.Visible = false;

				this.Text = "Modellbahnsteuerung";
			}
		}

		private void toolStripMenuItemDateiSpeichern_Click(object sender, EventArgs e) {
			if (string.IsNullOrEmpty(this.Model.AnlageDateiPfadName)) {
				this.toolStripMenuItemDateiSpeichernUnter_Click(null, null);
			}
			else {
				this.Controller.AnlageSpeichern();
			}
		}

		private void toolStripMenuItemDateiSpeichernUnter_Click(object sender, EventArgs e) {
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Title = "Anlage speicher unter ...";
			if (string.IsNullOrEmpty(this.Model.AnlageDateiPfadName)) {
				saveFileDialog.InitialDirectory = "c:\\";
			}
			else {
				saveFileDialog.InitialDirectory = Path.GetDirectoryName(this.Model.AnlageDateiPfadName);
			}
			saveFileDialog.DefaultExt = "anl";
			saveFileDialog.FileName = this.Model.AnlageDateiName;
			saveFileDialog.Filter = "Anlage Datei (*.anl)|*.anl|Alle Dateien (*.*)|*.*";
			saveFileDialog.FilterIndex = 1;
			saveFileDialog.RestoreDirectory = true;

			if (saveFileDialog.ShowDialog() == DialogResult.OK) {
				this.Controller.AnlageSpeichernUnter(saveFileDialog.FileName);
			}
		}

		private void toolStripMenuItemDateiVerbinden_Click(object sender, EventArgs e) {
			this.SlaveMitMasterVerbinden();
		}

		private void toolStripMenuItemDateiTrennen_Click(object sender, EventArgs e) {
			try {
				// Slave (Client) trennen.
				if (this.Controller.SlaveVomMasterTrennen()) {
					this.toolStripMenuItemDateiNeu.Enabled = true;
					this.toolStripMenuItemDateiLaden.Enabled = true;
					this.toolStripMenuItemDateiSchließen.Enabled = false;
					this.toolStripMenuItemDateiSpeichern.Enabled = false;
					this.toolStripMenuItemDateiSpeichernUnter.Enabled = false;
					this.toolStripMenuItemDateiVerbinden.Enabled = true;
					this.toolStripMenuItemDateiTrennen.Enabled = false;

					this.ZoomEnabled(false);
					this.toolStripMenuItemBearbeiten.Enabled = false;
					this.toolStripMenuItemExtrasServer.Enabled = false;
					this.toolStripMenuItemExtraArduino.Enabled = false;
					this.BearbeitungsModusZeichnenZurücksetzen();

					this.toolStripMenuItemDateiZuletztGeöfnetteAnlage.Enabled = this.toolStripMenuItemDateiZuletztGeöfnetteAnlage.DropDownItems.Count > 0;
					this.toolStripMenuItemDateiZuletztVerbundeneMaster.Enabled = this.toolStripMenuItemDateiZuletztVerbundeneMaster.DropDownItems.Count > 0;

					this.panelView.Visible = false;

					this.Text = "Modellbahnsteuerung";
				}
			}
			catch (Exception ex) {
				Logging.Log.SchreibeException(ex);
			}
		}

		private void toolStripMenuItemDateiZuletztGeladenAnlage_Click(object sender, EventArgs e) {
			// Anlage laden.
			// Controller fragen, ob gespeichert werden müsste.  
			if (!this.MasterAnlageSpeichern()) {
				this.Controller.AnlageLaden((sender as ToolStripMenuItem).Tag.ToString());

				this.toolStripMenuItemDateiNeu.Enabled = true;
				this.toolStripMenuItemDateiLaden.Enabled = true;
				this.toolStripMenuItemDateiSchließen.Enabled = true;
				this.toolStripMenuItemDateiSpeichern.Enabled = true;
				this.toolStripMenuItemDateiSpeichernUnter.Enabled = true;
				this.toolStripMenuItemDateiVerbinden.Enabled = false;
				this.toolStripMenuItemDateiTrennen.Enabled = false;

				this.ZoomEnabled(true);
				this.toolStripMenuItemBearbeiten.Enabled = _anlageBearbeitenAktiviert;
				this.toolStripMenuItemBearbeiten.Checked = false;
				this.MenüBearbeitenDisabled();

				this.toolStripMenuItemExtrasServer.Enabled = true;
				this.toolStripMenuItemExtraArduino.Enabled = true;

				this.toolStripMenuItemDateiZuletztGeöfnetteAnlage.Enabled = this.toolStripMenuItemDateiZuletztGeöfnetteAnlage.DropDownItems.Count > 0;
				this.toolStripMenuItemDateiZuletztVerbundeneMaster.Enabled = false;

				this.panelView.Visible = true;
			}
		}

		private void toolStripMenuItemDateiZuletztVerbundeneMaster_Click(object sender, EventArgs e) {
			this.SlaveMitMasterVerbinden();
		}

		private void toolStripMenuItemDateiBeenden_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void SlaveMitMasterVerbinden() {
			try {
				// Controller fragen, ob gespeichert werden müsste.  
				if (!this.MasterAnlageSpeichern()) {
					this.Controller.SlaveMitMasterVerbinden(global::ModellBahnSteuerung.Properties.Settings.Default.MasterName);

					this.toolStripMenuItemDateiNeu.Enabled = false;
					this.toolStripMenuItemDateiLaden.Enabled = false;
					this.toolStripMenuItemDateiSchließen.Enabled = false;
					this.toolStripMenuItemDateiSpeichern.Enabled = false;
					this.toolStripMenuItemDateiSpeichernUnter.Enabled = false;
					this.toolStripMenuItemDateiVerbinden.Enabled = false;
					this.toolStripMenuItemDateiTrennen.Enabled = true;

					this.ZoomEnabled(true);
					this.toolStripMenuItemBearbeiten.Enabled = false;
					this.toolStripMenuItemExtrasServer.Enabled = false;
					this.toolStripMenuItemExtraArduino.Enabled = false;

					this.toolStripMenuItemDateiZuletztGeöfnetteAnlage.Enabled = false;
					this.toolStripMenuItemDateiZuletztVerbundeneMaster.Enabled = false;

					this.panelView.Visible = true;
					this.BearbeitungsModusZeichnenZurücksetzen();
					//this.controller.AppTyp = AppTyp.Slave;
				}
			}
			catch (Exception ex) {
				Logging.Log.SchreibeException(ex);
			}
		}

		#endregion

		#region Zoom

		private void toolStripMenuItemPlus_Click(object sender, EventArgs e) {
			this.Controller.Zoom = this.Controller.Zoom + 1;
		}

		private void toolStripMenuItemMinus_Click(object sender, EventArgs e) {
			this.Controller.Zoom = this.Controller.Zoom - 1;
		}

		private void toolStripMenuItemZurücksetzen_Click(object sender, EventArgs e) {
			this.Controller.ZoomStandard();
		}

		#endregion

		#region Bearbeiten

		private void toolStripMenuItemBearbeiten_Click(object sender, EventArgs e) {
			if (this.toolStripMenuItemBearbeiten.Checked) {
				this.toolStripBearbeiten.Visible = true;
				this.toolStripElemente.Visible = true;
				this.toolStripStatusLabelRaster.Visible = true;
				this.toolStripStatusLabelRasterInfo.Visible = true;
				if (this.toolStripButtonElementeEigenschaften.Checked) {
					this._toolBoxElemente.Show();
					this.Focus();
				}
				if (this.toolStripButtonFahrstrassenEditor.Checked) {
					this._fahrStraßenEditor.Show();
					this.Focus();
				}
				this.Controller.AnzeigeTyp = AnzeigeTyp.Bearbeiten;
			}
			else {
				this.toolStripBearbeiten.Visible = false;
				this.toolStripButtonElementGleis.Checked = false;
				this.toolStripButtonElementSignal.Checked = false;
				this.toolStripButtonElementSchalter.Checked = false;
				this.toolStripButtonElementEntkuppler.Checked = false;
				this.toolStripElemente.Visible = false;
				this.toolStripStatusLabelRaster.Visible = false;
				this.toolStripStatusLabelRasterInfo.Visible = false;
				this._toolBoxElemente.Hide();
				this._fahrStraßenEditor.Hide();
				this.Controller.AnzeigeTyp = AnzeigeTyp.Bedienen;
				this.Model.BearbeitenSelektionLöschen();
				this.BearbeitungsModusZeichnenZurücksetzen();
				//DialogResult result = MessageBox.Show("Alle Fahrstraßen automatisch neu suchen?","Caption", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				//if (result == DialogResult.Yes)
				//    this.Controller.FahrstrassenSuchen();


			}
		}

		#region Bearbeiten ToolStrip

		#region Gitter

		private void toolStripSplitButtonCheckableGitter_ButtonClick(object sender, EventArgs e) {
			this.toolStripSplitButtonCheckableGitter.Checked = !this.toolStripSplitButtonCheckableGitter.Checked;
			this.gitterTyp = global::ModellBahnSteuerung.Properties.Settings.Default.ViewGitterTyp.ToGitterTyp();
			this.gitterFarbe = global::ModellBahnSteuerung.Properties.Settings.Default.ViewGitterFarbe;
			this.gitterAnzeigen = this.toolStripSplitButtonCheckableGitter.Checked;
			this.Aktualisieren();
		}

		private void toolStripMenuItemGitterGitterLinie_Click(object sender, EventArgs e) {
			this.toolStripSplitButtonCheckableGitter.Image = this.toolStripMenuItemGitterGitterLinie.Image;
			global::ModellBahnSteuerung.Properties.Settings.Default.ViewGitterTyp = GitterTyp.Linie.ToString();
			global::ModellBahnSteuerung.Properties.Settings.Default.Save();
			this.gitterTyp = global::ModellBahnSteuerung.Properties.Settings.Default.ViewGitterTyp.ToGitterTyp();
			this.Aktualisieren();
		}

		private void toolStripMenuItemGitterGitterLinieVersetzt_Click(object sender, EventArgs e) {
			this.toolStripSplitButtonCheckableGitter.Image = this.toolStripMenuItemGitterGitterLinieVersetzt.Image;
			global::ModellBahnSteuerung.Properties.Settings.Default.ViewGitterTyp = GitterTyp.LinieVersetzt.ToString();
			global::ModellBahnSteuerung.Properties.Settings.Default.Save();
			this.gitterTyp = global::ModellBahnSteuerung.Properties.Settings.Default.ViewGitterTyp.ToGitterTyp();
			this.Aktualisieren();
		}

		private void toolStripMenuItemGitterGitterPunkt_Click(object sender, EventArgs e) {
			this.toolStripSplitButtonCheckableGitter.Image = this.toolStripMenuItemGitterGitterPunkt.Image;
			global::ModellBahnSteuerung.Properties.Settings.Default.ViewGitterTyp = GitterTyp.Punkt.ToString();
			global::ModellBahnSteuerung.Properties.Settings.Default.Save();
			this.gitterTyp = global::ModellBahnSteuerung.Properties.Settings.Default.ViewGitterTyp.ToGitterTyp();
			this.Aktualisieren();
		}

		private void toolStripMenuItemGitterGitterPunktVersetzt_Click(object sender, EventArgs e) {
			this.toolStripSplitButtonCheckableGitter.Image = this.toolStripMenuItemGitterGitterPunktVersetzt.Image;
			global::ModellBahnSteuerung.Properties.Settings.Default.ViewGitterTyp = GitterTyp.PunktVersetzt.ToString();
			global::ModellBahnSteuerung.Properties.Settings.Default.Save();
			this.gitterTyp = global::ModellBahnSteuerung.Properties.Settings.Default.ViewGitterTyp.ToGitterTyp();
			this.Aktualisieren();
		}

		private void toolStripMenuItemGitterGitterFarbe_Click(object sender, EventArgs e) {
			ColorDialogExtension colorDialog = new ColorDialogExtension(this.Location.X + ((this.Width - 225) / 2), this.Location.Y + ((this.Height - 330) / 2));
			colorDialog.Color = global::ModellBahnSteuerung.Properties.Settings.Default.ViewGitterFarbe;
			if (colorDialog.ShowDialog() == DialogResult.OK) {
				global::ModellBahnSteuerung.Properties.Settings.Default.ViewGitterFarbe = colorDialog.Color;
				global::ModellBahnSteuerung.Properties.Settings.Default.Save();
				this.gitterFarbe = global::ModellBahnSteuerung.Properties.Settings.Default.ViewGitterFarbe;
				this.toolStripMenuItemGitterGitterFarbe.Image = this.MenüFarbe(this.gitterFarbe);
				this.Aktualisieren();
			}
		}

		#endregion

		#region Fadenkreuz

		private void toolStripSplitButtonCheckableFadenkreuz_ButtonClick(object sender, EventArgs e) {
			this.toolStripSplitButtonCheckableFadenkreuz.Checked = !this.toolStripSplitButtonCheckableFadenkreuz.Checked;
			this.fadenkreuzTyp = global::ModellBahnSteuerung.Properties.Settings.Default.ViewFadenkreuzTyp.ToFadenkreuzTyp();
			this.fadenkreuzFarbe = global::ModellBahnSteuerung.Properties.Settings.Default.ViewFadenkreuzFarbe;
			this.fadenkreuzAnzeigen = this.toolStripSplitButtonCheckableFadenkreuz.Checked;
		}

		private void toolStripMenuItemFadenkreuzFadenkreuzLinie_Click(object sender, EventArgs e) {
			this.toolStripSplitButtonCheckableFadenkreuz.Image = this.toolStripMenuItemFadenkreuzFadenkreuzLinie.Image;
			global::ModellBahnSteuerung.Properties.Settings.Default.ViewFadenkreuzTyp = FadenkreuzTyp.Linie.ToString();
			global::ModellBahnSteuerung.Properties.Settings.Default.Save();
			this.fadenkreuzTyp = global::ModellBahnSteuerung.Properties.Settings.Default.ViewFadenkreuzTyp.ToFadenkreuzTyp();
		}

		private void toolStripMenuItemFadenkreuzFadenkreuzPunkt_Click(object sender, EventArgs e) {
			this.toolStripSplitButtonCheckableFadenkreuz.Image = this.toolStripMenuItemFadenkreuzFadenkreuzPunkt.Image;
			global::ModellBahnSteuerung.Properties.Settings.Default.ViewFadenkreuzTyp = FadenkreuzTyp.Punkte.ToString();
			global::ModellBahnSteuerung.Properties.Settings.Default.Save();
			this.fadenkreuzTyp = global::ModellBahnSteuerung.Properties.Settings.Default.ViewFadenkreuzTyp.ToFadenkreuzTyp();
		}

		private void toolStripMenuItemFadenkreuzFadenkreuzFarbe_Click(object sender, EventArgs e) {
			ColorDialogExtension colorDialog = new ColorDialogExtension(this.Location.X + ((this.Width - 225) / 2), this.Location.Y + ((this.Height - 330) / 2));
			colorDialog.Color = global::ModellBahnSteuerung.Properties.Settings.Default.ViewFadenkreuzFarbe;
			if (colorDialog.ShowDialog() == DialogResult.OK) {
				global::ModellBahnSteuerung.Properties.Settings.Default.ViewFadenkreuzFarbe = colorDialog.Color;
				global::ModellBahnSteuerung.Properties.Settings.Default.Save();
				this.fadenkreuzFarbe = global::ModellBahnSteuerung.Properties.Settings.Default.ViewFadenkreuzFarbe;
				this.toolStripMenuItemFadenkreuzFadenkreuzFarbe.Image = this.MenüFarbe(this.fadenkreuzFarbe);
			}
		}

		#endregion

		private void toolStripButtonElementeEigenschaften_Click(object sender, EventArgs e) {
			if (this.toolStripButtonElementeEigenschaften.Checked) {
				this._toolBoxElemente.Show();
				this.Focus();
			}
			else {
				this._toolBoxElemente.Hide();
			}
		}

		#endregion

		#endregion

		#region Extra

		#region Server

		private void toolStripMenuItemExtrasServerStart_Click(object sender, EventArgs e) {
			try {
				// ToDo Serverstart
				this.Controller.StartServer("MoBaSteuerung", global::ModellBahnSteuerung.Properties.Settings.Default.Port);

				this.toolStripMenuItemExtrasServerStart.Enabled = false;
				this.toolStripMenuItemExtrasServerStop.Enabled = true;
			}
			catch (Exception ex) {
				Logging.Log.SchreibeException(ex);
			}
		}

		private void toolStripMenuItemExtrasServerStop_Click(object sender, EventArgs e) {
			try {
				// ToDo Serverstop
				this.Controller.StopServer();

				this.toolStripMenuItemExtrasServerStart.Enabled = true;
				this.toolStripMenuItemExtrasServerStop.Enabled = false;
			}
			catch (Exception ex) {
				Logging.Log.SchreibeException(ex);
			}
		}

		#endregion

		#region Einstellungen

		private void toolStripMenuItemEinstellungen_Click(object sender, EventArgs e) {
			frmEinstellung einstellungenForm = new frmEinstellung();

			einstellungenForm.RückmeldungAnzeigen = this.Controller.RückmeldungAnzeigen;
			einstellungenForm.RückmeldungAktiv = this.Controller.RückmeldungAktiv;
			einstellungenForm.FahrstraßenStartVerzögerung = this.Controller.FahrstraßenStartVerzögerung;
			einstellungenForm.AdminAktiviert = this._adminAktiviert;
			einstellungenForm.AnlageBearbeitenAktiviert = this.toolStripMenuItemBearbeiten.Enabled;
			einstellungenForm.Pwd = this._adminPwd;
			einstellungenForm.EntkupplerAbschaltAutoAktiv = this.Model.EntkupplerAbschaltAutoAktiv;
			einstellungenForm.EntkupplerAbschaltAutoWert = this.Model.EntkupplerAbschaltAutoWert;
			einstellungenForm.ServoSchrittweite = this.Model.ZubehörServoSchrittweite;

			if (einstellungenForm.ShowDialog(this) == DialogResult.OK) {
				this.Controller.RückmeldungAnzeigen = einstellungenForm.RückmeldungAnzeigen;
				this.Controller.RückmeldungAktiv = einstellungenForm.RückmeldungAktiv;
				this.Controller.FahrstraßenStartVerzögerung = einstellungenForm.FahrstraßenStartVerzögerung;
				this._adminAktiviert = einstellungenForm.AdminAktiviert;
				if (!einstellungenForm.AnlageBearbeitenAktiviert) {
					this.toolStripMenuItemBearbeiten.Checked = false;
					this.toolStripMenuItemBearbeiten_Click(null, null);
				}
				this._anlageBearbeitenAktiviert = einstellungenForm.AnlageBearbeitenAktiviert;
				this.toolStripMenuItemBearbeiten.Enabled = einstellungenForm.AnlageBearbeitenAktiviert;
				this.Model.EntkupplerAbschaltAutoAktiv = einstellungenForm.EntkupplerAbschaltAutoAktiv;
				this.Model.EntkupplerAbschaltAutoWert = einstellungenForm.EntkupplerAbschaltAutoWert;
				this.Model.ZubehörServoSchrittweite = einstellungenForm.ServoSchrittweite;
			}

		}

		#endregion

		#endregion

		#region Hilfe

		private void toolStripMenuItemHilfeHilfe_Click(object sender, EventArgs e) {
			// ToDo  Hilfe
			//MessageBox.Show("Hilf dir selbst!");
#if DEBUG
			Help.ShowHelp(this, @"..\..\..\MoBS.chm", HelpNavigator.TopicId, "1");
			return;
#endif
			Help.ShowHelp(this, Application.StartupPath + @"\MoBS.chm", HelpNavigator.TopicId, "1");
		}

		private void logToolStripMenuItem_Click_1(object sender, EventArgs e) {
			new frmLog().Show(this);
		}

		private void toolStripMenuItemHilfeInfo_Click(object sender, EventArgs e) {
			new frmInfo().ShowDialog(this);
		}

		#endregion

		#endregion

		#region ToolStrip Events

		#region Zoom Plus

		private void toolStripStatusLabelMinus_MouseDown(object sender, MouseEventArgs e) {
			switch (e.Button) {
				case MouseButtons.Left:
					this.toolStripStatusLabelMinus.Image = global::ModellBahnSteuerung.Properties.Resources.Minus_MouseDown;
					break;
			}
		}

		private void toolStripStatusLabelMinus_MouseEnter(object sender, EventArgs e) {
			this.toolStripStatusLabelMinus.Image = global::ModellBahnSteuerung.Properties.Resources.Minus_MouseOver;
		}

		private void toolStripStatusLabelMinus_MouseLeave(object sender, EventArgs e) {
			this.toolStripStatusLabelMinus.Image = global::ModellBahnSteuerung.Properties.Resources.Minus;
		}

		private void toolStripStatusLabelMinus_MouseUp(object sender, MouseEventArgs e) {
			this.toolStripStatusLabelMinus.Image = global::ModellBahnSteuerung.Properties.Resources.Minus_MouseOver;
			switch (e.Button) {
				case MouseButtons.Left:
					this.Controller.Zoom = this.Controller.Zoom - 1;
					this.Aktualisieren();
					break;
				case MouseButtons.Right:

					break;
			}
		}

		#endregion

		#region Zoom Minus

		private void toolStripStatusLabelPlus_MouseDown(object sender, MouseEventArgs e) {
			switch (e.Button) {
				case MouseButtons.Left:
					this.toolStripStatusLabelPlus.Image = global::ModellBahnSteuerung.Properties.Resources.Plus_MouseDown;
					break;
			}
		}

		private void toolStripStatusLabelPlus_MouseEnter(object sender, EventArgs e) {
			this.toolStripStatusLabelPlus.Image = global::ModellBahnSteuerung.Properties.Resources.Plus_MouseOver;
		}

		private void toolStripStatusLabelPlus_MouseLeave(object sender, EventArgs e) {
			this.toolStripStatusLabelPlus.Image = global::ModellBahnSteuerung.Properties.Resources.Plus;
		}

		private void toolStripStatusLabelPlus_MouseUp(object sender, MouseEventArgs e) {
			this.toolStripStatusLabelPlus.Image = global::ModellBahnSteuerung.Properties.Resources.Plus_MouseOver;
			switch (e.Button) {
				case MouseButtons.Left:
					this.Controller.Zoom = this.Controller.Zoom + 1;
					this.Aktualisieren();
					break;
				case MouseButtons.Right:

					break;
			}
		}


		#endregion

		private void toolStripStatusLabelZoomInfo_Click(object sender, EventArgs e) {
			this.Controller.ZoomStandard();
		}

		#endregion

		private bool MasterAnlageSpeichern() {
			// Abfrage, ob gespeichert werden müsste, oder ob Clients verbunden sind.
			switch (this.Controller.AnzeigeTyp) {
				case AnzeigeTyp.Bedienen:
					#region Bedienen

					// 1. sind noch Clients (Slaves) verbunden
					if (this.Controller.MitClientsVerbunden) {
						// Ja, es sind noch Clients (Slaves) verbunden
						if (MsgBox.Show("Es sind noch Clients (Slaves) verbunden, soll beended werden?", Constanten.ProgrammName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No) {
							// Wenn nein, dann abbrechen true
							return true;
						}
					}

					#endregion
					break;
				case AnzeigeTyp.Bearbeiten:
					#region AnzeigeTap Bearbeiten

					// 1. müsste gespeichert werden
					if (this.Model.IstAnlageSpeichernErforderlich) {
						// Ja, es müsste gespeichert werden
						switch (MsgBox.Show("Möchten sie die Änderungen an " + this.Model.AnlageDateiName + " speichern.", Constanten.ProgrammName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)) {
							// Wenn abbrechen, dann abbrechen true
							case DialogResult.Cancel:
								return true;
							case DialogResult.Yes:
								try {
									// Ja, ist Datei schon mal gespeichert worden
									// Wenn Dateiname dem Sdandardname entspricht
									if (this.Model.AnlageDateiName == Constanten.STANDARDFILENAME) {
										// Dateiname entspricht Sdandardname, also speicher unter
										SaveFileDialog saveFileDialog = new SaveFileDialog();
										saveFileDialog.Title = "Anlage speicher unter ...";
										saveFileDialog.InitialDirectory = "c:\\";
										saveFileDialog.DefaultExt = "anl";
										saveFileDialog.FileName = Constanten.STANDARDFILENAME;
										saveFileDialog.Filter = "Anlage Datei (*.anl)|*.anl|Alle Dateien (*.*)|*.*";
										saveFileDialog.FilterIndex = 1;
										saveFileDialog.RestoreDirectory = true;

										if (saveFileDialog.ShowDialog() == DialogResult.OK) {
											// Speichern unter
											this.Controller.AnlageSpeichernUnter(saveFileDialog.FileName);
										}
										else {
											// Wenn abbrechen, dann abbrechen true
											return true;
										}
									}
									else {
										// Speichern
										this.Controller.AnlageSpeichern();
									}
								}
								catch (Exception ex) {
									MsgBox.Show(ex.Message, "MoBoSt speichern", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
								}
								break;
						}
					}

					#endregion
					break;
			}
			return false;
		}

		private bool SlaveVerbindungTrennen() {
			// Abfrage, ob mit Master verbunden
			if (this.Controller.MitServerVerbunden) {
				if (MsgBox.Show("Slave ist noch mit Master verbunden, soll getrennt und beended werden?", Constanten.ProgrammName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No) {
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// "C:\Windows\System32\Test\Test.dll" dann "C:\Windows\...\Test.dll" wird.
		/// </summary>
		/// <param name="anlageDateiName">Der Pfad, der gekürzt zurückgegeben werden soll.</param>
		/// <param name="length">Die gewünschte Länge, die nicht überschritten werden darf.</param>
		/// <param name="textFont">Die Schriftart, die angewendet wird.</param>
		private string DateipfadEinkürzen(string anlageDateiName, int length, Font textFont) {
			string[] PathParts = anlageDateiName.Split('\\');
			StringBuilder PathBuild = new StringBuilder(anlageDateiName.Length);
			string LastPart = PathParts[PathParts.Length - 1];
			string PrevPath = "";

			//Erst prüfen ob der komplette String evtl. bereits kürzer als die Maximallänge ist
			if (TextRenderer.MeasureText(anlageDateiName, textFont).Width < length) {
				return anlageDateiName;
			}

			for (int i = 0; i < PathParts.Length - 1; i++) {
				PathBuild.Append(PathParts[i] + @"\");
				if (TextRenderer.MeasureText(PathBuild.ToString() + @"...\" + LastPart, textFont).Width >= length) {
					return PrevPath;
				}
				else {
					PrevPath = PathBuild.ToString() + @"...\" + LastPart;
				}
			}
			return PrevPath;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="anlageDateiName"></param>
		private void ZuleztGeladeneAnlagenHinzufügen(string anlageDateiName) {
			this.Text = "Modellbahnsteuerung Master [" + Path.GetFileNameWithoutExtension(anlageDateiName) + "]";

			foreach (ToolStripMenuItem item in this.toolStripMenuItemDateiZuletztGeöfnetteAnlage.DropDownItems) {
				item.Click -= new System.EventHandler(this.toolStripMenuItemDateiZuletztGeladenAnlage_Click);
			}
			this.toolStripMenuItemDateiZuletztGeöfnetteAnlage.DropDownItems.Clear();

			if (global::ModellBahnSteuerung.Properties.Settings.Default.ZuletztGeladenAnlagen.IndexOf(anlageDateiName) > -1) {
				global::ModellBahnSteuerung.Properties.Settings.Default.ZuletztGeladenAnlagen = global::ModellBahnSteuerung.Properties.Settings.Default.ZuletztGeladenAnlagen.Replace(anlageDateiName, "");
			}
			global::ModellBahnSteuerung.Properties.Settings.Default.ZuletztGeladenAnlagen = anlageDateiName + ";" + global::ModellBahnSteuerung.Properties.Settings.Default.ZuletztGeladenAnlagen;
			global::ModellBahnSteuerung.Properties.Settings.Default.ZuletztGeladenAnlagen = global::ModellBahnSteuerung.Properties.Settings.Default.ZuletztGeladenAnlagen.Replace(";;", ";");
			global::ModellBahnSteuerung.Properties.Settings.Default.ZuletztGeladenAnlagen = global::ModellBahnSteuerung.Properties.Settings.Default.ZuletztGeladenAnlagen.TrimEnd(';');
			global::ModellBahnSteuerung.Properties.Settings.Default.Save();

			this.InitializeZuletztGeladenAnlagen();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="master"></param>
		private void ZuleztVerbundeMasterHinzufügen(string master) {
			this.Text = "Modellbahnsteuerung Slave [" + master + "]";

			foreach (ToolStripMenuItem item in this.toolStripMenuItemDateiZuletztVerbundeneMaster.DropDownItems) {
				item.Click -= new System.EventHandler(this.toolStripMenuItemDateiZuletztVerbundeneMaster_Click);
			}
			this.toolStripMenuItemDateiZuletztVerbundeneMaster.DropDownItems.Clear();

			if (global::ModellBahnSteuerung.Properties.Settings.Default.ZuletztVerbundeneMaster.IndexOf(master) > -1) {
				global::ModellBahnSteuerung.Properties.Settings.Default.ZuletztVerbundeneMaster = global::ModellBahnSteuerung.Properties.Settings.Default.ZuletztVerbundeneMaster.Replace(master, "");
			}
			global::ModellBahnSteuerung.Properties.Settings.Default.ZuletztVerbundeneMaster = master + ";" + global::ModellBahnSteuerung.Properties.Settings.Default.ZuletztVerbundeneMaster;
			global::ModellBahnSteuerung.Properties.Settings.Default.ZuletztVerbundeneMaster = global::ModellBahnSteuerung.Properties.Settings.Default.ZuletztVerbundeneMaster.Replace(";;", ";");
			global::ModellBahnSteuerung.Properties.Settings.Default.ZuletztVerbundeneMaster = global::ModellBahnSteuerung.Properties.Settings.Default.ZuletztVerbundeneMaster.TrimEnd(';');
			global::ModellBahnSteuerung.Properties.Settings.Default.Save();

			this.InitializeZuletztVerbundeneMaster();
		}

		private void ZoomEnabled(bool enabled) {
			this.toolStripMenuItemZoom.Enabled = enabled;
			this.toolStripStatusLabelZoom.Enabled = enabled;
			this.toolStripStatusLabelZoomInfo.Enabled = enabled;
			this.toolStripStatusLabelMinus.Enabled = enabled;
			this.toolStripStatusLabelPlus.Enabled = enabled;
		}

		private void MenüBearbeitenDisabled() {
			this.toolStripBearbeiten.Visible = false;
			this.toolStripElemente.Visible = false;

			this.toolStripSplitButtonCheckableGitter.Checked = false;
			this.gitterAnzeigen = false;
			this.Aktualisieren();

			this.toolStripSplitButtonCheckableFadenkreuz.Checked = false;
			this.fadenkreuzAnzeigen = false;

			this.toolStripButtonElementeEigenschaften.Checked = false;
			this._toolBoxElemente.Hide();
			this._fahrStraßenEditor.Hide();
		}

		private Image MenüFarbe(Color color) {
			Image image = new Bitmap(24, 24);
			Graphics graphics = Graphics.FromImage(image);
			graphics.FillRectangle(new SolidBrush(color), 2, 2, 18, 18);
			graphics.Dispose();

			return image;
		}

		#endregion

		private void ToolStripMenuItemArduinoVerbinden_Click(object sender, EventArgs e) {
			frmArduino frm = new frmArduino(this.Controller.GetSerialPortNames());
			if (frm.ShowDialog(this) == DialogResult.OK) {
				if (this.Controller.OpenComPort(frm.PortName)) {
					this.toolStripMenuItemExtrasArduinoVerbinden.Enabled = false;
					this.toolStripMenuItemExtrasArduinoTrennen.Enabled = true;
					//MessageBox.Show(this, "Verbindung zur Anlage hergestellt.", "Arduino Verbinden", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					this.toolStripStatusLabelInfo.Text = "Verbindung zur Anlage hergestellt";
				}
			}
			else {
				MessageBox.Show(this, "Es konnte keine Verbindung hergetsellt werden!", "Arduino Verbinden", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void trennenToolStripMenuItem_Click(object sender, EventArgs e) {
			if (this.Controller.CloseComPort()) {
				this.toolStripStatusLabelInfo.Text = "Verbindung zur Anlage getrennt";
				this.toolStripMenuItemExtrasArduinoVerbinden.Enabled = true;
				this.toolStripMenuItemExtrasArduinoTrennen.Enabled = false;
			}
		}

		private void toolStripButtonElementSchalter_Click(object sender, EventArgs e) {
			this._model.BearbeitenSelektionLöschen();
			pictureBoxView.Invalidate();
			toolStripButtonElementFss.Checked = false;
			toolStripButtonElementGleis.Checked = false;
			toolStripButtonElementEntkuppler.Checked = false;
			toolStripButtonElementSignal.Checked = false;
			toolStripButtonElementInfoElement.Checked = false;
			this.Controller.NeuesElementVorschauReset();
			if (toolStripButtonElementSchalter.Checked)
				this.bearbeitungsModus = BearbeitungsModus.Schalter;
			else
				this.bearbeitungsModus = BearbeitungsModus.Selektieren;
		}

		private void toolStripButtonElementFss_Click(object sender, EventArgs e) {
			this._model.BearbeitenSelektionLöschen();
			pictureBoxView.Invalidate();
			toolStripButtonElementGleis.Checked = false;
			toolStripButtonElementSchalter.Checked = false;
			toolStripButtonElementEntkuppler.Checked = false;
			toolStripButtonElementSignal.Checked = false;
			toolStripButtonElementInfoElement.Checked = false;
			this.Controller.NeuesElementVorschauReset();
			if (toolStripButtonElementFss.Checked)
				this.bearbeitungsModus = BearbeitungsModus.Fss;
			else
				this.bearbeitungsModus = BearbeitungsModus.Selektieren;
		}

		private void toolStripButtonElementInfoElement_Click(object sender, EventArgs e) {
			this._model.BearbeitenSelektionLöschen();
			pictureBoxView.Invalidate();
			toolStripButtonElementFss.Checked = false;
			toolStripButtonElementGleis.Checked = false;
			toolStripButtonElementSchalter.Checked = false;
			toolStripButtonElementSignal.Checked = false;
			toolStripButtonElementEntkuppler.Checked = false;
			this.Controller.NeuesElementVorschauReset();
			if (toolStripButtonElementInfoElement.Checked)
				this.bearbeitungsModus = BearbeitungsModus.InfoElement;
			else
				this.bearbeitungsModus = BearbeitungsModus.Selektieren;
		}

		private void toolStripButtonElementEntkuppler_Click(object sender, EventArgs e) {
			this._model.BearbeitenSelektionLöschen();
			pictureBoxView.Invalidate();
			toolStripButtonElementFss.Checked = false;
			toolStripButtonElementGleis.Checked = false;
			toolStripButtonElementSchalter.Checked = false;
			toolStripButtonElementSignal.Checked = false;
			toolStripButtonElementInfoElement.Checked = false;
			this.Controller.NeuesElementVorschauReset();
			if (toolStripButtonElementEntkuppler.Checked)
				this.bearbeitungsModus = BearbeitungsModus.Entkuppler;
			else
				this.bearbeitungsModus = BearbeitungsModus.Selektieren;
		}

		private void toolStripButtonElementSignal_Click(object sender, EventArgs e) {
			this._model.BearbeitenSelektionLöschen();
			pictureBoxView.Invalidate();
			toolStripButtonElementFss.Checked = false;
			toolStripButtonElementGleis.Checked = false;
			toolStripButtonElementSchalter.Checked = false;
			toolStripButtonElementEntkuppler.Checked = false;
			toolStripButtonElementInfoElement.Checked = false;
			this.Controller.NeuesElementVorschauReset();
			if (toolStripButtonElementSignal.Checked)
				this.bearbeitungsModus = BearbeitungsModus.Signal;
			else
				this.bearbeitungsModus = BearbeitungsModus.Selektieren;
		}

		private void toolStripButtonElementGleis_Click(object sender, EventArgs e) {
			this._model.BearbeitenSelektionLöschen();
			pictureBoxView.Invalidate();
			toolStripButtonElementFss.Checked = false;
			toolStripButtonElementSchalter.Checked = false;
			toolStripButtonElementEntkuppler.Checked = false;
			toolStripButtonElementSignal.Checked = false;
			toolStripButtonElementInfoElement.Checked = false;
			this.Controller.NeuesElementVorschauReset();
			if (toolStripButtonElementGleis.Checked)
				this.bearbeitungsModus = BearbeitungsModus.Gleis;
			else
				this.bearbeitungsModus = BearbeitungsModus.Selektieren;
		}


		protected override bool ProcessDialogKey(Keys keyData) {
			if (_servoKeyAction != ServoAction.LinksHold && _servoKeyAction != ServoAction.RechtsHold) {
				switch (keyData) {
					case Keys.Left:
						if (_servoKeyAction == ServoAction.LinksClick) {
							_servoKeyAction = ServoAction.LinksHold;
							Model.BedienenServoManuell(_servoKeyAction);
						}
						else {
							_servoKeyAction = ServoAction.LinksClick;
						}
						break;
					case Keys.Right:
						if (_servoKeyAction == ServoAction.RechtsClick) {
							_servoKeyAction = ServoAction.RechtsHold;
							Model.BedienenServoManuell(_servoKeyAction);
						}
						else {
							if (_servoKeyAction != ServoAction.None) {

							}
							_servoKeyAction = ServoAction.RechtsClick;
						}
						break;
					default:
						break;
				}
			}
			//if (keyData == Keys.Right || keyData == Keys.Left) {
			//    Debug.Print("ProcessDialogKey " + keyData);
			//    //_servoKeyAction = 
			//    Model.BedienenServoManuell(keyData);
			//}
			return base.ProcessDialogKey(keyData);
		}

		private void MoBaStForm_KeyDown(object sender, KeyEventArgs e) {
			this.keydownEventArgs = e;

			bool neuZeichnen = false;
			switch (this.Controller.AnzeigeTyp) {
				case AnzeigeTyp.Bearbeiten:
					if (this.bearbeitungsModus != BearbeitungsModus.Selektieren) {
						if (e.KeyData == Keys.R && this.bearbeitungsModus == BearbeitungsModus.Signal)
							neuZeichnen = this.Controller.SignalDrehen();
						if (e.KeyData == Keys.Escape) {
							this.BearbeitungsModusZeichnenZurücksetzen();
						}
					}
					else {
						neuZeichnen = _model.BearbeitenKeyDown(e);
					}
					break;
				case AnzeigeTyp.Bedienen:
					if (e.KeyData == (Keys.NumPad0 | Keys.Control) || e.KeyData == (Keys.D0 | Keys.Control)) {
						if (_signalNummer < 0) {
							_signalNummer = 0;
						}
						else {
							_signalNummer = _signalNummer * 10 + 0;
						}
					}
					if (e.KeyData == (Keys.NumPad1 | Keys.Control) || e.KeyData == (Keys.D1 | Keys.Control)) {
						if (_signalNummer < 0) {
							_signalNummer = 1;
						}
						else {
							_signalNummer = _signalNummer * 10 + 1;
						}
					}
					if (e.KeyData == (Keys.NumPad2 | Keys.Control) || e.KeyData == (Keys.D2 | Keys.Control)) {
						if (_signalNummer < 0) {
							_signalNummer = 2;
						}
						else {
							_signalNummer = _signalNummer * 10 + 2;
						}
					}
					if (e.KeyData == (Keys.NumPad3 | Keys.Control) || e.KeyData == (Keys.D3 | Keys.Control)) {
						if (_signalNummer < 0) {
							_signalNummer = 3;
						}
						else {
							_signalNummer = _signalNummer * 10 + 3;
						}
					}
					if (e.KeyData == (Keys.NumPad4 | Keys.Control) || e.KeyData == (Keys.D4 | Keys.Control)) {
						if (_signalNummer < 0) {
							_signalNummer = 4;
						}
						else {
							_signalNummer = _signalNummer * 10 + 4;
						}
					}
					if (e.KeyData == (Keys.NumPad5 | Keys.Control) || e.KeyData == (Keys.D5 | Keys.Control)) {
						if (_signalNummer < 0) {
							_signalNummer = 5;
						}
						else {
							_signalNummer = _signalNummer * 10 + 5;
						}
					}
					if (e.KeyData == (Keys.NumPad6 | Keys.Control) || e.KeyData == (Keys.D6 | Keys.Control)) {
						if (_signalNummer < 0) {
							_signalNummer = 6;
						}
						else {
							_signalNummer = _signalNummer * 10 + 6;
						}
					}
					if (e.KeyData == (Keys.NumPad7 | Keys.Control) || e.KeyData == (Keys.D7 | Keys.Control)) {
						if (_signalNummer < 0) {
							_signalNummer = 7;
						}
						else {
							_signalNummer = _signalNummer * 10 + 7;
						}
					}
					if (e.KeyData == (Keys.NumPad8 | Keys.Control) || e.KeyData == (Keys.D8 | Keys.Control)) {
						if (_signalNummer < 0) {
							_signalNummer = 8;
						}
						else {
							_signalNummer = _signalNummer * 10 + 8;
						}
					}
					if (e.KeyData == (Keys.NumPad9 | Keys.Control) || e.KeyData == (Keys.D9 | Keys.Control)) {
						if (_signalNummer < 0) {
							_signalNummer = 9;
						}
						else {
							_signalNummer = _signalNummer * 10 + 9;
						}
					}
					if (e.KeyData == Keys.Escape) {
						neuZeichnen = this.Controller.BedienenAuswahlLöschen();
					}
					if (e.KeyData == Keys.Left || e.KeyData == Keys.Right) {
						neuZeichnen = this.Controller.BedienenAuswahlLöschen();
					}
					break;
			}
			if (neuZeichnen) this.pictureBoxView.Invalidate();
		}

		public void BearbeitungsModusZeichnenZurücksetzen() {
			this.Controller.NeuesElementVorschauReset();
			this.toolStripButtonElementGleis.Checked = false;
			this.toolStripButtonElementSchalter.Checked = false;
			this.toolStripButtonElementSignal.Checked = false;
			this.toolStripButtonElementEntkuppler.Checked = false;
			this.toolStripButtonElementInfoElement.Checked = false;
			this.bearbeitungsModus = BearbeitungsModus.Selektieren;
		}

		private void MoBaStForm_KeyUp(object sender, KeyEventArgs e) {
			if (e.KeyData == (Keys.ControlKey)) {
				if (this.Controller.FahrstrassenSignal(_signalNummer))
					this.pictureBoxView.Invalidate();
				_signalNummer = -1;
			}
			if (e.KeyData == Keys.Left || e.KeyData == Keys.Right) {
				if ((_servoKeyAction == ServoAction.RechtsHold && e.KeyData == Keys.Right)
						|| (_servoKeyAction == ServoAction.LinksHold && e.KeyData == Keys.Left)) {
					Model.BedienenServoManuell(ServoAction.HoldStop);
				}
				else {
					Model.BedienenServoManuell(_servoKeyAction);
				}
				_servoKeyAction = ServoAction.None;
			}
			if (_signalNummer >= 0) {
				this.toolStripStatusLabelInfo.Text = "Signal " + _signalNummer;
			}
			else {
				//this.toolStripStatusLabelInfo.Text = "Info";
			}
		}

		private void zugEditorToolStripMenuItem_Click(object sender, EventArgs e) {
			frmZugEditor frm = new frmZugEditor(this.Controller.ZeichnenElemente);
			if (frm.ShowDialog(this) == DialogResult.OK) {
			//model.
                // string[][] t =  frm.auslesen();
			}
			else {
			}
		}

		/// <summary>
		/// speichert Arduino-Daten in eine Textdatei
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void toolStripButtonElementeExportieren_Click(object sender, EventArgs e) {
			StreamWriter arduinoStreamWriter = new StreamWriter("ArduinoDaten.txt", false, System.Text.Encoding.UTF8);
			foreach (MCSpeicher ard in _model.ZeichnenElemente.ListeMCSpeicher.Elemente) {
				arduinoStreamWriter.WriteLine("Arduino " + ard.ID);
				for (int i = 0; i < 3; i++) {

					arduinoStreamWriter.WriteLine("Relais-Platine " + i);
					string[] kurzBezeichnungArray = new string[16];
					string[] anlagenBezeichnungArray = new string[16];
					string[] rmAdressenArray = new string[16];
					List<AnlagenElement> anlagenElementeExport = _model.ZeichnenElemente.RelaisAdresseSuchen(ard.ID, i);
					foreach (AnlagenElement a in anlagenElementeExport) {
						int b = a.Ausgang.BitNr;
						kurzBezeichnungArray[b] = kurzBezeichnungArray[b] + a.KurzBezeichnung + " ";
						anlagenBezeichnungArray[b] = anlagenBezeichnungArray[b] + a.Bezeichnung + " ";
					}
					arduinoStreamWriter.WriteLine("");
					for (int a = 0; a < 16; a++) { arduinoStreamWriter.Write(kurzBezeichnungArray[a] + "\t"); }
					arduinoStreamWriter.WriteLine("");
					for (int a = 0; a < 16; a++) { arduinoStreamWriter.Write(anlagenBezeichnungArray[a] + "\t"); }
					arduinoStreamWriter.WriteLine(" ");
				}
				List<Gleis> anlagenElementeRM = _model.ZeichnenElemente.RueckmeldungSuchen(ard.ID);
				string[,] rmArray = new string[2, 16];
				foreach (Gleis g in anlagenElementeRM) {

				}
				for (int i = 0; i < 2; i++) {
					arduinoStreamWriter.WriteLine("RM-Platine " + i);


				}
			}
			// _model.ZeichnenElemente.ListeMCSpeicher.Elemente;
			//_anlagenElemente.L
			//   _model.zeichnenElemente.ListeMCSpeicher.SpeicherString
			arduinoStreamWriter.Flush();
			arduinoStreamWriter.Dispose();
		}

		private void toolStripButton1_Click(object sender, EventArgs e) {
			FormStecker frmStecker = new FormStecker();
			frmStecker.Model = _model;
			frmStecker.Show();

		}

		private void toolStripButtonFahrstrassenEditor_Click(object sender, EventArgs e) {
			if (this.toolStripButtonFahrstrassenEditor.Checked) {
				this._fahrStraßenEditor.Show();
				this.Focus();
			}
			else {
				this._fahrStraßenEditor.Hide();
			}
		}

       

        private void pictureBoxView_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void pictureBoxView_Click(object sender, EventArgs e)
        {

        }
    }
}