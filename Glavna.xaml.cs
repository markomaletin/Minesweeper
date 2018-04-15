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

namespace TVP_Projekat_2_Minesweeper
{
    /// <summary>
    /// Interaction logic for Glavna.xaml
    /// </summary>
    public partial class Glavna : Window
    {
        public Glavna()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (tbIme.Text.Length > 0)
            {
                Igra igra = new Igra(10, 15, new Igrac(tbIme.Text));
                App.Current.MainWindow = igra;
                this.Close();
                igra.Show();
            }
            else
            {
                MessageBox.Show("Unesite ime!");
            }
        }

        private void btnMedium_Click(object sender, RoutedEventArgs e)
        {
            if (tbIme.Text.Length > 0)
            {
                 Igra igra = new Igra(12, 25 ,new Igrac(tbIme.Text));
                 App.Current.MainWindow = igra;
                 this.Close();
                 igra.Show();
            }
            else
            {
                MessageBox.Show("Unesite ime!");
            }
        }

        private void btnHard_Click(object sender, RoutedEventArgs e)
        {
            if (tbIme.Text.Length > 0)
            {
                Igra igra = new Igra(14, 35, new Igrac(tbIme.Text));
                App.Current.MainWindow = igra;
                this.Close();
                igra.Show();
            }
            else
            {
                MessageBox.Show("Unesite ime!");
            }
        }

        private void btnRangListaHard_Click(object sender, RoutedEventArgs e)
        {
            PrikazRangListe r = new PrikazRangListe(14);
            App.Current.MainWindow = r;
            r.ShowDialog();
        }

        private void btnRangListaMedium_Click(object sender, RoutedEventArgs e)
        {
            PrikazRangListe r = new PrikazRangListe(12);
            App.Current.MainWindow = r;
            r.ShowDialog();
        }

        private void btnRangListaEasy_Click(object sender, RoutedEventArgs e)
        {
            PrikazRangListe r = new PrikazRangListe(10);
            App.Current.MainWindow = r;
            r.ShowDialog();
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
