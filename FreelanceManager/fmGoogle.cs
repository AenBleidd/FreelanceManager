using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

// Google API
using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace FreelanceManager
{
  class fmGoogle
  {
    UserCredential credential = null;
    Userinfoplus userInfo = null;
    public bool GoogleConnect()
    {
      try
      {
        credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
          new ClientSecrets
          {
            ClientId = "1062892910030-7gcc7j699m5dkt969fae9rh3a8ukodbd.apps.googleusercontent.com",
            ClientSecret = "gujgOgQ6prDWrPB_VloIu7g5",
          },
          new[] { 
            DriveService.Scope.Drive, 
            GmailService.Scope.GmailCompose, 
            Oauth2Service.Scope.UserinfoEmail
          },
          "user",
          CancellationToken.None).Result;       
      }
      catch (AggregateException err)
      {
        MessageBox.Show(err.InnerException.Message);
        return false;
      }
      return true;
    }
    public UserCredential getUserCredential()
    {
      return credential;
    }

    private void getUserInfo()
    {
      bool result = true;
      if (credential == null)
      {
        result = GoogleConnect();
      }

      if (result == false || credential == null)
      {
        return;
      }

      Oauth2Service service = new Oauth2Service(
        new BaseClientService.Initializer()
        {
          HttpClientInitializer = credential,
          ApplicationName = Program.getProgramName(),
        });
      userInfo = service.Userinfo.Get().Execute();
    }

    public string getUserEmailAddress()
    {
      if (userInfo == null)
      {
        getUserInfo();
      }

      if (userInfo == null)
      {
        return null;
      }

      return userInfo.Email;
    }

  }
}
