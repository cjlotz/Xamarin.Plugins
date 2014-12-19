using System.Windows;
using Lotz.Xam.Messaging.Samples;
using Microsoft.Phone.Controls;

namespace Lotz.Xam.Messaging.WinPhone.Sample
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        #region Event Handlers

        private void ButtonPhoneCall_OnClick(object sender, RoutedEventArgs e)
        {
            MessagingPlugin.PhoneDialer.MakeSamplePhoneCall();
        }

        private void ButtonSendEmail_OnClick(object sender, RoutedEventArgs e)
        {
            // NOTE: On Windows Phone Emulator, an exception occurs when using the email compose task. 
            // Test the email compose task on a physical device.

            MessagingPlugin.EmailMessenger.SendSampleEmail();
        }

        private void ButtonSendSms_OnClick(object sender, RoutedEventArgs e)
        {
            // NOTE: On Windows Phone Emulator, the SMS message always appears to be sent successfully, but the message is not actually sent. 
            // The emulator uses Fake GSM and always has a false Subscriber Identity Module (SIM) card.

            MessagingPlugin.SmsMessenger.SendSampleSms();
        }

        #endregion
    }
}