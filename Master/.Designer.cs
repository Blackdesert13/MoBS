namespace ModellBahnSteuerung
{
    partial class FormStecker
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Nummerr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AnlagenBezeichnung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PCBezeichnung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Relais = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonLaden = new System.Windows.Forms.Button();
            this.buttonTyp = new System.Windows.Forms.Button();
            this.textBoxStecker = new System.Windows.Forms.TextBox();
            this.labelTyp = new System.Windows.Forms.Label();
            this.buttonExport = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nummerr,
            this.AnlagenBezeichnung,
            this.PCBezeichnung,
            this.Relais});
            this.dataGridView1.Location = new System.Drawing.Point(2, 38);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 25;
            this.dataGridView1.Size = new System.Drawing.Size(482, 382);
            this.dataGridView1.TabIndex = 0;
            // 
            // Nummerr
            // 
            this.Nummerr.HeaderText = "Nr.";
            this.Nummerr.Name = "Nummerr";
            this.Nummerr.Width = 27;
            // 
            // AnlagenBezeichnung
            // 
            this.AnlagenBezeichnung.HeaderText = "AnlagenBez.";
            this.AnlagenBezeichnung.Name = "AnlagenBezeichnung";
            // 
            // PCBezeichnung
            // 
            this.PCBezeichnung.HeaderText = "PC-Bez.";
            this.PCBezeichnung.Name = "PCBezeichnung";
            // 
            // Relais
            // 
            this.Relais.HeaderText = "Relais / RM";
            this.Relais.Name = "Relais";
            this.Relais.Width = 140;
            // 
            // buttonLaden
            // 
            this.buttonLaden.Location = new System.Drawing.Point(2, 9);
            this.buttonLaden.Name = "buttonLaden";
            this.buttonLaden.Size = new System.Drawing.Size(49, 23);
            this.buttonLaden.TabIndex = 1;
            this.buttonLaden.Text = "Laden";
            this.buttonLaden.UseVisualStyleBackColor = true;
            this.buttonLaden.Click += new System.EventHandler(this.buttonLaden_Click);
            // 
            // buttonTyp
            // 
            this.buttonTyp.Location = new System.Drawing.Point(163, 9);
            this.buttonTyp.Name = "buttonTyp";
            this.buttonTyp.Size = new System.Drawing.Size(73, 23);
            this.buttonTyp.TabIndex = 2;
            this.buttonTyp.Text = "Stecker-Typ";
            this.buttonTyp.UseVisualStyleBackColor = true;
            this.buttonTyp.Click += new System.EventHandler(this.buttonTyp_Click);
            // 
            // textBoxStecker
            // 
            this.textBoxStecker.Location = new System.Drawing.Point(57, 11);
            this.textBoxStecker.Name = "textBoxStecker";
            this.textBoxStecker.Size = new System.Drawing.Size(100, 20);
            this.textBoxStecker.TabIndex = 3;
            // 
            // labelTyp
            // 
            this.labelTyp.AutoSize = true;
            this.labelTyp.Location = new System.Drawing.Point(242, 14);
            this.labelTyp.Name = "labelTyp";
            this.labelTyp.Size = new System.Drawing.Size(25, 13);
            this.labelTyp.TabIndex = 4;
            this.labelTyp.Text = "Typ";
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(330, 9);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(47, 23);
            this.buttonExport.TabIndex = 5;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(383, 11);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // FormStecker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 420);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.labelTyp);
            this.Controls.Add(this.textBoxStecker);
            this.Controls.Add(this.buttonTyp);
            this.Controls.Add(this.buttonLaden);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormStecker";
            this.Text = "Stecker-Anzeige";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonLaden;
        private System.Windows.Forms.Button buttonTyp;
        private System.Windows.Forms.TextBox textBoxStecker;
        private System.Windows.Forms.Label labelTyp;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nummerr;
        private System.Windows.Forms.DataGridViewTextBoxColumn AnlagenBezeichnung;
        private System.Windows.Forms.DataGridViewTextBoxColumn PCBezeichnung;
        private System.Windows.Forms.DataGridViewTextBoxColumn Relais;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}