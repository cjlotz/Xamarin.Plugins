using System;
using System.Collections.Generic;

namespace Lotz.Xam.Messaging.Abstractions
{
    public interface IEmailTask
    {
        bool CanSendEmail { get; }
        void SendEmail(EmailMessageRequest email);
    }

    // TODO: Consider adding EmailBuilder
    // TODO: Consider adding Attachments
    // TODO: Consider HTML Body content

    public class EmailMessageRequest
    {
        private List<string> _recipientsBcc;
        private List<string> _recipientsCc;

        public EmailMessageRequest(string to)
        {
            if (string.IsNullOrWhiteSpace(to))
                throw new ArgumentNullException("to");

            Recipients = new List<string> { to };
        }

        public EmailMessageRequest(string to, string subject, string message)
            : this(to)
        {
            Subject = subject;
            Message = message;
        }

        #region Properties

        public string Message { get; set; }

        public List<string> Recipients { get; private set; }

        public List<string> RecipientsBcc
        {
            get { return _recipientsBcc ?? (_recipientsBcc = new List<string>()); }
            set { _recipientsBcc = value; }
        }

        public List<string> RecipientsCc
        {
            get { return _recipientsCc ?? (_recipientsCc = new List<string>()); }
            set { _recipientsCc = value; }
        }

        public string Subject { get; set; }

        #endregion
    }
}