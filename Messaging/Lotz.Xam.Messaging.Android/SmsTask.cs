using System.Threading.Tasks;
using Android.Content;
using Android.Net;
using Lotz.Xam.Messaging.Abstractions;

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

        public Task SendSmsAsync(SmsMessageRequest sms)
        {
            var smsUri = Uri.Parse("smsto:" + sms.DestinationAddress);
            var smsIntent = new Intent(Intent.ActionSendto, smsUri);
            smsIntent.PutExtra("sms_body", sms.Message);

            _context.Activity.StartActivity(smsIntent);

            return Task.FromResult<object>(null);
        }

        #endregion
    }
}