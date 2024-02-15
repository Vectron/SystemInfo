using System.Collections.Generic;
using SystemInfo.WPF.Settings;

namespace SystemInfo.WPF.ViewModels.MainWindow;

/// <summary>
/// Marker for the view model of the main window.
/// </summary>
public interface IMainWindowViewModel
{
    /// <summary>
    /// Gets an <see cref="ICollection{T}"/> view models to display.
    /// </summary>
    ICollection<object> Items
    {
        get;
    }

    /// <summary>
    /// Gets or sets the settings object for the window.
    /// </summary>
    WindowSettings WindowSettings
    {
        get;
        set;
    }
}
