using System;

namespace Plugin.Messaging
{
    internal class PhoneCallTask : IPhoneCallTask
    {
        public PhoneCallTask()
        {
        }

        #region IPhoneCallTask Members

        public bool CanMakePhoneCall => false;

        public void MakePhoneCall(string number, string name = null)
        {
            throw new PlatformNotSupportedException("Making Phone call not supported on Windows Store");
        }

        #endregion
    }
}