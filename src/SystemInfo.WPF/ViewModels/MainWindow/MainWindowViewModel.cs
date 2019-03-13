using System.Collections.Generic;
using System.Collections.ObjectModel;
using SystemInfo.Core.ViewModels;
using SystemInfo.WPF.Settings;
using VectronsLibrary.DI.Factory;

namespace SystemInfo.WPF.ViewModels.MainWindow
{
    public class MainWindowViewModel : IMainWindowViewModel
    {
        private readonly IFactory<IViewModel> viewModelFactory;

        public MainWindowViewModel(IFactory<IViewModel> viewModelFactory)
        {
            WindowSettings = new WindowSettings();
            Items = new ObservableCollection<object>();
            this.viewModelFactory = viewModelFactory;

            Items.Add(viewModelFactory.GetValue("CPUViewModel"));
            Items.Add(viewModelFactory.GetValue("MemoryViewModel"));
            Items.Add(viewModelFactory.GetValue("NetworkViewModel"));
            Items.Add(viewModelFactory.GetValue("DriveViewModel"));
        }

        public ICollection<object> Items
        {
            get;
        }

        public WindowSettings WindowSettings
        {
            get;
            set;
        }
    }
}