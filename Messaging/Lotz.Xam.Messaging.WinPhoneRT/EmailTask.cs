using System;
using Windows.ApplicationModel.Email;
using Lotz.Xam.Messaging.Abstractions;

namespace Lotz.Xam.Messaging
{
    public class EmailTask : IEmailTask
    {
        public EmailTask(IMessagingContext context)
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
                var mail = new EmailMessage();
                mail.Subject = email.Subject;
                mail.Body = email.Message;

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

        #endregion
    }
}