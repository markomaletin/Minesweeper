using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVP_Projekat_2_Minesweeper
{
    [Serializable()]
    public class Igrac
    {
        string ime_igraca;
        int broj_bodova;
        
        public Igrac(string ime_igraca)
        {
            this.ime_igraca = ime_igraca;
            this.broj_bodova = 0;
        }

        public string Ime_igraca
        {
            get
            {
                return ime_igraca;
            }

            set
            {
                ime_igraca = value;
            }
        }

        public int Broj_bodova
        {
            get
            {
                return broj_bodova;
            }

            set
            {
                broj_bodova = value;
            }
        }

        public override string ToString()
        {
            return "Ime igraca: " + ime_igraca + "          Broj bodova: " + broj_bodova; 
        }

    }
}
