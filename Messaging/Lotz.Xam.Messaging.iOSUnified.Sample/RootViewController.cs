using System;
using Lotz.Xam.Messaging.Sample;
using UIKit;

namespace Lotz.Xam.Messaging.iOSUnified.Sample
{
    // NOTE: No sample provided on iOS Classic as the API usage is precisely the same

    public partial class RootViewController : UIViewController
    {
        public RootViewController(IntPtr handle)
            : base(handle)
        {
        }

        #region Methods

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            ButtonMakePhoneCall.TouchUpInside += ButtonMakePhoneCall_TouchUpInside;
            ButtonSendEmail.TouchUpInside += ButtonSendEmail_TouchUpInside;
            ButtonSendHtmlEmail.TouchUpInside += ButtonSendHtmlEmail_TouchUpInside;
            ButtonSendSms.TouchUpInside += ButtonSendSms_TouchUpInside;
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);

            ButtonMakePhoneCall.TouchUpInside -= ButtonMakePhoneCall_TouchUpInside;
            ButtonSendEmail.TouchUpInside -= ButtonSendEmail_TouchUpInside;
            ButtonSendHtmlEmail.TouchUpInside -= ButtonSendHtmlEmail_TouchUpInside;
            ButtonSendSms.TouchUpInside -= ButtonSendSms_TouchUpInside;
        }

        #endregion

        #region Event Handlers

        private void ButtonMakePhoneCall_TouchUpInside(object o, EventArgs eventArgs)
        {
            MessagingPlugin.PhoneDialer.MakeSamplePhoneCall();
        }

        private void ButtonSendEmail_TouchUpInside(object o, EventArgs eventArgs)
        {
            MessagingPlugin.EmailMessenger.SendSampleEmail();
        }

        private void ButtonSendHtmlEmail_TouchUpInside(object o, EventArgs eventArgs)
        {
            MessagingPlugin.EmailMessenger.SendSampleEmail(true);
        }

        private void ButtonSendSms_TouchUpInside(object o, EventArgs eventArgs)
        {
            MessagingPlugin.SmsMessenger.SendSampleSms();
        }

        #endregion
    }
}