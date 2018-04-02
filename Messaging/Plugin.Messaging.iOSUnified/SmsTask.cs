using System;
using MessageUI;
using UIKit;

namespace Plugin.Messaging
{
    internal class SmsTask : ISmsTask
    {
        private MFMessageComposeViewController _smsController;

        public SmsTask(SmsSettings settings)
        {
            Settings = settings;
        }

        #region ISmsTask Members

        public bool CanSendSms => MFMessageComposeViewController.CanSendText;
        public bool CanSendSmsInBackground => false;

        public void SendSms(string recipient = null, string message = null)
        {
            message = message ?? string.Empty;

            if (CanSendSms)
            {
                _smsController = new MFMessageComposeViewController();

                if (!string.IsNullOrWhiteSpace(recipient))
                { 
                    string[] recipients = recipient.Split(';'); 
                    if (recipients.Length > 0)  
                        _smsController.Recipients = recipients;
                }
                
                _smsController.Body = message;

                Settings.SmsPresenter.PresentMessageComposeViewController(_smsController);
            }
        }

        public void SendSmsInBackground(string recipient, string message = null)
        {
            throw new PlatformNotSupportedException("Sending SMS in background not supported on iOS");
        }

        #endregion

        #region Properties

        private SmsSettings Settings { get; }

        #endregion
    }
}
