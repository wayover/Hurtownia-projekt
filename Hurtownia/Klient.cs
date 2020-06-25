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
                if (towary.Count() == 0)
                {
                    int p = Program.rand.Next(0, ListaTowarow.towary.Count()-1);
                    towary.Add(ListaTowarow.towary[p]);
                }
            }
            int rnd= Program.rand.Next(0, 99);
            if (rnd < 50)
            {
                wiadomosci.Enqueue(new Wiadomosc(id, 0, EnWiadomosc.ZamowNormalnie, towary, 0));
            }else
            {
                wiadomosci.Enqueue(new Wiadomosc(id, 0, EnWiadomosc.ZamowPriorytet, towary, 0));
            }
        }



        public void znajdz(EnWiadomosc wiad)
        {
            lock (this)
            {
                if (wiad == EnWiadomosc.ZamowPriorytet)
                {

                    Console.Write("Klinet: " + id + " chce naprawić priorytetowo części: ");
                }
                else
                {
                    Console.Write("Klinet: " + id + " chce naprawić normalnie części: ");
                }

                for (int i = 0; i < towary.Count(); i++)
                {
                    Console.Write(towary[i].nazwa + ", ");
                }
                Console.Write("\n");

                if (wiad == EnWiadomosc.ZamowPriorytet)
                {
                    Broker.wiadomosci.Enqueue(new Wiadomosc(id, 0, EnWiadomosc.ZnajdzPriorytetowo, towary, 0));
                }
                else
                {
                    Broker.wiadomosci.Enqueue(new Wiadomosc(id, 0, EnWiadomosc.ZnajdzNormalnie, towary, 0));
                }


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
                        lock (this)
                        {
                            Console.Write("Klinet: " + id + " naprawił części: ");
                            for (int i = 0; i < towary.Count(); i++)
                            {
                                Console.Write(towary[i].nazwa + ", ");
                            }
                            Console.Write(" od hurtowni " + w.idHurtowni + " za " + w.suma + "\n");
                        }
                    }
                    else if(w.wiadomosc == EnWiadomosc.ZamowNormalnie)
                    {
                        znajdz(EnWiadomosc.ZamowNormalnie);
                    }
                    else if(w.wiadomosc == EnWiadomosc.ZamowPriorytet)
                    {
                        znajdz(EnWiadomosc.ZamowPriorytet);
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
