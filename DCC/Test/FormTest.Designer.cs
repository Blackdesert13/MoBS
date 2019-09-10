namespace Test
{
  partial class FormTest
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

    #region Vom Windows Form-Designer generierter Code

    /// <summary>
    /// Erforderliche Methode für die Designerunterstützung.
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTest));
      this.buttonUnitTest = new System.Windows.Forms.Button();
      this.groupBoxUnitTest = new System.Windows.Forms.GroupBox();
      this.buttonLeeren = new System.Windows.Forms.Button();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.buttonFahrregler = new System.Windows.Forms.Button();
      this.buttonZubehör = new System.Windows.Forms.Button();
      this.groupBoxUnitTest.SuspendLayout();
      this.SuspendLayout();
      // 
      // buttonUnitTest
      // 
      this.buttonUnitTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonUnitTest.Location = new System.Drawing.Point(260, 456);
      this.buttonUnitTest.Name = "buttonUnitTest";
      this.buttonUnitTest.Size = new System.Drawing.Size(75, 23);
      this.buttonUnitTest.TabIndex = 0;
      this.buttonUnitTest.Text = "AutoTest";
      this.buttonUnitTest.UseVisualStyleBackColor = true;
      this.buttonUnitTest.Click += new System.EventHandler(this.ButtonUnitTest_Click);
      // 
      // groupBoxUnitTest
      // 
      this.groupBoxUnitTest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBoxUnitTest.Controls.Add(this.buttonLeeren);
      this.groupBoxUnitTest.Controls.Add(this.richTextBox1);
      this.groupBoxUnitTest.Controls.Add(this.buttonUnitTest);
      this.groupBoxUnitTest.Location = new System.Drawing.Point(12, 57);
      this.groupBoxUnitTest.Name = "groupBoxUnitTest";
      this.groupBoxUnitTest.Size = new System.Drawing.Size(341, 485);
      this.groupBoxUnitTest.TabIndex = 1;
      this.groupBoxUnitTest.TabStop = false;
      this.groupBoxUnitTest.Text = "AutoTest";
      // 
      // buttonLeeren
      // 
      this.buttonLeeren.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.buttonLeeren.Location = new System.Drawing.Point(6, 456);
      this.buttonLeeren.Name = "buttonLeeren";
      this.buttonLeeren.Size = new System.Drawing.Size(75, 23);
      this.buttonLeeren.TabIndex = 2;
      this.buttonLeeren.Text = "leeren";
      this.buttonLeeren.UseVisualStyleBackColor = true;
      this.buttonLeeren.Click += new System.EventHandler(this.ButtonLeeren_Click);
      // 
      // richTextBox1
      // 
      this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.richTextBox1.Location = new System.Drawing.Point(6, 19);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.Size = new System.Drawing.Size(329, 431);
      this.richTextBox1.TabIndex = 1;
      this.richTextBox1.Text = "";
      // 
      // buttonFahrregler
      // 
      this.buttonFahrregler.Location = new System.Drawing.Point(12, 12);
      this.buttonFahrregler.Name = "buttonFahrregler";
      this.buttonFahrregler.Size = new System.Drawing.Size(90, 39);
      this.buttonFahrregler.TabIndex = 2;
      this.buttonFahrregler.Text = "Fahrregler";
      this.buttonFahrregler.UseVisualStyleBackColor = true;
      this.buttonFahrregler.Click += new System.EventHandler(this.ButtonFahrregler_Click);
      // 
      // buttonZubehör
      // 
      this.buttonZubehör.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonZubehör.Enabled = false;
      this.buttonZubehör.Location = new System.Drawing.Point(263, 12);
      this.buttonZubehör.Name = "buttonZubehör";
      this.buttonZubehör.Size = new System.Drawing.Size(90, 39);
      this.buttonZubehör.TabIndex = 3;
      this.buttonZubehör.Text = "Zubehör";
      this.buttonZubehör.UseVisualStyleBackColor = true;
      // 
      // FormTest
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(365, 554);
      this.Controls.Add(this.buttonZubehör);
      this.Controls.Add(this.buttonFahrregler);
      this.Controls.Add(this.groupBoxUnitTest);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FormTest";
      this.Text = "Test";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.groupBoxUnitTest.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button buttonUnitTest;
    private System.Windows.Forms.GroupBox groupBoxUnitTest;
    private System.Windows.Forms.Button buttonLeeren;
    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.Button buttonFahrregler;
    private System.Windows.Forms.Button buttonZubehör;
  }
}

