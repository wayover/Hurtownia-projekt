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


        public Towar(string prod, string naz)
        {
            producent = prod;
            nazwa = naz;
        }

        public Towar(string naz)
        {
            producent = "nieznany";
            nazwa = naz;
        }

        public Towar()
        {
            producent = "nieznany";
            nazwa = "nieznana";
        }


    }
}
