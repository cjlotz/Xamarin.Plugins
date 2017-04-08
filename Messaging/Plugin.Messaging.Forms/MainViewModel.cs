using System.Windows.Input;
using Plugin.Messaging.Sample;
using Xamarin.Forms;

namespace Plugin.Messaging.Forms
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            MakePhoneCallCommand = new Command(MakePhoneCall);
            SendSmsCommand = new Command(SendSms);
            SendMultipleSmsCommand = new Command(SendMultipleSms);
            SendEmailCommand = new Command(SendEmail);
            SendHtmlEmailCommand = new Command(SendHtmlEmail);
        }

        public ICommand MakePhoneCallCommand { get; set; }
        public ICommand SendSmsCommand { get; set; }
        public ICommand SendMultipleSmsCommand { get; set; }
        public ICommand SendEmailCommand { get; set; }
        public ICommand SendHtmlEmailCommand { get; set; }

        private void SendMultipleSms()
        {
        	CrossMessaging.Current.SmsMessenger.SendSampleMultipleSms();
        }

        private void SendEmail()
        {
        	CrossMessaging.Current.EmailMessenger.SendSampleEmail(false);
        }

        private void SendHtmlEmail()
        {
        	CrossMessaging.Current.EmailMessenger.SendSampleEmail(true);
        }

        private void SendSms()
        {
        	CrossMessaging.Current.SmsMessenger.SendSampleSms();
        }

        private void MakePhoneCall()
        {
        	CrossMessaging.Current.PhoneDialer.MakeSamplePhoneCall();
        }

    }
}
