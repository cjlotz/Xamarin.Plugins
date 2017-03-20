namespace Plugin.Messaging.Sample
{
    public static class SamplesExtensions
    {
        #region Methods

        public static void MakeSamplePhoneCall(this IPhoneCallTask phoneCall)
        {
            if (phoneCall.CanMakePhoneCall)
            {
                phoneCall.MakePhoneCall("+27219330000", "Xamarin Demo User");
            }
        }

        public static EmailMessageBuilder BuildSampleEmail(bool sendAsHtml = false, bool strictMode = false)
        {
            var builder = new EmailMessageBuilder()
                .To("to.plugins@xamarin.com")
                .Cc("cc.plugins@xamarin.com")
                .Bcc(new[] { "bcc1.plugins@xamarin.com", "bcc2.plugins@xamarin.com" })
                .Subject("Xamarin Messaging Plugin");

#if __ANDROID__ || __IOS__

            if (sendAsHtml)
                builder.BodyAsHtml("Well hello there from <b>Xam.Messaging.Plugin</b>");
#endif
            if (!sendAsHtml)
                builder.Body("Well hello there from Xam.Messaging.Plugin");

#if __ANDROID__

            if (strictMode)
                builder.UseStrictMode();
#endif
            return builder;
        }

        public static void SendSampleEmail(this IEmailTask emailTask, IEmailMessage email)
        {
            if (emailTask.CanSendEmail)
            {
                emailTask.SendEmail(email);
            }
        }

        public static void SendSampleEmail(this IEmailTask emailTask, bool sendAsHtml, bool strictMode = false)
        {
            var email = BuildSampleEmail(sendAsHtml, strictMode).Build();
            emailTask.SendSampleEmail(email);
        }

        public static void SendSampleSms(this ISmsTask smsTask)
        {
            if (smsTask.CanSendSms)
            {
                smsTask.SendSms("+27219330000", "Well hello there from Xam.Messaging.Plugin");
            }
        }

        public static void SendSampleMultipleSms(this ISmsTask smsTask)
        {
            if (smsTask.CanSendSms)
            {
                smsTask.SendSms("+27219330000;+27219330001", "Well hello there from Xam.Messaging.Plugin");
            }
        }

        #endregion
    }
}