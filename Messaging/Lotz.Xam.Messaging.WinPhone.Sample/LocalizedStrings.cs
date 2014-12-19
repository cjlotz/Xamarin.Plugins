using Lotz.Xam.Messaging.WinPhone.Sample.Resources;

namespace Lotz.Xam.Messaging.WinPhone.Sample
{
    /// <summary>
    /// Provides access to string resources.
    /// </summary>
    public class LocalizedStrings
    {
        private static AppResources _localizedResources = new AppResources();

        public AppResources LocalizedResources { get { return _localizedResources; } }
    }
}