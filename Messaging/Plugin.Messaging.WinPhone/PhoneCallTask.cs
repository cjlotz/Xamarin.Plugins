using System;

namespace Plugin.Messaging
{
    internal class PhoneCallTask : IPhoneCallTask
    {
        public PhoneCallTask()
        {
        }

        #region IPhoneCallTask Members

        public bool CanMakePhoneCall
        {
            get { return true; }
        }

        public void MakePhoneCall(string number, string name = null)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentNullException(nameof(number));

            // NOTE: Requires ID_CAP_PHONEDIALER capabilities
            if (CanMakePhoneCall)
            {
                Microsoft.Phone.Tasks.PhoneCallTask dialer = new Microsoft.Phone.Tasks.PhoneCallTask
                                                             {
                                                                 PhoneNumber = number,
                                                                 DisplayName = name ?? ""
                                                             };
                dialer.Show();
            }
        }

        #endregion
    }
}