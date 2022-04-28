using System.Windows;

namespace Nis.WpfApp.Views
{

    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();

            MessageBox.Show("Prihlaseni");
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ShellView shl = new ShellView();
            shl.Show();
        }
    }
}
