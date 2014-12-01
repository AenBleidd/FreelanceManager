namespace FreelanceManager
{
  partial class frmProperties
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
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.edtDBPath = new System.Windows.Forms.TextBox();
      this.edtGoogleDriveRootPath = new System.Windows.Forms.TextBox();
      this.edtFreelanceDirectoryPath = new System.Windows.Forms.TextBox();
      this.btnDBPathOpen = new System.Windows.Forms.Button();
      this.btnGoogleDriveRootPathOpen = new System.Windows.Forms.Button();
      this.btnFreelanceDirectoryPathOpen = new System.Windows.Forms.Button();
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.btnCancel);
      this.panel1.Controls.Add(this.btnOk);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 82);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(528, 37);
      this.panel1.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(151, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Расположение базы данных";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 30);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(180, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Расположение папки Google Drive";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 51);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(165, 13);
      this.label3.TabIndex = 3;
      this.label3.Text = "Расположение папки Freelance";
      // 
      // edtDBPath
      // 
      this.edtDBPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.edtDBPath.Location = new System.Drawing.Point(200, 6);
      this.edtDBPath.Name = "edtDBPath";
      this.edtDBPath.Size = new System.Drawing.Size(235, 20);
      this.edtDBPath.TabIndex = 4;
      // 
      // edtGoogleDriveRootPath
      // 
      this.edtGoogleDriveRootPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.edtGoogleDriveRootPath.Location = new System.Drawing.Point(200, 27);
      this.edtGoogleDriveRootPath.Name = "edtGoogleDriveRootPath";
      this.edtGoogleDriveRootPath.Size = new System.Drawing.Size(235, 20);
      this.edtGoogleDriveRootPath.TabIndex = 5;
      // 
      // edtFreelanceDirectoryPath
      // 
      this.edtFreelanceDirectoryPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.edtFreelanceDirectoryPath.Location = new System.Drawing.Point(200, 48);
      this.edtFreelanceDirectoryPath.Name = "edtFreelanceDirectoryPath";
      this.edtFreelanceDirectoryPath.Size = new System.Drawing.Size(235, 20);
      this.edtFreelanceDirectoryPath.TabIndex = 6;
      // 
      // btnDBPathOpen
      // 
      this.btnDBPathOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnDBPathOpen.Location = new System.Drawing.Point(441, 6);
      this.btnDBPathOpen.Name = "btnDBPathOpen";
      this.btnDBPathOpen.Size = new System.Drawing.Size(75, 20);
      this.btnDBPathOpen.TabIndex = 7;
      this.btnDBPathOpen.Text = "Обзор";
      this.btnDBPathOpen.UseVisualStyleBackColor = true;
      this.btnDBPathOpen.Click += new System.EventHandler(this.btnDBPathOpen_Click);
      // 
      // btnGoogleDriveRootPathOpen
      // 
      this.btnGoogleDriveRootPathOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnGoogleDriveRootPathOpen.Location = new System.Drawing.Point(441, 27);
      this.btnGoogleDriveRootPathOpen.Name = "btnGoogleDriveRootPathOpen";
      this.btnGoogleDriveRootPathOpen.Size = new System.Drawing.Size(75, 20);
      this.btnGoogleDriveRootPathOpen.TabIndex = 8;
      this.btnGoogleDriveRootPathOpen.Text = "Обзор";
      this.btnGoogleDriveRootPathOpen.UseVisualStyleBackColor = true;
      this.btnGoogleDriveRootPathOpen.Click += new System.EventHandler(this.btnGoogleDriveRootPathOpen_Click);
      // 
      // btnFreelanceDirectoryPathOpen
      // 
      this.btnFreelanceDirectoryPathOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnFreelanceDirectoryPathOpen.Location = new System.Drawing.Point(441, 48);
      this.btnFreelanceDirectoryPathOpen.Name = "btnFreelanceDirectoryPathOpen";
      this.btnFreelanceDirectoryPathOpen.Size = new System.Drawing.Size(75, 20);
      this.btnFreelanceDirectoryPathOpen.TabIndex = 9;
      this.btnFreelanceDirectoryPathOpen.Text = "Обзор";
      this.btnFreelanceDirectoryPathOpen.UseVisualStyleBackColor = true;
      this.btnFreelanceDirectoryPathOpen.Click += new System.EventHandler(this.btnFreelanceDirectoryPathOpen_Click);
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.Location = new System.Drawing.Point(360, 7);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 23);
      this.btnOk.TabIndex = 0;
      this.btnOk.Text = "Сохранить";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(441, 7);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "Закрыть";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // frmProperties
      // 
      this.AcceptButton = this.btnOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(528, 119);
      this.Controls.Add(this.btnFreelanceDirectoryPathOpen);
      this.Controls.Add(this.btnGoogleDriveRootPathOpen);
      this.Controls.Add(this.btnDBPathOpen);
      this.Controls.Add(this.edtFreelanceDirectoryPath);
      this.Controls.Add(this.edtGoogleDriveRootPath);
      this.Controls.Add(this.edtDBPath);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.panel1);
      this.Name = "frmProperties";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Настройки";
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox edtDBPath;
    private System.Windows.Forms.TextBox edtGoogleDriveRootPath;
    private System.Windows.Forms.TextBox edtFreelanceDirectoryPath;
    private System.Windows.Forms.Button btnDBPathOpen;
    private System.Windows.Forms.Button btnGoogleDriveRootPathOpen;
    private System.Windows.Forms.Button btnFreelanceDirectoryPathOpen;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
  }
}