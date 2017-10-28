using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Telephony;
using Uri = Android.Net.Uri;

namespace Plugin.Messaging
{
    internal class PhoneCallTask : IPhoneCallTask
    {
        public PhoneCallTask(PhoneSettings settings)
        {
            Settings = settings;
        }

        #region IPhoneCallTask Members

        public bool CanMakePhoneCall
        {
            get
            {
                var packageManager = Application.Context.PackageManager;
                var dialIntent = ResolveDialIntent("0000000000");

                return null != dialIntent.ResolveActivity(packageManager);
            }
        }

        public void MakePhoneCall(string number, string name = null)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("number");

            if (CanMakePhoneCall)
            {
                string phoneNumber = number;
                if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
                {
                    phoneNumber = PhoneNumberUtils.FormatNumber(number);
                }
                else
                {
                    if (!string.IsNullOrEmpty(Settings.DefaultCountryIso))
                        phoneNumber = PhoneNumberUtils.FormatNumber(number, Settings.DefaultCountryIso);
                }
                    
                var dialIntent = ResolveDialIntent(phoneNumber);
                dialIntent.StartNewActivity();
            }
        }

        #endregion

        #region Properties

        private PhoneSettings Settings { get; }

        #endregion

        #region Methods

        private Intent ResolveDialIntent(string phoneNumber)
        {
            string dialIntent = Settings.AutoDial ? Intent.ActionCall : Intent.ActionDial;

            Uri telUri = Uri.Parse("tel:" + phoneNumber);
            return new Intent(dialIntent, telUri);
        }

        #endregion
    }
}