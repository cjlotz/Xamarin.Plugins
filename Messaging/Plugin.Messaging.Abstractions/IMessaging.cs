namespace Plugin.Messaging
{
    /// <summary>
    ///     Abstraction to access the <see cref="IEmailTask"/>, <see cref="IPhoneCallTask"/> and <see cref="ISmsTask"/> functionality
    /// </summary>
    public interface IMessaging
    {
        /// <summary>
        ///     Gets an instance of the platform implementation for the <see cref="IEmailTask" />
        /// </summary>
        IEmailTask EmailMessenger { get; }

        /// <summary>
        ///     Gets an instance of the platform implementation for the <see cref="IPhoneCallTask" />
        /// </summary>
        IPhoneCallTask PhoneDialer { get; }

        /// <summary>
        ///     Gets an instance of the platform implementation for the <see cref="ISmsTask" />
        /// </summary>
        ISmsTask SmsMessenger { get; }
    }
}