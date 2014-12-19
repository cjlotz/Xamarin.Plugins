using System;
using Lotz.Xam.Messaging.Abstractions;

namespace Lotz.Xam.Messaging
{
    public class PhoneCallTask : IPhoneCallTask
    {
        public PhoneCallTask(IMessagingContext context)
        {
        }

        #region IPhoneCallTask Members

        // Requires ID_CAP_PHONEDIALER capabilities

        public void MakePhoneCall(string number, string name = null)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentNullException("number");

            Microsoft.Phone.Tasks.PhoneCallTask dialer = new Microsoft.Phone.Tasks.PhoneCallTask
                                                         {
                                                             PhoneNumber = number,
                                                             DisplayName = name ?? ""
                                                         };
            dialer.Show();
        }

        #endregion
    }
}