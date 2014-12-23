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
  public partial class frmStatistics : Form
  {
    public fmDB db;
    public frmStatistics(fmDB _db)
    {
      InitializeComponent();
      db = _db;
    }
  }
}
