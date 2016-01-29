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
            get { return true; }
        }

        public void MakePhoneCall(string number, string name = null)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentNullException("number");

            if (CanMakePhoneCall)
            {
                var nsurl = CreateNSUrl(number);
                UIApplication.SharedApplication.OpenUrl(nsurl);                
            }
        }

        private NSUrl CreateNSUrl(string number)
        {
            return new NSUrl(new Uri($"tel:{number}").AbsoluteUri);
        }

        #endregion
    }
}