using RS1_P120_MobitelShop.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS1_P120_MobitelShop.Models
{
    public class Narudzba : IEntity
    {
        public int BrojNarudzbe { get; set; }
        public DateTime DatumNarudzbe { get; set; }
        public string StatusNarudzbe { get; set; }
        public bool Otkazano { get; set; }

        public Isporuka Isporuka { get; set; }
        public int IsporukaId { get; set; }

        public Klijent Klijent { get; set; }
        public int KlijentId { get; set; }

        public int Id { get; set; }
    }
}