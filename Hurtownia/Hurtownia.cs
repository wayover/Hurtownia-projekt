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
        public Queue<Wiadomosc> wiadomosci = new Queue<Wiadomosc>();
        public int id;
        public bool start = true;
        Dictionary<Towar, TowarInfor> tow = new Dictionary<Towar, TowarInfor>();
        //private List<Towar> tow = new List<Towar>();


        public Hurtownia(int Id)
        {
            lock (tow)
            {
                id = Id;

                for (int i = 0; i < ListaTowarow.towary.Count(); i++)
                {
                    int rnd = Program.rand.Next(0, 100);
                    if (rnd < 70)
                    {
                        Towar t = ListaTowarow.towary[i];
                        TowarInfor ti = new TowarInfor();
                        ti.cena= Program.rand.Next(1, 100);
                        ti.ilosc= Program.rand.Next(1, 10);
                       
                        tow.Add(t, ti);
                        //tow[tow.Count() - 1].cena = Program.rand.Next(1, 100);
                    }

                }
            }
        }

        public void wypisz()
        {
            Console.WriteLine("Hurtownia " + id);
            foreach (KeyValuePair<Towar, TowarInfor> t in tow)
            {
                Console.WriteLine(t.Key.nazwa +" "+t.Value.ilosc+ "  "+t.Value.cena);
            }
            
            Console.WriteLine("\n\n");
        }


        public void dodaj(Towar t)
        {
;
        }

        public Towar znajdzTowar(string nazwa)
        {
            foreach (KeyValuePair<Towar, TowarInfor> t in tow)
            {
                if (t.Key.nazwa.Equals(nazwa))
                {
                    return t.Key;
                }
            }

            return null;
        }


        public double znajdzListeTowarow(List<Towar> Ltowar)
        {

            double suma = 0;
            int ile = 0;

            foreach (Towar lt in Ltowar)
            {
                foreach (KeyValuePair<Towar, TowarInfor> t in tow)
                {
                    if (t.Key.nazwa.Equals(lt.nazwa))
                    {
                        //Console.WriteLine("Hurtowanie " + id + " " + lt.nazwa + " " + t.cena);
                        suma += t.Value.cena;
                        ile++;
                        break;
                    }
                }
            }

            if (ile != Ltowar.Count())
            {
                return 0;
            }
            else
            {
                return suma;
            }

        }


        public bool sprzedaj(string nazwa)
        {

            foreach (KeyValuePair<Towar, TowarInfor> t in tow)
            {
                if (t.Key.nazwa.Equals(nazwa))
                {
                    tow[t.Key].ilosc--;
                    if (tow[t.Key].ilosc == 0)
                    {
                        tow.Remove(t.Key);
                    }
                    return true;
                }
            }

            return false;

        }

        public bool sprzedajTowary(List<Towar> Ltowar)
        {
                List<Towar> to = new List<Towar>();
                int ile = 0;
                foreach (Towar lt in Ltowar)
                {
                foreach (KeyValuePair<Towar, TowarInfor> t in tow)
                {
                        if (t.Key.nazwa.Equals(lt.nazwa))
                        {
                            
                            to.Add(t.Key);
                            ile++;
                        }
                    }
                }

                if (ile == Ltowar.Count())
                {
                    foreach (Towar t in to)
                    {
                     tow[t].ilosc--;
                    if (tow[t].ilosc == 0)
                    {
                        tow.Remove(t);
                    }
                    }
                    return true;
                }
                else
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
                        double ile= znajdzListeTowarow(w.t);
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
