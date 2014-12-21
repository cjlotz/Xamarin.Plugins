using Lotz.Xam.Messaging.Abstractions;

namespace Lotz.Xam.Messaging.Samples
{
    public static class SamplesExtensions
    {
        #region Methods

        public static void MakeSamplePhoneCall(this IPhoneCallTask phoneCall)
        {
            if (phoneCall.CanMakePhoneCall)
            {
                phoneCall.MakePhoneCall("+272193343499", "Xamarin Demo User");
            }
        }

        public static void SendSampleEmail(this IEmailTask emailTask)
        {
            if (emailTask.CanSendEmail)
            {
                var email = new EmailMessageBuilder()
                    .To("to.plugins@xamarin.com")
                    .Cc("cc.plugins@xamarin.com")
                    .Bcc(new[] { "bcc1.plugins@xamarin.com", "bcc2.plugins@xamarin.com" })
                    .Subject("Xamarin Messaging Plugin")
                    .Body("Well hello there from Xam.Messaging.Plugin")
                    .Build();

                emailTask.SendEmail(email);
            }
        }

        public static void SendSampleSms(this ISmsTask smsTask)
        {
            if (smsTask.CanSendSms)
            {
                smsTask.SendSms("+27213894839493", "Well hello there from Xam.Messaging.Plugin");
            }
        }

        #endregion
    }
}