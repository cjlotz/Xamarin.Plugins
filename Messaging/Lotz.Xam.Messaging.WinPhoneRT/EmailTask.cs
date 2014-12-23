using System;
using System.Linq;
using Windows.ApplicationModel.Email;

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

        public void SendEmail(IEmailMessage email)
        {
            if (email == null)
                throw new ArgumentNullException("email");

            if (CanSendEmail)
            {
                var mail = new Windows.ApplicationModel.Email.EmailMessage
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

                foreach (var attachment in email.Attachments.Cast<EmailAttachment>())
                {
                    mail.Attachments.Add(new Windows.ApplicationModel.Email.EmailAttachment(
                        attachment.FileName, attachment.Content));
                }

                EmailManager.ShowComposeNewEmailAsync(mail);
            }
        }

        public void SendEmail(string to, string subject, string message)
        {
            SendEmail(new EmailMessage(to, subject, message));
        }

        #endregion
    }
}