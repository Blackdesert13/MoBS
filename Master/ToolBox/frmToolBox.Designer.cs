namespace MoBaSteuerung.ToolBox
{
  partial class ToolBoxElemente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolBoxElemente));
            this.panel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.knoten = new ModellBahnSteuerung.ToolBox.FrmKnoten();
            this.gleis = new ModellBahnSteuerung.ToolBox.FrmGleis();
            this.weiche = new ModellBahnSteuerung.ToolBox.FrmWeiche();
            this.schalter = new ModellBahnSteuerung.ToolBox.FrmSchalter();
            this.entkuppler = new ModellBahnSteuerung.ToolBox.FrmEntkuppler();
            this.signal = new ModellBahnSteuerung.ToolBox.Signal();
            this.relaise = new ModellBahnSteuerung.ToolBox.Relaise();
            this.fss = new ModellBahnSteuerung.ToolBox.FrmFSS();
            this.rueckMeldePlatine = new ModellBahnSteuerung.ToolBox.RueckMeldung();
            this.panel.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.AutoScroll = true;
            this.panel.Controls.Add(this.tableLayoutPanel);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(5, 5);
            this.panel.Margin = new System.Windows.Forms.Padding(0);
            this.panel.Name = "panel";
            this.panel.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.panel.Size = new System.Drawing.Size(477, 820);
            this.panel.TabIndex = 4;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoSize = true;
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.knoten, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.gleis, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.weiche, 0, 5);
            this.tableLayoutPanel.Controls.Add(this.schalter, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.entkuppler, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.signal, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.relaise, 0, 7);
            this.tableLayoutPanel.Controls.Add(this.fss, 0, 6);
            this.tableLayoutPanel.Controls.Add(this.rueckMeldePlatine, 0, 8);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 9;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(455, 1343);
            this.tableLayoutPanel.TabIndex = 15;
            // 
            // knoten
            // 
            this.knoten.BackColor = System.Drawing.Color.White;
            this.knoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.knoten.Dock = System.Windows.Forms.DockStyle.Fill;
            this.knoten.Location = new System.Drawing.Point(0, 0);
            this.knoten.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.knoten.MinimumSize = new System.Drawing.Size(305, 26);
            this.knoten.Name = "knoten";
            this.knoten.Size = new System.Drawing.Size(455, 112);
            this.knoten.TabIndex = 5;
            this.knoten.SizeChanged += new System.EventHandler(this.Elemente_SizeChanged);
            // 
            // gleis
            // 
            this.gleis.BackColor = System.Drawing.Color.White;
            this.gleis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gleis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gleis.Location = new System.Drawing.Point(0, 117);
            this.gleis.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.gleis.MinimumSize = new System.Drawing.Size(206, 26);
            this.gleis.Name = "gleis";
            this.gleis.Size = new System.Drawing.Size(455, 112);
            this.gleis.TabIndex = 7;
            this.gleis.SizeChanged += new System.EventHandler(this.Elemente_SizeChanged);
            // 
            // weiche
            // 
            this.weiche.BackColor = System.Drawing.Color.White;
            this.weiche.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.weiche.Dock = System.Windows.Forms.DockStyle.Fill;
            this.weiche.Location = new System.Drawing.Point(0, 527);
            this.weiche.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.weiche.MinimumSize = new System.Drawing.Size(225, 26);
            this.weiche.Name = "weiche";
            this.weiche.Size = new System.Drawing.Size(455, 115);
            this.weiche.TabIndex = 14;
            this.weiche.SizeChanged += new System.EventHandler(this.Elemente_SizeChanged);
            // 
            // schalter
            // 
            this.schalter.BackColor = System.Drawing.Color.White;
            this.schalter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.schalter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schalter.Location = new System.Drawing.Point(0, 234);
            this.schalter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.schalter.MinimumSize = new System.Drawing.Size(262, 26);
            this.schalter.Name = "schalter";
            this.schalter.Size = new System.Drawing.Size(455, 92);
            this.schalter.TabIndex = 9;
            this.schalter.SizeChanged += new System.EventHandler(this.Elemente_SizeChanged);
            // 
            // entkuppler
            // 
            this.entkuppler.BackColor = System.Drawing.Color.White;
            this.entkuppler.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.entkuppler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entkuppler.Location = new System.Drawing.Point(0, 331);
            this.entkuppler.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.entkuppler.MinimumSize = new System.Drawing.Size(232, 26);
            this.entkuppler.Name = "entkuppler";
            this.entkuppler.Size = new System.Drawing.Size(455, 93);
            this.entkuppler.TabIndex = 10;
            this.entkuppler.SizeChanged += new System.EventHandler(this.Elemente_SizeChanged);
            // 
            // signal
            // 
            this.signal.BackColor = System.Drawing.Color.White;
            this.signal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.signal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.signal.Location = new System.Drawing.Point(0, 429);
            this.signal.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.signal.MinimumSize = new System.Drawing.Size(280, 26);
            this.signal.Name = "signal";
            this.signal.Size = new System.Drawing.Size(455, 93);
            this.signal.TabIndex = 12;
            this.signal.SizeChanged += new System.EventHandler(this.Elemente_SizeChanged);
            // 
            // relaise
            // 
            this.relaise.BackColor = System.Drawing.Color.White;
            this.relaise.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.relaise.Dock = System.Windows.Forms.DockStyle.Fill;
            this.relaise.Location = new System.Drawing.Point(0, 767);
            this.relaise.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.relaise.MinimumSize = new System.Drawing.Size(334, 26);
            this.relaise.Name = "relaise";
            this.relaise.Size = new System.Drawing.Size(455, 237);
            this.relaise.TabIndex = 16;
            this.relaise.SizeChanged += new System.EventHandler(this.Elemente_SizeChanged);
            // 
            // fss
            // 
            this.fss.BackColor = System.Drawing.Color.White;
            this.fss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fss.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fss.Location = new System.Drawing.Point(0, 647);
            this.fss.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.fss.MinimumSize = new System.Drawing.Size(268, 26);
            this.fss.Name = "fss";
            this.fss.Size = new System.Drawing.Size(455, 115);
            this.fss.TabIndex = 15;
            this.fss.SizeChanged += new System.EventHandler(this.Elemente_SizeChanged);
            // 
            // rueckMeldePlatine
            // 
            this.rueckMeldePlatine.AutoSize = true;
            this.rueckMeldePlatine.BackColor = System.Drawing.Color.White;
            this.rueckMeldePlatine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rueckMeldePlatine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rueckMeldePlatine.Location = new System.Drawing.Point(0, 1009);
            this.rueckMeldePlatine.Margin = new System.Windows.Forms.Padding(0);
            this.rueckMeldePlatine.MinimumSize = new System.Drawing.Size(334, 26);
            this.rueckMeldePlatine.Name = "rueckMeldePlatine";
            this.rueckMeldePlatine.Size = new System.Drawing.Size(455, 334);
            this.rueckMeldePlatine.TabIndex = 17;
            this.rueckMeldePlatine.SizeChanged += new System.EventHandler(this.Elemente_SizeChanged);
            // 
            // ToolBoxElemente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 830);
            this.Controls.Add(this.panel);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(410, 50);
            this.Name = "ToolBoxElemente";
            this.Padding = new System.Windows.Forms.Padding(5, 5, 0, 5);
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Elemente Eigenschaften";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmElemente_FormClosing);
            this.Shown += new System.EventHandler(this.ToolBoxElemente_Shown);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel;
    private ModellBahnSteuerung.ToolBox.FrmEntkuppler entkuppler;
    private ModellBahnSteuerung.ToolBox.Signal signal;
    private ModellBahnSteuerung.ToolBox.FrmWeiche weiche;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
    private ModellBahnSteuerung.ToolBox.Relaise relaise;
    private ModellBahnSteuerung.ToolBox.FrmSchalter schalter;
    private ModellBahnSteuerung.ToolBox.FrmGleis gleis;
    private ModellBahnSteuerung.ToolBox.FrmKnoten knoten;
        private ModellBahnSteuerung.ToolBox.FrmFSS fss;
		private ModellBahnSteuerung.ToolBox.RueckMeldung rueckMeldePlatine;
	}
}