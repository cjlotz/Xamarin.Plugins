using System;
using Android.Content;
using Android.Text;
using Lotz.Xam.Messaging.Abstractions;

namespace Lotz.Xam.Messaging
{
    internal class EmailTask : IEmailTask
    {
        public EmailTask()
        {
        }

        #region IEmailTask Members

        public bool CanSendEmail { get { return true; } }

        public void SendEmail(EmailMessageRequest email)
        {
            // NOTE: http://developer.xamarin.com/recipes/android/networking/email/send_an_email/

            if (email == null)
                throw new ArgumentNullException("email");

            if (CanSendEmail)
            {
                Intent emailIntent = new Intent(Intent.ActionSend);
                emailIntent.SetType("message/rfc822");

                if (email.Recipients.Count > 0)
                    emailIntent.PutExtra(Intent.ExtraEmail, email.Recipients.ToArray());

                if (email.RecipientsCc.Count > 0)
                    emailIntent.PutExtra(Intent.ExtraCc, email.RecipientsCc.ToArray());

                if (email.RecipientsBcc.Count > 0)
                    emailIntent.PutExtra(Intent.ExtraBcc, email.RecipientsBcc.ToArray());

                emailIntent.PutExtra(Intent.ExtraSubject, email.Subject);

                // NOTE: http://stackoverflow.com/questions/13756200/send-html-email-with-gmail-4-2-1

                if (email.IsHtml)
                    emailIntent.PutExtra(Intent.ExtraText, Html.FromHtml(email.Message));
                else
                    emailIntent.PutExtra(Intent.ExtraText, email.Message);

                emailIntent.StartNewActivity();
            }
        }

        public void SendEmail(string to, string subject, string message)
        {
            SendEmail(new EmailMessageRequest(to, subject, message));
        }

        #endregion
    }
}