using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.Options;
using SystemInfo.WPF.Settings;
using SystemInfo.WPF.ViewModels.MainWindow;
using VectronsLibrary;
using VectronsLibrary.Wpf;
using VectronsLibrary.Wpf.Dialogs;

namespace SystemInfo.WPF.ViewModels.Settings
{
    /// <summary>
    /// Implementation of <see cref="ISettingsViewModel"/>.
    /// </summary>
    public class SettingsViewModel : ObservableObject, ISettingsViewModel
    {
        private readonly WindowSettings defaultSettings = new WindowSettings();
        private readonly IOptions<WindowSettings> settings;
        private readonly ISettingsSaver settingsSaver;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        /// <param name="settings">The window settings.</param>
        /// <param name="mainWindowViewModel">The <see cref="IMainWindowViewModel"/>.</param>
        /// <param name="settingsSaver">The <see cref="ISettingsSaver"/>.</param>
        public SettingsViewModel(IOptions<WindowSettings> settings, IMainWindowViewModel mainWindowViewModel, ISettingsSaver settingsSaver)
        {
            this.settings = settings;
            MainWindowViewModel = mainWindowViewModel;
            this.settingsSaver = settingsSaver;
            mainWindowViewModel.WindowSettings = new WindowSettings();
            CopyWindowSettings(WindowSettings, NewSettings);
        }

        /// <summary>
        /// Gets an <see cref="ICommand"/> to change fonts.
        /// </summary>
        public ICommand ChangeFont
            => new RelayCommand(w =>
            {
                var parent = w as Window;
                var fontPicker = new FontPickerDialog(parent!)
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

                if (fontPicker.ShowDialog() == true)
                {
                    NewSettings.FontSettings.Color = fontPicker.SelectedFontColor;
                    NewSettings.FontSettings.Family = fontPicker.SelectedFontFamily;
                    NewSettings.FontSettings.Size = fontPicker.SelectedFontSize;
                    NewSettings.FontSettings.Stretch = fontPicker.SelectedFontStretch;
                    NewSettings.FontSettings.Style = fontPicker.SelectedFontStyle;
                    NewSettings.FontSettings.Weight = fontPicker.SelectedFontWeight;
                }
            });

        /// <summary>
        /// Gets the view model for the main window.
        /// </summary>
        public IMainWindowViewModel MainWindowViewModel
        {
            get;
        }

        /// <summary>
        /// Gets the new window settings.
        /// </summary>
        public WindowSettings NewSettings
            => MainWindowViewModel.WindowSettings;

        /// <summary>
        /// Gets an <see cref="ICommand"/> to apply the settings.
        /// </summary>
        public ICommand Ok
            => new RelayCommand(_ =>
            {
                CopyWindowSettings(NewSettings, WindowSettings);
                settingsSaver.SaveConfiguration();
            });

        /// <summary>
        /// Gets an <see cref="ICommand"/> to reset the settings to default..
        /// </summary>
        public ICommand ResetDefault
            => new RelayCommand(_ => CopyWindowSettings(defaultSettings, NewSettings));

        /// <summary>
        /// Gets an <see cref="ICommand"/> to reset the settings to the start value.
        /// </summary>
        public ICommand ResetStart
            => new RelayCommand(_ => CopyWindowSettings(WindowSettings, NewSettings));

        /// <summary>
        /// Gets the current window settings.
        /// </summary>
        public WindowSettings WindowSettings
            => settings.Value;

        private static void CopyFontSettings(FontSettings source, FontSettings target)
        {
            target.Color = source.Color;
            target.Family = source.Family;
            target.Size = source.Size;
            target.Stretch = source.Stretch;
            target.Style = source.Style;
            target.Weight = source.Weight;
        }

        private static void CopyProgressBarSettings(ProgressBarSettings source, ProgressBarSettings target)
        {
            target.BackgroundColor = source.BackgroundColor;
            target.ForegroundColorEnd = source.ForegroundColorEnd;
            target.ForegroundColorMiddle = source.ForegroundColorMiddle;
            target.ForegroundColorStart = source.ForegroundColorStart;
            target.Height = source.Height;
        }

        private static void CopyWindowSettings(WindowSettings source, WindowSettings target)
        {
            target.BackgroundColor = source.BackgroundColor;
            target.BorderColor = source.BorderColor;
            target.LeftPosition = source.LeftPosition;
            target.LockPlacement = source.LockPlacement;
            target.TopPosition = source.TopPosition;

            CopyProgressBarSettings(source.CpuProgressBarSettings, target.CpuProgressBarSettings);
            CopyProgressBarSettings(source.DrivesProgressBarSettings, target.DrivesProgressBarSettings);
            CopyProgressBarSettings(source.MemoryProgressBarSettings, target.MemoryProgressBarSettings);
            CopyFontSettings(source.FontSettings, target.FontSettings);
        }
    }
}
