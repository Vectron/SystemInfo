using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using SystemInfo.Core.Poco;

namespace SystemInfo.Core.Controllers
{
    public class MemoryController : IMemoryController
    {
        public IObservable<UsageData> MemoryUse
            => Observable
            .Create<UsageData>(x => GetMemoryUseStream(x))
            .Publish()
            .RefCount();

        private IDisposable GetMemoryUseStream(IObserver<UsageData> observer)
        {
            var disposables = new CompositeDisposable();
            ulong totalVisibleMemorySize;

            using (var mos_System = new ManagementObjectSearcher("select TotalVisibleMemorySize from Win32_OperatingSystem"))
            {
                totalVisibleMemorySize = (ulong)mos_System
                    .Get()
                    .Cast<ManagementObject>()
                    .First()
                    .Properties["TotalVisibleMemorySize"]
                    .Value;
            }

            var availibleMemoryPerformanceCounter = new PerformanceCounter()
            {
                CategoryName = "Memory",
                CounterName = "Available bytes",
                ReadOnly = true,
            };

            disposables.Add(availibleMemoryPerformanceCounter);
            var updateTimer = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
                .Select(_ => availibleMemoryPerformanceCounter.NextValue())
                .Select(x => Convert.ToUInt64(x))
                .Select(x => new UsageData(totalVisibleMemorySize * 1024, x))
                .Subscribe(observer);
            disposables.Add(updateTimer);

            return disposables;
        }
    }
}