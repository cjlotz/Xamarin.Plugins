using System.Threading.Tasks;
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

        public Task SendSmsAsync(SmsMessageRequest sms)
        {
            SmsComposeTask smsComposeTask = new SmsComposeTask
                                            {
                                                To = sms.DestinationAddress,
                                                Body = sms.Message
                                            };

            smsComposeTask.Show();

            return Task.FromResult<object>(null);
        }

        #endregion
    }
}