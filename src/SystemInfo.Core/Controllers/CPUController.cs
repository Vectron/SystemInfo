using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace SystemInfo.Core.Controllers
{
    public class CPUController
    {
        public IObservable<IEnumerable<int>> CoreUse
            => Observable
            .Create<IEnumerable<int>>(x => GetCoreUseStream(x))
            .Publish()
            .RefCount();

        public IObservable<int> TotalCpuUse
            => Observable
            .Create<int>(x => GetTotalUseStream(x))
            .Publish()
            .RefCount();

        private IDisposable GetCoreUseStream(IObserver<IEnumerable<int>> observer)
        {
            var disposables = new CompositeDisposable();
            var NumberOfCores = Environment.ProcessorCount;
            var coreCounters = new List<PerformanceCounter>();

            for (int i = 0; i < NumberOfCores; i++)
            {
                var CpuCoreUse = new PerformanceCounter()
                {
                    CategoryName = "Processor",
                    CounterName = "% Processor Time",
                    InstanceName = i.ToString(),
                    ReadOnly = true
                };

                disposables.Add(CpuCoreUse);
                coreCounters.Add(CpuCoreUse);
            }

            var updateTimer = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
            .Select(_ => coreCounters.Select(counter => counter.NextValue()).Select(use => Utils.FloatToPercent(use)))
            .Subscribe(observer);
            disposables.Add(updateTimer);
            return disposables;
        }

        private IDisposable GetTotalUseStream(IObserver<int> observer)
        {
            var disposables = new CompositeDisposable();
            var CpuTotalUse = new PerformanceCounter()
            {
                CategoryName = "Processor",
                CounterName = "% Processor Time",
                InstanceName = "_Total",
                ReadOnly = true
            };

            disposables.Add(CpuTotalUse);
            var updateTimer = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
                .Select(_ => CpuTotalUse.NextValue())
                .Select(use => Utils.FloatToPercent(use))
                .Subscribe(observer);
            disposables.Add(updateTimer);

            return disposables;
        }
    }
}