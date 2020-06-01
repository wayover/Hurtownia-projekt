using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hurtownia
{
    class Hurtownia
    {
        public Random rand = new Random();
        public Queue<Wiadomosc> wiadomosci = new Queue<Wiadomosc>();
        public int id;
        public bool start = true;
        public List<Towar> tow = new List<Towar>();


        public Hurtownia(int Id)
        {
            id = Id;

            for(int i=0;i<ListaTowarow.towary.Count(); i++)
            {
                int rnd = rand.Next(0, 100);
                if (rnd < 70)
                {
                    dodaj(ListaTowarow.towary[i]);
                }

            }
        }




        public void dodaj(Towar t)
        {
            t.cena = rand.Next(1, 25);
            tow.Add(t);
        }

        public Towar znajdzTowar(string nazwa)
        {
            foreach(Towar t in tow)
            {
                if (t.nazwa.Equals(nazwa))
                {
                    return t;
                }
            }

            return null;
        }


        public float znajdzListeTowarow(List<Towar> Ltowar)
        {
            float suma=0;
            int ile = 0;
            foreach(Towar lt in Ltowar)
            {
                foreach(Towar t in tow)
                {
                    if (t.nazwa.Equals(lt.nazwa))
                    {
                        suma += t.cena;
                        ile++;
                        break;
                    }
                }
            }

            if (ile != Ltowar.Count())
            {
                return 0;
            }
            else {
                return suma;
            }
        }


        public bool sprzedaj(string nazwa)
        {
            foreach (Towar t in tow)
            {
                if (t.nazwa.Equals(nazwa))
                {
                    tow.Remove(t);
                    //dodać ilosc
                    return true;
                }
            }

            return false;

        }

        public bool sprzedajTowary(List<Towar> Ltowar)
        {
            List<Towar> to=new List<Towar>();
            int ile = 0;
            foreach (Towar lt in Ltowar)
            {
                foreach (Towar t in tow)
                {
                    if (t.nazwa.Equals(lt.nazwa))
                    {
                        //zmienic na ilosc
                        to.Add(t);
                        ile++;
                    }
                }
            }

            if (ile == Ltowar.Count())
            {
                foreach(Towar t in to)
                {
                    tow.Remove(t);
                }
                return true;
            }else
            {
                return false;
            }

        }


        public void kolejka()
        {


            while (start)
            {
                if (wiadomosci.Count > 0)
                {
                    Wiadomosc w = wiadomosci.Dequeue();
                    if (w.wiadomosc == EnWiadomosc.Znajdz)
                    {
                        //Console.WriteLine("Hurtownia " + id + " szuka ksiązki " + w.k.tytul);
                        float ile= znajdzListeTowarow(w.t);
                        if (ile != 0)
                        {
                            Console.WriteLine("Hurtownia " + id + ": znalazla towary za  " + ile);
                            Broker.wiadomosci.Enqueue(new Wiadomosc(w.IdKlienta, id, EnWiadomosc.Znalazlem, w.t,ile));

                        }
                        else
                        {
                            Console.WriteLine("Hurtownia " + id + ": nie znalazla wszystkich towarów ");
                            Broker.wiadomosci.Enqueue(new Wiadomosc(w.IdKlienta, id, EnWiadomosc.NieZnalazlem, w.t,ile));
                        }
                    }
                    else if (w.wiadomosc == EnWiadomosc.Sprzedaj)
                    {
                        Console.WriteLine("Hurtownia " + id + ": Przyjąłęm zamówienie od klienta " + w.IdKlienta);
                        bool b = sprzedajTowary(w.t);
                        if (b == true)
                        {
                            Broker.wiadomosci.Enqueue(new Wiadomosc(w.IdKlienta, id, EnWiadomosc.Sprzedalem, w.t,w.suma));
                        }
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
