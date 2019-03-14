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
        private readonly ISettingsSaver settingsSaver;

        public NotifyIconViewModel(IOptions<WindowSettings> options, ISettingsSaver settingsSaver)
        {
            this.options = options;
            this.settingsSaver = settingsSaver;
        }

        public ICommand ExitApplicationCommand
            => new RelayCommand(_ =>
            {
                settingsSaver.SaveConfiguration();
                Application.Current.Shutdown();
            });

        public ICommand OpenSettingsCommand
            => new RelayCommand(_ => { });

        public WindowSettings WindowSettings
            => options.Value;
    }
}