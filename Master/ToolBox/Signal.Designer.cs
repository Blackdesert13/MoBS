﻿namespace ModellBahnSteuerung.ToolBox
{
  partial class Signal
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
      this.buttonLöschen = new System.Windows.Forms.Button();
      this.textBoxLageY = new System.Windows.Forms.TextBox();
      this.textBoxLageX = new System.Windows.Forms.TextBox();
      this.LabelAusgang = new System.Windows.Forms.Label();
      this.labelRichtung = new System.Windows.Forms.Label();
      this.labelSignal = new System.Windows.Forms.Label();
      this.Adresse_TB = new System.Windows.Forms.TextBox();
      this.labelPosition = new System.Windows.Forms.Label();
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
      this.PanelMenü.Size = new System.Drawing.Size(270, 26);
      this.PanelMenü.TabIndex = 0;
      // 
      // LabelMenü
      // 
      this.LabelMenü.Dock = System.Windows.Forms.DockStyle.Fill;
      this.LabelMenü.Location = new System.Drawing.Point(0, 0);
      this.LabelMenü.Name = "LabelMenü";
      this.LabelMenü.Padding = new System.Windows.Forms.Padding(5, 2, 2, 2);
      this.LabelMenü.Size = new System.Drawing.Size(240, 24);
      this.LabelMenü.TabIndex = 1;
      this.LabelMenü.Text = "Signal";
      this.LabelMenü.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // PictureBoxMenü
      // 
      this.PictureBoxMenü.Dock = System.Windows.Forms.DockStyle.Right;
      this.PictureBoxMenü.Image = global::ModellBahnSteuerung.Properties.Resources.Open;
      this.PictureBoxMenü.Location = new System.Drawing.Point(240, 0);
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
      this.PanelEigenschaften.Controls.Add(this.buttonLöschen);
      this.PanelEigenschaften.Controls.Add(this.textBoxLageY);
      this.PanelEigenschaften.Controls.Add(this.textBoxLageX);
      this.PanelEigenschaften.Controls.Add(this.LabelAusgang);
      this.PanelEigenschaften.Controls.Add(this.labelRichtung);
      this.PanelEigenschaften.Controls.Add(this.labelSignal);
      this.PanelEigenschaften.Controls.Add(this.Adresse_TB);
      this.PanelEigenschaften.Controls.Add(this.labelPosition);
      this.PanelEigenschaften.Controls.Add(this.textBoxGleis);
      this.PanelEigenschaften.Controls.Add(this.labelGleis);
      this.PanelEigenschaften.Controls.Add(this.textBoxEntkuppler);
      this.PanelEigenschaften.Controls.Add(this.buttonMC);
      this.PanelEigenschaften.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.PanelEigenschaften.Location = new System.Drawing.Point(0, 26);
      this.PanelEigenschaften.Margin = new System.Windows.Forms.Padding(0);
      this.PanelEigenschaften.Name = "PanelEigenschaften";
      this.PanelEigenschaften.Size = new System.Drawing.Size(266, 67);
      this.PanelEigenschaften.TabIndex = 1;
      // 
      // buttonLöschen
      // 
      this.buttonLöschen.Location = new System.Drawing.Point(3, 5);
      this.buttonLöschen.Name = "buttonLöschen";
      this.buttonLöschen.Size = new System.Drawing.Size(58, 23);
      this.buttonLöschen.TabIndex = 5;
      this.buttonLöschen.Text = "Löschen";
      this.buttonLöschen.UseVisualStyleBackColor = true;
      // 
      // textBoxLageY
      // 
      this.textBoxLageY.Location = new System.Drawing.Point(212, 24);
      this.textBoxLageY.MaximumSize = new System.Drawing.Size(30, 20);
      this.textBoxLageY.MinimumSize = new System.Drawing.Size(50, 20);
      this.textBoxLageY.Name = "textBoxLageY";
      this.textBoxLageY.ReadOnly = true;
      this.textBoxLageY.Size = new System.Drawing.Size(50, 20);
      this.textBoxLageY.TabIndex = 4;
      // 
      // textBoxLageX
      // 
      this.textBoxLageX.Location = new System.Drawing.Point(212, 5);
      this.textBoxLageX.Name = "textBoxLageX";
      this.textBoxLageX.ReadOnly = true;
      this.textBoxLageX.Size = new System.Drawing.Size(50, 20);
      this.textBoxLageX.TabIndex = 3;
      // 
      // LabelAusgang
      // 
      this.LabelAusgang.AutoSize = true;
      this.LabelAusgang.Location = new System.Drawing.Point(160, 28);
      this.LabelAusgang.Name = "LabelAusgang";
      this.LabelAusgang.Size = new System.Drawing.Size(49, 13);
      this.LabelAusgang.TabIndex = 11;
      this.LabelAusgang.Text = "Ausgang";
      // 
      // labelRichtung
      // 
      this.labelRichtung.AutoSize = true;
      this.labelRichtung.Location = new System.Drawing.Point(160, 9);
      this.labelRichtung.Name = "labelRichtung";
      this.labelRichtung.Size = new System.Drawing.Size(50, 13);
      this.labelRichtung.TabIndex = 10;
      this.labelRichtung.Text = "Richtung";
      // 
      // labelSignal
      // 
      this.labelSignal.AutoSize = true;
      this.labelSignal.Location = new System.Drawing.Point(62, 9);
      this.labelSignal.Name = "labelSignal";
      this.labelSignal.Size = new System.Drawing.Size(36, 13);
      this.labelSignal.TabIndex = 7;
      this.labelSignal.Text = "Signal";
      // 
      // Adresse_TB
      // 
      this.Adresse_TB.Location = new System.Drawing.Point(108, 43);
      this.Adresse_TB.Name = "Adresse_TB";
      this.Adresse_TB.ReadOnly = true;
      this.Adresse_TB.Size = new System.Drawing.Size(50, 20);
      this.Adresse_TB.TabIndex = 2;
      // 
      // labelPosition
      // 
      this.labelPosition.AutoSize = true;
      this.labelPosition.Location = new System.Drawing.Point(62, 47);
      this.labelPosition.Name = "labelPosition";
      this.labelPosition.Size = new System.Drawing.Size(44, 13);
      this.labelPosition.TabIndex = 9;
      this.labelPosition.Text = "Position";
      // 
      // textBoxGleis
      // 
      this.textBoxGleis.Location = new System.Drawing.Point(108, 24);
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
      this.textBoxEntkuppler.Location = new System.Drawing.Point(108, 5);
      this.textBoxEntkuppler.Name = "textBoxEntkuppler";
      this.textBoxEntkuppler.ReadOnly = true;
      this.textBoxEntkuppler.Size = new System.Drawing.Size(50, 20);
      this.textBoxEntkuppler.TabIndex = 0;
      // 
      // buttonMC
      // 
      this.buttonMC.Location = new System.Drawing.Point(3, 41);
      this.buttonMC.Name = "buttonMC";
      this.buttonMC.Size = new System.Drawing.Size(58, 23);
      this.buttonMC.TabIndex = 6;
      this.buttonMC.Text = "MC";
      this.buttonMC.UseVisualStyleBackColor = true;
      // 
      // Signal
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Control;
      this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.Controls.Add(this.PanelMenü);
      this.Controls.Add(this.PanelEigenschaften);
      this.MinimumSize = new System.Drawing.Size(266, 26);
      this.Name = "Signal";
      this.Size = new System.Drawing.Size(266, 93);
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
    private System.Windows.Forms.TextBox textBoxLageY;
    private System.Windows.Forms.TextBox textBoxLageX;
    private System.Windows.Forms.Label LabelAusgang;
    private System.Windows.Forms.Label labelRichtung;
    private System.Windows.Forms.Label labelSignal;
    private System.Windows.Forms.TextBox Adresse_TB;
    private System.Windows.Forms.Label labelPosition;
    private System.Windows.Forms.TextBox textBoxGleis;
    private System.Windows.Forms.Label labelGleis;
    private System.Windows.Forms.TextBox textBoxEntkuppler;
    private System.Windows.Forms.Button buttonMC;
    private System.Windows.Forms.Button buttonLöschen;
  }
}
