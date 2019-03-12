using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using SystemInfo.Core.Controllers;
using VectronsLibrary;
using VectronsLibrary.Extensions;

namespace SystemInfo.Core.ViewModels
{
    public class CPUViewModel : ObservableObject, IDisposable, IViewModel
    {
        private readonly ICPUController cpuController;
        private readonly CompositeDisposable disposables = new CompositeDisposable();
        private bool disposedValue = false;
        private float totalUse;

        public CPUViewModel(ICPUController cpuController)
        {
            CoreUses = new ObservableCollection<float>();
            this.cpuController = cpuController;
            var totalCpuUseSubscription = cpuController
                .TotalCpuUse
                .Subscribe(x => TotalUse = x);
            disposables.Add(totalCpuUseSubscription);

            var coreUseSubscription = cpuController
                .CoreUse
                .ObserveOnDispatcher()
                .Subscribe(x =>
                {
                    CoreUses.Clear();
                    CoreUses.AddRange(x);
                });
            disposables.Add(coreUseSubscription);
        }

        public ObservableCollection<float> CoreUses
        {
            get;
        }

        public float TotalUse
        {
            get => totalUse;
            set => SetField(ref totalUse, value);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    disposables.Dispose();
                }

                disposedValue = true;
            }
        }
    }
}