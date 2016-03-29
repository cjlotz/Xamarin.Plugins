using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Xamarin.Media;

namespace Plugin.Messaging.Sample.Android
{
    [Activity(Label = "Xam Messaging Demo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private const int SelectPhoto = 1;

        #region Methods

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button buttonPhoneCall = FindViewById<Button>(Resource.Id.ButtonMakePhoneCall);
            Button buttonSendEmail = FindViewById<Button>(Resource.Id.ButtonSendEmail);
            Button buttonSendHtmlEmail = FindViewById<Button>(Resource.Id.ButtonSendHtmlEmail);
            Button buttonSendAttachmentEmail = FindViewById<Button>(Resource.Id.ButtonSendAttachmentEmail);
            Button buttonSendSms = FindViewById<Button>(Resource.Id.ButtonSendSms);

            buttonPhoneCall.Click += ButtonPhoneCall_Click;
            buttonSendEmail.Click += ButtonSendEmail_Click;
            buttonSendHtmlEmail.Click += ButtonSendHtmlEmail_Click;
            buttonSendAttachmentEmail.Click += ButtonSendAttachmentEmail_Click;
            buttonSendSms.Click += ButtonSendSms_Click;
        }

        #endregion

        #region Event Handlers

        private void ButtonPhoneCall_Click(object sender, EventArgs eventArgs)
        {
            CrossMessaging.Current.PhoneDialer.MakeSamplePhoneCall();
        }

        private void ButtonSendEmail_Click(object sender, EventArgs eventArgs)
        {
            CrossMessaging.Current.EmailMessenger.SendSampleEmail(false);
        }

        private void ButtonSendHtmlEmail_Click(object sender, EventArgs eventArgs)
        {
            CrossMessaging.Current.EmailMessenger.SendSampleEmail(true);
        }

        private void ButtonSendAttachmentEmail_Click(object sender, EventArgs eventArgs)
        {
            var picker = new MediaPicker(Application.Context);
            var intent = picker.GetPickPhotoUI();

            StartActivityForResult(intent, SelectPhoto);
        }

        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (resultCode == Result.Canceled || requestCode != SelectPhoto)
                return;

            MediaFile file = await data.GetMediaFileExtraAsync(this);
            var email = SamplesExtensions.BuildSampleEmail()
                .WithAttachment(file.Path)
                .Build();

            CrossMessaging.Current.EmailMessenger.SendSampleEmail(email);
        }

        private void ButtonSendSms_Click(object sender, EventArgs eventArgs)
        {
            // NOTE: requires android.permission.SEND_SMS permission in the Android manifest.

            CrossMessaging.Current.SmsMessenger.SendSampleSms();
        }

        #endregion
    }
}