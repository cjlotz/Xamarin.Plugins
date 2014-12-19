using System;
using Lotz.Xam.Messaging.Abstractions;

namespace Lotz.Xam.Messaging
{
    /// <summary>
    ///     Container API for accessing the various Messaging plugin API's
    /// </summary>
    public static class MessagingPlugin 
    {
        /// <summary>
        /// Gets an instance of the platform implementation for the <see cref="IEmailTask"/>
        /// </summary>
        public static IEmailTask EmailMessenger
        {
            get
            {
#if PORTABLE
                throw NotImplementedInReferenceAssembly();
#else
                return new EmailTask();
#endif
            }
        }

        /// <summary>
        /// Gets an instance of the platform implementation for the <see cref="IPhoneCallTask"/>
        /// </summary>
        public static IPhoneCallTask PhoneDialer
        {
            get
            {
#if PORTABLE
                throw NotImplementedInReferenceAssembly();
#else
                return new PhoneCallTask();
#endif
            }
        }

        /// <summary>
        /// Gets an instance of the platform implementation for the <see cref="ISmsTask"/>
        /// </summary>
        public static ISmsTask SmsMessenger
        {
            get
            {
#if PORTABLE
                throw NotImplementedInReferenceAssembly();
#else
                return new SmsTask();
#endif
            }
        }

        internal static Exception NotImplementedInReferenceAssembly()
        {
            return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the Xam.Plugins.Messaging NuGet package from your main application project in order to reference the platform-specific implementation.");
        }
    }
}