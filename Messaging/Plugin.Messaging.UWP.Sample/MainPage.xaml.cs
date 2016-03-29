using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using Plugin.Messaging.Sample;
using Windows.Storage.Pickers;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Plugin.Messaging.UWP.Sample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        #region Event Handlers

        private void ButtonPhoneCall_OnClick(object sender, RoutedEventArgs e)
        {
            CrossMessaging.Current.PhoneDialer.MakeSamplePhoneCall();
        }

        private async void ButtonSendAttachmentsEmail_OnClick(object sender, RoutedEventArgs e)
        {
            // NOTE: The calling app will be suspended and re-activated once the user has selected
            // a photo. To handle the selection of the photo, implement the IFileOpenPickerContinuable
            // on the Page that launched the SelectPicture call (see DeviceTaskApp for sample code)

            var openPicker = new FileOpenPicker();

            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            IStorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                var email = SamplesExtensions.BuildSampleEmail()
                                .WithAttachment(file)
                                .Build();

                CrossMessaging.Current.EmailMessenger.SendSampleEmail(email);
            }
        }

        private void ButtonSendEmail_OnClick(object sender, RoutedEventArgs e)
        {
            // NOTE: On Windows Phone Emulator, an exception occurs when using the email compose task. 
            // Test the email compose task on a physical device.

            CrossMessaging.Current.EmailMessenger.SendSampleEmail(false);
        }

        private void ButtonSendSms_OnClick(object sender, RoutedEventArgs e)
        {
            // NOTE: On Windows Phone Emulator, the SMS message always appears to be sent successfully, but the message is not actually sent. 
            // The emulator uses Fake GSM and always has a false Subscriber Identity Module (SIM) card.

            CrossMessaging.Current.SmsMessenger.SendSampleSms();
        }

        #endregion
    }
}
