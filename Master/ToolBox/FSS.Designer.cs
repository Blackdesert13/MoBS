namespace ModellBahnSteuerung.ToolBox {
    partial class FrmFSS {
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

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            this.PanelMenü = new System.Windows.Forms.Panel();
            this.LabelMenü = new System.Windows.Forms.Label();
            this.PictureBoxMenü = new System.Windows.Forms.PictureBox();
            this.PanelEigenschaften = new System.Windows.Forms.Panel();
            this.labelStecker = new System.Windows.Forms.Label();
            this.textBoxStecker = new System.Windows.Forms.TextBox();
            this.buttonSpeichern = new System.Windows.Forms.Button();
            this.textBoxRegler2 = new System.Windows.Forms.TextBox();
            this.textBoxRegler1 = new System.Windows.Forms.TextBox();
            this.buttonRegler2 = new System.Windows.Forms.Button();
            this.buttonRegler1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelRegler1 = new System.Windows.Forms.Label();
            this.labelID = new System.Windows.Forms.Label();
            this.Bezeichnung_Label = new System.Windows.Forms.Label();
            this.textBoxBezeichnung = new System.Windows.Forms.TextBox();
            this.Adr_Ausgang_label = new System.Windows.Forms.Label();
            this.textBoxAusgang = new System.Windows.Forms.TextBox();
            this.textBoxFSS = new System.Windows.Forms.TextBox();
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
            this.LabelMenü.Text = "FSS";
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
            this.PanelEigenschaften.Controls.Add(this.labelStecker);
            this.PanelEigenschaften.Controls.Add(this.textBoxStecker);
            this.PanelEigenschaften.Controls.Add(this.buttonSpeichern);
            this.PanelEigenschaften.Controls.Add(this.textBoxRegler2);
            this.PanelEigenschaften.Controls.Add(this.textBoxRegler1);
            this.PanelEigenschaften.Controls.Add(this.buttonRegler2);
            this.PanelEigenschaften.Controls.Add(this.buttonRegler1);
            this.PanelEigenschaften.Controls.Add(this.label1);
            this.PanelEigenschaften.Controls.Add(this.labelRegler1);
            this.PanelEigenschaften.Controls.Add(this.labelID);
            this.PanelEigenschaften.Controls.Add(this.Bezeichnung_Label);
            this.PanelEigenschaften.Controls.Add(this.textBoxBezeichnung);
            this.PanelEigenschaften.Controls.Add(this.Adr_Ausgang_label);
            this.PanelEigenschaften.Controls.Add(this.textBoxAusgang);
            this.PanelEigenschaften.Controls.Add(this.textBoxFSS);
            this.PanelEigenschaften.Controls.Add(this.buttonLaden);
            this.PanelEigenschaften.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelEigenschaften.Location = new System.Drawing.Point(0, 26);
            this.PanelEigenschaften.Margin = new System.Windows.Forms.Padding(0);
            this.PanelEigenschaften.Name = "PanelEigenschaften";
            this.PanelEigenschaften.Size = new System.Drawing.Size(338, 85);
            this.PanelEigenschaften.TabIndex = 1;
            // 
            // labelStecker
            // 
            this.labelStecker.AutoSize = true;
            this.labelStecker.Location = new System.Drawing.Point(190, 64);
            this.labelStecker.Name = "labelStecker";
            this.labelStecker.Size = new System.Drawing.Size(44, 13);
            this.labelStecker.TabIndex = 15;
            this.labelStecker.Text = "Stecker";
            // 
            // textBoxStecker
            // 
            this.textBoxStecker.Location = new System.Drawing.Point(235, 61);
            this.textBoxStecker.Name = "textBoxStecker";
            this.textBoxStecker.Size = new System.Drawing.Size(99, 20);
            this.textBoxStecker.TabIndex = 14;
            // 
            // buttonSpeichern
            // 
            this.buttonSpeichern.Location = new System.Drawing.Point(3, 33);
            this.buttonSpeichern.Name = "buttonSpeichern";
            this.buttonSpeichern.Size = new System.Drawing.Size(45, 23);
            this.buttonSpeichern.TabIndex = 13;
            this.buttonSpeichern.Text = "Übern.";
            this.buttonSpeichern.UseVisualStyleBackColor = true;
            this.buttonSpeichern.Click += new System.EventHandler(this.buttonSpeichern_Click);
            // 
            // textBoxRegler2
            // 
            this.textBoxRegler2.Location = new System.Drawing.Point(130, 62);
            this.textBoxRegler2.Name = "textBoxRegler2";
            this.textBoxRegler2.Size = new System.Drawing.Size(37, 20);
            this.textBoxRegler2.TabIndex = 12;
            // 
            // textBoxRegler1
            // 
            this.textBoxRegler1.Location = new System.Drawing.Point(130, 23);
            this.textBoxRegler1.Name = "textBoxRegler1";
            this.textBoxRegler1.Size = new System.Drawing.Size(37, 20);
            this.textBoxRegler1.TabIndex = 11;
            // 
            // buttonRegler2
            // 
            this.buttonRegler2.Location = new System.Drawing.Point(108, 32);
            this.buttonRegler2.Name = "buttonRegler2";
            this.buttonRegler2.Size = new System.Drawing.Size(13, 23);
            this.buttonRegler2.TabIndex = 10;
            this.buttonRegler2.UseVisualStyleBackColor = true;
            // 
            // buttonRegler1
            // 
            this.buttonRegler1.Location = new System.Drawing.Point(108, 7);
            this.buttonRegler1.Name = "buttonRegler1";
            this.buttonRegler1.Size = new System.Drawing.Size(13, 23);
            this.buttonRegler1.TabIndex = 9;
            this.buttonRegler1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(127, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Regler2 Schließer";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelRegler1
            // 
            this.labelRegler1.AutoSize = true;
            this.labelRegler1.Location = new System.Drawing.Point(127, 6);
            this.labelRegler1.Name = "labelRegler1";
            this.labelRegler1.Size = new System.Drawing.Size(76, 13);
            this.labelRegler1.TabIndex = 7;
            this.labelRegler1.Text = "Regler1 Öffner";
            // 
            // labelID
            // 
            this.labelID.AutoSize = true;
            this.labelID.Location = new System.Drawing.Point(48, 11);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(18, 13);
            this.labelID.TabIndex = 6;
            this.labelID.Text = "ID";
            // 
            // Bezeichnung_Label
            // 
            this.Bezeichnung_Label.AutoSize = true;
            this.Bezeichnung_Label.Location = new System.Drawing.Point(222, 38);
            this.Bezeichnung_Label.Name = "Bezeichnung_Label";
            this.Bezeichnung_Label.Size = new System.Drawing.Size(69, 13);
            this.Bezeichnung_Label.TabIndex = 5;
            this.Bezeichnung_Label.Text = "Bezeichnung";
            // 
            // textBoxBezeichnung
            // 
            this.textBoxBezeichnung.Location = new System.Drawing.Point(291, 35);
            this.textBoxBezeichnung.Name = "textBoxBezeichnung";
            this.textBoxBezeichnung.Size = new System.Drawing.Size(44, 20);
            this.textBoxBezeichnung.TabIndex = 4;
            // 
            // Adr_Ausgang_label
            // 
            this.Adr_Ausgang_label.AutoSize = true;
            this.Adr_Ausgang_label.Location = new System.Drawing.Point(222, 12);
            this.Adr_Ausgang_label.Name = "Adr_Ausgang_label";
            this.Adr_Ausgang_label.Size = new System.Drawing.Size(71, 13);
            this.Adr_Ausgang_label.TabIndex = 3;
            this.Adr_Ausgang_label.Text = "Adr. Ausgang";
            // 
            // textBoxAusgang
            // 
            this.textBoxAusgang.Location = new System.Drawing.Point(291, 9);
            this.textBoxAusgang.Name = "textBoxAusgang";
            this.textBoxAusgang.Size = new System.Drawing.Size(44, 20);
            this.textBoxAusgang.TabIndex = 2;
            // 
            // textBoxFSS
            // 
            this.textBoxFSS.Location = new System.Drawing.Point(68, 8);
            this.textBoxFSS.Name = "textBoxFSS";
            this.textBoxFSS.Size = new System.Drawing.Size(34, 20);
            this.textBoxFSS.TabIndex = 1;
            // 
            // buttonLaden
            // 
            this.buttonLaden.Location = new System.Drawing.Point(3, 6);
            this.buttonLaden.Name = "buttonLaden";
            this.buttonLaden.Size = new System.Drawing.Size(45, 23);
            this.buttonLaden.TabIndex = 0;
            this.buttonLaden.Text = "Laden";
            this.buttonLaden.UseVisualStyleBackColor = true;
            this.buttonLaden.Click += new System.EventHandler(this.buttonLaden_Click);
            // 
            // FrmFSS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.PanelMenü);
            this.Controls.Add(this.PanelEigenschaften);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(268, 26);
            this.Name = "FrmFSS";
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
        private System.Windows.Forms.Button buttonLaden;
        private System.Windows.Forms.TextBox textBoxFSS;
        private System.Windows.Forms.Label Adr_Ausgang_label;
        private System.Windows.Forms.TextBox textBoxAusgang;
        private System.Windows.Forms.Label Bezeichnung_Label;
        private System.Windows.Forms.TextBox textBoxBezeichnung;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelRegler1;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.TextBox textBoxRegler2;
        private System.Windows.Forms.TextBox textBoxRegler1;
        private System.Windows.Forms.Button buttonRegler2;
        private System.Windows.Forms.Button buttonRegler1;
        private System.Windows.Forms.Button buttonSpeichern;
        private System.Windows.Forms.Label labelStecker;
        private System.Windows.Forms.TextBox textBoxStecker;
    }
}
