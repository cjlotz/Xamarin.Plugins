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
        /// <param name="context">The messaging context to use on the platform.  Use the platform
        /// specific implementation of the <see cref="IMessagingContext"/></param>
        /// <returns>A instance of platform  implementation for the <see cref="IEmailTask"/></returns>
        public static IEmailTask EmailMessenger(IMessagingContext context)
        {
#if PORTABLE
            throw NotImplementedInReferenceAssembly();
#else
            return new EmailTask(context);
#endif
        }

        /// <summary>
        /// Gets an instance of the platform implementation for the <see cref="IPhoneCallTask"/>
        /// </summary>
        /// <param name="context">The messaging context to use on the platform.  Use the platform
        /// specific implementation of the <see cref="IMessagingContext"/></param>
        /// <returns>A instance of platform  implementation for the <see cref="IPhoneCallTask"/></returns>
        public static IPhoneCallTask PhoneDialer(IMessagingContext context)
        {
#if PORTABLE
            throw NotImplementedInReferenceAssembly();
#else
            return new PhoneCallTask(context);
#endif
        }

        /// <summary>
        /// Gets an instance of the platform implementation for the <see cref="ISmsTask"/>
        /// </summary>
        /// <param name="context">The messaging context to use on the platform.  Use the platform
        /// specific implementation of the <see cref="IMessagingContext"/></param>
        /// <returns>A instance of platform  implementation for the <see cref="ISmsTask"/></returns>
        public static ISmsTask SmsMessenger(IMessagingContext context)
        {
#if PORTABLE
            throw NotImplementedInReferenceAssembly();
#else
            return new SmsTask(context);
#endif
        }

        internal static Exception NotImplementedInReferenceAssembly()
        {
            return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the Xam.Plugins.Messaging NuGet package from your main application project in order to reference the platform-specific implementation.");
        }
    }
}