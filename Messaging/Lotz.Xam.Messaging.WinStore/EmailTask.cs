using System;
using System.Diagnostics;
using Windows.System;
using Lotz.Xam.Messaging.Abstractions;

namespace Lotz.Xam.Messaging
{
    internal class EmailTask : IEmailTask
    {
        public EmailTask()
        {
        }

        #region IEmailTask Members

        public bool CanSendEmail
        {
            get { return true; }
        }

        public void SendEmail(EmailMessageRequest email)
        {
            if (email == null)
                throw new ArgumentNullException("email");

            if (CanSendEmail)
            {
                if (email.Recipients.Count > 0)
                    Debug.WriteLine("Can only send to single recipient on Window Store - using first recipient");

                if (email.RecipientsCc.Count > 0)
                    Debug.WriteLine("Cc recipients not supported on Windows Store - ignoring RecipientsCc");

                if (email.RecipientsBcc.Count > 0)
                    Debug.WriteLine("Bcc recipients not supported on Windows Store - ignoring RecipientsBcc");

                var emailText = string.Format(@"mailto:{0}?subject={1}&body={2}",
                    email.Recipients[0], email.Subject, email.Message);

                var escaped = Uri.EscapeUriString(emailText);

                Launcher.LaunchUriAsync(new Uri(escaped, UriKind.Absolute));
            }
        }

        public void SendEmail(string to, string subject, string message)
        {
            SendEmail(new EmailMessageRequest(to, subject, message));
        }

        #endregion
    }
}