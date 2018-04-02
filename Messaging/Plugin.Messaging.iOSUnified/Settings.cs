namespace Plugin.Messaging
{
    /// <summary>
    ///     iOS settings for the plugin
    /// </summary>
    public class Settings
    {
        public Settings()
        {
            Email = new EmailSettings();
            Sms = new SmsSettings();
        }

        #region Properties

        /// <summary>
        ///     Gets iOS email settings
        /// </summary>
        public EmailSettings Email { get; }

        /// <summary>
        ///     Gets iOS sms settings
        /// </summary>
        public SmsSettings Sms { get; }

        #endregion
    }

    /// <summary>
    ///     Email settings for <see cref="IEmailTask" /> on the iOS platform
    /// </summary>
    public class EmailSettings
    {
        #region Properties

        /// <summary>
        ///     Mail presenter to use for showing MFMailComposeViewController. 
        ///     Defaults to <see cref="DefaultEmailPresenter"/>. Create custom
        ///     implementation if default doesn't match your app navigation requirements.
        /// </summary>
        public IEmailPresenter EmailPresenter { get; set; } = new DefaultEmailPresenter();

        #endregion
    }

    /// <summary>
    ///     Sms settings for <see cref="ISmsTask" /> on the iOS platform
    /// </summary>
    public class SmsSettings
    {
        #region Properties

        /// <summary>
        ///     Mail presenter to use for showing MFMessageComposeViewController. 
        ///     Defaults to <see cref="DefaultSmsPresenter"/>. Create custom
        ///     implementation if default doesn't match your app navigation requirements.
        /// </summary>
        public ISmsPresenter SmsPresenter { get; set; } = new DefaultSmsPresenter();

        #endregion
    }

}