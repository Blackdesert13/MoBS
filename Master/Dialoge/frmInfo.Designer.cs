namespace MoBaSteuerung.Dialoge
{
  partial class frmInfo
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInfo));
      this.buttonOK = new System.Windows.Forms.Button();
      this.labelApp = new System.Windows.Forms.Label();
      this.labelVersion = new System.Windows.Forms.Label();
      this.labelCopyright = new System.Windows.Forms.Label();
      this.labelProgrammierer = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // buttonOK
      // 
      this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonOK.Location = new System.Drawing.Point(287, 141);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new System.Drawing.Size(75, 23);
      this.buttonOK.TabIndex = 0;
      this.buttonOK.Text = "OK";
      this.buttonOK.UseVisualStyleBackColor = true;
      this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
      // 
      // labelApp
      // 
      this.labelApp.Dock = System.Windows.Forms.DockStyle.Top;
      this.labelApp.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelApp.Location = new System.Drawing.Point(0, 0);
      this.labelApp.Margin = new System.Windows.Forms.Padding(3);
      this.labelApp.Name = "labelApp";
      this.labelApp.Size = new System.Drawing.Size(374, 44);
      this.labelApp.TabIndex = 1;
      this.labelApp.Text = "Modellbahnsteuerung";
      this.labelApp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // labelVersion
      // 
      this.labelVersion.Dock = System.Windows.Forms.DockStyle.Top;
      this.labelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelVersion.Location = new System.Drawing.Point(0, 44);
      this.labelVersion.Name = "labelVersion";
      this.labelVersion.Size = new System.Drawing.Size(374, 30);
      this.labelVersion.TabIndex = 2;
      this.labelVersion.Text = "4.0.0.0";
      this.labelVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // labelCopyright
      // 
      this.labelCopyright.Dock = System.Windows.Forms.DockStyle.Top;
      this.labelCopyright.Location = new System.Drawing.Point(0, 74);
      this.labelCopyright.Name = "labelCopyright";
      this.labelCopyright.Size = new System.Drawing.Size(374, 30);
      this.labelCopyright.TabIndex = 3;
      this.labelCopyright.Text = "Copyright © 2014-2016";
      this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // labelProgrammierer
      // 
      this.labelProgrammierer.Dock = System.Windows.Forms.DockStyle.Top;
      this.labelProgrammierer.Location = new System.Drawing.Point(0, 104);
      this.labelProgrammierer.Name = "labelProgrammierer";
      this.labelProgrammierer.Size = new System.Drawing.Size(374, 23);
      this.labelProgrammierer.TabIndex = 4;
      this.labelProgrammierer.Text = "Jürgen Muh, Christoph ..., Robert Muh und René Gäbler";
      this.labelProgrammierer.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // frmInfo
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(374, 176);
      this.Controls.Add(this.labelProgrammierer);
      this.Controls.Add(this.labelCopyright);
      this.Controls.Add(this.labelVersion);
      this.Controls.Add(this.labelApp);
      this.Controls.Add(this.buttonOK);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmInfo";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Info zu Modellbahnsteuerung";
      this.Load += new System.EventHandler(this.frmInfo_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.Label labelApp;
    private System.Windows.Forms.Label labelVersion;
    private System.Windows.Forms.Label labelCopyright;
    private System.Windows.Forms.Label labelProgrammierer;
  }
}