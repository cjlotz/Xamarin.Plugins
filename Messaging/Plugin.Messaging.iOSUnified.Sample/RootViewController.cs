using System;
using System.Threading.Tasks;
using Foundation;
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
            ButtonSendAttachmentsEmailPcl.TouchUpInside += ButtonSendAttachmentsEmailPcl_TouchUpInside;
            ButtonSendSms.TouchUpInside += ButtonSendSms_TouchUpInside;
            ButtonSendMultipleSMS.TouchUpInside += ButtonSendMultipleSms_TouchUpInside;
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);

            ButtonMakePhoneCall.TouchUpInside -= ButtonMakePhoneCall_TouchUpInside;
            ButtonSendEmail.TouchUpInside -= ButtonSendEmail_TouchUpInside;
            ButtonSendHtmlEmail.TouchUpInside -= ButtonSendHtmlEmail_TouchUpInside;
            ButtonSendAttachmentsEmail.TouchUpInside -= ButtonSendAttachmentsEmail_TouchUpInside;
            ButtonSendAttachmentsEmailPcl.TouchUpInside -= ButtonSendAttachmentsEmailPcl_TouchUpInside;
            ButtonSendSms.TouchUpInside -= ButtonSendSms_TouchUpInside;
            ButtonSendMultipleSMS.TouchUpInside -= ButtonSendMultipleSms_TouchUpInside;
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
            await SendAttachmentsEmail(true);
        }

        private async void ButtonSendAttachmentsEmailPcl_TouchUpInside(object o, EventArgs eventArgs)
        {
            await SendAttachmentsEmail(false);
        }

        private void ButtonSendSms_TouchUpInside(object o, EventArgs eventArgs)
        {
            CrossMessaging.Current.SmsMessenger.SendSampleSms();
        }

        private void ButtonSendMultipleSms_TouchUpInside(object o, EventArgs eventArgs)
        {
            CrossMessaging.Current.SmsMessenger.SendSampleMultipleSms();
        }

        private async Task SendAttachmentsEmail(bool usePlatformApi = true)
        {
            var mediaPicker = new MediaPicker();
            MediaFile mediaFile = await mediaPicker.PickPhotoAsync();

            if (mediaFile != null)
            {
                IEmailMessage email;
                if (usePlatformApi)
                {
                    NSUrl url = new NSUrl(mediaFile.Path, false);
                    email = SamplesExtensions.BuildSampleEmail()
                        .WithAttachment(url)
                        .Build();
                }
                else
                {
                    // Hard coded mimetype for sample. Should query file to determine at run-time
                    email = SamplesExtensions.BuildSampleEmail()
                        .WithAttachment(mediaFile.Path, @"image/jpeg")
                        .Build();
                }

                CrossMessaging.Current.EmailMessenger.SendSampleEmail(email);
            }
        }

        #endregion
    }
}