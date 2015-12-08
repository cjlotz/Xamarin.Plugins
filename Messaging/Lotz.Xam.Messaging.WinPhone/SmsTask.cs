using System;
using Microsoft.Phone.Tasks;

namespace Plugin.Messaging
{
    internal class SmsTask : ISmsTask
    {
        public SmsTask()
        {
        }

        #region ISmsTask Members

        public bool CanSendSms { get { return true; } }

        public void SendSms(string recipient = null, string message = null)
        {
            message = message ?? string.Empty;

            if (CanSendSms)
            {
                SmsComposeTask smsComposeTask = new SmsComposeTask
                                                {
                                                    Body = message
                                                };
                if (!string.IsNullOrWhiteSpace(recipient))
                    smsComposeTask.To = recipient;

                smsComposeTask.Show();
            }
        }

        #endregion
    }
}