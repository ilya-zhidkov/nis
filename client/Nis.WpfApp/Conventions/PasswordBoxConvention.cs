using System.Windows;
using System.Reflection;
using System.Windows.Controls;

namespace Nis.WpfApp.Conventions;

public static class PasswordBoxConvention
{
    public static readonly DependencyProperty BoundPasswordProperty =
        DependencyProperty.RegisterAttached(
            name: "BoundPassword",
            propertyType: typeof(string),
            ownerType: typeof(PasswordBoxConvention),
            defaultMetadata: new FrameworkPropertyMetadata(string.Empty, OnBoundPasswordChanged)
        );

    private static string GetBoundPassword(DependencyObject control)
    {
        if (control is not PasswordBox input)
            return (string)control.GetValue(BoundPasswordProperty);

        input.PasswordChanged -= PasswordChanged;
        input.PasswordChanged += PasswordChanged;

        return (string)control.GetValue(BoundPasswordProperty);
    }

    private static void SetBoundPassword(DependencyObject control, string value)
    {
        if (string.Equals(value, GetBoundPassword(control)))
            return;

        control.SetValue(BoundPasswordProperty, value);
    }

    private static void OnBoundPasswordChanged(
        DependencyObject control,
        DependencyPropertyChangedEventArgs arguments
    )
    {
        if (control is not PasswordBox input)
            return;

        input.Password = GetBoundPassword(control);
    }

    private static void PasswordChanged(object sender, RoutedEventArgs arguments)
    {
        var input = sender as PasswordBox;

        SetBoundPassword(input, input?.Password);

        input?.GetType().GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic)?.Invoke(input, new object[] { input.Password.Length, 0 });
    }
}
