using System;
using System.Collections.Generic;

namespace Plugin.Messaging
{
    /// <summary>
    ///     Builder pattern for constructing a <see cref="EmailMessage" />
    /// </summary>
    public class EmailMessageBuilder
    {
        private readonly EmailMessage _email;

        /// <summary>
        ///     Create instance of message builder to construct e-mail message
        /// </summary>
        public EmailMessageBuilder()
        {
            _email = new EmailMessage();
        }

        #region Methods

        /// <summary>
        ///     Add <paramref name="bcc"/> to Bcc recipients of e-mail message
        /// </summary>
        /// <param name="bcc">Email address of recipient to Bcc on e-mail message</param>
        /// <returns></returns>
        public EmailMessageBuilder Bcc(string bcc)
        {
            if (!string.IsNullOrWhiteSpace(bcc))
                _email.RecipientsBcc.Add(bcc);

            return this;
        }

        /// <summary>
        ///     Add <paramref name="bcc"/> to Bcc recipients of e-mail message
        /// </summary>
        /// <param name="bcc">Email addresses of recipient to Bcc on e-mail message</param>
        /// <returns></returns>
        public EmailMessageBuilder Bcc(IEnumerable<string> bcc)
        {
            _email.RecipientsBcc.AddRange(bcc);
            return this;
        }

        /// <summary>
        ///     Set the body (text) of the e-mail message
        /// </summary>
        /// <param name="body">Text of the e-mail message</param>
        /// <returns></returns>
        public EmailMessageBuilder Body(string body)
        {
            if (!string.IsNullOrEmpty(body))
                _email.Message = body;

            return this;
        }

        /// <summary>
        ///     Set the body (text) of the e-mail message as HTML snippet
        /// </summary>
        /// <param name="htmlBody">Html text of the e-mail message</param>
        /// <returns></returns>
        public EmailMessageBuilder BodyAsHtml(string htmlBody)
        {
#if __ANDROID__ || __IOS__
            if (!string.IsNullOrEmpty(htmlBody))
            {                
                _email.Message = htmlBody;
                _email.IsHtml = true;
            }

            return this;
#else
            throw new PlatformNotSupportedException("API not supported on platform. Use IEmailTask.CanSendEmailBodyAsHtml to check availability");
#endif
        }

        /// <summary>
        ///     Add the file located at <paramref name="filePath"/> as an attachment
        /// </summary>
        /// <param name="filePath">Full path to the file to attach</param>
        /// <param name="contentType">File content type (image/jpeg etc.)</param>
#if WINDOWS_PHONE_APP || WINDOWS_UWP
        /// <remarks>On Windows, apps cannot access files by <paramref name="filePath"/> unless they reside in <see cref="Windows.Storage.ApplicationData"/>. To attach any other file, use
        /// <see cref="WithAttachment(Windows.Storage.IStorageFile)"/> overload.
        /// </remarks>            
#endif
        public EmailMessageBuilder WithAttachment(string filePath, string contentType)
        {
#if WINDOWS_PHONE_APP || WINDOWS_UWP

            var file = System.Threading.Tasks.Task.Run(async () =>
            {
                try
                {
                    var f = await Windows.Storage.StorageFile.GetFileFromPathAsync(filePath).AsTask().ConfigureAwait(false);
                    return f;
                }
                catch (UnauthorizedAccessException)
                {
                    throw new PlatformNotSupportedException("Windows apps cannot access files by filePath unless they reside in ApplicationData. Use the platform-specific WithAttachment(IStorageFile) overload instead.");
                }
            }).Result;

            _email.Attachments.Add(new EmailAttachment(file));
            return this;
#else
            _email.Attachments.Add(new EmailAttachment(filePath, contentType));
            return this;
#endif
        }

#if __ANDROID__

        /// <summary>
        ///     Add the <paramref name="file"/> as an attachment
        /// </summary>
        /// <param name="file">File to attach</param>
        public EmailMessageBuilder WithAttachment(Java.IO.File file)
        {
            _email.Attachments.Add(new EmailAttachment(file));
            return this;
        }

#elif __IOS__

        /// <summary>
        ///     Add the <paramref name="file"/> as an attachment
        /// </summary>
        /// <param name="file">File to attach</param>
        public EmailMessageBuilder WithAttachment(Foundation.NSUrl file)
        {
            _email.Attachments.Add(new EmailAttachment(file));
            return this;
        }

#elif WINDOWS_PHONE_APP || WINDOWS_UWP

        /// <summary>
        ///     Add the <paramref name="file"/> as an attachment
        /// </summary>
        /// <param name="file">File to attach</param>
        public EmailMessageBuilder WithAttachment(Windows.Storage.IStorageFile file)
        {
            _email.Attachments.Add(new EmailAttachment(file));
            return this;
        }

#endif

        /// <summary>
        ///     Create instance of <see cref="IEmailMessage"/>
        /// </summary>
        /// <returns>New <see cref="IEmailMessage"/></returns>
        public IEmailMessage Build()
        {
            return _email;
        }

        /// <summary>
        ///     Add <paramref name="cc"/> to Cc recipients of e-mail message
        /// </summary>
        /// <param name="cc">Email address of recipient to Cc on e-mail message</param>
        /// <returns></returns>
        public EmailMessageBuilder Cc(string cc)
        {
            if (!string.IsNullOrWhiteSpace(cc))
                _email.RecipientsCc.Add(cc);

            return this;
        }

        /// <summary>
        ///     Add <paramref name="cc"/> to Cc recipients of e-mail message
        /// </summary>
        /// <param name="cc">Email addresses of recipient to Cc on e-mail message</param>
        /// <returns></returns>
        public EmailMessageBuilder Cc(IEnumerable<string> cc)
        {
            _email.RecipientsCc.AddRange(cc);
            return this;
        }

        /// <summary>
        ///     Set the subject of the e-mail message
        /// </summary>
        /// <param name="subject">Subject of the e-mail message</param>
        /// <returns></returns>
        public EmailMessageBuilder Subject(string subject)
        {
            if (!string.IsNullOrEmpty(subject))
                _email.Subject = subject;

            return this;
        }

        /// <summary>
        ///     Add <paramref name="to"/> to To recipients of e-mail message
        /// </summary>
        /// <param name="to">Email address of recipient to send e-mail message to</param>
        /// <returns></returns>
        public EmailMessageBuilder To(string to)
        {
            if (!string.IsNullOrWhiteSpace(to))
                _email.Recipients.Add(to);

            return this;
        }

        /// <summary>
        ///     Add <paramref name="to"/> to To recipients of e-mail message
        /// </summary>
        /// <param name="to">Email addresses of recipients to send e-mail message to</param>
        /// <returns></returns>
        public EmailMessageBuilder To(IEnumerable<string> to)
        {
            _email.Recipients.AddRange(to);
            return this;
        }

        #endregion
    }
}