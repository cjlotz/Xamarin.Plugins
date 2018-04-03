using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Widget;
using Java.IO;
using Xamarin.Media;

namespace Plugin.Messaging.Sample.Android
{
    [Activity(Label = "Xam Messaging Demo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private const int RequestPhoneCall = 3;
        private const int RequestSendBackgroundSms = 4;
        private const int RequestReadPhoneState = 5;
        private const int SelectPhotoPcl = 2;
        private const int SelectPhotoPlatform = 1;

        #region Methods

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (requestCode == RequestPhoneCall && grantResults.Length > 0 && grantResults[0] == Permission.Granted)
                AutoDialNumber();
            else if (requestCode == RequestSendBackgroundSms && grantResults.Length > 0 && grantResults[0] == Permission.Granted)
                SendBackgroundSms();
        }

        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (resultCode == Result.Canceled || (!(requestCode == SelectPhotoPlatform || requestCode == SelectPhotoPcl)))
                return;

            MediaFile mediaFile = await data.GetMediaFileExtraAsync(this);

            if (mediaFile != null)
            {
                IEmailMessage email;
                if (requestCode == SelectPhotoPlatform)
                {
                    File file = new File(mediaFile.Path);
                    email = SamplesExtensions.BuildSampleEmail()
                        .WithAttachment(file)
                        .Build();
                }
                else
                {
                    // Hard coded mimetype for sample. Should really use Media Query to resolve at run-time
                    email = SamplesExtensions.BuildSampleEmail()
                        .WithAttachment(mediaFile.Path, @"image/jpeg")
                        .Build();
                }

                CrossMessaging.Current.EmailMessenger.SendSampleEmail(email);
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button buttonPhoneCall = FindViewById<Button>(Resource.Id.ButtonMakePhoneCall);
            Button buttonPhoneCallAutoDial = FindViewById<Button>(Resource.Id.ButtonMakePhoneCallAutoDial);
            Button buttonSendEmail = FindViewById<Button>(Resource.Id.ButtonSendEmail);
            Button buttonSendEmailStrictMode = FindViewById<Button>(Resource.Id.ButtonSendEmailStrictMode);
            Button buttonSendHtmlEmail = FindViewById<Button>(Resource.Id.ButtonSendHtmlEmail);
            Button buttonSendAttachmentEmail = FindViewById<Button>(Resource.Id.ButtonSendAttachmentEmail);
            Button buttonSendAttachmentEmailPcl = FindViewById<Button>(Resource.Id.ButtonSendAttachmentEmailPCL);
            Button buttonSendSms = FindViewById<Button>(Resource.Id.ButtonSendSms);
            Button buttonSendMultipleSms = FindViewById<Button>(Resource.Id.ButtonSendMultipleSms);
            Button buttonSendBackgroundSms = FindViewById<Button>(Resource.Id.ButtonSendBackgroundSms);

            buttonPhoneCall.Click += ButtonPhoneCall_Click;
            buttonPhoneCallAutoDial.Click += ButtonPhoneCallAutoDial_Click;
            buttonSendEmail.Click += ButtonSendEmail_Click;
            buttonSendEmailStrictMode.Click += ButtonSendEmailStrictMode_Click;
            buttonSendHtmlEmail.Click += ButtonSendHtmlEmail_Click;
            buttonSendAttachmentEmail.Click += ButtonSendAttachmentEmail_Click;
            buttonSendAttachmentEmailPcl.Click += ButtonSendAttachmentEmailPcl_Click;
            buttonSendSms.Click += ButtonSendSms_Click;
            buttonSendMultipleSms.Click += ButtonSendMultipleSms_Click;
            buttonSendBackgroundSms.Click += ButtonSendBackgroundSms_Click;
        }

        private static void AutoDialNumber()
        {
            CrossMessaging.Current.Settings().Phone.AutoDial = true;
            CrossMessaging.Current.PhoneDialer.MakeSamplePhoneCall();
            CrossMessaging.Current.Settings().Phone.AutoDial = false;
        }

        private static void SendBackgroundSms()
        {
            CrossMessaging.Current.SmsMessenger.SendSampleBackgroundSms();
        }

        #endregion

        #region Event Handlers

        private void ButtonPhoneCall_Click(object sender, EventArgs eventArgs)
        {
            CrossMessaging.Current.PhoneDialer.MakeSamplePhoneCall();
        }

        private void ButtonPhoneCallAutoDial_Click(object sender, EventArgs eventArgs)
        {
            if (ContextCompat.CheckSelfPermission(this, "android.permission.CALL_PHONE") != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new[] { "android.permission.CALL_PHONE" }, RequestPhoneCall);
            }
            else
            {
                AutoDialNumber();
            }
        }

        private void ButtonSendAttachmentEmail_Click(object sender, EventArgs eventArgs)
        {
            var picker = new MediaPicker(Application.Context);
            var intent = picker.GetPickPhotoUI();

            StartActivityForResult(intent, SelectPhotoPlatform);
        }

        private void ButtonSendAttachmentEmailPcl_Click(object sender, EventArgs eventArgs)
        {
            var picker = new MediaPicker(Application.Context);
            var intent = picker.GetPickPhotoUI();

            StartActivityForResult(intent, SelectPhotoPcl);
        }

        private void ButtonSendBackgroundSms_Click(object sender, EventArgs eventArgs)
        {
            // NOTE: requires android.permission.SEND_SMS permission in the Android manifest.
            // NOTE: requires android.permission.READ_PHONE_STATE permission in the Android manifest.

            var permissionChecks = new List<string>();
            if (ContextCompat.CheckSelfPermission(this, "android.permission.READ_PHONE_STATE") != Permission.Granted)
            {
                permissionChecks.Add("android.permission.READ_PHONE_STATE");
            }
            if (ContextCompat.CheckSelfPermission(this, "android.permission.SEND_SMS") != Permission.Granted)
            {
                permissionChecks.Add("android.permission.SEND_SMS");
            }

            if (permissionChecks.Count > 0)
            {
                ActivityCompat.RequestPermissions(this, permissionChecks.ToArray(), RequestSendBackgroundSms);
            }
            else
            {
                SendBackgroundSms();
            }
        }

        private void ButtonSendEmail_Click(object sender, EventArgs eventArgs)
        {
            CrossMessaging.Current.EmailMessenger.SendSampleEmail(false);
        }

        private void ButtonSendEmailStrictMode_Click(object sender, EventArgs eventArgs)
        {
            CrossMessaging.Current.Settings().Email.UseStrictMode = true;
            CrossMessaging.Current.EmailMessenger.SendSampleEmail(false);
            CrossMessaging.Current.Settings().Email.UseStrictMode = false;
        }

        private void ButtonSendHtmlEmail_Click(object sender, EventArgs eventArgs)
        {
            CrossMessaging.Current.EmailMessenger.SendSampleEmail(true);
        }

        private void ButtonSendMultipleSms_Click(object sender, EventArgs eventArgs)
        {
            // NOTE: requires android.permission.SEND_SMS permission in the Android manifest.

            CrossMessaging.Current.SmsMessenger.SendSampleMultipleSms();
        }

        private void ButtonSendSms_Click(object sender, EventArgs eventArgs)
        {
            // NOTE: requires android.permission.SEND_SMS permission in the Android manifest.

            CrossMessaging.Current.SmsMessenger.SendSampleSms();
        }

        #endregion
    }
}