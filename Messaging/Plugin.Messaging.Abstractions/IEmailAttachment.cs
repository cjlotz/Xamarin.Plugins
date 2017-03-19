namespace Plugin.Messaging
{
    public interface IEmailAttachment
    {
        string FileName { get; }
        string FilePath { get; }
        string ContentType { get; }
    }
}