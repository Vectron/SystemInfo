using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading;
using SystemInfo.Core.Controllers;
using SystemInfo.Core.Poco;
using VectronsLibrary;

namespace SystemInfo.Core.ViewModels;

/// <summary>
/// A view model for showing network information.
/// </summary>
public sealed class NetworkViewModel : ObservableObject, IDisposable, IViewModel
{
    private readonly IDisposable networkUseSubscription;
    private bool disposed;
    private object? settings;

    /// <summary>
    /// Initializes a new instance of the <see cref="NetworkViewModel"/> class.
    /// </summary>
    /// <param name="networkController">The <see cref="INetworkController"/>.</param>
    public NetworkViewModel(INetworkController networkController)
    {
        Models = [];
        networkUseSubscription = networkController
           .NetworkUse
           .ObserveOn(SynchronizationContext.Current ?? new SynchronizationContext())
           .Subscribe(x => Models.UpdateAndRemove(x));
    }

    /// <summary>
    /// Gets an <see cref="ObservableCollection{T}"/>. with data on all interfaces.
    /// </summary>
    public ObservableCollection<NetworkData> Models
    {
        get;
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
        networkUseSubscription.Dispose();
    }
}
