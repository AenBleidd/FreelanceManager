using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

// SQLite
using System.Data.SQLite;

// CSharp
using SevenZip;

using FreelanceManager.Database;
using FreelanceManager.Properties;

namespace FreelanceManager
{
  public partial class frmMain : Form
  {    
    fmProperties properties = null;
    fmGoogleDrive googledrive = null;
    fmDB db = null;

    SQLiteDataAdapter adapterTasks = null;
    DataTable tableTasks = null;
    SQLiteDataAdapter adapterLinks = null;
    DataTable tableLinks = null;

    DataGridView tempTable = null;

    private DateTimePicker cellDateTimePickerTasks;

    public frmMain()
    {
      InitializeComponent();      
      properties = new fmProperties();
      properties.Load();
      googledrive = new fmGoogleDrive();
      if (!googledrive.GoogleDriveConnect())
      {
        MessageBox.Show("Не удалось соединиться с Google Drive. Программа работает в автономном режиме", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      db = new fmDB();
      db.setProperties(properties);
      db.Connect();
      if (CreateDBIfNotExists() == false)
      {
        MessageBox.Show("Ошибка работы с базой данных. Дальнейшая работа невозможна", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        Environment.Exit(0);
      }
      if (Environment.Is64BitProcess == true)
      {
        Text += " (64-bit)";
      }
      else
      {
        Text += " (32-bit)";
      }
    }    

    private void menuExit_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void menuProperties_Click(object sender, EventArgs e)
    {
      properties.showPropertiesWindow();
      if (db != null)
      {
        db.ReConnect();
      }
    }

    private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (db != null)
      {
        db.CloseConnection();
      }
    }

    private bool CreateDBIfNotExists()
    {
      if (db == null)
      {
        throw new Exception("fmDB is not assigned!");
      }
      return db.ExecuteNonQuery(Properties.Resources.dbCreate);
    }

    private void menuReferenceSources_Click(object sender, EventArgs e)
    {
      if (db == null)
      {
        throw new Exception("fmDB is not assigned!");
      }
      SQLiteDataAdapter adapter = null;
      DataTable table = db.ExecuteReferenceSources(ref adapter);
      frmReference frm = new frmReference("Источники", table, false, false);
      DataGridView tblData = null;
      frm.gettblData(ref tblData);

      foreach (DataGridViewColumn c in tblData.Columns)
      {
        if (c.Name == "SourceName")
        {
          c.HeaderText = "Наименование";
          c.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        else if (c.Name == "email")
        {
          c.HeaderText = "EMail";
          c.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        else
        {
          c.Visible = false;
        }
      }

      DataGridViewButtonColumn btnColor = new DataGridViewButtonColumn();
      btnColor.HeaderText = "Цвет";
      btnColor.Name = "btnColor";
      btnColor.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      tblData.Columns.Add(btnColor);
      tempTable = tblData;
      tblData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(tblData_drawColorButtonSources_CellClick);
      tblData.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(tblData_drawColorButtonSources_CellPainting);

      DataGridViewCheckBoxColumn cbIsVisible = new DataGridViewCheckBoxColumn();
      cbIsVisible.HeaderText = "Включено";
      cbIsVisible.Name = "cbIsVisible";
      cbIsVisible.DataPropertyName = "isVisible";
      cbIsVisible.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      tblData.Columns.Add(cbIsVisible);

      if (frm.ShowDialog() == DialogResult.OK)
      {
        adapter.Update(table);
        if (tableTasks != null && adapterTasks != null)
        {
          tableTasks.Clear();
          adapterTasks.Fill(tableTasks);
        }
      }
      frm.Dispose();
    }

    private void menuReferenceStatuses_Click(object sender, EventArgs e)
    {
      if (db == null)
      {
        throw new Exception("fmDB is not assigned!");
      }
      SQLiteDataAdapter adapter = null;
      DataTable table = db.ExecuteReferenceStatuses(ref adapter);
      frmReference frm = new frmReference("Статусы", table, false, false);
      DataGridView tblData = null;
      frm.gettblData(ref tblData);

      foreach (DataGridViewColumn c in tblData.Columns)
      {
        if (c.Name == "StatusName")
        {
          c.HeaderText = "Наименование";
          c.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        else
        {
          c.Visible = false;
        }
      }

      DataGridViewButtonColumn btnColor = new DataGridViewButtonColumn();
      btnColor.HeaderText = "Цвет";
      btnColor.Name = "btnColor";
      btnColor.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      tblData.Columns.Add(btnColor);
      tempTable = tblData;
      tblData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(tblData_drawColorButton_CellClick);
      tblData.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(tblData_drawColorButton_CellPainting);

      DataGridViewCheckBoxColumn cbIsVisible = new DataGridViewCheckBoxColumn();
      cbIsVisible.HeaderText = "Включено";
      cbIsVisible.Name = "cbIsVisible";
      cbIsVisible.DataPropertyName = "isVisible";
      cbIsVisible.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      tblData.Columns.Add(cbIsVisible);

      if (frm.ShowDialog() == DialogResult.OK)
      {
        adapter.Update(table);
        if (tableTasks != null && adapterTasks != null)
        {
          tableTasks.Clear();
          adapterTasks.Fill(tableTasks);
        }
      }
      frm.Dispose();
    }

    private void tblData_drawColorButton_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
    {
      if (e.RowIndex != -1)
      {
        object value = tempTable.Rows[e.RowIndex].Cells["StatusColor"].Value;
        if (value == null) return;
        string str = value.ToString();
        if (str != "")
        {
          e.CellStyle.BackColor = Color.FromArgb(Convert.ToInt32(str));
        }
      }
    }

    private void tblData_drawColorButtonSources_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
    {
      if (e.RowIndex != -1)
      {
        object value = tempTable.Rows[e.RowIndex].Cells["SourceColor"].Value;
        if (value == null) return;
        string str = value.ToString();
        if (str != "")
        {
          e.CellStyle.BackColor = Color.FromArgb(Convert.ToInt32(str));
        }
      }
    }

    private void tblData_drawColorButton_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      ColorDialog dlg = new ColorDialog();
      if (e.RowIndex != -1 && tempTable.Rows[e.RowIndex].Cells["StatusColor"].Value != DBNull.Value)
      {
        object value = tempTable.Rows[e.RowIndex].Cells["StatusColor"].Value;
        if (value == null) return;
        string str = value.ToString();
        if (str != "")
        {
          dlg.Color = Color.FromArgb(Convert.ToInt32(str));
        }
      }
      if (e.ColumnIndex == tempTable.Columns["btnColor"].Index && e.RowIndex != -1)
      {
        if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
          tempTable.Rows[e.RowIndex].Cells["StatusColor"].Value = dlg.Color.ToArgb();
          tempTable.Invalidate();
        }
      }
    }

    private void tblData_drawColorButtonSources_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      ColorDialog dlg = new ColorDialog();
      if (e.RowIndex != -1 && tempTable.Rows[e.RowIndex].Cells["SourceColor"].Value != DBNull.Value)
      {
        object value = tempTable.Rows[e.RowIndex].Cells["SourceColor"].Value;
        if (value == null) return;
        string str = value.ToString();
        if (str != "")
        {
          dlg.Color = Color.FromArgb(Convert.ToInt32(str));
        }
      }
      if (e.ColumnIndex == tempTable.Columns["btnColor"].Index && e.RowIndex != -1)
      {
        if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
          tempTable.Rows[e.RowIndex].Cells["SourceColor"].Value = dlg.Color.ToArgb();
          tempTable.Invalidate();
        }
      }
    }

    private void menuReferenceLanguages_Click(object sender, EventArgs e)
    {
      if (db == null)
      {
        throw new Exception("fmDB is not assigned!");
      }
      SQLiteDataAdapter adapter = null;
      DataTable table = db.ExecuteReferenceLanguages(ref adapter);
      frmReference frm = new frmReference("Языки", table, false, false);
      DataGridView tblData = null;
      frm.gettblData(ref tblData);

      foreach (DataGridViewColumn c in tblData.Columns)
      {
        if (c.Name == "LanguageName")
        {
          c.HeaderText = "Наименование";
          c.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        else
        {
          c.Visible = false;
        }
      }

      if (frm.ShowDialog() == DialogResult.OK)
      {
        adapter.Update(table);
        ShowTasks();
      }
      frm.Dispose();
    }

    private void ShowTasks()
    {
      if (db == null)
      {
        throw new Exception("fmDB is not assigned!");
      }   

      tableTasks = db.ExecuteMainTasks(ref adapterTasks);

      BindingSource bindTasks = new BindingSource();
      bindTasks.DataSource = tableTasks;
      tblTasks.DataSource = bindTasks;
      navTasks.BindingSource = bindTasks;

      if (tblTasks.Columns.Count == 0)
      {
        cellDateTimePickerTasks = new DateTimePicker();
        tblTasks.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(cellDateTimePickerTasks_CellBeginEdit);
        tblTasks.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(cellDateTimePickerTasks_CellEndEdit);
        cellDateTimePickerTasks.ValueChanged += new EventHandler(cellDateTimePickerTasks_ValueChanged);
        cellDateTimePickerTasks.Visible = false;
        tblTasks.Controls.Add(cellDateTimePickerTasks);

        DataGridViewTextBoxColumn colIdTask = new DataGridViewTextBoxColumn();
        colIdTask.DataPropertyName = "idTask";
        colIdTask.Name = "idTask";
        colIdTask.HeaderText = "ID задания";
        colIdTask.ReadOnly = true;

        tblTasks.Columns.Add(colIdTask);

        DataGridViewComboBoxColumn colSource = new DataGridViewComboBoxColumn();
        SQLiteDataAdapter adapterSource = null;
        colSource.DataSource = db.ExecuteReferenceSources(ref adapterSource);
        colSource.DisplayMember = "SourceName";
        colSource.ValueMember = "idSource";
        colSource.DataPropertyName = "idSource";
        colSource.Name = "SourceName";
        colSource.HeaderText = "Источник";
        tblTasks.Columns.Add(colSource);

        DataGridViewTextBoxColumn colNumber = new DataGridViewTextBoxColumn();
        colNumber.DataPropertyName = "TaskNumber";
        colNumber.Name = "TaskNumber";
        colNumber.HeaderText = "№ заказа";
        colNumber.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        tblTasks.Columns.Add(colNumber);

        DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn();
        colName.DataPropertyName = "TaskName";
        colName.Name = "TaskName";
        colName.HeaderText = "Наименование";
        colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        tblTasks.Columns.Add(colName);

        DataGridViewComboBoxColumn colLanguage = new DataGridViewComboBoxColumn();
        SQLiteDataAdapter adapterLanguage = null;
        colLanguage.DataSource = db.ExecuteReferenceLanguages(ref adapterLanguage);
        colLanguage.DisplayMember = "LanguageName";
        colLanguage.ValueMember = "idLanguage";
        colLanguage.DataPropertyName = "idLanguage";
        colLanguage.Name = "LanguageName";
        colLanguage.HeaderText = "Язык";
        tblTasks.Columns.Add(colLanguage);

        DataGridViewTextBoxColumn colSubtaskCount = new DataGridViewTextBoxColumn();
        colSubtaskCount.DataPropertyName = "SubtaskCount";
        colSubtaskCount.Name = "SubtaskCount";
        colSubtaskCount.HeaderText = "Кол-во заданий";
        colSubtaskCount.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        tblTasks.Columns.Add(colSubtaskCount);

        DataGridViewTextBoxColumn colSubtaskNumber = new DataGridViewTextBoxColumn();
        colSubtaskNumber.DataPropertyName = "SubtaskNumber";
        colSubtaskNumber.Name = "SubtaskNumber";
        colSubtaskNumber.HeaderText = "№ задания";
        colSubtaskNumber.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        tblTasks.Columns.Add(colSubtaskNumber);

        DataGridViewTextBoxColumn colTaskReceiveDate = new DataGridViewTextBoxColumn();
        colTaskReceiveDate.DataPropertyName = "TaskReceiveDate";
        colTaskReceiveDate.Name = "TaskReceiveDate";
        colTaskReceiveDate.HeaderText = "Дата выдачи задания";
        colTaskReceiveDate.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        tblTasks.Columns.Add(colTaskReceiveDate);

        DataGridViewTextBoxColumn colTaskDeadlineDate = new DataGridViewTextBoxColumn();
        colTaskDeadlineDate.DataPropertyName = "TaskDeadlineDate";
        colTaskDeadlineDate.Name = "TaskDeadlineDate";
        colTaskDeadlineDate.HeaderText = "Конечный срок выполнения";
        colTaskDeadlineDate.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        tblTasks.Columns.Add(colTaskDeadlineDate);

        DataGridViewTextBoxColumn colFirstDoneVersionDate = new DataGridViewTextBoxColumn();
        colFirstDoneVersionDate.DataPropertyName = "FirstDoneVersionDate";
        colFirstDoneVersionDate.Name = "FirstDoneVersionDate";
        colFirstDoneVersionDate.HeaderText = "Дата сдачи первой версии";
        colFirstDoneVersionDate.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        tblTasks.Columns.Add(colFirstDoneVersionDate);

        DataGridViewTextBoxColumn colPeriod = new DataGridViewTextBoxColumn();
        colPeriod.DataPropertyName = "Period";
        colPeriod.Name = "Period";
        colPeriod.HeaderText = "Период";
        colPeriod.ReadOnly = true;
        colPeriod.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        tblTasks.Columns.Add(colPeriod);

        DataGridViewTextBoxColumn colCost = new DataGridViewTextBoxColumn();
        colCost.DataPropertyName = "Cost";
        colCost.Name = "Cost";
        colCost.HeaderText = "Стоимость";
        colCost.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        tblTasks.Columns.Add(colCost);

        DataGridViewComboBoxColumn colStatus = new DataGridViewComboBoxColumn();
        SQLiteDataAdapter adapterStatus = null;
        colStatus.DataSource = db.ExecuteReferenceStatuses(ref adapterStatus);
        colStatus.DisplayMember = "StatusName";
        colStatus.ValueMember = "idStatus";
        colStatus.DataPropertyName = "idStatus";
        colStatus.Name = "StatusName";
        colStatus.HeaderText = "Статус";
        tblTasks.Columns.Add(colStatus);

        DataGridViewTextBoxColumn colRemark = new DataGridViewTextBoxColumn();
        colRemark.DataPropertyName = "Remark";
        colRemark.Name = "Remark";
        colRemark.HeaderText = "Примечание";
        colRemark.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        tblTasks.Columns.Add(colRemark);

        DataGridViewTextBoxColumn colColor = new DataGridViewTextBoxColumn();
        colColor.DataPropertyName = "StatusColor";
        colColor.Name = "StatusColor";
        colColor.HeaderText = "Цвет";
        colColor.Visible = false;
        tblTasks.Columns.Add(colColor);
      }

      foreach (DataGridViewColumn c in tblTasks.Columns)
      {
        if (c.Name == "idTask")
        {
          c.Visible = false;
        }
      }

      if (tblTasks.DataBindings.Count == 0)
      {
        System.Collections.IEnumerator enumerator = tableTasks.Columns.GetEnumerator();
        if (enumerator.MoveNext() == false)
        {
          throw new Exception("Table doesn't contain columns");
        }
        tblTasks.DataBindings.Add(new Binding("text", tableTasks, ((System.Data.DataColumn)enumerator.Current).ColumnName));
      }
      ShowTotalUnpaid();
    }

    private void ShowTotalUnpaid()
    {
      if (db == null)
      {
        throw new Exception("fmDB is not assigned!");
      } 

      SQLiteDataAdapter adapter = null;

      string tu = db.GetTotalUnpaid(ref adapter);

      lblTotalUnpaid.Text = "К оплате: " + tu + " р.";
    }

    private void ShowLinks(object idTask)
    {
      if (db == null)
      {
        throw new Exception("fmDB is not assigned!");
      }      

      Int32 _idTask = (idTask == DBNull.Value ? -1 : Convert.ToInt32(idTask));

      tableLinks = db.ExecuteMainLinks(ref adapterLinks, _idTask);

      BindingSource bindLinks = new BindingSource();
      bindLinks.DataSource = tableLinks;
      tblLinks.DataSource = bindLinks;
      navLinks.BindingSource = bindLinks;

      if (tblLinks.Columns.Count == 0)
      {
        DataGridViewTextBoxColumn colIdLink = new DataGridViewTextBoxColumn();
        colIdLink.DataPropertyName = "idLink";
        colIdLink.Name = "idLink";
        colIdLink.HeaderText = "ID вложения";
        colIdLink.Width = 0;
        colIdLink.ReadOnly = true;
        tblLinks.Columns.Add(colIdLink);

        DataGridViewTextBoxColumn colIdTask = new DataGridViewTextBoxColumn();
        colIdTask.DataPropertyName = "idTask";
        colIdTask.Name = "idTask";
        colIdTask.HeaderText = "ID задания";
        colIdTask.Width = 0;
        colIdTask.ReadOnly = true;
        tblLinks.Columns.Add(colIdTask);

        DataGridViewTextBoxColumn colLinkName = new DataGridViewTextBoxColumn();
        colLinkName.DataPropertyName = "LinkName";
        colLinkName.Name = "LinkName";
        colLinkName.HeaderText = "Наименование";
        tblLinks.Columns.Add(colLinkName);

        DataGridViewTextBoxColumn colLink = new DataGridViewTextBoxColumn();
        colLink.DataPropertyName = "Link";
        colLink.Name = "Link";
        colLink.HeaderText = "Ссылка";
        tblLinks.Columns.Add(colLink);
      }

      foreach (DataGridViewColumn c in tblLinks.Columns)
      {
        if (c.Name == "idLink")
        {
          c.Visible = false;
        }
        else if (c.Name == "idTask")
        {
          c.Visible = false;
        }
      }

      if (tblLinks.DataBindings.Count == 0)
      {
        System.Collections.IEnumerator enumerator = tableLinks.Columns.GetEnumerator();
        if (enumerator.MoveNext() == false)
        {
          throw new Exception("Table doesn't contain columns");
        }
        tblLinks.DataBindings.Add(new Binding("text", tableLinks, ((System.Data.DataColumn)enumerator.Current).ColumnName));
      }
    }

    private void frmMain_Load(object sender, EventArgs e)
    {
      this.tblTasks.AutoGenerateColumns = false;
      this.tblLinks.AutoGenerateColumns = false;
    }

    private void cellDateTimePickerTasks_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
    {
      if ((e.ColumnIndex == tblTasks.Columns["TaskReceiveDate"].Index || e.ColumnIndex == tblTasks.Columns["TaskDeadlineDate"].Index || e.ColumnIndex == tblTasks.Columns["FirstDoneVersionDate"].Index) && e.RowIndex != -1)
      {
        Rectangle tempRect = tblTasks.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
        cellDateTimePickerTasks.Location = tempRect.Location;
        cellDateTimePickerTasks.Width = tempRect.Width;
        try
        {
          cellDateTimePickerTasks.Value = DateTime.Parse(tblTasks.CurrentCell.Value.ToString());
        }
        catch
        {
          cellDateTimePickerTasks.Value = DateTime.Now;
        }
        cellDateTimePickerTasks.Visible = true;
      }
    }

    private void cellDateTimePickerTasks_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
      cellDateTimePickerTasks.Visible = false;
    }

    private void cellDateTimePickerTasks_ValueChanged(object sender, EventArgs e)
    {
      tblTasks.CurrentCell.Value = cellDateTimePickerTasks.Value.ToString("dd/MM/yyyy");
    }

    private void tblTasks_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
    {
      if (e.RowIndex != -1)
      {
        object value = tblTasks.Rows[e.RowIndex].Cells["StatusColor"].Value;
        if (value == null) return;
        string str = value.ToString();
        if (str != "")
        {
          e.CellStyle.BackColor = Color.FromArgb(Convert.ToInt32(str));
        }
      }
    }

    private void SaveTasks()
    {
      if (adapterTasks != null && tableTasks != null)
      {
        try
        {
          adapterTasks.Update(tableTasks);
        }
        catch { }
      }
    }

    private void SaveLinks()
    {
      SaveTasks();

      if (adapterLinks != null && tableLinks != null)
      {
        try
        {        
          adapterLinks.Update(tableLinks);
        }
        catch { }
      }
    }

    private void tblTasks_RowLeave(object sender, DataGridViewCellEventArgs e)
    {
      SaveTasks();
    }

    private void tblLinks_RowLeave(object sender, DataGridViewCellEventArgs e)
    {
      if (tblLinks.Rows[e.RowIndex].Cells["idTask"].Value == DBNull.Value)
      {
        tblLinks.Rows[e.RowIndex].Cells["idTask"].Value = tblTasks.Rows[tblTasks.CurrentCell.RowIndex].Cells["idTask"].Value;
      }
      SaveLinks();
    }

    private void frmMain_Shown(object sender, EventArgs e)
    {
      ShowTasks();
      ShowLinks((tblTasks.CurrentCell != null && tblTasks.CurrentCell.RowIndex != -1) ? tblTasks.Rows[tblTasks.CurrentCell.RowIndex].Cells["idTask"].Value : DBNull.Value);
      this.tblTasks.SelectionChanged += new System.EventHandler(this.tblTasks_SelectionChanged);
    }

    private void tblTasks_SelectionChanged(object sender, EventArgs e)
    {
      ShowLinks((tblTasks.CurrentCell != null && tblTasks.CurrentCell.RowIndex != -1) ? tblTasks.Rows[tblTasks.CurrentCell.RowIndex].Cells["idTask"].Value : DBNull.Value);
    }

    private void menuSave_Click(object sender, EventArgs e)
    {
      if (tblTasks.Focused)
      {
        int rowindex = tblTasks.CurrentCell.RowIndex;
        int colindex = tblTasks.CurrentCell.ColumnIndex;
        SaveTasks();
        ShowTasks();
        if (rowindex >= 0 && colindex >= 0)
        {
          DataGridViewCell selected = tblTasks.Rows[rowindex].Cells[colindex];
          tblTasks.CurrentCell = selected;
        }
        tblTasks_SelectionChanged(sender, e);
      }
      else if (tblLinks.Focused)
      {
        int rowindex = tblLinks.CurrentCell.RowIndex;
        int colindex = tblLinks.CurrentCell.ColumnIndex;
        SaveLinks();
        tblTasks_SelectionChanged(sender, e);
        if (rowindex >= 0 && colindex >= 0)
        {
          DataGridViewCell selected = tblLinks.Rows[rowindex].Cells[colindex];
          tblLinks.CurrentCell = selected;
        }
      }
    }

    private void tblLinks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      OpenLink();
    }

