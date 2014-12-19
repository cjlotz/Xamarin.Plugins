using System;
using Lotz.Xam.Messaging.Samples;
using UIKit;

namespace Lotz.Xam.Messaging.iOSUnified.Sample
{
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
            ButtonSendSms.TouchUpInside += ButtonSendSms_TouchUpInside;
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);

            ButtonMakePhoneCall.TouchUpInside -= ButtonMakePhoneCall_TouchUpInside;
            ButtonSendEmail.TouchUpInside -= ButtonSendEmail_TouchUpInside;
            ButtonSendSms.TouchUpInside -= ButtonSendSms_TouchUpInside;
        }

        #endregion

        #region Event Handlers

        private void ButtonMakePhoneCall_TouchUpInside(object o, EventArgs eventArgs)
        {
            MessagingPlugin.PhoneDialer(new MessagingContext(this))
                .MakeSamplePhoneCall();
        }

        private void ButtonSendEmail_TouchUpInside(object o, EventArgs eventArgs)
        {
            MessagingPlugin.EmailMessenger(new MessagingContext(this))
                .SendSampleEmail();
        }

        private void ButtonSendSms_TouchUpInside(object o, EventArgs eventArgs)
        {
            MessagingPlugin.SmsMessenger(new MessagingContext(this))
                .SendSampleSms();
        }

        #endregion
    }
}