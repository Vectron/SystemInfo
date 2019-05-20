using Microsoft.Extensions.Options;
using System;
using System.Windows;
using System.Windows.Input;
using SystemInfo.WPF.Extensions;
using SystemInfo.WPF.Settings;
using SystemInfo.WPF.ViewModels.Settings;
using SystemInfo.WPF.Views;
using VectronsLibrary.Wpf;

namespace SystemInfo.WPF.ViewModels.NotifyIcon
{
    public class NotifyIconViewModel : INotifyIconViewModel
    {
        private readonly IOptions<WindowSettings> options;
        private readonly IServiceProvider serviceProvider;
        private readonly ISettingsSaver settingsSaver;

        public NotifyIconViewModel(IOptions<WindowSettings> options, ISettingsSaver settingsSaver, IServiceProvider serviceProvider)
        {
            this.options = options;
            this.settingsSaver = settingsSaver;
            this.serviceProvider = serviceProvider;
        }

        public ICommand ExitApplicationCommand
            => new RelayCommand(_ =>
            {
                settingsSaver.SaveConfiguration();
                Application.Current.Shutdown();
            });

        public ICommand OpenSettingsCommand
            => new RelayCommand(_ =>
            {
                var view = serviceProvider.GetViewScoped<SettingsView, ISettingsViewModel>();
                view.Show();
            });

        public ICommand SaveSettingsCommand
            => new RelayCommand(_ => settingsSaver.SaveConfiguration());

        public WindowSettings WindowSettings
            => options.Value;
    }
}