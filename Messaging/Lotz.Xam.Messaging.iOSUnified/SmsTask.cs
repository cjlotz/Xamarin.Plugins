using System;
#if __UNIFIED__
using MessageUI;
using UIKit;
#else
using MonoTouch.MessageUI;
using MonoTouch.UIKit;
#endif

namespace Lotz.Xam.Messaging
{
    internal class SmsTask : ISmsTask
    {
        private MFMessageComposeViewController _smsController;

        public SmsTask()
        {
        }

        #region ISmsTask Members

        public bool CanSendSms
        {
            get { return MFMessageComposeViewController.CanSendText; }
        }

        public void SendSms(string recipient = null, string message = null)
        {
            message = message ?? string.Empty;

            if (CanSendSms)
            {
                _smsController = new MFMessageComposeViewController();

                if (!string.IsNullOrWhiteSpace(recipient))
                    _smsController.Recipients = new[] { recipient };
                
                _smsController.Body = message;

                EventHandler<MFMessageComposeResultEventArgs> handler = null;
                handler = (sender, args) =>
                {
                    _smsController.Finished -= handler;

                    var uiViewController = sender as UIViewController;
                    if (uiViewController == null)
                    {
                        throw new ArgumentException("sender");
                    }

                    uiViewController.DismissViewController(true, () => { });
                };

                _smsController.Finished += handler;

                _smsController.PresentUsingRootViewController();
            }
        }

        #endregion
    }
}