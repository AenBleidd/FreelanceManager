using System;
using System.Windows.Forms;

// Google API
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Services;

namespace FreelanceManager.GoogleHelper
{
  public class fmGoogleDrive
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
