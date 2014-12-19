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
    public class SmsTask : ISmsTask
    {
        private readonly MessagingContext _context;
        private MFMessageComposeViewController _smsController;

        public SmsTask(IMessagingContext context)
        {
            _context = context.AsPlatformContext();
        }

        #region ISmsTask Members

        public bool CanSendSms
        {
            get { return MFMessageComposeViewController.CanSendText; }
        }

        public void SendSms(SmsMessageRequest sms)
        {
            if (CanSendSms)
            {
                _smsController = new MFMessageComposeViewController();

                _smsController.Recipients = new[] { sms.DestinationAddress };
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

                _context.ViewController.PresentViewController(_smsController, true, () => {});
            }
        }

        #endregion
    }
}