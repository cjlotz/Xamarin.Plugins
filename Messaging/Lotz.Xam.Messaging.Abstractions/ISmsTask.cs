
using System;

namespace Lotz.Xam.Messaging.Abstractions
{
    public interface ISmsTask
    {
        bool CanSendSms { get; }
        void SendSms(SmsMessageRequest sms);
    }

    // TODO: Consider multiple destinations
    // TODO: Consider adding SmsBuilder 

    public class SmsMessageRequest
    {
        public SmsMessageRequest(string destinationAddress, string message)
        {
            if (string.IsNullOrWhiteSpace(destinationAddress))
                throw new ArgumentNullException("destinationAddress");

            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException("message");

            DestinationAddress = destinationAddress;
            Message = message;
        }

        #region Properties

        public string DestinationAddress { get; private set; }
        public string Message { get; private set; }

        #endregion
    }
}