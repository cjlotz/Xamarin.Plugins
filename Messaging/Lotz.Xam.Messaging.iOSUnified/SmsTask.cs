using System;
using Lotz.Xam.Messaging.Abstractions;
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

        public void SendSms(SmsMessageRequest sms)
        {
            if (sms == null)
                throw new ArgumentNullException("sms");

            if (CanSendSms)
            {
                _smsController = new MFMessageComposeViewController();

                _smsController.Recipients = new[] { sms.ReceiverAddress };
                _smsController.Body = sms.Message;

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