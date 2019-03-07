using System.Windows.Input;

namespace SystemInfo.WPF.ViewModels.NotifyIcon
{
    public interface INotifyIconViewModel
    {
        ICommand ExitApplicationCommand
        {
            get;
        }

        ICommand OpenSettingsCommand
        {
            get;
        }
    }
}