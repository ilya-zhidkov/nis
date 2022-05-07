using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nis.WpfApp.Views
{
    /// <summary>
    /// Interakční logika pro TestCheckView.xaml
    /// </summary>
    public partial class TestCheckView : UserControl
    {
        public TestCheckView()
        {
            InitializeComponent();
        }

        private void btn_end(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
    }
}
