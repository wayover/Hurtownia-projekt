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
        
        public int id;
        public bool start = true;
        public Queue<Wiadomosc> wiadomosci = new Queue<Wiadomosc>();
        public List<Towar> towary=new List<Towar>();

        public Klient(int Id)
        {
            id = Id;
            lock (this)
            {
                for (int i = 0; i < ListaTowarow.towary.Count(); i++)
                {
                    int a = Program.rand.Next(0, 100);
                    if (a < 20)
                    {

                        towary.Add(ListaTowarow.towary[i]);
                    }

                }
            }
            wiadomosci.Enqueue(new Wiadomosc(id, 0, EnWiadomosc.Zamow, towary, 0));
        }



        public void znajdz()
        {
            lock (this)
            {
                Console.Write("Klinet" + id + " chce towary: ");
                for (int i = 0; i < towary.Count(); i++)
                {
                    Console.Write(towary[i].nazwa + ", ");
                }
                Console.Write("\n");
                Broker.wiadomosci.Enqueue(new Wiadomosc(id, 0, EnWiadomosc.Znajdz, towary, 0));
            }
        }




        public void kolejka()
        {
            while (start)
            {
                if (wiadomosci.Count > 0)
                {
                    Wiadomosc w = wiadomosci.Dequeue();
                    if (w.wiadomosc == EnWiadomosc.Sprzedane)
                    {
                        Console.Write("Klinet" + id + " kupił towary: ");
                        for (int i = 0; i < towary.Count(); i++)
                        {
                            Console.Write(towary[i].nazwa + ", ");
                        }
                        Console.Write(" od hurtowni " + w.idHurtowni+" za "+w.suma+"\n");
                    }
                    else if(w.wiadomosc == EnWiadomosc.Zamow)
                    {
                        znajdz();
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
