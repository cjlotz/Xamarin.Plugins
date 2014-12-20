using System;
using Lotz.Xam.Messaging.Abstractions;
using Microsoft.Phone.Tasks;

namespace Lotz.Xam.Messaging
{
    internal class SmsTask : ISmsTask
    {
        public SmsTask()
        {
        }

        #region ISmsTask Members

        public bool CanSendSms { get { return true; } }

        public void SendSms(string recipient, string message)
        {
            if (string.IsNullOrWhiteSpace(recipient))
                throw new ArgumentNullException("recipient");

            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException("message");

            if (CanSendSms)
            {
                SmsComposeTask smsComposeTask = new SmsComposeTask
                                                {
                                                    To = recipient,
                                                    Body = message
                                                };

                smsComposeTask.Show();
            }
        }

        #endregion
    }
}