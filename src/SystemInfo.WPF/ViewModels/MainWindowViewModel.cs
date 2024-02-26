using Microsoft.Extensions.Options;
using SystemInfo.Core.ViewModels;
using SystemInfo.WPF.Settings;
using Vectron.Extensions.DependencyInjection.Factory;
using VectronsLibrary;

namespace SystemInfo.WPF.ViewModels;

/// <summary>
/// The view model for the main window.
/// </summary>
public class MainWindowViewModel : ObservableObject, IMainWindowViewModel
{
    private readonly IFactory<IViewModel> viewModelFactory;
    private WindowSettings windowSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
    /// </summary>
    /// <param name="viewModelFactory">The <see cref="IFactory{IViewModel}"/>.</param>
    /// <param name="options">The window settings.</param>
    public MainWindowViewModel(IFactory<IViewModel> viewModelFactory, IOptions<WindowSettings> options)
    {
        Items = [];
        this.viewModelFactory = viewModelFactory;
        windowSettings = options.Value;
        Items.Add(viewModelFactory.GetValue(nameof(CPUViewModel)));
        Items.Add(viewModelFactory.GetValue(nameof(MemoryViewModel)));
        Items.Add(viewModelFactory.GetValue(nameof(NetworkViewModel)));
        Items.Add(viewModelFactory.GetValue(nameof(DriveViewModel)));
        SetSettings();
    }

    /// <inheritdoc/>
    public ICollection<object> Items
    {
        get;
    }

    /// <inheritdoc/>
    public WindowSettings WindowSettings
    {
        get => windowSettings;
        set
        {
            _ = SetField(ref windowSettings, value);
            SetSettings();
        }
    }

    private void SetSettings()
    {
        foreach (var item in Items.Cast<IViewModel>())
        {
            switch (item.GetType().Name)
            {
                case nameof(CPUViewModel):
                    item.Settings = null;
                    item.Settings = windowSettings.CpuProgressBarSettings;
                    break;

                case nameof(MemoryViewModel):
                    item.Settings = null;
                    item.Settings = windowSettings.MemoryProgressBarSettings;
                    break;

                case nameof(DriveViewModel):
                    item.Settings = null;
                    item.Settings = windowSettings.DrivesProgressBarSettings;
                    break;

                case nameof(NetworkViewModel):
                    break;

                default:
                    break;
            }
        }
    }
}
