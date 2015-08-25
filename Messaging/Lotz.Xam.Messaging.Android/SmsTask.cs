using System;
using Android.Content;
using Uri = Android.Net.Uri;

namespace Lotz.Xam.Messaging
{
    // NOTE: http://developer.xamarin.com/recipes/android/networking/sms/send_an_sms/

    internal class SmsTask : ISmsTask
    {
        public SmsTask()
        {
        }

        #region ISmsTask Members

        public bool CanSendSms { get { return true; } }

        public void SendSms(string recipient, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException("message");

            if (CanSendSms)
            {
                Uri smsUri;
                if (!string.IsNullOrEmpty(recipient))
                    smsUri = Uri.Parse("smsto:" + recipient);
                else
                    smsUri = Uri.Parse("smsto:");
                
                var smsIntent = new Intent(Intent.ActionSendto, smsUri);
                smsIntent.PutExtra("sms_body", message);

                smsIntent.StartNewActivity();
            }
        }

        #endregion
    }
}