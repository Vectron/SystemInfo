using System.Windows.Media;
using VectronsLibrary;

namespace SystemInfo.WPF.Settings
{
    public class WindowSettings : ObservableObject
    {
        private string backgroundColor;
        private string borderColor;
        private double leftPosition;
        private bool lockPlacement;
        private double topPosition;

        public WindowSettings()
        {
            BackgroundColor = Colors.Black.ToString();
            BorderColor = "#FF666666";
            LockPlacement = false;
            LeftPosition = 0D;
            TopPosition = 0D;
        }

        public string BackgroundColor
        {
            get => backgroundColor;
            set => SetField(ref backgroundColor, value);
        }

        public string BorderColor
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