namespace ModellBahnSteuerung.ToolBox
{
  partial class RueckMeldung
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Nr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bezeichnung0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bezeichnung1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelArdunio = new System.Windows.Forms.Label();
            this.textBoxPlatine = new System.Windows.Forms.TextBox();
            this.buttonLaden = new System.Windows.Forms.Button();
            this.labelPlatine = new System.Windows.Forms.Label();
            this.textBoxArduino = new System.Windows.Forms.TextBox();
            this.PanelMenü.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxMenü)).BeginInit();
            this.PanelEigenschaften.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.PanelMenü.Size = new System.Drawing.Size(498, 26);
            this.PanelMenü.TabIndex = 0;
            // 
            // LabelMenü
            // 
            this.LabelMenü.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelMenü.Location = new System.Drawing.Point(0, 0);
            this.LabelMenü.Name = "LabelMenü";
            this.LabelMenü.Padding = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.LabelMenü.Size = new System.Drawing.Size(471, 24);
            this.LabelMenü.TabIndex = 1;
            this.LabelMenü.Text = "Rückmeldeplatine";
            this.LabelMenü.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictureBoxMenü
            // 
            this.PictureBoxMenü.Dock = System.Windows.Forms.DockStyle.Right;
            this.PictureBoxMenü.Image = global::ModellBahnSteuerung.Properties.Resources.Open;
            this.PictureBoxMenü.Location = new System.Drawing.Point(471, 0);
            this.PictureBoxMenü.Margin = new System.Windows.Forms.Padding(0);
            this.PictureBoxMenü.Name = "PictureBoxMenü";
            this.PictureBoxMenü.Size = new System.Drawing.Size(22, 24);
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
            this.PanelEigenschaften.AutoScroll = true;
            this.PanelEigenschaften.AutoSize = true;
            this.PanelEigenschaften.BackColor = System.Drawing.Color.White;
            this.PanelEigenschaften.Controls.Add(this.labelArdunio);
            this.PanelEigenschaften.Controls.Add(this.textBoxPlatine);
            this.PanelEigenschaften.Controls.Add(this.dataGridView1);
            this.PanelEigenschaften.Controls.Add(this.buttonLaden);
            this.PanelEigenschaften.Controls.Add(this.labelPlatine);
            this.PanelEigenschaften.Controls.Add(this.textBoxArduino);
            this.PanelEigenschaften.Location = new System.Drawing.Point(-1, 24);
            this.PanelEigenschaften.Margin = new System.Windows.Forms.Padding(0);
            this.PanelEigenschaften.Name = "PanelEigenschaften";
            this.PanelEigenschaften.Size = new System.Drawing.Size(495, 308);
            this.PanelEigenschaften.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nr,
            this.Bezeichnung0,
            this.Column2,
            this.Bezeichnung1,
            this.Column1});
            this.dataGridView1.Location = new System.Drawing.Point(61, 7);
            this.dataGridView1.MinimumSize = new System.Drawing.Size(289, 240);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 10;
            this.dataGridView1.RowTemplate.Height = 20;
            this.dataGridView1.Size = new System.Drawing.Size(431, 284);
            this.dataGridView1.TabIndex = 3;
            // 
            // Nr
            // 
            this.Nr.HeaderText = "Bit";
            this.Nr.Name = "Nr";
            this.Nr.ReadOnly = true;
            this.Nr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Nr.Width = 30;
            // 
            // Bezeichnung0
            // 
            this.Bezeichnung0.HeaderText = "KurzBez.";
            this.Bezeichnung0.Name = "Bezeichnung0";
            this.Bezeichnung0.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Bezeichnung";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Bezeichnung1
            // 
            this.Bezeichnung1.HeaderText = "Stecker";
            this.Bezeichnung1.Name = "Bezeichnung1";
            this.Bezeichnung1.ReadOnly = true;
            this.Bezeichnung1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Relais";
            this.Column1.Name = "Column1";
            // 
            // labelArdunio
            // 
            this.labelArdunio.AutoSize = true;
            this.labelArdunio.Location = new System.Drawing.Point(12, 11);
            this.labelArdunio.Name = "labelArdunio";
            this.labelArdunio.Size = new System.Drawing.Size(43, 13);
            this.labelArdunio.TabIndex = 1;
            this.labelArdunio.Text = "Arduino";
            // 
            // textBoxPlatine
            // 
            this.textBoxPlatine.Location = new System.Drawing.Point(12, 81);
            this.textBoxPlatine.Name = "textBoxPlatine";
            this.textBoxPlatine.Size = new System.Drawing.Size(39, 20);
            this.textBoxPlatine.TabIndex = 4;
            // 
            // buttonLaden
            // 
            this.buttonLaden.Location = new System.Drawing.Point(8, 107);
            this.buttonLaden.Name = "buttonLaden";
            this.buttonLaden.Size = new System.Drawing.Size(47, 23);
            this.buttonLaden.TabIndex = 0;
            this.buttonLaden.Text = "Laden";
            this.buttonLaden.UseVisualStyleBackColor = true;
            this.buttonLaden.Click += new System.EventHandler(this.buttonLaden_Click);
            // 
            // labelPlatine
            // 
            this.labelPlatine.AutoSize = true;
            this.labelPlatine.Location = new System.Drawing.Point(12, 65);
            this.labelPlatine.Name = "labelPlatine";
            this.labelPlatine.Size = new System.Drawing.Size(39, 13);
            this.labelPlatine.TabIndex = 2;
            this.labelPlatine.Text = "Platine";
            // 
            // textBoxArduino
            // 
            this.textBoxArduino.Location = new System.Drawing.Point(12, 27);
            this.textBoxArduino.Name = "textBoxArduino";
            this.textBoxArduino.Size = new System.Drawing.Size(39, 20);
            this.textBoxArduino.TabIndex = 2;
            // 
            // RueckMeldung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.PanelMenü);
            this.Controls.Add(this.PanelEigenschaften);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(353, 26);
            this.Name = "RueckMeldung";
            this.Size = new System.Drawing.Size(494, 332);
            this.PanelMenü.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxMenü)).EndInit();
            this.PanelEigenschaften.ResumeLayout(false);
            this.PanelEigenschaften.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel PanelMenü;
    private System.Windows.Forms.Label LabelMenü;
    private System.Windows.Forms.PictureBox PictureBoxMenü;
    private System.Windows.Forms.Panel PanelEigenschaften;
        private System.Windows.Forms.TextBox textBoxArduino;
        private System.Windows.Forms.Label labelArdunio;
        private System.Windows.Forms.Button buttonLaden;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nr;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bezeichnung0;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bezeichnung1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.TextBox textBoxPlatine;
        private System.Windows.Forms.Label labelPlatine;
    }
}
