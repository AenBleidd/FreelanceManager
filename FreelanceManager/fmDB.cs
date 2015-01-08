using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// SQLite
using System.Data;
using System.Data.Common;
using System.Data.SQLite;

namespace FreelanceManager
{
  public class fmDB
  {
    fmProperties properties = null;
    SQLiteFactory factory = null;
    SQLiteConnection connection = null;
    const string DBName = "FreelanceManager.db";
    public fmDB()
    {
      try
      {
        factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
      }
      catch
      {
        MessageBox.Show("Не установлен System.Data.SQLite (1.0.93.0)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        Application.Exit();
      }
    }
    public void setProperties(fmProperties _properties)
    {
      properties = _properties;
    }

    public bool Connect()
    {
      if (connection != null)
        CloseConnection();
      if (properties == null)
      {
        throw new Exception("fmProperties is not assigned to fmDB!");
      }
      connection = (SQLiteConnection)factory.CreateConnection();
      connection.ConnectionString = "Data Source = " + properties.strDBPath + "\\"+ DBName;
      try
      {
        connection.Open();
        return true;
      }
      catch (Exception err)
      {
        MessageBox.Show(err.Message);
        return false;
      }
    }

    public bool ReConnect()
    {
      CloseConnection();
      return Connect();
    }

    public void CloseConnection()
    {
      connection.Close();
      connection = null;
    }

    public bool ExecuteNonQuery(string query, Dictionary<string, SQLiteParameter> parameters = null)
    {
      try
      {
        SQLiteCommand command = new SQLiteCommand(query, connection);
        if (parameters != null)
        {
          foreach (var p in parameters)
          {
            command.Parameters.Add(p.Value);
          }
        }
        command.ExecuteNonQuery();
        return true;
      }
      catch (Exception err)
      {
        MessageBox.Show(err.Message);
        return false;
      }
    }

    public DbDataRecord ExecuteCommand(ref SQLiteDataAdapter adapter, string query, Dictionary<string, SQLiteParameter> parameters = null)
    {
      if (query == null)
      {
        throw new Exception("fmDB: no query is assigned");
      }
      try
      {
        SQLiteCommand command = new SQLiteCommand(query, connection);
        if (parameters != null)
        {
          foreach (var p in parameters)
          {
            command.Parameters.Add(p.Value);
          }
        }
        SQLiteDataReader reader = command.ExecuteReader();
        foreach (DbDataRecord r in reader)
        {
          return r;
        }
        return null;
      }
      catch (Exception err)
      {
        MessageBox.Show(err.Message);
        return null;
      }
    }

    public DataTable ExecuteQuery(ref SQLiteDataAdapter adapter,
      string querySelect = null, Dictionary<string, SQLiteParameter> parametersSelect = null,
      string queryInsert = null, Dictionary<string, SQLiteParameter> parametersInsert = null,
      string queryUpdate = null, Dictionary<string, SQLiteParameter> parametersUpdate = null,
      string queryDelete = null, Dictionary<string, SQLiteParameter> parametersDelete = null)
    {
      if (querySelect == null && queryInsert == null && queryUpdate == null && queryDelete == null)
      {
        throw new Exception("fmDB: no query is assigned");
      }
      try
      {
        SQLiteCommand select = null;
        if (querySelect != null)
        {
          select = new SQLiteCommand(querySelect, connection);
          if (parametersSelect != null)
          {
            foreach (var p in parametersSelect)
            {
              select.Parameters.Add(p.Value);
            }
          }
        }
        SQLiteCommand insert = null;
        if (queryInsert != null)
        {
          insert = new SQLiteCommand(queryInsert, connection);
          if (parametersInsert != null)
          {
            foreach (var p in parametersInsert)
            {
              insert.Parameters.Add(p.Value);
            }
          }
        }
        SQLiteCommand update = null;
        if (queryUpdate != null)
        {
          update = new SQLiteCommand(queryUpdate, connection);
          if (parametersUpdate != null)
          {
            foreach (var p in parametersUpdate)
            {
              update.Parameters.Add(p.Value);
            }
          }
        }
        SQLiteCommand delete = null;
        if (queryDelete != null)
        {
          delete = new SQLiteCommand(queryDelete, connection);
          if (parametersDelete != null)
          {
            foreach (var p in parametersDelete)
            {
              delete.Parameters.Add(p.Value);
            }
          }
        }
        adapter = new SQLiteDataAdapter();
        adapter.SelectCommand = select;
        adapter.InsertCommand = insert;
        adapter.UpdateCommand = update;
        adapter.DeleteCommand = delete;
        DataTable table = new DataTable();
        adapter.Fill(table);
        return table;
      }
      catch (Exception err)
      {
        MessageBox.Show(err.Message);
        return null;
      }
    }

    public DataTable ExecuteReferenceSources(ref SQLiteDataAdapter adapter)
    {
      const string IdSource = "IdSource";
      const string IdSourceParam = "@" + IdSource;
      const string SourceName = "SourceName";
      const string SourceNameParam = "@" + SourceName;
      const string IsVisible = "isVisible";
      const string IsVisibleParam = "@" + IsVisible;
      const string Email = "email";
      const string EMailParam = "@" + Email;

      SQLiteParameter pID = new SQLiteParameter();
      pID.ParameterName = IdSourceParam;
      pID.DbType = DbType.Int32;
      pID.Size = 4;
      pID.SourceColumn = IdSource;

      SQLiteParameter pName = new SQLiteParameter();
      pName.ParameterName = SourceNameParam;
      pName.DbType = DbType.String;
      pName.Size = 256;
      pName.SourceColumn = SourceName;

      SQLiteParameter pIsVisible = new SQLiteParameter();
      pIsVisible.ParameterName = IsVisibleParam;
      pIsVisible.DbType = DbType.Boolean;
      pIsVisible.Size = 1;
      pIsVisible.SourceColumn = IsVisible;

      SQLiteParameter pEmail = new SQLiteParameter();
      pEmail.ParameterName = EMailParam;
      pEmail.DbType = DbType.String;
      pEmail.Size = 256;
      pEmail.SourceColumn = Email;

      const string select = "select * from tblSources;";

      const string insert = "insert into tblSources (SourceName, isVisible, email) values (@SourceName, @isVisible, @email);";
      Dictionary<string, SQLiteParameter> insertParams = new Dictionary<string, SQLiteParameter>();
      insertParams[pName.ParameterName] = pName;
      insertParams[pIsVisible.ParameterName] = pIsVisible;
      insertParams[pEmail.ParameterName] = pEmail;

      const string update = "update tblSources set SourceName = @SourceName, isVisible = @isVisible, email = @email where idSource = @idSource;";
      Dictionary<string, SQLiteParameter> updateParams = new Dictionary<string, SQLiteParameter>();
      updateParams[pName.ParameterName] = pName;
      updateParams[pID.ParameterName] = pID;
      updateParams[pIsVisible.ParameterName] = pIsVisible;
      updateParams[pEmail.ParameterName] = pEmail;

      const string delete = "delete from tblSources where idSource = @idSource;";
      Dictionary<string, SQLiteParameter> deleteParams = new Dictionary<string, SQLiteParameter>();
      deleteParams[pID.ParameterName] = pID;
      return ExecuteQuery(ref adapter, select, null, insert, insertParams, update, updateParams, delete, deleteParams);
    }

    public DataTable ExecuteReferenceStatuses(ref SQLiteDataAdapter adapter)
    {
      const string IdStatus = "idStatus";
      const string IdStatusParam = "@" + IdStatus;
      const string StatusName = "StatusName";
      const string StatusNameParam = "@" + StatusName;
      const string StatusColor = "StatusColor";
      const string StatusColorParam = "@" + StatusColor;
      const string IsVisible = "isVisible";
      const string IsVisibleParam = "@" + IsVisible;

      SQLiteParameter pID = new SQLiteParameter();
      pID.ParameterName = IdStatusParam;
      pID.DbType = DbType.Int32;
      pID.Size = 4;
      pID.SourceColumn = IdStatus;

      SQLiteParameter pName = new SQLiteParameter();
      pName.ParameterName = StatusNameParam;
      pName.DbType = DbType.String;
      pName.Size = 256;
      pName.SourceColumn = StatusName;

      SQLiteParameter pColor = new SQLiteParameter();
      pColor.ParameterName = StatusColorParam;
      pColor.DbType = DbType.Int32;
      pColor.Size = 4;
      pColor.SourceColumn = StatusColor;

      SQLiteParameter pIsVisible = new SQLiteParameter();
      pIsVisible.ParameterName = IsVisibleParam;
      pIsVisible.DbType = DbType.Boolean;
      pIsVisible.Size = 1;
      pIsVisible.SourceColumn = IsVisible;

      const string select = "select * from tblStatuses;";

      const string insert = "insert into tblStatuses (StatusName, StatusColor, isVisible) values (@StatusName, @StatusColor, @isVisible);";
      Dictionary<string, SQLiteParameter> insertParams = new Dictionary<string, SQLiteParameter>();
      insertParams[pName.ParameterName] = pName;
      insertParams[pColor.ParameterName] = pColor;
      insertParams[pIsVisible.ParameterName] = pIsVisible;

      const string update = "update tblStatuses set StatusName = @StatusName, StatusColor = @StatusColor, isVisible = @isVisible where idStatus = @idStatus;";
      Dictionary<string, SQLiteParameter> updateParams = new Dictionary<string, SQLiteParameter>();
      updateParams[pID.ParameterName] = pID;
      updateParams[pName.ParameterName] = pName;
      updateParams[pColor.ParameterName] = pColor;
      updateParams[pIsVisible.ParameterName] = pIsVisible;

      const string delete = "delete from tblStatuses where idStatus = @idStatus;";
      Dictionary<string, SQLiteParameter> deleteParams = new Dictionary<string, SQLiteParameter>();
      deleteParams[pID.ParameterName] = pID;

      return ExecuteQuery(ref adapter, select, null, insert, insertParams, update, updateParams, delete, deleteParams);
    }

    public DataTable ExecuteReferenceLanguages(ref SQLiteDataAdapter adapter)
    {
      const string IdLanguage = "idLanguage";
      const string IdLanguageParam = "@" + IdLanguage;
      const string LanguageName = "LanguageName";
      const string LanguageNameParam = "@" + LanguageName;

      SQLiteParameter pID = new SQLiteParameter();
      pID.ParameterName = IdLanguageParam;
      pID.DbType = DbType.Int32;
      pID.Size = 4;
      pID.SourceColumn = IdLanguage;

      SQLiteParameter pName = new SQLiteParameter();
      pName.ParameterName = LanguageNameParam;
      pName.DbType = DbType.String;
      pName.Size = 256;
      pName.SourceColumn = LanguageName;

      const string select = "select * from tblLanguages;";

      const string insert = "insert into tblLanguages (LanguageName) values (@LanguageName);";
      Dictionary<string, SQLiteParameter> insertParams = new Dictionary<string, SQLiteParameter>();
      insertParams[pName.ParameterName] = pName;

      const string update = "update tblLanguages set LanguageName = @LanguageName where idLanguage = @idLanguage;";
      Dictionary<string, SQLiteParameter> updateParams = new Dictionary<string, SQLiteParameter>();
      updateParams[pID.ParameterName] = pID;
      updateParams[pName.ParameterName] = pName;

      const string delete = "delete from tblLanguages where idLanguage = @idLanguage;";
      Dictionary<string, SQLiteParameter> deleteParams = new Dictionary<string, SQLiteParameter>();
      deleteParams[pID.ParameterName] = pID;

      return ExecuteQuery(ref adapter, select, null, insert, insertParams, update, updateParams, delete, deleteParams);
    }

    public DataTable ExecuteMainTasks(ref SQLiteDataAdapter adapter)
    {
      const string idTask = "idTask";
      const string idTaskParam = "@" + idTask;
      const string idSource = "idSource";
      const string idSourceParam = "@" + idSource;
      const string TaskNumber = "TaskNumber";
      const string TaskNumberParam = "@" + TaskNumber;
      const string TaskName = "TaskName";
      const string TaskNameParam = "@" + TaskName;
      const string idLanguage = "idLanguage";
      const string idLanguageParam = "@" + idLanguage;
      const string SubtaskCount = "SubtaskCount";
      const string SubtaskCountParam = "@" + SubtaskCount;
      const string SubtaskNumber = "SubtaskNumber";
      const string SubtaskNumberParam = "@" + SubtaskNumber;
      const string TaskReceiveDate = "TaskReceiveDate";
      const string TaskReceiveDateParam = "@" + TaskReceiveDate;
      const string TaskDeadlineDate = "TaskDeadlineDate";
      const string TaskDeadlineDateParam = "@" + TaskDeadlineDate;
      const string FirstDoneVersionDate = "FirstDoneVersionDate";
      const string FirstDoneVersionDateParam = "@" + FirstDoneVersionDate;
      const string Cost = "Cost";
      const string CostParam = "@" + Cost;
      const string idStatus = "idStatus";
      const string idStatusParam = "@" + idStatus;
      const string Remark = "Remark";
      const string RemarkParam = "@" + Remark;

      SQLiteParameter pIdTask = new SQLiteParameter();
      pIdTask.ParameterName = idTaskParam;
      pIdTask.DbType = DbType.Int32;
      pIdTask.Size = 4;
      pIdTask.SourceColumn = idTask;

      SQLiteParameter pidSource = new SQLiteParameter();
      pidSource.ParameterName = idSourceParam;
      pidSource.DbType = DbType.Int32;
      pidSource.Size = 4;
      pidSource.SourceColumn = idSource;

      SQLiteParameter pTaskNumber = new SQLiteParameter();
      pTaskNumber.ParameterName = TaskNumberParam;
      pTaskNumber.DbType = DbType.String;
      pTaskNumber.Size = 256;
      pTaskNumber.SourceColumn = TaskNumber;

      SQLiteParameter pTaskName = new SQLiteParameter();
      pTaskName.ParameterName = TaskNameParam;
      pTaskName.DbType = DbType.String;
      pTaskName.Size = 256;
      pTaskName.SourceColumn = TaskName;

      SQLiteParameter pidLanguage = new SQLiteParameter();
      pidLanguage.ParameterName = idLanguageParam;
      pidLanguage.DbType = DbType.Int32;
      pidLanguage.Size = 4;
      pidLanguage.SourceColumn = idLanguage;

      SQLiteParameter pSubtaskCount = new SQLiteParameter();
      pSubtaskCount.ParameterName = SubtaskCountParam;
      pSubtaskCount.DbType = DbType.Int32;
      pSubtaskCount.Size = 4;
      pSubtaskCount.SourceColumn = SubtaskCount;

      SQLiteParameter pSubtaskNumber = new SQLiteParameter();
      pSubtaskNumber.ParameterName = SubtaskNumberParam;
      pSubtaskNumber.DbType = DbType.Int32;
      pSubtaskNumber.Size = 4;
      pSubtaskNumber.SourceColumn = SubtaskNumber;

      SQLiteParameter pTaskReceiveDate = new SQLiteParameter();
      pTaskReceiveDate.ParameterName = TaskReceiveDateParam;
      pTaskReceiveDate.DbType = DbType.Date;
      pTaskReceiveDate.Size = 4;
      pTaskReceiveDate.SourceColumn = TaskReceiveDate;

      SQLiteParameter pTaskDeadlineDate = new SQLiteParameter();
      pTaskDeadlineDate.ParameterName = TaskDeadlineDateParam;
      pTaskDeadlineDate.DbType = DbType.Date;
      pTaskDeadlineDate.Size = 4;
      pTaskDeadlineDate.SourceColumn = TaskDeadlineDate;

      SQLiteParameter pFirstDoneVersionDate = new SQLiteParameter();
      pFirstDoneVersionDate.ParameterName = FirstDoneVersionDateParam;
      pFirstDoneVersionDate.DbType = DbType.Date;
      pFirstDoneVersionDate.Size = 4;
      pFirstDoneVersionDate.SourceColumn = FirstDoneVersionDate;

      SQLiteParameter pCost = new SQLiteParameter();
      pCost.ParameterName = CostParam;
      pCost.DbType = DbType.Double;
      pCost.Size = 8;
      pCost.SourceColumn = Cost;

      SQLiteParameter pidStatus = new SQLiteParameter();
      pidStatus.ParameterName = idStatusParam;
      pidStatus.DbType = DbType.Int32;
      pidStatus.Size = 4;
      pidStatus.SourceColumn = idStatus;

      SQLiteParameter pRemark = new SQLiteParameter();
      pRemark.ParameterName = RemarkParam;
      pRemark.DbType = DbType.String;
      pRemark.Size = 4000;
      pRemark.SourceColumn = Remark;

      const string select = "select t.*, case when TaskDeadlineDate is null then printf('%s.%s', strftime('%m', TaskReceiveDate), strftime('%Y', TaskReceiveDate)) else printf('%s.%s', strftime('%m', TaskDeadlineDate), strftime('%Y', TaskDeadlineDate)) end as [Period],  s.StatusColor from tblTasks t inner join tblStatuses s on t.idStatus = s.idStatus inner join tblSources src on t.idSource = src.idSource where s.isVisible = 1 and src.isVisible = 1;";

      const string insert = "insert into tblTasks (idSource, TaskNumber, TaskName, idLanguage, SubtaskCount, SubtaskNumber, TaskReceiveDate, TaskDeadlineDate, FirstDoneVersionDate, Cost, idStatus, Remark) values (@idSource, @TaskNumber, @TaskName, @idLanguage, @SubtaskCount, @SubtaskNumber, @TaskReceiveDate, @TaskDeadlineDate, @FirstDoneVersionDate, @Cost, @idStatus, @Remark);";
      Dictionary<string, SQLiteParameter> insertParams = new Dictionary<string, SQLiteParameter>();
      insertParams[pidSource.ParameterName] = pidSource;
      insertParams[pTaskNumber.ParameterName] = pTaskNumber;
      insertParams[pTaskName.ParameterName] = pTaskName;
      insertParams[pidLanguage.ParameterName] = pidLanguage;
      insertParams[pSubtaskCount.ParameterName] = pSubtaskCount;
      insertParams[pSubtaskNumber.ParameterName] = pSubtaskNumber;
      insertParams[pTaskReceiveDate.ParameterName] = pTaskReceiveDate;
      insertParams[pTaskDeadlineDate.ParameterName] = pTaskDeadlineDate;
      insertParams[pFirstDoneVersionDate.ParameterName] = pFirstDoneVersionDate;
      insertParams[pCost.ParameterName] = pCost;
      insertParams[pidStatus.ParameterName] = pidStatus;
      insertParams[pRemark.ParameterName] = pRemark;

      const string update = "update tblTasks set idSource = @idSource, TaskNumber = @TaskNumber, TaskName = @TaskName, idLanguage = @idLanguage, SubtaskCount = @SubtaskCount, SubtaskNumber = @SubtaskNumber, TaskReceiveDate = @TaskReceiveDate, TaskDeadlineDate = @TaskDeadlineDate, FirstDoneVersionDate = @FirstDoneVersionDate, Cost = @Cost, idStatus = @idStatus, Remark = @Remark where idTask = @idTask;";
      Dictionary<string, SQLiteParameter> updateParams = new Dictionary<string, SQLiteParameter>();
      updateParams[pIdTask.ParameterName] = pIdTask;
      updateParams[pidSource.ParameterName] = pidSource;
      updateParams[pTaskNumber.ParameterName] = pTaskNumber;
      updateParams[pTaskName.ParameterName] = pTaskName;
      updateParams[pidLanguage.ParameterName] = pidLanguage;
      updateParams[pSubtaskCount.ParameterName] = pSubtaskCount;
      updateParams[pSubtaskNumber.ParameterName] = pSubtaskNumber;
      updateParams[pTaskReceiveDate.ParameterName] = pTaskReceiveDate;
      updateParams[pTaskDeadlineDate.ParameterName] = pTaskDeadlineDate;
      updateParams[pFirstDoneVersionDate.ParameterName] = pFirstDoneVersionDate;
      updateParams[pCost.ParameterName] = pCost;
      updateParams[pidStatus.ParameterName] = pidStatus;
      updateParams[pRemark.ParameterName] = pRemark;

      const string delete = "delete from tblTasks where idTask = @idTask;";
      Dictionary<string, SQLiteParameter> deleteParams = new Dictionary<string, SQLiteParameter>();
      deleteParams[pIdTask.ParameterName] = pIdTask;

      return ExecuteQuery(ref adapter, select, null, insert, insertParams, update, updateParams, delete, deleteParams);
    }

    public DataTable ExecuteMainLinks(ref SQLiteDataAdapter adapter, Int32 _idTask)
    {
      const string idLink = "idLink";
      const string idLinkParam = "@" + idLink;
      const string idTask = "idTask";
      const string idTaskParam = "@" + idTask;
      const string Link = "Link";
      const string LinkParam = "@" + Link;
      const string LinkName = "LinkName";
      const string LinkNameParam = "@" + LinkName;

      SQLiteParameter pidLink = new SQLiteParameter();
      pidLink.ParameterName = idLinkParam;
      pidLink.DbType = DbType.Int32;
      pidLink.Size = 4;
      pidLink.SourceColumn = idLink;

      SQLiteParameter pIdTask = new SQLiteParameter();
      pIdTask.ParameterName = idTaskParam;
      pIdTask.DbType = DbType.Int32;
      pIdTask.Size = 4;
      pIdTask.SourceColumn = idTask;

      SQLiteParameter pIdTaskSelect = new SQLiteParameter();
      pIdTaskSelect.ParameterName = idTaskParam;
      pIdTaskSelect.DbType = DbType.Int32;
      pIdTaskSelect.Size = 4;
      pIdTaskSelect.Value = _idTask;

      SQLiteParameter pLink = new SQLiteParameter();
      pLink.ParameterName = LinkParam;
      pLink.DbType = DbType.String;
      pLink.Size = 4000;
      pLink.SourceColumn = Link;

      SQLiteParameter pLinkName = new SQLiteParameter();
      pLinkName.ParameterName = LinkNameParam;
      pLinkName.DbType = DbType.String;
      pLinkName.Size = 256;
      pLinkName.SourceColumn = LinkName;

      const string select = "select * from tblTasksLinks where idTask = @idTask;";
      Dictionary<string, SQLiteParameter> selectParams = new Dictionary<string, SQLiteParameter>();
      selectParams[pIdTask.ParameterName] = pIdTaskSelect;

      const string insert = "insert into tblTasksLinks (idTask, Link, LinkName) values (@idTask, @Link, @LinkName);";
      Dictionary<string, SQLiteParameter> insertParams = new Dictionary<string, SQLiteParameter>();
      insertParams[pIdTask.ParameterName] = pIdTask;
      insertParams[pLink.ParameterName] = pLink;
      insertParams[pLinkName.ParameterName] = pLinkName;

      const string update = "update tblTasksLinks set idTask = @idTask, Link = @Link, LinkName = @LinkName where idLink = @idLink;";
      Dictionary<string, SQLiteParameter> updateParams = new Dictionary<string,SQLiteParameter>();
      updateParams[pidLink.ParameterName] = pidLink;
      updateParams[pIdTask.ParameterName] = pIdTask;
      updateParams[pLink.ParameterName] = pLink;
      updateParams[pLinkName.ParameterName] = pLinkName;

      const string delete = "delete from tblTasksLinks where idLink = @idLink;";
      Dictionary<string, SQLiteParameter> deleteParams = new Dictionary<string, SQLiteParameter>();
      deleteParams[pidLink.ParameterName] = pidLink;

      return ExecuteQuery(ref adapter, select, selectParams, insert, insertParams, update, updateParams, delete, deleteParams);
    }

    public DataTable ExecuteMainGetLinkPath(ref SQLiteDataAdapter adapter, Int32 _idLink)
    {
      const string idLink = "idLink";
      const string idLinkParam = "@" + idLink;

      SQLiteParameter pidLink = new SQLiteParameter();
      pidLink.ParameterName = idLinkParam;
      pidLink.DbType = DbType.Int32;
      pidLink.Size = 4;
      pidLink.Value = _idLink;

      const string select = "select SourceName, TaskNumber, LinkName from tblTasksLinks l inner join tblTasks t on l.idTask = t.idTask inner join tblSources s on t.idSource = s.idSource where idLink = @idLink;";
      Dictionary<string, SQLiteParameter> selectParams = new Dictionary<string, SQLiteParameter>();
      selectParams[pidLink.ParameterName] = pidLink;

      return ExecuteQuery(ref adapter, select, selectParams);
    }

    public DataTable ExecuteBill(ref SQLiteDataAdapter adapter, Int32 _idSource)
    {
      const string idSource = "idSource";
      const string idSourceParam = "@" + idSource;

      SQLiteParameter pidSource = new SQLiteParameter();
      pidSource.ParameterName = idSourceParam;
      pidSource.DbType = DbType.Int32;
      pidSource.Size = 4;
      pidSource.Value = _idSource;

      const string select = "select TaskNumber, FirstDoneVersionDate, sum(Cost) as Cost, cast(1 as bit) as isInclude from tblTasks where idStatus = 7 and idSource = @idSource group by TaskNumber, FirstDoneVersionDate;";
      Dictionary<string, SQLiteParameter> selectParams = new Dictionary<string, SQLiteParameter>();
      selectParams[pidSource.ParameterName] = pidSource;

      return ExecuteQuery(ref adapter, select, selectParams);
    }

    public void MarkAsPayed(Int32 _idSource)
    {
      const string idSource = "idSource";
      const string idSourceParam = "@" + idSource;

      SQLiteParameter pidSource = new SQLiteParameter();
      pidSource.ParameterName = idSourceParam;
      pidSource.DbType = DbType.Int32;
      pidSource.Size = 4;
      pidSource.Value = _idSource;

      const string query = "update tblTasks set idStatus = 3 where idStatus = 7 and idSource = @idSource";
      Dictionary<string, SQLiteParameter> queryParams = new Dictionary<string, SQLiteParameter>();
      queryParams[pidSource.ParameterName] = pidSource;

      ExecuteNonQuery(query, queryParams);
    }

    public void ExecuteMultiplication(ref SQLiteDataAdapter adapter, Int32 _idTask)
    {
      const string idTask = "idTask";
      const string idTaskParam = "@" + idTask;
      const string SubtaskCount = "SubtaskCount";
      const string SubtaskNumber = "SubtaskNumber";
      const string SubtaskNumberParam = "@" + SubtaskNumber;
      const string idNewTaskParam = "@idNewTask";

      SQLiteParameter pidTask = new SQLiteParameter();
      pidTask.ParameterName = idTaskParam;
      pidTask.DbType = DbType.Int32;
      pidTask.Size = 4;
      pidTask.Value = _idTask;

      const string getSubtaskCountQuery = "select SubtaskCount, SubtaskNumber from tblTasks where idTask = @idTask;";
      Dictionary<string, SQLiteParameter> getSubtaskCountQueryParams = new Dictionary<string, SQLiteParameter>();
      getSubtaskCountQueryParams[pidTask.ParameterName] = pidTask;

      DbDataRecord getSubtaskCountRecord = ExecuteCommand(ref adapter, getSubtaskCountQuery, getSubtaskCountQueryParams);
      int _SubtaskCount = Convert.ToInt32(getSubtaskCountRecord[SubtaskCount].ToString());
      int _SubtaskNumber = Convert.ToInt32(getSubtaskCountRecord[SubtaskNumber].ToString());
      const string CopyRecord = "insert into tblTasks (idSource, TaskNumber, TaskName, idLanguage, SubtaskCount, SubtaskNumber, TaskReceiveDate, TaskDeadlineDate, FirstDoneVersionDate, Cost, idStatus, Remark) select idSource, TaskNumber, TaskName, idLanguage, SubtaskCount, @SubtaskNumber, TaskReceiveDate, TaskDeadlineDate, FirstDoneVersionDate, Cost, idStatus, Remark from tblTasks where idTask = @idTask;";
      Dictionary<string, SQLiteParameter> CopyRecordQueryParams = new Dictionary<string, SQLiteParameter>();
      CopyRecordQueryParams[pidTask.ParameterName] = pidTask;
      SQLiteParameter pSubtaskNumber = new SQLiteParameter();
      pSubtaskNumber.ParameterName = SubtaskNumberParam;
      pSubtaskNumber.DbType = DbType.Int32;
      pSubtaskNumber.Size = 4;
      const string CopyRecordLinks = "insert into tblTasksLinks (idTask, Link, LinkName) select @idNewTask, Link, LinkName from tblTasksLinks where idTask = @idTask;";
      SQLiteParameter pNewTask = new SQLiteParameter();
      pNewTask.ParameterName = idNewTaskParam;
      pNewTask.DbType = DbType.Int32;
      pNewTask.Size = 4;
      Dictionary<string, SQLiteParameter> CopyRecordLinksParams = new Dictionary<string, SQLiteParameter>();
      const string getLastInsertIDQuery = "select last_insert_rowid();";
      
      for (int i = _SubtaskNumber + 1; i <= _SubtaskCount; i++)
      {
        pSubtaskNumber.Value = i;
        CopyRecordQueryParams[pSubtaskNumber.ParameterName] = pSubtaskNumber;
        ExecuteNonQuery(CopyRecord, CopyRecordQueryParams);
        DbDataRecord getLastInsertIDRecord = ExecuteCommand(ref adapter, getLastInsertIDQuery);
        pNewTask.Value = Convert.ToInt32(getLastInsertIDRecord[0].ToString());
        CopyRecordLinksParams[pNewTask.ParameterName] = pNewTask;
        CopyRecordLinksParams[pidTask.ParameterName] = pidTask;
        ExecuteNonQuery(CopyRecordLinks, CopyRecordLinksParams);
      }
    }

    public DataTable ExecuteMonthPayedStatistics(ref SQLiteDataAdapter adapter)
    {
      const string query = "select case when TaskDeadlineDate is null then printf('%s.%s', strftime('%m', TaskReceiveDate), strftime('%Y', TaskReceiveDate)) else printf('%s.%s', strftime('%m', TaskDeadlineDate), strftime('%Y', TaskDeadlineDate)) end as [Period], sum(Cost) as Summ from tblTasks where idStatus = 3 group by Period order by Period;";
      return ExecuteQuery(ref adapter, query);
    }

    public DataTable ExecuteGetTasksToArchive(ref SQLiteDataAdapter adapter, string _Date)
    {
      const string Date = "strDate";
      const string DateParam = "@" + Date;
      SQLiteParameter pDate = new SQLiteParameter();
      pDate.ParameterName = DateParam;
      pDate.DbType = DbType.String;
      pDate.Size = 10;
      pDate.Value = _Date;
      Dictionary<string, SQLiteParameter> queryParams = new Dictionary<string, SQLiteParameter>();
      queryParams[pDate.ParameterName] = pDate;
      const string query = "select TaskNumber from tblTasks where idStatus in (2, 3, 4) and isArchived = 0 and case when TaskDeadlineDate is null then printf('%s.%s', strftime('%m', TaskReceiveDate), strftime('%Y', TaskReceiveDate)) else printf('%s.%s', strftime('%m', TaskDeadlineDate), strftime('%Y', TaskDeadlineDate)) end = @strDate group by TaskNumber;";
      return ExecuteQuery(ref adapter, query, queryParams);
    }

    public void ExecuteSetArchivedTasks(ref SQLiteDataAdapter adapter, string _Date)
    {
      const string Date = "strDate";
      const string DateParam = "@" + Date;
      SQLiteParameter pDate = new SQLiteParameter();
      pDate.ParameterName = DateParam;
      pDate.DbType = DbType.String;
      pDate.Size = 10;
      pDate.Value = _Date;
      Dictionary<string, SQLiteParameter> queryParams = new Dictionary<string, SQLiteParameter>();
      queryParams[pDate.ParameterName] = pDate;
      const string query = "update tblTasks set isArchived = 1 where idStatus in (2, 3, 4) and isArchived = 0 and case when TaskDeadlineDate is null then printf('%s.%s', strftime('%m', TaskReceiveDate), strftime('%Y', TaskReceiveDate)) else printf('%s.%s', strftime('%m', TaskDeadlineDate), strftime('%Y', TaskDeadlineDate)) end = @strDate";
      ExecuteNonQuery(query, queryParams);
    }

  }
}

// update tblTasks set isArchived = 1 where case when TaskDeadlineDate is null then printf('%s.%s', strftime('%m', TaskReceiveDate), strftime('%Y', TaskReceiveDate)) else printf('%s.%s', strftime('%m', TaskDeadlineDate), strftime('%Y', TaskDeadlineDate)) end = '10.2014'
