using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public Task SendEmailAsync(EmailMessageRequest email)
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

            return Task.FromResult<object>(null);
        }

        private static string ToDelimitedString(IList<string> collection, string delimiter)
        {
            string delimited = string.Empty;
            return collection.Aggregate(delimited, (current, stringValue) => current + (stringValue + delimiter));
        }

        #endregion
    }
}