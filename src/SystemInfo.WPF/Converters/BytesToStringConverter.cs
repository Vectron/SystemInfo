using System;
using System.Globalization;
using System.Windows.Data;

namespace SystemInfo.WPF.Converters
{
    [ValueConversion(typeof(ulong), typeof(string))]
    internal class BytesToStringConverter : IValueConverter
    {
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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}