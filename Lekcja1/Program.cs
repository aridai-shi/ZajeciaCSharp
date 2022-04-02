using System;
using System.IO;

namespace Lekcja1
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = Directory.GetCurrentDirectory() + "\\slowa.txt"; // zapisujemy słowa w pliku słowa.txt, który znajduje się w folderze z którego odpalamy program
            while(true){
                Console.WriteLine("Czy chcesz wczytać czy zapisać nowe słowa? [W/Z/ESC]");
                ConsoleKeyInfo key = Console.ReadKey(true);
                while (key.Key != ConsoleKey.Z && key.Key != ConsoleKey.W && key.Key != ConsoleKey.Escape)
                {
                    key = Console.ReadKey(true); // czekaj aż wciśniemy w końcu Z, W albo ESC
                }
                if (key.Key == ConsoleKey.Z) // zapisujemy słowa
                {
                    Console.WriteLine("Czy chcesz dopisać słowa, czy nadpisać istniejący plik? [D/N]");
                    ConsoleKeyInfo key2 = Console.ReadKey(true);
                    while (key2.Key != ConsoleKey.D && key2.Key != ConsoleKey.N)
                    {
                        key2 = Console.ReadKey(true);
                    }
                    Console.WriteLine("Proszę podać słowa rozdzielone średnikiem");
                    string slowa = Console.ReadLine();
                    string[] slowaTablica = slowa.Split(";"); // jak w js'ie
                    StreamWriter sw = new StreamWriter(file); // tego używamy by zapisywać rzeczy w pliku - StreamWriter przyjmuje parametr ścieżka, ale w przeciwieństwie do HTMLa nie ma ścieżek względnych.
                    if (key2.Key == ConsoleKey.D) // jeśli chcemy dodawać a nie nadpisywać
                    {
                        sw.Close(); // zmiana planu!
                        sw = new StreamWriter(file,true); // drugi argument w streamwriterze pozwala ustalić czy dopisujemy do pliku - true = dopisujemy, false = nadpisujemy to co tam było
                    }
                    foreach (string slowo in slowaTablica) // przeleć przez wszystkie słowa zapisane w slowaTablica
                    {
                        Console.WriteLine(slowo); // wypisz w konsoli
                        sw.WriteLine(slowo); // zapisz w pliku slowa.txt
                    }
                    sw.Close(); // zamknij pisanie - pozwala nam to edytować plik w Notatniku bez zamykania programu
                }
                else if (key.Key == ConsoleKey.W) // jak wczytujemy plik
                {
                    if (!File.Exists(file)) // jeśli nie ma tego pliku
                    {
                        var myFile = File.Create(file); // tworzymy go 
                        myFile.Close(); // i zwalniamy, żeby móc go używać
                    }
                    StreamReader sr = new StreamReader(file); // StreamReader pozwala czytać pliki, działa podobnie jak StreamWriter
                    string text = sr.ReadToEnd(); // odczytaj do końca - zapobiega edytowaniu pliku kiedy go czytamy, co może powodować straszne konsekwencje
                    if (text != null) // jeśli coś tam jest
                    {
                        Console.WriteLine(text); // wypluwamy do konsoli
                    }
                    sr.Dispose(); // uwalniamy znowu slowa.txt
                    sr.Close(); // zamykamy
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    return; // polecenie return w funkcji Main() zamyka nam aplikację
                }
            }
        }
    }
}
