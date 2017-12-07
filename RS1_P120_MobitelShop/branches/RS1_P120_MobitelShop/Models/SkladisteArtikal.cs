using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RS1_P120_MobitelShop.Helper;

namespace RS1_P120_MobitelShop.Models
{
    public class SkladisteArtikal:IEntity
    {
        public int Id { get; set; }
        public int Kolicina { get; set; }

        public virtual Artikal Artikal { get; set; }
        public int ArtikalId { get; set; }

        public virtual Skladiste Skladiste { get; set; }
        public int SkladisteId { get; set; }
    }
}