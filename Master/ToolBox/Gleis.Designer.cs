namespace ModellBahnSteuerung.ToolBox
{
  partial class FrmGleis
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
            this.components = new System.ComponentModel.Container();
            this.PanelMenü = new System.Windows.Forms.Panel();
            this.LabelMenü = new System.Windows.Forms.Label();
            this.PictureBoxMenü = new System.Windows.Forms.PictureBox();
            this.PanelEigenschaften = new System.Windows.Forms.Panel();
            this.labelStecker = new System.Windows.Forms.Label();
            this.textBoxStecker = new System.Windows.Forms.TextBox();
            this.buttonSpeichern = new System.Windows.Forms.Button();
            this.ButtonLaden = new System.Windows.Forms.Button();
            this.Bezeichnung_Label = new System.Windows.Forms.Label();
            this.TextBoxBezeichnung = new System.Windows.Forms.TextBox();
            this.Regler_Label = new System.Windows.Forms.Label();
            this.textBoxRegler = new System.Windows.Forms.TextBox();
            this.textBoxAusgang = new System.Windows.Forms.TextBox();
            this.Adr_Ausgang_label = new System.Windows.Forms.Label();
            this.textBoxRM = new System.Windows.Forms.TextBox();
            this.Adr_Eingang_Label = new System.Windows.Forms.Label();
            this.buttonTauschen = new System.Windows.Forms.Button();
            this.buttonEndKnoten = new System.Windows.Forms.Button();
            this.buttonStartKnoten = new System.Windows.Forms.Button();
            this.textBoxEndKnoten = new System.Windows.Forms.TextBox();
            this.textBoxStartKnoten = new System.Windows.Forms.TextBox();
            this.labelEndknoten = new System.Windows.Forms.Label();
            this.labelStartknoten = new System.Windows.Forms.Label();
            this.labelGleis = new System.Windows.Forms.Label();
            this.textBoxGleis = new System.Windows.Forms.TextBox();
            this.buttonMC = new System.Windows.Forms.Button();
            this.buttonLöschen = new System.Windows.Forms.Button();
            this.toolTipGleis = new System.Windows.Forms.ToolTip(this.components);
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
            this.PanelMenü.Size = new System.Drawing.Size(396, 26);
            this.PanelMenü.TabIndex = 0;
            // 
            // LabelMenü
            // 
            this.LabelMenü.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelMenü.Location = new System.Drawing.Point(0, 0);
            this.LabelMenü.Name = "LabelMenü";
            this.LabelMenü.Padding = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.LabelMenü.Size = new System.Drawing.Size(366, 24);
            this.LabelMenü.TabIndex = 1;
            this.LabelMenü.Text = "Gleis";
            this.LabelMenü.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBoxMenü
            // 
            this.PictureBoxMenü.Dock = System.Windows.Forms.DockStyle.Right;
            this.PictureBoxMenü.Image = global::ModellBahnSteuerung.Properties.Resources.Open;
            this.PictureBoxMenü.Location = new System.Drawing.Point(366, 0);
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
            this.PanelEigenschaften.Controls.Add(this.labelStecker);
            this.PanelEigenschaften.Controls.Add(this.textBoxStecker);
            this.PanelEigenschaften.Controls.Add(this.buttonSpeichern);
            this.PanelEigenschaften.Controls.Add(this.ButtonLaden);
            this.PanelEigenschaften.Controls.Add(this.Bezeichnung_Label);
            this.PanelEigenschaften.Controls.Add(this.TextBoxBezeichnung);
            this.PanelEigenschaften.Controls.Add(this.Regler_Label);
            this.PanelEigenschaften.Controls.Add(this.textBoxRegler);
            this.PanelEigenschaften.Controls.Add(this.textBoxAusgang);
            this.PanelEigenschaften.Controls.Add(this.Adr_Ausgang_label);
            this.PanelEigenschaften.Controls.Add(this.textBoxRM);
            this.PanelEigenschaften.Controls.Add(this.Adr_Eingang_Label);
            this.PanelEigenschaften.Controls.Add(this.buttonTauschen);
            this.PanelEigenschaften.Controls.Add(this.buttonEndKnoten);
            this.PanelEigenschaften.Controls.Add(this.buttonStartKnoten);
            this.PanelEigenschaften.Controls.Add(this.textBoxEndKnoten);
            this.PanelEigenschaften.Controls.Add(this.textBoxStartKnoten);
            this.PanelEigenschaften.Controls.Add(this.labelEndknoten);
            this.PanelEigenschaften.Controls.Add(this.labelStartknoten);
            this.PanelEigenschaften.Controls.Add(this.labelGleis);
            this.PanelEigenschaften.Controls.Add(this.textBoxGleis);
            this.PanelEigenschaften.Controls.Add(this.buttonMC);
            this.PanelEigenschaften.Controls.Add(this.buttonLöschen);
            this.PanelEigenschaften.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelEigenschaften.Location = new System.Drawing.Point(0, 27);
            this.PanelEigenschaften.Margin = new System.Windows.Forms.Padding(0);
            this.PanelEigenschaften.Name = "PanelEigenschaften";
            this.PanelEigenschaften.Size = new System.Drawing.Size(392, 85);
            this.PanelEigenschaften.TabIndex = 1;
            // 
            // labelStecker
            // 
            this.labelStecker.AutoSize = true;
            this.labelStecker.Location = new System.Drawing.Point(209, 65);
            this.labelStecker.Name = "labelStecker";
            this.labelStecker.Size = new System.Drawing.Size(44, 13);
            this.labelStecker.TabIndex = 22;
            this.labelStecker.Text = "Stecker";
            // 
            // textBoxStecker
            // 
            this.textBoxStecker.Location = new System.Drawing.Point(251, 63);
            this.textBoxStecker.Name = "textBoxStecker";
            this.textBoxStecker.Size = new System.Drawing.Size(97, 20);
            this.textBoxStecker.TabIndex = 21;
            // 
            // buttonSpeichern
            // 
            this.buttonSpeichern.Location = new System.Drawing.Point(4, 31);
            this.buttonSpeichern.Name = "buttonSpeichern";
            this.buttonSpeichern.Size = new System.Drawing.Size(57, 23);
            this.buttonSpeichern.TabIndex = 20;
            this.buttonSpeichern.Text = "Überna.";
            this.buttonSpeichern.UseVisualStyleBackColor = true;
            this.buttonSpeichern.Click += new System.EventHandler(this.buttonSpeichern_Click);
            // 
            // ButtonLaden
            // 
            this.ButtonLaden.Location = new System.Drawing.Point(4, 3);
            this.ButtonLaden.Name = "ButtonLaden";
            this.ButtonLaden.Size = new System.Drawing.Size(57, 23);
            this.ButtonLaden.TabIndex = 19;
            this.ButtonLaden.Text = "Laden";
            this.ButtonLaden.UseVisualStyleBackColor = true;
            this.ButtonLaden.Click += new System.EventHandler(this.ButtonLaden_Click);
            // 
            // Bezeichnung_Label
            // 
            this.Bezeichnung_Label.AutoSize = true;
            this.Bezeichnung_Label.Location = new System.Drawing.Point(225, 47);
            this.Bezeichnung_Label.Name = "Bezeichnung_Label";
            this.Bezeichnung_Label.Size = new System.Drawing.Size(69, 13);
            this.Bezeichnung_Label.TabIndex = 18;
            this.Bezeichnung_Label.Text = "Bezeichnung";
            // 
            // TextBoxBezeichnung
            // 
            this.TextBoxBezeichnung.Location = new System.Drawing.Point(298, 43);
            this.TextBoxBezeichnung.MinimumSize = new System.Drawing.Size(50, 20);
            this.TextBoxBezeichnung.Name = "TextBoxBezeichnung";
            this.TextBoxBezeichnung.Size = new System.Drawing.Size(50, 20);
            this.TextBoxBezeichnung.TabIndex = 6;
            // 
            // Regler_Label
            // 
            this.Regler_Label.AutoSize = true;
            this.Regler_Label.Location = new System.Drawing.Point(225, 28);
            this.Regler_Label.Name = "Regler_Label";
            this.Regler_Label.Size = new System.Drawing.Size(38, 13);
            this.Regler_Label.TabIndex = 17;
            this.Regler_Label.Text = "Regler";
            // 
            // textBoxRegler
            // 
            this.textBoxRegler.Location = new System.Drawing.Point(298, 24);
            this.textBoxRegler.MinimumSize = new System.Drawing.Size(50, 20);
            this.textBoxRegler.Name = "textBoxRegler";
            this.textBoxRegler.Size = new System.Drawing.Size(50, 20);
            this.textBoxRegler.TabIndex = 5;
            // 
            // textBoxAusgang
            // 
            this.textBoxAusgang.Location = new System.Drawing.Point(298, 5);
            this.textBoxAusgang.MinimumSize = new System.Drawing.Size(50, 20);
            this.textBoxAusgang.Name = "textBoxAusgang";
            this.textBoxAusgang.Size = new System.Drawing.Size(50, 20);
            this.textBoxAusgang.TabIndex = 4;
            this.textBoxAusgang.TextChanged += new System.EventHandler(this.TextBoxAusgang_TextChanged);
            // 
            // Adr_Ausgang_label
            // 
            this.Adr_Ausgang_label.AutoSize = true;
            this.Adr_Ausgang_label.Location = new System.Drawing.Point(225, 9);
            this.Adr_Ausgang_label.Name = "Adr_Ausgang_label";
            this.Adr_Ausgang_label.Size = new System.Drawing.Size(71, 13);
            this.Adr_Ausgang_label.TabIndex = 16;
            this.Adr_Ausgang_label.Text = "Adr. Ausgang";
            // 
            // textBoxRM
            // 
            this.textBoxRM.Location = new System.Drawing.Point(173, 61);
            this.textBoxRM.MinimumSize = new System.Drawing.Size(40, 20);
            this.textBoxRM.Name = "textBoxRM";
            this.textBoxRM.Size = new System.Drawing.Size(40, 20);
            this.textBoxRM.TabIndex = 3;
            // 
            // Adr_Eingang_Label
            // 
            this.Adr_Eingang_Label.AutoSize = true;
            this.Adr_Eingang_Label.Location = new System.Drawing.Point(98, 67);
            this.Adr_Eingang_Label.Name = "Adr_Eingang_Label";
            this.Adr_Eingang_Label.Size = new System.Drawing.Size(73, 13);
            this.Adr_Eingang_Label.TabIndex = 15;
            this.Adr_Eingang_Label.Text = "Rückmeldung";
            // 
            // buttonTauschen
            // 
            this.buttonTauschen.Location = new System.Drawing.Point(64, 26);
            this.buttonTauschen.Name = "buttonTauschen";
            this.buttonTauschen.Size = new System.Drawing.Size(15, 34);
            this.buttonTauschen.TabIndex = 9;
            this.toolTipGleis.SetToolTip(this.buttonTauschen, "Knoten tauschen.");
            this.buttonTauschen.UseVisualStyleBackColor = true;
            // 
            // buttonEndKnoten
            // 
            this.buttonEndKnoten.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEndKnoten.Location = new System.Drawing.Point(82, 45);
            this.buttonEndKnoten.Name = "buttonEndKnoten";
            this.buttonEndKnoten.Size = new System.Drawing.Size(15, 15);
            this.buttonEndKnoten.TabIndex = 8;
            this.toolTipGleis.SetToolTip(this.buttonEndKnoten, "Endknoten");
            this.buttonEndKnoten.UseVisualStyleBackColor = true;
            // 
            // buttonStartKnoten
            // 
            this.buttonStartKnoten.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStartKnoten.Location = new System.Drawing.Point(82, 26);
            this.buttonStartKnoten.Name = "buttonStartKnoten";
            this.buttonStartKnoten.Size = new System.Drawing.Size(15, 15);
            this.buttonStartKnoten.TabIndex = 7;
            this.toolTipGleis.SetToolTip(this.buttonStartKnoten, "Startknoten");
            this.buttonStartKnoten.UseVisualStyleBackColor = true;
            // 
            // textBoxEndKnoten
            // 
            this.textBoxEndKnoten.Location = new System.Drawing.Point(173, 42);
            this.textBoxEndKnoten.MinimumSize = new System.Drawing.Size(50, 20);
            this.textBoxEndKnoten.Name = "textBoxEndKnoten";
            this.textBoxEndKnoten.ReadOnly = true;
            this.textBoxEndKnoten.Size = new System.Drawing.Size(50, 20);
            this.textBoxEndKnoten.TabIndex = 2;
            // 
            // textBoxStartKnoten
            // 
            this.textBoxStartKnoten.Location = new System.Drawing.Point(173, 23);
            this.textBoxStartKnoten.MinimumSize = new System.Drawing.Size(50, 20);
            this.textBoxStartKnoten.Name = "textBoxStartKnoten";
            this.textBoxStartKnoten.ReadOnly = true;
            this.textBoxStartKnoten.Size = new System.Drawing.Size(50, 20);
            this.textBoxStartKnoten.TabIndex = 1;
            // 
            // labelEndknoten
            // 
            this.labelEndknoten.AutoSize = true;
            this.labelEndknoten.Location = new System.Drawing.Point(98, 46);
            this.labelEndknoten.Name = "labelEndknoten";
            this.labelEndknoten.Size = new System.Drawing.Size(59, 13);
            this.labelEndknoten.TabIndex = 14;
            this.labelEndknoten.Text = "Endknoten";
            // 
            // labelStartknoten
            // 
            this.labelStartknoten.AutoSize = true;
            this.labelStartknoten.Location = new System.Drawing.Point(98, 27);
            this.labelStartknoten.Name = "labelStartknoten";
            this.labelStartknoten.Size = new System.Drawing.Size(62, 13);
            this.labelStartknoten.TabIndex = 13;
            this.labelStartknoten.Text = "Startknoten";
            // 
            // labelGleis
            // 
            this.labelGleis.AutoSize = true;
            this.labelGleis.Location = new System.Drawing.Point(98, 8);
            this.labelGleis.Name = "labelGleis";
            this.labelGleis.Size = new System.Drawing.Size(47, 13);
            this.labelGleis.TabIndex = 12;
            this.labelGleis.Text = "Gleis-Nr.";
            // 
            // textBoxGleis
            // 
            this.textBoxGleis.Location = new System.Drawing.Point(173, 4);
            this.textBoxGleis.MinimumSize = new System.Drawing.Size(50, 20);
            this.textBoxGleis.Name = "textBoxGleis";
            this.textBoxGleis.Size = new System.Drawing.Size(50, 20);
            this.textBoxGleis.TabIndex = 0;
            this.textBoxGleis.TextChanged += new System.EventHandler(this.textBoxGleis_TextChanged);
            // 
            // buttonMC
            // 
            this.buttonMC.Location = new System.Drawing.Point(61, 60);
            this.buttonMC.Name = "buttonMC";
            this.buttonMC.Size = new System.Drawing.Size(36, 23);
            this.buttonMC.TabIndex = 10;
            this.buttonMC.Text = "MC";
            this.buttonMC.UseVisualStyleBackColor = true;
            // 
            // buttonLöschen
            // 
            this.buttonLöschen.Location = new System.Drawing.Point(3, 60);
            this.buttonLöschen.Name = "buttonLöschen";
            this.buttonLöschen.Size = new System.Drawing.Size(58, 23);
            this.buttonLöschen.TabIndex = 11;
            this.buttonLöschen.Text = "Löschen";
            this.buttonLöschen.UseVisualStyleBackColor = true;
            // 
            // toolTipGleis
            // 
            this.toolTipGleis.ShowAlways = true;
            this.toolTipGleis.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipGleis.ToolTipTitle = "Gleis";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(354, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 23);
            this.button1.TabIndex = 23;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmGleis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.PanelMenü);
            this.Controls.Add(this.PanelEigenschaften);
            this.MinimumSize = new System.Drawing.Size(352, 112);
            this.Name = "FrmGleis";
            this.Size = new System.Drawing.Size(392, 112);
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
    private System.Windows.Forms.Button buttonTauschen;
    private System.Windows.Forms.Button buttonEndKnoten;
    private System.Windows.Forms.Button buttonStartKnoten;
    private System.Windows.Forms.TextBox textBoxEndKnoten;
    private System.Windows.Forms.TextBox textBoxStartKnoten;
    private System.Windows.Forms.Label labelEndknoten;
    private System.Windows.Forms.Label labelStartknoten;
    private System.Windows.Forms.Label labelGleis;
    private System.Windows.Forms.TextBox textBoxGleis;
    private System.Windows.Forms.Button buttonMC;
    private System.Windows.Forms.Button buttonLöschen;
    private System.Windows.Forms.Label Bezeichnung_Label;
    private System.Windows.Forms.TextBox TextBoxBezeichnung;
    private System.Windows.Forms.Label Regler_Label;
    private System.Windows.Forms.TextBox textBoxRegler;
    private System.Windows.Forms.TextBox textBoxAusgang;
    private System.Windows.Forms.Label Adr_Ausgang_label;
    private System.Windows.Forms.TextBox textBoxRM;
    private System.Windows.Forms.Label Adr_Eingang_Label;
    private System.Windows.Forms.ToolTip toolTipGleis;
        private System.Windows.Forms.Button ButtonLaden;
        private System.Windows.Forms.Button buttonSpeichern;
        private System.Windows.Forms.Label labelStecker;
        private System.Windows.Forms.TextBox textBoxStecker;
        private System.Windows.Forms.Button button1;
    }
}
