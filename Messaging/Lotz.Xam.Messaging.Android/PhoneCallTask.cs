using System;
using Android.Content;
using Android.Telephony;
using Lotz.Xam.Messaging.Abstractions;
using Uri = Android.Net.Uri;

namespace Lotz.Xam.Messaging
{
    internal class PhoneCallTask : IPhoneCallTask
    {
        public PhoneCallTask()
        {
        }

        #region IPhoneCallTask Members

        public void MakePhoneCall(string number, string name = null)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentNullException("number");

            var phoneNumber = PhoneNumberUtils.FormatNumber(number);

            Uri telUri = Uri.Parse("tel:" + phoneNumber);
            var dialIntent = new Intent(Intent.ActionDial, telUri);

            dialIntent.StartNewActivity();
        }

        #endregion
    }
}