using System.Windows.Input;

namespace SystemInfo.WPF.ViewModels.NotifyIcon
{
    /// <summary>
    /// A view model for the notification area icon.
    /// </summary>
    public interface INotifyIconViewModel
    {
        /// <summary>
        /// Gets an <see cref="ICommand"/> to exit the application.
        /// </summary>
        ICommand ExitApplicationCommand
        {
            get;
        }

        /// <summary>
        /// Gets an <see cref="ICommand"/> to open the settings window.
        /// </summary>
        ICommand OpenSettingsCommand
        {
            get;
        }

        /// <summary>
        /// Gets an <see cref="ICommand"/> to save settings.
        /// </summary>
        ICommand SaveSettingsCommand
        {
            get;
        }
    }
}
