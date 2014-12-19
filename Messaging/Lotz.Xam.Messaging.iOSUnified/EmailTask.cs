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
    public class EmailTask : IEmailTask
    {
        public EmailTask(IMessagingContext context)
        {
            _context = context.AsPlatformContext();
        }

        private readonly MessagingContext _context;
        private MFMailComposeViewController _mailController;

        #region IEmailTask Members

        public bool CanSendEmail
        {
            get { return MFMailComposeViewController.CanSendMail; }
        }

        public void SendEmail(EmailMessageRequest email)
        {
            if (email == null)
                throw new ArgumentNullException("email");

            if (CanSendEmail)
            {
                _mailController = new MFMailComposeViewController();
                _mailController.SetSubject(email.Subject);
                _mailController.SetMessageBody(email.Message, false);
                _mailController.SetToRecipients(email.Recipients.ToArray());

                if (email.RecipientsCc.Count > 0)
                    _mailController.SetCcRecipients(email.RecipientsCc.ToArray());

                if (email.RecipientsBcc.Count > 0)
                    _mailController.SetBccRecipients(email.RecipientsBcc.ToArray());

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
                };

                _mailController.Finished += handler;

                _context.ViewController.PresentViewController(_mailController, true, () => {});
            }
        }

        #endregion
    }
}