using System.Windows;

namespace Nis.WpfApp.Views;

public partial class ShellView
{
    TestingView shl;

    public ShellView()
    {
        InitializeComponent();
        MessageBox.Show("Prihlaseni");
    }

    private void button1_Click(object sender, RoutedEventArgs e)
    {
        this.shl = new TestingView();
        shl.ShowDialog();
    }
}
