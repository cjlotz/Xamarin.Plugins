using System;
using Android.Content;
using Lotz.Xam.Messaging.Abstractions;
using Uri = Android.Net.Uri;

namespace Lotz.Xam.Messaging
{
    // NOTE: http://developer.xamarin.com/recipes/android/networking/sms/send_an_sms/

    public class SmsTask : ISmsTask
    {
        private readonly MessagingContext _context;

        public SmsTask(IMessagingContext context)
        {
            _context = context.AsPlatformContext();
        }

        #region ISmsTask Members

        public bool CanSendSms { get { return true; } }

        public void SendSms(SmsMessageRequest sms)
        {
            if (sms == null)
                throw new ArgumentNullException("sms");

            if (CanSendSms)
            {
                var smsUri = Uri.Parse("smsto:" + sms.DestinationAddress);
                var smsIntent = new Intent(Intent.ActionSendto, smsUri);
                smsIntent.PutExtra("sms_body", sms.Message);

                _context.Activity.StartActivity(smsIntent);
            }
        }

        #endregion
    }
}