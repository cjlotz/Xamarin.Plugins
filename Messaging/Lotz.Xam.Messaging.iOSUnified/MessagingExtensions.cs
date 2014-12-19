using System;
using Lotz.Xam.Messaging.Abstractions;

namespace Lotz.Xam.Messaging
{
    internal static class MessagingExtensions
    {
        #region Methods

        public static MessagingContext AsPlatformContext(this IMessagingContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var iOSContext = context as MessagingContext;

            if (iOSContext == null)
                throw new ArgumentException("Context not of type iOS MessagingContext", "context");

            return iOSContext;
        }

        #endregion
    }
}