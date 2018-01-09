using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RS1_P120_MobitelShop.Models;
using PagedList;
namespace RS1_P120_MobitelShop.Areas.ModulKlijenti.ViewModel
{
    public class HomeIndexRow
    {
        public string Slika { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public double Cijena { get; set; }
        public double ArtikalId { get; set; }
    }
    public class HomeIndexVM
    {
        public List<HomeIndexRow> listaNajnovijihArtikala { get; set; }
        public IPagedList<Artikal> listaArtikala { get; set; }
        public List<string> searchArtikliString { get; set; }
        public IPagedList<Artikal> listaArtikalaPoSearch { get; set; }
        public int BrojArtikalaUKorpi { get; set; }
    }
}