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
        public float suma;
        public List<Towar> t;
        public EnWiadomosc wiadomosc;

        public Wiadomosc()
        {
            IdKlienta = 0;
            idHurtowni = 0;
            t = null;
        }
        public Wiadomosc(int KlientId, int HurtowaniaId, EnWiadomosc en, List<Towar> tow,float sum)
        {
            IdKlienta = KlientId;
            idHurtowni = HurtowaniaId;
            wiadomosc = en;
            t = tow;
            suma = sum;
        }
        public Wiadomosc(int KlientId, int HurtowniaId, List<Towar> tow)
        {
            IdKlienta = KlientId;
            idHurtowni = HurtowniaId;
            t = tow;
            suma = 0;
        }

    }
}
