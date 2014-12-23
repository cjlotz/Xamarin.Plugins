using System;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Lotz.Xam.Messaging
{
    public class EmailAttachment : IEmailAttachment
    {
        public EmailAttachment(IStorageFile file)
        {
            if (file == null)
                throw new ArgumentNullException("file");

            FileName = file.Name;
            Content = RandomAccessStreamReference.CreateFromFile(file);
        }

        #region IEmailAttachment Members

        public string FileName { get; private set; }
        public IRandomAccessStreamReference Content { get; private set; }

        #endregion
    }
}