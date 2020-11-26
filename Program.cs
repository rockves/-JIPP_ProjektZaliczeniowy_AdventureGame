using System;
namespace Zaliczenie
{
    class Program
    {
        private static void Gra()
        {
            Gra gra = new Gra("Adventure Game", "saveGame.txt");
            System.Threading.Thread.Sleep(200);
            bool czyUruchomionoPoprawnie = false;
            while (!czyUruchomionoPoprawnie)
            {
                Console.Clear();
                Console.WriteLine(AsciiDraw.logoGry);
                byte wybor = (byte)DisplayManager.WyswietlMenuWyboru(new string[] { "Nowa gra", "Wczytaj grę", "Zakończ" }, true);
                switch (wybor)
                {
                    case 0:
                        gra.NowaGra();
                        czyUruchomionoPoprawnie = true;
                        break;
                    case 1:
                        if (gra.WczytajGre()) czyUruchomionoPoprawnie = true;
                        else
                        {
                            DisplayManager.WypiszLiterujac("Nie znaleziono zapisu gry.\nUtwórz nową grę\n", 5);
                            DisplayManager.WyswietlMenuWyboru(new string[] {"OK"}, true);
                        }
                        break;
                    case 2:
                        czyUruchomionoPoprawnie = true;
                        break;
                    default:
                        break;
                }
            }
        }
        static void Main(string[] args)
        {
            //Funkcja jest używana tylko po to aby mógł zadziałać Garbage Collector i wywołać destruktor klasy Gra
            Gra();
            GC.Collect();
        }
    }
}
