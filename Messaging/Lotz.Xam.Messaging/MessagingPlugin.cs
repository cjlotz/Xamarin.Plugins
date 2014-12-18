using System;
using Lotz.Xam.Messaging.Abstractions;

namespace Lotz.Xam.Messaging
{
    public static class MessagingPlugin 
    {
        public static IEmailTask EmailMessenger(IMessagingContext context = null)
        {
#if PORTABLE
            throw NotImplementedInReferenceAssembly();
#else
            return new EmailTask(context);
#endif
        }

        public static IPhoneCallTask PhoneDialer(IMessagingContext context = null)
        {
#if PORTABLE
            throw NotImplementedInReferenceAssembly();
#else
            return new PhoneCallTask(context);
#endif
        }

        public static ISmsTask SmsMessenger(IMessagingContext context = null)
        {
#if PORTABLE
            throw NotImplementedInReferenceAssembly();
#else
            return new SmsTask(context);
#endif
        }

        internal static Exception NotImplementedInReferenceAssembly()
        {
            return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the Xam.Plugins.TextToSpeech NuGet package from your main application project in order to reference the platform-specific implementation.");
        }
    }
}