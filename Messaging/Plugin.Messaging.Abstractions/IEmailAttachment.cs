namespace Plugin.Messaging
{
    /// <summary>
    ///     Abstraction for adding an attachment to an e-mail
    /// </summary>
    public interface IEmailAttachment
    {
        /// <summary>
        ///     File name for the attachment
        /// </summary>
        string FileName { get; }

        /// <summary>
        ///     File path for the attachment
        /// </summary>
        string FilePath { get; }

        /// <summary>
        ///     Content type of the attachment
        /// </summary>
        string ContentType { get; }
    }
}