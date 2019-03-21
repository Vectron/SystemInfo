using Microsoft.Extensions.Options;
using SystemInfo.WPF.Settings;
using VectronsLibrary;

namespace SystemInfo.WPF.ViewModels.Settings
{
    public class SettingsViewModel : ObservableObject, ISettingsViewModel
    {
        private readonly IOptions<WindowSettings> settings;

        public SettingsViewModel(IOptions<WindowSettings> settings)
        {
            this.settings = settings;
        }

        public WindowSettings WindowSettings
            => settings.Value;
    }
}