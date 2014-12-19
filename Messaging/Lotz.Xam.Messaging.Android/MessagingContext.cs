using Android.App;
using Lotz.Xam.Messaging.Abstractions;

namespace Lotz.Xam.Messaging
{
    /// <summary>
    ///     Android context containing the current <see cref="Activity"/>
    /// </summary>
    public class MessagingContext : IMessagingContext
    {
        public MessagingContext(Activity activity)
        {
            Activity = activity;
        }

        public Activity Activity { get; private set; }
    }
}