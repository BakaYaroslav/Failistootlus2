using System;
using System.Collections.Generic;
using System.Text;

namespace Failistootlus
{
    public class menuu
    {
        public string Nimetus { get; set; }
        public List<string> Koostisosad { get; set; }
        public double Hind { get; set;  }


        // konstruktor, et luue menuu objekt
        public menuu(string nimetus, List<string> koostisosad, double hind)
        {
            Nimetus = nimetus;
            Koostisosad = koostisosad;
            Hind = hind;
        }

        //meetod mis teeb, objektist tekstirea
        // nt: "Pizza;Tomat;Juust; Peperoni; 12.99
        public string VormindaFailiJaoksRea()
        {
            string ained = string.Join(", ", Koostisosad);
            return $"{Nimetus}; {ained}; {Hind}";
        }
    }
}
