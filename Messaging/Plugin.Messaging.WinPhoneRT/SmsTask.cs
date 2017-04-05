using System;
using Windows.ApplicationModel.Chat;
using Windows.Devices.Sms;

namespace Plugin.Messaging
{
    internal class SmsTask : ISmsTask
    {
        public SmsTask()
        {
        }

        #region ISmsTask Members

        public bool CanSendSms => true;
        public bool CanSendSmsInBackground => true;

        public void SendSms(string recipient = null, string message = null)
        {
            message = message ?? string.Empty;

            if (CanSendSms)
            {
                var msg = new ChatMessage { Body = message };
                if (!string.IsNullOrWhiteSpace(recipient))
                    msg.Recipients.Add(recipient);

#pragma warning disable 4014
                ChatMessageManager.ShowComposeSmsMessageAsync(msg);
#pragma warning restore 4014
            }
        }

        public void SendSmsInBackground(string recipient, string message = null)
        {
            if (string.IsNullOrEmpty(recipient))
                throw new ArgumentException(nameof(recipient));

            message = message ?? string.Empty;

            if (CanSendSmsInBackground)
            {
                var sendingMessage = new SmsTextMessage2
                                     {
                                         Body = message,
                                         To = recipient
                                     };

                SmsDevice2.GetDefault().SendMessageAndGetResultAsync(sendingMessage).AsTask().Wait();
            }
        }

        #endregion
    }
}