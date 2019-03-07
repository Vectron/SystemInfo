using System.Windows;
using System.Windows.Input;
using VectronsLibrary.Wpf;

namespace SystemInfo.WPF.ViewModels.NotifyIcon
{
    public class NotifyIconViewModel : INotifyIconViewModel
    {
        public NotifyIconViewModel()
        {
        }

        public ICommand ExitApplicationCommand
            => new RelayCommand(_ => Application.Current.Shutdown());

        public ICommand OpenSettingsCommand
            => new RelayCommand(_ => { });
    }
}