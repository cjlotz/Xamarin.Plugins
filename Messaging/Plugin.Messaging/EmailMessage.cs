using System.Collections.Generic;

namespace Plugin.Messaging
{
    /// <summary>
    ///     Email used for sending e-mails.
    /// </summary>
    internal class EmailMessage : IEmailMessage
    {
        private List<string> _recipientsBcc;
        private List<string> _recipientsCc;
        private List<string> _recipients;
        private List<IEmailAttachment> _attachments;
 
        /// <summary>
        ///     Create new email request
        /// </summary>
        /// <param name="to">Email recipient</param>
        public EmailMessage(string to)
            : this()
        {
            if (!string.IsNullOrWhiteSpace(to))
                Recipients.Add(to);
        }

        /// <summary>
        ///     Create new email request
        /// </summary>
        /// <param name="to">Email recipient</param>
        /// <param name="subject">Email subject</param>
        /// <param name="message">Email message</param>
        public EmailMessage(string to = null, string subject = null, string message = null)
            : this(to)
        {
            Subject = subject ?? string.Empty;
            Message = message ?? string.Empty;
        }

        /// <summary>
        ///     Constructor used by the <see cref="EmailMessageBuilder"/>
        /// </summary>
        internal EmailMessage()
        {            
            Subject = string.Empty;
            Message = string.Empty;
        }

        #region Properties

        /// <summary>
        ///     Email message body.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Gets or set a value indicating whether the <see cref="Message"/> is HTML content
        ///     or plain text.
        /// </summary>
        /// <remarks>
        ///     HTML content type is only supported on Android and iOS platforms
        /// </remarks>
        public bool IsHtml { get; set; }

        /// <summary>
        ///     List of attachments.
        /// </summary>
        public List<IEmailAttachment> Attachments
        {
            get { return _attachments ?? (_attachments = new List<IEmailAttachment>()); }
            set { _attachments = value; }
        }

        /// <summary>
        ///     List of To recipients
        /// </summary>
        public List<string> Recipients
        {
            get { return _recipients ?? (_recipients = new List<string>()); }
            set { _recipients = value; }            
        }

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
    }
}