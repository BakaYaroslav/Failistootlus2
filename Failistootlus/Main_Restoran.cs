using System;
using System.Collections.Generic;
using System.Text;

namespace Failistootlus
{
    internal class Main_Restoran
    {
        static List<string> list = new List<string>();
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            while (true)
            {
                Console.WriteLine("Funktsioonid");
                string valik = Console.ReadLine();
                string fail = "";
                List<string> list = new List<string>();
                switch (valik)
                {
                    case "1": Restoran_funktsioonid.Lemmiktoidu_salvestamine_faili(); break;
                    case "2": Restoran_funktsioonid.Kogu_menüü_kuvamine(); break;

                    case "3": List<string> koostisosad = Restoran_funktsioonid.Koostisosade_muutmine_nimekirjas(); break;

                    case "4": Restoran_funktsioonid.Külmkapi_kontroll_ehk_otsing_listist(); break;
                    
                    case "5": Restoran_funktsioonid.Uuendatud_nimekirja_salvestamine(list); break;
                    case "6": Restoran_funktsioonid.ItaaliaRestoran(); break;
                    case "7": Restoran_funktsioonid.ItaaliaRestoraanKlassiga(); break;
                    case "8": Restoran_funktsioonid.LaeAndmedFailist(); break;
                    case "9": Restoran_funktsioonid.LisaUusToit(); break;
                    case "10": Restoran_funktsioonid.SalvestaAndmedFaili(); break;
                    case "11": Restoran_funktsioonid.KustutaToit(); break;
                    case "12": 
                }
            }
        }
    }
}
