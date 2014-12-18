using Windows.ApplicationModel.Calls;
using Lotz.Xam.Messaging.Abstractions;

namespace Lotz.Xam.Messaging
{
    public class PhoneCallTask : IPhoneCallTask
    {
        public PhoneCallTask(IMessagingContext context)
        {
        }

        #region IPhoneCallTask Members

        public void MakePhoneCall(string number, string name = null)
        {
            PhoneCallManager.ShowPhoneCallUI(number, name);
        }

        #endregion
    }
}