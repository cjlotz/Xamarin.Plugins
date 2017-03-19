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

        public EmailAttachment(Java.IO.File file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(file.Path);
            string contentType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);

            File = file;
            FilePath = file.Path;
            FileName = file.Name;
            ContentType = contentType;
        }

        public Java.IO.File File { get; }

#elif __IOS__

        public EmailAttachment(Foundation.NSUrl file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            File = file;
            FilePath = file.Path;
            FileName = Foundation.NSFileManager.DefaultManager.DisplayName(file.Path);
            string id = MobileCoreServices.UTType.CreatePreferredIdentifier(MobileCoreServices.UTType.TagClassFilenameExtension, file.PathExtension, null);
            ContentType = MobileCoreServices.UTType.CopyAllTags(id, MobileCoreServices.UTType.TagClassMIMEType)[0];
        }

        public Foundation.NSUrl File { get; }
#endif

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

        #region IEmailAttachment Members

        public string ContentType { get; }
        public string FileName { get; }
        public string FilePath { get; }

        #endregion
    }
}