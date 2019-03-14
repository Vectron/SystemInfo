using System.Collections.Generic;
using System.Windows.Media;
using VectronsLibrary;

namespace SystemInfo.WPF.Settings
{
    public class ProgressbarSettings : ObservableObject
    {
        private string backgroundColor;
        private string foregroundColorEnd;
        private string foregroundColorMiddle;
        private string foregroundColorStart;
        private double height;

        public ProgressbarSettings()
        {
            Height = 5;
            BackgroundColor = Colors.Gray.ToString();
            ForegroundColorStart = Colors.LawnGreen.ToString();
            ForegroundColorMiddle = Colors.Yellow.ToString();
            ForegroundColorEnd = Colors.DarkRed.ToString();
        }

        public string BackgroundColor
        {
            get => backgroundColor;
            set => SetField(ref backgroundColor, value);
        }

        public string ForegroundColorEnd
        {
            get => foregroundColorEnd;
            set => SetField(ref foregroundColorEnd, value);
        }

        public string ForegroundColorMiddle
        {
            get => foregroundColorMiddle;
            set => SetField(ref foregroundColorMiddle, value);
        }

        public string ForegroundColorStart
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
        {
            return obj as ProgressbarSettings != null &&
                   backgroundColor == (obj as ProgressbarSettings).backgroundColor &&
                   foregroundColorEnd == (obj as ProgressbarSettings).foregroundColorEnd &&
                   foregroundColorMiddle == (obj as ProgressbarSettings).foregroundColorMiddle &&
                   foregroundColorStart == (obj as ProgressbarSettings).foregroundColorStart &&
                   height == (obj as ProgressbarSettings).height;
        }

        public override int GetHashCode()
        {
            var hashCode = 831672693;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(backgroundColor);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(foregroundColorEnd);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(foregroundColorMiddle);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(foregroundColorStart);
            hashCode = hashCode * -1521134295 + height.GetHashCode();
            return hashCode;
        }
    }
}