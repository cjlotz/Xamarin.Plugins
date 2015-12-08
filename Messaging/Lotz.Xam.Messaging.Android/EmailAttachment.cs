using System;

namespace Plugin.Messaging
{
    public class EmailAttachment : IEmailAttachment
    {
        public EmailAttachment(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException("filePath");

            FilePath = filePath;
        }

        #region IEmailAttachment Members

        public string FilePath { get; private set; }

        #endregion
    }
}