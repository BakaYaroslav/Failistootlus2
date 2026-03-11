using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace Failistootlus
{
    public class Restoran_funktsioonid
    {
        static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Koostisosad.txt");
        static string retseptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Retseptid.txt");
        static string menuuPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Menuu.txt");
        static List<menuu> menuuList = new List<menuu>();
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
            Console.WriteLine("--- 6. ITAALIA RESTORANI MENÜÜ ---");

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
                Console.WriteLine("               *  ITALIA *          ");
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





        public static void ItaaliaRestoraanKlassiga()
        {
            Console.Clear();
            Console.WriteLine("tere tulemast Itaalia restorani!");
            Console.WriteLine("================================");
            Console.WriteLine("             Menüü              ");
            Console.WriteLine("================================");
            if (menuuList.Count == 0)
            {
                Console.WriteLine("menüü on tühi. palun laadige andmed failist. ");
            }
            else
            {
                foreach (menuu item in menuuList)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Nimetus: {item.Nimetus}");
                    Console.ResetColor();
                    Console.WriteLine($"Koostisosad: {string.Join(", ", item.Koostisosad)}");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"hind: {item.Hind} €");
                    Console.ResetColor();
                    Console.WriteLine("-------------------------------------");



                }
            }

        }
        public static void LaeAndmedFailist()
        {
            if (File.Exists(menuuPath))
            {
                string[] osad = File.ReadAllLines(menuuPath);
                foreach (string line in osad)
                {
                    string[] parts = line.Split(';');
                    if (parts.Length == 3)
                    {
                        string nimetus = parts[0];
                        List<string> koostisosad = new List<string>(parts[1].Split(", "));
                        double hind = double.Parse(parts[2].Replace('.', ','));
 
                        menuu menuuItem = new menuu(nimetus, koostisosad, hind);
                        menuuList.Add(menuuItem);
                    }
                }
                Console.WriteLine($"andmed on adukalt laaditud. Kokku on {menuuList.Count} toitu");
            }
            else
            {
                Console.WriteLine("andmefaili ei leidnud. ");
            }
        }

        public static void LisaUusToit()
        {
            Console.WriteLine("sisesta uue toitu info...  ");
            Console.WriteLine("Nimetus:  ");
            string nimetus = Console.ReadLine();
            Console.WriteLine("Koostisosad: ");
            List<string> koostisosad = new List<string>();
            while (true)
            {
                Console.WriteLine("Aine (või vajuta Enter, et lõpetada): ");
                string aine = Console.ReadLine();
                if (string.IsNullOrEmpty(aine))
                {
                    break; 
                }
                koostisosad.Add(aine);
            }
            Console.WriteLine("sisesta hind (nt 12.99): ");
            double hind = double.Parse(Console.ReadLine().Replace('.', ','));

            menuuList.Add(new menuu(nimetus, koostisosad, hind));
            Console.WriteLine($"uus toit {nimetus} on lisatud.");
           
        }

        public static void SalvestaAndmedFaili()
        {
            Console.WriteLine("salvestamine andmeid faili... ");
            try
            {
                List<string> failiread = new List<string>(); 
                foreach ( menuu item in menuuList)
                {
                    failiread.Add(item.VormindaFailiJaoksRea());
                }
                File.WriteAllLines(menuuPath, failiread);
                Console.WriteLine("andmed on salvestatud!");
            }
            catch (Exception e)
            {

                Console.WriteLine("viga: ", e);
            }
        }

        public static void KustutaToit()
        {
            Console.WriteLine("sisesta kustutava toidu nimetus:  ");
            string nimetus = Console.ReadLine();
            menuu itemToRemove = menuuList.Find(item => item.Nimetus.Equals(nimetus, StringComparison.OrdinalIgnoreCase));
            if (itemToRemove != null)
            {
                menuuList.Remove(itemToRemove);
                Console.WriteLine($"toit {nimetus} on menüüst kustutatud. ");
            }
            else
            {
                Console.WriteLine($"toitu nimega {nimetus} ei leidnud menüüst. ");
            }
        }

        public static void InfoToit()
        {
            Console.WriteLine("sisesta toidu nimetus:  ");
            string nimetus = Console.ReadLine();
            menuu itemToInfo = menuuList.Find(item => item.Nimetus.Equals(nimetus, StringComparison.OrdinalIgnoreCase));
            if (itemToInfo != null)
            {
               
                Console.WriteLine($"toit {nimetus} on menüüst koostab: ");
                foreach (menuu item in menuuList)
                {
                    Console.WriteLine($"Koostisosad: {string.Join(", ", item.Koostisosad)}");
                }
                   
            }
            else
            {
                Console.WriteLine($"toitu nimega {nimetus} ei leidnud menüüst. ");
            }
        }
    }
}
