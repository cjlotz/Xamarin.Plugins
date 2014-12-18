using System.Threading.Tasks;

namespace Lotz.Xam.Messaging.Abstractions
{
    public interface ISmsTask
    {
        Task SendSmsAsync(SmsMessageRequest sms);
    }

    public class SmsMessageRequest
    {
        public SmsMessageRequest(string destinationAddress, string message)
        {
            DestinationAddress = destinationAddress;
            Message = message;
        }

        #region Properties

        public string DestinationAddress { get; private set; }
        public string Message { get; private set; }

        #endregion
    }
}