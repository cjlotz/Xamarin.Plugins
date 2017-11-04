using System;
using System.Linq;
#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;
#endif

namespace Plugin.Messaging
{
    internal static class MessagingExtensions
    {
        #region Methods

        public static void PresentUsingRootViewController(this UIViewController controller)
        {
            if (controller == null)
                throw new ArgumentNullException(nameof(controller));

            var visibleViewController = GetVisibleViewController(null);
            if (visibleViewController == null)
            {
                throw new InvalidOperationException("Could not find a visible UIViewController on the Window hierarchy");
            }
            visibleViewController.PresentViewController(controller, true, () => { });
        }

        private static UIViewController GetVisibleViewController(UIViewController controller)
        {
            UIViewController viewController = null;
            UIWindow window = UIApplication.SharedApplication.KeyWindow;

            if (window == null)
                throw new InvalidOperationException("There's no current active window");

            if (window.WindowLevel == UIWindowLevel.Normal)
                viewController = window.RootViewController;

            if (viewController == null)
            {
                window = UIApplication.SharedApplication.Windows.OrderByDescending(w => w.WindowLevel)
                                      .FirstOrDefault(w => w.RootViewController != null && w.WindowLevel == UIWindowLevel.Normal);
                if (window == null)
                    throw new InvalidOperationException("Could not find current view controller");
                else
                    viewController = window.RootViewController;
            }

            while (viewController.PresentedViewController != null)
                viewController = viewController.PresentedViewController;
            
            return viewController;
        }

        #endregion
    }
}