using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Chat;
using Lotz.Xam.Messaging.Abstractions;

namespace Lotz.Xam.Messaging
{
    public class SmsTask : ISmsTask
    {
        public SmsTask(IMessagingContext context)
        {
        }

        #region ISmsTask Members

        public Task SendSmsAsync(SmsMessageRequest sms)
        {
            var msg = new ChatMessage();
            msg.Body = sms.Message;
            msg.Recipients.Add(sms.DestinationAddress);

            return ChatMessageManager.ShowComposeSmsMessageAsync(msg).AsTask();
        }

        #endregion
    }
}