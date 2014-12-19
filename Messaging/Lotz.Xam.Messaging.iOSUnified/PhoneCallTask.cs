using System;
using Lotz.Xam.Messaging.Abstractions;
#if __UNIFIED__
using Foundation;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif

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

            var nsurl = new NSUrl("tel://" + number);
            UIApplication.SharedApplication.OpenUrl(nsurl);
        }        

        #endregion
    }
}