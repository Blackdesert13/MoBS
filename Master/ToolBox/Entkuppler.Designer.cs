namespace ModellBahnSteuerung.ToolBox
{
  partial class FrmEntkuppler
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
            this.textBoxStecker = new System.Windows.Forms.TextBox();
            this.labelBezeichnung = new System.Windows.Forms.Label();
            this.textBoxBezeichnung = new System.Windows.Forms.TextBox();
            this.buttonLaden = new System.Windows.Forms.Button();
            this.buttonLöschen = new System.Windows.Forms.Button();
            this.textBoxLageY = new System.Windows.Forms.TextBox();
            this.textBoxLageX = new System.Windows.Forms.TextBox();
            this.LabelLageY = new System.Windows.Forms.Label();
            this.labelLageX = new System.Windows.Forms.Label();
            this.labelEntkuppler = new System.Windows.Forms.Label();
            this.textBoxAdresse = new System.Windows.Forms.TextBox();
            this.labelAdresse = new System.Windows.Forms.Label();
            this.textBoxGleis = new System.Windows.Forms.TextBox();
            this.labelGleis = new System.Windows.Forms.Label();
            this.textBoxEntkuppler = new System.Windows.Forms.TextBox();
            this.buttonMC = new System.Windows.Forms.Button();
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
            this.PanelMenü.Size = new System.Drawing.Size(367, 26);
            this.PanelMenü.TabIndex = 0;
            // 
            // LabelMenü
            // 
            this.LabelMenü.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelMenü.Location = new System.Drawing.Point(0, 0);
            this.LabelMenü.Name = "LabelMenü";
            this.LabelMenü.Padding = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.LabelMenü.Size = new System.Drawing.Size(337, 24);
            this.LabelMenü.TabIndex = 1;
            this.LabelMenü.Text = "Entkuppler";
            this.LabelMenü.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBoxMenü
            // 
            this.PictureBoxMenü.Dock = System.Windows.Forms.DockStyle.Right;
            this.PictureBoxMenü.Image = global::ModellBahnSteuerung.Properties.Resources.Open;
            this.PictureBoxMenü.Location = new System.Drawing.Point(337, 0);
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
            this.PanelEigenschaften.Controls.Add(this.textBoxStecker);
            this.PanelEigenschaften.Controls.Add(this.labelBezeichnung);
            this.PanelEigenschaften.Controls.Add(this.textBoxBezeichnung);
            this.PanelEigenschaften.Controls.Add(this.buttonLaden);
            this.PanelEigenschaften.Controls.Add(this.buttonLöschen);
            this.PanelEigenschaften.Controls.Add(this.textBoxLageY);
            this.PanelEigenschaften.Controls.Add(this.textBoxLageX);
            this.PanelEigenschaften.Controls.Add(this.LabelLageY);
            this.PanelEigenschaften.Controls.Add(this.labelLageX);
            this.PanelEigenschaften.Controls.Add(this.labelEntkuppler);
            this.PanelEigenschaften.Controls.Add(this.textBoxAdresse);
            this.PanelEigenschaften.Controls.Add(this.labelAdresse);
            this.PanelEigenschaften.Controls.Add(this.textBoxGleis);
            this.PanelEigenschaften.Controls.Add(this.labelGleis);
            this.PanelEigenschaften.Controls.Add(this.textBoxEntkuppler);
            this.PanelEigenschaften.Controls.Add(this.buttonMC);
            this.PanelEigenschaften.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelEigenschaften.Location = new System.Drawing.Point(0, 26);
            this.PanelEigenschaften.Margin = new System.Windows.Forms.Padding(0);
            this.PanelEigenschaften.Name = "PanelEigenschaften";
            this.PanelEigenschaften.Size = new System.Drawing.Size(363, 67);
            this.PanelEigenschaften.TabIndex = 1;
            // 
            // textBoxStecker
            // 
            this.textBoxStecker.Location = new System.Drawing.Point(131, 45);
            this.textBoxStecker.Name = "textBoxStecker";
            this.textBoxStecker.Size = new System.Drawing.Size(41, 20);
            this.textBoxStecker.TabIndex = 15;
            // 
            // labelBezeichnung
            // 
            this.labelBezeichnung.AutoSize = true;
            this.labelBezeichnung.Location = new System.Drawing.Point(176, 47);
            this.labelBezeichnung.Name = "labelBezeichnung";
            this.labelBezeichnung.Size = new System.Drawing.Size(28, 13);
            this.labelBezeichnung.TabIndex = 14;
            this.labelBezeichnung.Text = "Bez.";
            // 
            // textBoxBezeichnung
            // 
            this.textBoxBezeichnung.Location = new System.Drawing.Point(217, 45);
            this.textBoxBezeichnung.Name = "textBoxBezeichnung";
            this.textBoxBezeichnung.Size = new System.Drawing.Size(50, 20);
            this.textBoxBezeichnung.TabIndex = 13;
            // 
            // buttonLaden
            // 
            this.buttonLaden.Location = new System.Drawing.Point(3, 3);
            this.buttonLaden.Name = "buttonLaden";
            this.buttonLaden.Size = new System.Drawing.Size(58, 22);
            this.buttonLaden.TabIndex = 12;
            this.buttonLaden.Text = "laden";
            this.buttonLaden.UseVisualStyleBackColor = true;
            this.buttonLaden.Click += new System.EventHandler(this.buttonLaden_Click);
            // 
            // buttonLöschen
            // 
            this.buttonLöschen.Location = new System.Drawing.Point(3, 41);
            this.buttonLöschen.Name = "buttonLöschen";
            this.buttonLöschen.Size = new System.Drawing.Size(58, 23);
            this.buttonLöschen.TabIndex = 5;
            this.buttonLöschen.Text = "Löschen";
            this.buttonLöschen.UseVisualStyleBackColor = true;
            // 
            // textBoxLageY
            // 
            this.textBoxLageY.Location = new System.Drawing.Point(217, 24);
            this.textBoxLageY.MaximumSize = new System.Drawing.Size(30, 20);
            this.textBoxLageY.MinimumSize = new System.Drawing.Size(50, 20);
            this.textBoxLageY.Name = "textBoxLageY";
            this.textBoxLageY.ReadOnly = true;
            this.textBoxLageY.Size = new System.Drawing.Size(50, 20);
            this.textBoxLageY.TabIndex = 4;
            // 
            // textBoxLageX
            // 
            this.textBoxLageX.Location = new System.Drawing.Point(217, 5);
            this.textBoxLageX.Name = "textBoxLageX";
            this.textBoxLageX.ReadOnly = true;
            this.textBoxLageX.Size = new System.Drawing.Size(50, 20);
            this.textBoxLageX.TabIndex = 3;
            // 
            // LabelLageY
            // 
            this.LabelLageY.AutoSize = true;
            this.LabelLageY.Location = new System.Drawing.Point(174, 28);
            this.LabelLageY.Name = "LabelLageY";
            this.LabelLageY.Size = new System.Drawing.Size(41, 13);
            this.LabelLageY.TabIndex = 11;
            this.LabelLageY.Text = "Lage Y";
            // 
            // labelLageX
            // 
            this.labelLageX.AutoSize = true;
            this.labelLageX.Location = new System.Drawing.Point(174, 9);
            this.labelLageX.Name = "labelLageX";
            this.labelLageX.Size = new System.Drawing.Size(41, 13);
            this.labelLageX.TabIndex = 10;
            this.labelLageX.Text = "Lage X";
            // 
            // labelEntkuppler
            // 
            this.labelEntkuppler.AutoSize = true;
            this.labelEntkuppler.Location = new System.Drawing.Point(62, 9);
            this.labelEntkuppler.Name = "labelEntkuppler";
            this.labelEntkuppler.Size = new System.Drawing.Size(58, 13);
            this.labelEntkuppler.TabIndex = 7;
            this.labelEntkuppler.Text = "Entkuppler";
            // 
            // textBoxAdresse
            // 
            this.textBoxAdresse.Location = new System.Drawing.Point(310, 43);
            this.textBoxAdresse.Name = "textBoxAdresse";
            this.textBoxAdresse.Size = new System.Drawing.Size(50, 20);
            this.textBoxAdresse.TabIndex = 2;
            // 
            // labelAdresse
            // 
            this.labelAdresse.AutoSize = true;
            this.labelAdresse.Location = new System.Drawing.Point(62, 47);
            this.labelAdresse.Name = "labelAdresse";
            this.labelAdresse.Size = new System.Drawing.Size(45, 13);
            this.labelAdresse.TabIndex = 9;
            this.labelAdresse.Text = "Adresse";
            // 
            // textBoxGleis
            // 
            this.textBoxGleis.Location = new System.Drawing.Point(122, 24);
            this.textBoxGleis.Name = "textBoxGleis";
            this.textBoxGleis.ReadOnly = true;
            this.textBoxGleis.Size = new System.Drawing.Size(50, 20);
            this.textBoxGleis.TabIndex = 1;
            // 
            // labelGleis
            // 
            this.labelGleis.AutoSize = true;
            this.labelGleis.Location = new System.Drawing.Point(62, 28);
            this.labelGleis.Name = "labelGleis";
            this.labelGleis.Size = new System.Drawing.Size(30, 13);
            this.labelGleis.TabIndex = 8;
            this.labelGleis.Text = "Gleis";
            // 
            // textBoxEntkuppler
            // 
            this.textBoxEntkuppler.Location = new System.Drawing.Point(122, 5);
            this.textBoxEntkuppler.Name = "textBoxEntkuppler";
            this.textBoxEntkuppler.Size = new System.Drawing.Size(50, 20);
            this.textBoxEntkuppler.TabIndex = 0;
            // 
            // buttonMC
            // 
            this.buttonMC.Location = new System.Drawing.Point(7, 22);
            this.buttonMC.Name = "buttonMC";
            this.buttonMC.Size = new System.Drawing.Size(38, 23);
            this.buttonMC.TabIndex = 6;
            this.buttonMC.Text = "MC";
            this.buttonMC.UseVisualStyleBackColor = true;
            // 
            // FrmEntkuppler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.PanelMenü);
            this.Controls.Add(this.PanelEigenschaften);
            this.MinimumSize = new System.Drawing.Size(271, 26);
            this.Name = "FrmEntkuppler";
            this.Size = new System.Drawing.Size(363, 93);
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
    private System.Windows.Forms.Button buttonMC;
    private System.Windows.Forms.Label labelEntkuppler;
    private System.Windows.Forms.TextBox textBoxAdresse;
    private System.Windows.Forms.Label labelAdresse;
    private System.Windows.Forms.TextBox textBoxGleis;
    private System.Windows.Forms.Label labelGleis;
    private System.Windows.Forms.TextBox textBoxEntkuppler;
    private System.Windows.Forms.TextBox textBoxLageY;
    private System.Windows.Forms.TextBox textBoxLageX;
    private System.Windows.Forms.Label LabelLageY;
    private System.Windows.Forms.Label labelLageX;
    private System.Windows.Forms.Button buttonLöschen;
        private System.Windows.Forms.Button buttonLaden;
        private System.Windows.Forms.TextBox textBoxBezeichnung;
        private System.Windows.Forms.Label labelBezeichnung;
        private System.Windows.Forms.TextBox textBoxStecker;
    }
}
