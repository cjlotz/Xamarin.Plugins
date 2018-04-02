using System;
using MessageUI;
using UIKit;

namespace Plugin.Messaging
{
    public interface ISmsPresenter
    {
        void PresentMessageComposeViewController(MFMessageComposeViewController smsController);
    }

    public class DefaultSmsPresenter : ISmsPresenter
    {
        public void PresentMessageComposeViewController(MFMessageComposeViewController smsController)
        {
            EventHandler<MFMessageComposeResultEventArgs> handler = null;
            handler = (sender, args) =>
            {
                smsController.Finished -= handler;

                if (!(sender is UIViewController uiViewController))
                {
                    throw new ArgumentException("sender");
                }

                uiViewController.DismissViewController(true, () => { });
            };

            smsController.Finished += handler;
            smsController.PresentUsingRootViewController();
        }
    }
}