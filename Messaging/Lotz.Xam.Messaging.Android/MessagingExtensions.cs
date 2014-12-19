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

            var androidContext = context as MessagingContext;

            if (androidContext == null)
                throw new ArgumentException("Context not of type Android MessagingContext", "context");

            return androidContext;
        }

        #endregion
    }
}