using System;
using System.Globalization;
using System.Windows.Data;

namespace SystemInfo.WPF.Converters;

/// <summary>
/// An <see cref="IValueConverter"/> for converting ulong to string.
/// </summary>
[ValueConversion(typeof(ulong), typeof(string))]
internal sealed class BytesToStringConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (targetType != typeof(string))
        {
            throw new InvalidOperationException($"The target must be a {nameof(String)}");
        }

        if (parameter != null && parameter.GetType() == typeof(int))
        {
            return VectronsLibrary.Utils.FormatBytes((ulong)value, (int)parameter);
        }

        return VectronsLibrary.Utils.FormatBytes((ulong)value);
    }

    /// <inheritdoc/>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
