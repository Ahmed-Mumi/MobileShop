using RS1_P120_MobitelShop.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS1_P120_MobitelShop.Models
{
    public class Korpa : IEntity
    {
        public int Kolicina { get; set; }
        public int Id { get; set; }
        public virtual Artikal Artikal { get; set; }
        public int ArtikalId { get; set; }

        public virtual Klijent Klijent { get; set; }
        public int KlijentId { get; set; }
    }
}