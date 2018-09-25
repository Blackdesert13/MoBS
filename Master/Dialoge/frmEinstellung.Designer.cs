namespace MoBaSteuerung.Dialoge
{
  partial class frmEinstellung
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEinstellung));
            this.buttonSpeichern = new System.Windows.Forms.Button();
            this.buttonAbbrechen = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageAnzeige = new System.Windows.Forms.TabPage();
            this.entkupplerAbschaltAutoAktiv = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.entkupplerAbschaltAutoWert = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.fahrstraßeStartVerzögerung = new System.Windows.Forms.NumericUpDown();
            this.rückmeldungAktiv = new System.Windows.Forms.CheckBox();
            this.rückmeldungAnzeigen = new System.Windows.Forms.CheckBox();
            this.tabPageMaster = new System.Windows.Forms.TabPage();
            this.tabPageSlave = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.passwort = new System.Windows.Forms.TextBox();
            this.aktivierungAnlageBearbeiten = new System.Windows.Forms.CheckBox();
            this.servoSchrittweite = new System.Windows.Forms.NumericUpDown();
            this.labelServoSchrittweite = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPageAnzeige.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entkupplerAbschaltAutoWert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fahrstraßeStartVerzögerung)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.servoSchrittweite)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSpeichern
            // 
            this.buttonSpeichern.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSpeichern.Location = new System.Drawing.Point(540, 349);
            this.buttonSpeichern.Name = "buttonSpeichern";
            this.buttonSpeichern.Size = new System.Drawing.Size(75, 23);
            this.buttonSpeichern.TabIndex = 0;
            this.buttonSpeichern.Text = "Speichern";
            this.buttonSpeichern.UseVisualStyleBackColor = true;
            this.buttonSpeichern.Click += new System.EventHandler(this.buttonSpeichern_Click);
            // 
            // buttonAbbrechen
            // 
            this.buttonAbbrechen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAbbrechen.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonAbbrechen.Location = new System.Drawing.Point(459, 349);
            this.buttonAbbrechen.Name = "buttonAbbrechen";
            this.buttonAbbrechen.Size = new System.Drawing.Size(75, 23);
            this.buttonAbbrechen.TabIndex = 1;
            this.buttonAbbrechen.Text = "Abbrechen";
            this.buttonAbbrechen.UseVisualStyleBackColor = true;
            this.buttonAbbrechen.Click += new System.EventHandler(this.buttonAbbrechen_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageAnzeige);
            this.tabControl1.Controls.Add(this.tabPageMaster);
            this.tabControl1.Controls.Add(this.tabPageSlave);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(603, 331);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPageAnzeige
            // 
            this.tabPageAnzeige.Controls.Add(this.labelServoSchrittweite);
            this.tabPageAnzeige.Controls.Add(this.servoSchrittweite);
            this.tabPageAnzeige.Controls.Add(this.entkupplerAbschaltAutoAktiv);
            this.tabPageAnzeige.Controls.Add(this.label2);
            this.tabPageAnzeige.Controls.Add(this.entkupplerAbschaltAutoWert);
            this.tabPageAnzeige.Controls.Add(this.label1);
            this.tabPageAnzeige.Controls.Add(this.fahrstraßeStartVerzögerung);
            this.tabPageAnzeige.Controls.Add(this.rückmeldungAktiv);
            this.tabPageAnzeige.Controls.Add(this.rückmeldungAnzeigen);
            this.tabPageAnzeige.Location = new System.Drawing.Point(4, 22);
            this.tabPageAnzeige.Name = "tabPageAnzeige";
            this.tabPageAnzeige.Size = new System.Drawing.Size(595, 305);
            this.tabPageAnzeige.TabIndex = 2;
            this.tabPageAnzeige.Text = "allg. Einstellungen";
            this.tabPageAnzeige.UseVisualStyleBackColor = true;
            // 
            // entkupplerAbschaltAutoAktiv
            // 
            this.entkupplerAbschaltAutoAktiv.AutoSize = true;
            this.entkupplerAbschaltAutoAktiv.Location = new System.Drawing.Point(21, 221);
            this.entkupplerAbschaltAutoAktiv.Name = "entkupplerAbschaltAutoAktiv";
            this.entkupplerAbschaltAutoAktiv.Size = new System.Drawing.Size(197, 17);
            this.entkupplerAbschaltAutoAktiv.TabIndex = 6;
            this.entkupplerAbschaltAutoAktiv.Text = "Entkuppler automatisch ausschalten";
            this.entkupplerAbschaltAutoAktiv.UseVisualStyleBackColor = true;
            this.entkupplerAbschaltAutoAktiv.CheckedChanged += new System.EventHandler(this.entkupplerAbschaltAutoAktiv_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Entkuppler Timer (s)";
            // 
            // entkupplerAbschaltAutoWert
            // 
            this.entkupplerAbschaltAutoWert.Location = new System.Drawing.Point(21, 259);
            this.entkupplerAbschaltAutoWert.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.entkupplerAbschaltAutoWert.Name = "entkupplerAbschaltAutoWert";
            this.entkupplerAbschaltAutoWert.Size = new System.Drawing.Size(120, 20);
            this.entkupplerAbschaltAutoWert.TabIndex = 4;
            this.entkupplerAbschaltAutoWert.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(350, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Startverzögerung für Fahrstraßen (ms)";
            // 
            // fahrstraßeStartVerzögerung
            // 
            this.fahrstraßeStartVerzögerung.Location = new System.Drawing.Point(353, 43);
            this.fahrstraßeStartVerzögerung.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.fahrstraßeStartVerzögerung.Name = "fahrstraßeStartVerzögerung";
            this.fahrstraßeStartVerzögerung.Size = new System.Drawing.Size(120, 20);
            this.fahrstraßeStartVerzögerung.TabIndex = 2;
            // 
            // rückmeldungAktiv
            // 
            this.rückmeldungAktiv.AutoSize = true;
            this.rückmeldungAktiv.Location = new System.Drawing.Point(21, 60);
            this.rückmeldungAktiv.Name = "rückmeldungAktiv";
            this.rückmeldungAktiv.Size = new System.Drawing.Size(141, 17);
            this.rückmeldungAktiv.TabIndex = 1;
            this.rückmeldungAktiv.Text = "Rückmeldung aktivieren";
            this.rückmeldungAktiv.UseVisualStyleBackColor = true;
            // 
            // rückmeldungAnzeigen
            // 
            this.rückmeldungAnzeigen.AutoSize = true;
            this.rückmeldungAnzeigen.Location = new System.Drawing.Point(21, 27);
            this.rückmeldungAnzeigen.Name = "rückmeldungAnzeigen";
            this.rückmeldungAnzeigen.Size = new System.Drawing.Size(138, 17);
            this.rückmeldungAnzeigen.TabIndex = 0;
            this.rückmeldungAnzeigen.Text = "Rückmeldung anzeigen";
            this.rückmeldungAnzeigen.UseVisualStyleBackColor = true;
            // 
            // tabPageMaster
            // 
            this.tabPageMaster.Location = new System.Drawing.Point(4, 22);
            this.tabPageMaster.Name = "tabPageMaster";
            this.tabPageMaster.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMaster.Size = new System.Drawing.Size(595, 305);
            this.tabPageMaster.TabIndex = 0;
            this.tabPageMaster.Text = "Master";
            this.tabPageMaster.UseVisualStyleBackColor = true;
            // 
            // tabPageSlave
            // 
            this.tabPageSlave.Location = new System.Drawing.Point(4, 22);
            this.tabPageSlave.Name = "tabPageSlave";
            this.tabPageSlave.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSlave.Size = new System.Drawing.Size(595, 305);
            this.tabPageSlave.TabIndex = 1;
            this.tabPageSlave.Text = "Slave";
            this.tabPageSlave.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.passwort);
            this.tabPage1.Controls.Add(this.aktivierungAnlageBearbeiten);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(595, 305);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Admin";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // passwort
            // 
            this.passwort.Location = new System.Drawing.Point(32, 39);
            this.passwort.MaxLength = 20;
            this.passwort.Name = "passwort";
            this.passwort.PasswordChar = '*';
            this.passwort.Size = new System.Drawing.Size(100, 20);
            this.passwort.TabIndex = 1;
            this.passwort.TextChanged += new System.EventHandler(this.passwort_TextChanged);
            // 
            // aktivierungAnlageBearbeiten
            // 
            this.aktivierungAnlageBearbeiten.AutoSize = true;
            this.aktivierungAnlageBearbeiten.Location = new System.Drawing.Point(32, 77);
            this.aktivierungAnlageBearbeiten.Name = "aktivierungAnlageBearbeiten";
            this.aktivierungAnlageBearbeiten.Size = new System.Drawing.Size(168, 17);
            this.aktivierungAnlageBearbeiten.TabIndex = 0;
            this.aktivierungAnlageBearbeiten.Text = "Bearbeitung Anlage aktivieren";
            this.aktivierungAnlageBearbeiten.UseVisualStyleBackColor = true;
            // 
            // servoSchrittweite
            // 
            this.servoSchrittweite.Location = new System.Drawing.Point(21, 184);
            this.servoSchrittweite.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.servoSchrittweite.Name = "servoSchrittweite";
            this.servoSchrittweite.Size = new System.Drawing.Size(120, 20);
            this.servoSchrittweite.TabIndex = 7;
            this.servoSchrittweite.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelServoSchrittweite
            // 
            this.labelServoSchrittweite.AutoSize = true;
            this.labelServoSchrittweite.Location = new System.Drawing.Point(18, 168);
            this.labelServoSchrittweite.Name = "labelServoSchrittweite";
            this.labelServoSchrittweite.Size = new System.Drawing.Size(131, 13);
            this.labelServoSchrittweite.TabIndex = 8;
            this.labelServoSchrittweite.Text = "Zubehörservo Schrittweite";
            // 
            // frmEinstellung
            // 
            this.AcceptButton = this.buttonSpeichern;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonAbbrechen;
            this.ClientSize = new System.Drawing.Size(627, 384);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonAbbrechen);
            this.Controls.Add(this.buttonSpeichern);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEinstellung";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modellbahnsteuerung Einstellungen";
            this.tabControl1.ResumeLayout(false);
            this.tabPageAnzeige.ResumeLayout(false);
            this.tabPageAnzeige.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entkupplerAbschaltAutoWert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fahrstraßeStartVerzögerung)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.servoSchrittweite)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button buttonSpeichern;
    private System.Windows.Forms.Button buttonAbbrechen;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPageMaster;
    private System.Windows.Forms.TabPage tabPageSlave;
        private System.Windows.Forms.TabPage tabPageAnzeige;
        private System.Windows.Forms.CheckBox rückmeldungAnzeigen;
        private System.Windows.Forms.CheckBox rückmeldungAktiv;
        private System.Windows.Forms.NumericUpDown fahrstraßeStartVerzögerung;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox passwort;
        private System.Windows.Forms.CheckBox aktivierungAnlageBearbeiten;
        private System.Windows.Forms.CheckBox entkupplerAbschaltAutoAktiv;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown entkupplerAbschaltAutoWert;
        private System.Windows.Forms.Label labelServoSchrittweite;
        private System.Windows.Forms.NumericUpDown servoSchrittweite;
    }
}