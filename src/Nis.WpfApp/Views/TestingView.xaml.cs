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
    /// Interakční logika pro TestingView.xaml
    /// </summary>
    public partial class TestingView : Window
    {
        public TestingView()
        {
            InitializeComponent();
            button1.Visibility = Visibility.Hidden;
            textBox1.Visibility = Visibility.Hidden;
        }




        int mist = 0;


        private void mist_check(int mst)
        {
            if (mist >= 3)
                MessageBox.Show("Neúspěšné ukončení testu!");
        }

        private void mist_add()
        {
            mist++;
            mistn.Content = "Počet chyb: " + mist.ToString();
            mist_check(mist);
        }






        private void ComboBox1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ComboBox1.SelectedValue.ToString() == "Spravna odpoved!")
            {
                comboBox.IsEnabled = true;
                comboBox.Visibility = Visibility.Visible;

            }
            else
            {
                mist_add();
            }


        }

        private void comboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (comboBox.SelectedValue.ToString() == "Spravna odpoved!")
            {
                textBox.IsEnabled = true;
                textBox.Visibility = Visibility.Visible; //odkomentovat a zneviditelnit prislusne prvky az budu mit hotovo
                label.IsEnabled = true;
                label.Visibility = Visibility.Visible;
            }
            else
            {
                mist_add();
            }
        }

        private void textBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text != "")
            {
                MessageBox.Show("Odesláno!");
            }
            else
            {
                MessageBox.Show("Vyplňte všchna pole!");
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ShellView shl = new ShellView();
            shl.ShowDialog();

        }

    }
}
