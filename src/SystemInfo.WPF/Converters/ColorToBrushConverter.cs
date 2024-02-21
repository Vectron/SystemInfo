using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SystemInfo.WPF.Converters;

/// <summary>
/// An <see cref="IValueConverter"/> for converting colors to brushes.
/// </summary>
[ValueConversion(typeof(Color), typeof(Brush))]
public class ColorToBrushConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (targetType != typeof(Brush))
        {
            throw new InvalidOperationException($"The target must be a {nameof(Brush)}");
        }

        return new SolidColorBrush((Color)value);
    }

    /// <inheritdoc/>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (targetType != typeof(Color))
        {
            throw new InvalidOperationException($"The target must be a {nameof(Color)}");
        }

        return ((SolidColorBrush)value).Color;
    }
}
