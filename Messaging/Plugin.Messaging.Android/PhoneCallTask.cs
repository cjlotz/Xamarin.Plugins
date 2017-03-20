using System;
using Android.Content;
using Android.Telephony;
using Uri = Android.Net.Uri;

namespace Plugin.Messaging
{
    internal class PhoneCallTask : IPhoneCallTask
    {
        public PhoneCallTask()
        {
        }

        #region IPhoneCallTask Members

        public bool CanMakePhoneCall
        {
            get
            {
                var packageManager = Android.App.Application.Context.PackageManager;
                var dialIntent = new Intent(Intent.ActionDial, Uri.Parse("tel:0000000000"));

                return null != dialIntent.ResolveActivity(packageManager);
            }
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