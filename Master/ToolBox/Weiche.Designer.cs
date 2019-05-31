namespace ModellBahnSteuerung.ToolBox
{
  partial class FrmWeiche
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

    #region Vom Komponenten-Designer generierter Code

    /// <summary> 
    /// Erforderliche Methode für die Designerunterstützung. 
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent()
    {
            this.PanelMenü = new System.Windows.Forms.Panel();
            this.LabelMenü = new System.Windows.Forms.Label();
            this.PictureBoxMenü = new System.Windows.Forms.PictureBox();
            this.PanelEigenschaften = new System.Windows.Forms.Panel();
            this.buttonKoppelung = new System.Windows.Forms.Button();
            this.labelStecker = new System.Windows.Forms.Label();
            this.textBoxStecker = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.buttonSpeichern = new System.Windows.Forms.Button();
            this.buttonAuswaehlen = new System.Windows.Forms.Button();
            this.textBoxBezeichnung = new System.Windows.Forms.TextBox();
            this.labelBez = new System.Windows.Forms.Label();
            this.buttonLöschen = new System.Windows.Forms.Button();
            this.textBoxAusgang = new System.Windows.Forms.TextBox();
            this.textBoxGrundstellung = new System.Windows.Forms.TextBox();
            this.LabelAusgang = new System.Windows.Forms.Label();
            this.labelGrundstell = new System.Windows.Forms.Label();
            this.labelWeichen = new System.Windows.Forms.Label();
            this.textBoxKnoten = new System.Windows.Forms.TextBox();
            this.labelKnoten = new System.Windows.Forms.Label();
            this.textBoxWeiche = new System.Windows.Forms.TextBox();
            this.buttonMC = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.PanelMenü.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxMenü)).BeginInit();
            this.PanelEigenschaften.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelMenü
            // 
            this.PanelMenü.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelMenü.BackColor = System.Drawing.SystemColors.Control;
            this.PanelMenü.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelMenü.Controls.Add(this.LabelMenü);
            this.PanelMenü.Controls.Add(this.PictureBoxMenü);
            this.PanelMenü.Location = new System.Drawing.Point(-1, -1);
            this.PanelMenü.Name = "PanelMenü";
            this.PanelMenü.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.PanelMenü.Size = new System.Drawing.Size(342, 26);
            this.PanelMenü.TabIndex = 0;
            // 
            // LabelMenü
            // 
            this.LabelMenü.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelMenü.Location = new System.Drawing.Point(0, 0);
            this.LabelMenü.Name = "LabelMenü";
            this.LabelMenü.Padding = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.LabelMenü.Size = new System.Drawing.Size(312, 24);
            this.LabelMenü.TabIndex = 1;
            this.LabelMenü.Text = "Weiche";
            this.LabelMenü.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBoxMenü
            // 
            this.PictureBoxMenü.Dock = System.Windows.Forms.DockStyle.Right;
            this.PictureBoxMenü.Image = global::ModellBahnSteuerung.Properties.Resources.Open;
            this.PictureBoxMenü.Location = new System.Drawing.Point(312, 0);
            this.PictureBoxMenü.Margin = new System.Windows.Forms.Padding(0);
            this.PictureBoxMenü.Name = "PictureBoxMenü";
            this.PictureBoxMenü.Size = new System.Drawing.Size(25, 24);
            this.PictureBoxMenü.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBoxMenü.TabIndex = 0;
            this.PictureBoxMenü.TabStop = false;
            this.PictureBoxMenü.Click += new System.EventHandler(this.PictureBoxMenü_Click);
            this.PictureBoxMenü.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBoxMenü_MouseDown);
            this.PictureBoxMenü.MouseEnter += new System.EventHandler(this.PictureBoxMenü_MouseEnter);
            this.PictureBoxMenü.MouseLeave += new System.EventHandler(this.PictureBoxMenü_MouseLeave);
            this.PictureBoxMenü.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBoxMenü_MouseUp);
            // 
            // PanelEigenschaften
            // 
            this.PanelEigenschaften.BackColor = System.Drawing.Color.White;
            this.PanelEigenschaften.Controls.Add(this.button1);
            this.PanelEigenschaften.Controls.Add(this.buttonKoppelung);
            this.PanelEigenschaften.Controls.Add(this.labelStecker);
            this.PanelEigenschaften.Controls.Add(this.textBoxStecker);
            this.PanelEigenschaften.Controls.Add(this.label1);
            this.PanelEigenschaften.Controls.Add(this.textBox2);
            this.PanelEigenschaften.Controls.Add(this.buttonSpeichern);
            this.PanelEigenschaften.Controls.Add(this.buttonAuswaehlen);
            this.PanelEigenschaften.Controls.Add(this.textBoxBezeichnung);
            this.PanelEigenschaften.Controls.Add(this.labelBez);
            this.PanelEigenschaften.Controls.Add(this.buttonLöschen);
            this.PanelEigenschaften.Controls.Add(this.textBoxAusgang);
            this.PanelEigenschaften.Controls.Add(this.textBoxGrundstellung);
            this.PanelEigenschaften.Controls.Add(this.LabelAusgang);
            this.PanelEigenschaften.Controls.Add(this.labelGrundstell);
            this.PanelEigenschaften.Controls.Add(this.labelWeichen);
            this.PanelEigenschaften.Controls.Add(this.textBoxKnoten);
            this.PanelEigenschaften.Controls.Add(this.labelKnoten);
            this.PanelEigenschaften.Controls.Add(this.textBoxWeiche);
            this.PanelEigenschaften.Controls.Add(this.buttonMC);
            this.PanelEigenschaften.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelEigenschaften.Location = new System.Drawing.Point(0, 26);
            this.PanelEigenschaften.Margin = new System.Windows.Forms.Padding(0);
            this.PanelEigenschaften.Name = "PanelEigenschaften";
            this.PanelEigenschaften.Size = new System.Drawing.Size(338, 85);
            this.PanelEigenschaften.TabIndex = 1;
            // 
            // buttonKoppelung
            // 
            this.buttonKoppelung.Location = new System.Drawing.Point(270, 23);
            this.buttonKoppelung.Name = "buttonKoppelung";
            this.buttonKoppelung.Size = new System.Drawing.Size(67, 23);
            this.buttonKoppelung.TabIndex = 18;
            this.buttonKoppelung.Text = "Koppelung";
            this.buttonKoppelung.UseVisualStyleBackColor = true;
            this.buttonKoppelung.Click += new System.EventHandler(this.buttonKoppelung_Click);
            // 
            // labelStecker
            // 
            this.labelStecker.AutoSize = true;
            this.labelStecker.Location = new System.Drawing.Point(167, 50);
            this.labelStecker.Name = "labelStecker";
            this.labelStecker.Size = new System.Drawing.Size(44, 13);
            this.labelStecker.TabIndex = 17;
            this.labelStecker.Text = "Stecker";
            // 
            // textBoxStecker
            // 
            this.textBoxStecker.Location = new System.Drawing.Point(214, 47);
            this.textBoxStecker.Name = "textBoxStecker";
            this.textBoxStecker.Size = new System.Drawing.Size(74, 20);
            this.textBoxStecker.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Anschluß";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(111, 44);
            this.textBox2.MinimumSize = new System.Drawing.Size(50, 20);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(50, 20);
            this.textBox2.TabIndex = 14;
            // 
            // buttonSpeichern
            // 
            this.buttonSpeichern.Location = new System.Drawing.Point(-1, 30);
            this.buttonSpeichern.Name = "buttonSpeichern";
            this.buttonSpeichern.Size = new System.Drawing.Size(62, 23);
            this.buttonSpeichern.TabIndex = 13;
            this.buttonSpeichern.Text = "Überna.";
            this.buttonSpeichern.UseVisualStyleBackColor = true;
            this.buttonSpeichern.Click += new System.EventHandler(this.buttonSpeichern_Click);
            // 
            // buttonAuswaehlen
            // 
            this.buttonAuswaehlen.Location = new System.Drawing.Point(0, 3);
            this.buttonAuswaehlen.Name = "buttonAuswaehlen";
            this.buttonAuswaehlen.Size = new System.Drawing.Size(61, 23);
            this.buttonAuswaehlen.TabIndex = 12;
            this.buttonAuswaehlen.Text = "Laden";
            this.buttonAuswaehlen.UseVisualStyleBackColor = true;
            this.buttonAuswaehlen.Click += new System.EventHandler(this.buttonAuswaehlen_Click);
            // 
            // textBoxBezeichnung
            // 
            this.textBoxBezeichnung.Location = new System.Drawing.Point(214, 24);
            this.textBoxBezeichnung.MaximumSize = new System.Drawing.Size(30, 20);
            this.textBoxBezeichnung.MinimumSize = new System.Drawing.Size(50, 20);
            this.textBoxBezeichnung.Name = "textBoxBezeichnung";
            this.textBoxBezeichnung.Size = new System.Drawing.Size(50, 20);
            this.textBoxBezeichnung.TabIndex = 4;
            // 
            // labelBez
            // 
            this.labelBez.AutoSize = true;
            this.labelBez.Location = new System.Drawing.Point(163, 28);
            this.labelBez.Name = "labelBez";
            this.labelBez.Size = new System.Drawing.Size(28, 13);
            this.labelBez.TabIndex = 11;
            this.labelBez.Text = "Bez.";
            // 
            // buttonLöschen
            // 
            this.buttonLöschen.Location = new System.Drawing.Point(-1, 59);
            this.buttonLöschen.Name = "buttonLöschen";
            this.buttonLöschen.Size = new System.Drawing.Size(62, 23);
            this.buttonLöschen.TabIndex = 5;
            this.buttonLöschen.Text = "Löschen";
            this.buttonLöschen.UseVisualStyleBackColor = true;
            // 
            // textBoxAusgang
            // 
            this.textBoxAusgang.Location = new System.Drawing.Point(214, 5);
            this.textBoxAusgang.MaximumSize = new System.Drawing.Size(30, 20);
            this.textBoxAusgang.MinimumSize = new System.Drawing.Size(50, 20);
            this.textBoxAusgang.Name = "textBoxAusgang";
            this.textBoxAusgang.Size = new System.Drawing.Size(50, 20);
            this.textBoxAusgang.TabIndex = 3;
            // 
            // textBoxGrundstellung
            // 
            this.textBoxGrundstellung.Location = new System.Drawing.Point(111, 63);
            this.textBoxGrundstellung.MinimumSize = new System.Drawing.Size(50, 20);
            this.textBoxGrundstellung.Name = "textBoxGrundstellung";
            this.textBoxGrundstellung.ReadOnly = true;
            this.textBoxGrundstellung.Size = new System.Drawing.Size(50, 20);
            this.textBoxGrundstellung.TabIndex = 2;
            this.textBoxGrundstellung.DoubleClick += new System.EventHandler(this.textBoxGrundstellung_DoubleClick);
            // 
            // LabelAusgang
            // 
            this.LabelAusgang.AutoSize = true;
            this.LabelAusgang.Location = new System.Drawing.Point(163, 9);
            this.LabelAusgang.Name = "LabelAusgang";
            this.LabelAusgang.Size = new System.Drawing.Size(49, 13);
            this.LabelAusgang.TabIndex = 10;
            this.LabelAusgang.Text = "Ausgang";
            // 
            // labelGrundstell
            // 
            this.labelGrundstell.AutoSize = true;
            this.labelGrundstell.Location = new System.Drawing.Point(62, 66);
            this.labelGrundstell.Name = "labelGrundstell";
            this.labelGrundstell.Size = new System.Drawing.Size(47, 13);
            this.labelGrundstell.TabIndex = 9;
            this.labelGrundstell.Text = "Grundst.";
            // 
            // labelWeichen
            // 
            this.labelWeichen.AutoSize = true;
            this.labelWeichen.Location = new System.Drawing.Point(62, 9);
            this.labelWeichen.Name = "labelWeichen";
            this.labelWeichen.Size = new System.Drawing.Size(44, 13);
            this.labelWeichen.TabIndex = 7;
            this.labelWeichen.Text = "Weiche";
            // 
            // textBoxKnoten
            // 
            this.textBoxKnoten.Location = new System.Drawing.Point(111, 24);
            this.textBoxKnoten.MinimumSize = new System.Drawing.Size(50, 20);
            this.textBoxKnoten.Name = "textBoxKnoten";
            this.textBoxKnoten.ReadOnly = true;
            this.textBoxKnoten.Size = new System.Drawing.Size(50, 20);
            this.textBoxKnoten.TabIndex = 1;
            // 
            // labelKnoten
            // 
            this.labelKnoten.AutoSize = true;
            this.labelKnoten.Location = new System.Drawing.Point(62, 28);
            this.labelKnoten.Name = "labelKnoten";
            this.labelKnoten.Size = new System.Drawing.Size(41, 13);
            this.labelKnoten.TabIndex = 8;
            this.labelKnoten.Text = "Knoten";
            // 
            // textBoxWeiche
            // 
            this.textBoxWeiche.Location = new System.Drawing.Point(112, 3);
            this.textBoxWeiche.MinimumSize = new System.Drawing.Size(50, 20);
            this.textBoxWeiche.Name = "textBoxWeiche";
            this.textBoxWeiche.ShortcutsEnabled = false;
            this.textBoxWeiche.Size = new System.Drawing.Size(50, 20);
            this.textBoxWeiche.TabIndex = 0;
            // 
            // buttonMC
            // 
            this.buttonMC.Location = new System.Drawing.Point(270, 0);
            this.buttonMC.Name = "buttonMC";
            this.buttonMC.Size = new System.Drawing.Size(58, 23);
            this.buttonMC.TabIndex = 6;
            this.buttonMC.Text = "MC";
            this.buttonMC.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(294, 52);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(58, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmWeiche
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.PanelMenü);
            this.Controls.Add(this.PanelEigenschaften);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(268, 26);
            this.Name = "FrmWeiche";
            this.Size = new System.Drawing.Size(338, 111);
            this.PanelMenü.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxMenü)).EndInit();
            this.PanelEigenschaften.ResumeLayout(false);
            this.PanelEigenschaften.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel PanelMenü;
    private System.Windows.Forms.Label LabelMenü;
    private System.Windows.Forms.PictureBox PictureBoxMenü;
    private System.Windows.Forms.Panel PanelEigenschaften;
    private System.Windows.Forms.Label labelWeichen;
    private System.Windows.Forms.TextBox textBoxKnoten;
    private System.Windows.Forms.Label labelKnoten;
    private System.Windows.Forms.TextBox textBoxWeiche;
    private System.Windows.Forms.Button buttonMC;
    private System.Windows.Forms.TextBox textBoxAusgang;
    private System.Windows.Forms.TextBox textBoxGrundstellung;
    private System.Windows.Forms.Label LabelAusgang;
    private System.Windows.Forms.Label labelGrundstell;
    private System.Windows.Forms.Button buttonLöschen;
    private System.Windows.Forms.TextBox textBoxBezeichnung;
    private System.Windows.Forms.Label labelBez;
        private System.Windows.Forms.Button buttonSpeichern;
        private System.Windows.Forms.Button buttonAuswaehlen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label labelStecker;
        private System.Windows.Forms.TextBox textBoxStecker;
        private System.Windows.Forms.Button buttonKoppelung;
        private System.Windows.Forms.Button button1;
    }
}
