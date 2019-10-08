using System.Windows.Forms;

namespace MoBaSteuerung {
	partial class MoBaStForm {
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoBaStForm));
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabelInfo = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelRaster = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelRasterInfo = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelZoom = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelZoomInfo = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelAbstand1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelMinus = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelAbstand2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelPlus = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelAbstand3 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelZeit = new System.Windows.Forms.ToolStripStatusLabel();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.toolStripMenuItemDatei = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDateiNeu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDateiLaden = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparatorDatei1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemDateiSchließen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparatorDatei2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemDateiSpeichern = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDateiSpeichernUnter = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparatorDatei3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemDateiVerbinden = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDateiTrennen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparatorDatei4 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemDateiZuletztGeöfnetteAnlage = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDateiZuletztVerbundeneMaster = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparatorDatei5 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemDateiBeenden = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemZoom = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemZoomPlus = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemZoomMinus = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparatorZoom1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemZoomZurücksetzen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemBearbeiten = new Master.ToolStripMenuItemCheckable();
			this.zugEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemExtras = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemExtrasServer = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemExtrasServerStart = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemExtrasServerStop = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemExtraArduino = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemExtrasArduinoVerbinden = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemExtrasArduinoTrennen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparatorExtras1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemExtrasEinstellungen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemHilfe = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemHilfeHilfe = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparatorHilfe1 = new System.Windows.Forms.ToolStripSeparator();
			this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemHilfeInfo = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
			this.panelView = new System.Windows.Forms.Panel();
			this.vScrollBarView = new System.Windows.Forms.VScrollBar();
			this.panelScrollView = new System.Windows.Forms.Panel();
			this.pictureBoxView = new System.Windows.Forms.PictureBox();
			this.hScrollBarView = new System.Windows.Forms.HScrollBar();
			this.toolStripElemente = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonElementGleis = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonElementSignal = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonElementEntkuppler = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonElementSchalter = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonElementFss = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonElementInfoElement = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonElementEingSchalter = new System.Windows.Forms.ToolStripButton();
			this.toolStripBearbeiten = new System.Windows.Forms.ToolStrip();
			this.toolStripSplitButtonCheckableGitter = new Master.ToolStripSplitButtonCheckable();
			this.toolStripMenuItemGitterGitterLinie = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemGitterGitterLinieVersetzt = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemGitterGitterPunkt = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemGitterGitterPunktVersetzt = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparatorGitterGitter1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemGitterGitterFarbe = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripLabelAbstand1 = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSplitButtonCheckableFadenkreuz = new Master.ToolStripSplitButtonCheckable();
			this.toolStripMenuItemFadenkreuzFadenkreuzLinie = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemFadenkreuzFadenkreuzPunkt = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparatorFadenkreuzFadenkreuz1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemFadenkreuzFadenkreuzFarbe = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripLabelAbstand2 = new System.Windows.Forms.ToolStripLabel();
			this.toolStripButtonElementeEigenschaften = new Master.ToolStripMenuItemCheckable();
			this.toolStripButtonFahrstrassenEditor = new Master.ToolStripMenuItemCheckable();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonElementeExportieren = new System.Windows.Forms.ToolStripButton();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.timerZeit = new System.Windows.Forms.Timer(this.components);
			this.contextMenuStripAuswahlRechteck = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemAufheben = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparatorAuswahl1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemDrehen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSpiegeln = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparatorAuswahl2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemLöschen = new System.Windows.Forms.ToolStripMenuItem();
			this.fontDialog1 = new System.Windows.Forms.FontDialog();
			this.statusStrip.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.toolStripContainer.ContentPanel.SuspendLayout();
			this.toolStripContainer.LeftToolStripPanel.SuspendLayout();
			this.toolStripContainer.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer.SuspendLayout();
			this.panelView.SuspendLayout();
			this.panelScrollView.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxView)).BeginInit();
			this.toolStripElemente.SuspendLayout();
			this.toolStripBearbeiten.SuspendLayout();
			this.contextMenuStripAuswahlRechteck.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelInfo,
            this.toolStripStatusLabelRaster,
            this.toolStripStatusLabelRasterInfo,
            this.toolStripStatusLabelZoom,
            this.toolStripStatusLabelZoomInfo,
            this.toolStripStatusLabelAbstand1,
            this.toolStripStatusLabelMinus,
            this.toolStripStatusLabelAbstand2,
            this.toolStripStatusLabelPlus,
            this.toolStripStatusLabelAbstand3,
            this.toolStripStatusLabelZeit});
			this.statusStrip.Location = new System.Drawing.Point(0, 426);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(966, 24);
			this.statusStrip.TabIndex = 0;
			this.statusStrip.Text = "statusStrip";
			// 
			// toolStripStatusLabelInfo
			// 
			this.toolStripStatusLabelInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripStatusLabelInfo.Name = "toolStripStatusLabelInfo";
			this.toolStripStatusLabelInfo.Size = new System.Drawing.Size(793, 19);
			this.toolStripStatusLabelInfo.Spring = true;
			this.toolStripStatusLabelInfo.Text = "Info";
			this.toolStripStatusLabelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripStatusLabelRaster
			// 
			this.toolStripStatusLabelRaster.Name = "toolStripStatusLabelRaster";
			this.toolStripStatusLabelRaster.Size = new System.Drawing.Size(42, 19);
			this.toolStripStatusLabelRaster.Text = "Raster:";
			this.toolStripStatusLabelRaster.Visible = false;
			// 
			// toolStripStatusLabelRasterInfo
			// 
			this.toolStripStatusLabelRasterInfo.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.toolStripStatusLabelRasterInfo.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.toolStripStatusLabelRasterInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripStatusLabelRasterInfo.Name = "toolStripStatusLabelRasterInfo";
			this.toolStripStatusLabelRasterInfo.Size = new System.Drawing.Size(26, 19);
			this.toolStripStatusLabelRasterInfo.Text = "0,0";
			this.toolStripStatusLabelRasterInfo.Visible = false;
			// 
			// toolStripStatusLabelZoom
			// 
			this.toolStripStatusLabelZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripStatusLabelZoom.Enabled = false;
			this.toolStripStatusLabelZoom.Name = "toolStripStatusLabelZoom";
			this.toolStripStatusLabelZoom.Size = new System.Drawing.Size(39, 19);
			this.toolStripStatusLabelZoom.Text = "Zoom";
			this.toolStripStatusLabelZoom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// toolStripStatusLabelZoomInfo
			// 
			this.toolStripStatusLabelZoomInfo.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.toolStripStatusLabelZoomInfo.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.toolStripStatusLabelZoomInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripStatusLabelZoomInfo.Enabled = false;
			this.toolStripStatusLabelZoomInfo.Name = "toolStripStatusLabelZoomInfo";
			this.toolStripStatusLabelZoomInfo.Size = new System.Drawing.Size(23, 19);
			this.toolStripStatusLabelZoomInfo.Text = "20";
			this.toolStripStatusLabelZoomInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolStripStatusLabelZoomInfo.Click += new System.EventHandler(this.toolStripStatusLabelZoomInfo_Click);
			// 
			// toolStripStatusLabelAbstand1
			// 
			this.toolStripStatusLabelAbstand1.AutoSize = false;
			this.toolStripStatusLabelAbstand1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripStatusLabelAbstand1.Name = "toolStripStatusLabelAbstand1";
			this.toolStripStatusLabelAbstand1.Size = new System.Drawing.Size(5, 19);
			// 
			// toolStripStatusLabelMinus
			// 
			this.toolStripStatusLabelMinus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripStatusLabelMinus.Enabled = false;
			this.toolStripStatusLabelMinus.Image = global::ModellBahnSteuerung.Properties.Resources.Minus;
			this.toolStripStatusLabelMinus.Name = "toolStripStatusLabelMinus";
			this.toolStripStatusLabelMinus.Size = new System.Drawing.Size(16, 19);
			this.toolStripStatusLabelMinus.Text = "Zoom Minus";
			this.toolStripStatusLabelMinus.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
			this.toolStripStatusLabelMinus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolStripStatusLabelMinus_MouseDown);
			this.toolStripStatusLabelMinus.MouseEnter += new System.EventHandler(this.toolStripStatusLabelMinus_MouseEnter);
			this.toolStripStatusLabelMinus.MouseLeave += new System.EventHandler(this.toolStripStatusLabelMinus_MouseLeave);
			this.toolStripStatusLabelMinus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.toolStripStatusLabelMinus_MouseUp);
			// 
			// toolStripStatusLabelAbstand2
			// 
			this.toolStripStatusLabelAbstand2.AutoSize = false;
			this.toolStripStatusLabelAbstand2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripStatusLabelAbstand2.Name = "toolStripStatusLabelAbstand2";
			this.toolStripStatusLabelAbstand2.Size = new System.Drawing.Size(5, 19);
			// 
			// toolStripStatusLabelPlus
			// 
			this.toolStripStatusLabelPlus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripStatusLabelPlus.Enabled = false;
			this.toolStripStatusLabelPlus.Image = global::ModellBahnSteuerung.Properties.Resources.Plus;
			this.toolStripStatusLabelPlus.Name = "toolStripStatusLabelPlus";
			this.toolStripStatusLabelPlus.Size = new System.Drawing.Size(16, 19);
			this.toolStripStatusLabelPlus.Text = "Zoom Plus";
			this.toolStripStatusLabelPlus.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
			this.toolStripStatusLabelPlus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolStripStatusLabelPlus_MouseDown);
			this.toolStripStatusLabelPlus.MouseEnter += new System.EventHandler(this.toolStripStatusLabelPlus_MouseEnter);
			this.toolStripStatusLabelPlus.MouseLeave += new System.EventHandler(this.toolStripStatusLabelPlus_MouseLeave);
			this.toolStripStatusLabelPlus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.toolStripStatusLabelPlus_MouseUp);
			// 
			// toolStripStatusLabelAbstand3
			// 
			this.toolStripStatusLabelAbstand3.AutoSize = false;
			this.toolStripStatusLabelAbstand3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripStatusLabelAbstand3.Name = "toolStripStatusLabelAbstand3";
			this.toolStripStatusLabelAbstand3.Size = new System.Drawing.Size(5, 19);
			// 
			// toolStripStatusLabelZeit
			// 
			this.toolStripStatusLabelZeit.Name = "toolStripStatusLabelZeit";
			this.toolStripStatusLabelZeit.Size = new System.Drawing.Size(49, 19);
			this.toolStripStatusLabelZeit.Text = "00:00:00";
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDatei,
            this.toolStripMenuItemZoom,
            this.toolStripMenuItemBearbeiten,
            this.zugEditorToolStripMenuItem,
            this.toolStripMenuItemExtras,
            this.toolStripMenuItemHilfe});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(966, 26);
			this.menuStrip.TabIndex = 1;
			this.menuStrip.Text = "menuStrip";
			// 
			// toolStripMenuItemDatei
			// 
			this.toolStripMenuItemDatei.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDateiNeu,
            this.toolStripMenuItemDateiLaden,
            this.toolStripSeparatorDatei1,
            this.toolStripMenuItemDateiSchließen,
            this.toolStripSeparatorDatei2,
            this.toolStripMenuItemDateiSpeichern,
            this.toolStripMenuItemDateiSpeichernUnter,
            this.toolStripSeparatorDatei3,
            this.toolStripMenuItemDateiVerbinden,
            this.toolStripMenuItemDateiTrennen,
            this.toolStripSeparatorDatei4,
            this.toolStripMenuItemDateiZuletztGeöfnetteAnlage,
            this.toolStripMenuItemDateiZuletztVerbundeneMaster,
            this.toolStripSeparatorDatei5,
            this.toolStripMenuItemDateiBeenden});
			this.toolStripMenuItemDatei.Name = "toolStripMenuItemDatei";
			this.toolStripMenuItemDatei.Size = new System.Drawing.Size(46, 22);
			this.toolStripMenuItemDatei.Text = "&Datei";
			// 
			// toolStripMenuItemDateiNeu
			// 
			this.toolStripMenuItemDateiNeu.Image = global::ModellBahnSteuerung.Properties.Resources.NewFile_16x;
			this.toolStripMenuItemDateiNeu.Name = "toolStripMenuItemDateiNeu";
			this.toolStripMenuItemDateiNeu.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
			this.toolStripMenuItemDateiNeu.Size = new System.Drawing.Size(254, 22);
			this.toolStripMenuItemDateiNeu.Text = "&Neu";
			this.toolStripMenuItemDateiNeu.Click += new System.EventHandler(this.toolStripMenuItemDateiNeu_Click);
			// 
			// toolStripMenuItemDateiLaden
			// 
			this.toolStripMenuItemDateiLaden.Image = global::ModellBahnSteuerung.Properties.Resources.OpenFileFromProject_16x;
			this.toolStripMenuItemDateiLaden.Name = "toolStripMenuItemDateiLaden";
			this.toolStripMenuItemDateiLaden.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
			this.toolStripMenuItemDateiLaden.Size = new System.Drawing.Size(254, 22);
			this.toolStripMenuItemDateiLaden.Text = "&Laden";
			this.toolStripMenuItemDateiLaden.Click += new System.EventHandler(this.toolStripMenuItemDateiLaden_Click);
			// 
			// toolStripSeparatorDatei1
			// 
			this.toolStripSeparatorDatei1.Name = "toolStripSeparatorDatei1";
			this.toolStripSeparatorDatei1.Size = new System.Drawing.Size(251, 6);
			// 
			// toolStripMenuItemDateiSchließen
			// 
			this.toolStripMenuItemDateiSchließen.Enabled = false;
			this.toolStripMenuItemDateiSchließen.Name = "toolStripMenuItemDateiSchließen";
			this.toolStripMenuItemDateiSchließen.Size = new System.Drawing.Size(254, 22);
			this.toolStripMenuItemDateiSchließen.Text = "&Schließen";
			this.toolStripMenuItemDateiSchließen.Click += new System.EventHandler(this.toolStripMenuItemDateiSchließen_Click);
			// 
			// toolStripSeparatorDatei2
			// 
			this.toolStripSeparatorDatei2.Name = "toolStripSeparatorDatei2";
			this.toolStripSeparatorDatei2.Size = new System.Drawing.Size(251, 6);
			// 
			// toolStripMenuItemDateiSpeichern
			// 
			this.toolStripMenuItemDateiSpeichern.Enabled = false;
			this.toolStripMenuItemDateiSpeichern.Image = global::ModellBahnSteuerung.Properties.Resources.Save_16x;
			this.toolStripMenuItemDateiSpeichern.Name = "toolStripMenuItemDateiSpeichern";
			this.toolStripMenuItemDateiSpeichern.Size = new System.Drawing.Size(254, 22);
			this.toolStripMenuItemDateiSpeichern.Text = "&Speichern";
			this.toolStripMenuItemDateiSpeichern.Click += new System.EventHandler(this.toolStripMenuItemDateiSpeichern_Click);
			// 
			// toolStripMenuItemDateiSpeichernUnter
			// 
			this.toolStripMenuItemDateiSpeichernUnter.Enabled = false;
			this.toolStripMenuItemDateiSpeichernUnter.Image = global::ModellBahnSteuerung.Properties.Resources.SaveAs_16x;
			this.toolStripMenuItemDateiSpeichernUnter.Name = "toolStripMenuItemDateiSpeichernUnter";
			this.toolStripMenuItemDateiSpeichernUnter.Size = new System.Drawing.Size(254, 22);
			this.toolStripMenuItemDateiSpeichernUnter.Text = "&Speichern unter ...";
			this.toolStripMenuItemDateiSpeichernUnter.Click += new System.EventHandler(this.toolStripMenuItemDateiSpeichernUnter_Click);
			// 
			// toolStripSeparatorDatei3
			// 
			this.toolStripSeparatorDatei3.Name = "toolStripSeparatorDatei3";
			this.toolStripSeparatorDatei3.Size = new System.Drawing.Size(251, 6);
			// 
			// toolStripMenuItemDateiVerbinden
			// 
			this.toolStripMenuItemDateiVerbinden.Name = "toolStripMenuItemDateiVerbinden";
			this.toolStripMenuItemDateiVerbinden.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.V)));
			this.toolStripMenuItemDateiVerbinden.Size = new System.Drawing.Size(254, 22);
			this.toolStripMenuItemDateiVerbinden.Text = "&Verbinden";
			this.toolStripMenuItemDateiVerbinden.Click += new System.EventHandler(this.toolStripMenuItemDateiVerbinden_Click);
			// 
			// toolStripMenuItemDateiTrennen
			// 
			this.toolStripMenuItemDateiTrennen.Enabled = false;
			this.toolStripMenuItemDateiTrennen.Name = "toolStripMenuItemDateiTrennen";
			this.toolStripMenuItemDateiTrennen.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.T)));
			this.toolStripMenuItemDateiTrennen.Size = new System.Drawing.Size(254, 22);
			this.toolStripMenuItemDateiTrennen.Text = "&Trennen";
			this.toolStripMenuItemDateiTrennen.Click += new System.EventHandler(this.toolStripMenuItemDateiTrennen_Click);
			// 
			// toolStripSeparatorDatei4
			// 
			this.toolStripSeparatorDatei4.Name = "toolStripSeparatorDatei4";
			this.toolStripSeparatorDatei4.Size = new System.Drawing.Size(251, 6);
			// 
			// toolStripMenuItemDateiZuletztGeöfnetteAnlage
			// 
			this.toolStripMenuItemDateiZuletztGeöfnetteAnlage.Enabled = false;
			this.toolStripMenuItemDateiZuletztGeöfnetteAnlage.Name = "toolStripMenuItemDateiZuletztGeöfnetteAnlage";
			this.toolStripMenuItemDateiZuletztGeöfnetteAnlage.Size = new System.Drawing.Size(254, 22);
			this.toolStripMenuItemDateiZuletztGeöfnetteAnlage.Text = "&Zuletzt geöfnette Anlage";
			// 
			// toolStripMenuItemDateiZuletztVerbundeneMaster
			// 
			this.toolStripMenuItemDateiZuletztVerbundeneMaster.Enabled = false;
			this.toolStripMenuItemDateiZuletztVerbundeneMaster.Name = "toolStripMenuItemDateiZuletztVerbundeneMaster";
			this.toolStripMenuItemDateiZuletztVerbundeneMaster.Size = new System.Drawing.Size(254, 22);
			this.toolStripMenuItemDateiZuletztVerbundeneMaster.Text = "&Zuletzt verbundene Master";
			// 
			// toolStripSeparatorDatei5
			// 
			this.toolStripSeparatorDatei5.Name = "toolStripSeparatorDatei5";
			this.toolStripSeparatorDatei5.Size = new System.Drawing.Size(251, 6);
			// 
			// toolStripMenuItemDateiBeenden
			// 
			this.toolStripMenuItemDateiBeenden.Image = global::ModellBahnSteuerung.Properties.Resources.Close_16x;
			this.toolStripMenuItemDateiBeenden.Name = "toolStripMenuItemDateiBeenden";
			this.toolStripMenuItemDateiBeenden.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.toolStripMenuItemDateiBeenden.Size = new System.Drawing.Size(254, 22);
			this.toolStripMenuItemDateiBeenden.Text = "&Beenden";
			this.toolStripMenuItemDateiBeenden.Click += new System.EventHandler(this.toolStripMenuItemDateiBeenden_Click);
			// 
			// toolStripMenuItemZoom
			// 
			this.toolStripMenuItemZoom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemZoomPlus,
            this.toolStripMenuItemZoomMinus,
            this.toolStripSeparatorZoom1,
            this.toolStripMenuItemZoomZurücksetzen});
			this.toolStripMenuItemZoom.Enabled = false;
			this.toolStripMenuItemZoom.Name = "toolStripMenuItemZoom";
			this.toolStripMenuItemZoom.Size = new System.Drawing.Size(51, 22);
			this.toolStripMenuItemZoom.Text = "&Zoom";
			// 
			// toolStripMenuItemZoomPlus
			// 
			this.toolStripMenuItemZoomPlus.Image = global::ModellBahnSteuerung.Properties.Resources.Plus;
			this.toolStripMenuItemZoomPlus.Name = "toolStripMenuItemZoomPlus";
			this.toolStripMenuItemZoomPlus.Size = new System.Drawing.Size(144, 22);
			this.toolStripMenuItemZoomPlus.Text = "&Plus";
			this.toolStripMenuItemZoomPlus.Click += new System.EventHandler(this.toolStripMenuItemPlus_Click);
			// 
			// toolStripMenuItemZoomMinus
			// 
			this.toolStripMenuItemZoomMinus.Image = global::ModellBahnSteuerung.Properties.Resources.Minus;
			this.toolStripMenuItemZoomMinus.Name = "toolStripMenuItemZoomMinus";
			this.toolStripMenuItemZoomMinus.Size = new System.Drawing.Size(144, 22);
			this.toolStripMenuItemZoomMinus.Text = "&Minus";
			this.toolStripMenuItemZoomMinus.Click += new System.EventHandler(this.toolStripMenuItemMinus_Click);
			// 
			// toolStripSeparatorZoom1
			// 
			this.toolStripSeparatorZoom1.Name = "toolStripSeparatorZoom1";
			this.toolStripSeparatorZoom1.Size = new System.Drawing.Size(141, 6);
			// 
			// toolStripMenuItemZoomZurücksetzen
			// 
			this.toolStripMenuItemZoomZurücksetzen.Image = global::ModellBahnSteuerung.Properties.Resources.Raster;
			this.toolStripMenuItemZoomZurücksetzen.Name = "toolStripMenuItemZoomZurücksetzen";
			this.toolStripMenuItemZoomZurücksetzen.Size = new System.Drawing.Size(144, 22);
			this.toolStripMenuItemZoomZurücksetzen.Text = "&Zurücksetzen";
			this.toolStripMenuItemZoomZurücksetzen.Click += new System.EventHandler(this.toolStripMenuItemZurücksetzen_Click);
			// 
			// toolStripMenuItemBearbeiten
			// 
			this.toolStripMenuItemBearbeiten.CheckOnClick = true;
			this.toolStripMenuItemBearbeiten.Enabled = false;
			this.toolStripMenuItemBearbeiten.Margin = new System.Windows.Forms.Padding(0, 1, 0, 2);
			this.toolStripMenuItemBearbeiten.Name = "toolStripMenuItemBearbeiten";
			this.toolStripMenuItemBearbeiten.Size = new System.Drawing.Size(115, 19);
			this.toolStripMenuItemBearbeiten.Text = "Anlage Bearbeiten";
			this.toolStripMenuItemBearbeiten.Click += new System.EventHandler(this.toolStripMenuItemBearbeiten_Click);
			// 
			// zugEditorToolStripMenuItem
			// 
			this.zugEditorToolStripMenuItem.Name = "zugEditorToolStripMenuItem";
			this.zugEditorToolStripMenuItem.Size = new System.Drawing.Size(76, 22);
			this.zugEditorToolStripMenuItem.Text = "Zug-Editor";
			this.zugEditorToolStripMenuItem.Click += new System.EventHandler(this.zugEditorToolStripMenuItem_Click);
			// 
			// toolStripMenuItemExtras
			// 
			this.toolStripMenuItemExtras.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemExtrasServer,
            this.toolStripMenuItemExtraArduino,
            this.toolStripSeparatorExtras1,
            this.toolStripMenuItemExtrasEinstellungen});
			this.toolStripMenuItemExtras.Name = "toolStripMenuItemExtras";
			this.toolStripMenuItemExtras.Size = new System.Drawing.Size(49, 22);
			this.toolStripMenuItemExtras.Text = "E&xtras";
			// 
			// toolStripMenuItemExtrasServer
			// 
			this.toolStripMenuItemExtrasServer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemExtrasServerStart,
            this.toolStripMenuItemExtrasServerStop});
			this.toolStripMenuItemExtrasServer.Enabled = false;
			this.toolStripMenuItemExtrasServer.Name = "toolStripMenuItemExtrasServer";
			this.toolStripMenuItemExtrasServer.Size = new System.Drawing.Size(145, 22);
			this.toolStripMenuItemExtrasServer.Text = "&Server";
			// 
			// toolStripMenuItemExtrasServerStart
			// 
			this.toolStripMenuItemExtrasServerStart.Image = global::ModellBahnSteuerung.Properties.Resources.StartWithoutDebug_16x;
			this.toolStripMenuItemExtrasServerStart.Name = "toolStripMenuItemExtrasServerStart";
			this.toolStripMenuItemExtrasServerStart.Size = new System.Drawing.Size(98, 22);
			this.toolStripMenuItemExtrasServerStart.Text = "&Start";
			this.toolStripMenuItemExtrasServerStart.Click += new System.EventHandler(this.toolStripMenuItemExtrasServerStart_Click);
			// 
			// toolStripMenuItemExtrasServerStop
			// 
			this.toolStripMenuItemExtrasServerStop.Enabled = false;
			this.toolStripMenuItemExtrasServerStop.Image = global::ModellBahnSteuerung.Properties.Resources.Stop_16x;
			this.toolStripMenuItemExtrasServerStop.Name = "toolStripMenuItemExtrasServerStop";
			this.toolStripMenuItemExtrasServerStop.Size = new System.Drawing.Size(98, 22);
			this.toolStripMenuItemExtrasServerStop.Text = "&Stop";
			this.toolStripMenuItemExtrasServerStop.Click += new System.EventHandler(this.toolStripMenuItemExtrasServerStop_Click);
			// 
			// toolStripMenuItemExtraArduino
			// 
			this.toolStripMenuItemExtraArduino.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemExtrasArduinoVerbinden,
            this.toolStripMenuItemExtrasArduinoTrennen});
			this.toolStripMenuItemExtraArduino.Name = "toolStripMenuItemExtraArduino";
			this.toolStripMenuItemExtraArduino.Size = new System.Drawing.Size(145, 22);
			this.toolStripMenuItemExtraArduino.Text = "Arduino";
			// 
			// toolStripMenuItemExtrasArduinoVerbinden
			// 
			this.toolStripMenuItemExtrasArduinoVerbinden.Name = "toolStripMenuItemExtrasArduinoVerbinden";
			this.toolStripMenuItemExtrasArduinoVerbinden.Size = new System.Drawing.Size(128, 22);
			this.toolStripMenuItemExtrasArduinoVerbinden.Text = "Verbinden";
			this.toolStripMenuItemExtrasArduinoVerbinden.Click += new System.EventHandler(this.ToolStripMenuItemArduinoVerbinden_Click);
			// 
			// toolStripMenuItemExtrasArduinoTrennen
			// 
			this.toolStripMenuItemExtrasArduinoTrennen.Enabled = false;
			this.toolStripMenuItemExtrasArduinoTrennen.Name = "toolStripMenuItemExtrasArduinoTrennen";
			this.toolStripMenuItemExtrasArduinoTrennen.Size = new System.Drawing.Size(128, 22);
			this.toolStripMenuItemExtrasArduinoTrennen.Text = "Trennen";
			this.toolStripMenuItemExtrasArduinoTrennen.Click += new System.EventHandler(this.trennenToolStripMenuItem_Click);
			// 
			// toolStripSeparatorExtras1
			// 
			this.toolStripSeparatorExtras1.Name = "toolStripSeparatorExtras1";
			this.toolStripSeparatorExtras1.Size = new System.Drawing.Size(142, 6);
			// 
			// toolStripMenuItemExtrasEinstellungen
			// 
			this.toolStripMenuItemExtrasEinstellungen.Image = global::ModellBahnSteuerung.Properties.Resources.Setting;
			this.toolStripMenuItemExtrasEinstellungen.Name = "toolStripMenuItemExtrasEinstellungen";
			this.toolStripMenuItemExtrasEinstellungen.Size = new System.Drawing.Size(145, 22);
			this.toolStripMenuItemExtrasEinstellungen.Text = "&Einstellungen";
			this.toolStripMenuItemExtrasEinstellungen.Click += new System.EventHandler(this.toolStripMenuItemEinstellungen_Click);
			// 
			// toolStripMenuItemHilfe
			// 
			this.toolStripMenuItemHilfe.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemHilfeHilfe,
            this.toolStripSeparatorHilfe1,
            this.logToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripMenuItemHilfeInfo});
			this.toolStripMenuItemHilfe.Name = "toolStripMenuItemHilfe";
			this.toolStripMenuItemHilfe.Size = new System.Drawing.Size(44, 22);
			this.toolStripMenuItemHilfe.Text = "&Hilfe";
			// 
			// toolStripMenuItemHilfeHilfe
			// 
			this.toolStripMenuItemHilfeHilfe.Image = global::ModellBahnSteuerung.Properties.Resources.Help;
			this.toolStripMenuItemHilfeHilfe.Name = "toolStripMenuItemHilfeHilfe";
			this.toolStripMenuItemHilfeHilfe.Size = new System.Drawing.Size(149, 22);
			this.toolStripMenuItemHilfeHilfe.Text = "&Hilfe anzeigen";
			this.toolStripMenuItemHilfeHilfe.Click += new System.EventHandler(this.toolStripMenuItemHilfeHilfe_Click);
			// 
			// toolStripSeparatorHilfe1
			// 
			this.toolStripSeparatorHilfe1.Name = "toolStripSeparatorHilfe1";
			this.toolStripSeparatorHilfe1.Size = new System.Drawing.Size(146, 6);
			// 
			// logToolStripMenuItem
			// 
			this.logToolStripMenuItem.Name = "logToolStripMenuItem";
			this.logToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
			this.logToolStripMenuItem.Text = "Log anzeigen";
			this.logToolStripMenuItem.Click += new System.EventHandler(this.logToolStripMenuItem_Click_1);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(146, 6);
			// 
			// toolStripMenuItemHilfeInfo
			// 
			this.toolStripMenuItemHilfeInfo.Image = global::ModellBahnSteuerung.Properties.Resources.Info;
			this.toolStripMenuItemHilfeInfo.Name = "toolStripMenuItemHilfeInfo";
			this.toolStripMenuItemHilfeInfo.Size = new System.Drawing.Size(149, 22);
			this.toolStripMenuItemHilfeInfo.Text = "&Info über ...";
			this.toolStripMenuItemHilfeInfo.Click += new System.EventHandler(this.toolStripMenuItemHilfeInfo_Click);
			// 
			// toolStripContainer
			// 
			// 
			// toolStripContainer.ContentPanel
			// 
			this.toolStripContainer.ContentPanel.BackColor = System.Drawing.Color.White;
			this.toolStripContainer.ContentPanel.Controls.Add(this.panelView);
			this.toolStripContainer.ContentPanel.Margin = new System.Windows.Forms.Padding(0);
			this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(931, 400);
			this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			// 
			// toolStripContainer.LeftToolStripPanel
			// 
			this.toolStripContainer.LeftToolStripPanel.Controls.Add(this.toolStripElemente);
			this.toolStripContainer.Location = new System.Drawing.Point(0, 26);
			this.toolStripContainer.Name = "toolStripContainer";
			this.toolStripContainer.Size = new System.Drawing.Size(966, 400);
			this.toolStripContainer.TabIndex = 2;
			this.toolStripContainer.Text = "toolStripContainer1";
			// 
			// toolStripContainer.TopToolStripPanel
			// 
			this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStripBearbeiten);
			// 
			// panelView
			// 
			this.panelView.Controls.Add(this.vScrollBarView);
			this.panelView.Controls.Add(this.panelScrollView);
			this.panelView.Controls.Add(this.hScrollBarView);
			this.panelView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelView.Location = new System.Drawing.Point(0, 0);
			this.panelView.Margin = new System.Windows.Forms.Padding(0);
			this.panelView.Name = "panelView";
			this.panelView.Size = new System.Drawing.Size(931, 400);
			this.panelView.TabIndex = 6;
			this.panelView.Visible = false;
			this.panelView.VisibleChanged += new System.EventHandler(this.panelView_VisibleChanged);
			this.panelView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panelView_MouseDoubleClick);
			this.panelView.Resize += new System.EventHandler(this.panelView_Resize);
			// 
			// vScrollBarView
			// 
			this.vScrollBarView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.vScrollBarView.Location = new System.Drawing.Point(914, 0);
			this.vScrollBarView.Name = "vScrollBarView";
			this.vScrollBarView.Size = new System.Drawing.Size(17, 425);
			this.vScrollBarView.TabIndex = 4;
			this.vScrollBarView.ValueChanged += new System.EventHandler(this.vScrollBarView_ValueChanged);
			// 
			// panelScrollView
			// 
			this.panelScrollView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelScrollView.BackColor = System.Drawing.Color.White;
			this.panelScrollView.Controls.Add(this.pictureBoxView);
			this.panelScrollView.Location = new System.Drawing.Point(0, 0);
			this.panelScrollView.Margin = new System.Windows.Forms.Padding(0);
			this.panelScrollView.Name = "panelScrollView";
			this.panelScrollView.Size = new System.Drawing.Size(914, 383);
			this.panelScrollView.TabIndex = 3;
			// 
			// pictureBoxView
			// 
			this.pictureBoxView.BackColor = System.Drawing.Color.White;
			this.pictureBoxView.Location = new System.Drawing.Point(0, 0);
			this.pictureBoxView.Margin = new System.Windows.Forms.Padding(0);
			this.pictureBoxView.Name = "pictureBoxView";
			this.pictureBoxView.Size = new System.Drawing.Size(400, 200);
			this.pictureBoxView.TabIndex = 0;
			this.pictureBoxView.TabStop = false;
			this.pictureBoxView.DragDrop += new System.Windows.Forms.DragEventHandler(this.pictureBoxView_DragDrop);
			this.pictureBoxView.DragEnter += new System.Windows.Forms.DragEventHandler(this.pictureBoxView_DragEnter);
			this.pictureBoxView.DragOver += new System.Windows.Forms.DragEventHandler(this.pictureBoxView_DragOver);
			this.pictureBoxView.DragLeave += new System.EventHandler(this.pictureBoxView_DragLeave);
			this.pictureBoxView.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxView_Paint);
			this.pictureBoxView.DoubleClick += new System.EventHandler(this.pictureBoxView_DoubleClick);
			this.pictureBoxView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxView_MouseClick);
			this.pictureBoxView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxView_MouseDown);
			this.pictureBoxView.MouseEnter += new System.EventHandler(this.pictureBoxView_MouseEnter);
			this.pictureBoxView.MouseLeave += new System.EventHandler(this.pictureBoxView_MouseLeave);
			this.pictureBoxView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxView_MouseMove);
			this.pictureBoxView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxView_MouseUp);
			// 
			// hScrollBarView
			// 
			this.hScrollBarView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.hScrollBarView.Location = new System.Drawing.Point(0, 383);
			this.hScrollBarView.Name = "hScrollBarView";
			this.hScrollBarView.Size = new System.Drawing.Size(914, 17);
			this.hScrollBarView.TabIndex = 5;
			this.hScrollBarView.ValueChanged += new System.EventHandler(this.hScrollBarView_ValueChanged);
			// 
			// toolStripElemente
			// 
			this.toolStripElemente.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStripElemente.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonElementGleis,
            this.toolStripButtonElementSignal,
            this.toolStripButtonElementEntkuppler,
            this.toolStripButtonElementSchalter,
            this.toolStripButtonElementFss,
            this.toolStripButtonElementInfoElement,
            this.toolStripButtonElementEingSchalter});
			this.toolStripElemente.Location = new System.Drawing.Point(0, 3);
			this.toolStripElemente.Name = "toolStripElemente";
			this.toolStripElemente.Size = new System.Drawing.Size(35, 184);
			this.toolStripElemente.TabIndex = 0;
			this.toolStripElemente.Visible = false;
			// 
			// toolStripButtonElementGleis
			// 
			this.toolStripButtonElementGleis.CheckOnClick = true;
			this.toolStripButtonElementGleis.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonElementGleis.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonElementGleis.Image")));
			this.toolStripButtonElementGleis.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonElementGleis.Name = "toolStripButtonElementGleis";
			this.toolStripButtonElementGleis.Size = new System.Drawing.Size(33, 19);
			this.toolStripButtonElementGleis.Text = "G";
			this.toolStripButtonElementGleis.ToolTipText = "Gleis";
			this.toolStripButtonElementGleis.Click += new System.EventHandler(this.toolStripButtonElementGleis_Click);
			// 
			// toolStripButtonElementSignal
			// 
			this.toolStripButtonElementSignal.CheckOnClick = true;
			this.toolStripButtonElementSignal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonElementSignal.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonElementSignal.Image")));
			this.toolStripButtonElementSignal.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonElementSignal.Name = "toolStripButtonElementSignal";
			this.toolStripButtonElementSignal.Size = new System.Drawing.Size(33, 19);
			this.toolStripButtonElementSignal.Text = "Sig";
			this.toolStripButtonElementSignal.ToolTipText = "Signal";
			this.toolStripButtonElementSignal.Click += new System.EventHandler(this.toolStripButtonElementSignal_Click);
			// 
			// toolStripButtonElementEntkuppler
			// 
			this.toolStripButtonElementEntkuppler.CheckOnClick = true;
			this.toolStripButtonElementEntkuppler.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonElementEntkuppler.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonElementEntkuppler.Image")));
			this.toolStripButtonElementEntkuppler.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonElementEntkuppler.Name = "toolStripButtonElementEntkuppler";
			this.toolStripButtonElementEntkuppler.Size = new System.Drawing.Size(33, 19);
			this.toolStripButtonElementEntkuppler.Text = "E";
			this.toolStripButtonElementEntkuppler.ToolTipText = "Entkuppler";
			this.toolStripButtonElementEntkuppler.Click += new System.EventHandler(this.toolStripButtonElementEntkuppler_Click);
			// 
			// toolStripButtonElementSchalter
			// 
			this.toolStripButtonElementSchalter.CheckOnClick = true;
			this.toolStripButtonElementSchalter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonElementSchalter.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonElementSchalter.Image")));
			this.toolStripButtonElementSchalter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonElementSchalter.Name = "toolStripButtonElementSchalter";
			this.toolStripButtonElementSchalter.Size = new System.Drawing.Size(33, 19);
			this.toolStripButtonElementSchalter.Text = "Sch";
			this.toolStripButtonElementSchalter.ToolTipText = "Schalter";
			this.toolStripButtonElementSchalter.Click += new System.EventHandler(this.toolStripButtonElementSchalter_Click);
			// 
			// toolStripButtonElementFss
			// 
			this.toolStripButtonElementFss.CheckOnClick = true;
			this.toolStripButtonElementFss.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonElementFss.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonElementFss.Image")));
			this.toolStripButtonElementFss.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonElementFss.Name = "toolStripButtonElementFss";
			this.toolStripButtonElementFss.Size = new System.Drawing.Size(33, 19);
			this.toolStripButtonElementFss.Text = "Fss";
			this.toolStripButtonElementFss.ToolTipText = "Fahrstromschalter";
			this.toolStripButtonElementFss.Click += new System.EventHandler(this.toolStripButtonElementFss_Click);
			// 
			// toolStripButtonElementInfoElement
			// 
			this.toolStripButtonElementInfoElement.CheckOnClick = true;
			this.toolStripButtonElementInfoElement.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonElementInfoElement.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonElementInfoElement.Image")));
			this.toolStripButtonElementInfoElement.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonElementInfoElement.Name = "toolStripButtonElementInfoElement";
			this.toolStripButtonElementInfoElement.Size = new System.Drawing.Size(33, 19);
			this.toolStripButtonElementInfoElement.Text = "Info";
			this.toolStripButtonElementInfoElement.Click += new System.EventHandler(this.toolStripButtonElementInfoElement_Click);
			// 
			// toolStripButtonElementEingSchalter
			// 
			this.toolStripButtonElementEingSchalter.CheckOnClick = true;
			this.toolStripButtonElementEingSchalter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonElementEingSchalter.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonElementEingSchalter.Image")));
			this.toolStripButtonElementEingSchalter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonElementEingSchalter.Name = "toolStripButtonElementEingSchalter";
			this.toolStripButtonElementEingSchalter.Size = new System.Drawing.Size(33, 19);
			this.toolStripButtonElementEingSchalter.Text = "Eing";
			this.toolStripButtonElementEingSchalter.Click += new System.EventHandler(this.toolStripButtonElementEingSchalter_Click);
			// 
			// toolStripBearbeiten
			// 
			this.toolStripBearbeiten.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStripBearbeiten.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButtonCheckableGitter,
            this.toolStripLabelAbstand1,
            this.toolStripSplitButtonCheckableFadenkreuz,
            this.toolStripLabelAbstand2,
            this.toolStripButtonElementeEigenschaften,
            this.toolStripButtonFahrstrassenEditor,
            this.toolStripButton1,
            this.toolStripButtonElementeExportieren});
			this.toolStripBearbeiten.Location = new System.Drawing.Point(3, 0);
			this.toolStripBearbeiten.Name = "toolStripBearbeiten";
			this.toolStripBearbeiten.Size = new System.Drawing.Size(585, 25);
			this.toolStripBearbeiten.TabIndex = 0;
			this.toolStripBearbeiten.Visible = false;
			// 
			// toolStripSplitButtonCheckableGitter
			// 
			this.toolStripSplitButtonCheckableGitter.Checked = false;
			this.toolStripSplitButtonCheckableGitter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemGitterGitterLinie,
            this.toolStripMenuItemGitterGitterLinieVersetzt,
            this.toolStripMenuItemGitterGitterPunkt,
            this.toolStripMenuItemGitterGitterPunktVersetzt,
            this.toolStripSeparatorGitterGitter1,
            this.toolStripMenuItemGitterGitterFarbe});
			this.toolStripSplitButtonCheckableGitter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripSplitButtonCheckableGitter.Name = "toolStripSplitButtonCheckableGitter";
			this.toolStripSplitButtonCheckableGitter.Size = new System.Drawing.Size(52, 22);
			this.toolStripSplitButtonCheckableGitter.Text = "&Gitter";
			this.toolStripSplitButtonCheckableGitter.ButtonClick += new System.EventHandler(this.toolStripSplitButtonCheckableGitter_ButtonClick);
			// 
			// toolStripMenuItemGitterGitterLinie
			// 
			this.toolStripMenuItemGitterGitterLinie.Name = "toolStripMenuItemGitterGitterLinie";
			this.toolStripMenuItemGitterGitterLinie.Size = new System.Drawing.Size(148, 22);
			this.toolStripMenuItemGitterGitterLinie.Text = "&Linie";
			this.toolStripMenuItemGitterGitterLinie.Click += new System.EventHandler(this.toolStripMenuItemGitterGitterLinie_Click);
			// 
			// toolStripMenuItemGitterGitterLinieVersetzt
			// 
			this.toolStripMenuItemGitterGitterLinieVersetzt.Name = "toolStripMenuItemGitterGitterLinieVersetzt";
			this.toolStripMenuItemGitterGitterLinieVersetzt.Size = new System.Drawing.Size(148, 22);
			this.toolStripMenuItemGitterGitterLinieVersetzt.Text = "&Linie versetzt";
			this.toolStripMenuItemGitterGitterLinieVersetzt.Click += new System.EventHandler(this.toolStripMenuItemGitterGitterLinieVersetzt_Click);
			// 
			// toolStripMenuItemGitterGitterPunkt
			// 
			this.toolStripMenuItemGitterGitterPunkt.Name = "toolStripMenuItemGitterGitterPunkt";
			this.toolStripMenuItemGitterGitterPunkt.Size = new System.Drawing.Size(148, 22);
			this.toolStripMenuItemGitterGitterPunkt.Text = "&Punkt";
			this.toolStripMenuItemGitterGitterPunkt.Click += new System.EventHandler(this.toolStripMenuItemGitterGitterPunkt_Click);
			// 
			// toolStripMenuItemGitterGitterPunktVersetzt
			// 
			this.toolStripMenuItemGitterGitterPunktVersetzt.Name = "toolStripMenuItemGitterGitterPunktVersetzt";
			this.toolStripMenuItemGitterGitterPunktVersetzt.Size = new System.Drawing.Size(148, 22);
			this.toolStripMenuItemGitterGitterPunktVersetzt.Text = "&Punkt versetzt";
			this.toolStripMenuItemGitterGitterPunktVersetzt.Click += new System.EventHandler(this.toolStripMenuItemGitterGitterPunktVersetzt_Click);
			// 
			// toolStripSeparatorGitterGitter1
			// 
			this.toolStripSeparatorGitterGitter1.Name = "toolStripSeparatorGitterGitter1";
			this.toolStripSeparatorGitterGitter1.Size = new System.Drawing.Size(145, 6);
			// 
			// toolStripMenuItemGitterGitterFarbe
			// 
			this.toolStripMenuItemGitterGitterFarbe.Name = "toolStripMenuItemGitterGitterFarbe";
			this.toolStripMenuItemGitterGitterFarbe.Size = new System.Drawing.Size(148, 22);
			this.toolStripMenuItemGitterGitterFarbe.Text = "&Farbe";
			this.toolStripMenuItemGitterGitterFarbe.Click += new System.EventHandler(this.toolStripMenuItemGitterGitterFarbe_Click);
			// 
			// toolStripLabelAbstand1
			// 
			this.toolStripLabelAbstand1.AutoSize = false;
			this.toolStripLabelAbstand1.Name = "toolStripLabelAbstand1";
			this.toolStripLabelAbstand1.Size = new System.Drawing.Size(5, 22);
			// 
			// toolStripSplitButtonCheckableFadenkreuz
			// 
			this.toolStripSplitButtonCheckableFadenkreuz.Checked = false;
			this.toolStripSplitButtonCheckableFadenkreuz.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFadenkreuzFadenkreuzLinie,
            this.toolStripMenuItemFadenkreuzFadenkreuzPunkt,
            this.toolStripSeparatorFadenkreuzFadenkreuz1,
            this.toolStripMenuItemFadenkreuzFadenkreuzFarbe});
			this.toolStripSplitButtonCheckableFadenkreuz.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripSplitButtonCheckableFadenkreuz.Name = "toolStripSplitButtonCheckableFadenkreuz";
			this.toolStripSplitButtonCheckableFadenkreuz.Size = new System.Drawing.Size(83, 22);
			this.toolStripSplitButtonCheckableFadenkreuz.Text = "Fadenkreuz";
			this.toolStripSplitButtonCheckableFadenkreuz.ButtonClick += new System.EventHandler(this.toolStripSplitButtonCheckableFadenkreuz_ButtonClick);
			// 
			// toolStripMenuItemFadenkreuzFadenkreuzLinie
			// 
			this.toolStripMenuItemFadenkreuzFadenkreuzLinie.Name = "toolStripMenuItemFadenkreuzFadenkreuzLinie";
			this.toolStripMenuItemFadenkreuzFadenkreuzLinie.Size = new System.Drawing.Size(105, 22);
			this.toolStripMenuItemFadenkreuzFadenkreuzLinie.Text = "&Linie";
			this.toolStripMenuItemFadenkreuzFadenkreuzLinie.Click += new System.EventHandler(this.toolStripMenuItemFadenkreuzFadenkreuzLinie_Click);
			// 
			// toolStripMenuItemFadenkreuzFadenkreuzPunkt
			// 
			this.toolStripMenuItemFadenkreuzFadenkreuzPunkt.Name = "toolStripMenuItemFadenkreuzFadenkreuzPunkt";
			this.toolStripMenuItemFadenkreuzFadenkreuzPunkt.Size = new System.Drawing.Size(105, 22);
			this.toolStripMenuItemFadenkreuzFadenkreuzPunkt.Text = "&Punkt";
			this.toolStripMenuItemFadenkreuzFadenkreuzPunkt.Click += new System.EventHandler(this.toolStripMenuItemFadenkreuzFadenkreuzPunkt_Click);
			// 
			// toolStripSeparatorFadenkreuzFadenkreuz1
			// 
			this.toolStripSeparatorFadenkreuzFadenkreuz1.Name = "toolStripSeparatorFadenkreuzFadenkreuz1";
			this.toolStripSeparatorFadenkreuzFadenkreuz1.Size = new System.Drawing.Size(102, 6);
			// 
			// toolStripMenuItemFadenkreuzFadenkreuzFarbe
			// 
			this.toolStripMenuItemFadenkreuzFadenkreuzFarbe.Name = "toolStripMenuItemFadenkreuzFadenkreuzFarbe";
			this.toolStripMenuItemFadenkreuzFadenkreuzFarbe.Size = new System.Drawing.Size(105, 22);
			this.toolStripMenuItemFadenkreuzFadenkreuzFarbe.Text = "&Farbe";
			this.toolStripMenuItemFadenkreuzFadenkreuzFarbe.Click += new System.EventHandler(this.toolStripMenuItemFadenkreuzFadenkreuzFarbe_Click);
			// 
			// toolStripLabelAbstand2
			// 
			this.toolStripLabelAbstand2.AutoSize = false;
			this.toolStripLabelAbstand2.Name = "toolStripLabelAbstand2";
			this.toolStripLabelAbstand2.Size = new System.Drawing.Size(5, 22);
			// 
			// toolStripButtonElementeEigenschaften
			// 
			this.toolStripButtonElementeEigenschaften.CheckOnClick = true;
			this.toolStripButtonElementeEigenschaften.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonElementeEigenschaften.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonElementeEigenschaften.Image")));
			this.toolStripButtonElementeEigenschaften.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonElementeEigenschaften.Margin = new System.Windows.Forms.Padding(0, 1, 0, 2);
			this.toolStripButtonElementeEigenschaften.Name = "toolStripButtonElementeEigenschaften";
			this.toolStripButtonElementeEigenschaften.Size = new System.Drawing.Size(145, 22);
			this.toolStripButtonElementeEigenschaften.Text = "Elemente Eigenschaften";
			this.toolStripButtonElementeEigenschaften.Click += new System.EventHandler(this.toolStripButtonElementeEigenschaften_Click);
			// 
			// toolStripButtonFahrstrassenEditor
			// 
			this.toolStripButtonFahrstrassenEditor.CheckOnClick = true;
			this.toolStripButtonFahrstrassenEditor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonFahrstrassenEditor.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonFahrstrassenEditor.Image")));
			this.toolStripButtonFahrstrassenEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonFahrstrassenEditor.Margin = new System.Windows.Forms.Padding(0, 1, 0, 2);
			this.toolStripButtonFahrstrassenEditor.Name = "toolStripButtonFahrstrassenEditor";
			this.toolStripButtonFahrstrassenEditor.Size = new System.Drawing.Size(117, 22);
			this.toolStripButtonFahrstrassenEditor.Text = "Fahrstraßen-Editor";
			this.toolStripButtonFahrstrassenEditor.Click += new System.EventHandler(this.toolStripButtonFahrstrassenEditor_Click);
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(96, 22);
			this.toolStripButton1.Text = "Stecker-Anzeige";
			this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
			// 
			// toolStripButtonElementeExportieren
			// 
			this.toolStripButtonElementeExportieren.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonElementeExportieren.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonElementeExportieren.Image")));
			this.toolStripButtonElementeExportieren.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonElementeExportieren.Name = "toolStripButtonElementeExportieren";
			this.toolStripButtonElementeExportieren.Size = new System.Drawing.Size(70, 22);
			this.toolStripButtonElementeExportieren.Text = "Exportieren";
			this.toolStripButtonElementeExportieren.Click += new System.EventHandler(this.toolStripButtonElementeExportieren_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
			this.toolStripMenuItem1.Text = "toolStripMenuItem1";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
			this.toolStripMenuItem2.Text = "toolStripMenuItem2";
			// 
			// timerZeit
			// 
			this.timerZeit.Interval = 1000;
			this.timerZeit.Tick += new System.EventHandler(this.TimerZeit_Tick);
			// 
			// contextMenuStripAuswahlRechteck
			// 
			this.contextMenuStripAuswahlRechteck.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAufheben,
            this.toolStripSeparatorAuswahl1,
            this.toolStripMenuItemDrehen,
            this.toolStripMenuItemSpiegeln,
            this.toolStripSeparatorAuswahl2,
            this.toolStripMenuItemLöschen});
			this.contextMenuStripAuswahlRechteck.Name = "contextMenuStripAuswahlRechteck";
			this.contextMenuStripAuswahlRechteck.Size = new System.Drawing.Size(127, 104);
			// 
			// toolStripMenuItemAufheben
			// 
			this.toolStripMenuItemAufheben.Name = "toolStripMenuItemAufheben";
			this.toolStripMenuItemAufheben.Size = new System.Drawing.Size(126, 22);
			this.toolStripMenuItemAufheben.Text = "Aufheben";
			this.toolStripMenuItemAufheben.Click += new System.EventHandler(this.toolStripMenuItemAufheben_Click);
			// 
			// toolStripSeparatorAuswahl1
			// 
			this.toolStripSeparatorAuswahl1.Name = "toolStripSeparatorAuswahl1";
			this.toolStripSeparatorAuswahl1.Size = new System.Drawing.Size(123, 6);
			// 
			// toolStripMenuItemDrehen
			// 
			this.toolStripMenuItemDrehen.Name = "toolStripMenuItemDrehen";
			this.toolStripMenuItemDrehen.Size = new System.Drawing.Size(126, 22);
			this.toolStripMenuItemDrehen.Text = "Drehen";
			this.toolStripMenuItemDrehen.Click += new System.EventHandler(this.toolStripMenuItemDrehen_Click);
			// 
			// toolStripMenuItemSpiegeln
			// 
			this.toolStripMenuItemSpiegeln.Name = "toolStripMenuItemSpiegeln";
			this.toolStripMenuItemSpiegeln.Size = new System.Drawing.Size(126, 22);
			this.toolStripMenuItemSpiegeln.Text = "Spiegeln";
			this.toolStripMenuItemSpiegeln.Click += new System.EventHandler(this.toolStripMenuItemSpiegeln_Click);
			// 
			// toolStripSeparatorAuswahl2
			// 
			this.toolStripSeparatorAuswahl2.Name = "toolStripSeparatorAuswahl2";
			this.toolStripSeparatorAuswahl2.Size = new System.Drawing.Size(123, 6);
			// 
			// toolStripMenuItemLöschen
			// 
			this.toolStripMenuItemLöschen.Name = "toolStripMenuItemLöschen";
			this.toolStripMenuItemLöschen.Size = new System.Drawing.Size(126, 22);
			this.toolStripMenuItemLöschen.Text = "Löschen";
			this.toolStripMenuItemLöschen.Click += new System.EventHandler(this.toolStripMenuItemAuswahlRechteckLöschen_Click);
			// 
			// MoBaStForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(966, 450);
			this.Controls.Add(this.toolStripContainer);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.menuStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MainMenuStrip = this.menuStrip;
			this.Name = "MoBaStForm";
			this.Text = "Modellbahnsteuerung";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMoBaSt_FormClosing);
			this.Load += new System.EventHandler(this.MoBaStForm_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MoBaStForm_KeyDown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MoBaStForm_KeyUp);
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.toolStripContainer.ContentPanel.ResumeLayout(false);
			this.toolStripContainer.LeftToolStripPanel.ResumeLayout(false);
			this.toolStripContainer.LeftToolStripPanel.PerformLayout();
			this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer.TopToolStripPanel.PerformLayout();
			this.toolStripContainer.ResumeLayout(false);
			this.toolStripContainer.PerformLayout();
			this.panelView.ResumeLayout(false);
			this.panelScrollView.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxView)).EndInit();
			this.toolStripElemente.ResumeLayout(false);
			this.toolStripElemente.PerformLayout();
			this.toolStripBearbeiten.ResumeLayout(false);
			this.toolStripBearbeiten.PerformLayout();
			this.contextMenuStripAuswahlRechteck.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}


		#endregion

		private System.Windows.Forms.ToolStripContainer toolStripContainer;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDatei;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDateiNeu;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDateiLaden;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorDatei1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDateiSpeichern;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDateiSpeichernUnter;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorDatei2;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDateiSchließen;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDateiVerbinden;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDateiTrennen;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorDatei3;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDateiZuletztGeöfnetteAnlage;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDateiZuletztVerbundeneMaster;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorDatei4;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDateiBeenden;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemZoom;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemZoomPlus;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemZoomMinus;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorZoom1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemZoomZurücksetzen;
		private Master.ToolStripMenuItemCheckable toolStripMenuItemBearbeiten;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExtras;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExtrasServer;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExtrasServerStart;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExtrasServerStop;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorExtras1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExtrasEinstellungen;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHilfe;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHilfeHilfe;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHilfeInfo;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorHilfe1;
		private System.Windows.Forms.ToolStrip toolStripBearbeiten;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemGitterGitterLinieVersetzt;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemGitterGitterLinie;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorGitterGitter1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemGitterGitterPunktVersetzt;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemGitterGitterPunkt;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemGitterGitterFarbe;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFadenkreuzFadenkreuzLinie;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFadenkreuzFadenkreuzPunkt;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorFadenkreuzFadenkreuz1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFadenkreuzFadenkreuzFarbe;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelInfo;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelZoom;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelZoomInfo;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelAbstand1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelAbstand2;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelMinus;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelAbstand3;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelZeit;
		private Master.ToolStripSplitButtonCheckable toolStripSplitButtonCheckableGitter;
		private Master.ToolStripSplitButtonCheckable toolStripSplitButtonCheckableFadenkreuz;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorDatei5;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelPlus;
		private System.Windows.Forms.ToolStripLabel toolStripLabelAbstand2;
		private System.Windows.Forms.Timer timerZeit;
		private Master.ToolStripMenuItemCheckable toolStripButtonElementeEigenschaften;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRaster;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRasterInfo;
		private System.Windows.Forms.ToolStripLabel toolStripLabelAbstand1;
		private System.Windows.Forms.Panel panelScrollView;
		private System.Windows.Forms.PictureBox pictureBoxView;
		private System.Windows.Forms.VScrollBar vScrollBarView;
		private System.Windows.Forms.HScrollBar hScrollBarView;
		private System.Windows.Forms.Panel panelView;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripAuswahlRechteck;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLöschen;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDrehen;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSpiegeln;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorAuswahl2;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAufheben;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorAuswahl1;
		private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExtraArduino;
		private ToolStripMenuItem toolStripMenuItemExtrasArduinoVerbinden;
		private ToolStripMenuItem toolStripMenuItemExtrasArduinoTrennen;
		private ToolStrip toolStripElemente;
		private ToolStripButton toolStripButtonElementGleis;
		private ToolStripButton toolStripButtonElementSignal;
		private ToolStripButton toolStripButtonElementEntkuppler;
		private ToolStripButton toolStripButtonElementSchalter;
		private ToolStripMenuItem zugEditorToolStripMenuItem;
		private ToolStripButton toolStripButtonElementFss;
		private ToolStripButton toolStripButtonElementeExportieren;
		private ToolStripButton toolStripButton1;
		private FontDialog fontDialog1;
		private Master.ToolStripMenuItemCheckable toolStripButtonFahrstrassenEditor;
		private ToolStripButton toolStripButtonElementInfoElement;
		private ToolStripButton toolStripButtonElementEingSchalter;
	}
}