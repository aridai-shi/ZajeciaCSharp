using System;
using System.IO;

namespace Lekcja1
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = Directory.GetCurrentDirectory() + "\\slowa.aridai";
            while(true){
                Console.WriteLine("Czy chcesz wczytać czy zapisać nowe słowa do slowa.aridai? [W/Z/ESC]");
                ConsoleKeyInfo key = Console.ReadKey(true);
                while (key.Key != ConsoleKey.Z && key.Key != ConsoleKey.W && key.Key != ConsoleKey.Escape)
                {
                    key = Console.ReadKey(true);
                }
                if (key.Key == ConsoleKey.Z)
                {
                    Console.WriteLine("Czy chcesz dopisać słowa, czy nadpisać istniejący plik? [D/N]");
                    ConsoleKeyInfo key2 = Console.ReadKey(true);
                    while (key2.Key != ConsoleKey.D && key2.Key != ConsoleKey.N)
                    {
                        key2 = Console.ReadKey(true);
                    }
                    Console.WriteLine("Proszę podać słowa rozdzielone średnikiem");
                    string slowa = Console.ReadLine();
                    string[] slowaTablica = slowa.Split(";");
                    StreamWriter sw = new StreamWriter(file);
                    if (key2.Key == ConsoleKey.D)
                    {
                        sw.Close();
                        sw = new StreamWriter(file,true);
                    }
                    foreach (string slowo in slowaTablica)
                    {
                        Console.WriteLine(slowo);
                        sw.WriteLine(slowo);
                    }
                    sw.Close();
                }
                else if (key.Key == ConsoleKey.W)
                {
                    if (!File.Exists(file))
                    {
                        var myFile = File.Create(file);
                        myFile.Close();
                    }
                    StreamReader sr = new StreamReader(file);
                    string text = sr.ReadToEnd();
                    if (text != null)
                    {
                        Console.WriteLine(text);
                    }
                    sr.Dispose();
                    sr.Close();
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }
            }
        }
    }
}
