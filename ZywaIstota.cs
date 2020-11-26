using System;
using System.Collections.Generic;
using System.Text;

namespace Zaliczenie
{
    /// <summary>
    /// Klasa abstrakcyjna dla istoty żywej.
    /// </summary>
    [Serializable]
    abstract class ZywaIstota : ZdolnosciIstot
    {
        private string nazwa;
        private float maxHp;
        private float hp;
        private float sila;
        private float odpornosc;
        private int punktyAkcji;
        private int poziom;
        protected float exp;

        public ZywaIstota(string _nazwa, float _maxHp = 0, float _sila = 0, float _odpornosc = 0, int _punktyAkcji = 0, int _poziom = 0, float _exp = 0)
        {
            this.Nazwa = _nazwa;
            this.MaxHP = _maxHp;
            this.HP = this.MaxHP;
            this.Sila = _sila;
            this.Odpornosc = _odpornosc;
            this.PunktyAkcji = _punktyAkcji;
            this.Poziom = _poziom;
        }
        /// <summary>
        /// Nazwa istoty.
        /// </summary>
        public string Nazwa
        {
            get
            {
                return this.nazwa;
            }
            set
            {
                if (value.Length != 0) this.nazwa = value;
            }
        }
        public float MaxHP
        {
            get
            {
                return this.maxHp;
            }
            set
            {
                this.maxHp = value;
                if (this.maxHp < 0) this.maxHp = 0;
            }
        }
        /// <summary>
        /// Punkty życia istoty.
        /// </summary>
        public float HP
        {
            get
            {
                return this.hp;
            }
            set
            {
                this.hp = value;
                if (this.hp < 0) this.hp = 0;
                if (this.hp > this.maxHp) this.hp = maxHp;
            }
        }
        /// <summary>
        /// Siła bazowa istoty.
        /// </summary>
        public float Sila
        {
            get
            {
                return this.sila;
            }
            set
            {
                this.sila = value;
                if (this.sila < 0) this.sila = 0;
            }
        }
        /// <summary>
        /// Odporność na obrażenia istoty.
        /// </summary>
        public float Odpornosc
        {
            get
            {
                return this.odpornosc;
            }
            set
            {
                this.odpornosc = value;
                if (this.odpornosc < 0) this.odpornosc = 0;
            }
        }
        /// <summary>
        /// Punkty akcji istoty.
        /// </summary>
        public int PunktyAkcji
        {
            get
            {
                return this.punktyAkcji;
            }
            set
            {
                this.punktyAkcji = value;
                if (this.punktyAkcji < 0) this.punktyAkcji = 0;
            }
        }
        /// <summary>
        /// Poziom istoty.
        /// </summary>
        public int Poziom
        {
            get
            {
                return this.poziom;
            }
            set
            {
                this.poziom = value;
                if (this.poziom < 0) this.poziom = 0;
            }
        }
        public virtual float Doswiadczenie
        {
            get
            {
                return this.exp;
            }
            set
            {
                this.exp = value;
                if (this.exp < 0) this.exp = 0;
            }
        }
        public virtual float Atak(ZywaIstota istotaAtakowana)
        {
            return istotaAtakowana.Obrona(this.Sila);
        }
        public virtual float Obrona(float obrazenia)
        {
            if (this.Odpornosc >= obrazenia) return 0;
            obrazenia -= this.Odpornosc;
            this.HP -= obrazenia; ;
            return obrazenia;
        }
        public bool CzyZyje()
        {
            if (this.HP <= 0) return false;
            return true;
        }
        public void Ulecz()
        {
            this.HP = this.MaxHP;
        }
        public void Ulecz(float ileUleczyc)
        {
            if (ileUleczyc > 0) this.hp += ileUleczyc;
        }
    }
}
