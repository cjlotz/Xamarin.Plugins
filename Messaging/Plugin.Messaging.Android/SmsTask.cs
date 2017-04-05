using Android.Content;
using Uri = Android.Net.Uri;

namespace Plugin.Messaging
{
    // NOTE: http://developer.xamarin.com/recipes/android/networking/sms/send_an_sms/

    internal class SmsTask : ISmsTask
    {
        public SmsTask()
        {
        }

        #region ISmsTask Members

        public bool CanSendSms => true;

        public void SendSms(string recipient = null, string message = null)
        {
            message = message ?? string.Empty;

            if (CanSendSms)
            {
                Uri smsUri;
                if (!string.IsNullOrWhiteSpace(recipient))
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