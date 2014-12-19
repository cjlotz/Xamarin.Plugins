using System;
using Android.App;
using Android.OS;
using Android.Widget;
using Lotz.Xam.Messaging.Samples;

namespace Lotz.Xam.Messaging.Android.Sample
{
    [Activity(Label = "Xam Messaging Demo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
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
            Button buttonSendSms = FindViewById<Button>(Resource.Id.ButtonSendSms);

            buttonPhoneCall.Click += ButtonPhoneCall_Click;
            buttonSendEmail.Click += ButtonSendEmail_Click;
            buttonSendSms.Click += ButtonSendSms_Click;
        }

        #endregion

        #region Event Handlers

        private void ButtonPhoneCall_Click(object sender, EventArgs eventArgs)
        {
            MessagingPlugin.PhoneDialer.MakeSamplePhoneCall();
        }

        private void ButtonSendEmail_Click(object sender, EventArgs eventArgs)
        {
            MessagingPlugin.EmailMessenger.SendSampleEmail();
        }

        private void ButtonSendSms_Click(object sender, EventArgs eventArgs)
        {
            // NOTE: requires android.permission.SEND_SMS permission in the Android manifest.

            MessagingPlugin.SmsMessenger.SendSampleSms();
        }

        #endregion
    }
}