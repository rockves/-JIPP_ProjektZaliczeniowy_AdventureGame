using System;
using System.Collections.Generic;
using System.Text;

namespace Zaliczenie
{
    class Loch
    {
        private Potwor potwor;
        private Bohater bohater;
        public Loch()
        {
            this.potwor = GenerujPotwora();
        }
        public Bohater WejdzDoLochu(Bohater bohater)
        {
            this.bohater = bohater;
            WalkaZPotworem();
            return this.bohater;
        }
        private Potwor GenerujPotwora()
        {
            Potwor generowanyPotwor = new Potwor("Minotaur", 10, 5, 1, 2, 1, 20);
            return generowanyPotwor;
        }
        private string statusBohatera()
        {
            return $"Bohater: {this.bohater.Nazwa} Poziom: {this.bohater.Poziom} Hp: {this.bohater.HP} Ap: {this.bohater.PunktyAkcji} \n";
        }
        private string statusPotwora()
        {
            return $"Nazwa: {this.potwor.Nazwa} Poziom: {this.potwor.Poziom} Hp: {this.potwor.HP} \n";
        }
        private void turaBohatera(int roundInfoCursorX, int roundInfoCursorY, int menuCursorX, int menuCursorY)
        {
            //Przywrócenie punktów akcji bohatera na początku tury
            int punktyAkcji = this.bohater.PunktyAkcji;
            int wybor;
            while(punktyAkcji > 0)
            {
                Console.SetCursorPosition(menuCursorX, menuCursorY);
                wybor = DisplayManager.WyswietlMenuWyboru(new string[] { "Atak", "Obrona" });
                switch (wybor)
                {
                    case 0:
                        float zadaneObrazenia = this.bohater.Atak(this.potwor);
                        punktyAkcji -= 1;
                        string info = $"Atakujesz potwora zadając {zadaneObrazenia} obrażeń.\n";
                        Console.SetCursorPosition(roundInfoCursorX, roundInfoCursorY);
                        DisplayManager.WyczyscLinieKonsoli();
                        DisplayManager.WypiszLiterujac(info, 5);
                        DisplayManager.WyswietlMenuWyboru(new string[] { "OK" });
                        break;
                    case 1:
                        break;
                }
            }
        }
        private void turaPotwora(int roundInfoCursorX, int roundInfoCursorY, int menuCursorX, int menuCursorY)
        {
            float zadaneObrazenia = this.potwor.Atak(this.bohater);
            string info = $"Potwór atakuje zadając {zadaneObrazenia} obrażeń.\n";
            Console.SetCursorPosition(roundInfoCursorX, roundInfoCursorY);
            DisplayManager.WyczyscLinieKonsoli();
            DisplayManager.WypiszLiterujac(info, 5);
            Console.SetCursorPosition(menuCursorX, menuCursorY);
            DisplayManager.WyswietlMenuWyboru(new string[] { "OK" });
        }
        private void ekranPoWalce(bool czyWygrana)
        {
            if (czyWygrana)
            {
                DisplayManager.WypiszLiterujac("Wygrałeś!\n", 5);
                DisplayManager.WyswietlMenuWyboru(new string[] {"Super!"});
            }
            else
            {
                DisplayManager.WypiszLiterujac("Przegrałeś...\n", 5);
                DisplayManager.WyswietlMenuWyboru(new string[] {"Następnym razem wygram!"});
            }
        }
        private bool WalkaZPotworem()
        {
            bool czyWygrana;
            //Pierwsze wypisanie statystyk
            int monsterStatsCursorX = Console.CursorLeft;
            int monsterStatsCursorY = Console.CursorTop;
            DisplayManager.WypiszLiterujac(this.statusPotwora(), 5);
            DisplayManager.WypiszLiterujac(AsciiDraw.RysunekPotwora(AsciiDraw.Potwor.Minotaur) + "\n", 1);
            int playerStatsCursorX = Console.CursorLeft;
            int playerStatsCursorY = Console.CursorTop;
            DisplayManager.WypiszLiterujac(this.statusBohatera(), 5);
            //Ustawienie pozycji kursora dla wypisywania informacji o rundzie pod statystykami bohatera
            int roundInfoCursorX = Console.CursorLeft;
            int roundInfoCursorY = Console.CursorTop;
            DisplayManager.WypiszLiterujac("Potwór wpatruje się w ciebie.", 5);
            //Ustawienie pozycji kursora dla menu
            int menuCursorX = 0;
            int menuCursorY = roundInfoCursorY + 1;

            while (true)
            {
                turaBohatera(roundInfoCursorX, roundInfoCursorY, menuCursorX, menuCursorY);
                //Odświeżenie statystyk potwora
                Console.SetCursorPosition(monsterStatsCursorX, monsterStatsCursorY);
                Console.Write(this.statusPotwora());
                //Sprawdzenie czy potwór żyje
                if (!this.potwor.CzyZyje())
                {
                    czyWygrana = true;
                    Console.Clear();
                    ekranPoWalce(czyWygrana);
                    break;
                }
                turaPotwora(roundInfoCursorX, roundInfoCursorY, menuCursorX, menuCursorY);
                //Odświeżenie statystyk bohatera
                Console.SetCursorPosition(playerStatsCursorX, playerStatsCursorY);
                Console.Write(this.statusBohatera());
                //Sprawdzenie czy bohater żyje
                if (!this.bohater.CzyZyje())
                {
                    czyWygrana = false;
                    Console.Clear();
                    ekranPoWalce(czyWygrana);
                    break;
                }
            }
            return czyWygrana;
        }
    }
}
