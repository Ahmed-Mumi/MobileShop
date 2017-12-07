using RS1_P120_MobitelShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RS1_P120_MobitelShop.Areas.ModulAdministracija.Models
{
    public class ArtikalEditVM
    {
        public int ArtikalId { get; set; }
        public Specifikacije Specifikacije { get; set; }
        public int SpecifikacijeId { get; set; }
        public DateTime datumObjave { get; set; }
        public string sifraArtikla { get; set; }
        public string slika { get; set; }
        public string model { get; set; }
        public string marka { get; set; }
        public double cijena { get; set; }
        public string garancija { get; set; }
        public string operativniSistem { get; set; }
        public string eksternaMemorija { get; set; }
        public string ekran { get; set; }
        public string rezolucija { get; set; }
        public string vrstaEkrana { get; set; }
        public string jezgreProcesora { get; set; }
        public string kamera { get; set; }
        public string povezivanje { get; set; }
        public string RAM { get; set; }       
    }
}