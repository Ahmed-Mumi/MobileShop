using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RS1_P120_MobitelShop.Helper;
namespace RS1_P120_MobitelShop.Models
{
    public class Specifikacije:IEntity
    {
        public int Id { get; set; }
        public string OperativniSistem { get; set; }
        public string EksternaMemorija { get; set; }
        public string Ekran { get; set; }
        public string Rezolucija { get; set; }
        public string VrstaEkrana { get; set; }
        public string JezgreProcesora { get; set; }
        public string Kamera { get; set; }
        public string Povezivanje { get; set; }
        public string RAM { get; set; }
    }
}