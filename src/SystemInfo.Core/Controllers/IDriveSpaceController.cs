using System;
using System.Collections.Generic;
using SystemInfo.Core.Poco;

namespace SystemInfo.Core.Controllers;

/// <summary>
/// An controller for monitoring hdd space usage.
/// </summary>
public interface IDriveSpaceController
{
    /// <summary>
    /// Gets an <see cref="IObservable{T}"/> with data on every drive.
    /// </summary>
    IObservable<IEnumerable<DriveSpaceData>> HDDUse
    {
        get;
    }

    /// <summary>
    /// Gets an <see cref="IObservable{T}"/> reporting total drive use.
    /// </summary>
    IObservable<UsageData> TotalHDDUse
    {
        get;
    }
}
