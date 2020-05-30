﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hurtownia
{
    class Wiadomosc
    {
        public int idHurtowni;
        public int IdKlienta;
        public string wiadomosc;
        public List<Towar> t;


        public Wiadomosc()
        {
            IdKlienta = 0;
            idHurtowni = 0;
            wiadomosc = "";
            t = null;
        }
        public Wiadomosc(int KlientId, int HurtowaniaId, string wiad, List<Towar> tow)
        {
            IdKlienta = KlientId;
            idHurtowni = HurtowaniaId;
            wiadomosc = wiad;
            t = tow;
        }
        public Wiadomosc(int KlientId, int HurtowniaId, List<Towar> tow)
        {
            IdKlienta = KlientId;
            idHurtowni = HurtowniaId;
            t = tow;
            wiadomosc = " ";
        }

    }
}