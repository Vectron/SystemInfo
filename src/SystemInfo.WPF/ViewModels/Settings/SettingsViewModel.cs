using Microsoft.Extensions.Options;
using System.Windows;
using System.Windows.Input;
using SystemInfo.WPF.Settings;
using SystemInfo.WPF.ViewModels.MainWindow;
using VectronsLibrary;
using VectronsLibrary.Wpf;
using VectronsLibrary.Wpf.Dialogs;

namespace SystemInfo.WPF.ViewModels.Settings
{
    public class SettingsViewModel : ObservableObject, ISettingsViewModel
    {
        private readonly WindowSettings defaultSettings = new WindowSettings();
        private readonly IOptions<WindowSettings> settings;
        private readonly ISettingsSaver settingsSaver;

        public SettingsViewModel(IOptions<WindowSettings> settings, IMainWindowViewModel mainWindowViewModel, ISettingsSaver settingsSaver)
        {
            this.settings = settings;
            MainWindowViewModel = mainWindowViewModel;
            this.settingsSaver = settingsSaver;
            mainWindowViewModel.WindowSettings = new WindowSettings();
            CopyWindowSettings(WindowSettings, NewSettings);
        }

        public ICommand ChangeFont
            => new RelayCommand(w =>
            {
                var parrent = w as Window;

                var fontpicker = new FontPickerDialog(parrent)
                {
                    DefaultFontColor = defaultSettings.FontSettings.Color,
                    DefaultFontFamily = defaultSettings.FontSettings.Family,
                    DefaultFontSize = defaultSettings.FontSettings.Size,
                    DefaultFontStretch = defaultSettings.FontSettings.Stretch,
                    DefaultFontStyle = defaultSettings.FontSettings.Style,
                    DefaultFontWeight = defaultSettings.FontSettings.Weight,

                    SelectedFontColor = NewSettings.FontSettings.Color,
                    SelectedFontFamily = NewSettings.FontSettings.Family,
                    SelectedFontSize = NewSettings.FontSettings.Size,
                    SelectedFontStretch = NewSettings.FontSettings.Stretch,
                    SelectedFontStyle = NewSettings.FontSettings.Style,
                    SelectedFontWeight = NewSettings.FontSettings.Weight,
                };

                if (fontpicker.ShowDialog() == true)
                {
                    NewSettings.FontSettings.Color = fontpicker.SelectedFontColor;
                    NewSettings.FontSettings.Family = fontpicker.SelectedFontFamily;
                    NewSettings.FontSettings.Size = fontpicker.SelectedFontSize;
                    NewSettings.FontSettings.Stretch = fontpicker.SelectedFontStretch;
                    NewSettings.FontSettings.Style = fontpicker.SelectedFontStyle;
                    NewSettings.FontSettings.Weight = fontpicker.SelectedFontWeight;
                }
            });

        public IMainWindowViewModel MainWindowViewModel
        {
            get;
        }

        public WindowSettings NewSettings
            => MainWindowViewModel.WindowSettings;

        public ICommand Ok
            => new RelayCommand(_ =>
            {
                CopyWindowSettings(NewSettings, WindowSettings);
                settingsSaver.SaveConfiguration();
            });

        public ICommand ResetDefault
            => new RelayCommand(_ => CopyWindowSettings(defaultSettings, NewSettings));

        public ICommand ResetStart
            => new RelayCommand(_ => CopyWindowSettings(WindowSettings, NewSettings));

        public WindowSettings WindowSettings
            => settings.Value;

        private void CopyFontSettings(FontSettings source, FontSettings target)
        {
            target.Color = source.Color;
            target.Family = source.Family;
            target.Size = source.Size;
            target.Stretch = source.Stretch;
            target.Style = source.Style;
            target.Weight = source.Weight;
        }

        private void CopyProgressbarSettings(ProgressbarSettings source, ProgressbarSettings target)
        {
            target.BackgroundColor = source.BackgroundColor;
            target.ForegroundColorEnd = source.ForegroundColorEnd;
            target.ForegroundColorMiddle = source.ForegroundColorMiddle;
            target.ForegroundColorStart = source.ForegroundColorStart;
            target.Height = source.Height;
        }

        private void CopyWindowSettings(WindowSettings source, WindowSettings target)
        {
            target.BackgroundColor = source.BackgroundColor;
            target.BorderColor = source.BorderColor;
            target.LeftPosition = source.LeftPosition;
            target.LockPlacement = source.LockPlacement;
            target.TopPosition = source.TopPosition;

            CopyProgressbarSettings(source.CpuProgressbarSettings, target.CpuProgressbarSettings);
            CopyProgressbarSettings(source.DrivesProgressbarSettings, target.DrivesProgressbarSettings);
            CopyProgressbarSettings(source.MemoryProgressbarSettings, target.MemoryProgressbarSettings);
            CopyFontSettings(source.FontSettings, target.FontSettings);
        }
    }
}