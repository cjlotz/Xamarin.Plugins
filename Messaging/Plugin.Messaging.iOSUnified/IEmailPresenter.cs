using System;
using MessageUI;
using UIKit;

namespace Plugin.Messaging
{
    public interface IEmailPresenter
    {
        void PresentMailComposeViewController(MFMailComposeViewController mailController);
    }

    public class DefaultEmailPresenter : IEmailPresenter
    {
        public void PresentMailComposeViewController(MFMailComposeViewController mailController)
        {
            EventHandler<MFComposeResultEventArgs> handler = null;
            handler = (sender, e) =>
            {
                mailController.Finished -= handler;

                var uiViewController = sender as UIViewController;
                if (uiViewController == null)
                {
                    throw new ArgumentException("sender");
                }

                uiViewController.DismissViewController(true, () => { });
            };

            mailController.Finished += handler;
            mailController.PresentUsingRootViewController();
        }
    }
}