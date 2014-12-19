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
    public class PhoneCallTask : IPhoneCallTask
    {
        public PhoneCallTask(IMessagingContext context)
        {
            // context not required
        }

        #region IPhoneCallTask Members

        public void MakePhoneCall(string number, string name = null)
        {
            var nsurl = new NSUrl("tel:" + number);
            UIApplication.SharedApplication.OpenUrl(nsurl);
        }        

        #endregion
    }
}