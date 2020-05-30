using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hurtownia
{
    class Towar
    {

        public string nazwa;
        public float cena;

        public Towar(string naz,float ce)
        {
            nazwa = naz;
            cena = ce;
        }

        public Towar(string naz)
        {
            nazwa = naz;
            cena = 0;
        }


    }
}
