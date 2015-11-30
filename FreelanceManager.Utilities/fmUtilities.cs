using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using System.Data.SQLite;
using SevenZip;

using FreelanceManager.Database;
using FreelanceManager.Properties;

namespace FreelanceManager.Utilities
{
  public class fmUtilities
  {
    public static void AddFilesFromDirectoryToDictionary(Dictionary<string, string> filesDictionary, string pathToDirectory)
    {
      var dirInfo = new DirectoryInfo(pathToDirectory);
      var fileInfos = dirInfo.GetFiles("*.*", SearchOption.AllDirectories);
      foreach (var fi in fileInfos)
        filesDictionary.Add(fi.FullName.Replace(dirInfo.Parent.FullName + "\\", ""), fi.FullName);
    }

    public static void Archive(fmDB db, fmProperties properties, bool bSilent = true)
    {
      var source = AppDomain.CurrentDomain.FriendlyName;
      if (bSilent)
      {
        if (!EventLog.SourceExists(source))
          EventLog.CreateEventSource(source, "Application");
      }
      if (db == null)
      {
        if (!bSilent)
          throw new Exception("fmDB is not assigned!");
        else
        {
          EventLog.WriteEntry(source, "fmDB is not assigned!", EventLogEntryType.Error);
          return;
        }
      }
      if (properties == null)
      {
        if (!bSilent)
          throw new Exception("fmProperties is not assigned!");
        else
        {
          EventLog.WriteEntry(source, "fmProperties is not assigned!", EventLogEntryType.Error);
          return;
        }
      }
      var dllpath = properties.str7ZipDirectoryPath + "\\" + "7z.dll";
      SevenZipBase.SetLibraryPath(dllpath);
      SQLiteDataAdapter adapter = null;
      var tableMonths = db.ExecuteGetMonthsToArchive(ref adapter);
      if (tableMonths.Rows.Count == 0)
      {
        if (!bSilent)
          MessageBox.Show("Нет данных для архивации", "Данные архивированы", MessageBoxButtons.OK, MessageBoxIcon.Information);
        else
          EventLog.WriteEntry(source, "Нет данных для архивации", EventLogEntryType.Warning);
        return;
      }
      foreach (DataRow m in tableMonths.Rows)
      {
        var date = m["TaskDate"].ToString();
        var tableTasks = db.ExecuteGetTasksToArchive(ref adapter, date);
        if (tableTasks.Rows.Count == 0)
        {
          if (!bSilent)
            MessageBox.Show("Нет данных для архивации", "Данные архивированы", MessageBoxButtons.OK, MessageBoxIcon.Information);
          else
            EventLog.WriteEntry(source, "Нет данных для архивации", EventLogEntryType.Warning);
          return;
        }
        string[] dirs = null;
        string[] files = null;
        var path = properties.strFreelanceDirectoryPath + "\\" + "done";
        foreach (DataRow r in tableTasks.Rows)
        {
          var TaskNumber = r["TaskNumber"].ToString();
          var finded = Directory.GetDirectories(path, TaskNumber + "*");
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
          continue;
        var szc = new SevenZipCompressor
        {
          CompressionMethod = CompressionMethod.Lzma2,
          CompressionLevel = CompressionLevel.Ultra,
          CompressionMode = CompressionMode.Create,
          DirectoryStructure = true,
          PreserveDirectoryRoot = false,
          ArchiveFormat = OutArchiveFormat.SevenZip
        };
        var archname = path + "\\" + "done.arch." + m["DateTask"].ToString() + ".7z";

        var filesDictionary = new Dictionary<string, string>();
        foreach (var d in dirs)
          AddFilesFromDirectoryToDictionary(filesDictionary, d);

        if (!bSilent)
          Cursor.Current = Cursors.WaitCursor;

        var fs = new FileStream(archname, FileMode.Create);
        szc.CompressFileDictionary(filesDictionary, fs);
        fs.Close();

        archname = path + "\\" + "done.arch.zip." + m["DateTask"].ToString() + ".7z";
        fs = new FileStream(archname, FileMode.Create);
        szc.CompressFiles(fs, files);
        fs.Close();

        foreach (var s in dirs)
          Directory.Delete(s, true);

        foreach (var f in files)
          File.Delete(f);

        db.ExecuteSetArchivedTasks(ref adapter, date);
      }

      if (!bSilent)
        Cursor.Current = Cursors.Default;

      if (!bSilent)
        MessageBox.Show("Готово", "Архивация", MessageBoxButtons.OK, MessageBoxIcon.Information);
      else
        EventLog.WriteEntry(source, "Готово", EventLogEntryType.Information);
    }
  }
}
