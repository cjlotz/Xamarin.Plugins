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

        public EmailMessageBuilder()
        {
            _email = new EmailMessage();
        }

        #region Methods

        public EmailMessageBuilder Bcc(string bcc)
        {
            if (!string.IsNullOrWhiteSpace(bcc))
                _email.RecipientsBcc.Add(bcc);

            return this;
        }

        public EmailMessageBuilder Bcc(IEnumerable<string> bcc)
        {
            _email.RecipientsBcc.AddRange(bcc);
            return this;
        }

        public EmailMessageBuilder Body(string body)
        {
            if (!string.IsNullOrEmpty(body))
                _email.Message = body;

            return this;
        }

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

        /// <summary>
        ///     Turn on strict mode, to ensure that the email can only be handled by email apps, 
        ///     and not also by any other text messaging or social apps
        /// </summary>
        /// <remarks>
        ///     Please note that sending attachments with strict mode is not supported
        /// </remarks>
        public EmailMessageBuilder UseStrictMode()
        {
            _email.StrictMode = true;
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

        public IEmailMessage Build()
        {
            return _email;
        }

        public EmailMessageBuilder Cc(string cc)
        {
            if (!string.IsNullOrWhiteSpace(cc))
                _email.RecipientsCc.Add(cc);

            return this;
        }

        public EmailMessageBuilder Cc(IEnumerable<string> cc)
        {
            _email.RecipientsCc.AddRange(cc);
            return this;
        }

        public EmailMessageBuilder Subject(string subject)
        {
            if (!string.IsNullOrEmpty(subject))
                _email.Subject = subject;

            return this;
        }

        public EmailMessageBuilder To(string to)
        {
            if (!string.IsNullOrWhiteSpace(to))
                _email.Recipients.Add(to);

            return this;
        }

        public EmailMessageBuilder To(IEnumerable<string> to)
        {
            _email.Recipients.AddRange(to);
            return this;
        }

        #endregion
    }
}