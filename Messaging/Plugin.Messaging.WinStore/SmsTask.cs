using System;

namespace Plugin.Messaging
{
    internal class SmsTask : ISmsTask
    {
        public SmsTask()
        {
        }

        #region ISmsTask Members

        public bool CanSendSms => false;
        public bool CanSendSmsInBackground => false;

        public void SendSms(string recipient, string message)
        {
            throw new PlatformNotSupportedException("Sending SMS not supported on Windows Store");
        }

        public void SendSmsInBackground(string recipient, string message = null)
        {
            throw new PlatformNotSupportedException("Sending SMS in background not supported on Windows Store");
        }

        #endregion
    }
}