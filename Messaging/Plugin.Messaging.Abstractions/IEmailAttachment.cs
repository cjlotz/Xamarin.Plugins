namespace Plugin.Messaging
{
    public interface IEmailAttachment
    {
        string FileName { get; }
        string ContentType { get; }
    }
}