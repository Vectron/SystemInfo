using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SystemInfo.Core.ViewModels;
using SystemInfo.WPF.Settings;
using VectronsLibrary;
using VectronsLibrary.DI.Factory;

namespace SystemInfo.WPF.ViewModels.MainWindow
{
    public class MainWindowViewModel : ObservableObject, IMainWindowViewModel
    {
        private readonly IFactory<IViewModel> viewModelFactory;
        private WindowSettings windowSettings;

        public MainWindowViewModel(IFactory<IViewModel> viewModelFactory, IOptions<WindowSettings> options)
        {
            Items = new ObservableCollection<object>();
            this.viewModelFactory = viewModelFactory;
            windowSettings = options.Value;
            Items.Add(viewModelFactory.GetValue(nameof(CPUViewModel)));
            Items.Add(viewModelFactory.GetValue(nameof(MemoryViewModel)));
            Items.Add(viewModelFactory.GetValue(nameof(NetworkViewModel)));
            Items.Add(viewModelFactory.GetValue(nameof(DriveViewModel)));
            SetSettings();
        }

        public ICollection<object> Items
        {
            get;
        }

        public WindowSettings WindowSettings
        {
            get => windowSettings;
            set
            {
                SetField(ref windowSettings, value);
                SetSettings();
            }
        }

        private void SetSettings()
        {
            foreach (IViewModel item in Items)
            {
                switch (item.GetType().Name)
                {
                    case nameof(CPUViewModel):
                        item.Settings = windowSettings.CpuProgressbarSettings;
                        break;

                    case nameof(MemoryViewModel):
                        item.Settings = windowSettings.MemoryProgressbarSettings;
                        break;

                    case nameof(DriveViewModel):
                        item.Settings = windowSettings.DrivesProgressbarSettings;
                        break;

                    case nameof(NetworkViewModel):
                        break;

                    default:
                        break;
                }
            }
        }
    }
}