using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Google API
using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace FreelanceManager
{
  class fmGoogleDrive
  {
    fmGoogle google = null;
    DriveService service = null;
    UserCredential credential = null;
    public fmGoogleDrive()
    {
      google = new fmGoogle();
      if (google.GoogleConnect())
      {
        credential = google.getUserCredential();
      }
    }
    public bool GoogleDriveConnect()
    {
      try
      {
        service = new DriveService(new BaseClientService.Initializer()
        {
          HttpClientInitializer = credential,
          ApplicationName = "Freelance Manager",
        });
        return true;
      }
      catch (Exception err)
      {
        MessageBox.Show(err.Message);
        return false;
      }
    }
    public DriveService getDriveService()
    {
      return service;
    }
  }
}
