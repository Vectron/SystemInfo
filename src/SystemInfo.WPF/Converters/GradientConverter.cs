using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace SystemInfo.WPF.Converters;

/// <summary>
/// An <see cref="IMultiValueConverter"/> for converting gradients.
/// </summary>
public class GradientConverter : IMultiValueConverter
{
    /// <inheritdoc/>
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var brush = new LinearGradientBrush
        {
            StartPoint = new Point(0, 0),
            EndPoint = new Point(1, 1),
        };

        if (values.Length != 3
            || (values[0].GetType() != typeof(Color))
            || (values[1].GetType() != typeof(Color))
            || (values[2].GetType() != typeof(Color)))
        {
            return brush;
        }

        brush.GradientStops.Add(new GradientStop((Color)values[0], 0));
        brush.GradientStops.Add(new GradientStop((Color)values[1], 0.6));
        brush.GradientStops.Add(new GradientStop((Color)values[2], 1));

        return brush;
    }

    /// <inheritdoc/>
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
