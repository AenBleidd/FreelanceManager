using System;
using System.Data;
using System.Windows.Forms;

namespace FreelanceManager
{
  public partial class frmReference : Form
  {
    public frmReference(string ReferenceName, DataTable dt, bool readOnly = false, bool hasFilter = true)
    {
      InitializeComponent();
      this.Text = ReferenceName;
      pnlFilter.Visible = hasFilter;
      tblData.ReadOnly = readOnly;
      btnSave.Visible = !readOnly;      
      System.Collections.IEnumerator enumerator = dt.Columns.GetEnumerator();
      if (enumerator.MoveNext() == false)
      {
        throw new Exception("Table doesn't contain columns");
      }
      BindingSource bind = new BindingSource();
      bind.DataSource = dt;
      tblData.DataBindings.Add(new Binding("text", dt, ((System.Data.DataColumn)enumerator.Current).ColumnName));
      tblData.DataSource = bind;
      navData.BindingSource = bind;
    }

    public void gettblData(ref DataGridView tbl)
    {
      tbl = tblData;
    }
  }
}
