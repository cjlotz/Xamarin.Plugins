using Lotz.Xam.Messaging.Abstractions;
using UIKit;

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