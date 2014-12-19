using System;
using Windows.ApplicationModel.Calls;
using Lotz.Xam.Messaging.Abstractions;

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

            PhoneCallManager.ShowPhoneCallUI(number, name ?? "");
        }

        #endregion
    }
}