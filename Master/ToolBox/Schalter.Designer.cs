namespace ModellBahnSteuerung.ToolBox
{
  partial class FrmSchalter
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
            this.buttonLöschen = new System.Windows.Forms.Button();
            this.Ausgang_TB = new System.Windows.Forms.TextBox();
            this.Ausgang_label = new System.Windows.Forms.Label();
            this.labelSchalter = new System.Windows.Forms.Label();
            this.textBoxLageY = new System.Windows.Forms.TextBox();
            this.textBoxLageX = new System.Windows.Forms.TextBox();
            this.textBoxGleis = new System.Windows.Forms.TextBox();
            this.LabelLageY = new System.Windows.Forms.Label();
            this.labelLageX = new System.Windows.Forms.Label();
            this.labelGleis = new System.Windows.Forms.Label();
            this.textBoxSchalter = new System.Windows.Forms.TextBox();
            this.buttonMC = new System.Windows.Forms.Button();
            this.buttonLaden = new System.Windows.Forms.Button();
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
            this.PanelMenü.Size = new System.Drawing.Size(266, 26);
            this.PanelMenü.TabIndex = 0;
            // 
            // LabelMenü
            // 
            this.LabelMenü.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelMenü.Location = new System.Drawing.Point(0, 0);
            this.LabelMenü.Name = "LabelMenü";
            this.LabelMenü.Padding = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.LabelMenü.Size = new System.Drawing.Size(236, 24);
            this.LabelMenü.TabIndex = 1;
            this.LabelMenü.Text = "Schalter";
            this.LabelMenü.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBoxMenü
            // 
            this.PictureBoxMenü.Dock = System.Windows.Forms.DockStyle.Right;
            this.PictureBoxMenü.Image = global::ModellBahnSteuerung.Properties.Resources.Open;
            this.PictureBoxMenü.Location = new System.Drawing.Point(236, 0);
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
            this.PanelEigenschaften.Controls.Add(this.buttonLaden);
            this.PanelEigenschaften.Controls.Add(this.buttonKoppelung);
            this.PanelEigenschaften.Controls.Add(this.buttonLöschen);
            this.PanelEigenschaften.Controls.Add(this.Ausgang_TB);
            this.PanelEigenschaften.Controls.Add(this.Ausgang_label);
            this.PanelEigenschaften.Controls.Add(this.labelSchalter);
            this.PanelEigenschaften.Controls.Add(this.textBoxLageY);
            this.PanelEigenschaften.Controls.Add(this.textBoxLageX);
            this.PanelEigenschaften.Controls.Add(this.textBoxGleis);
            this.PanelEigenschaften.Controls.Add(this.LabelLageY);
            this.PanelEigenschaften.Controls.Add(this.labelLageX);
            this.PanelEigenschaften.Controls.Add(this.labelGleis);
            this.PanelEigenschaften.Controls.Add(this.textBoxSchalter);
            this.PanelEigenschaften.Controls.Add(this.buttonMC);
            this.PanelEigenschaften.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelEigenschaften.Location = new System.Drawing.Point(0, 6);
            this.PanelEigenschaften.Margin = new System.Windows.Forms.Padding(0);
            this.PanelEigenschaften.Name = "PanelEigenschaften";
            this.PanelEigenschaften.Size = new System.Drawing.Size(262, 86);
            this.PanelEigenschaften.TabIndex = 1;
            // 
            // buttonKoppelung
            // 
            this.buttonKoppelung.Location = new System.Drawing.Point(169, 62);
            this.buttonKoppelung.Name = "buttonKoppelung";
            this.buttonKoppelung.Size = new System.Drawing.Size(89, 23);
            this.buttonKoppelung.TabIndex = 12;
            this.buttonKoppelung.Text = "Koppelung";
            this.buttonKoppelung.UseVisualStyleBackColor = true;
            this.buttonKoppelung.Click += new System.EventHandler(this.buttonKoppelung_Click);
            // 
            // buttonLöschen
            // 
            this.buttonLöschen.Location = new System.Drawing.Point(3, 40);
            this.buttonLöschen.Name = "buttonLöschen";
            this.buttonLöschen.Size = new System.Drawing.Size(58, 23);
            this.buttonLöschen.TabIndex = 5;
            this.buttonLöschen.Text = "Löschen";
            this.buttonLöschen.UseVisualStyleBackColor = true;
            // 
            // Ausgang_TB
            // 
            this.Ausgang_TB.Location = new System.Drawing.Point(113, 62);
            this.Ausgang_TB.Name = "Ausgang_TB";
            this.Ausgang_TB.ReadOnly = true;
            this.Ausgang_TB.Size = new System.Drawing.Size(50, 20);
            this.Ausgang_TB.TabIndex = 2;
            // 
            // Ausgang_label
            // 
            this.Ausgang_label.AutoSize = true;
            this.Ausgang_label.Location = new System.Drawing.Point(62, 66);
            this.Ausgang_label.Name = "Ausgang_label";
            this.Ausgang_label.Size = new System.Drawing.Size(49, 13);
            this.Ausgang_label.TabIndex = 9;
            this.Ausgang_label.Text = "Ausgang";
            // 
            // labelSchalter
            // 
            this.labelSchalter.AutoSize = true;
            this.labelSchalter.Location = new System.Drawing.Point(62, 28);
            this.labelSchalter.Name = "labelSchalter";
            this.labelSchalter.Size = new System.Drawing.Size(46, 13);
            this.labelSchalter.TabIndex = 7;
            this.labelSchalter.Text = "Schalter";
            // 
            // textBoxLageY
            // 
            this.textBoxLageY.Location = new System.Drawing.Point(208, 43);
            this.textBoxLageY.MaximumSize = new System.Drawing.Size(30, 20);
            this.textBoxLageY.MinimumSize = new System.Drawing.Size(50, 20);
            this.textBoxLageY.Name = "textBoxLageY";
            this.textBoxLageY.ReadOnly = true;
            this.textBoxLageY.Size = new System.Drawing.Size(50, 20);
            this.textBoxLageY.TabIndex = 4;
            // 
            // textBoxLageX
            // 
            this.textBoxLageX.Location = new System.Drawing.Point(208, 24);
            this.textBoxLageX.Name = "textBoxLageX";
            this.textBoxLageX.ReadOnly = true;
            this.textBoxLageX.Size = new System.Drawing.Size(50, 20);
            this.textBoxLageX.TabIndex = 3;
            // 
            // textBoxGleis
            // 
            this.textBoxGleis.Location = new System.Drawing.Point(113, 43);
            this.textBoxGleis.Name = "textBoxGleis";
            this.textBoxGleis.ReadOnly = true;
            this.textBoxGleis.Size = new System.Drawing.Size(50, 20);
            this.textBoxGleis.TabIndex = 1;
            // 
            // LabelLageY
            // 
            this.LabelLageY.AutoSize = true;
            this.LabelLageY.Location = new System.Drawing.Point(165, 47);
            this.LabelLageY.Name = "LabelLageY";
            this.LabelLageY.Size = new System.Drawing.Size(41, 13);
            this.LabelLageY.TabIndex = 11;
            this.LabelLageY.Text = "Lage Y";
            // 
            // labelLageX
            // 
            this.labelLageX.AutoSize = true;
            this.labelLageX.Location = new System.Drawing.Point(165, 28);
            this.labelLageX.Name = "labelLageX";
            this.labelLageX.Size = new System.Drawing.Size(41, 13);
            this.labelLageX.TabIndex = 10;
            this.labelLageX.Text = "Lage X";
            // 
            // labelGleis
            // 
            this.labelGleis.AutoSize = true;
            this.labelGleis.Location = new System.Drawing.Point(62, 47);
            this.labelGleis.Name = "labelGleis";
            this.labelGleis.Size = new System.Drawing.Size(30, 13);
            this.labelGleis.TabIndex = 8;
            this.labelGleis.Text = "Gleis";
            // 
            // textBoxSchalter
            // 
            this.textBoxSchalter.Location = new System.Drawing.Point(113, 24);
            this.textBoxSchalter.Name = "textBoxSchalter";
            this.textBoxSchalter.ReadOnly = true;
            this.textBoxSchalter.Size = new System.Drawing.Size(50, 20);
            this.textBoxSchalter.TabIndex = 0;
            // 
            // buttonMC
            // 
            this.buttonMC.Location = new System.Drawing.Point(3, 62);
            this.buttonMC.Name = "buttonMC";
            this.buttonMC.Size = new System.Drawing.Size(58, 23);
            this.buttonMC.TabIndex = 6;
            this.buttonMC.Text = "MC";
            this.buttonMC.UseVisualStyleBackColor = true;
            // 
            // buttonLaden
            // 
            this.buttonLaden.Location = new System.Drawing.Point(3, 18);
            this.buttonLaden.Name = "buttonLaden";
            this.buttonLaden.Size = new System.Drawing.Size(58, 23);
            this.buttonLaden.TabIndex = 13;
            this.buttonLaden.Text = "Laden";
            this.buttonLaden.UseVisualStyleBackColor = true;
            // 
            // FrmSchalter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.PanelMenü);
            this.Controls.Add(this.PanelEigenschaften);
            this.MinimumSize = new System.Drawing.Size(262, 26);
            this.Name = "FrmSchalter";
            this.Size = new System.Drawing.Size(262, 92);
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
    private System.Windows.Forms.Label labelSchalter;
    private System.Windows.Forms.TextBox textBoxLageY;
    private System.Windows.Forms.TextBox textBoxLageX;
    private System.Windows.Forms.TextBox textBoxGleis;
    private System.Windows.Forms.Label LabelLageY;
    private System.Windows.Forms.Label labelLageX;
    private System.Windows.Forms.Label labelGleis;
    private System.Windows.Forms.TextBox textBoxSchalter;
    private System.Windows.Forms.TextBox Ausgang_TB;
    private System.Windows.Forms.Label Ausgang_label;
    private System.Windows.Forms.Button buttonLöschen;
        private System.Windows.Forms.Button buttonKoppelung;
        private System.Windows.Forms.Button buttonLaden;
    }
}
