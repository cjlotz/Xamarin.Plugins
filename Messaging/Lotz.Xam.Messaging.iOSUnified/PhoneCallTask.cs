using System;
#if __UNIFIED__
using Foundation;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif

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
                // UIApplication.SharedApplication.CanOpenUrl does not validate the URL, it merely checks whether a handler for
                // the URL has been installed on the system. Therefore string.Empty can be used as phone number.
                var nsurl = CreateNsUrl(string.Empty);
                return UIApplication.SharedApplication.CanOpenUrl(nsurl);
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