using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using SystemInfo.Core.Controllers;
using SystemInfo.Core.Poco;
using VectronsLibrary;
using VectronsLibrary.Extensions;

namespace SystemInfo.Core.ViewModels
{
    public class NetworkViewModel : ObservableObject, IDisposable, IViewModel
    {
        private readonly INetworkController networkController;
        private bool disposedValue = false;
        private IDisposable networkUseSubscription;

        public NetworkViewModel(INetworkController networkController)
        {
            this.networkController = networkController;
            Models = new ObservableCollection<NetworkData>();
            networkUseSubscription = networkController
               .NetworkUse
               .ObserveOnDispatcher()
               .Subscribe(x =>
               {
                   Models.Clear();
                   Models.AddRange(x);
               });
        }

        public ObservableCollection<NetworkData> Models
        {
            get;
            set;
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