using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RS1_P120_MobitelShop.Models;
using RS1_P120_MobitelShop.Helper;

namespace RS1_P120_MobitelShop.Models
{
    public class Artikal : IEntity
    {
        public int Id { get; set; }
        public string Sifra { get; set; }
        public byte Slika { get; set; }
        public string Model { get; set; }
        public string Marka { get; set; }
        public double Cijena { get; set; }

        public virtual Specifikacije Specifikacije { get; set; }
        public int SpecifikacijeId { get; set; }

        public virtual Popust Popust { get; set; }
        public int PopustId { get; set; }

        public virtual TipServisa TipServisa { get; set; }
        public int TipServisaId { get; set; }       
    }
}