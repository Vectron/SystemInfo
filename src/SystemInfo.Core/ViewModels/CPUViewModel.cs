using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using SystemInfo.Core.Controllers;
using VectronsLibrary;

namespace SystemInfo.Core.ViewModels
{
    public class CPUViewModel : ObservableObject, IDisposable, IViewModel
    {
        private readonly ICPUController cpuController;
        private readonly CompositeDisposable disposables = new CompositeDisposable();
        private bool disposedValue = false;
        private object settings;
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
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(x => CoreUses.UpdateAndRemove(x));
            disposables.Add(coreUseSubscription);
        }

        public ObservableCollection<float> CoreUses
        {
            get;
        }

        public object Settings
        {
            get => settings;
            set => SetField(ref settings, value);
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