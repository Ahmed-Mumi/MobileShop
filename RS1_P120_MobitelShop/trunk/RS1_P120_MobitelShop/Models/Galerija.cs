using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS1_P120_MobitelShop.Models
{
    public class Galerija
    {
        public int Id { get; set; }
        public virtual Artikal Artikal { get; set; }
        public int ArtikalId { get; set; }
        public string Slika { get; set; }
    }
}