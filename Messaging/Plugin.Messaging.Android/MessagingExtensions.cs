using System;
using Android.Content;

namespace Plugin.Messaging
{
    internal static class MessagingExtensions
    {
        #region Methods

        public static void StartNewActivity(this Intent intent)
        {
            if (intent == null)
                throw new ArgumentNullException(nameof(intent));

            intent.SetFlags(ActivityFlags.ClearTop);
            intent.SetFlags(ActivityFlags.NewTask);
            
            Android.App.Application.Context.StartActivity(intent);
        }

        #endregion
    }

    public static class SettingsExtensions
    {
        public static Settings Settings(this IMessaging messaging)
        {
            return ((MessagingImplementation)CrossMessaging.Current).Settings;
        }
    }
}