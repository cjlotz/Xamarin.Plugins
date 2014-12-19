using System;
using System.Collections.Generic;
using System.Linq;
using Lotz.Xam.Messaging.Abstractions;
using Microsoft.Phone.Tasks;

namespace Lotz.Xam.Messaging
{
    public class EmailTask : IEmailTask
    {
        public EmailTask(IMessagingContext context)
        {
        }

        #region IEmailTask Members

        public bool CanSendEmail { get { return true; } }

        public void SendEmail(EmailMessageRequest email)
        {
            if (email == null)
                throw new ArgumentNullException("email");

            if (CanSendEmail)
            {
                EmailComposeTask emailComposeTask = new EmailComposeTask
                                                    {
                                                        Subject = email.Subject,
                                                        Body = email.Message,
                                                        To = ToDelimitedString(email.Recipients, ";"),
                                                        Cc = ToDelimitedString(email.RecipientsCc, ";"),
                                                        Bcc = ToDelimitedString(email.RecipientsBcc, ";")
                                                    };
                emailComposeTask.Show();
            }
        }

        #endregion

        #region Methods

        private static string ToDelimitedString(IList<string> collection, string delimiter)
        {
            string delimited = string.Empty;
            return collection.Aggregate(delimited, (current, stringValue) => current + (stringValue + delimiter));
        }

        #endregion
    }
}