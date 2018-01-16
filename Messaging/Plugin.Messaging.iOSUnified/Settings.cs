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
        }

        #region Properties

        /// <summary>
        ///     Gets iOS email settings
        /// </summary>
        public EmailSettings Email { get; }

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
}