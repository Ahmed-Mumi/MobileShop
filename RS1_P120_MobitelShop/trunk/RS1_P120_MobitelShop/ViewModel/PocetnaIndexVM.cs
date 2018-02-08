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
    public class SpecVM
    {
        public int RamId { get; set; }
        public string RamNaziv { get; set; }
        public bool isRamChecked { get; set; }
        public bool isRam { get; set; }
        public int EksternaMemorijaId { get; set; }
        public string EksternaMemorijaNaziv { get; set; }
        public bool isEksternaMemorijaChecked { get; set; }
        public bool isEksternaMemorija { get; set; }
        public int OperativniSistemId { get; set; }
        public string OperativniSistemNaziv { get; set; }
        public bool isOperativniSistemChecked { get; set; }
        public bool isOperativniSistem { get; set; }
        public int EkranId { get; set; }
        public string EkranNaziv { get; set; }
        public bool isEkranChecked { get; set; }
        public bool isEkran { get; set; }
    }
    public class CijeneVM
    {
        public double cijenaOd { get; set;  }
        public double cijeneDo { get; set; }
    }
    public class PocetnaIndexVM
    {
        public List<PocetnaIndexRow> listaNajnovijihArtikala { get; set; }
        public IPagedList<Artikal> listaArtikala { get; set; }
        public List<SpecVM> specifikacijeList { get; set; }
        public List<string> searchArtikliString { get; set; }
        public IPagedList<Artikal> listaArtikalaPoSearch { get; set; }
        public int BrojArtikalaUKorpi { get; set; }
        public Korisnik Korisnik { get; set; }
        public List<Obavijest> Obavijesti { get; set; }
        public List<CijeneVM> cijeneRange { get; set; }
        public double cijenaOd { get; set; }
        public double cijenaDo { get; set; }
    }
  
}