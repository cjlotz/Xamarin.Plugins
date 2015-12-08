using System;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Plugin.Messaging.Sample.WinPhoneRT
{
    public class ContinuationManager
    {
        private IContinuationActivatedEventArgs _args;
        private bool _handled;

        #region Properties

        /// <summary>
        ///     Retrieves the continuation args, if they have not already been retrieved, and
        ///     prevents further retrieval via this property (to avoid accidentla double-usage)
        /// </summary>
        public IContinuationActivatedEventArgs ContinuationArgs
        {
            get
            {
                if (_handled)
                    return null;
                MarkAsStale();
                return _args;
            }
        }

        /// <summary>
        ///     Unique identifier for this particular continuation. Most useful for components that
        ///     retrieve the continuation data via <see cref="GetContinuationArgs" /> and need
        ///     to perform their own replay check
        /// </summary>
        public Guid Id { get; private set; } = Guid.Empty;

        #endregion

        #region Methods

        /// <summary>
        ///     Retrieves the continuation args, optionally retrieving them even if they have already
        ///     been retrieved
        /// </summary>
        /// <param name="includeStaleArgs">Set to true to return args even if they have previously been returned</param>
        /// <returns>The continuation args, or null if there aren't any</returns>
        public IContinuationActivatedEventArgs GetContinuationArgs(bool includeStaleArgs)
        {
            if (!includeStaleArgs && _handled)
                return null;
            MarkAsStale();
            return _args;
        }

        /// <summary>
        ///     Sets the ContinuationArgs for this instance. Using default Frame of current Window
        ///     Should be called by the main activation handling code in App.xaml.cs
        /// </summary>
        /// <param name="args">The activation args</param>
        internal void Continue(IContinuationActivatedEventArgs args)
        {
            Continue(args, Window.Current.Content as Frame);
        }

        /// <summary>
        ///     Sets the ContinuationArgs for this instance. Should be called by the main activation
        ///     handling code in App.xaml.cs
        /// </summary>
        /// <param name="args">The activation args</param>
        /// <param name="rootFrame">The frame control that contains the current page</param>
        internal void Continue(IContinuationActivatedEventArgs args, Frame rootFrame)
        {
            if (args == null)
                throw new ArgumentNullException("args");

            if (_args != null && !_handled)
                throw new InvalidOperationException("Can't set args more than once");

            _args = args;
            _handled = false;
            Id = Guid.NewGuid();

            if (rootFrame == null)
                return;

            switch (args.Kind)
            {
                case ActivationKind.PickFileContinuation:
                    var fileOpenPickerPage = rootFrame.Content as IFileOpenPickerContinuable;
                    if (fileOpenPickerPage != null)
                    {
                        fileOpenPickerPage.ContinueFileOpenPicker(args as FileOpenPickerContinuationEventArgs);
                    }
                    break;
            }
        }

        /// <summary>
        ///     Marks the contination data as 'stale', meaning that it is probably no longer of
        ///     any use. Called when the app is suspended (to ensure future activations don't appear
        ///     to be for the same continuation) and whenever the continuation data is retrieved
        ///     (so that it isn't retrieved on subsequent navigations)
        /// </summary>
        internal void MarkAsStale()
        {
            _handled = true;
        }

        #endregion
    }

    /// <summary>
    ///     Implement this interface if your page invokes the file open picker
    ///     API.
    /// </summary>
    public interface IFileOpenPickerContinuable
    {
        /// <summary>
        ///     This method is invoked when the file open picker returns picked
        ///     files
        /// </summary>
        /// <param name="args">Activated event args object that contains returned files from file open picker</param>
        void ContinueFileOpenPicker(FileOpenPickerContinuationEventArgs args);
    }
}