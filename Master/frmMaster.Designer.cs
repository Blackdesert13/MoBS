namespace UCfM
{
  partial class frmMaster
  {
    /// <summary>
    /// Erforderliche Designervariable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Vom Windows Form-Designer generierter Code

    /// <summary>
    /// Erforderliche Methode für die Designerunterstützung.
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMaster));
      this.statusStrip = new System.Windows.Forms.StatusStrip();
      this.toolStripStatusLabelInfo = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripStatusLabelMaster = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripStatusLabelMasterInfo = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripStatusLabelZoom = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripStatusLabelZoomInfo = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripStatusLabelAbstand1 = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripStatusLabelAbstand2 = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripStatusLabelAbstand3 = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripStatusLabelZeit = new System.Windows.Forms.ToolStripStatusLabel();
      this.menuStrip = new System.Windows.Forms.MenuStrip();
      this.toolStripMenuItemDatei = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemNeu = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemLaden = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemVerbinden = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemTrennen = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparatorDatei1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripMenuItemSpeichern = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemSpeichernUnter = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparatorDatei2 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripMenuItemZuletztGeöfnetteAnlage = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparatorDatei3 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripMenuItemBeenden = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemZoom = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemPlus = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemMinus = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparatorZoom1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripMenuItemZurücksetzen = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemBearbeiten = new System.Windows.Forms.ToolStripButton();
      this.toolStripMenuItemExtras = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemServer = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemStart = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemStop = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparatorExtras1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripMenuItemEinstellungen = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemHilfe = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemHilfeAnzeigen = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparatorHilfe1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripMenuItemInfoÜber = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
      this.viewMaster = new UCfM.View();
      this.toolStripBearbeiten = new System.Windows.Forms.ToolStrip();
      this.toolStripSplitButtonRaster = new Master.ToolStripSplitButtonCheckable();
      this.toolStripMenuItemLinie = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemLinieVersetzt = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemPunkt = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemPunktVersetzt = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.farbeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripButtonFadenKreuz = new System.Windows.Forms.ToolStripButton();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.statusStrip.SuspendLayout();
      this.menuStrip.SuspendLayout();
      this.toolStripContainer.ContentPanel.SuspendLayout();
      this.toolStripContainer.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer.SuspendLayout();
      this.toolStripBearbeiten.SuspendLayout();
      this.SuspendLayout();
      // 
      // statusStrip
      // 
      this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelInfo,
            this.toolStripStatusLabelMaster,
            this.toolStripStatusLabelMasterInfo,
            this.toolStripStatusLabelZoom,
            this.toolStripStatusLabelZoomInfo,
            this.toolStripStatusLabelAbstand1,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabelAbstand2,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabelAbstand3,
            this.toolStripStatusLabelZeit});
      this.statusStrip.Location = new System.Drawing.Point(0, 416);
      this.statusStrip.Name = "statusStrip";
      this.statusStrip.Size = new System.Drawing.Size(671, 24);
      this.statusStrip.TabIndex = 0;
      this.statusStrip.Text = "statusStrip";
      // 
      // toolStripStatusLabelInfo
      // 
      this.toolStripStatusLabelInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.toolStripStatusLabelInfo.Name = "toolStripStatusLabelInfo";
      this.toolStripStatusLabelInfo.Size = new System.Drawing.Size(438, 19);
      this.toolStripStatusLabelInfo.Spring = true;
      this.toolStripStatusLabelInfo.Text = "Info";
      this.toolStripStatusLabelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // toolStripStatusLabelMaster
      // 
      this.toolStripStatusLabelMaster.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.toolStripStatusLabelMaster.Name = "toolStripStatusLabelMaster";
      this.toolStripStatusLabelMaster.Size = new System.Drawing.Size(43, 19);
      this.toolStripStatusLabelMaster.Text = "Master";
      this.toolStripStatusLabelMaster.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // toolStripStatusLabelMasterInfo
      // 
      this.toolStripStatusLabelMasterInfo.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
      this.toolStripStatusLabelMasterInfo.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
      this.toolStripStatusLabelMasterInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.toolStripStatusLabelMasterInfo.Name = "toolStripStatusLabelMasterInfo";
      this.toolStripStatusLabelMasterInfo.Size = new System.Drawing.Size(20, 19);
      this.toolStripStatusLabelMasterInfo.Text = "...";
      this.toolStripStatusLabelMasterInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // toolStripStatusLabelZoom
      // 
      this.toolStripStatusLabelZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
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
      this.toolStripStatusLabelZoomInfo.Name = "toolStripStatusLabelZoomInfo";
      this.toolStripStatusLabelZoomInfo.Size = new System.Drawing.Size(20, 19);
      this.toolStripStatusLabelZoomInfo.Text = "...";
      this.toolStripStatusLabelZoomInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // toolStripStatusLabelAbstand1
      // 
      this.toolStripStatusLabelAbstand1.AutoSize = false;
      this.toolStripStatusLabelAbstand1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.toolStripStatusLabelAbstand1.Name = "toolStripStatusLabelAbstand1";
      this.toolStripStatusLabelAbstand1.Size = new System.Drawing.Size(5, 19);
      // 
      // toolStripStatusLabel1
      // 
      this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripStatusLabel1.Image = global::Master.Properties.Resources.Minus;
      this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      this.toolStripStatusLabel1.Size = new System.Drawing.Size(16, 19);
      this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
      this.toolStripStatusLabel1.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
      // 
      // toolStripStatusLabelAbstand2
      // 
      this.toolStripStatusLabelAbstand2.AutoSize = false;
      this.toolStripStatusLabelAbstand2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.toolStripStatusLabelAbstand2.Name = "toolStripStatusLabelAbstand2";
      this.toolStripStatusLabelAbstand2.Size = new System.Drawing.Size(5, 19);
      // 
      // toolStripStatusLabel2
      // 
      this.toolStripStatusLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripStatusLabel2.Image = global::Master.Properties.Resources.Plus;
      this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
      this.toolStripStatusLabel2.Size = new System.Drawing.Size(16, 19);
      this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
      this.toolStripStatusLabel2.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
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
            this.toolStripMenuItemExtras,
            this.toolStripMenuItemHilfe});
      this.menuStrip.Location = new System.Drawing.Point(0, 0);
      this.menuStrip.Name = "menuStrip";
      this.menuStrip.Size = new System.Drawing.Size(671, 26);
      this.menuStrip.TabIndex = 1;
      this.menuStrip.Text = "menuStrip";
      // 
      // toolStripMenuItemDatei
      // 
      this.toolStripMenuItemDatei.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemNeu,
            this.toolStripMenuItemLaden,
            this.toolStripMenuItemVerbinden,
            this.toolStripMenuItemTrennen,
            this.toolStripSeparatorDatei1,
            this.toolStripMenuItemSpeichern,
            this.toolStripMenuItemSpeichernUnter,
            this.toolStripSeparatorDatei2,
            this.toolStripMenuItemZuletztGeöfnetteAnlage,
            this.toolStripSeparatorDatei3,
            this.toolStripMenuItemBeenden});
      this.toolStripMenuItemDatei.Name = "toolStripMenuItemDatei";
      this.toolStripMenuItemDatei.Size = new System.Drawing.Size(46, 22);
      this.toolStripMenuItemDatei.Text = "&Datei";
      // 
      // toolStripMenuItemNeu
      // 
      this.toolStripMenuItemNeu.Image = global::Master.Properties.Resources.NewFile_16x;
      this.toolStripMenuItemNeu.Name = "toolStripMenuItemNeu";
      this.toolStripMenuItemNeu.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
      this.toolStripMenuItemNeu.Size = new System.Drawing.Size(253, 22);
      this.toolStripMenuItemNeu.Text = "&Neu";
      // 
      // toolStripMenuItemLaden
      // 
      this.toolStripMenuItemLaden.Image = global::Master.Properties.Resources.OpenFileFromProject_16x;
      this.toolStripMenuItemLaden.Name = "toolStripMenuItemLaden";
      this.toolStripMenuItemLaden.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
      this.toolStripMenuItemLaden.Size = new System.Drawing.Size(253, 22);
      this.toolStripMenuItemLaden.Text = "&Laden";
      // 
      // toolStripMenuItemVerbinden
      // 
      this.toolStripMenuItemVerbinden.Name = "toolStripMenuItemVerbinden";
      this.toolStripMenuItemVerbinden.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.V)));
      this.toolStripMenuItemVerbinden.Size = new System.Drawing.Size(253, 22);
      this.toolStripMenuItemVerbinden.Text = "&Verbinden";
      // 
      // toolStripMenuItemTrennen
      // 
      this.toolStripMenuItemTrennen.Name = "toolStripMenuItemTrennen";
      this.toolStripMenuItemTrennen.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.T)));
      this.toolStripMenuItemTrennen.Size = new System.Drawing.Size(253, 22);
      this.toolStripMenuItemTrennen.Text = "&Trennen";
      // 
      // toolStripSeparatorDatei1
      // 
      this.toolStripSeparatorDatei1.Name = "toolStripSeparatorDatei1";
      this.toolStripSeparatorDatei1.Size = new System.Drawing.Size(250, 6);
      // 
      // toolStripMenuItemSpeichern
      // 
      this.toolStripMenuItemSpeichern.Image = global::Master.Properties.Resources.Save_16x;
      this.toolStripMenuItemSpeichern.Name = "toolStripMenuItemSpeichern";
      this.toolStripMenuItemSpeichern.Size = new System.Drawing.Size(253, 22);
      this.toolStripMenuItemSpeichern.Text = "&Speichern";
      // 
      // toolStripMenuItemSpeichernUnter
      // 
      this.toolStripMenuItemSpeichernUnter.Image = global::Master.Properties.Resources.SaveAs_16x;
      this.toolStripMenuItemSpeichernUnter.Name = "toolStripMenuItemSpeichernUnter";
      this.toolStripMenuItemSpeichernUnter.Size = new System.Drawing.Size(253, 22);
      this.toolStripMenuItemSpeichernUnter.Text = "&Speichern unter ...";
      // 
      // toolStripSeparatorDatei2
      // 
      this.toolStripSeparatorDatei2.Name = "toolStripSeparatorDatei2";
      this.toolStripSeparatorDatei2.Size = new System.Drawing.Size(250, 6);
      // 
      // toolStripMenuItemZuletztGeöfnetteAnlage
      // 
      this.toolStripMenuItemZuletztGeöfnetteAnlage.Name = "toolStripMenuItemZuletztGeöfnetteAnlage";
      this.toolStripMenuItemZuletztGeöfnetteAnlage.Size = new System.Drawing.Size(253, 22);
      this.toolStripMenuItemZuletztGeöfnetteAnlage.Text = "&Zuletzt geöfnette Anlage";
      // 
      // toolStripSeparatorDatei3
      // 
      this.toolStripSeparatorDatei3.Name = "toolStripSeparatorDatei3";
      this.toolStripSeparatorDatei3.Size = new System.Drawing.Size(250, 6);
      // 
      // toolStripMenuItemBeenden
      // 
      this.toolStripMenuItemBeenden.Image = global::Master.Properties.Resources.Close_16x;
      this.toolStripMenuItemBeenden.Name = "toolStripMenuItemBeenden";
      this.toolStripMenuItemBeenden.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
      this.toolStripMenuItemBeenden.Size = new System.Drawing.Size(253, 22);
      this.toolStripMenuItemBeenden.Text = "&Beenden";
      // 
      // toolStripMenuItemZoom
      // 
      this.toolStripMenuItemZoom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemPlus,
            this.toolStripMenuItemMinus,
            this.toolStripSeparatorZoom1,
            this.toolStripMenuItemZurücksetzen});
      this.toolStripMenuItemZoom.Name = "toolStripMenuItemZoom";
      this.toolStripMenuItemZoom.Size = new System.Drawing.Size(51, 22);
      this.toolStripMenuItemZoom.Text = "&Zoom";
      // 
      // toolStripMenuItemPlus
      // 
      this.toolStripMenuItemPlus.Image = global::Master.Properties.Resources.Plus;
      this.toolStripMenuItemPlus.Name = "toolStripMenuItemPlus";
      this.toolStripMenuItemPlus.Size = new System.Drawing.Size(144, 22);
      this.toolStripMenuItemPlus.Text = "&Plus";
      // 
      // toolStripMenuItemMinus
      // 
      this.toolStripMenuItemMinus.Image = global::Master.Properties.Resources.Minus;
      this.toolStripMenuItemMinus.Name = "toolStripMenuItemMinus";
      this.toolStripMenuItemMinus.Size = new System.Drawing.Size(144, 22);
      this.toolStripMenuItemMinus.Text = "&Minus";
      // 
      // toolStripSeparatorZoom1
      // 
      this.toolStripSeparatorZoom1.Name = "toolStripSeparatorZoom1";
      this.toolStripSeparatorZoom1.Size = new System.Drawing.Size(141, 6);
      // 
      // toolStripMenuItemZurücksetzen
      // 
      this.toolStripMenuItemZurücksetzen.Image = global::Master.Properties.Resources.Raster;
      this.toolStripMenuItemZurücksetzen.Name = "toolStripMenuItemZurücksetzen";
      this.toolStripMenuItemZurücksetzen.Size = new System.Drawing.Size(144, 22);
      this.toolStripMenuItemZurücksetzen.Text = "&Zurücksetzen";
      // 
      // toolStripMenuItemBearbeiten
      // 
      this.toolStripMenuItemBearbeiten.CheckOnClick = true;
      this.toolStripMenuItemBearbeiten.Name = "toolStripMenuItemBearbeiten";
      this.toolStripMenuItemBearbeiten.Size = new System.Drawing.Size(67, 19);
      this.toolStripMenuItemBearbeiten.Text = "&Bearbeiten";
      this.toolStripMenuItemBearbeiten.Click += new System.EventHandler(this.toolStripMenuItemBearbeiten_Click);
      // 
      // toolStripMenuItemExtras
      // 
      this.toolStripMenuItemExtras.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemServer,
            this.toolStripSeparatorExtras1,
            this.toolStripMenuItemEinstellungen});
      this.toolStripMenuItemExtras.Name = "toolStripMenuItemExtras";
      this.toolStripMenuItemExtras.Size = new System.Drawing.Size(49, 22);
      this.toolStripMenuItemExtras.Text = "E&xtras";
      // 
      // toolStripMenuItemServer
      // 
      this.toolStripMenuItemServer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemStart,
            this.toolStripMenuItemStop});
      this.toolStripMenuItemServer.Name = "toolStripMenuItemServer";
      this.toolStripMenuItemServer.Size = new System.Drawing.Size(145, 22);
      this.toolStripMenuItemServer.Text = "&Server";
      // 
      // toolStripMenuItemStart
      // 
      this.toolStripMenuItemStart.Image = global::Master.Properties.Resources.StartWithoutDebug_16x;
      this.toolStripMenuItemStart.Name = "toolStripMenuItemStart";
      this.toolStripMenuItemStart.Size = new System.Drawing.Size(98, 22);
      this.toolStripMenuItemStart.Text = "&Start";
      // 
      // toolStripMenuItemStop
      // 
      this.toolStripMenuItemStop.Image = global::Master.Properties.Resources.Stop_16x;
      this.toolStripMenuItemStop.Name = "toolStripMenuItemStop";
      this.toolStripMenuItemStop.Size = new System.Drawing.Size(98, 22);
      this.toolStripMenuItemStop.Text = "&Stop";
      // 
      // toolStripSeparatorExtras1
      // 
      this.toolStripSeparatorExtras1.Name = "toolStripSeparatorExtras1";
      this.toolStripSeparatorExtras1.Size = new System.Drawing.Size(142, 6);
      // 
      // toolStripMenuItemEinstellungen
      // 
      this.toolStripMenuItemEinstellungen.Image = global::Master.Properties.Resources.Setting;
      this.toolStripMenuItemEinstellungen.Name = "toolStripMenuItemEinstellungen";
      this.toolStripMenuItemEinstellungen.Size = new System.Drawing.Size(145, 22);
      this.toolStripMenuItemEinstellungen.Text = "&Einstellungen";
      // 
      // toolStripMenuItemHilfe
      // 
      this.toolStripMenuItemHilfe.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemHilfeAnzeigen,
            this.toolStripSeparatorHilfe1,
            this.toolStripMenuItemInfoÜber});
      this.toolStripMenuItemHilfe.Name = "toolStripMenuItemHilfe";
      this.toolStripMenuItemHilfe.Size = new System.Drawing.Size(44, 22);
      this.toolStripMenuItemHilfe.Text = "&Hilfe";
      // 
      // toolStripMenuItemHilfeAnzeigen
      // 
      this.toolStripMenuItemHilfeAnzeigen.Image = global::Master.Properties.Resources.Help;
      this.toolStripMenuItemHilfeAnzeigen.Name = "toolStripMenuItemHilfeAnzeigen";
      this.toolStripMenuItemHilfeAnzeigen.Size = new System.Drawing.Size(149, 22);
      this.toolStripMenuItemHilfeAnzeigen.Text = "&Hilfe anzeigen";
      // 
      // toolStripSeparatorHilfe1
      // 
      this.toolStripSeparatorHilfe1.Name = "toolStripSeparatorHilfe1";
      this.toolStripSeparatorHilfe1.Size = new System.Drawing.Size(146, 6);
      // 
      // toolStripMenuItemInfoÜber
      // 
      this.toolStripMenuItemInfoÜber.Image = global::Master.Properties.Resources.Info;
      this.toolStripMenuItemInfoÜber.Name = "toolStripMenuItemInfoÜber";
      this.toolStripMenuItemInfoÜber.Size = new System.Drawing.Size(149, 22);
      this.toolStripMenuItemInfoÜber.Text = "&Info über ...";
      // 
      // toolStripContainer
      // 
      // 
      // toolStripContainer.ContentPanel
      // 
      this.toolStripContainer.ContentPanel.Controls.Add(this.viewMaster);
      this.toolStripContainer.ContentPanel.Margin = new System.Windows.Forms.Padding(0);
      this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(671, 365);
      this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer.Location = new System.Drawing.Point(0, 26);
      this.toolStripContainer.Name = "toolStripContainer";
      this.toolStripContainer.Size = new System.Drawing.Size(671, 390);
      this.toolStripContainer.TabIndex = 2;
      this.toolStripContainer.Text = "toolStripContainer1";
      // 
      // toolStripContainer.TopToolStripPanel
      // 
      this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStripBearbeiten);
      // 
      // viewMaster
      // 
      this.viewMaster.Dock = System.Windows.Forms.DockStyle.Fill;
      this.viewMaster.Location = new System.Drawing.Point(0, 0);
      this.viewMaster.Margin = new System.Windows.Forms.Padding(0);
      this.viewMaster.Name = "viewMaster";
      this.viewMaster.Size = new System.Drawing.Size(671, 365);
      this.viewMaster.TabIndex = 0;
      // 
      // toolStripBearbeiten
      // 
      this.toolStripBearbeiten.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStripBearbeiten.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButtonRaster,
            this.toolStripButtonFadenKreuz});
      this.toolStripBearbeiten.Location = new System.Drawing.Point(3, 0);
      this.toolStripBearbeiten.Name = "toolStripBearbeiten";
      this.toolStripBearbeiten.Size = new System.Drawing.Size(138, 25);
      this.toolStripBearbeiten.TabIndex = 0;
      // 
      // toolStripSplitButtonRaster
      // 
      this.toolStripSplitButtonRaster.Checked = false;
      this.toolStripSplitButtonRaster.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemLinie,
            this.toolStripMenuItemLinieVersetzt,
            this.toolStripMenuItemPunkt,
            this.toolStripMenuItemPunktVersetzt,
            this.toolStripSeparator1,
            this.farbeToolStripMenuItem});
      this.toolStripSplitButtonRaster.Name = "toolStripSplitButtonRaster";
      this.toolStripSplitButtonRaster.Size = new System.Drawing.Size(55, 22);
      this.toolStripSplitButtonRaster.Text = "&Raster";
      this.toolStripSplitButtonRaster.ButtonClick += new System.EventHandler(this.toolStripSplitButtonRaster_ButtonClick);
      // 
      // toolStripMenuItemLinie
      // 
      this.toolStripMenuItemLinie.Name = "toolStripMenuItemLinie";
      this.toolStripMenuItemLinie.Size = new System.Drawing.Size(148, 22);
      this.toolStripMenuItemLinie.Text = "&Linie";
      // 
      // toolStripMenuItemLinieVersetzt
      // 
      this.toolStripMenuItemLinieVersetzt.Name = "toolStripMenuItemLinieVersetzt";
      this.toolStripMenuItemLinieVersetzt.Size = new System.Drawing.Size(148, 22);
      this.toolStripMenuItemLinieVersetzt.Text = "&Linie versetzt";
      // 
      // toolStripMenuItemPunkt
      // 
      this.toolStripMenuItemPunkt.Name = "toolStripMenuItemPunkt";
      this.toolStripMenuItemPunkt.Size = new System.Drawing.Size(148, 22);
      this.toolStripMenuItemPunkt.Text = "&Punkt";
      // 
      // toolStripMenuItemPunktVersetzt
      // 
      this.toolStripMenuItemPunktVersetzt.Name = "toolStripMenuItemPunktVersetzt";
      this.toolStripMenuItemPunktVersetzt.Size = new System.Drawing.Size(148, 22);
      this.toolStripMenuItemPunktVersetzt.Text = "&Punkt versetzt";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
      // 
      // farbeToolStripMenuItem
      // 
      this.farbeToolStripMenuItem.Name = "farbeToolStripMenuItem";
      this.farbeToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
      this.farbeToolStripMenuItem.Text = "&Farbe";
      this.farbeToolStripMenuItem.Click += new System.EventHandler(this.farbeToolStripMenuItem_Click);
      // 
      // toolStripButtonFadenKreuz
      // 
      this.toolStripButtonFadenKreuz.CheckOnClick = true;
      this.toolStripButtonFadenKreuz.Name = "toolStripButtonFadenKreuz";
      this.toolStripButtonFadenKreuz.Size = new System.Drawing.Size(71, 22);
      this.toolStripButtonFadenKreuz.Text = "&Fadenkreuz";
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
      // frmMaster
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(671, 440);
      this.Controls.Add(this.toolStripContainer);
      this.Controls.Add(this.statusStrip);
      this.Controls.Add(this.menuStrip);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.menuStrip;
      this.Name = "frmMaster";
      this.Text = "Modellbahnsteuerung Master";
      this.statusStrip.ResumeLayout(false);
      this.statusStrip.PerformLayout();
      this.menuStrip.ResumeLayout(false);
      this.menuStrip.PerformLayout();
      this.toolStripContainer.ContentPanel.ResumeLayout(false);
      this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer.TopToolStripPanel.PerformLayout();
      this.toolStripContainer.ResumeLayout(false);
      this.toolStripContainer.PerformLayout();
      this.toolStripBearbeiten.ResumeLayout(false);
      this.toolStripBearbeiten.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.StatusStrip statusStrip;
    private System.Windows.Forms.MenuStrip menuStrip;
    private System.Windows.Forms.ToolStripContainer toolStripContainer;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDatei;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNeu;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLaden;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparatorDatei1;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSpeichern;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSpeichernUnter;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparatorDatei3;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemBeenden;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemZoom;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPlus;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMinus;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparatorZoom1;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemZurücksetzen;
    private System.Windows.Forms.ToolStripButton toolStripMenuItemBearbeiten;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExtras;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemServer;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemStart;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemStop;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEinstellungen;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHilfe;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHilfeAnzeigen;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemInfoÜber;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparatorDatei2;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemZuletztGeöfnetteAnlage;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparatorHilfe1;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparatorExtras1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPunktVersetzt;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPunkt;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLinieVersetzt;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLinie;
    private Master.ToolStripSplitButtonCheckable toolStripSplitButtonRaster;
    private System.Windows.Forms.ToolStrip toolStripBearbeiten;
    private System.Windows.Forms.ToolStripMenuItem farbeToolStripMenuItem;
    private System.Windows.Forms.ToolStripButton toolStripButtonFadenKreuz;
    private View viewMaster;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelInfo;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelMaster;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelMasterInfo;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelZoom;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelZoomInfo;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelAbstand1;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelAbstand2;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelAbstand3;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelZeit;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemVerbinden;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTrennen;
  }
}

