using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1
{
    public class Person
    {
        public int id;
        public int numer;
        public string imie;
        public string nazwisko;
        public Person(string imie, string nazwisko, int numer,int id= 1)
        {
            this.id = id;
            this.imie = imie;
            this.nazwisko = nazwisko;
            this.numer = numer;
        }
        public Person(string toParse)
        {
            string[] rozbite = toParse.Split(" ");
            rozbite[0] = rozbite[0].Remove(rozbite[0].Length - 1);
            this.id = int.Parse(rozbite[0]);
            this.imie = rozbite[1];
            this.nazwisko = rozbite[2];
            this.numer = int.Parse(rozbite[3]);
        }
        public override string ToString()
        {
            return id.ToString() + ". " + imie + " " + nazwisko + " " + numer.ToString();
        }
    }
}
