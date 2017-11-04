using System;

namespace Plugin.Messaging
{
    /// <summary>
    /// Cross platform Messaging implementation
    /// </summary>
    public static class CrossMessaging
    {
        private static readonly Lazy<IMessaging> _implementation = new Lazy<IMessaging>(CreateMessaging, System.Threading.LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Gets if the plugin is supported on the current platform.
        /// </summary>
        public static bool IsSupported => _implementation.Value != null; 

        /// <summary>
        ///     Get singleton <see cref="IMessaging"/> instance
        /// </summary>
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
#if NETSTANDARD1_0
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
#if NETSTANDARD1_0
            EmailMessenger = null;
            PhoneDialer = null;
            SmsMessenger = null;
#elif __ANDROID__
            Settings = new Settings();
            EmailMessenger = new EmailTask(Settings.Email);
            PhoneDialer = new PhoneCallTask(Settings.Phone);
            SmsMessenger = new SmsTask();            
#else
            EmailMessenger = new EmailTask();
            PhoneDialer = new PhoneCallTask();
            SmsMessenger = new SmsTask();
#endif
        }

#if __ANDROID__
        public Settings Settings { get; }
#endif
        public IEmailTask EmailMessenger { get; }
        public IPhoneCallTask PhoneDialer { get; }
        public ISmsTask SmsMessenger { get; }
  
    }
}