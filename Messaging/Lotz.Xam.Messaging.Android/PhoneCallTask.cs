using Android.Content;
using Android.Net;
using Android.Telephony;
using Lotz.Xam.Messaging.Abstractions;

namespace Lotz.Xam.Messaging
{
    public class PhoneCallTask : IPhoneCallTask
    {
        private readonly MessagingContext _context;

        public PhoneCallTask(IMessagingContext context)
        {
            _context = context.AsPlatformContext();
        }

        #region IPhoneCallTask Members

        public void MakePhoneCall(string number, string name = null)
        {
            var phoneNumber = PhoneNumberUtils.FormatNumber(number);

            Uri telUri = Uri.Parse("tel:" + phoneNumber);
            var intent = new Intent(Intent.ActionDial, telUri);

            _context.Activity.StartActivity(intent);
        }

        #endregion
    }
}