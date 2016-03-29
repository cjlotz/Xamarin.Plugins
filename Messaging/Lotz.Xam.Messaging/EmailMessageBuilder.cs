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
            if (!string.IsNullOrEmpty(htmlBody))
            {                
                _email.Message = htmlBody;
                _email.IsHtml = true;
            }

            return this;
        }

        public EmailMessageBuilder WithAttachment(string filePath, string contentType)
        {
#if WINDOWS_PHONE_APP || WINDOWS_UWP
            throw new PlatformNotSupportedException("API not supported on platform. Use WithAttachment(Windows.Storage.IStorageFile file) overload instead");
#else
            _email.Attachments.Add(new EmailAttachment(filePath, contentType));
            return this;
#endif
        }

#if __ANDROID__

        /// <summary>
        ///     Add the file located at <paramref name="filePath"/> as an attachment
        /// </summary>
        /// <param name="filePath">Full path to the file to attach</param>
        public EmailMessageBuilder WithAttachment(string filePath)
        {
            _email.Attachments.Add(new EmailAttachment(filePath));
            return this;
        }

#elif __IOS__

        /// <summary>
        ///     Add the <paramref name="content"/> as an attachment to the email.
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <param name="content">File content</param>
        /// <param name="contentType">File content type (image/jpeg etc.)</param>
        public EmailMessageBuilder WithAttachment(string fileName, System.IO.Stream content, string contentType)
        {
            _email.Attachments.Add(new EmailAttachment(fileName, content, contentType));
            return this;
        }

#elif WINDOWS_PHONE_APP || WINDOWS_UWP

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