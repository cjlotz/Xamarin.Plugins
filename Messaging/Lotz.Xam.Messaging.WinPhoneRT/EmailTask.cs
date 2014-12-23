using System;
using Windows.ApplicationModel.Email;
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

            if (email.IsHtml)
                throw new PlatformNotSupportedException("Sending HTML email not supported for Windows Phone RT");

            if (CanSendEmail)
            {
                var mail = new EmailMessage
                           {
                               Subject = email.Subject, 
                               Body = email.Message
                           };

                foreach (var recipient in email.Recipients)
                {
                    mail.To.Add(new EmailRecipient(recipient));
                }
                foreach (var recipient in email.RecipientsCc)
                {
                    mail.CC.Add(new EmailRecipient(recipient));
                }
                foreach (var recipient in email.RecipientsBcc)
                {
                    mail.Bcc.Add(new EmailRecipient(recipient));
                }

                EmailManager.ShowComposeNewEmailAsync(mail);
            }
        }

        public void SendEmail(string to, string subject, string message)
        {
            SendEmail(new EmailMessageRequest(to, subject, message));
        }

        #endregion
    }
}