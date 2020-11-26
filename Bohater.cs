using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

namespace Zaliczenie
{
    /// <summary>
    /// Klasa bohatera.
    /// </summary>
    [Serializable]
    class Bohater : ZywaIstota
    {
        private float pieniadze;
        private float expDoNastepnegoPoziomu;
        //TODO: Dodać przedmioty
        /// <summary>
        /// Punkty doświadczenia bohatera.
        /// </summary>
        public override float Doswiadczenie
        {
            get
            {
                return this.exp;
            }
            set
            {
                this.exp = value;
                if (this.exp < 0) this.exp = 0;
                if(this.exp >= this.ExpDoNastepnegoPoziomu)
                {
                    this.exp -= this.ExpDoNastepnegoPoziomu;
                    this.Poziom++;
                    this.ExpDoNastepnegoPoziomu = Convert.ToSingle(ConfigurationManager.AppSettings["wartoscSkalujacaDoswiadczenieDoNastepnegoPoziomu"]);
                }
            }
        }
        /// <summary>
        /// Pieniądze bohatera.
        /// </summary>
        public float Pieniadze
        {
            get
            {
                return this.pieniadze;
            }
            set
            {
                this.pieniadze = value;
                if (this.pieniadze < 0) this.pieniadze = 0;
            }
        }
        /// <summary>
        /// Punkty doświadczenia potrzebne do następnego poziomu.
        /// </summary>
        public float ExpDoNastepnegoPoziomu
        {
            get
            {
                return this.expDoNastepnegoPoziomu;
            }
            set
            {
                this.expDoNastepnegoPoziomu = value;
                if (this.expDoNastepnegoPoziomu < 0) this.expDoNastepnegoPoziomu = 0;
            }
        }
        public Bohater(string _nazwa, float _pieniadze = 0, float _expDoNastepnegoPoziomu = 0) 
            : base(_nazwa)
        {
            this.Pieniadze = _pieniadze;
            this.ExpDoNastepnegoPoziomu = _expDoNastepnegoPoziomu;
        }
        public Bohater(string _nazwa, float _pieniadze, float _expDoNastepnegoPoziomu, float _MaxHp = 0, float _sila = 0, float _odpornosc = 0, int _punktyAkcji = 0, int _poziom = 0, float _exp = 0) 
            : base(_nazwa, _MaxHp, _sila, _odpornosc, _punktyAkcji, _poziom, _exp)
        {
            this.Pieniadze = _pieniadze;
            this.ExpDoNastepnegoPoziomu = _expDoNastepnegoPoziomu;
        }
        /*public Bohater(string _nazwa, float _hp = 0, float _sila = 0, float _odpornosc = 0, int _punktyAkcji = 0, int _poziom = 0, float _exp = 0) 
            : base(_nazwa, _hp, _sila, _odpornosc, _punktyAkcji, _poziom, _exp)
        {
            
        }*/
        public static Bohater operator +(Bohater bohater, Potwor potwor)
        {
            bohater.Doswiadczenie += potwor.Doswiadczenie;
            return bohater;
        }
    }
}
