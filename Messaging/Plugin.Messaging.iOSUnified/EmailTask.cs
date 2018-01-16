using System;
using System.Linq;
using Foundation;
using MessageUI;

namespace Plugin.Messaging
{
    internal class EmailTask : IEmailTask
    {
        public EmailTask(EmailSettings settings)
        {
            Settings = settings;
        }

        //private MFMailComposeViewController _mailController;

        #region IEmailTask Members

        public bool CanSendEmail => MFMailComposeViewController.CanSendMail;
        public bool CanSendEmailAttachments => true;
        public bool CanSendEmailBodyAsHtml => true;

        public void SendEmail(IEmailMessage email)
        {
            if (email == null)
                throw new ArgumentNullException(nameof(email));

            if (CanSendEmail)
            {
                 MFMailComposeViewController _mailController;                
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
                    if (attachment.File == null)
                        _mailController.AddAttachmentData(NSData.FromFile(attachment.FilePath), attachment.ContentType, attachment.FileName);
                    else
                        _mailController.AddAttachmentData(NSData.FromUrl(attachment.File), attachment.ContentType, attachment.FileName);
                }

                Settings.EmailPresenter.PresentMailComposeViewController(_mailController);
            }
        }

        public void SendEmail(string to, string subject, string message)
        {
            SendEmail(new EmailMessage(to, subject, message));
        }

        #endregion

        #region Properties

        private EmailSettings Settings { get; }

        #endregion
    }
}