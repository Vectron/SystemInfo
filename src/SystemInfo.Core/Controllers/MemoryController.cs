using System.Diagnostics;
using System.Management;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using SystemInfo.Core.Poco;

namespace SystemInfo.Core.Controllers;

/// <summary>
/// Implementation of <see cref="IMemoryController"/>.
/// </summary>
public class MemoryController : IMemoryController
{
    /// <inheritdoc/>
    public IObservable<UsageData> MemoryUse
        => Observable
        .Create<UsageData>(GetMemoryUseStream)
        .Publish()
        .RefCount();

    private IDisposable GetMemoryUseStream(IObserver<UsageData> observer)
    {
        var disposables = new CompositeDisposable();
        ulong totalVisibleMemorySize;

        using (var mos_System = new ManagementObjectSearcher("select TotalVisibleMemorySize from Win32_OperatingSystem"))
        {
            using var managementBaseObjects = mos_System.Get();
            totalVisibleMemorySize = (ulong)managementBaseObjects
                .Cast<ManagementObject>()
                .First()
                .Properties["TotalVisibleMemorySize"]
                .Value;
        }

        var availableMemoryPerformanceCounter = new PerformanceCounter()
        {
            CategoryName = "Memory",
            CounterName = "Available bytes",
            ReadOnly = true,
        };

        disposables.Add(availableMemoryPerformanceCounter);
        var updateTimer = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
            .Select(_ => availableMemoryPerformanceCounter.NextValue())
            .Select(Convert.ToUInt64)
            .Select(x => new UsageData(totalVisibleMemorySize * 1024, x))
            .Subscribe(observer);
        disposables.Add(updateTimer);

        return disposables;
    }
}
