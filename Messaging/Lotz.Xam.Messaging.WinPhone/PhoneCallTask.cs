using Lotz.Xam.Messaging.Abstractions;

namespace Lotz.Xam.Messaging
{
    public class PhoneCallTask : IPhoneCallTask
    {
        public PhoneCallTask(IMessagingContext context)
        {
        }

        #region IPhoneCallTask Members

        public void MakePhoneCall(string number, string name = null)
        {
            Microsoft.Phone.Tasks.PhoneCallTask dialer = new Microsoft.Phone.Tasks.PhoneCallTask
                                                         {
                                                             PhoneNumber = number,
                                                             DisplayName = name
                                                         };
            dialer.Show();
        }

        #endregion
    }
}