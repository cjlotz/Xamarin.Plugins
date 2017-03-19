using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Plugin.Messaging.Sample;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Plugin.Messaging.UWP.Sample
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        #region Methods

        private static async Task SendAttachmentEmail(bool usePlatformApi = true)
        {
            // NOTE: The calling app will be suspended and re-activated once the user has selected
            // a photo. To handle the selection of the photo, implement the IFileOpenPickerContinuable
            // on the Page that launched the SelectPicture call 

            var openPicker = new FileOpenPicker();

            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            IStorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                IEmailMessage email;
                if (usePlatformApi)
                {
                    email = SamplesExtensions.BuildSampleEmail()
                        .WithAttachment(file)
                        .Build();
                }
                else
                {
                    email = SamplesExtensions.BuildSampleEmail()
                        .WithAttachment(file.Path, file.ContentType)
                        .Build();
                }
                CrossMessaging.Current.EmailMessenger.SendSampleEmail(email);
            }
        }

        private static IStorageFile WithAttachment(string filePath)
        {
            var file = Task.Run(async () =>
            {
                try
                {
                    var f = await StorageFile.GetFileFromPathAsync(filePath).AsTask().ConfigureAwait(false);
                    return f;
                }
                catch (UnauthorizedAccessException ex)
                {
                    throw new PlatformNotSupportedException("Windows apps cannot access files by filePath unless they reside in ApplicationData. Use the platform-specific WithAttachment(IStorageFile) overload instead.");
                }
            }).Result;
            return file;
        }

        #endregion

        #region Event Handlers

        private void ButtonPhoneCall_OnClick(object sender, RoutedEventArgs e)
        {
            CrossMessaging.Current.PhoneDialer.MakeSamplePhoneCall();
        }

        private async void ButtonSendAttachmentsEmail_OnClick(object sender, RoutedEventArgs e)
        {
            await SendAttachmentEmail();
        }

        private async void ButtonSendAttachmentsEmailPCL_OnClick(object sender, RoutedEventArgs e)
        {
            await SendAttachmentEmail(false);
        }

        private void ButtonSendEmail_OnClick(object sender, RoutedEventArgs e)
        {
            // NOTE: On Windows Phone Emulator, an exception occurs when using the email compose task. 
            // Test the email compose task on a physical device.

            CrossMessaging.Current.EmailMessenger.SendSampleEmail(false);
        }

        private void ButtonSendMultipleSms_OnClick(object sender, RoutedEventArgs e)
        {
            // NOTE: On Windows Phone Emulator, the SMS message always appears to be sent successfully, but the message is not actually sent. 
            // The emulator uses Fake GSM and always has a false Subscriber Identity Module (SIM) card.

            CrossMessaging.Current.SmsMessenger.SendSampleMultipleSms();
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