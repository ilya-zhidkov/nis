using System.Windows.Data;
using System.Globalization;
using Nis.WpfApp.Extensions;

namespace Nis.WpfApp.Converters;

public class ToCapitalCaseConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is not string input || string.IsNullOrWhiteSpace(input) ? string.Empty : input.Capitalize();

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}
