using System.Diagnostics;
using Lotz.Xam.Messaging.Abstractions;

namespace Lotz.Xam.Messaging
{
    internal class SmsTask : ISmsTask
    {
        public SmsTask()
        {
        }

        #region ISmsTask Members

        public bool CanSendSms { get { return false; } }

        public void SendSms(string recipient, string message)
        {
            Debug.WriteLine("Messaging not supported on Windows Store apps.");
        }

        #endregion
    }
}