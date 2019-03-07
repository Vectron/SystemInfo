using System.Windows.Media;
using VectronsLibrary;

namespace SystemInfo.WPF.Settings
{
    public class WindowSettings : ObservableObject
    {
        private Brush backgroundColor;
        private Brush borderColor;
        private double leftPosition;
        private bool lockPlacement;
        private double topPosition;

        public WindowSettings()
        {
            BackgroundColor = new SolidColorBrush(Colors.Black);
            BorderColor = new SolidColorBrush(Color.FromArgb(0xFF, 0x66, 0x66, 0x66));
            LockPlacement = false;
            LeftPosition = 0D;
            TopPosition = 0D;
        }

        public Brush BackgroundColor
        {
            get => backgroundColor;
            set => SetField(ref backgroundColor, value);
        }

        public Brush BorderColor
        {
            get => borderColor;
            set => SetField(ref borderColor, value);
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

        public double TopPosition
        {
            get => topPosition;
            set => SetField(ref topPosition, value);
        }
    }
}