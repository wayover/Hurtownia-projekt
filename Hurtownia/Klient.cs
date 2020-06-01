using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hurtownia
{
    class Klient
    {
        
        public Random rand = new Random();
        public int id;
        public bool start = true;
        public Queue<Wiadomosc> wiadomosci = new Queue<Wiadomosc>();
        public List<Towar> towary;

        public Klient(int Id)
        {
            id = Id;
            
            for (int i = 0; i < ListaTowarow.t.Count(); i++)
            {
                int a = rand.Next(0, 100);
                if (a < 20)
                {

                    towary.Add(ListaTowarow.t[i]);
                }

            }
           
        }



        public void znajdz(string nazwa)
        {
            Console.Write("Klinet" + id + " chce towary: ");
            for(int i = 0; i < towary.Count(); i++)
            {
                Console.Write(towary[i].nazwa + ", ");
            }
            Console.Write("\n");
            Broker.wiadomosci.Enqueue(new Wiadomosc(id, 0, EnWiadomosc.Znajdz, towary,0));
        }




        public void kolejka()
        {
            while (start)
            {
                if (wiadomosci.Count > 0)
                {
                    Wiadomosc w = wiadomosci.Dequeue();
                    if (w.enu == EnWiadomosc.Sprzedane)
                    {
                        Console.Write("Klinet" + id + " kupił towary: ");
                        for (int i = 0; i < towary.Count(); i++)
                        {
                            Console.Write(towary[i].nazwa + ", ");
                        }
                        Console.Write(" od hurtowni " + w.idHurtowni+" za "+w.suma);
                    }
                }
                
                else
                {
                    Thread.Sleep(10);
                }

            }
        }

    }
}
