using System;
using System.Collections.Generic;
using System.Text;

namespace Zaliczenie
{
    /// <summary>
    /// Klasa potwora.
    /// </summary>
    class Potwor : ZywaIstota
    {
        public Potwor(string _nazwa, float _MaxHp = 0, float _sila = 0, float _odpornosc = 0, int _punktyAkcji = 0, int _poziom = 0, int _exp = 0)
            : base(_nazwa, _MaxHp, _sila, _odpornosc, _punktyAkcji, _poziom, _exp)
        {

        }
    }
}