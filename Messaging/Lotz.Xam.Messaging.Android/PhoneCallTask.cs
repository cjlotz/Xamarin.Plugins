using System;
using Android.Content;
using Android.Telephony;
using Lotz.Xam.Messaging.Abstractions;
using Uri = Android.Net.Uri;

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
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentNullException("number");

            var phoneNumber = PhoneNumberUtils.FormatNumber(number);

            Uri telUri = Uri.Parse("tel:" + phoneNumber);
            var intent = new Intent(Intent.ActionDial, telUri);

            _context.Activity.StartActivity(intent);
        }

        #endregion
    }
}