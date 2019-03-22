using System.Collections.Generic;
using SystemInfo.WPF.Settings;

namespace SystemInfo.WPF.ViewModels.MainWindow
{
    public interface IMainWindowViewModel
    {
        ICollection<object> Items
        {
            get;
        }

        WindowSettings WindowSettings
        {
            get;
            set;
        }
    }
}