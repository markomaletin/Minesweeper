using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace TVP_Projekat_2_Minesweeper
{
    class TopLista
    {
        public static List<Igrac> sviIgraciZaKategoriju(int kategorija)
        {
            List<Igrac> igraci = new List<Igrac>();
            BinaryFormatter bf = new BinaryFormatter();
           
            string putanja = "";

            switch (kategorija)
            {
                case 10:
                    {
                        putanja = "Easy.bin";
                        break;
                    }
                case 12:
                    {
                        putanja = "Medium.bin";
                        break;
                    }
                case 14:
                    {
                        putanja = "Hard.bin";
                        break;
                    }
            }

            FileStream fs = File.Open(putanja, FileMode.Open, FileAccess.Read);

            igraci.AddRange(bf.Deserialize(fs) as List<Igrac>);
            igraci = sortirajListuIgraca(igraci);

            fs.Close();
            return igraci;
        }

        public static bool dodajIgracaZaKategoriju(int kategorija, Igrac igrac)
        {
            List<Igrac> igraci = new List<Igrac>();
            BinaryFormatter bf = new BinaryFormatter();
            
            string putanja = "";

            switch (kategorija)
            {
                case 10:
                    {
                        putanja = "Easy.bin";
                        break;
                    }
                case 12:
                    {
                        putanja = "Medium.bin";
                        break;
                    }
                case 14:
                    {
                        putanja = "Hard.bin";
                        break;
                    }
            }
            

            if (File.Exists(putanja))
            {
                List<Igrac> svi_igraci = sviIgraciZaKategoriju(kategorija);
                
                if (svi_igraci.Count<10)
                {
                    svi_igraci.Add(igrac);
                    FileStream fs = File.Open(putanja,FileMode.Open,FileAccess.Write);
                    svi_igraci = sortirajListuIgraca(svi_igraci);
                    bf.Serialize(fs, svi_igraci);
                    fs.Close();
                    return true;
                }

                if (svi_igraci[svi_igraci.Count - 1].Broj_bodova < igrac.Broj_bodova)
                {
                    svi_igraci.RemoveAt(svi_igraci.Count - 1);
                    svi_igraci.Add(igrac);
                    FileStream fs = File.Open(putanja, FileMode.Open, FileAccess.Write);
                    svi_igraci = sortirajListuIgraca(svi_igraci);
                    bf.Serialize(fs, svi_igraci);
                    fs.Close();
                    return true;
                }
                else return false;

            }
            else
            {
                FileStream fs = File.Create(putanja);
                bf.Serialize(fs, igrac);
                fs.Close();
                return true;
            }



            return false;
        }


        public static List<Igrac> sortirajListuIgraca(List<Igrac> igraci)
        {
            for(int i=0; i< igraci.Count-1; i++)
            {
                for (int j = i+1; j < igraci.Count; j++)
                {
                    if(igraci[i].Broj_bodova < igraci[j].Broj_bodova)
                    {
                        Igrac pom = igraci[i];
                        igraci[i] = igraci[j];
                        igraci[j] = pom;
                    }
                }
            }

            return igraci;
        }


    }
}
