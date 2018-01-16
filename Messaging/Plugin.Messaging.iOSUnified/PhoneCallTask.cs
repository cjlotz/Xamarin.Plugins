using System;
using Foundation;
using UIKit;
using CoreTelephony;

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
                var nsurl = CreateNsUrl("0000000000");
                bool canCall = UIApplication.SharedApplication.CanOpenUrl(nsurl);

                if (canCall)
                {
                    using (CTTelephonyNetworkInfo netInfo = new CTTelephonyNetworkInfo())
                    {
                        string mnc = netInfo.SubscriberCellularProvider?.MobileNetworkCode;

                        return !string.IsNullOrEmpty(mnc) && mnc != "65535"; //65535 stands for NoNetwordProvider
                    }
                }
                return false;
            }
        }

        public void MakePhoneCall(string number, string name = null)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentNullException(nameof(number));

            if (CanMakePhoneCall)
            {
                var nsurl = CreateNsUrl(number);
                UIApplication.SharedApplication.OpenUrl(nsurl);                
            }
        }

        private NSUrl CreateNsUrl(string number)
        {
            return new NSUrl(new Uri($"tel:{number}").AbsoluteUri);
        }

        #endregion
    }
}