namespace FreelanceManager
{
  partial class frmStatistics
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
      System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
      this.panel1 = new System.Windows.Forms.Panel();
      this.tblData = new System.Windows.Forms.DataGridView();
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.panel2 = new System.Windows.Forms.Panel();
      this.chartMonth = new System.Windows.Forms.DataVisualization.Charting.Chart();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.tblData)).BeginInit();
      this.panel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.chartMonth)).BeginInit();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.tblData);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(180, 474);
      this.panel1.TabIndex = 0;
      // 
      // tblData
      // 
      this.tblData.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
      this.tblData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.tblData.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tblData.Location = new System.Drawing.Point(0, 0);
      this.tblData.Name = "tblData";
      this.tblData.ReadOnly = true;
      this.tblData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.tblData.Size = new System.Drawing.Size(180, 474);
      this.tblData.TabIndex = 0;
      // 
      // splitter1
      // 
      this.splitter1.Location = new System.Drawing.Point(180, 0);
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(3, 474);
      this.splitter1.TabIndex = 1;
      this.splitter1.TabStop = false;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.chartMonth);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(183, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(474, 474);
      this.panel2.TabIndex = 2;
      // 
      // chartMonth
      // 
      chartArea1.AxisX.Interval = 1D;
      chartArea1.Name = "areaMonth";
      this.chartMonth.ChartAreas.Add(chartArea1);
      this.chartMonth.Dock = System.Windows.Forms.DockStyle.Fill;
      this.chartMonth.Location = new System.Drawing.Point(0, 0);
      this.chartMonth.Name = "chartMonth";
      this.chartMonth.Size = new System.Drawing.Size(474, 474);
      this.chartMonth.TabIndex = 0;
      // 
      // frmStatistics
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(657, 474);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.splitter1);
      this.Controls.Add(this.panel1);
      this.Name = "frmStatistics";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Статистика";
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.tblData)).EndInit();
      this.panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.chartMonth)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Splitter splitter1;
    private System.Windows.Forms.Panel panel2;
    public System.Windows.Forms.DataGridView tblData;
    public System.Windows.Forms.DataVisualization.Charting.Chart chartMonth;
  }
}