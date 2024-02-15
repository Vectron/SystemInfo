using System;
using System.Reactive.Linq;
using SystemInfo.Core.Controllers;
using SystemInfo.Core.Poco;
using VectronsLibrary;

namespace SystemInfo.Core.ViewModels;

/// <summary>
/// A view model that shows data of the memory.
/// </summary>
public sealed class MemoryViewModel : ObservableObject, IDisposable, IViewModel
{
    private readonly IDisposable memoryUseSubscription;
    private bool disposed;
    private UsageData model;
    private object? settings;

    /// <summary>
    /// Initializes a new instance of the <see cref="MemoryViewModel"/> class.
    /// </summary>
    /// <param name="memoryController">The <see cref="IMemoryController"/>.</param>
    public MemoryViewModel(IMemoryController memoryController)
    {
        model = new UsageData(0, 0);
        memoryUseSubscription = memoryController
            .MemoryUse
            .Subscribe(x => Model = x);
    }

    /// <summary>
    /// Gets or sets the usage of the memory.
    /// </summary>
    public UsageData Model
    {
        get => model;
        set => SetField(ref model, value);
    }

    /// <inheritdoc/>
    public object? Settings
    {
        get => settings;
        set => SetField(ref settings, value);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        disposed = true;
        memoryUseSubscription.Dispose();
    }
}
