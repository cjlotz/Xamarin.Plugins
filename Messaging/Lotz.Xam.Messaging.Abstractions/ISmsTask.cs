
using System;

namespace Lotz.Xam.Messaging.Abstractions
{
    // TODO: Consider multiple destinations
    // TODO: Consider adding SmsBuilder 

    /// <summary>
    ///     Abstraction for sending cross-platform sms messages using
    ///     the default sms messenger on the device.
    /// </summary>
    /// <remarks>
    ///     On Android platform, the android.permission.SEND_SMS needs
    ///     to be added to the Android manifest.
    /// </remarks>
    public interface ISmsTask
    {
        /// <summary>
        ///     Gets a value indicating whether the device can send a sms
        /// </summary>
        bool CanSendSms { get; }

        /// <summary>
        ///     Send the <paramref name="sms" /> using the default sms messenger
        ///     on the device
        /// </summary>
        /// <param name="sms">Sms request to send</param>
        /// <remarks>
        ///     On Android platform, the android.permission.SEND_SMS needs
        ///     to be added to the Android manifest.
        /// </remarks>
        void SendSms(SmsMessageRequest sms);
    }

    /// <summary>
    ///     Sms request used for sending sms.
    /// </summary>
    public class SmsMessageRequest
    {
        public SmsMessageRequest(string destinationAddress, string message)
        {
            if (string.IsNullOrWhiteSpace(destinationAddress))
                throw new ArgumentNullException("destinationAddress");

            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException("message");

            DestinationAddress = destinationAddress;
            Message = message;
        }

        #region Properties

        /// <summary>
        ///     Gets the address to send the sms to.
        /// </summary>
        public string DestinationAddress { get; private set; }

        /// <summary>
        ///     Gets the message to send within the sms.
        /// </summary>
        public string Message { get; private set; }

        #endregion
    }
}