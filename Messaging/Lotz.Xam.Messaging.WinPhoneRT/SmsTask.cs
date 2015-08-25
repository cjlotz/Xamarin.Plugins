using System;
using Windows.ApplicationModel.Chat;

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
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException("message");

            if (CanSendSms)
            {
                var msg = new ChatMessage { Body = message };
                if (!string.IsNullOrWhiteSpace(recipient))
                    msg.Recipients.Add(recipient);

                ChatMessageManager.ShowComposeSmsMessageAsync(msg);
            }
        }

        #endregion
    }
}