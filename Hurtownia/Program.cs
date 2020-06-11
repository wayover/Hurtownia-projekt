﻿using System;
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

            lt.wypisz();

            Broker b = new Broker();
            Thread br = new Thread(b.kolejka);
            br.Start();

            for(int i = 0; i < 5; i++)
            {
                hr.Add(new Hurtownia(i));
                thr.Add(new Thread(hr.Last().kolejka));
            }

            for (int i = 0; i < 2; i++)
            {
                kl.Add(new Klient(i));
                thr.Add(new Thread(kl.Last().kolejka));
            }

            for (int i = 0; i < hr.Count(); i++)
            {
                Broker.wiadomosci.Enqueue(new Wiadomosc(0,i, EnWiadomosc.ZarejestrujHurtownia, null, 0));
            }


            for (int i = 0; i < 0; i++)
            {
                Broker.wiadomosci.Enqueue(new Wiadomosc(i, 0, EnWiadomosc.ZarejestrujKlient, null, 0));
            }



            for(int i = 0; i<thr.Count(); i++)
            {
                thr[i].Start();
            }

           
        }
    }
}
