using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using SystemInfo.Core.Controllers;
using VectronsLibrary;

namespace SystemInfo.Core.ViewModels;

/// <summary>
/// A view model for displaying the Cpu data.
/// </summary>
public sealed class CPUViewModel : ObservableObject, IDisposable, IViewModel
{
    private readonly CompositeDisposable disposables = [];
    private bool disposed;
    private object? settings;
    private float totalUse;

    /// <summary>
    /// Initializes a new instance of the <see cref="CPUViewModel"/> class.
    /// </summary>
    /// <param name="cpuController">The <see cref="ICPUController"/>.</param>
    public CPUViewModel(ICPUController cpuController)
    {
        CoreUses = [];
        var totalCpuUseSubscription = cpuController
            .TotalCpuUse
            .Subscribe(x => TotalUse = x);
        disposables.Add(totalCpuUseSubscription);

        var coreUseSubscription = cpuController
            .CoreUse
            .ObserveOn(SynchronizationContext.Current ?? new SynchronizationContext())
            .Subscribe(x => CoreUses.UpdateAndRemove(x));
        disposables.Add(coreUseSubscription);
    }

    /// <summary>
    /// Gets the usage of each core.
    /// </summary>
    public ObservableCollection<float> CoreUses
    {
        get;
    }

    /// <inheritdoc/>
    public object? Settings
    {
        get => settings;
        set => SetField(ref settings, value);
    }

    /// <summary>
    /// Gets or sets the total cpu usage.
    /// </summary>
    public float TotalUse
    {
        get => totalUse;
        set => SetField(ref totalUse, value);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        disposed = true;
        disposables.Dispose();
    }
}
