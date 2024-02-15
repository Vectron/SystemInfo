using System.Windows.Media;
using VectronsLibrary;

namespace SystemInfo.WPF.Settings
{
    /// <summary>
    /// Settings for the window.
    /// </summary>
    public class WindowSettings : ObservableObject
    {
        private Color backgroundColor;
        private Color borderColor;
        private FontSettings fontSettings;
        private double leftPosition;
        private bool lockPlacement;
        private double topPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowSettings"/> class.
        /// </summary>
        public WindowSettings()
        {
            BackgroundColor = Colors.Black;
            BorderColor = Color.FromArgb(0xFF, 0x66, 0x66, 0x66);
            LockPlacement = false;
            LeftPosition = 0D;
            TopPosition = 0D;
            CpuProgressBarSettings = new ProgressBarSettings() { Height = 4 };
            DrivesProgressBarSettings = new ProgressBarSettings();
            MemoryProgressBarSettings = new ProgressBarSettings();
            fontSettings = new FontSettings();
        }

        /// <summary>
        /// Gets or sets background color.
        /// </summary>
        public Color BackgroundColor
        {
            get => backgroundColor;
            set => SetField(ref backgroundColor, value);
        }

        /// <summary>
        /// Gets or sets the border color.
        /// </summary>
        public Color BorderColor
        {
            get => borderColor;
            set => SetField(ref borderColor, value);
        }

        /// <summary>
        /// Gets or sets the cpu progress bar settings.
        /// </summary>
        public ProgressBarSettings CpuProgressBarSettings
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the drives progress bar settings.
        /// </summary>
        public ProgressBarSettings DrivesProgressBarSettings
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the font settings.
        /// </summary>
        public FontSettings FontSettings
        {
            get => fontSettings;
            set => SetField(ref fontSettings, value);
        }

        /// <summary>
        /// Gets or sets the left position.
        /// </summary>
        public double LeftPosition
        {
            get => leftPosition;
            set => SetField(ref leftPosition, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the window is locked in position.
        /// </summary>
        public bool LockPlacement
        {
            get => lockPlacement;
            set => SetField(ref lockPlacement, value);
        }

        /// <summary>
        /// Gets or sets the memory progress bar settings.
        /// </summary>
        public ProgressBarSettings MemoryProgressBarSettings
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the windows top position.
        /// </summary>
        public double TopPosition
        {
            get => topPosition;
            set => SetField(ref topPosition, value);
        }
    }
}
