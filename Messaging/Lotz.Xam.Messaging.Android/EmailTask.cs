using System;
using System.Collections.Generic;
using Android.Content;
using Android.OS;
using Android.Text;
using System.Linq;

namespace Plugin.Messaging
{
    internal class EmailTask : IEmailTask
    {
        public EmailTask()
        {
        }

        #region IEmailTask Members

        public bool CanSendEmail
        {
            get
            {
                var mgr = Android.App.Application.Context.PackageManager;
                var emailIntent = new Intent(Intent.ActionSend);
                emailIntent.SetType("message/rfc822");

                return emailIntent.ResolveActivity(mgr) != null;
            }
        }

        public bool CanSendEmailAttachments { get { return true; } }
        public bool CanSendEmailBodyAsHtml { get { return true; } }

        public void SendEmail(IEmailMessage email)
        {
            // NOTE: http://developer.xamarin.com/recipes/android/networking/email/send_an_email/

            if (email == null)
                throw new ArgumentNullException(nameof(email));

            if (CanSendEmail)
            {
                // NOTE: http://developer.android.com/guide/components/intents-common.html#Email

                string intentAction = Intent.ActionSend;
                if (email.Attachments.Count > 1)
                    intentAction = Intent.ActionSendMultiple;

                Intent emailIntent = new Intent(intentAction);
                emailIntent.SetType("message/rfc822");

                if (email.Recipients.Count > 0)
                    emailIntent.PutExtra(Intent.ExtraEmail, email.Recipients.ToArray());

                if (email.RecipientsCc.Count > 0)
                    emailIntent.PutExtra(Intent.ExtraCc, email.RecipientsCc.ToArray());

                if (email.RecipientsBcc.Count > 0)
                    emailIntent.PutExtra(Intent.ExtraBcc, email.RecipientsBcc.ToArray());

                emailIntent.PutExtra(Intent.ExtraSubject, email.Subject);

                // NOTE: http://stackoverflow.com/questions/13756200/send-html-email-with-gmail-4-2-1

                if (((EmailMessage)email).IsHtml)
                    emailIntent.PutExtra(Intent.ExtraText, Html.FromHtml(email.Message));
                else
                    emailIntent.PutExtra(Intent.ExtraText, email.Message);

                if (email.Attachments.Count > 0)
                {
                    var uris = new List<IParcelable>();
                    foreach (var attachment in email.Attachments.Cast<EmailAttachment>())
                    {
                        var uri = Android.Net.Uri.Parse("file://" + attachment.FilePath);
                        uris.Add(uri);
                    }

                    if (uris.Count > 1)
                        emailIntent.PutParcelableArrayListExtra(Intent.ExtraStream, uris);
                    else
                        emailIntent.PutExtra(Intent.ExtraStream, uris[0]);
                }

                emailIntent.StartNewActivity();
            }
        }

        public void SendEmail(string to, string subject, string message)
        {
            SendEmail(new EmailMessage(to, subject, message));
        }

        #endregion
    }
}