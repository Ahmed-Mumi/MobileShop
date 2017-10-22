using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RS1_P120_MobitelShop.Helper;

namespace RS1_P120_MobitelShop.Models
{
    public class OcjenaMobitela:IEntity
    {
        public int Id { get; set; }
        public int Ocjena { get; set; }

        public Klijent Klijent { get; set; }
        public int KlijentId { get; set; }

        public Artikal Artikal { get; set; }
        public int ArtikalId { get; set; }
    }
}