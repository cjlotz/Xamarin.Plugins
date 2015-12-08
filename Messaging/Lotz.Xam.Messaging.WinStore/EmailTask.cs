using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Windows.System;

namespace Plugin.Messaging
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
                // NOTE: Refer to http://www.faqs.org/rfcs/rfc2368.html for info on mailto protocol

                if (email.RecipientsBcc.Count > 0)
                    Debug.WriteLine("Bcc headers are inherently unsafe to include in a message generated from a URL - ignoring RecipientBcc");

                var sb = new StringBuilder();

                sb.AppendFormat(CultureInfo.InvariantCulture, "mailto:{0}?", ToDelimitedAddress(email.Recipients));
                
                if (email.RecipientsCc.Count > 0)
                    sb.AppendFormat(CultureInfo.InvariantCulture, "cc={0}&", ToDelimitedAddress(email.RecipientsCc));

                sb.AppendFormat(CultureInfo.InvariantCulture, "subject={0}&body={1}",
                    email.Subject, email.Message);

                var escaped = Uri.EscapeUriString(sb.ToString());

                Launcher.LaunchUriAsync(new Uri(escaped, UriKind.Absolute));
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
            return collection.Count == 0 ? string.Empty : string.Join(",", collection);
        }

        #endregion

    }
}