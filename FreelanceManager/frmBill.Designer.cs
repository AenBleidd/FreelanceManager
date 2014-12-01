namespace FreelanceManager
{
  partial class frmBill
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
      this.panel1 = new System.Windows.Forms.Panel();
      this.lblSource = new System.Windows.Forms.Label();
      this.cbSource = new System.Windows.Forms.ComboBox();
      this.panel2 = new System.Windows.Forms.Panel();
      this.tblData = new System.Windows.Forms.DataGridView();
      this.panel3 = new System.Windows.Forms.Panel();
      this.panel4 = new System.Windows.Forms.Panel();
      this.panel5 = new System.Windows.Forms.Panel();
      this.rtbBill = new System.Windows.Forms.RichTextBox();
      this.btnSendMail = new System.Windows.Forms.Button();
      this.btnCopyToClipboard = new System.Windows.Forms.Button();
      this.btnCreateBill = new System.Windows.Forms.Button();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.tblData)).BeginInit();
      this.panel3.SuspendLayout();
      this.panel4.SuspendLayout();
      this.panel5.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.lblSource);
      this.panel1.Controls.Add(this.cbSource);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(804, 40);
      this.panel1.TabIndex = 0;
      // 
      // lblSource
      // 
      this.lblSource.AutoSize = true;
      this.lblSource.Location = new System.Drawing.Point(3, 15);
      this.lblSource.Name = "lblSource";
      this.lblSource.Size = new System.Drawing.Size(55, 13);
      this.lblSource.TabIndex = 1;
      this.lblSource.Text = "Источник";
      // 
      // cbSource
      // 
      this.cbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbSource.FormattingEnabled = true;
      this.cbSource.Location = new System.Drawing.Point(64, 12);
      this.cbSource.Name = "cbSource";
      this.cbSource.Size = new System.Drawing.Size(227, 21);
      this.cbSource.TabIndex = 0;
      this.cbSource.SelectedIndexChanged += new System.EventHandler(this.cbSource_SelectedIndexChanged);
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.tblData);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(0, 40);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(804, 360);
      this.panel2.TabIndex = 1;
      // 
      // tblData
      // 
      this.tblData.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
      this.tblData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.tblData.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tblData.Location = new System.Drawing.Point(0, 0);
      this.tblData.Name = "tblData";
      this.tblData.Size = new System.Drawing.Size(804, 360);
      this.tblData.TabIndex = 0;
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.panel5);
      this.panel3.Controls.Add(this.panel4);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel3.Location = new System.Drawing.Point(0, 400);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(804, 141);
      this.panel3.TabIndex = 2;
      // 
      // panel4
      // 
      this.panel4.Controls.Add(this.btnCreateBill);
      this.panel4.Controls.Add(this.btnCopyToClipboard);
      this.panel4.Controls.Add(this.btnSendMail);
      this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
      this.panel4.Location = new System.Drawing.Point(626, 0);
      this.panel4.Name = "panel4";
      this.panel4.Size = new System.Drawing.Size(178, 141);
      this.panel4.TabIndex = 0;
      // 
      // panel5
      // 
      this.panel5.Controls.Add(this.rtbBill);
      this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel5.Location = new System.Drawing.Point(0, 0);
      this.panel5.Name = "panel5";
      this.panel5.Size = new System.Drawing.Size(626, 141);
      this.panel5.TabIndex = 1;
      // 
      // rtbBill
      // 
      this.rtbBill.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rtbBill.Location = new System.Drawing.Point(0, 0);
      this.rtbBill.Name = "rtbBill";
      this.rtbBill.ReadOnly = true;
      this.rtbBill.Size = new System.Drawing.Size(626, 141);
      this.rtbBill.TabIndex = 0;
      this.rtbBill.Text = "";
      // 
      // btnSendMail
      // 
      this.btnSendMail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSendMail.Location = new System.Drawing.Point(3, 82);
      this.btnSendMail.Name = "btnSendMail";
      this.btnSendMail.Size = new System.Drawing.Size(172, 23);
      this.btnSendMail.TabIndex = 0;
      this.btnSendMail.Text = "Отправить письмо";
      this.btnSendMail.UseVisualStyleBackColor = true;
      this.btnSendMail.Click += new System.EventHandler(this.btnSendMail_Click);
      // 
      // btnCopyToClipboard
      // 
      this.btnCopyToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCopyToClipboard.Location = new System.Drawing.Point(2, 111);
      this.btnCopyToClipboard.Name = "btnCopyToClipboard";
      this.btnCopyToClipboard.Size = new System.Drawing.Size(173, 23);
      this.btnCopyToClipboard.TabIndex = 1;
      this.btnCopyToClipboard.Text = "Скопировать в буфер обмена";
      this.btnCopyToClipboard.UseVisualStyleBackColor = true;
      this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
      // 
      // btnCreateBill
      // 
      this.btnCreateBill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCreateBill.Location = new System.Drawing.Point(3, 3);
      this.btnCreateBill.Name = "btnCreateBill";
      this.btnCreateBill.Size = new System.Drawing.Size(172, 23);
      this.btnCreateBill.TabIndex = 2;
      this.btnCreateBill.Text = "Сформировать счет";
      this.btnCreateBill.UseVisualStyleBackColor = true;
      this.btnCreateBill.Click += new System.EventHandler(this.btnCreateBill_Click);
      // 
      // frmBill
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(804, 541);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.panel3);
      this.Controls.Add(this.panel1);
      this.Name = "frmBill";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Выставить счет";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.tblData)).EndInit();
      this.panel3.ResumeLayout(false);
      this.panel4.ResumeLayout(false);
      this.panel5.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Label lblSource;
    public System.Windows.Forms.ComboBox cbSource;
    private System.Windows.Forms.DataGridView tblData;
    private System.Windows.Forms.Panel panel5;
    private System.Windows.Forms.Panel panel4;
    private System.Windows.Forms.RichTextBox rtbBill;
    private System.Windows.Forms.Button btnCopyToClipboard;
    private System.Windows.Forms.Button btnSendMail;
    private System.Windows.Forms.Button btnCreateBill;
  }
}