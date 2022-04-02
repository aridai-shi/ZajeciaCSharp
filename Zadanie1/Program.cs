using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Zadanie1
{
    class Program
    {
        public static string file;
        public static string menuOpts = "123456";
        static void Main(string[] args)
        {
            file = Directory.GetCurrentDirectory() + "\\ksiazka_telefoniczna.txt";
            while (true)
            { // tu będzie tylko menu, reszta to funkcje;
                Console.Clear();
                Console.WriteLine("---KSIĄŻKA TELEFONICZNA: MENU GŁÓWNE---");
                Console.WriteLine("Wciśnij numer 1-6 by wybrać właściwą opcję");
                Console.WriteLine("1. Wyświetl wszystkie kontakty");
                Console.WriteLine("2. Wyszukaj po imieniu");
                Console.WriteLine("3. Wyszukaj po liczbie porządkowej");
                Console.WriteLine("4. Dodaj nowy kontakt");
                Console.WriteLine("5. Wyczyść pamięć aplikacji");
                Console.WriteLine("6. Zakończ");
                ConsoleKeyInfo key = Console.ReadKey(true);
                while (!menuOpts.Contains(key.KeyChar))
                {
                    key = Console.ReadKey(true);
                }
                Console.Clear();
                switch (key.KeyChar)
                {
                    case '1':
                        wyswietlWszystkie();
                        break;
                    case '2':
                        wyswietlZImienia();
                        break;
                    case '3':
                        wyswietlZNumeru();
                        break;
                    case '4':
                        wprowadzanieNumeru();
                        break;
                    case '5':
                        czysczeniePamieci();
                        break;
                    case '6':
                        return;

                }
                Console.WriteLine("Skończono wykonywanie określonej funkcji, wciśnij dowolny klawisz aby wrócić do menu.");
                Console.ReadKey();
            }
        }

        static void czysczeniePamieci()
        {
            Console.WriteLine("Czy chcesz wyczyścić pamięć aplikacji? [T/N]");
            ConsoleKeyInfo conf1 = Console.ReadKey(true);
            while (conf1.Key != ConsoleKey.T && conf1.Key != ConsoleKey.N)
            {
                conf1 = Console.ReadKey(true);
            }
            if (conf1.Key == ConsoleKey.N){
                return;
            }
            Console.WriteLine("Jesteś tego pewien? [T/N]");
            ConsoleKeyInfo conf2 = Console.ReadKey(true);
            while (conf2.Key != ConsoleKey.T && conf2.Key != ConsoleKey.N)
            {
                conf2 = Console.ReadKey(true);
            }
            if (conf2.Key == ConsoleKey.N)
            {
                return;
            }
            File.Delete(file);
            var myFile = File.Create(file);
            myFile.Close();
            Console.WriteLine("Wyczyszczono pamięć!");
        }

        static void wyswietlZNumeru(int num=-999)
        {
            if (num ==-999)
            {
                Console.WriteLine("Wprowadź liczbę porządkową:");
                int input = Math.Abs(int.Parse(Console.ReadLine()));
                wyswietlZNumeru(input);
            }
            else
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
                    using (StringReader reader = new StringReader(text))
                    {
                        string line = reader.ReadLine();
                        string[] rozbite = line.Split(" ");
                        if (num.ToString()+"."== rozbite[0])
                        {
                            Console.WriteLine(line);
                        }
                    }
                }
                sr.Dispose();
                sr.Close();
            }
        }
        static void wyswietlWszystkie()
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
        static void wyswietlZImienia(string imie = "")
        {
            if (imie == "")
            {
                Console.WriteLine("Wprowadź imię do wyszukania:");
                wyswietlZImienia(Console.ReadLine());

            }
            else
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
                    using (StringReader reader = new StringReader(text))
                    {
                        string line = reader.ReadLine();
                        string[] rozbite = line.Split(" ");
                        if (rozbite[1] == imie)
                        {
                            Console.WriteLine(line);
                        }
                    }
                }
                sr.Dispose();
                sr.Close();
            }

        }
        static void wprowadzanieNumeru()
        {
            Console.WriteLine("Podaj imię");
            string imie = Console.ReadLine();
            Console.WriteLine("Podaj nazwisko");
            string nazw = Console.ReadLine();
            Console.WriteLine("Podaj numer telefonu");
            int nrTel = int.Parse(Console.ReadLine());
            if (nrTel.ToString().Length != 9)
            {
                Console.WriteLine("Numer telefonu błędny, spróbuj ponownie");
            }
            else
            {
                dodajNumer(imie, nazw, nrTel);
                Console.WriteLine("Dodano pomyślnie numer telefonu");
            }
        }
        static void dodajNumer(string imie, string nazw, int nrTel)
        {
            if (!File.Exists(file))
            {
                var myFile = File.Create(file);
                myFile.Close();
            }
            imie = Regex.Replace(imie, @"\s", "_");
            nazw = Regex.Replace(nazw, @"\s", "_");
            int ostatniaLinia = File.ReadAllLines(file).Length;
            StreamWriter sw = new StreamWriter(file, append: true);
            sw.WriteLine((ostatniaLinia + 1).ToString() + ". " + imie + " " + nazw + " " + nrTel.ToString());
            sw.Close();
        }
    }
}
