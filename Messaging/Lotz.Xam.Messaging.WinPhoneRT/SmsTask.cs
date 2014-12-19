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

        public bool CanSendSms { get { return true; } }

        public void SendSms(SmsMessageRequest sms)
        {
            var msg = new ChatMessage { Body = sms.Message };
            msg.Recipients.Add(sms.DestinationAddress);

            ChatMessageManager.ShowComposeSmsMessageAsync(msg);
        }

        #endregion
    }
}