using System;
using System.Collections.Generic;

namespace Lotz.Xam.Messaging.Abstractions
{
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
            Subject = string.Empty;
            Message = string.Empty;
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
            Subject = subject ?? string.Empty;
            Message = message ?? string.Empty;
        }

        /// <summary>
        ///     Constructor used by the <see cref="EmailMessageBuilder"/>
        /// </summary>
        internal EmailMessageRequest()
        {
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
        ///     List of Cc recipients
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

        #region Methods

        /// <summary>
        ///     Gets a builder to construct a new <see cref="EmailMessageRequest"/> with.
        /// </summary>
        /// <returns><see cref="EmailMessageBuilder"/> instance to construct email message</returns>
        public static EmailMessageBuilder Builder()
        {
            return new EmailMessageBuilder();
        }

        #endregion
    }
}