    private void OpenLink()
    {
      if (tblLinks.CurrentCell == null)
        return;
      int rowindex = tblLinks.CurrentCell.RowIndex;
      if (rowindex >= 0 && tblLinks[3, rowindex].Value != null && Convert.ToString(tblLinks[3, rowindex].Value).Length > 0)
      {
        string url = Convert.ToString(tblLinks[3, rowindex].Value);
        Process.Start(url);
      }
    }

    private void OpenFile()
    {
      if (tblLinks.CurrentCell == null)
        return;
      int rowindex = tblLinks.CurrentCell.RowIndex;
      if (rowindex < 0 || tblLinks[0, rowindex].Value == null || Convert.ToString(tblLinks[0, rowindex].Value).Length == 0)
        return;    
      if (properties == null)
        return;
      SQLiteDataAdapter adapter = null;
      int idLink = Convert.ToInt32(tblLinks[0, rowindex].Value);
      DataTable t = db.ExecuteMainGetLinkPath(ref adapter, idLink);
      if (t == null || t.Rows.Count == 0)
        return;
      DataRow row = t.Rows[0];
      string projectName = Convert.ToString(row["SourceName"]);
      string taskName = Convert.ToString(row["TaskNumber"]);
      string fileName = Convert.ToString(row["LinkName"]);

      try
      {
        string path = Path.Combine(properties.strFreelanceDirectoryPath, projectName, taskName, fileName);
        try
        {
          Process.Start(path);
        }
        catch
        {
          MessageBox.Show("Файл не найден");
        }
      }
      catch
      {
        MessageBox.Show("Невозможно открыть файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void btnOpenLink_Click(object sender, EventArgs e)
    {
        OpenLink();
    }

    private void btnOpenFile_Click(object sender, EventArgs e)
    {
      OpenFile();
    }

    private void menuBill_Click(object sender, EventArgs e)
    {
      if (db == null)
      {
        throw new Exception("fmDB is not assigned!");
      }
      frmBill frm = new frmBill(db);
      SQLiteDataAdapter adapter = null;
      DataTable tableSources = db.ExecuteReferenceSources(ref adapter);
      frm.cbSource.DataSource = tableSources;
      frm.cbSource.DisplayMember = "SourceName";
      frm.cbSource.ValueMember = "idSource";
      frm.ShowDialog();
      ShowTasks();
    }

    private void menuMult_Click(object sender, EventArgs e)
    {
      if (tblTasks.CurrentCell == null)
        return;
      int rowindex = tblTasks.CurrentCell.RowIndex;
      if (rowindex < 0)
        return; 
      SQLiteDataAdapter adapter = null;
      if (db == null)
      {
        throw new Exception("fmDB is not assigned!");
      }
      if (MessageBox.Show("Размножить текущую запись?", "Размножить", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
      {
        db.ExecuteMultiplication(ref adapter, Convert.ToInt32(tblTasks.Rows[rowindex].Cells["idTask"].Value));
        ShowTasks();
      }
    }

    private void menuShowStatistics_Click(object sender, EventArgs e)
    {
      const string seriesTotalName = "Итого";
      if (db == null)
      {
        throw new Exception("fmDB is not assigned!");
      }
      frmStatistics frm = new frmStatistics(db);
      SQLiteDataAdapter adapter = null;
      DataTable tableStatistics = db.ExecuteMonthPayedStatistics(ref adapter);
      frm.tblData.DataSource = tableStatistics;
      foreach (DataGridViewColumn c in frm.tblData.Columns)
      {
        if (c.Name == "Summ")
        {
          c.HeaderText = "Доход";
          c.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
          c.ReadOnly = true;
        }
        else if (c.Name == "Period")
        {
          c.HeaderText = "Период";
          c.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
          c.ReadOnly = true;
        }
        else
        {
          c.Visible = false;
        }
      }

      SQLiteDataAdapter adapterSources = null;
      DataTable tableSources = db.ExecuteReferenceSources(ref adapterSources);

      SQLiteDataAdapter adapterStatisticsWithSources = null;
      DataTable tableStatisticsWithSources = db.ExecuteMonthPayedStatisticsWithSources(ref adapterStatisticsWithSources);

      foreach (DataRow r in tableSources.Rows)
      {
        frm.chartMonth.Series.Add(r["SourceName"].ToString());
        frm.chartMonth.Series[r["SourceName"].ToString()].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
        frm.chartMonth.Series[r["SourceName"].ToString()].BorderWidth = 2;
        object value = r["SourceColor"];
        if (value == null) return;
        string str = value.ToString();
        if (str != "")
        {
          frm.chartMonth.Series[r["SourceName"].ToString()].Color = Color.FromArgb(Convert.ToInt32(str));
        }
        frm.chartMonth.Legends.Add(r["SourceName"].ToString());
        frm.chartMonth.Legends[r["SourceName"].ToString()].BorderColor = frm.chartMonth.Series[r["SourceName"].ToString()].Color;
      }

      frm.chartMonth.Series.Add(seriesTotalName);
      frm.chartMonth.Series[seriesTotalName].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
      frm.chartMonth.Series[seriesTotalName].BorderWidth = 4;

      frm.chartMonth.Legends.Add(seriesTotalName);
      frm.chartMonth.Legends[seriesTotalName].BorderColor = frm.chartMonth.Series[seriesTotalName].Color;

      foreach (DataRow r in tableStatistics.Rows)
      {
        frm.chartMonth.Series[seriesTotalName].Points.Add(Convert.ToDouble(r["Summ"].ToString()));
        frm.chartMonth.Series[seriesTotalName].Points[frm.chartMonth.Series[seriesTotalName].Points.Count - 1].AxisLabel = r["Period"].ToString();
      }

      foreach (DataRow s in tableSources.Rows)
      {
        foreach (DataRow t in tableStatistics.Rows)
        {
          object value = null;
          foreach (DataRow r in tableStatisticsWithSources.Rows)
          {
            if (r["SourceName"].ToString() == s["SourceName"].ToString() && r["Period"].ToString() == t["Period"].ToString())
            {
              value = r["Summ"];
            }
          }
          if (value == null)
          {
            frm.chartMonth.Series[s["SourceName"].ToString()].Points.Add(0.0);
          }
          else
          {
            frm.chartMonth.Series[s["SourceName"].ToString()].Points.Add(Convert.ToDouble(value.ToString()));
          }
        }
      }
      frm.ShowDialog();
    }

    private static void AddFilesFromDirectoryToDictionary(Dictionary<string, string> filesDictionary, string pathToDirectory)
    {
      DirectoryInfo dirInfo = new DirectoryInfo(pathToDirectory);

      FileInfo[] fileInfos = dirInfo.GetFiles("*.*", SearchOption.AllDirectories);

      foreach (FileInfo fi in fileInfos)
      {
        filesDictionary.Add(fi.FullName.Replace(dirInfo.Parent.FullName + "\\", ""), fi.FullName);
      }
    }

    private void menuArchive_Click(object sender, EventArgs e)
    {
      if (db == null)
      {
        throw new Exception("fmDB is not assigned!");
      }
      if (properties == null)
      {
        throw new Exception("fmProperties is not assigned!");
      }
      string dllpath = properties.str7ZipDirectoryPath + "\\" + "7z.dll";
      SevenZipCompressor.SetLibraryPath(dllpath);
      SQLiteDataAdapter adapter = null;
      DataTable tableMonths = db.ExecuteGetMonthsToArchive(ref adapter);
      if (tableMonths.Rows.Count == 0)
      {
        MessageBox.Show("Нет данных для архивации", "Данные архивированы", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
      }
      foreach (DataRow m in tableMonths.Rows)
      {
        string date = m["TaskDate"].ToString();
        DataTable tableTasks = db.ExecuteGetTasksToArchive(ref adapter, date);
        if (tableTasks.Rows.Count == 0)
        {
          MessageBox.Show("Нет данных для архивации", "Данные архивированы", MessageBoxButtons.OK, MessageBoxIcon.Information);
          return;
        }
        string[] dirs = null;
        string[] files = null;
        string path = properties.strFreelanceDirectoryPath + "\\" + "done";
        foreach (DataRow r in tableTasks.Rows)
        {
          string TaskNumber = r["TaskNumber"].ToString();
          string[] finded = Directory.GetDirectories(path, TaskNumber + "*");
          if (dirs == null)
            dirs = finded;
          else
            dirs = dirs.Concat(finded).ToArray();
          finded = Directory.GetFiles(path, TaskNumber + "*.zip");
          if (files == null)
            files = finded;
          else
            files = files.Concat(finded).ToArray();
        }
        if (files.Count() == 0)
        {
          continue;
        }
        SevenZipCompressor szc = new SevenZipCompressor
        {
          CompressionMethod = CompressionMethod.Lzma2,
          CompressionLevel = CompressionLevel.Ultra,
          CompressionMode = CompressionMode.Create,
          DirectoryStructure = true,
          PreserveDirectoryRoot = false,
          ArchiveFormat = OutArchiveFormat.SevenZip
        };
        string archname = path + "\\" + "done.arch." + m["DateTask"].ToString() + ".7z";

        Dictionary<string, string> filesDictionary = new Dictionary<string, string>();
        foreach (string d in dirs)
          AddFilesFromDirectoryToDictionary(filesDictionary, d);

        Cursor.Current = Cursors.WaitCursor;

        FileStream fs = new FileStream(archname, FileMode.Create);
        szc.CompressFileDictionary(filesDictionary, fs);
        fs.Close();

        archname = path + "\\" + "done.arch.zip." + m["DateTask"].ToString() + ".7z";
        fs = new FileStream(archname, FileMode.Create);
        szc.CompressFiles(fs, files);
        fs.Close();

        foreach (string s in dirs)
        {
          Directory.Delete(s, true);
        }

        foreach (string f in files)
        {
          File.Delete(f);
        }

        db.ExecuteSetArchivedTasks(ref adapter, date);
      }

      Cursor.Current = Cursors.Default;

      MessageBox.Show("Готово", "Архивация", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void menuCopy_Click(object sender, EventArgs e)
    {
      if (tblTasks.CurrentCell == null)
        return;
      int rowindex = tblTasks.CurrentCell.RowIndex;
      if (rowindex < 0)
        return;
      SQLiteDataAdapter adapter = null;
      if (db == null)
      {
        throw new Exception("fmDB is not assigned!");
      }
      if (MessageBox.Show("Скопировать текущую запись?", "Копирование", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
      {
        db.CopyTask(ref adapter, Convert.ToInt32(tblTasks.Rows[rowindex].Cells["idTask"].Value));
        ShowTasks();
      }
    }

    private void menuTblTaskCreateOutFolder_Click(object sender, EventArgs e)
    {
      if (db == null)
      {
        throw new Exception("fmDB is not assigned!");
      }   
      if (properties == null)
      {
        throw new Exception("fmProperties is not assigned!");
      }
      if (tblTasks.CurrentCell == null)
        return;
      int rowindex = tblTasks.CurrentCell.RowIndex;
      if (rowindex < 0)
        return;
      SQLiteDataAdapter adapter = null;
      string projectName = db.GetSourceNameById(ref adapter, Convert.ToInt32(tblTasks.Rows[rowindex].Cells["SourceName"].Value));
      string taskName = Convert.ToString(tblTasks.Rows[rowindex].Cells["TaskNumber"].Value);
      string path = Path.Combine(properties.strFreelanceDirectoryPath, "sources", projectName, taskName);
      if (!Directory.Exists(path))
      {
        Directory.CreateDirectory(path);
      }
    }

    private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
    {
      // Get the subdirectories for the specified directory.
      DirectoryInfo dir = new DirectoryInfo(sourceDirName);
      DirectoryInfo[] dirs = dir.GetDirectories();

      if (!dir.Exists)
      {
        throw new DirectoryNotFoundException(
            "Source directory does not exist or could not be found: "
            + sourceDirName);
      }

      // If the destination directory doesn't exist, create it. 
      if (!Directory.Exists(destDirName))
      {
        Directory.CreateDirectory(destDirName);
      }

      // Get the files in the directory and copy them to the new location.
      FileInfo[] files = dir.GetFiles();
      foreach (FileInfo file in files)
      {
        string temppath = Path.Combine(destDirName, file.Name);
        file.CopyTo(temppath, true);
      }

      // If copying subdirectories, copy them and their contents to new location. 
      if (copySubDirs)
      {
        foreach (DirectoryInfo subdir in dirs)
        {
          string temppath = Path.Combine(destDirName, subdir.Name);
          DirectoryCopy(subdir.FullName, temppath, copySubDirs);
        }
      }
    }

    private void menuTblTaskCreateArchiveCopy_Click(object sender, EventArgs e)
    {
      if (db == null)
      {
        throw new Exception("fmDB is not assigned!");
      }
      if (properties == null)
      {
        throw new Exception("fmProperties is not assigned!");
      }
      if (tblTasks.CurrentCell == null)
        return;
      int rowindex = tblTasks.CurrentCell.RowIndex;
      if (rowindex < 0)
        return;
      SQLiteDataAdapter adapter = null;
      string projectName = db.GetSourceNameById(ref adapter, Convert.ToInt32(tblTasks.Rows[rowindex].Cells["SourceName"].Value));
      string taskName = Convert.ToString(tblTasks.Rows[rowindex].Cells["TaskNumber"].Value);
      string srcPath = Path.Combine(properties.strFreelanceDirectoryPath, "sources", projectName, taskName);
      string dstPath = Path.Combine(properties.strFreelanceDirectoryPath, "done", taskName);
      if (!Directory.Exists(srcPath))
      {
        return;
      }
      string dllpath = properties.str7ZipDirectoryPath + "\\" + "7z.dll";
      SevenZipCompressor.SetLibraryPath(dllpath);

      Cursor.Current = Cursors.WaitCursor;

      DirectoryCopy(srcPath, dstPath, true);

      SevenZipCompressor szc = new SevenZipCompressor
      {
        CompressionMethod = CompressionMethod.Deflate,
        CompressionLevel = CompressionLevel.Ultra,
        CompressionMode = CompressionMode.Create,
        DirectoryStructure = true,
        PreserveDirectoryRoot = false,
        ArchiveFormat = OutArchiveFormat.Zip
      };

      string archname = Path.Combine(properties.strFreelanceDirectoryPath, "done") + "\\" + taskName + ".zip";
      int num = 0;
      while(true)
      {
        if (!File.Exists(archname))
        {
          break;
        }
        archname = Path.Combine(properties.strFreelanceDirectoryPath, "done") + "\\" + taskName + "." + num.ToString("D4") + ".zip";
        if (!File.Exists(archname))
        {
          break;
        }
        num++;
      }

      Dictionary<string, string> filesDictionary = new Dictionary<string, string>();
      AddFilesFromDirectoryToDictionary(filesDictionary, dstPath);

      FileStream fs = new FileStream(archname, FileMode.Create);
      szc.CompressFileDictionary(filesDictionary, fs);
      fs.Close();

      Cursor.Current = Cursors.Default;

      MessageBox.Show("Готово", "Создание архивной копии", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
  }
}
