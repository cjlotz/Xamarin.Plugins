using System;
using UIKit;
using Xamarin.Media;

namespace Plugin.Messaging.Sample.iOSUnified
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
            ButtonSendAttachmentsEmail.TouchUpInside += ButtonSendAttachmentsEmail_TouchUpInside;
            ButtonSendSms.TouchUpInside += ButtonSendSms_TouchUpInside;
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);

            ButtonMakePhoneCall.TouchUpInside -= ButtonMakePhoneCall_TouchUpInside;
            ButtonSendEmail.TouchUpInside -= ButtonSendEmail_TouchUpInside;
            ButtonSendHtmlEmail.TouchUpInside -= ButtonSendHtmlEmail_TouchUpInside;
            ButtonSendAttachmentsEmail.TouchUpInside -= ButtonSendAttachmentsEmail_TouchUpInside;
            ButtonSendSms.TouchUpInside -= ButtonSendSms_TouchUpInside;
        }

        #endregion

        #region Event Handlers

        private void ButtonMakePhoneCall_TouchUpInside(object o, EventArgs eventArgs)
        {
            CrossMessaging.Current.PhoneDialer.MakeSamplePhoneCall();
        }

        private void ButtonSendEmail_TouchUpInside(object o, EventArgs eventArgs)
        {
            CrossMessaging.Current.EmailMessenger.SendSampleEmail(false);
        }

        private void ButtonSendHtmlEmail_TouchUpInside(object o, EventArgs eventArgs)
        {
            CrossMessaging.Current.EmailMessenger.SendSampleEmail(true);
        }

        private async void ButtonSendAttachmentsEmail_TouchUpInside(object o, EventArgs eventArgs)
        {
            var mediaPicker = new MediaPicker();
            MediaFile file = await mediaPicker.PickPhotoAsync();

            if (file != null)
            {
                var fileName = System.IO.Path.GetFileName(file.Path);

                // Assume image content is default jpeg
                var email = SamplesExtensions.BuildSampleEmail()
                    .WithAttachment(fileName, file.GetStream(), "image/jpeg")
                    .Build();

                CrossMessaging.Current.EmailMessenger.SendSampleEmail(email);
            }
        }

        private void ButtonSendSms_TouchUpInside(object o, EventArgs eventArgs)
        {
            CrossMessaging.Current.SmsMessenger.SendSampleSms();
        }

        #endregion
    }
}