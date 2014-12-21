using System.Collections.Generic;

namespace Lotz.Xam.Messaging.Abstractions
{
    /// <summary>
    ///     Builder pattern for constructing a <see cref="EmailMessageRequest" />
    /// </summary>
    public class EmailMessageBuilder
    {
        private readonly EmailMessageRequest _email;

        public EmailMessageBuilder()
        {
            _email = new EmailMessageRequest();
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

        public EmailMessageRequest Build()
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

        public static implicit operator EmailMessageRequest(EmailMessageBuilder builder)
        {
            return builder.Build();
        }
    }
}