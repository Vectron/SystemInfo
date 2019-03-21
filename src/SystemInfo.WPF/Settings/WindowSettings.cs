using System.Windows.Media;
using VectronsLibrary;

namespace SystemInfo.WPF.Settings
{
    public class WindowSettings : ObservableObject
    {
        private Color backgroundColor;
        private Color borderColor;
        private FontSettings fontSettings;
        private double leftPosition;
        private bool lockPlacement;
        private double topPosition;

        public WindowSettings()
        {
            BackgroundColor = Colors.Black;
            BorderColor = Color.FromArgb(0xFF, 0x66, 0x66, 0x66);
            LockPlacement = false;
            LeftPosition = 0D;
            TopPosition = 0D;
            CpuProgressbarSettings = new ProgressbarSettings() { Height = 4 };
            DrivesProgressbarSettings = new ProgressbarSettings();
            MemoryProgressbarSettings = new ProgressbarSettings();
            FontSettings = new FontSettings();
        }

        public Color BackgroundColor
        {
            get => backgroundColor;
            set => SetField(ref backgroundColor, value);
        }

        public Color BorderColor
        {
            get => borderColor;
            set => SetField(ref borderColor, value);
        }

        public ProgressbarSettings CpuProgressbarSettings
        {
            get;
            set;
        }

        public ProgressbarSettings DrivesProgressbarSettings
        {
            get;
            set;
        }

        public FontSettings FontSettings
        {
            get => fontSettings;
            set => SetField(ref fontSettings, value);
        }

        public double LeftPosition
        {
            get => leftPosition;
            set => SetField(ref leftPosition, value);
        }

        public bool LockPlacement
        {
            get => lockPlacement;
            set => SetField(ref lockPlacement, value);
        }

        public ProgressbarSettings MemoryProgressbarSettings
        {
            get;
            set;
        }

        public double TopPosition
        {
            get => topPosition;
            set => SetField(ref topPosition, value);
        }
    }
}