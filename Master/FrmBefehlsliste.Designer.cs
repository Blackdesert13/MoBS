namespace ModellBahnSteuerung
{
    partial class FrmBefehlsliste
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
            this.Element = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stellung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonUebernahme = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Element,
            this.Stellung});
            this.dataGridView1.Location = new System.Drawing.Point(0, 24);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(240, 363);
            this.dataGridView1.TabIndex = 0;
            // 
            // Element
            // 
            this.Element.HeaderText = "Element";
            this.Element.Name = "Element";
            // 
            // Stellung
            // 
            this.Stellung.HeaderText = "Stellung";
            this.Stellung.Name = "Stellung";
            // 
            // buttonUebernahme
            // 
            this.buttonUebernahme.Location = new System.Drawing.Point(0, 2);
            this.buttonUebernahme.Name = "buttonUebernahme";
            this.buttonUebernahme.Size = new System.Drawing.Size(76, 23);
            this.buttonUebernahme.TabIndex = 1;
            this.buttonUebernahme.Text = "Übernehmen";
            this.buttonUebernahme.UseVisualStyleBackColor = true;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(82, 7);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(35, 13);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "Name";
            // 
            // FrmBefehlsliste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 388);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.buttonUebernahme);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FrmBefehlsliste";
            this.Text = "Befehlsliste";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Element;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stellung;
        private System.Windows.Forms.Button buttonUebernahme;
        private System.Windows.Forms.Label labelName;
    }
}