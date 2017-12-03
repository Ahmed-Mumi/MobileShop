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
        public string Slika { get; set; }
        public string Model { get; set; }
        public string Marka { get; set; }
        public double Cijena { get; set; }
        public DateTime DatumObjave { get; set; }
        public string Garancija { get; set; }
        public virtual Specifikacije Specifikacije { get; set; }
        public int SpecifikacijeId { get; set; }  
    }
}