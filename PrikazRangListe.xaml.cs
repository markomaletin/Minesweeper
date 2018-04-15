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
    /// Interaction logic for PrikazRangListe.xaml
    /// </summary>
    public partial class PrikazRangListe : Window
    {
        public PrikazRangListe()
        {
            InitializeComponent();
        }

        public PrikazRangListe(int kategorija) : this()
        {
            switch(kategorija)
            {
                case 10:
                    {
                        lblTezina.Content = "Top 10(Easy):";
                        break;
                    }
                case 12:
                    {
                        lblTezina.Content = "Top 10(Medium):";
                        break;
                    }
                case 14:
                    {
                        lblTezina.Content = "Top 10(Hard):";
                        break;
                    }
            }

           List<Igrac> igraci = TopLista.sviIgraciZaKategoriju (kategorija);

            for(int i=0; i<igraci.Count; i++)
            {
                tbPrikaz.Text +="   "+(i+1) +". "+ igraci[i].ToString()+ Environment.NewLine;
            }
        }
    }
}
