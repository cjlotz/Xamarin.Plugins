using System;

namespace Plugin.Messaging
{
    /// <summary>
    ///     Container API for accessing the various Messaging plugin task API's
    /// </summary>
    [Obsolete("Use CrossMessaging instead")]
    public static class MessagingPlugin
    {
        #region Properties

        /// <summary>
        ///     Gets an instance of the platform implementation for the <see cref="IEmailTask" />
        /// </summary>
        public static IEmailTask EmailMessenger
        {
            get { return CrossMessaging.Current.EmailMessenger; }
        }

        /// <summary>
        ///     Gets an instance of the platform implementation for the <see cref="IPhoneCallTask" />
        /// </summary>
        public static IPhoneCallTask PhoneDialer
        {
            get { return CrossMessaging.Current.PhoneDialer; }
        }

        /// <summary>
        ///     Gets an instance of the platform implementation for the <see cref="ISmsTask" />
        /// </summary>
        public static ISmsTask SmsMessenger
        {
            get { return CrossMessaging.Current.SmsMessenger; }
        }

        #endregion

        #region Methods

        internal static Exception NotImplementedInReferenceAssembly()
        {
            return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the Xam.Plugins.Messaging NuGet package from your main application project in order to reference the platform-specific implementation.");
        }

        #endregion
    }

    /// <summary>
    /// Cross platform Messaging implementation
    /// </summary>
    public static class CrossMessaging
    {
        private static readonly Lazy<IMessaging> _implementation = new Lazy<IMessaging>(CreateMessaging, System.Threading.LazyThreadSafetyMode.PublicationOnly);

        public static IMessaging Current
        {
            get
            {
                var ret = _implementation.Value;
                if (ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }
                return ret;
            }
        }

        private static IMessaging CreateMessaging()
        {
#if PORTABLE
            return null;
#else
            return new MessagingImplementation();
#endif
        }

        internal static Exception NotImplementedInReferenceAssembly()
        {
            return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
        }
    }

    internal class MessagingImplementation : IMessaging
    {
        public MessagingImplementation()
        {
#if PORTABLE
            EmailMessenger = null;
            PhoneDialer = null;
            SmsMessenger = null;
#else
            EmailMessenger = new EmailTask();
            PhoneDialer = new PhoneCallTask();
            SmsMessenger = new SmsTask();
#endif
        }

        public IEmailTask EmailMessenger { get; }
        public IPhoneCallTask PhoneDialer { get; }
        public ISmsTask SmsMessenger { get; }
    }
}