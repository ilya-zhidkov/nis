using System.Windows.Data;
using System.Globalization;

namespace Nis.WpfApp.Converters;

internal class BooleanToDisabilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => value is not null && !(bool)value;

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => value is not null && (bool)value;
}

