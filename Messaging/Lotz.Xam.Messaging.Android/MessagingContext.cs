using Android.App;
using Lotz.Xam.Messaging.Abstractions;

namespace Lotz.Xam.Messaging
{
    public class MessagingContext : IMessagingContext
    {
        public MessagingContext(Activity activity)
        {
            Activity = activity;
        }

        public Activity Activity { get; private set; }
    }
}