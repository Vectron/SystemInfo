using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using SystemInfo.Core.Poco;

namespace SystemInfo.Core.Controllers
{
    /// <summary>
    /// Implementation of <see cref="IDriveSpaceController"/>.
    /// </summary>
    public class DriveSpaceController : IDriveSpaceController
    {
        /// <inheritdoc/>
        public IObservable<IEnumerable<DriveSpaceData>> HDDUse
            => Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(10))
            .Select(_ => DriveInfo
                .GetDrives()
                .Where(d => d.IsReady && (d.DriveType == DriveType.Fixed || d.DriveType == DriveType.Network))
                .Select(d =>
                {
                    try
                    {
                        return new DriveSpaceData(d.Name, (ulong)d.TotalSize, (ulong)d.AvailableFreeSpace);
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                })
                .Where(x => x != null)
                .Select(x => x!))
            .Publish()
            .RefCount();

        /// <inheritdoc/>
        public IObservable<UsageData> TotalHDDUse
            => Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(10))
            .Select(_ => GetTotalHDDUse())
            .Publish()
            .RefCount();

        private static UsageData GetTotalHDDUse()
        {
            var allDrives = DriveInfo
                .GetDrives()
                .Where(d => d.IsReady && (d.DriveType == DriveType.Fixed));

            ulong totalSize = 0;
            ulong totalAvailable = 0;

            foreach (var drive in allDrives)
            {
                totalSize += (ulong)drive.TotalSize;
                totalAvailable += (ulong)drive.AvailableFreeSpace;
            }

            return new UsageData(totalSize, totalAvailable);
        }
    }
}
