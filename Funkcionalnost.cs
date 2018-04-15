using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TVP_Projekat_2_Minesweeper
{
    class Funkcionalnost
    {
        static Random random = new Random();


        //metoda koja dodaje bombe u matricu na slucajnim pozicijama
        public static void dodavanjeBombi(Button[,] dugmici, int brojBombi, int velicina_matrice)
        {
            int brojac = 0;

            while (brojac < brojBombi)
            {
                int i = random.Next(0, velicina_matrice);
                int j = random.Next(0, velicina_matrice);

                Polje polje = (Polje)dugmici[i, j].Tag;

                if (!polje.Vrednost.Equals(Vrednost_polja.BOMBA))
                {
                    dugmici[i, j].Tag = new Polje(false, Vrednost_polja.BOMBA, i, j);
                    brojac++;
                }

            }
        }


        //metoda koja racuna koliko bombi ima oko polja u matrici
        public static void ispisBrojeva(Button[,] dugmici, int velicina_matrice)
        {
            for (int i = 0; i < velicina_matrice; i++)
            {
                for (int j = 0; j < velicina_matrice; j++)
                {
                    int brojac = 0;

                    Polje polje = (Polje)dugmici[i, j].Tag;

                    if (!polje.Vrednost.Equals(Vrednost_polja.BOMBA))
                    {
                        if ((i - 1 >= 0) && (i - 1 < velicina_matrice) && (j - 1 >= 0) && (j - 1 < velicina_matrice))
                        {
                            if (((Polje)dugmici[i - 1, j - 1].Tag).Vrednost.Equals(Vrednost_polja.BOMBA))
                                brojac++;
                        }

                        if ((i - 1 >= 0) && (i - 1 < velicina_matrice) && (j >= 0) && (j < velicina_matrice))
                        {
                            if (((Polje)dugmici[i - 1, j].Tag).Vrednost.Equals(Vrednost_polja.BOMBA))
                                brojac++;
                        }

                        if ((i - 1 >= 0) && (i - 1 < velicina_matrice) && (j + 1 >= 0) && (j + 1 < velicina_matrice))
                        {
                            if (((Polje)dugmici[i - 1, j + 1].Tag).Vrednost.Equals(Vrednost_polja.BOMBA))
                                brojac++;
                        }

                        if ((i >= 0) && (i < velicina_matrice) && (j - 1 >= 0) && (j - 1 < velicina_matrice))
                        {
                            if (((Polje)dugmici[i, j - 1].Tag).Vrednost.Equals(Vrednost_polja.BOMBA))
                                brojac++;
                        }

                        if ((i >= 0) && (i < velicina_matrice) && (j + 1 >= 0) && (j + 1 < velicina_matrice))
                        {
                            if (((Polje)dugmici[i, j + 1].Tag).Vrednost.Equals(Vrednost_polja.BOMBA))
                                brojac++;
                        }

                        if ((i + 1 >= 0) && (i + 1 < velicina_matrice) && (j - 1 >= 0) && (j - 1 < velicina_matrice))
                        {
                            if (((Polje)dugmici[i + 1, j - 1].Tag).Vrednost.Equals(Vrednost_polja.BOMBA))
                                brojac++;
                        }

                        if ((i + 1 >= 0) && (i + 1 < velicina_matrice) && (j >= 0) && (j < velicina_matrice))
                        {
                            if (((Polje)dugmici[i + 1, j].Tag).Vrednost.Equals(Vrednost_polja.BOMBA))
                                brojac++;
                        }

                        if ((i + 1 >= 0) && (i + 1 < velicina_matrice) && (j + 1 >= 0) && (j + 1 < velicina_matrice))
                        {
                            if (((Polje)dugmici[i + 1, j + 1].Tag).Vrednost.Equals(Vrednost_polja.BOMBA))
                                brojac++;
                        }

                        if (brojac > 0)
                        {

                            polje.Broj_bombi_okolo = brojac;
                            polje.Vrednost = Vrednost_polja.BROJ;
                            dugmici[i, j].Tag = polje;

                        }
                        else
                        {
                            dugmici[i, j].Content = "";
                        }

                    }



                }
            }
        }


        //metoda koja formira formu i dinamicki dodaje dugmice u grid
        public static void dodajDugmice(Button[,] dugmici, int velicina_matrice, Grid grid)
        {
            for (int i = 0; i < velicina_matrice; i++)
            {
                for (int j = 0; j < velicina_matrice; j++)
                {
                 
                    Grid.SetRow(dugmici[i, j], i);
                    Grid.SetColumn(dugmici[i, j], j);
                    grid.Children.Add(dugmici[i, j]);

                }
            }

            
        }


        //vraca sliku sa brojem na osnovu broja bombi okolo polja
        public static Image obojiBroj(FrameworkElement glavna, int broj)
        {
            
            switch (broj)
            {
                case 1:
                    {
                        return glavna.FindResource("1") as Image;
                        break;
                    }

                case 2:
                    {
                        return glavna.FindResource("2") as Image;
                        break;
                    }

                case 3:
                    {
                        return glavna.FindResource("3") as Image;
                        break;
                    }


                case 4:
                    {
                        return glavna.FindResource("4") as Image;
                        break;
                    }

                case 5:
                    {
                        return glavna.FindResource("5") as Image;
                        break;
                    }

                case 6:
                    {
                        return glavna.FindResource("6") as Image;
                        break;
                    }

                case 7:
                    {
                        return glavna.FindResource("7") as Image;
                        break;
                    }

                case 8:
                    {
                        return glavna.FindResource("8") as Image;
                        break;
                    }

                default:
                    {
                        return new Image();
                        break;
                    }

            }
        }


    }



}
