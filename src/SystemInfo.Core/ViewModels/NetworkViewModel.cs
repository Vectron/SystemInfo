using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading;
using SystemInfo.Core.Controllers;
using SystemInfo.Core.Poco;
using VectronsLibrary;

namespace SystemInfo.Core.ViewModels
{
    public class NetworkViewModel : ObservableObject, IDisposable, IViewModel
    {
        private readonly INetworkController networkController;
        private bool disposedValue = false;
        private IDisposable networkUseSubscription;
        private object settings;

        public NetworkViewModel(INetworkController networkController)
        {
            this.networkController = networkController;
            Models = new ObservableCollection<NetworkData>();
            networkUseSubscription = networkController
               .NetworkUse
               .ObserveOn(SynchronizationContext.Current)
               .Subscribe(x => Models.UpdateAndRemove(x));
        }

        public ObservableCollection<NetworkData> Models
        {
            get;
            set;
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
                    networkUseSubscription?.Dispose();
                    networkUseSubscription = null;
                }

                disposedValue = true;
            }
        }
    }
}