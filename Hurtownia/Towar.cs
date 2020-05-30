using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hurtownia
{
    class Towar
    {
        public string producent;
        public string nazwa;
        public float cena;


        public Towar(string prod, string naz,float ce)
        {
            producent = prod;
            nazwa = naz;
            cena = ce;
        }

        public Towar(string naz)
        {
            producent = "nieznany";
            nazwa = naz;
            cena = 0;
        }

        public Towar()
        {
            producent = "nieznany";
            nazwa = "nieznana";
            cena = 0;
        }


    }
}
