using System;
using System.Reactive.Linq;
using SystemInfo.Core.Controllers;
using SystemInfo.Core.Poco;
using VectronsLibrary;

namespace SystemInfo.Core.ViewModels
{
    public class MemoryViewModel : ObservableObject, IDisposable, IViewModel
    {
        private readonly IMemoryController memoryController;
        private bool disposedValue = false;
        private IDisposable memoryUseSubscription;
        private UsageData model;
        private object settings;

        public MemoryViewModel(IMemoryController memoryController)
        {
            this.memoryController = memoryController;
            memoryUseSubscription = memoryController
               .MemoryUse
               .ObserveOnDispatcher()
               .Subscribe(x => Model = x);
        }

        public UsageData Model
        {
            get => model;
            set => SetField(ref model, value);
        }

        public object Settings
        {
            get => settings;
            set => SetField(ref settings, value);
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
                    memoryUseSubscription?.Dispose();
                    memoryUseSubscription = null;
                }

                disposedValue = true;
            }
        }
    }
}