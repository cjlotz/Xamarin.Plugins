using System;
using System.IO;

namespace Plugin.Messaging
{
    public class EmailAttachment : IEmailAttachment
    {

#if WINDOWS_PHONE_APP || WINDOWS_UWP

        public EmailAttachment(Windows.Storage.IStorageFile file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            File = file;
            FilePath = file.Path;
            FileName = file.Name;
            ContentType = file.ContentType;
        }

        public Windows.Storage.IStorageFile File { get; }

#elif __ANDROID__

        public EmailAttachment(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath));

            string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(filePath);
            string contentType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);

            FilePath = filePath;
            FileName = Path.GetFileName(filePath);
            ContentType = contentType;
        }

#elif __IOS__

        public EmailAttachment(string fileName, Stream content, string contentType)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));

            if (content == null)
                throw new ArgumentNullException(nameof(content));

            if (string.IsNullOrWhiteSpace(contentType))
                throw new ArgumentNullException(nameof(contentType));

            FileName = fileName;
            Content = content;
            ContentType = contentType;
        }

        public Stream Content { get; }
#endif

#if !WINDOWS_PHONE_APP || !WINDOWS_UWP

        public EmailAttachment(string filePath, string contentType) 
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath));

            if (string.IsNullOrWhiteSpace(contentType))
                throw new ArgumentNullException(nameof(contentType));

            FilePath = filePath;
            FileName = Path.GetFileName(filePath);
            ContentType = contentType;
        }

#endif

        #region IEmailAttachment Members

        public string ContentType { get; }
        public string FileName { get; }
        public string FilePath { get; }

        #endregion
    }
}