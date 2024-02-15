using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace SystemInfo.Core.Controllers;

/// <summary>
/// Implementation of <see cref="ICPUController"/>.
/// </summary>
public class CPUController : ICPUController
{
    /// <inheritdoc/>
    public IObservable<IEnumerable<float>> CoreUse
        => Observable
        .Create<IEnumerable<float>>(GetCoreUseStream)
        .Publish()
        .RefCount();

    /// <inheritdoc/>
    public IObservable<float> TotalCpuUse
        => Observable
        .Create<float>(GetTotalUseStream)
        .Publish()
        .RefCount();

    private IDisposable GetCoreUseStream(IObserver<IEnumerable<float>> observer)
    {
        var disposables = new CompositeDisposable();
        var numberOfCores = Environment.ProcessorCount;
        var coreCounters = new List<PerformanceCounter>();

        for (var i = 0; i < numberOfCores; i++)
        {
            var cpuCoreUse = new PerformanceCounter()
            {
                CategoryName = "Processor",
                CounterName = "% Processor Time",
                InstanceName = i.ToString(CultureInfo.InvariantCulture),
                ReadOnly = true,
            };

            disposables.Add(cpuCoreUse);
            coreCounters.Add(cpuCoreUse);
        }

        observer.OnNext(Enumerable.Repeat(0f, numberOfCores));

        var updateTimer = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
            .Select(_ => coreCounters.Select(counter => counter.NextValue()).ToArray())
            .Subscribe(observer);
        disposables.Add(updateTimer);
        return disposables;
    }

    private IDisposable GetTotalUseStream(IObserver<float> observer)
    {
        var disposables = new CompositeDisposable();
        var cpuTotalUse = new PerformanceCounter()
        {
            CategoryName = "Processor",
            CounterName = "% Processor Time",
            InstanceName = "_Total",
            ReadOnly = true,
        };

        disposables.Add(cpuTotalUse);
        var updateTimer = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
            .Select(_ => cpuTotalUse.NextValue())
            .Subscribe(observer);
        disposables.Add(updateTimer);

        return disposables;
    }
}
