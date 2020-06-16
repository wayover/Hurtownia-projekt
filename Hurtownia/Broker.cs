using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hurtownia
{
    class Broker
    {
        bool start = true;
        public static Queue<Wiadomosc> wiadomosci = new Queue<Wiadomosc>();
        public List<Hurtownia> Hurtownie = new List<Hurtownia>();
        public List<Klient> Klienci = new List<Klient>();

        public void znajdz(List<Towar> tow, int idklient, EnWiadomosc wiad)
        {
            foreach (Hurtownia h in Hurtownie)
            {
                if (wiad == EnWiadomosc.ZnajdzPriorytetowo)
                {
                    h.wiadomosci.Enqueue(new Wiadomosc(idklient, h.id, EnWiadomosc.ZnajdzPriorytetowo, tow, 0));
                } else if(wiad == EnWiadomosc.ZnajdzNormalnie)
                {
                    h.wiadomosci.Enqueue(new Wiadomosc(idklient, h.id, EnWiadomosc.ZnajdzNormalnie, tow, 0));
                } 
            }

            Thread.Sleep(200);
            int ile = wiadomosci.Count;
            int niezn = 0;
            int idhurt = -1;
            double suma = -1;
            for (int i = 0; i < ile; i++)
            {
                Wiadomosc w = wiadomosci.Dequeue();
                if (w.wiadomosc == EnWiadomosc.Znalazlem && w.IdKlienta == idklient)
                {
                    if (suma == -1)
                    {
                        suma = w.suma;
                        idhurt = w.idHurtowni;
                    }
                    else if (suma > w.suma)
                    {
                        suma = w.suma;
                        idhurt = w.idHurtowni;
                    }
                }
                else if (w.wiadomosc == EnWiadomosc.NieZnalazlem && w.IdKlienta == idklient)
                {
                }
                else
                {
                    wiadomosci.Enqueue(w);
                    niezn++;
                }

                if (niezn == ile)
                {
                    Console.WriteLine("Hurtownie nie znalazły towarów dla klienta " + idklient);
                }
            }

            foreach (Hurtownia h in Hurtownie)
            {
                if (h.id == idhurt)
                {
                    h.wiadomosci.Enqueue(new Wiadomosc(idklient, h.id, EnWiadomosc.Sprzedaj, tow, suma));
                }
            }



        }

        public void ZarejestrujHurtownie(Hurtownia hu)
        {
            lock (this)
            {
                Hurtownie.Add(hu);
                Console.WriteLine("Rejestruje hurtownie " + hu.id);
            }
        }

        public void ZarejestrujKlient(Klient kli)
        {
            lock (this)
            {
                Klienci.Add(kli);
                Console.WriteLine("Rejestruje klienta " + kli.id);
            }
        }





        public void kolejka()
        {
            while (start)
            {
                if (wiadomosci.Count > 0)
                {
                    Wiadomosc w = wiadomosci.Dequeue();
                    switch (w.wiadomosc)
                    {
                        case EnWiadomosc.Sprzedalem:
                            foreach (Klient k in Klienci)
                            {
                                if (k.id == w.IdKlienta)
                                {
                                    Console.WriteLine("Hurtownia: " + w.idHurtowni + " sprzedała towary dla  " + w.IdKlienta);
                                    k.wiadomosci.Enqueue(new Wiadomosc(w.IdKlienta, w.idHurtowni, EnWiadomosc.Sprzedane, w.t, w.suma));
                                }
                            }
                            break;

                        case EnWiadomosc.ZnajdzNormalnie:
                            Console.WriteLine("Klient: {0} chce naprawic w normalnym czasie {1} części ", w.IdKlienta, w.t.Count());
                            znajdz(w.t, w.IdKlienta, EnWiadomosc.ZnajdzNormalnie);
                            break;

                            case EnWiadomosc.ZnajdzPriorytetowo:
                            Console.WriteLine("Klient: {0} chce naprawic w priorytetowym czasie {1} części ", w.IdKlienta, w.t.Count());
                            znajdz(w.t, w.IdKlienta, EnWiadomosc.ZnajdzPriorytetowo);
                            break;

                        case EnWiadomosc.ZarejestrujHurtownia:
                            ZarejestrujHurtownie(w.hurt);
                            break;

                        case EnWiadomosc.ZarejestrujKlient:
                            ZarejestrujKlient(w.kli);
                            break;
                        default:
                            break;
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
