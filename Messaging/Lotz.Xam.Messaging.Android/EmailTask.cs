using System;
using Android.Content;
using Android.Text;

namespace Lotz.Xam.Messaging
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
               return mgr.QueryIntentActivities (new Intent (Intent.ActionSend), Android.Content.PM.PackageInfoFlags.MatchDefaultOnly).Count > 0;
           }
       }

        public void SendEmail(IEmailMessage email)
        {
            // NOTE: http://developer.xamarin.com/recipes/android/networking/email/send_an_email/

            if (email == null)
                throw new ArgumentNullException("email");

            if (email.Attachments.Count > 1)
                throw new NotSupportedException("Cannot send more than once attachment for Android"); 

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

                if (((EmailMessage) email).IsHtml)
                    emailIntent.PutExtra(Intent.ExtraText, Html.FromHtml(email.Message));
                else
                    emailIntent.PutExtra(Intent.ExtraText, email.Message);

                if (email.Attachments.Count > 0)
                {
                    var attachment = (EmailAttachment) email.Attachments[0];
                    var uri = Android.Net.Uri.Parse("file://" + attachment.FilePath);
                    
                    emailIntent.PutExtra(Intent.ExtraStream, uri);
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