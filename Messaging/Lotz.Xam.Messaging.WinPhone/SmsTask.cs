using System;
using Lotz.Xam.Messaging.Abstractions;
using Microsoft.Phone.Tasks;

namespace Lotz.Xam.Messaging
{
    public class SmsTask : ISmsTask
    {
        public SmsTask(IMessagingContext context)
        {
        }

        #region ISmsTask Members

        public bool CanSendSms { get { return true; } }

        public void SendSms(SmsMessageRequest sms)
        {
            if (sms == null)
                throw new ArgumentNullException("sms");

            if (CanSendSms)
            {
                SmsComposeTask smsComposeTask = new SmsComposeTask
                                                {
                                                    To = sms.ReceiverAddress,
                                                    Body = sms.Message
                                                };

                smsComposeTask.Show();
            }
        }

        #endregion
    }
}