using System.Windows;

namespace Nis.WpfApp.Views;

public partial class MedicalScaleView
{
    public MedicalScaleView() => InitializeComponent();

    private void btn_end(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this)?.Close();
    }
}
