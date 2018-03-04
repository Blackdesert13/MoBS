namespace ModellBahnSteuerung.ToolBox
{
  partial class FrmKnoten
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
            this.textBoxGleis2a = new System.Windows.Forms.TextBox();
            this.textBoxGleis2 = new System.Windows.Forms.TextBox();
            this.labelGleis2a = new System.Windows.Forms.Label();
            this.labelGleis2 = new System.Windows.Forms.Label();
            this.textBoxGleis1a = new System.Windows.Forms.TextBox();
            this.labelGleis1a = new System.Windows.Forms.Label();
            this.textBoxGleis1 = new System.Windows.Forms.TextBox();
            this.labelGleis1 = new System.Windows.Forms.Label();
            this.textBoxLageY = new System.Windows.Forms.TextBox();
            this.textBoxLageX = new System.Windows.Forms.TextBox();
            this.labelLageY = new System.Windows.Forms.Label();
            this.labelLageX = new System.Windows.Forms.Label();
            this.labelKnoten = new System.Windows.Forms.Label();
            this.textBoxKnoten = new System.Windows.Forms.TextBox();
            this.textBoxWeiche2 = new System.Windows.Forms.TextBox();
            this.labelWeiche2 = new System.Windows.Forms.Label();
            this.textBoxWeiche1 = new System.Windows.Forms.TextBox();
            this.labelWeiche1 = new System.Windows.Forms.Label();
            this.buttonMC = new System.Windows.Forms.Button();
            this.buttonLöschen = new System.Windows.Forms.Button();
            this.buttonSpeichern = new System.Windows.Forms.Button();
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
            this.PanelMenü.Size = new System.Drawing.Size(369, 26);
            this.PanelMenü.TabIndex = 0;
            // 
            // LabelMenü
            // 
            this.LabelMenü.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelMenü.Location = new System.Drawing.Point(0, 0);
            this.LabelMenü.Name = "LabelMenü";
            this.LabelMenü.Padding = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.LabelMenü.Size = new System.Drawing.Size(339, 24);
            this.LabelMenü.TabIndex = 1;
            this.LabelMenü.Text = "Knoten";
            this.LabelMenü.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBoxMenü
            // 
            this.PictureBoxMenü.Dock = System.Windows.Forms.DockStyle.Right;
            this.PictureBoxMenü.Image = global::ModellBahnSteuerung.Properties.Resources.Open;
            this.PictureBoxMenü.Location = new System.Drawing.Point(339, 0);
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
            this.PanelEigenschaften.Controls.Add(this.buttonSpeichern);
            this.PanelEigenschaften.Controls.Add(this.textBoxGleis2a);
            this.PanelEigenschaften.Controls.Add(this.textBoxGleis2);
            this.PanelEigenschaften.Controls.Add(this.labelGleis2a);
            this.PanelEigenschaften.Controls.Add(this.labelGleis2);
            this.PanelEigenschaften.Controls.Add(this.textBoxGleis1a);
            this.PanelEigenschaften.Controls.Add(this.labelGleis1a);
            this.PanelEigenschaften.Controls.Add(this.textBoxGleis1);
            this.PanelEigenschaften.Controls.Add(this.labelGleis1);
            this.PanelEigenschaften.Controls.Add(this.textBoxLageY);
            this.PanelEigenschaften.Controls.Add(this.textBoxLageX);
            this.PanelEigenschaften.Controls.Add(this.labelLageY);
            this.PanelEigenschaften.Controls.Add(this.labelLageX);
            this.PanelEigenschaften.Controls.Add(this.labelKnoten);
            this.PanelEigenschaften.Controls.Add(this.textBoxKnoten);
            this.PanelEigenschaften.Controls.Add(this.textBoxWeiche2);
            this.PanelEigenschaften.Controls.Add(this.labelWeiche2);
            this.PanelEigenschaften.Controls.Add(this.textBoxWeiche1);
            this.PanelEigenschaften.Controls.Add(this.labelWeiche1);
            this.PanelEigenschaften.Controls.Add(this.buttonMC);
            this.PanelEigenschaften.Controls.Add(this.buttonLöschen);
            this.PanelEigenschaften.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelEigenschaften.Location = new System.Drawing.Point(0, 26);
            this.PanelEigenschaften.Margin = new System.Windows.Forms.Padding(0);
            this.PanelEigenschaften.Name = "PanelEigenschaften";
            this.PanelEigenschaften.Size = new System.Drawing.Size(365, 86);
            this.PanelEigenschaften.TabIndex = 1;
            // 
            // textBoxGleis2a
            // 
            this.textBoxGleis2a.Location = new System.Drawing.Point(204, 62);
            this.textBoxGleis2a.Name = "textBoxGleis2a";
            this.textBoxGleis2a.ReadOnly = true;
            this.textBoxGleis2a.Size = new System.Drawing.Size(50, 20);
            this.textBoxGleis2a.TabIndex = 6;
            // 
            // textBoxGleis2
            // 
            this.textBoxGleis2.Location = new System.Drawing.Point(204, 43);
            this.textBoxGleis2.Name = "textBoxGleis2";
            this.textBoxGleis2.ReadOnly = true;
            this.textBoxGleis2.Size = new System.Drawing.Size(50, 20);
            this.textBoxGleis2.TabIndex = 5;
            // 
            // labelGleis2a
            // 
            this.labelGleis2a.AutoSize = true;
            this.labelGleis2a.Location = new System.Drawing.Point(157, 66);
            this.labelGleis2a.Name = "labelGleis2a";
            this.labelGleis2a.Size = new System.Drawing.Size(45, 13);
            this.labelGleis2a.TabIndex = 18;
            this.labelGleis2a.Text = "Gleis 2a";
            // 
            // labelGleis2
            // 
            this.labelGleis2.AutoSize = true;
            this.labelGleis2.Location = new System.Drawing.Point(157, 47);
            this.labelGleis2.Name = "labelGleis2";
            this.labelGleis2.Size = new System.Drawing.Size(39, 13);
            this.labelGleis2.TabIndex = 17;
            this.labelGleis2.Text = "Gleis 2";
            // 
            // textBoxGleis1a
            // 
            this.textBoxGleis1a.Location = new System.Drawing.Point(204, 24);
            this.textBoxGleis1a.Name = "textBoxGleis1a";
            this.textBoxGleis1a.ReadOnly = true;
            this.textBoxGleis1a.Size = new System.Drawing.Size(50, 20);
            this.textBoxGleis1a.TabIndex = 4;
            // 
            // labelGleis1a
            // 
            this.labelGleis1a.AutoSize = true;
            this.labelGleis1a.Location = new System.Drawing.Point(157, 28);
            this.labelGleis1a.Name = "labelGleis1a";
            this.labelGleis1a.Size = new System.Drawing.Size(45, 13);
            this.labelGleis1a.TabIndex = 16;
            this.labelGleis1a.Text = "Gleis 1a";
            // 
            // textBoxGleis1
            // 
            this.textBoxGleis1.Location = new System.Drawing.Point(204, 5);
            this.textBoxGleis1.Name = "textBoxGleis1";
            this.textBoxGleis1.ReadOnly = true;
            this.textBoxGleis1.Size = new System.Drawing.Size(50, 20);
            this.textBoxGleis1.TabIndex = 3;
            // 
            // labelGleis1
            // 
            this.labelGleis1.AutoSize = true;
            this.labelGleis1.Location = new System.Drawing.Point(157, 9);
            this.labelGleis1.Name = "labelGleis1";
            this.labelGleis1.Size = new System.Drawing.Size(39, 13);
            this.labelGleis1.TabIndex = 15;
            this.labelGleis1.Text = "Gleis 1";
            // 
            // textBoxLageY
            // 
            this.textBoxLageY.Location = new System.Drawing.Point(105, 43);
            this.textBoxLageY.Name = "textBoxLageY";
            this.textBoxLageY.Size = new System.Drawing.Size(50, 20);
            this.textBoxLageY.TabIndex = 2;
            // 
            // textBoxLageX
            // 
            this.textBoxLageX.Location = new System.Drawing.Point(105, 24);
            this.textBoxLageX.Name = "textBoxLageX";
            this.textBoxLageX.Size = new System.Drawing.Size(50, 20);
            this.textBoxLageX.TabIndex = 1;
            // 
            // labelLageY
            // 
            this.labelLageY.AutoSize = true;
            this.labelLageY.Location = new System.Drawing.Point(62, 47);
            this.labelLageY.Name = "labelLageY";
            this.labelLageY.Size = new System.Drawing.Size(41, 13);
            this.labelLageY.TabIndex = 14;
            this.labelLageY.Text = "Lage Y";
            // 
            // labelLageX
            // 
            this.labelLageX.AutoSize = true;
            this.labelLageX.Location = new System.Drawing.Point(62, 28);
            this.labelLageX.Name = "labelLageX";
            this.labelLageX.Size = new System.Drawing.Size(41, 13);
            this.labelLageX.TabIndex = 13;
            this.labelLageX.Text = "Lage X";
            // 
            // labelKnoten
            // 
            this.labelKnoten.AutoSize = true;
            this.labelKnoten.Location = new System.Drawing.Point(62, 9);
            this.labelKnoten.Name = "labelKnoten";
            this.labelKnoten.Size = new System.Drawing.Size(41, 13);
            this.labelKnoten.TabIndex = 12;
            this.labelKnoten.Text = "Knoten";
            // 
            // textBoxKnoten
            // 
            this.textBoxKnoten.Location = new System.Drawing.Point(105, 5);
            this.textBoxKnoten.Name = "textBoxKnoten";
            this.textBoxKnoten.ReadOnly = true;
            this.textBoxKnoten.Size = new System.Drawing.Size(50, 20);
            this.textBoxKnoten.TabIndex = 0;
            // 
            // textBoxWeiche2
            // 
            this.textBoxWeiche2.Location = new System.Drawing.Point(311, 24);
            this.textBoxWeiche2.Name = "textBoxWeiche2";
            this.textBoxWeiche2.ReadOnly = true;
            this.textBoxWeiche2.Size = new System.Drawing.Size(50, 20);
            this.textBoxWeiche2.TabIndex = 8;
            // 
            // labelWeiche2
            // 
            this.labelWeiche2.AutoSize = true;
            this.labelWeiche2.Location = new System.Drawing.Point(256, 28);
            this.labelWeiche2.Name = "labelWeiche2";
            this.labelWeiche2.Size = new System.Drawing.Size(53, 13);
            this.labelWeiche2.TabIndex = 20;
            this.labelWeiche2.Text = "Weiche 2";
            // 
            // textBoxWeiche1
            // 
            this.textBoxWeiche1.Location = new System.Drawing.Point(311, 5);
            this.textBoxWeiche1.Name = "textBoxWeiche1";
            this.textBoxWeiche1.ReadOnly = true;
            this.textBoxWeiche1.Size = new System.Drawing.Size(50, 20);
            this.textBoxWeiche1.TabIndex = 7;
            // 
            // labelWeiche1
            // 
            this.labelWeiche1.AutoSize = true;
            this.labelWeiche1.Location = new System.Drawing.Point(256, 9);
            this.labelWeiche1.Name = "labelWeiche1";
            this.labelWeiche1.Size = new System.Drawing.Size(53, 13);
            this.labelWeiche1.TabIndex = 19;
            this.labelWeiche1.Text = "Weiche 1";
            // 
            // buttonMC
            // 
            this.buttonMC.Location = new System.Drawing.Point(3, 59);
            this.buttonMC.Name = "buttonMC";
            this.buttonMC.Size = new System.Drawing.Size(58, 23);
            this.buttonMC.TabIndex = 10;
            this.buttonMC.Text = "MC";
            this.buttonMC.UseVisualStyleBackColor = true;
            // 
            // buttonLöschen
            // 
            this.buttonLöschen.Location = new System.Drawing.Point(3, 5);
            this.buttonLöschen.Name = "buttonLöschen";
            this.buttonLöschen.Size = new System.Drawing.Size(58, 23);
            this.buttonLöschen.TabIndex = 9;
            this.buttonLöschen.Text = "Löschen";
            this.buttonLöschen.UseVisualStyleBackColor = true;
            // 
            // buttonSpeichern
            // 
            this.buttonSpeichern.Location = new System.Drawing.Point(3, 30);
            this.buttonSpeichern.Name = "buttonSpeichern";
            this.buttonSpeichern.Size = new System.Drawing.Size(58, 23);
            this.buttonSpeichern.TabIndex = 21;
            this.buttonSpeichern.Text = "Überna.";
            this.buttonSpeichern.UseVisualStyleBackColor = true;
            this.buttonSpeichern.Click += new System.EventHandler(this.buttonSpeichern_Click);
            // 
            // FrmKnoten
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.PanelMenü);
            this.Controls.Add(this.PanelEigenschaften);
            this.MinimumSize = new System.Drawing.Size(365, 26);
            this.Name = "FrmKnoten";
            this.Size = new System.Drawing.Size(365, 112);
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
    private System.Windows.Forms.Button buttonLöschen;
    private System.Windows.Forms.Button buttonMC;
    private System.Windows.Forms.Label Weiche_1_Label;
    private System.Windows.Forms.TextBox Weiche_1_TB;
    private System.Windows.Forms.Label Weiche_2_Label;
    private System.Windows.Forms.TextBox Weiche_2_TB;
    private System.Windows.Forms.Label labelGleis1;
    private System.Windows.Forms.TextBox textBoxLageY;
    private System.Windows.Forms.TextBox textBoxLageX;
    private System.Windows.Forms.Label labelLageY;
    private System.Windows.Forms.Label labelLageX;
    private System.Windows.Forms.Label labelKnoten;
    private System.Windows.Forms.TextBox textBoxKnoten;
    private System.Windows.Forms.TextBox textBoxWeiche2;
    private System.Windows.Forms.Label labelWeiche2;
    private System.Windows.Forms.TextBox textBoxWeiche1;
    private System.Windows.Forms.Label labelWeiche1;
    private System.Windows.Forms.TextBox textBoxGleis1;
    private System.Windows.Forms.Label labelGleis1a;
    private System.Windows.Forms.TextBox textBoxGleis1a;
    private System.Windows.Forms.Label labelGleis2;
    private System.Windows.Forms.Label labelGleis2a;
    private System.Windows.Forms.TextBox textBoxGleis2;
    private System.Windows.Forms.TextBox textBoxGleis2a;
        private System.Windows.Forms.Button buttonSpeichern;
    }
}
