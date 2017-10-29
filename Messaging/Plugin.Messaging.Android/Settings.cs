namespace Plugin.Messaging
{
    /// <summary>
    ///     Android settings for the plugin
    /// </summary>
    public class Settings
    {
        public Settings()
        {
            Email = new EmailSettings();
            Phone = new PhoneSettings();
        }

        #region Properties

        /// <summary>
        ///     Gets Android email settings
        /// </summary>
        public EmailSettings Email { get; }

        /// <summary>
        ///     Gets Android phone settings
        /// </summary>
        public PhoneSettings Phone { get; }

        #endregion
    }

    /// <summary>
    ///     Email settings for <see cref="IEmailTask" /> on the Android platform
    /// </summary>
    public class EmailSettings
    {
        #region Properties

        /// <summary>
        ///     Strict mode ensures that email can only be handled by email apps
        ///     and not also by any other text messaging or social apps.
        /// </summary>
        /// <remarks>
        ///     Please note that sending attachments with strict mode is not supported
        /// </remarks>
        public bool UseStrictMode { get; set; } = false;

        #endregion
    }

    /// <summary>
    ///     Phone settings for <see cref="IPhoneCallTask" /> on the Android platform
    /// </summary>
    public class PhoneSettings
    {
        #region Properties

        /// <summary>
        ///     AutoDial will automatically start dialing the phone number using the phone dialer
        ///     instead of only showing the phone dialer with the number populated. Default is <c>false</c>.
        /// </summary>
        /// <remarks>
        ///     Please note that if set to <c>true</c>, you need the <c>android.permission.CALL_PHONE</c>
        ///     permission added to your Android manifest file.
        /// </remarks>
        public bool AutoDial { get; set; } = false;

        /// <summary>
        ///     If the Phone number doesn't have a country code, the phone will be formatted using the default country's convention.
        /// </summary>
        /// <remarks>
        ///     The ISO 3166-1 two letters country code whose convention will be used if the given number doesn't have the country code.
        ///     Only available on API 21 or later.
        /// </remarks>
        public string DefaultCountryIso { get; set; }

        #endregion
    }
}