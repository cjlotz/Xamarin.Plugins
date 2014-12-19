using Lotz.Xam.Messaging.Abstractions;
#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;
#endif

namespace Lotz.Xam.Messaging
{
    public class MessagingContext : IMessagingContext
    {
        public MessagingContext(UIViewController viewController)
        {
            ViewController = viewController;
        }

        public UIViewController ViewController { get; private set; }
    }
}