using RS1_P120_MobitelShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS1_P120_MobitelShop.Areas.ModulKlijenti.ViewModel
{
    public class ArtikliDetaljiVM
    {
        public string Slika { get; set; }
        public string Model { get; set; }
        public double Cijena { get; set; }
        public string Garancija { get; set; } 
        public string OperativniSistem { get; set; }
        public string EksternaMemorija { get; set; }
        public string Ekran { get; set; }
        public string Rezolucija { get; set; }
        public string VrstaEkrana { get; set; }
        public string JezgreProcesora { get; set; }
        public string Kamera { get; set; }
        public string Povezivanje { get; set; }
        public string RAM { get; set; }
        public int ArtikalId { get; set; }
        public int KlijentId { get; set; }
        public int BrojArtikalaUKorpi { get; set; }
    }
}