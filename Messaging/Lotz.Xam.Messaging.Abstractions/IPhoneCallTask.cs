namespace Lotz.Xam.Messaging
{
    /// <summary>
    ///     Abstraction for making cross-platform phone calls on the device.
    /// </summary>
    /// <remarks>
    ///     On WinPhone platform, the ID_CAP_PHONEDIALER capability needs to be
    ///     added to the application manifest
    /// </remarks>
    public interface IPhoneCallTask
    {
        /// <summary>
        ///     Gets a value indicating whether the device can make a phone call
        /// </summary>
        bool CanMakePhoneCall { get; }

        /// <summary>
        ///     Make a phone call using the default dialer UI on the device.
        /// </summary>
        /// <param name="number">Number to phone</param>
        /// <param name="name">Optional name of the contact being phoned used for visual display
        /// on some platforms</param>
        /// <remarks>
        ///     On WinPhone platform, the ID_CAP_PHONEDIALER capability needs to be
        ///     added to the application manifest
        /// </remarks>
        void MakePhoneCall(string number, string name = null);
    }
}