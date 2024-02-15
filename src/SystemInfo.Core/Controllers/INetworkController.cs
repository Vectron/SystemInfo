using System;
using System.Collections.Generic;
using SystemInfo.Core.Poco;

namespace SystemInfo.Core.Controllers;

/// <summary>
/// A controller for monitoring the network use.
/// </summary>
public interface INetworkController
{
    /// <summary>
    /// Gets an <see cref="IObservable{T}"/> containing the network use.
    /// </summary>
    IObservable<IEnumerable<NetworkData>> NetworkUse
    {
        get;
    }
}
