using System.Windows.Forms;

using FreelanceManager.Database;

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
