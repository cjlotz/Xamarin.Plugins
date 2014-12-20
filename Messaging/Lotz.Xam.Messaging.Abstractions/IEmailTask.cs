using System;
using System.Collections.Generic;

namespace Lotz.Xam.Messaging.Abstractions
{
    // TODO: Consider adding EmailBuilder
    // TODO: Consider adding Attachments
    // TODO: Consider supporting HTML Body content

    /// <summary>
    ///     Abstraction for sending cross-platform email messages using
    ///     the default mail application on the device.
    /// </summary>
    public interface IEmailTask
    {
        /// <summary>
        ///     Gets a value indicating whether the device can send e-mails
        /// </summary>
        bool CanSendEmail { get; }

        /// <summary>
        ///     Send the <paramref name="email" /> using the default email application
        ///     on the device
        /// </summary>
        /// <param name="email">Email request to send</param>
        void SendEmail(EmailMessageRequest email);

        /// <summary>
        ///     Send an email using the default email application on the device
        /// </summary>
        /// <param name="to">Email recipient</param>
        /// <param name="subject">Email subject</param>
        /// <param name="message">Email message</param>
        void SendEmail(string to, string subject, string message);
    }

    /// <summary>
    ///     Email request used for sending e-mails.
    /// </summary>
    public class EmailMessageRequest
    {
        private List<string> _recipientsBcc;
        private List<string> _recipientsCc;

        /// <summary>
        ///     Create new email request
        /// </summary>
        /// <param name="to">Email recipient</param>
        public EmailMessageRequest(string to)
        {
            if (string.IsNullOrWhiteSpace(to))
                throw new ArgumentNullException("to");

            Recipients = new List<string> { to };
        }

        /// <summary>
        ///     Create new email request
        /// </summary>
        /// <param name="to">Email recipient</param>
        /// <param name="subject">Email subject</param>
        /// <param name="message">Email message</param>
        public EmailMessageRequest(string to, string subject, string message)
            : this(to)
        {
            Subject = subject;
            Message = message;
        }

        #region Properties

        /// <summary>
        ///     Email message body.  Currently supportes only text context.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     List of To recipients
        /// </summary>
        public List<string> Recipients { get; private set; }

        /// <summary>
        ///     List of Bcc recipients
        /// </summary>
        public List<string> RecipientsBcc
        {
            get { return _recipientsBcc ?? (_recipientsBcc = new List<string>()); }
            set { _recipientsBcc = value; }
        }

        /// <summary>
        ///     List of Bcc recipients
        /// </summary>
        public List<string> RecipientsCc
        {
            get { return _recipientsCc ?? (_recipientsCc = new List<string>()); }
            set { _recipientsCc = value; }
        }

        /// <summary>
        ///     Email subject
        /// </summary>
        public string Subject { get; set; }

        #endregion
    }
}