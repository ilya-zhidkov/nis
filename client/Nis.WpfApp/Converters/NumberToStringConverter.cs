using System.Windows.Data;
using System.Globalization;

namespace Nis.WpfApp.Converters;

public class NumberToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (double)value <= 0 ? "Čas vypršel!" : (double)value;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}
