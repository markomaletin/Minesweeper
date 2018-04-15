using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVP_Projekat_2_Minesweeper
{
    enum Vrednost_polja { BROJ, BOMBA, PRAZNO_POLJE}

    class Polje
    {
        private bool zastavica;
        private Vrednost_polja vrednost;
        private int broj_bombi_okolo;
        private int red;
        private int kolona;

        

        public Polje(bool zastavica, Vrednost_polja vrednost, int red, int kolona)
        {
            this.zastavica = zastavica;
            this.vrednost = vrednost;
            this.Broj_bombi_okolo = 0;
            this.red = red;
            this.kolona = kolona;
        }

        public bool Zastavica
        {
            get
            {
                return zastavica;
            }

            set
            {
                zastavica = value;
            }
        }

        internal Vrednost_polja Vrednost
        {
            get
            {
                return vrednost;
            }

            set
            {
                vrednost = value;
            }
        }

        public int Broj_bombi_okolo
        {
            get
            {
                return broj_bombi_okolo;
            }

            set
            {
                broj_bombi_okolo = value;
            }
        }

        public int Red
        {
            get
            {
                return red;
            }
        }

        public int Kolona
        {
            get
            {
                return kolona;
            }
        }

        public override string ToString()
        {
            return vrednost + " " + zastavica + " " + broj_bombi_okolo;
        }
    }
}
