namespace ModellBahnSteuerung.FahrstrassenEditor {
	partial class FahrstrassenEditor {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.alleFahrstraßenSuchenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fahrstraßenSuchenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageTabelle = new System.Windows.Forms.TabPage();
			this.tabPagePropertyGrid = new System.Windows.Forms.TabPage();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewButtonColumn();
			this.Column3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.menuStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPageTabelle.SuspendLayout();
			this.tabPagePropertyGrid.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alleFahrstraßenSuchenToolStripMenuItem,
            this.fahrstraßenSuchenToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(431, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// alleFahrstraßenSuchenToolStripMenuItem
			// 
			this.alleFahrstraßenSuchenToolStripMenuItem.AutoToolTip = true;
			this.alleFahrstraßenSuchenToolStripMenuItem.Name = "alleFahrstraßenSuchenToolStripMenuItem";
			this.alleFahrstraßenSuchenToolStripMenuItem.Size = new System.Drawing.Size(168, 20);
			this.alleFahrstraßenSuchenToolStripMenuItem.Text = "Alle Fahrstraßen neu suchen";
			this.alleFahrstraßenSuchenToolStripMenuItem.ToolTipText = "löscht alle vorhanden Fahrstraßen und sucht alle Fahrstraßen von allen Signalen";
			this.alleFahrstraßenSuchenToolStripMenuItem.Click += new System.EventHandler(this.alleFahrstraßenSuchenToolStripMenuItem_Click);
			// 
			// fahrstraßenSuchenToolStripMenuItem
			// 
			this.fahrstraßenSuchenToolStripMenuItem.AutoToolTip = true;
			this.fahrstraßenSuchenToolStripMenuItem.Name = "fahrstraßenSuchenToolStripMenuItem";
			this.fahrstraßenSuchenToolStripMenuItem.Size = new System.Drawing.Size(122, 20);
			this.fahrstraßenSuchenToolStripMenuItem.Text = "Fahrstraßen suchen";
			this.fahrstraßenSuchenToolStripMenuItem.ToolTipText = "sucht alle Fahrstraßen vom ausgewählten Signal";
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.propertyGrid1.Location = new System.Drawing.Point(20, 6);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(380, 331);
			this.propertyGrid1.TabIndex = 1;
			// 
			// tabControl1
			// 
			this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.tabControl1.Controls.Add(this.tabPageTabelle);
			this.tabControl1.Controls.Add(this.tabPagePropertyGrid);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 24);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(431, 379);
			this.tabControl1.TabIndex = 2;
			// 
			// tabPageTabelle
			// 
			this.tabPageTabelle.Controls.Add(this.dataGridView1);
			this.tabPageTabelle.Location = new System.Drawing.Point(4, 4);
			this.tabPageTabelle.Name = "tabPageTabelle";
			this.tabPageTabelle.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageTabelle.Size = new System.Drawing.Size(423, 353);
			this.tabPageTabelle.TabIndex = 0;
			this.tabPageTabelle.Text = "TabellenAnsicht";
			this.tabPageTabelle.UseVisualStyleBackColor = true;
			// 
			// tabPagePropertyGrid
			// 
			this.tabPagePropertyGrid.Controls.Add(this.propertyGrid1);
			this.tabPagePropertyGrid.Location = new System.Drawing.Point(4, 4);
			this.tabPagePropertyGrid.Name = "tabPagePropertyGrid";
			this.tabPagePropertyGrid.Padding = new System.Windows.Forms.Padding(3);
			this.tabPagePropertyGrid.Size = new System.Drawing.Size(423, 353);
			this.tabPagePropertyGrid.TabIndex = 1;
			this.tabPagePropertyGrid.Text = "PropertyGrid";
			this.tabPagePropertyGrid.UseVisualStyleBackColor = true;
			// 
			// dataGridView1
			// 
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridView1.Location = new System.Drawing.Point(9, 7);
			this.dataGridView1.Name = "dataGridView1";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridView1.Size = new System.Drawing.Size(406, 340);
			this.dataGridView1.TabIndex = 0;
			// 
			// Column1
			// 
			this.Column1.HeaderText = "Column1";
			this.Column1.Name = "Column1";
			// 
			// Column2
			// 
			this.Column2.HeaderText = "Column2";
			this.Column2.Name = "Column2";
			// 
			// Column3
			// 
			this.Column3.HeaderText = "Column3";
			this.Column3.Name = "Column3";
			// 
			// Column4
			// 
			this.Column4.HeaderText = "Column4";
			this.Column4.Name = "Column4";
			// 
			// FahrstrassenEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(431, 403);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "FahrstrassenEditor";
			this.Text = "FahrstrassenEditor";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FahrstrassenEditor_FormClosing);
			this.Shown += new System.EventHandler(this.FahrstrassenEditor_Shown);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPageTabelle.ResumeLayout(false);
			this.tabPagePropertyGrid.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem alleFahrstraßenSuchenToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fahrstraßenSuchenToolStripMenuItem;
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPageTabelle;
		private System.Windows.Forms.TabPage tabPagePropertyGrid;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
		private System.Windows.Forms.DataGridViewButtonColumn Column2;
		private System.Windows.Forms.DataGridViewComboBoxColumn Column3;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
	}
}