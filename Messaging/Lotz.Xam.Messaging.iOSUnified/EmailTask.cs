using System;
using System.Linq;
#if __UNIFIED__
using Foundation;
using MessageUI;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.MessageUI;
using MonoTouch.UIKit;
#endif

namespace Plugin.Messaging
{
    internal class EmailTask : IEmailTask
    {
        public EmailTask()
        {
        }

        private MFMailComposeViewController _mailController;

        #region IEmailTask Members

        public bool CanSendEmail
        {
            get { return MFMailComposeViewController.CanSendMail; }
        }

        public void SendEmail(IEmailMessage email)
        {
            if (email == null)
                throw new ArgumentNullException("email");

            if (CanSendEmail)
            {
                _mailController = new MFMailComposeViewController();
                _mailController.SetSubject(email.Subject);
                _mailController.SetMessageBody(email.Message, ((EmailMessage) email).IsHtml);
                _mailController.SetToRecipients(email.Recipients.ToArray());

                if (email.RecipientsCc.Count > 0)
                    _mailController.SetCcRecipients(email.RecipientsCc.ToArray());

                if (email.RecipientsBcc.Count > 0)
                    _mailController.SetBccRecipients(email.RecipientsBcc.ToArray());

                foreach (var attachment in email.Attachments.Cast<EmailAttachment>())
                {
                    _mailController.AddAttachmentData(NSData.FromStream(attachment.Content),
                        attachment.ContentType, attachment.FileName);
                }

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

                _mailController.PresentUsingRootViewController();
            }
        }

        public void SendEmail(string to, string subject, string message)
        {
            SendEmail(new EmailMessage(to, subject, message));
        }

        #endregion
    }
}