using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TVP_Projekat_2_Minesweeper
{
    /// <summary>
    /// Interaction logic for EasyTezina.xaml
    /// </summary>
    public partial class Igra : Window
    {

       
        private int velicina_matrice;
        private int broj_bombi;
        private int broj_zastavica=0;
        int broj_pritisnutih_dugmica = 0;

        DispatcherTimer tajmer;
        int brojac_vremena = 1;
       
        private Button[,] dugmici;

        Igrac trenutni_igrac;

        Grid grid;

        public Igra()
        {
            InitializeComponent();
           
        }

        public Igra(int velicina_matrice, int broj_bombi, Igrac igrac) : this()
        {
            
            this.trenutni_igrac = igrac;
            this.velicina_matrice = velicina_matrice;
            this.broj_bombi = broj_bombi;
            this.broj_zastavica = 0;

            grid = new Grid();
          
            grid.Width = velicina_matrice * 20 + 17;
            grid.Height = velicina_matrice * 20;
           
            Width = grid.Width;
            Height = grid.Height+90;
                      
            //dinamicko formiranje grida
            for (int i = 0; i < velicina_matrice; i++)
            {
               
                    ColumnDefinition cd = new ColumnDefinition();
                    cd.Width = new GridLength(20);
                    grid.ColumnDefinitions.Add(cd);

                    RowDefinition rd = new RowDefinition();
                    rd.Height = new GridLength(20);
                    grid.RowDefinitions.Add(rd);
                
            }

            matrica.Children.Add(grid);


            dugmici = new Button[velicina_matrice, velicina_matrice];
            

            for (int i = 0; i < velicina_matrice; i++)
            {
                for (int j = 0; j < velicina_matrice; j++)
                {

                    dugmici[i, j] = new Button();
                    dugmici[i, j].Style = (this.FindResource("dugmici") as Style);
                    dugmici[i, j].Tag = new Polje(false, Vrednost_polja.PRAZNO_POLJE, i, j);
                    dugmici[i, j].IsEnabled = true;
                    dugmici[i, j].Click += pritisnutoDugme;
                    dugmici[i, j].MouseRightButtonDown += desniKlik;
                }
            }

            tbBombe.Text = broj_bombi+"";

            Funkcionalnost.dodavanjeBombi(dugmici, broj_bombi, velicina_matrice);
            Funkcionalnost.ispisBrojeva(dugmici, velicina_matrice);
            Funkcionalnost.dodajDugmice(dugmici, velicina_matrice, grid);


            //Tajmer
            tajmer = new DispatcherTimer();
            tajmer.Tick += new EventHandler(TimerProc);
            tajmer.Interval = new TimeSpan(0, 0, 1);

            tbVreme.Text = formirajVreme(0);

        }

        
        //metoda tajmera
        private void TimerProc(object sender, EventArgs e)
        {
           
            tbVreme.Text = formirajVreme(brojac_vremena);
            brojac_vremena++;
        }

        //formatiranje vremena
        private static string formirajVreme(int sekunde)
        {
            int min = sekunde / 60;
            int sek = sekunde % 60;
            string ispis = "";

            if (min < 10) ispis = "0" + min + ":";
            else ispis = min.ToString();

            if (sek < 10) ispis += "0" + sek;
            else ispis += sek.ToString();

            return ispis;
        }
        


        private void pritisnutoDugme(object sender, EventArgs e)
        {
            
            if(broj_pritisnutih_dugmica==0)
            {
                tajmer.Start();
            }
            
            Button dugme = (Button)sender;
            Polje polje_dugmeta = (Polje)dugme.Tag;

            if (dugme.IsEnabled && !polje_dugmeta.Zastavica)
            {
               

                if (polje_dugmeta.Vrednost.Equals(Vrednost_polja.BOMBA))
                {
                   
                    btnRestart.Content = (this.FindResource("sad") as Image);
                    otvoriSvaPolja();
                    dugme.Content = (this.FindResource("bomb_red") as Image);
                    tajmer.Stop();
                    return;
                }

                else if (polje_dugmeta.Vrednost.Equals(Vrednost_polja.BROJ))
                {
                   
                    Image slika_broj = new Image();
                    slika_broj.Source = Funkcionalnost.obojiBroj(this, polje_dugmeta.Broj_bombi_okolo).Source;
                    dugme.Content = slika_broj;
                    dugme.IsEnabled = false;
                   
                }

                else
                {
                    dugme.IsEnabled = false;
                    otvori_prazna_polja_okolo(dugme);
                  
                }

               

                broj_pritisnutih_dugmica = 0;
                //broji pritisnute dugmice
                for (int i = 0; i < velicina_matrice; i++)
                {
                    for (int j = 0; j < velicina_matrice; j++)
                    {
                        if(!dugmici[i,j].IsEnabled)
                        {
                            dugmici[i, j].Style = this.FindResource("dugmici_pritisnuto") as Style;
                            broj_pritisnutih_dugmica++;
                        }
                        
                    }
                }


                //ako su ostale samo bombe
                if ((velicina_matrice * velicina_matrice - broj_bombi) == broj_pritisnutih_dugmica)
                {
                    //Pokretanje animacije
                    btnRestart.BeginStoryboard (this.FindResource("animacija") as Storyboard);
                     tajmer.Stop();
                    trenutni_igrac.Broj_bodova = 1000 - brojac_vremena;
                   

                    if (TopLista.dodajIgracaZaKategoriju(velicina_matrice, trenutni_igrac))
                    {
                        MessageBox.Show("Osvojili ste "+ trenutni_igrac.Broj_bodova + " boda i nalazite se medju 10 najboljih igrača ");
                    }
                    else
                    {
                        MessageBox.Show("Osvojili ste " + trenutni_igrac.Broj_bodova + " boda ali niste uspeli da se plasirate medju 10 najboljih igrača ");

                    }

                    //postavlja zastavice na mestima gde se nalaze bombe kada su ostale samo bombe
                    for (int i = 0; i < velicina_matrice; i++)
                    {
                        for (int j = 0; j < velicina_matrice; j++)
                        {
                            if (((Polje)dugmici[i, j].Tag).Vrednost==Vrednost_polja.BOMBA)
                            {
                                ((Polje)dugmici[i, j].Tag).Zastavica = true;
                                dugmici[i, j].MouseRightButtonDown -= desniKlik;
                                Image img = new Image();
                                img.Source = (this.FindResource("flag") as Image).Source;
                                dugmici[i, j].Content = img;
                            }
                        }
                    }



                }

            }




        }

       


        //Postavljanje i skidanje zastavica
        private void desniKlik(object sender, MouseEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                Button dugme = (Button)sender;

                if (!((Polje)dugme.Tag).Zastavica && broj_zastavica < broj_bombi)
                {
                    
                    Image img = new Image();
                    img.Source = (this.FindResource("flag")as Image).Source;
                    dugme.Content = img;
                    ((Polje)dugme.Tag).Zastavica = true;
                    broj_zastavica++;
                   
                }
                else if (((Polje)dugme.Tag).Zastavica)
                {
                    dugme.Content = "";
                    ((Polje)dugme.Tag).Zastavica = false;
                    broj_zastavica--;
                }

                tbBombe.Text = (broj_bombi - broj_zastavica)+"";


                //ako su postavljene sve zastavice ispituje da li je kraj igre
                if(broj_bombi==broj_zastavica)
                {
                    int br = 0;
                    for (int i = 0; i < velicina_matrice; i++)
                    {
                        for (int j = 0; j < velicina_matrice; j++)
                        {
                            Polje polje = dugmici[i, j].Tag as Polje;
                            if(polje.Zastavica && polje.Vrednost==Vrednost_polja.BOMBA)
                            {
                                br++;
                            }
                        }
                    }

                    if(br==broj_bombi)
                    {
                        otvoriSvaPolja();
                        tajmer.Stop();
                        
                        Storyboard sb = this.FindResource("animacija") as Storyboard;
                        btnRestart.BeginStoryboard(sb);


                        trenutni_igrac.Broj_bodova = 1000 - brojac_vremena;

                        if(TopLista.dodajIgracaZaKategoriju(velicina_matrice, trenutni_igrac))
                        {
                            MessageBox.Show("Osvojili ste " + trenutni_igrac.Broj_bodova + " boda i nalazite se medju 10 najboljih igrača ");
                        }
                        else
                        {
                            MessageBox.Show("Osvojili ste " + trenutni_igrac.Broj_bodova + " boda ali niste uspeli da se plasirate medju 10 najboljih igrača ");

                        }

                    }

                }



            }
        }




       
        //otvara sva prazna polja oko praznog polja na koji si kliknuo
        private void otvori_prazna_polja_okolo( Button dugme)
        {
            List<Button> prazna_polja;
            prazna_polja = new List<Button>();
            prazna_polja.Add(dugme);


            while(prazna_polja.Count>0)
            {
                Polje polje = (Polje)prazna_polja[0].Tag;
                int i = polje.Red;
                int j = polje.Kolona;


                
                if(proveriPolje(i - 1, j - 1))
                {
                    prazna_polja.Add(dugmici[i-1, j-1]);
                }

                if (proveriPolje(i - 1, j))
                {
                    prazna_polja.Add(dugmici[i - 1, j]);
                }

                if (proveriPolje(i - 1, j + 1))
                {
                    prazna_polja.Add(dugmici[i - 1, j + 1]);
                }

                if (proveriPolje(i, j - 1))
                {
                    prazna_polja.Add(dugmici[i, j - 1]);
                }

                if (proveriPolje(i, j + 1))
                {
                    prazna_polja.Add(dugmici[i, j + 1]);
                }

                if (proveriPolje(i + 1, j - 1))
                {
                    prazna_polja.Add(dugmici[i + 1, j - 1]);
                }

                if (proveriPolje(i + 1, j))
                {
                    prazna_polja.Add(dugmici[i + 1, j]);
                }

                if (proveriPolje(i + 1, j + 1))
                {
                    prazna_polja.Add(dugmici[i + 1, j + 1]);
                }

                //brise prvi element
                prazna_polja.RemoveAt(0);
                

            }
               
        }


        //Porverava da li je na toj poziciji prazno polje
        public bool proveriPolje(int i, int j)
        {
            if ((i >= 0) && (i < velicina_matrice) && (j >= 0) && (j < velicina_matrice) && !((Polje)dugmici[i, j].Tag).Zastavica)
            {
                if (((Polje)dugmici[i,j].Tag).Vrednost.Equals(Vrednost_polja.PRAZNO_POLJE))
                {
                    
                    if (dugmici[i, j].IsEnabled)
                    {
                        dugmici[i, j].IsEnabled = false;
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }


                if (((Polje)dugmici[i, j].Tag).Vrednost.Equals(Vrednost_polja.BROJ))
                {
                    
                    int broj= ((Polje)dugmici[i, j].Tag).Broj_bombi_okolo;

                    Image slika_broj = new Image();
                    slika_broj.Source= Funkcionalnost.obojiBroj(this, broj).Source;

                    dugmici[i, j].Content = slika_broj;
                    dugmici[i, j].IsEnabled = false;
                }

                
            }

            return false;
        }
        


        //restartuje igricu
        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < velicina_matrice; i++)
            {
                for (int j = 0; j < velicina_matrice; j++)
                {
                    if(((Polje)dugmici[i,j].Tag).Zastavica)
                    {
                        dugmici[i, j].MouseRightButtonDown += desniKlik;
                    }

                    dugmici[i, j].Style = (this.FindResource("dugmici") as Style);
                    dugmici[i, j].Tag = new Polje(false, Vrednost_polja.PRAZNO_POLJE, i, j);
                    dugmici[i, j].IsEnabled = true;
                    dugmici[i, j].Content = null;
                    
                }
            }
            
            broj_zastavica = 0;
            broj_pritisnutih_dugmica = 0;
            tbBombe.Text = (broj_bombi - broj_zastavica) + "";
            btnRestart.Content = (this.FindResource("happy") as Image);

            brojac_vremena =1;
            tbVreme.Text = formirajVreme(0);
            tajmer.Stop();

            Funkcionalnost.dodavanjeBombi(dugmici, broj_bombi, velicina_matrice);
            Funkcionalnost.ispisBrojeva(dugmici, velicina_matrice);
           
        }


        //otvara sva polja 
        public void otvoriSvaPolja()
        {
            for (int i = 0; i < velicina_matrice; i++)
            {
                for (int j = 0; j < velicina_matrice; j++)
                {
                   
                    Polje polje = dugmici[i, j].Tag as Polje;

                    if (!polje.Zastavica)
                    {
                        if (polje.Vrednost == Vrednost_polja.PRAZNO_POLJE)
                        {
                            dugmici[i, j].IsEnabled = false;
                            dugmici[i, j].Style = this.FindResource("dugmici_pritisnuto") as Style;

                        }

                        else if (polje.Vrednost == Vrednost_polja.BROJ)
                        {
                            Image img = new Image();
                            img.Source = Funkcionalnost.obojiBroj(this, polje.Broj_bombi_okolo).Source;
                            dugmici[i, j].Content = img;
                            dugmici[i, j].IsEnabled = false;
                            dugmici[i, j].Style = this.FindResource("dugmici_pritisnuto") as Style;

                        }

                        else if (polje.Vrednost == Vrednost_polja.BOMBA)
                        {
                            Image img = new Image();
                            img.Source = (this.FindResource("bomb") as Image).Source;
                            dugmici[i, j].Style = this.FindResource("dugmici_pritisnuto") as Style;
                            dugmici[i, j].Content = img;
                            dugmici[i, j].IsEnabled = false;
                        }
                    }
                    else
                    {
                        
                        if (polje.Vrednost == Vrednost_polja.BOMBA)
                        {
                            dugmici[i, j].MouseRightButtonDown -= desniKlik;

                        }
                        else if (polje.Vrednost != Vrednost_polja.BOMBA)
                        {
                            Image img = new Image();
                            img.Source = (this.FindResource("Xflag") as Image).Source;
                            dugmici[i, j].Content = img;
                            dugmici[i, j].MouseRightButtonDown -= desniKlik;

                        }

                    }

                }
            }
        }


        
        private void gasenjeForme(object sender, EventArgs e)
        {
           
            Glavna g = new Glavna();
            App.Current.MainWindow = g;
            g.Show();
            this.Hide();
        }

    }

   

}
