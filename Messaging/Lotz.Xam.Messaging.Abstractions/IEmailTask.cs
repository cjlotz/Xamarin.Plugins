namespace Plugin.Messaging
{
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
        ///     Gets a value indicating whether the device can send e-mail attachments
        /// </summary>
        bool CanSendEmailAttachments { get; }

        /// <summary>
        ///     Gets a value indicating whether the device can send the email body as html content
        /// </summary>
        bool CanSendEmailBodyAsHtml { get; }

        /// <summary>
        ///     Send the <paramref name="email" /> using the default email application
        ///     on the device
        /// </summary>
        /// <param name="email">Email to send</param>
        void SendEmail(IEmailMessage email);

        /// <summary>
        ///     Send an email using the default email application on the device
        /// </summary>
        /// <param name="to">Email recipient</param>
        /// <param name="subject">Email subject</param>
        /// <param name="message">Email message</param>        
        void SendEmail(string to = null, string subject = null, string message = null);
    }
}