using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// SQLite
using System.Data.SQLite;

namespace FreelanceManager
{
  public partial class frmBill : Form
  {
    public fmDB db;
    private int id;
    public frmBill(fmDB _db)
    {
      InitializeComponent();
      db = _db;
    }

    private void cbSource_SelectedIndexChanged(object sender, EventArgs e)
    {
      SQLiteDataAdapter adapter = null;
      DataRow selectedDataRow = ((DataRowView)cbSource.SelectedItem).Row;
      id = Convert.ToInt32(selectedDataRow["idSource"]); 
      DataTable t = db.ExecuteBill(ref adapter, id);
      tblData.DataSource = t;

      foreach (DataGridViewColumn c in tblData.Columns)
      {
        if (c.Name == "TaskNumber")
        {
          c.HeaderText = "№ заказа";
          c.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
          c.ReadOnly = true;
        }
        else if (c.Name == "FirstDoneVersionDate")
        {
          c.HeaderText = "Дата сдачи первой версии";
          c.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
          c.ReadOnly = true;
        }
        else if (c.Name == "Cost")
        {
          c.HeaderText = "Стоимость";
          c.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
          c.ReadOnly = true;
        }
        else
        {
          c.Visible = false;
        }
      }

      DataGridViewCheckBoxColumn cbIsInclude = new DataGridViewCheckBoxColumn();
      cbIsInclude.HeaderText = "Включено";
      cbIsInclude.Name = "IsInclude";
      cbIsInclude.DataPropertyName = "isInclude";
      cbIsInclude.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      tblData.Columns.Add(cbIsInclude);

      

      btnSendMail.Enabled = false;
      btnCopyToClipboard.Enabled = false;
      btnCreateBill.Enabled = t.Rows.Count > 0;
      btnMarkAsPayed.Enabled = t.Rows.Count > 0;

      rtbBill.Clear();
    }

    private void btnCreateBill_Click(object sender, EventArgs e)
    {
      int summ = 0;
      rtbBill.Clear();
      //rtbBill.Font = new Font(rtbBill.Font, FontStyle.Bold);
      string s = "Заказ\tСтоимость\r\n";
      rtbBill.AppendText(s);
      foreach (DataGridViewRow r in tblData.Rows)
      {
        if (r.Cells["isInclude"].Value != DBNull.Value && Convert.ToBoolean(r.Cells["isInclude"].Value) == true)
        {
          s = tblData.Rows[r.Index].Cells["TaskNumber"].FormattedValue.ToString() + "\t" + tblData.Rows[r.Index].Cells["Cost"].FormattedValue.ToString() + "\r\n";
          rtbBill.AppendText(s);
          summ += Convert.ToInt32(tblData.Rows[r.Index].Cells["Cost"].Value);
        }
      }
      s = "Итого:\t" + Convert.ToString(summ);
      rtbBill.AppendText(s);
      rtbBill.Select(rtbBill.GetFirstCharIndexFromLine(0), rtbBill.Lines[0].Length);
      rtbBill.SelectionFont = new Font(rtbBill.Font, FontStyle.Bold);
      rtbBill.Select(rtbBill.GetFirstCharIndexFromLine(rtbBill.Lines.Count() - 1), rtbBill.Lines[rtbBill.Lines.Count() - 1].Length);
      rtbBill.SelectionFont = new Font(rtbBill.Font, FontStyle.Bold);

      DataTable tblSources = (DataTable)cbSource.DataSource;

      foreach (DataRow r in tblSources.Rows)
      {
        if (Convert.ToInt32(r["idSource"]) == id)
        {
          if (r["email"] != DBNull.Value && rtbBill.Lines.Count() > 0)
          {
            btnSendMail.Enabled = true;
          }
          break;
        }
      }

      if (rtbBill.Lines.Count() > 0)
      {
        btnCopyToClipboard.Enabled = true;
      }

    }

    private void btnCopyToClipboard_Click(object sender, EventArgs e)
    {
      rtbBill.SelectAll();
      rtbBill.Copy();
    }

    private void btnSendMail_Click(object sender, EventArgs e)
    {
      fmGMail gmail = new fmGMail();
      gmail.SendMessage("lestat.de.lionkur@gmail.com", "Some subject", "Some body");
    }

    private void btnMarkAsPayed_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Отметить все как оплаченные?", "Оплата", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
      {
        db.MarkAsPayed(id);
      }
    }
  }
}
