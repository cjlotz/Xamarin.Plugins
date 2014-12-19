// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Lotz.Xam.Messaging.Abstractions;
using Lotz.Xam.Messaging.Samples;

namespace Lotz.Xam.Messaging.WinPhoneRT.Sample
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;
        }

        #region Methods

        /// <summary>
        ///     Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">
        ///     Event data that describes how this page was reached.
        ///     This parameter is typically used to configure the page.
        /// </param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        #endregion

        #region Event Handlers

        private void ButtonPhoneCall_OnClick(object sender, RoutedEventArgs e)
        {
            MessagingPlugin.PhoneDialer().MakeSamplePhoneCall();
        }

        private void ButtonSendEmail_OnClick(object sender, RoutedEventArgs e)
        {
            // NOTE: On Windows Phone Emulator, an exception occurs when using the email compose task. 
            // Test the email compose task on a physical device.

            MessagingPlugin.EmailMessenger().SendSampleEmail();
        }

        private void ButtonSendSms_OnClick(object sender, RoutedEventArgs e)
        {
            // NOTE: On Windows Phone Emulator, the SMS message always appears to be sent successfully, but the message is not actually sent. 
            // The emulator uses Fake GSM and always has a false Subscriber Identity Module (SIM) card.

            MessagingPlugin.SmsMessenger().SendSampleSms();
        }

        #endregion
    }
}