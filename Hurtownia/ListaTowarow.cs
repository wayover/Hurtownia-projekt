using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hurtownia
{
    class ListaTowarow
    {
        public static List<Towar> towary = new List<Towar>();

        public ListaTowarow()
        {
            string[] lines = System.IO.File.ReadAllLines(@"towar.txt");
            foreach (string line in lines)
            {
                string[] tow = line.Split(new string[] { " " }, StringSplitOptions.None);
                if (tow.Length == 2)
                {
                    Towar tt = new Towar();
                    tt.producent = tow[0];
                    tt.nazwa = tow[1];
                    towary.Add(tt);
                }
            }
        }


        public void wypisz()
        {
            for (int i = 0; i < towary.Count(); i++)
            {
                Console.WriteLine("producent: " + towary[i].producent + " nazwa: " + towary[i].nazwa);
            }

        }

    }
}
