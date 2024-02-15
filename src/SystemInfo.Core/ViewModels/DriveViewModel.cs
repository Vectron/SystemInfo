using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using SystemInfo.Core.Controllers;
using SystemInfo.Core.Poco;
using VectronsLibrary;

namespace SystemInfo.Core.ViewModels;

/// <summary>
/// A view model for showing disk data.
/// </summary>
public sealed class DriveViewModel : ObservableObject, IDisposable, IViewModel
{
    private readonly CompositeDisposable disposables = [];
    private bool disposed;
    private object? settings;
    private UsageData total;

    /// <summary>
    /// Initializes a new instance of the <see cref="DriveViewModel"/> class.
    /// </summary>
    /// <param name="driveSpaceController">The <see cref="IDriveSpaceController"/>.</param>
    public DriveViewModel(IDriveSpaceController driveSpaceController)
    {
        Drives = [];
        total = new UsageData(0, 0);
        var driveUseSubscription = driveSpaceController
            .HDDUse
            .ObserveOn(SynchronizationContext.Current ?? new SynchronizationContext())
            .Subscribe(x => Drives.UpdateAndRemove(x));

        disposables.Add(driveUseSubscription);
        var totalUseSubscription = driveSpaceController
            .TotalHDDUse
            .ObserveOn(SynchronizationContext.Current ?? new SynchronizationContext())
            .Subscribe(x => Total = x);

        disposables.Add(totalUseSubscription);
    }

    /// <summary>
    /// Gets an <see cref="ObservableCollection{T}"/> containing information of all drives.
    /// </summary>
    public ObservableCollection<DriveSpaceData> Drives
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
    /// Gets or sets the total use of the drives.
    /// </summary>
    public UsageData Total
    {
        get => total;
        set => SetField(ref total, value);
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
