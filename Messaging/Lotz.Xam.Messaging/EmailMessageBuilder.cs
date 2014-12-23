using System.Collections.Generic;

namespace Lotz.Xam.Messaging
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
                _email.RecipientsCc.Add(bcc);

            return this;
        }

        public EmailMessageBuilder Bcc(IEnumerable<string> bcc)
        {
            _email.RecipientsCc.AddRange(bcc);
            return this;
        }

        public EmailMessageBuilder Body(string body)
        {
            if (!string.IsNullOrEmpty(body))
                _email.Message = body;

            return this;
        }

#if __ANDROID__ || __IOS__

        public EmailMessageBuilder BodyAsHtml(string htmlBody)
        {
            if (!string.IsNullOrEmpty(htmlBody))
            {                
                _email.Message = htmlBody;
                _email.IsHtml = true;
            }

            return this;
        }

#endif

#if __ANDROID__

        public EmailMessageBuilder WithAttachment(string filePath)
        {
            _email.Attachments.Add(new EmailAttachment(filePath));
            return this;
        }

#elif __IOS__


        public EmailMessageBuilder WithAttachment(string fileName, System.IO.Stream content, string contentType)
        {
            _email.Attachments.Add(new EmailAttachment(fileName, content, contentType));
            return this;
        }

#elif WINDOWS_PHONE_APP

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