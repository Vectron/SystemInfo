using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SystemInfo.WPF.Converters
{
    [ValueConversion(typeof(Color), typeof(Brush))]
    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Brush))
            {
                throw new InvalidOperationException($"The target must be a {nameof(Brush)}");
            }

            return new SolidColorBrush((Color)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Color))
            {
                throw new InvalidOperationException($"The target must be a {nameof(Color)}");
            }

            return ((SolidColorBrush)value).Color;
        }
    }
}