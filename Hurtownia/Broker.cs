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

        public void znajdz(List<Towar>tow,int idklient)
        {
            foreach (Hurtownia h in Hurtownie)
            {
                h.wiadomosci.Enqueue(new Wiadomosc(idklient, h.id, EnWiadomosc.Znajdz, tow,0));
            }

            Thread.Sleep(200);
            int ile = wiadomosci.Count;
            int idhurt = -1;
            float suma = -1;
            for (int i = 0; i < ile; i++) {
                Wiadomosc w = wiadomosci.Dequeue();
                if (w.wiadomosc == EnWiadomosc.Znalazlem && w.IdKlienta == idklient)
                {
                    if (suma == -1)
                    {
                        suma = w.suma;
                        idhurt = w.idHurtowni;
                    } else if (suma > w.suma)
                    {
                        suma = w.suma;
                        idhurt = w.idHurtowni;
                    }
                }
                else if (w.wiadomosc == EnWiadomosc.NieZnalazlem && w.IdKlienta == idklient) { 
                } else
                {
                    wiadomosci.Enqueue(w);
                }


                foreach(Hurtownia h in Hurtownie)
                {
                    if (h.id == idhurt)
                    {
                        h.wiadomosci.Enqueue(new Wiadomosc(idklient, h.id, EnWiadomosc.Sprzedaj, tow, suma));
                    }
                }

            }

        }

        public void ZarejestrujHurtownie(int id)
        {
            Hurtownie.Add(new Hurtownia(id));
        }

        public void ZarejestrujKlient(int id)
        {
            Klienci.Add(new Klient(id));
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
                                    k.wiadomosci.Enqueue(new Wiadomosc(w.IdKlienta, w.idHurtowni, EnWiadomosc.Sprzedane, w.t, w.suma));
                            }
                            break;

                        case EnWiadomosc.Znajdz:
                            Console.WriteLine("Klient {0} chce kupic {1} prodowkow", w.IdKlienta, w.t.Count());
                            znajdz(w.t, w.IdKlienta);
                            break;

                        case EnWiadomosc.ZarejestrujHurtownia:
                            ZarejestrujHurtownie(w.idHurtowni);
                            break;

                        case EnWiadomosc.ZarejestrujKlient:
                            ZarejestrujKlient(w.IdKlienta);
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
