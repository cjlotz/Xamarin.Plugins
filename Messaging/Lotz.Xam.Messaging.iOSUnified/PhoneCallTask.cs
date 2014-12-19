using Foundation;
using Lotz.Xam.Messaging.Abstractions;
using UIKit;

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