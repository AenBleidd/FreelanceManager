using System;
using System.Diagnostics;

using FreelanceManager.Database;
using FreelanceManager.Properties;
using FreelanceManager.Utilities;

namespace FreelanceManager.Archiver
{
  class Program
  {
    static void Main(string[] args)
    {
      var source = AppDomain.CurrentDomain.FriendlyName;
      if (!EventLog.SourceExists(source))
        EventLog.CreateEventSource(source, "Application");
      EventLog.WriteEntry(source, "Starting...", EventLogEntryType.Information);
      var db = new fmDB(true);
      var prop = new fmProperties();
      prop.Load(false);
      db.setProperties(prop);
      if (db.Connect())
        fmUtilities.Archive(db, prop);
      else
        EventLog.WriteEntry(source, "Couldn't connect to database", EventLogEntryType.Error);
    }
  }
}
