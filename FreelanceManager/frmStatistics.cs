using System.Windows.Forms;

// SQLite

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
