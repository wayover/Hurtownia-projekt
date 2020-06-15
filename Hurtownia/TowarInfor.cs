using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hurtownia
{
    class TowarInfor
    {

        public double cena;
        public int ilosc;

        public TowarInfor(float c, int il)
        {
            cena = c;
            ilosc = il;
        }

        public TowarInfor()
        {
            cena = 0.01;
            ilosc = 5;
        }
    }
}
