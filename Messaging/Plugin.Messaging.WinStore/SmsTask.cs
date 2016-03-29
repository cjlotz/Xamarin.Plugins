using System;

namespace Plugin.Messaging
{
    internal class SmsTask : ISmsTask
    {
        public SmsTask()
        {
        }

        #region ISmsTask Members

        public bool CanSendSms
        {
            get { return false; }
        }

        public void SendSms(string recipient, string message)
        {
            throw new PlatformNotSupportedException("Sending SMS not supported on Windows Store");
        }

        #endregion
    }
}