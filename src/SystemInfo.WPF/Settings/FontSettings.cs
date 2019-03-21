using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VectronsLibrary;

namespace SystemInfo.WPF.Settings
{
    public class FontSettings : ObservableObject
    {
        private Color color;
        private FontFamily family;
        private double size;
        private FontStretch stretch;
        private FontStyle style;
        private FontWeight weight;

        public FontSettings()
        {
            Color = Colors.LightPink;
            Family = new FontFamily("Microsoft Sans Serif");
            size = 9;
            Stretch = (FontStretch)Control.FontStretchProperty.DefaultMetadata.DefaultValue;
            Style = (FontStyle)Control.FontStyleProperty.DefaultMetadata.DefaultValue;
            Weight = (FontWeight)Control.FontWeightProperty.DefaultMetadata.DefaultValue;
        }

        public Color Color
        {
            get => color;
            set => SetField(ref color, value);
        }

        public FontFamily Family
        {
            get => family;
            set => SetField(ref family, value);
        }

        public double Size
        {
            get => size;
            set => SetField(ref size, value);
        }

        public FontStretch Stretch
        {
            get => stretch;
            set => SetField(ref stretch, value);
        }

        public FontStyle Style
        {
            get => style;
            set => SetField(ref style, value);
        }

        public FontWeight Weight
        {
            get => weight;
            set => SetField(ref weight, value);
        }

        public override bool Equals(object obj)
            => obj is FontSettings settings &&
            Color.Equals(settings.Color) &&
            EqualityComparer<FontFamily>.Default.Equals(Family, settings.Family) &&
            Size == settings.Size &&
            EqualityComparer<FontStretch>.Default.Equals(Stretch, settings.Stretch) &&
            EqualityComparer<FontStyle>.Default.Equals(Style, settings.Style) &&
            EqualityComparer<FontWeight>.Default.Equals(Weight, settings.Weight);

        public override int GetHashCode()
        {
            var hashCode = 1588823014;
            hashCode = hashCode * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(Color);
            hashCode = hashCode * -1521134295 + EqualityComparer<FontFamily>.Default.GetHashCode(Family);
            hashCode = hashCode * -1521134295 + Size.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<FontStretch>.Default.GetHashCode(Stretch);
            hashCode = hashCode * -1521134295 + EqualityComparer<FontStyle>.Default.GetHashCode(Style);
            hashCode = hashCode * -1521134295 + EqualityComparer<FontWeight>.Default.GetHashCode(Weight);
            return hashCode;
        }
    }
}