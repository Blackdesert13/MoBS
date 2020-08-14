namespace ModellBahnSteuerung.FahrstrassenEditor {
	partial class frmKombiFahrstrasse {
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
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.comboBoxZielSignal = new System.Windows.Forms.ComboBox();
			this.comboBoxStartSignal = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.checkedComboBox2 = new CheckComboBoxTest.CheckedComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.checkedComboBox1 = new CheckComboBoxTest.CheckedComboBox();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.Location = new System.Drawing.Point(12, 394);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 1;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(117, 394);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "Abbrechen";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.74359F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.25641F));
			this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 6);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 382F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(531, 382);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// panel2
			// 
			this.panel2.AutoScroll = true;
			this.panel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(267, 3);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(261, 376);
			this.panel2.TabIndex = 1;
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.panel1.Controls.Add(this.comboBoxZielSignal);
			this.panel1.Controls.Add(this.comboBoxStartSignal);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.checkedComboBox2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.checkedComboBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(258, 376);
			this.panel1.TabIndex = 0;
			// 
			// comboBoxZielSignal
			// 
			this.comboBoxZielSignal.FormattingEnabled = true;
			this.comboBoxZielSignal.Location = new System.Drawing.Point(94, 146);
			this.comboBoxZielSignal.Name = "comboBoxZielSignal";
			this.comboBoxZielSignal.Size = new System.Drawing.Size(151, 21);
			this.comboBoxZielSignal.TabIndex = 2;
			this.comboBoxZielSignal.SelectedValueChanged += new System.EventHandler(this.comboBoxZielSignal_SelectedValueChanged);
			// 
			// comboBoxStartSignal
			// 
			this.comboBoxStartSignal.FormattingEnabled = true;
			this.comboBoxStartSignal.Location = new System.Drawing.Point(94, 119);
			this.comboBoxStartSignal.Name = "comboBoxStartSignal";
			this.comboBoxStartSignal.Size = new System.Drawing.Size(151, 21);
			this.comboBoxStartSignal.TabIndex = 2;
			this.comboBoxStartSignal.SelectedValueChanged += new System.EventHandler(this.comboBoxStartSignal_SelectedValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(18, 149);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Ziel-Signal";
			// 
			// checkedComboBox2
			// 
			this.checkedComboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkedComboBox2.CheckOnClick = true;
			this.checkedComboBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.checkedComboBox2.DropDownHeight = 1;
			this.checkedComboBox2.Enabled = false;
			this.checkedComboBox2.FormattingEnabled = true;
			this.checkedComboBox2.IntegralHeight = false;
			this.checkedComboBox2.Location = new System.Drawing.Point(94, 173);
			this.checkedComboBox2.Name = "checkedComboBox2";
			this.checkedComboBox2.Size = new System.Drawing.Size(151, 21);
			this.checkedComboBox2.TabIndex = 0;
			this.checkedComboBox2.Text = "Start-Ziel-Signal auswählen";
			this.checkedComboBox2.ValueSeparator = ", ";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(18, 122);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(61, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Start-Signal";
			// 
			// checkedComboBox1
			// 
			this.checkedComboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkedComboBox1.CheckOnClick = true;
			this.checkedComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.checkedComboBox1.DropDownHeight = 1;
			this.checkedComboBox1.Enabled = false;
			this.checkedComboBox1.FormattingEnabled = true;
			this.checkedComboBox1.IntegralHeight = false;
			this.checkedComboBox1.Location = new System.Drawing.Point(94, 92);
			this.checkedComboBox1.Name = "checkedComboBox1";
			this.checkedComboBox1.Size = new System.Drawing.Size(151, 21);
			this.checkedComboBox1.TabIndex = 0;
			this.checkedComboBox1.Text = "Start-Ziel-Signal auswählen";
			this.checkedComboBox1.ValueSeparator = ", ";
			// 
			// frmKombiFahrstrasse
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(539, 423);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmKombiFahrstrasse";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Kombi-Fahrstrasse";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmArduino_FormClosed);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion


		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label2;
		private CheckComboBoxTest.CheckedComboBox checkedComboBox2;
		private System.Windows.Forms.Label label1;
		private CheckComboBoxTest.CheckedComboBox checkedComboBox1;
		private System.Windows.Forms.ComboBox comboBoxZielSignal;
		private System.Windows.Forms.ComboBox comboBoxStartSignal;
	}
}