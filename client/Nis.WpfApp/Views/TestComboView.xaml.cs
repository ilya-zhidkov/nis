using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Nis.WpfApp.Views;

public partial class TestComboView
{
    int mist;
    DispatcherTimer timer = new();
    int stopwatch = 0;

    public TestComboView()
    {
        InitializeComponent();
        timer.Tick += timer_tick;
        timer.Interval = new TimeSpan(0, 0, 0, 1);
        timer.Start();
        cmb_diet.Visibility = Visibility.Hidden;
        cmb_diet.IsEnabled = false;
        lbL_diet.Visibility = Visibility.Hidden;
        cmb_odd.Visibility = Visibility.Hidden;
        cmb_odd.IsEnabled = false;
        lbL_odd.Visibility = Visibility.Hidden;
        int mist = 0;
    }

    private void timer_tick(Object sender, EventArgs e)
    {
        stopwatch++;
        if (stopwatch > 30)
        {
            MessageBox.Show("Cas vyprsel!");
            timer.Stop();
            btn_check.IsEnabled = false;
        }

        lb_timer.Content = stopwatch;
    }

    private void mist_check(int mst)
    {
        if (mist >= 3)
        {
            MessageBox.Show("Neúspěšné ukončení testu!");
            btn_check.IsEnabled = false;
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

    private void cmb_diag_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

    private void cmb_odd_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (cmb_odd.SelectedValue.ToString() == "Spravna odpoved!")
        {
            cmb_diet.IsEnabled = true;
            cmb_diet.Visibility =
                Visibility.Visible; //odkomentovat a zneviditelnit prislusne prvky az budu mit hotovo
            lbL_diet.Visibility = Visibility.Visible;
            cmb_odd.IsEnabled = false;
        }
        else
        {
            mist_add();
        }
    }

    private void cmb_diet_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (cmb_diet.SelectedValue.ToString() == "Spravna odpoved!")
        {
            btn_check.IsEnabled = true;
            cmb_diet.IsEnabled = false;
            timer.Stop();
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
