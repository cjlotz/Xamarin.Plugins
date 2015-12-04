using System;
using Android.Content;
using Android.Telephony;
using Uri = Android.Net.Uri;

namespace Lotz.Xam.Messaging
{
    internal class PhoneCallTask : IPhoneCallTask
    {
        public PhoneCallTask()
        {
        }

        #region IPhoneCallTask Members

        public bool CanMakePhoneCall
        {
            get { return true; }
        }

        public void MakePhoneCall(string number, string name = null)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("number");

            if (CanMakePhoneCall)
            {
                var phoneNumber = PhoneNumberUtils.FormatNumber(number);

                Uri telUri = Uri.Parse("tel:" + phoneNumber);
                var dialIntent = new Intent(Intent.ActionDial, telUri);

                dialIntent.StartNewActivity();
            }
        }

        #endregion
    }
}