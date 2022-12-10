using System.Text.RegularExpressions;

namespace Nis.WpfApp.Extensions;

public static class StringExtensions
{
    public static string Capitalize(this string input) => string.IsNullOrWhiteSpace(input) ? string.Empty : $"{input[..1].ToUpper()}{input[1..].ToLower()}";

    public static string Sanitize(this string input) => Regex.Replace(input, "<.*?>", string.Empty);
}
