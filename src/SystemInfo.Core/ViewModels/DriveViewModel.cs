using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using SystemInfo.Core.Controllers;
using SystemInfo.Core.Poco;
using VectronsLibrary;
using VectronsLibrary.Extensions;

namespace SystemInfo.Core.ViewModels
{
    public class DriveViewModel : ObservableObject, IDisposable, IViewModel
    {
        private readonly CompositeDisposable disposables = new CompositeDisposable();
        private readonly IDriveSpaceController driveSpaceController;
        private bool disposedValue = false;
        private UsageData total;

        public DriveViewModel(IDriveSpaceController driveSpaceController)
        {
            this.driveSpaceController = driveSpaceController;

            Drives = new ObservableCollection<DriveSpaceData>();
            var driveUseSubscription = driveSpaceController
                .HDDUse
                .ObserveOnDispatcher()
                .Subscribe(x =>
                {
                    Drives.Clear();
                    Drives.AddRange(x);
                });

            disposables.Add(driveUseSubscription);
            var totalUseSubscription = driveSpaceController
                .TotalHDDUse
                .ObserveOnDispatcher()
                .Subscribe(x => Total = x);

            disposables.Add(totalUseSubscription);
        }

        public ObservableCollection<DriveSpaceData> Drives
        {
            get;
            set;
        }

        public UsageData Total
        {
            get => total;
            set => SetField(ref total, value);
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
                }

                disposedValue = true;
            }
        }
    }
}