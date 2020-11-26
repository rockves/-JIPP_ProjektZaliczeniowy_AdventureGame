using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Zaliczenie
{
    /// <summary>
    /// Klasa służąca do obsługi złożonych struktur wejścia/wyjścia.
    /// </summary>
    abstract class DisplayManager
    {
        //Kolory dla tekstów w menu wyboru
        private static ConsoleColor defaultForeground = ConsoleColor.White;
        private static ConsoleColor defaultBackground = ConsoleColor.Black;
        private static ConsoleColor selectedForeground = ConsoleColor.Red;
        private static ConsoleColor selectedBackground = ConsoleColor.Black;

        /// <summary>
        /// Spis kodów specjalnych takich jak kod wyjścia klawiszem Escape
        /// </summary>
        public enum KodySpecjalne : int
        {
            Escape = -1,
        }

        private static int NajwiekszaDlugosc(string[] napisy)
        {
            int najwiekszaDlugosc = 0;
            foreach (var napis in napisy)
            {
                if (napis.Length > najwiekszaDlugosc) najwiekszaDlugosc = napis.Length;
            }
            return najwiekszaDlugosc;
        }
        /// <summary>
        /// Funkcja wyświetlająca menu z opcjami po którym porusza się za pomocą strzałek i wychodzi za pomocą Escape albo po dokonaniu wyboru za pomocą Enter
        /// </summary>
        /// <param name="opcje">Lista opcji w formie tablicy, każda pozycja z tablicy zostanie wyświetlona w oddzielnej linii bez żadnej modyfikacji tekstu</param>
        /// <param name="disableEscape">Czy można wyjść z menu za pomocą klawisza Escape</param>
        /// <param name="czyWypisaćLiterujac">Czy menu ma zostać wyświetlone literując opcje</param>
        /// <param name="czasPomiedzyLiterami">Czas pomiędzy pojawieniem się kolejnych liter</param>
        /// <returns>Zwraca numer wybranej opcji w postaci indexu w tablicy (dla pierwszego elementu 0)</returns>
        public static int WyswietlMenuWyboru(string[] opcje, bool disableEscape = false, bool czyWypisaćLiterujac = false, int czasPomiedzyLiterami = 0)
        {
            int cursorX, cursorY, startCursorX, startCursorY;
            int wybranaOpcja = 0;
            ConsoleKeyInfo przycisk;
            cursorX = startCursorX = Console.CursorLeft;
            cursorY = startCursorY = Console.CursorTop;
            Console.CursorVisible = false;

            //Wypisanie opcji i domyślne wybranie pierwszej
            foreach (var opcja in opcje)
            {
                if (opcja == opcje[0])
                {
                    Console.BackgroundColor = selectedBackground;
                    Console.ForegroundColor = selectedForeground;
                    if (czyWypisaćLiterujac) WypiszLiterujac(opcja + "\n", czasPomiedzyLiterami);
                    else Console.Write(opcja + "\n");
                    Console.BackgroundColor = defaultBackground;
                    Console.ForegroundColor = defaultForeground;
                }
                else
                {
                    if (czyWypisaćLiterujac) WypiszLiterujac(opcja + "\n", czasPomiedzyLiterami);
                    else Console.Write(opcja + "\n");
                }
            }

            //Obsługa przechodzenia pomiędzy opcjami
            while (true)
            {
                przycisk = Console.ReadKey();

                //Obsługa Enter i Escape
                if (przycisk.Key == ConsoleKey.Escape && !disableEscape)
                {
                    wybranaOpcja = (int)KodySpecjalne.Escape;
                    break;
                }
                else if (przycisk.Key == ConsoleKey.Enter) break;

                //Wypisanie z domyślnym kolorem dotychczasowej opcji
                Console.SetCursorPosition(cursorX, cursorY + wybranaOpcja);
                Console.BackgroundColor = defaultBackground;
                Console.ForegroundColor = defaultForeground;
                Console.Write(opcje[wybranaOpcja]);

                //Zmienienie opcji
                if (przycisk.Key == ConsoleKey.UpArrow && wybranaOpcja > 0) wybranaOpcja--;
                else if (przycisk.Key == ConsoleKey.DownArrow && wybranaOpcja < opcje.Length - 1) wybranaOpcja++;

                //Wypisanie nowej wybranej opcji z kolorem dla niej
                Console.SetCursorPosition(cursorX, cursorY + wybranaOpcja);
                Console.BackgroundColor = selectedBackground;
                Console.ForegroundColor = selectedForeground;
                Console.Write(opcje[wybranaOpcja]);
            }

            //Czyszczenie miejsca po menu
            Console.SetCursorPosition(startCursorX, startCursorY);
            WyczyscLinieKonsoli(opcje.Length, NajwiekszaDlugosc(opcje));
            //Ustawianie domyślnych kolorów i pozycji kursora przed wyjściem i pokazanie kursora
            Console.BackgroundColor = defaultBackground;
            Console.ForegroundColor = defaultForeground;
            Console.SetCursorPosition(startCursorX, startCursorY);
            Console.CursorVisible = true;
            return wybranaOpcja;
        }

        /// <summary>
        /// Wypisuje podany tekst litera po literze
        /// </summary>
        /// <param name="napis">Napis do wypisania</param>
        /// <param name="czasPomiedzyLiterami">Czas czekania przed wypisaniem kolejnej litery</param>
        public static void WypiszLiterujac(string napis, int czasPomiedzyLiterami)
        {
            foreach (var litera in napis)
            {
                Console.Write(litera);
                if (!litera.Equals('\n'))
                    Thread.Sleep(czasPomiedzyLiterami);
            }
        }
        public static void WyczyscLinieKonsoli()
        {
            int cursorX = Console.CursorLeft;
            int cursorY = Console.CursorTop;
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(cursorX , cursorY);
        }
        public static void WyczyscLinieKonsoli(int iloscLiniWDol, int dlugoscCzyszczonejLinii)
        {
            if (iloscLiniWDol < 1) 
            { 
                WyczyscLinieKonsoli();
                return;
            }
            int cursorX = Console.CursorLeft;
            int cursorY = Console.CursorTop;
            Console.Write(new string(' ', dlugoscCzyszczonejLinii));
            for (int i = 0; i < iloscLiniWDol; i++)
            {
                Console.SetCursorPosition(cursorX, Console.CursorTop + 1);
                Console.Write(new string(' ', dlugoscCzyszczonejLinii));
            }
            Console.SetCursorPosition(cursorX, cursorY);
        }
    }
}