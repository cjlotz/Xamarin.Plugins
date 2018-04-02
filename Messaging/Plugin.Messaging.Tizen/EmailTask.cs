using System;
using System.Linq;
using Tizen.Messaging.Email;

namespace Plugin.Messaging
{
	internal class EmailTask : IEmailTask
	{
		public EmailTask()
		{
		}

		public bool CanSendEmail => true;

	    public bool CanSendEmailAttachments => true;

	    public bool CanSendEmailBodyAsHtml => false;

	    public async void SendEmail(IEmailMessage email)
		{
			if (email == null)
				throw new ArgumentNullException(nameof(email));

			if (CanSendEmail)
			{
				Tizen.Messaging.Email.EmailMessage emailMessage = new Tizen.Messaging.Email.EmailMessage();
				EmailRecipient emailRecipientTo = new EmailRecipient();
				foreach (var to in email.Recipients)
				{
					emailRecipientTo.Address = to;
					emailMessage.To.Add(emailRecipientTo);
				}
				emailMessage.Subject = email.Subject;
				emailMessage.Body = email.Message;
				Tizen.Messaging.Email.EmailAttachment emailAttachment = new Tizen.Messaging.Email.EmailAttachment();
				foreach (var attachment in email.Attachments.Cast<EmailAttachment>())
				{
					emailAttachment.FilePath = attachment.FilePath;
					emailMessage.Attachments.Add(emailAttachment);
				}

				await EmailSender.SendAsync(emailMessage);
			}
		}

		public void SendEmail(string to = null, string subject = null, string message = null)
		{
			SendEmail(new EmailMessage(to, subject, message));
		}
	}
}