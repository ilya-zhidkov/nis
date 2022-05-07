using System.Windows;

namespace Nis.WpfApp.Views;

public partial class TestCheckView
{
    public TestCheckView() => InitializeComponent();

    private void btn_end(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this)?.Close();
    }
}
