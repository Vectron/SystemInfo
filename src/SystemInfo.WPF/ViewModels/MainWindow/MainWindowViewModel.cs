using System.Collections.Generic;
using System.Collections.ObjectModel;
using SystemInfo.WPF.Settings;

namespace SystemInfo.WPF.ViewModels.MainWindow
{
    public class MainWindowViewModel : IMainWindowViewModel
    {
        public MainWindowViewModel()
        {
            WindowSettings = new WindowSettings();
            Items = new ObservableCollection<object>();
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