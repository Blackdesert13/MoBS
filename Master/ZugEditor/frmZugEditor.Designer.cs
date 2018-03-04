namespace ModellBahnSteuerung.ZugEditor
{
    partial class frmZugEditor
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
            this.Nummer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Signal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lok = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Typ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Speed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bezeichnung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonUebernehmen = new System.Windows.Forms.Button();
            this.buttonAbbruch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nummer,
            this.Signal,
            this.Lok,
            this.Typ,
            this.Speed,
            this.Bezeichnung});
            this.dataGridView1.Location = new System.Drawing.Point(12, 38);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(582, 318);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Nummer
            // 
            this.Nummer.Frozen = true;
            this.Nummer.HeaderText = "Zug-Nr.";
            this.Nummer.Name = "Nummer";
            this.Nummer.Width = 50;
            // 
            // Signal
            // 
            this.Signal.DataPropertyName = "integer";
            this.Signal.Frozen = true;
            this.Signal.HeaderText = "Signal";
            this.Signal.Name = "Signal";
            this.Signal.ToolTipText = "Position auf Anlage";
            this.Signal.Width = 40;
            // 
            // Lok
            // 
            this.Lok.Frozen = true;
            this.Lok.HeaderText = "Lok";
            this.Lok.Name = "Lok";
            this.Lok.Width = 50;
            // 
            // Typ
            // 
            this.Typ.HeaderText = "Zug-Typ";
            this.Typ.Name = "Typ";
            this.Typ.Width = 50;
            // 
            // Speed
            // 
            this.Speed.HeaderText = "Geschw.";
            this.Speed.Name = "Speed";
            this.Speed.Width = 50;
            // 
            // Bezeichnung
            // 
            this.Bezeichnung.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Bezeichnung.HeaderText = "Bezeichnung";
            this.Bezeichnung.Name = "Bezeichnung";
            this.Bezeichnung.Width = 300;
            // 
            // buttonUebernehmen
            // 
            this.buttonUebernehmen.Location = new System.Drawing.Point(38, 376);
            this.buttonUebernehmen.Name = "buttonUebernehmen";
            this.buttonUebernehmen.Size = new System.Drawing.Size(83, 23);
            this.buttonUebernehmen.TabIndex = 3;
            this.buttonUebernehmen.Text = "Übernehmen";
            this.buttonUebernehmen.UseVisualStyleBackColor = true;
            this.buttonUebernehmen.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonAbbruch
            // 
            this.buttonAbbruch.Location = new System.Drawing.Point(308, 376);
            this.buttonAbbruch.Name = "buttonAbbruch";
            this.buttonAbbruch.Size = new System.Drawing.Size(75, 23);
            this.buttonAbbruch.TabIndex = 4;
            this.buttonAbbruch.Text = "Abbrechen";
            this.buttonAbbruch.UseVisualStyleBackColor = true;
            this.buttonAbbruch.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmZugEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 406);
            this.Controls.Add(this.buttonAbbruch);
            this.Controls.Add(this.buttonUebernehmen);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmZugEditor";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonUebernehmen;
        private System.Windows.Forms.Button buttonAbbruch;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nummer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Signal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lok;
        private System.Windows.Forms.DataGridViewTextBoxColumn Typ;
        private System.Windows.Forms.DataGridViewTextBoxColumn Speed;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bezeichnung;
    }
}