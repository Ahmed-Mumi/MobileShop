using RS1_P120_MobitelShop.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS1_P120_MobitelShop.Models
{
    public class DetaljiNarudzbe : IEntity
    {
        public int Kolicina { get; set; }
        public Narudzba Narudzba { get; set; }
        public int NarudzbaId { get; set;}

        public Artikal Artikal { get; set; }
        public int ArtikalId { get; set; }

        public int Id { get; set; }       
    }
}
