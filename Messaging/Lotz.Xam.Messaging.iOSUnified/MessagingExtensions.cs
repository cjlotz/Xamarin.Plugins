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

            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(controller, true, () => {});
        }

        #endregion
    }
}