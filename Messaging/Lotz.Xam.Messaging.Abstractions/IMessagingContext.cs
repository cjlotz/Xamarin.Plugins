namespace Lotz.Xam.Messaging.Abstractions
{
    /// <summary>
    ///     Abstraction implemented by the different platforms to send through the
    ///     messaging context (e.g. current Activity on Android or UIViewController on iOS)
    ///     required for successfully executing the messaging plugin tasks
    /// </summary>
    public interface IMessagingContext
    {
    }
}