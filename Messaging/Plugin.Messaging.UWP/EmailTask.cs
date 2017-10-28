using System;
using System.Linq;
using Windows.ApplicationModel.Email;
using Windows.Storage.Streams;

namespace Plugin.Messaging
{
    internal class EmailTask : IEmailTask
    {
        public EmailTask()
        {
        }

        #region IEmailTask Members

        public bool CanSendEmail => true;

        public bool CanSendEmailAttachments => true;

        public bool CanSendEmailBodyAsHtml => false;

        public void SendEmail(IEmailMessage email)
        {
            if (email == null)
                throw new ArgumentNullException(nameof(email));

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
                    RandomAccessStreamReference streamRef = RandomAccessStreamReference.CreateFromFile(attachment.File);
                    mail.Attachments.Add(new Windows.ApplicationModel.Email.EmailAttachment(attachment.FileName, streamRef));
                }

#pragma warning disable 4014
                EmailManager.ShowComposeNewEmailAsync(mail);
#pragma warning restore 4014
            }
        }

        public void SendEmail(string to, string subject, string message)
        {
            SendEmail(new EmailMessage(to, subject, message));
        }

        #endregion
    }
}