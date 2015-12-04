using System;
#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;
#endif

namespace Lotz.Xam.Messaging
{
    internal static class MessagingExtensions
    {
        #region Methods

        public static void PresentUsingRootViewController(this UIViewController controller)
        {
            if (controller == null)
                throw new ArgumentNullException("controller");

            var visibleViewController = GetVisibleViewController(null);
            if (visibleViewController == null)
            {
                throw new InvalidOperationException("Could not find a visible UIViewController on the Window hierarchy");
            }
            visibleViewController.PresentViewController(controller, true, () => { });
        }

        private static UIViewController GetVisibleViewController(UIViewController controller)
        {
            if (controller == null)
            {
                controller = UIApplication.SharedApplication.KeyWindow.RootViewController;
            }

            if (controller.NavigationController != null)
            {
                if (controller.NavigationController.VisibleViewController != null)
                {
                    return controller.NavigationController.VisibleViewController;
                }
            }

            if (controller.IsViewLoaded && controller.View.Window != null)
            {
                return controller;
            }
            else
            {
                foreach (var childViewController in controller.ChildViewControllers)
                {
                    var foundVisibleViewController = GetVisibleViewController(childViewController);
                    if (foundVisibleViewController != null)
                    {
                        return foundVisibleViewController;
                    }
                }
            }
            return null;
        }

        #endregion
    }
}