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
using System.Windows.Shapes;

namespace Nis.WpfApp.Views
{
    /// <summary>
    /// Okno, které se otevře po stisknutí tlačítka "Testovani".
    /// </summary>
    public partial class TestingView : Window
    {
        int mist;

        public TestingView()
        {
            InitializeComponent();
            cmb_diet.Visibility = Visibility.Hidden;
            cmb_diet.IsEnabled = false;
            lbL_diet.Visibility = Visibility.Hidden;
            cmb_odd.Visibility = Visibility.Hidden;
            cmb_odd.IsEnabled = false;
            lbL_odd.Visibility = Visibility.Hidden;
            int mist = 0;
        }
        

        private void mist_check(int mst)
        {
            if (mist >= 3)
            {
                MessageBox.Show("Neúspěšné ukončení testu!");
                button.IsEnabled = false;
                cmb_diag.IsEnabled = false;
                cmb_odd.IsEnabled = false;
                cmb_diet.IsEnabled = false;
            }
        }

        private void mist_add()
        {
            mist++;
            mistn.Content = "Počet chyb: " + mist.ToString();
            mist_check(mist);
        }






        private void cmb_diag_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cmb_diag.SelectedValue.ToString() == "Spravna odpoved!")
            {
                cmb_odd.IsEnabled = true;
                cmb_odd.Visibility = Visibility.Visible;
                lbL_odd.Visibility = Visibility.Visible;
                cmb_diag.IsEnabled = false;
            }
            else
            {
                mist_add();
            }


        }

        private void cmb_odd_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
           if (cmb_odd.SelectedValue.ToString() == "Spravna odpoved!")
            {
                cmb_diet.IsEnabled = true;
                cmb_diet.Visibility = Visibility.Visible; //odkomentovat a zneviditelnit prislusne prvky az budu mit hotovo
                lbL_diet.Visibility = Visibility.Visible;
                cmb_odd.IsEnabled = false;
            }
            else
            {
                mist_add();
            }
        }

        private void cmb_diet_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cmb_diet.SelectedValue.ToString() == "Spravna odpoved!")
            {
                button.IsEnabled = true;
                cmb_diet.IsEnabled = false;
            }
            else
            {
                mist_add();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Odesláno!");
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ShellView shl = new ShellView();
            shl.ShowDialog();
        }

    }
}
