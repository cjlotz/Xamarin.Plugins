using System;
using System.Threading.Tasks;
using Lotz.Xam.Messaging.Abstractions;
using MessageUI;
using UIKit;

namespace Lotz.Xam.Messaging
{
    public class EmailTask : IEmailTask
    {
        public EmailTask(IMessagingContext context)
        {
            _context = context.AsPlatformContext();
        }

        private readonly MessagingContext _context;
        private MFMailComposeViewController _mailController;

        #region IEmailTask Members

        public Task SendEmailAsync(EmailMessageRequest email)
        {
            var tcs = new TaskCompletionSource<object>();

            if (MFMailComposeViewController.CanSendMail)
            {
                _mailController = new MFMailComposeViewController();

                _mailController.SetToRecipients(email.Recipients.ToArray());
                _mailController.SetSubject(email.Subject);
                _mailController.SetMessageBody(email.Message, false);

                EventHandler<MFComposeResultEventArgs> handler = null;
                handler = (sender, e) =>
                {
                    _mailController.Finished -= handler;

                    var uiViewController = sender as UIViewController;
                    if (uiViewController == null)
                    {
                        throw new ArgumentException("sender");
                    }

                    uiViewController.DismissViewController(true, () => { });
                    tcs.SetResult(null);
                };

                _mailController.Finished += handler;

                _context.ViewController.PresentViewController(_mailController, true, () => {});
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