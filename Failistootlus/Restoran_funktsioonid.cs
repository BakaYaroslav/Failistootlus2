using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace Failistootlus
{
    internal class Restoran_funktsioonid
    {
        static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Koostisosad.txt");
        static string retseptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Retseptid.txt");
        static string menuuPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Menuu.txt");
        static List<string> list = new List<string>();

        public static void Lemmiktoidu_salvestamine_faili()
        {
            try
            {
                retseptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Retseptid.txt");
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Koostisosad.txt");
                Console.WriteLine("Olemasolevad toidud");
                foreach (string rida in File.ReadAllLines(retseptPath))
                    Console.WriteLine(rida);
                Console.WriteLine();
                Console.Write("Sisesta uus toidu nimi: ");
                string toit = Console.ReadLine();
                StreamWriter retseptWriter = new StreamWriter(retseptPath, true);
                retseptWriter.WriteLine(toit);
                retseptWriter.Close();

                Console.WriteLine("Olemasolevad koostisosad");
                foreach (string rida in File.ReadAllLines(path))
                Console.WriteLine($"  {rida}");
                Console.WriteLine();
                StreamWriter koostisWriter = new StreamWriter(path, true);
                ConsoleKeyInfo key;
                do
                {
                    Console.Write("Sisesta uus koostisosa (Backspace = lõpeta): ");
                    string koostisosa = Console.ReadLine();
                    key = Console.ReadKey(true);

                    if (!string.IsNullOrWhiteSpace(koostisosa))
                    {
                        koostisWriter.WriteLine(koostisosa);
                        Console.WriteLine($"{koostisosa} lisatud!");
                    }

                } while (key.Key != ConsoleKey.F);

                koostisWriter.Close();
                Console.WriteLine(" Kõik salvestatud!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Viga: {e.Message}");
            }}
        public static void Kogu_menüü_kuvamine()
        {
            try
            {
                Console.WriteLine("Retseptid");
                foreach (string rida in File.ReadAllLines(retseptPath))
                    Console.WriteLine($"{rida}");
            }
            catch (Exception)
            {
                Console.WriteLine("Retseptid.txt faili ei leitud!");
            }
            try
            {
                Console.WriteLine("Koostisosad");
                foreach (string rida in File.ReadAllLines(path))
                    Console.WriteLine(rida);
            }
            catch (Exception)
            {
                Console.WriteLine("Koostisosad.txt faili ei leitud!");
            }
        }
        public static List<string> Koostisosade_muutmine_nimekirjas()
        {

            try
            {
                foreach (string rida in File.ReadAllLines(path))
                    list.Add(rida);
                if (list.Count > 0)
                    list[0] = "Kvaliteetne oliiviõli";
                ConsoleKeyInfo key;
                do {
                    Console.WriteLine("Praegune nimekiri");
                    for (int i = 0; i < list.Count; i++)
                        Console.WriteLine($"  {i + 1}. {list[i]}");
                    Console.Write("Sisesta toit mida tahad eemaldada (Backspace = lõpp): ");
                    string lause = Console.ReadLine();
                    key = Console.ReadKey(true);
                    if (!string.IsNullOrEmpty(lause))
                        if (list.Remove(lause))
                            Console.WriteLine($"{lause} eemaldatud!");
                        else
                            Console.WriteLine($"{lause} ei leitud nimekirjast.");
                } while (key.Key != ConsoleKey.Backspace);

                Uuendatud_nimekirja_salvestamine(list);
            } catch (Exception e) { Console.WriteLine($"Viga: {e}"); } return list;
        }
        public static List<string> Külmkapi_kontroll_ehk_otsing_listist()
        {
            try
            {
                foreach (string rida in File.ReadAllLines(path))
                    list.Add(rida);
            }
            catch (Exception)
            {
                Console.WriteLine("Viga faili lugemisel!");
                return list;
            }
            Console.WriteLine("Saadaval koostisosad");
            foreach (string rida in list)
                Console.WriteLine($"  {rida}");
            Console.WriteLine("Sisesta mida soovid otsida: ");
            string otsitav = Console.ReadLine();
            if (list.Contains(otsitav))
                Console.WriteLine($"{otsitav} on nimekirjas olemas!");
            else
                Console.WriteLine($"{otsitav} ei ole retseptis.");
            return list;
        }
        public static void Uuendatud_nimekirja_salvestamine(List<string> list)
        {
            try
            {
                Console.WriteLine("Praegune nimekiri");
                for (int i = 0; i < list.Count; i++)
                    Console.WriteLine($"  {i + 1}. {list[i]}"); 
                File.WriteAllLines(path, list);
                Console.WriteLine("Uus retsept on edukalt faili salvestatud!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Viga: {e}");
            }
        }

        public static void ItaaliaRestoran()
        {
            Console.WriteLine("\n--- 6. ITAALIA RESTORANI MENÜÜ (TUPLE) ---");

            if (!File.Exists(menuuPath))
            {
                string[] algAndmed = {
                "Margherita pitsa;San Marzano tomatid, mozzarella, basiilik;8.50",
                "Pasta Carbonara;Spagetid, guanciale, pecorino, muna;12.00",
                "Tiramisu;Mascarpone, espresso, savoiardi;6.50"
            };
                File.WriteAllLines(menuuPath, algAndmed);
            }

            List<Tuple<string, string, double>> menyyList = new List<Tuple<string, string, double>>();
            try
            {
                foreach (string rida in File.ReadAllLines(menuuPath))
                {
                    string[] osad = rida.Split(';');
                    if (osad.Length == 3)
                    {
                        double hind = double.Parse(osad[2].Replace('.', ','));
                        menyyList.Add(Tuple.Create(osad[0], osad[1], hind));
                    }
                }
                Console.Clear();
                Console.WriteLine("===========================================");
                Console.WriteLine("          * LA BELLA ITALIA *          ");
                Console.WriteLine("===========================================\n");
                foreach (var toit in menyyList)
                {
                    Console.WriteLine($"{toit.Item1.PadRight(30)} {toit.Item3:F2} Eur");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"   Koostis: {toit.Item2}");
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Viga restoranimenüü töötlemisel: " + ex.Message);
            }
        }
    }
}
