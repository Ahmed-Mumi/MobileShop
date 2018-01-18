using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RS1_P120_MobitelShop.Models;
using PagedList;
namespace RS1_P120_MobitelShop.ViewModel
{
    public class PocetnaIndexRow
    {
        public string Slika { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public double Cijena { get; set; }
        public double ArtikalId { get; set; }
        public string Ekran { get; set; } 
        public string VrstaEkrana { get; set; } 
        public string Kamera { get; set; }
        public string OperativniSistem { get; set; } 
        public int? Popust { get; set; }
        public double CijenaSaPopustom { get; set; }
    }
    public class PocetnaIndexVM
    {
        public List<PocetnaIndexRow> listaNajnovijihArtikala { get; set; }
        public IPagedList<Artikal> listaArtikala { get; set; }
        public List<string> searchArtikliString { get; set; }
        public IPagedList<Artikal> listaArtikalaPoSearch { get; set; }
        public int BrojArtikalaUKorpi { get; set; }
        public Korisnik Korisnik { get; set; }
    }
}