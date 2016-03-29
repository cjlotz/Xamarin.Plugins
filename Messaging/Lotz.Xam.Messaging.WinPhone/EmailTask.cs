using System;
using System.Collections.Generic;
using Microsoft.Phone.Tasks;

namespace Plugin.Messaging
{
    internal class EmailTask : IEmailTask
    {
        public EmailTask()
        {
        }

        #region IEmailTask Members

        public bool CanSendEmail { get { return true; } }
        public bool CanSendEmailAttachments { get { return false; } }
        public bool CanSendEmailBodyAsHtml { get { return false; } }

        public void SendEmail(IEmailMessage email)
        {
            if (email == null)
                throw new ArgumentNullException(nameof(email));

            if (CanSendEmail)
            {
                EmailComposeTask emailComposeTask = new EmailComposeTask
                                                    {
                                                        Subject = email.Subject,
                                                        Body = email.Message,
                                                        To = ToDelimitedAddress(email.Recipients),
                                                        Cc = ToDelimitedAddress(email.RecipientsCc),
                                                        Bcc = ToDelimitedAddress(email.RecipientsBcc)
                                                    };
                emailComposeTask.Show();
            }
        }

        public void SendEmail(string to, string subject, string message)
        {
            SendEmail(new EmailMessage(to, subject, message));
        }

        #endregion

        #region Methods

        private static string ToDelimitedAddress(ICollection<string> collection)
        {
            return collection.Count == 0 ? string.Empty : string.Join(";", collection);
        }

        #endregion
    }
}