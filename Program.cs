using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace Lud
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> dobasok = new List<int>();
            foreach (string sor in File.ReadLines("dobasok.txt").ToList())
            {
                foreach (var item in sor.Split(' '))
                {
                    dobasok.Add(int.Parse(item));
                }
            }

            string[] osvenyek = File.ReadAllLines("osvenyek.txt");

            //2
            Console.WriteLine($"2.feladat\nA dobások száma: {dobasok.Count()}\nAz ösvények száma: {osvenyek.Length}");
            //3
            string leghosszabb = osvenyek[0];
            int leghosszabbIndex = 0;
            for (int i = 0; i < osvenyek.Length; i++)
            {
                if (osvenyek[i].Length > leghosszabb.Length)
                {
                    leghosszabb = osvenyek[i];
                    leghosszabbIndex = i;
                }
            }
            Console.WriteLine($"3.feladat\nAz egyik leghosszabb a(z): {leghosszabbIndex + 1}. ösvény, hossza {leghosszabb.Length}");

            //4
            bool successfullyParsed;
            int jatekosokSzama = 2;
            do
            {
                Console.WriteLine("add meg a játékosok számát(2-5): ");
                successfullyParsed = int.TryParse(Console.ReadLine(), out int result);
                if (successfullyParsed && result <= 5 && result > 1)
                    jatekosokSzama = result;
                else
                {
                    successfullyParsed = false;
                }
            } while (!successfullyParsed);

            int valasztottOsvenySzama = 0;

            do
            {
                Console.WriteLine("add meg a választott ösvény sorszámát");
                successfullyParsed = int.TryParse(Console.ReadLine(), out int result);
                if (successfullyParsed)
                    valasztottOsvenySzama = result - 1;
            } while (!successfullyParsed);

            string valasztottOsveny = osvenyek[valasztottOsvenySzama];

            //5
            Console.WriteLine($"5.Feladat");
            var query = valasztottOsveny.GroupBy(x => x).Select(x => new
            {
                Value = x.Key,
                Count = x.Count(),
            });

            foreach (var item in query)
            {
                Console.WriteLine($"{item.Value}: {item.Count} darab");
            }

            //6
            StreamWriter sw = new StreamWriter("kulonleges.txt");
            sw.WriteLine('_' * 100);
            for (int i = 0; i < valasztottOsveny.Count(); i++)
            {
                if (valasztottOsveny[i] == 'V' || valasztottOsveny[i] == 'E')
                    sw.WriteLine($"{i}\t{valasztottOsveny[i]}");
            }
            sw.Close();
            //7
            int[] jatekosPoziciok = new int[jatekosokSzama];


            do
            {
                for (int i = 0; i < dobasok.Count; i++)
                {
                    for (int z = 0; z < jatekosokSzama; z++)
                    {
                        jatekosPoziciok[z] += dobasok[i];
                        i++;
                    }
                }
            } while (!jatekosPoziciok.Any(x => x >= valasztottOsveny.Length - 1));

        }
    }
}