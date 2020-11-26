using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

namespace Zaliczenie
{
    /// <summary>
    /// Klasa służąca obsłudze gry.
    /// Zawiera główną pętle gry, a także obsługuje poruszanie się po grze
    /// </summary>
    class Gra
    {
        private string nazwaGry;
        private Bohater bohater;
        private SaveManager saveManager;
        public Gra(string _nazwaGry, string nazwaPlikuZapisu)
        {
            this.nazwaGry = _nazwaGry;
            this.saveManager = new SaveManager(nazwaPlikuZapisu);
        }
        public Gra(string _nazwaGry, string sciezkaZapisu, string nazwaPlikuZapisu)
        {
            this.nazwaGry = _nazwaGry;
            this.saveManager = new SaveManager(sciezkaZapisu,nazwaPlikuZapisu);
        }
        ~Gra()
        {
            Console.Clear();
            Console.WriteLine("Dziękuję za zagranie w " + nazwaGry + "!");
            Console.ReadKey();
        }
        /// <summary>
        /// Funkcja obsługująca rozpoczęcie nowej gry.
        /// </summary>
        public void NowaGra()
        {
            Console.Clear();
            DisplayManager.WypiszLiterujac("Podaj swoje imie: ", 5);
            string imieBohatera = Console.ReadLine();
            string[] statystykiStartoweBohateraString = ConfigurationManager.AppSettings["statystykiStartoweBohatera"].Replace(", ", ",").Split(',');
            float[] statystyki= new float[statystykiStartoweBohateraString.Length];
            for (int i = 0; i < statystykiStartoweBohateraString.Length; i++)
            {
                statystyki[i] = Convert.ToSingle(statystykiStartoweBohateraString[i]);
            }
            this.bohater = new Bohater(imieBohatera, statystyki[0], statystyki[1], statystyki[2], statystyki[3], statystyki[4], (int)statystyki[5], (int)statystyki[6], statystyki[7]);
            //Tu zapis
            saveManager.Zapisz(bohater);
            this.RozpocznijGre();
        }
        /// <summary>
        /// Funkcja zajmująca się wczytaniem stanu poprzedniej rozgrywki.
        /// </summary>
        public bool WczytajGre()
        {
            //Tu wczytanie
            this.bohater = saveManager.Wczytaj();
            if (this.bohater == null) return false;
            this.RozpocznijGre();
            return true;
        }
        /// <summary>
        /// Główna pętla gry.
        /// </summary>
        private void RozpocznijGre()
        {
            byte wybor = 0;
            string[] opcjeMiasta = ConfigurationManager.AppSettings["opcjeMiasta"].Replace(", ",",").Split(',');
            while (wybor != opcjeMiasta.Length-1)
            {
                Console.Clear();
                Console.WriteLine(AsciiDraw.miasto);
                DisplayManager.WypiszLiterujac(ConfigurationManager.AppSettings["miastoWiadomoscPowitalna"] + bohater.Nazwa + '\n', 5);
                DisplayManager.WypiszLiterujac("Co chciałbyś zrobić?\n\n", 5);
                wybor = (byte)DisplayManager.WyswietlMenuWyboru(opcjeMiasta, true, true, 5);
                switch (wybor)
                {
                    case 0:
                        WejdzDoLochu();
                        break;
                    case 1:
                        PokazKuznie();
                        break;
                    case 2:
                        PokazSklepUmiejetnosci();
                        break;
                    case 3:
                        PokazSklepMikstur();
                        break;
                    case 4:
                        SpojrzWLustro();
                        break;
                    case 5:
                        PokazPomoc();
                        break;
                    case 6:
                        Zapisz();
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// Funkcja obsługująca przygodę w lochu.
        /// </summary>
        private void WejdzDoLochu()
        {
            Console.Clear();
            Loch loch = new Loch();
            this.bohater = loch.WejdzDoLochu(this.bohater);
            this.bohater.Ulecz();
        }
        /// <summary>
        /// Funkcja obsługująca zakupy w kuźni.
        /// </summary>
        private void PokazKuznie()
        {
            Console.Clear();
        }
        /// <summary>
        /// Funkcja obsługująca zakupy umiejętności.
        /// </summary>
        private void PokazSklepUmiejetnosci()
        {
            Console.Clear();
        }
        /// <summary>
        /// Funkcja obsługująca sklep z miksturami.
        /// </summary>
        private void PokazSklepMikstur()
        {
            Console.Clear();
        }
        private void SpojrzWLustro()
        {
            Console.Clear();
            DisplayManager.WypiszLiterujac($"Imie: {this.bohater.Nazwa} Poziom: {this.bohater.Poziom}\n", 5);
            DisplayManager.WypiszLiterujac($"Złoto: {this.bohater.Pieniadze}\n", 5);
            DisplayManager.WypiszLiterujac($"Doświadczenie: {this.bohater.Doswiadczenie}/{this.bohater.ExpDoNastepnegoPoziomu}\n", 5);
            DisplayManager.WypiszLiterujac($"Statystyki:\n", 5);
            DisplayManager.WypiszLiterujac($"  Hp: {this.bohater.HP}\n  Siła: {this.bohater.Sila}\n  Odporność: {this.bohater.Odpornosc}\n  Punkty akcji: {this.bohater.PunktyAkcji}\n\n", 5);
            DisplayManager.WyswietlMenuWyboru(new string[] {"Wystarczy tego przeglądania się w lustrze."});
        }
        private void PokazPomoc()
        {
            Console.Clear();
            string[] pomoce = ConfigurationManager.AppSettings["pomocOpcjeMiasta"].Replace(", ",",").Split(',');
            foreach (var pomoc in pomoce)
            {
                DisplayManager.WypiszLiterujac(pomoc + ".\n", 2);
            }
            DisplayManager.WyswietlMenuWyboru(new string[] { "Rozumiem" });
        }
        private void Zapisz()
        {
            this.saveManager.Zapisz(this.bohater);
            Console.Clear();
            DisplayManager.WypiszLiterujac("Gra zapisana.\n", 5);
            DisplayManager.WyswietlMenuWyboru(new string[] { "Ok" });
            Console.Clear();
        }
    }
}
