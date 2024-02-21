using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.Options;
using SystemInfo.WPF.Extensions;
using SystemInfo.WPF.Settings;
using SystemInfo.WPF.ViewModels.Settings;
using SystemInfo.WPF.Views;
using VectronsLibrary.Wpf;

namespace SystemInfo.WPF.ViewModels.NotifyIcon;

/// <summary>
/// Implementation of <see cref="INotifyIconViewModel"/>.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="NotifyIconViewModel"/> class.
/// </remarks>
/// <param name="options">The window settings.</param>
/// <param name="settingsSaver">The <see cref="ISettingsSaver"/>.</param>
/// <param name="serviceProvider">The <see cref="IServiceProvider"/>.</param>
public class NotifyIconViewModel(IOptions<WindowSettings> options, ISettingsSaver settingsSaver, IServiceProvider serviceProvider) : INotifyIconViewModel
{
    private readonly IOptions<WindowSettings> options = options;
    private readonly IServiceProvider serviceProvider = serviceProvider;
    private readonly ISettingsSaver settingsSaver = settingsSaver;

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
