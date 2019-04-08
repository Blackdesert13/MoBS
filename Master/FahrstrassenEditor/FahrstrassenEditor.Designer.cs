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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.alleFahrstraßenSuchenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fahrstraßenSuchenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fahrstraßenSpeichernToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageTabelle = new System.Windows.Forms.TabPage();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.ColumnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnStartSignal = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnZielSignal = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnStartBefehle = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnEndBefehle = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tabPagePropertyGrid = new System.Windows.Forms.TabPage();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.menuStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPageTabelle.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.tabPagePropertyGrid.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alleFahrstraßenSuchenToolStripMenuItem,
            this.fahrstraßenSuchenToolStripMenuItem,
            this.fahrstraßenSpeichernToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(456, 24);
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
			this.fahrstraßenSuchenToolStripMenuItem.Click += new System.EventHandler(this.fahrstraßenSuchenToolStripMenuItem_Click);
			// 
			// fahrstraßenSpeichernToolStripMenuItem
			// 
			this.fahrstraßenSpeichernToolStripMenuItem.Name = "fahrstraßenSpeichernToolStripMenuItem";
			this.fahrstraßenSpeichernToolStripMenuItem.Size = new System.Drawing.Size(136, 20);
			this.fahrstraßenSpeichernToolStripMenuItem.Text = "Fahrstraßen Speichern";
			this.fahrstraßenSpeichernToolStripMenuItem.Click += new System.EventHandler(this.fahrstraßenSpeichernToolStripMenuItem_Click);
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
			this.tabControl1.Size = new System.Drawing.Size(456, 379);
			this.tabControl1.TabIndex = 2;
			// 
			// tabPageTabelle
			// 
			this.tabPageTabelle.Controls.Add(this.textBox1);
			this.tabPageTabelle.Controls.Add(this.numericUpDown1);
			this.tabPageTabelle.Controls.Add(this.label1);
			this.tabPageTabelle.Controls.Add(this.comboBox1);
			this.tabPageTabelle.Controls.Add(this.dataGridView1);
			this.tabPageTabelle.Location = new System.Drawing.Point(4, 4);
			this.tabPageTabelle.Name = "tabPageTabelle";
			this.tabPageTabelle.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageTabelle.Size = new System.Drawing.Size(448, 353);
			this.tabPageTabelle.TabIndex = 0;
			this.tabPageTabelle.Text = "TabellenAnsicht";
			this.tabPageTabelle.UseVisualStyleBackColor = true;
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(160, 7);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(66, 20);
			this.numericUpDown1.TabIndex = 3;
			this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(13, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 15);
			this.label1.TabIndex = 2;
			this.label1.Text = "Filter:";
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
            "kein Filter",
            "Start-Signal",
            "Ziel-Signal"});
			this.comboBox1.Location = new System.Drawing.Point(63, 6);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(80, 21);
			this.comboBox1.TabIndex = 1;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToResizeRows = false;
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
            this.ColumnId,
            this.ColumnStartSignal,
            this.ColumnZielSignal,
            this.ColumnStartBefehle,
            this.ColumnEndBefehle});
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridView1.Location = new System.Drawing.Point(9, 33);
			this.dataGridView1.Name = "dataGridView1";
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
			this.dataGridView1.Size = new System.Drawing.Size(431, 314);
			this.dataGridView1.TabIndex = 0;
			this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
			this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
			// 
			// ColumnId
			// 
			this.ColumnId.DataPropertyName = "ID";
			this.ColumnId.HeaderText = "ID";
			this.ColumnId.Name = "ColumnId";
			this.ColumnId.ReadOnly = true;
			this.ColumnId.Width = 40;
			// 
			// ColumnStartSignal
			// 
			this.ColumnStartSignal.DataPropertyName = "Start";
			this.ColumnStartSignal.HeaderText = "Start";
			this.ColumnStartSignal.Name = "ColumnStartSignal";
			this.ColumnStartSignal.ReadOnly = true;
			this.ColumnStartSignal.Width = 50;
			// 
			// ColumnZielSignal
			// 
			this.ColumnZielSignal.DataPropertyName = "Ziel";
			this.ColumnZielSignal.HeaderText = "Ziel";
			this.ColumnZielSignal.Name = "ColumnZielSignal";
			this.ColumnZielSignal.ReadOnly = true;
			this.ColumnZielSignal.Width = 50;
			// 
			// ColumnStartBefehle
			// 
			this.ColumnStartBefehle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColumnStartBefehle.DataPropertyName = "Start-Befehle";
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ColumnStartBefehle.DefaultCellStyle = dataGridViewCellStyle2;
			this.ColumnStartBefehle.HeaderText = "Start-Befehle";
			this.ColumnStartBefehle.MinimumWidth = 75;
			this.ColumnStartBefehle.Name = "ColumnStartBefehle";
			// 
			// ColumnEndBefehle
			// 
			this.ColumnEndBefehle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColumnEndBefehle.DataPropertyName = "End-Befehle";
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ColumnEndBefehle.DefaultCellStyle = dataGridViewCellStyle3;
			this.ColumnEndBefehle.HeaderText = "End-Befehle";
			this.ColumnEndBefehle.MinimumWidth = 75;
			this.ColumnEndBefehle.Name = "ColumnEndBefehle";
			// 
			// tabPagePropertyGrid
			// 
			this.tabPagePropertyGrid.Controls.Add(this.propertyGrid1);
			this.tabPagePropertyGrid.Location = new System.Drawing.Point(4, 4);
			this.tabPagePropertyGrid.Name = "tabPagePropertyGrid";
			this.tabPagePropertyGrid.Padding = new System.Windows.Forms.Padding(3);
			this.tabPagePropertyGrid.Size = new System.Drawing.Size(448, 353);
			this.tabPagePropertyGrid.TabIndex = 1;
			this.tabPagePropertyGrid.Text = "PropertyGrid";
			this.tabPagePropertyGrid.UseVisualStyleBackColor = true;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(317, 6);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(100, 20);
			this.textBox1.TabIndex = 4;
			// 
			// FahrstrassenEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(456, 403);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "FahrstrassenEditor";
			this.Text = "FahrstrassenEditor";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FahrstrassenEditor_FormClosing);
			this.Shown += new System.EventHandler(this.FahrstrassenEditor_Shown);
			this.VisibleChanged += new System.EventHandler(this.FahrstrassenEditor_VisibleChanged);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPageTabelle.ResumeLayout(false);
			this.tabPageTabelle.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.tabPagePropertyGrid.ResumeLayout(false);
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
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnId;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStartSignal;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnZielSignal;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStartBefehle;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEndBefehle;
		private System.Windows.Forms.ToolStripMenuItem fahrstraßenSpeichernToolStripMenuItem;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.TextBox textBox1;
	}
}