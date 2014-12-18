using System;
using System.Threading.Tasks;
using Android.Content;
using Lotz.Xam.Messaging.Abstractions;

namespace Lotz.Xam.Messaging
{
    public class EmailTask : IEmailTask
    {
        private readonly MessagingContext _context;

        public EmailTask(IMessagingContext context)
        {
            _context = context.AsPlatformContext();
        }

        #region IEmailTask Members

        public Task SendEmailAsync(EmailMessageRequest email)
        {
            // NOTE: http://developer.xamarin.com/recipes/android/networking/email/send_an_email/

            Intent emailIntent = new Intent(Intent.ActionSend);
            emailIntent.SetType("message/rfc822");

            emailIntent.PutExtra(Intent.ExtraEmail, email.Recipients.ToArray());
            emailIntent.PutExtra(Intent.ExtraCc, email.RecipientsCc.ToArray());
            emailIntent.PutExtra(Intent.ExtraBcc, email.RecipientsBcc.ToArray());
            emailIntent.PutExtra(Intent.ExtraSubject, email.Subject);
            emailIntent.PutExtra(Intent.ExtraText, email.Message);

            _context.Activity.StartActivity(emailIntent);

            return Task.FromResult<object>(null);
        }

        #endregion
    }
}