using System;
using System.Threading.Tasks;
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
        private MessagingContext _context;
        private MFMessageComposeViewController _smsController;

        public SmsTask(IMessagingContext context)
        {
            _context = context.AsPlatformContext();
        }

        #region ISmsTask Members

        public Task SendSmsAsync(SmsMessageRequest sms)
        {
            var tcs = new TaskCompletionSource<object>();

            if (MFMessageComposeViewController.CanSendText)
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
                    tcs.SetResult(null);
                };

                _smsController.Finished += handler;

                _context.ViewController.PresentViewController(_smsController, true, () => {});
            }
            else
            {
                tcs.SetResult(null);
            }

            return tcs.Task;
        }

        #endregion
    }
}