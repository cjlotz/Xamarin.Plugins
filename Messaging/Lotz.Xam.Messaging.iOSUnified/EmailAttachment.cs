using System;
using System.IO;

namespace Lotz.Xam.Messaging
{
    public class EmailAttachment : IEmailAttachment
    {
        public EmailAttachment(string fileName, Stream content, string contentType)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException("fileName");

            if (content == null)
                throw new ArgumentNullException("content");

            if (string.IsNullOrWhiteSpace(contentType))
                throw new ArgumentNullException("contentType");

            FileName = fileName;
            Content = content;
            ContentType = contentType;
        }

        #region IEmailAttachment Members

        public string FileName { get; private set; }
        public Stream Content { get; private set; }
        public string ContentType { get; private set; }

        #endregion
    }
}