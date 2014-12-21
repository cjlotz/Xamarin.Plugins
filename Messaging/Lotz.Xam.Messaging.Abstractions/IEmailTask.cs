namespace Lotz.Xam.Messaging.Abstractions
{
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
}