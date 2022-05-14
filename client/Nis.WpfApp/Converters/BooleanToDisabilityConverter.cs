using System.Windows.Data;
using System.Globalization;

namespace Nis.WpfApp.Converters;

public class BooleanToDisabilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => !(bool)value;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => (bool)value;
}

