using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hurtownia
{
    class Program
    {
        public static Random rand = new Random();
        static void Main(string[] args)
        {
            ListaTowarow lt = new ListaTowarow();
            List<Thread> thr = new List<Thread>();
            List<Klient> kl = new List<Klient>();
            List<Hurtownia> hr = new List<Hurtownia>();

           // lt.wypisz();

            Broker b = new Broker();
            Thread br = new Thread(b.kolejka);
            br.Start();

            for(int i = 0; i < 5; i++)
            {
                hr.Add(new Hurtownia(i));
                thr.Add(new Thread(hr.Last().kolejka));
                hr[i].wypisz();
            }

            //for (int i = 0; i < 5; i++)
            //{
            //    hr[i].wypisz();
            //}
            for (int i = 0; i < 2; i++)
            {
                kl.Add(new Klient(i));
                thr.Add(new Thread(kl.Last().kolejka));
            }

            for (int i = 0; i < 5; i++)
            {
                Broker.wiadomosci.Enqueue(new Wiadomosc(EnWiadomosc.ZarejestrujHurtownia,hr[i]));
            }


            for (int i = 0; i < 2; i++)
            {
                Broker.wiadomosci.Enqueue(new Wiadomosc(EnWiadomosc.ZarejestrujKlient,kl[i]));
            }





            for (int i = 0; i < thr.Count(); i++)
            {
                thr[i].Start();
            }


        }
    }
}
