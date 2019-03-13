using Microsoft.Extensions.Options;
using System.Windows;
using System.Windows.Input;
using SystemInfo.WPF.Settings;
using VectronsLibrary.Wpf;

namespace SystemInfo.WPF.ViewModels.NotifyIcon
{
    public class NotifyIconViewModel : INotifyIconViewModel
    {
        private readonly IOptions<WindowSettings> options;

        public NotifyIconViewModel(IOptions<WindowSettings> options)
        {
            this.options = options;
        }

        public ICommand ExitApplicationCommand
            => new RelayCommand(_ => Application.Current.Shutdown());

        public ICommand OpenSettingsCommand
            => new RelayCommand(_ => { });

        public WindowSettings WindowSettings
            => options.Value;
    }
}