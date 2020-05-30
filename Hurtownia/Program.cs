using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hurtownia
{
    class Program
    {
        static void Main(string[] args)
        {

            ListaTowarow lt=new ListaTowarow();
            lt.wypisz();


            System.Console.ReadKey();

            //Klient wysyla do brokera liste zakupów broker odsyla hurtownie z najtańszą sumą
        }
    }
}
