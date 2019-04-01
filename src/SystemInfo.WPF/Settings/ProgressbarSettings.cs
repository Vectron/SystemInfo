using System.Collections.Generic;
using System.Windows.Media;
using VectronsLibrary;

namespace SystemInfo.WPF.Settings
{
    public class ProgressbarSettings : ObservableObject
    {
        private Color backgroundColor;
        private Color foregroundColorEnd;
        private Color foregroundColorMiddle;
        private Color foregroundColorStart;
        private double height;

        public ProgressbarSettings()
        {
            Height = 5;
            BackgroundColor = Colors.Gray;
            ForegroundColorStart = Colors.LawnGreen;
            ForegroundColorMiddle = Colors.Yellow;
            ForegroundColorEnd = Colors.DarkRed;
        }

        public Color BackgroundColor
        {
            get => backgroundColor;
            set => SetField(ref backgroundColor, value);
        }

        public Color ForegroundColorEnd
        {
            get => foregroundColorEnd;
            set => SetField(ref foregroundColorEnd, value);
        }

        public Color ForegroundColorMiddle
        {
            get => foregroundColorMiddle;
            set => SetField(ref foregroundColorMiddle, value);
        }

        public Color ForegroundColorStart
        {
            get => foregroundColorStart;
            set => SetField(ref foregroundColorStart, value);
        }

        public double Height
        {
            get => height;
            set => SetField(ref height, value);
        }

        public override bool Equals(object obj)
            => obj is ProgressbarSettings settings &&
            BackgroundColor.Equals(settings.BackgroundColor) &&
            ForegroundColorEnd.Equals(settings.ForegroundColorEnd) &&
            ForegroundColorMiddle.Equals(settings.ForegroundColorMiddle) &&
            ForegroundColorStart.Equals(settings.ForegroundColorStart) &&
            Height == settings.Height;

        public override int GetHashCode()
        {
            var hashCode = 927679445;
            hashCode = hashCode * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(BackgroundColor);
            hashCode = hashCode * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(ForegroundColorEnd);
            hashCode = hashCode * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(ForegroundColorMiddle);
            hashCode = hashCode * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(ForegroundColorStart);
            hashCode = hashCode * -1521134295 + Height.GetHashCode();
            return hashCode;
        }
    }
}