using System.Collections.Generic;
using System.Windows.Media;
using VectronsLibrary;

namespace SystemInfo.WPF.Settings
{
    /// <summary>
    /// Settings for the progess bar.
    /// </summary>
    public class ProgressBarSettings : ObservableObject
    {
        private Color backgroundColor;
        private Color foregroundColorEnd;
        private Color foregroundColorMiddle;
        private Color foregroundColorStart;
        private double height;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBarSettings"/> class.
        /// </summary>
        public ProgressBarSettings()
        {
            Height = 5;
            BackgroundColor = Colors.Gray;
            ForegroundColorStart = Colors.LawnGreen;
            ForegroundColorMiddle = Colors.Yellow;
            ForegroundColorEnd = Colors.DarkRed;
        }

        /// <summary>
        /// Gets or sets the background color.
        /// </summary>
        public Color BackgroundColor
        {
            get => backgroundColor;
            set => SetField(ref backgroundColor, value);
        }

        /// <summary>
        /// Gets or sets the end foreground color.
        /// </summary>
        public Color ForegroundColorEnd
        {
            get => foregroundColorEnd;
            set => SetField(ref foregroundColorEnd, value);
        }

        /// <summary>
        /// Gets or sets the middle foreground color.
        /// </summary>
        public Color ForegroundColorMiddle
        {
            get => foregroundColorMiddle;
            set => SetField(ref foregroundColorMiddle, value);
        }

        /// <summary>
        /// Gets or sets the start foreground color.
        /// </summary>
        public Color ForegroundColorStart
        {
            get => foregroundColorStart;
            set => SetField(ref foregroundColorStart, value);
        }

        /// <summary>
        /// Gets or sets the bar height.
        /// </summary>
        public double Height
        {
            get => height;
            set => SetField(ref height, value);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
            => obj is ProgressBarSettings settings &&
            BackgroundColor.Equals(settings.BackgroundColor) &&
            ForegroundColorEnd.Equals(settings.ForegroundColorEnd) &&
            ForegroundColorMiddle.Equals(settings.ForegroundColorMiddle) &&
            ForegroundColorStart.Equals(settings.ForegroundColorStart) &&
            Height == settings.Height;

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = 927679445;
            hashCode = (hashCode * -1521134295) + EqualityComparer<Color>.Default.GetHashCode(BackgroundColor);
            hashCode = (hashCode * -1521134295) + EqualityComparer<Color>.Default.GetHashCode(ForegroundColorEnd);
            hashCode = (hashCode * -1521134295) + EqualityComparer<Color>.Default.GetHashCode(ForegroundColorMiddle);
            hashCode = (hashCode * -1521134295) + EqualityComparer<Color>.Default.GetHashCode(ForegroundColorStart);
            hashCode = (hashCode * -1521134295) + Height.GetHashCode();
            return hashCode;
        }
    }
}
