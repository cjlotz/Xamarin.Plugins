using System.Diagnostics;
using Lotz.Xam.Messaging.Abstractions;

namespace Lotz.Xam.Messaging
{
    internal class PhoneCallTask : IPhoneCallTask
    {
        public PhoneCallTask()
        {
        }

        #region IPhoneCallTask Members

        public bool CanMakePhoneCall
        {
            get { return false; }
        }

        public void MakePhoneCall(string number, string name = null)
        {
            Debug.WriteLine("Messaging not supported on Windows Store apps.");
        }

        #endregion
    }
}