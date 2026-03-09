using System;
using System.Collections.Generic;
using System.Text;

namespace Failistootlus
{
    internal class Main_Restoran
    {
        public static void Main(string[] args)
        {
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

                    case "3": List<string> koostisosad= Restoran_funktsioonid.Koostisosade_muutmine_nimekirjas(); break;

                    case "4": Restoran_funktsioonid.Külmkapi_kontroll_ehk_otsing_listist(); break;
                }
            }
        }
    }
}
