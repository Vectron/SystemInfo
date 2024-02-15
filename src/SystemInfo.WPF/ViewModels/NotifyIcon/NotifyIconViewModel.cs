using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.Options;
using SystemInfo.WPF.Extensions;
using SystemInfo.WPF.Settings;
using SystemInfo.WPF.ViewModels.Settings;
using SystemInfo.WPF.Views;
using VectronsLibrary.Wpf;

namespace SystemInfo.WPF.ViewModels.NotifyIcon
{
    /// <summary>
    /// Implementation of <see cref="INotifyIconViewModel"/>.
    /// </summary>
    public class NotifyIconViewModel : INotifyIconViewModel
    {
        private readonly IOptions<WindowSettings> options;
        private readonly IServiceProvider serviceProvider;
        private readonly ISettingsSaver settingsSaver;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyIconViewModel"/> class.
        /// </summary>
        /// <param name="options">The window settings.</param>
        /// <param name="settingsSaver">The <see cref="ISettingsSaver"/>.</param>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/>.</param>
        public NotifyIconViewModel(IOptions<WindowSettings> options, ISettingsSaver settingsSaver, IServiceProvider serviceProvider)
        {
            this.options = options;
            this.settingsSaver = settingsSaver;
            this.serviceProvider = serviceProvider;
        }

        /// <inheritdoc/>
        public ICommand ExitApplicationCommand
            => new RelayCommand(_ =>
            {
                settingsSaver.SaveConfiguration();
                Application.Current.Shutdown();
            });

        /// <inheritdoc/>
        public ICommand OpenSettingsCommand
            => new RelayCommand(_ =>
            {
                var view = serviceProvider.GetViewScoped<SettingsView, ISettingsViewModel>();
                view.Show();
            });

        /// <inheritdoc/>
        public ICommand SaveSettingsCommand
            => new RelayCommand(_ => settingsSaver.SaveConfiguration());

        /// <summary>
        /// Gets the window settings.
        /// </summary>
        public WindowSettings WindowSettings
            => options.Value;
    }
}
