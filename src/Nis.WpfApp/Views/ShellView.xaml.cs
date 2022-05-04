using System.Windows;

namespace Nis.WpfApp.Views
{

    public partial class ShellView : Window
    {
        TestingView shl;

        public ShellView()
        {
            InitializeComponent();
            MessageBox.Show("Prihlaseni");
            this.shl = new TestingView();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            shl.ShowDialog();
        }
    }
}
