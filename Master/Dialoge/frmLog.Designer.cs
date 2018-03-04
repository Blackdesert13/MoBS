namespace MoBaSteuerung.Dialoge
{
  partial class frmLog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLog));
      this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
      this.buttonOK = new System.Windows.Forms.Button();
      this.panelButton = new System.Windows.Forms.Panel();
      this.fileSystemWatcherLog = new System.IO.FileSystemWatcher();
      this.panelButton.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcherLog)).BeginInit();
      this.SuspendLayout();
      // 
      // richTextBoxLog
      // 
      this.richTextBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
      this.richTextBoxLog.Location = new System.Drawing.Point(0, 0);
      this.richTextBoxLog.Name = "richTextBoxLog";
      this.richTextBoxLog.ReadOnly = true;
      this.richTextBoxLog.Size = new System.Drawing.Size(451, 248);
      this.richTextBoxLog.TabIndex = 0;
      this.richTextBoxLog.Text = "";
      // 
      // buttonOK
      // 
      this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonOK.Location = new System.Drawing.Point(364, 13);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new System.Drawing.Size(75, 23);
      this.buttonOK.TabIndex = 1;
      this.buttonOK.Text = "OK";
      this.buttonOK.UseVisualStyleBackColor = true;
      this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
      // 
      // panelButton
      // 
      this.panelButton.Controls.Add(this.buttonOK);
      this.panelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panelButton.Location = new System.Drawing.Point(0, 248);
      this.panelButton.Name = "panelButton";
      this.panelButton.Size = new System.Drawing.Size(451, 48);
      this.panelButton.TabIndex = 2;
      // 
      // fileSystemWatcherLog
      // 
      this.fileSystemWatcherLog.EnableRaisingEvents = true;
      this.fileSystemWatcherLog.Filter = "*.log";
      this.fileSystemWatcherLog.SynchronizingObject = this;
      this.fileSystemWatcherLog.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcherLog_Changed);
      this.fileSystemWatcherLog.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcherLog_Created);
      // 
      // frmLog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(451, 296);
      this.Controls.Add(this.richTextBoxLog);
      this.Controls.Add(this.panelButton);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmLog";
      this.Text = "Modellbahnsteuerung Log Anzeige";
      this.panelButton.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcherLog)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.RichTextBox richTextBoxLog;
    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.Panel panelButton;
    private System.IO.FileSystemWatcher fileSystemWatcherLog;
  }
}