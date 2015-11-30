using System;
using System.Collections.Generic;
using System.Windows.Forms;

// Google API
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;

using GMailMessage = Google.Apis.Gmail.v1.Data.Message;

namespace FreelanceManager.GoogleHelper
{
  public class fmGMail
  {
    fmGoogle google = null;
    GmailService service = null;
    UserCredential credential = null;
    const string userId = "me";

    public fmGMail()
    {
      google = new fmGoogle();
      if (google.GoogleConnect())
      {
        credential = google.getUserCredential();
      }
    }

    public bool GMailConnect()
    {
      try
      {
        service = new GmailService(new BaseClientService.Initializer()
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

    public void SendMessage(string recepient, string subject, string body)
    {

      if (service == null)
      {
        if (GMailConnect() == false || service == null)
        {
          MessageBox.Show("Ошибка отправки сообщения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }

      string userMail = google.getUserEmailAddress();

      //MailMessage message = new MailMessage(userMail, recepient, subject, body);

      GMailMessage email = new GMailMessage();

      email.Payload = new MessagePart();
      email.Payload.Body = new MessagePartBody();
      email.Payload.Body.Data = body;
      email.Payload.Headers = new List<MessagePartHeader>();
      MessagePartHeader headerTo = new MessagePartHeader();
      MessagePartHeader headerFrom = new MessagePartHeader();
      MessagePartHeader headerSubject = new MessagePartHeader();
      headerTo.Name = "To";
      headerTo.Value = userMail;//recepient;
      headerFrom.Name = "From";
      headerFrom.Value = userMail;
      headerSubject.Name = "Subject";
      headerSubject.Value = "Счет за выполненные заказы";
      email.Payload.Headers.Add(headerTo);
      email.Payload.Headers.Add(headerFrom);
      email.Payload.Headers.Add(headerSubject);
      email.Payload.MimeType = "text/plain";
      
      
      try
      {
        service.Users.Messages.Send(email, userId).Execute();
      }
      catch (Exception e)
      {
        MessageBox.Show("An error occurred: " + e.Message);
      }

      return;
    }
  }
}
