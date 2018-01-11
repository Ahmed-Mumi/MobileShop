using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RS1_P120_MobitelShop.DAL;
using RS1_P120_MobitelShop.Models;
using RS1_P120_MobitelShop.Areas.ModulKlijenti.ViewModel;

namespace RS1_P120_MobitelShop.Areas.ModulKlijenti.Controllers
{
    public class UporediController : Controller
    {
        MojContext ctx = new MojContext();
        ArtikliDetaljiVM ModelArtikalDetalji = new ArtikliDetaljiVM();

        public ActionResult Index(int artikalId, string searchTerm2)
        {
            Artikal artikal = ctx.Artikli.Find(artikalId);
            ModelArtikalDetalji.ArtikalId = artikal.Id;
            ModelArtikalDetalji.Cijena = artikal.Cijena;
            ModelArtikalDetalji.Ekran = artikal.Specifikacije.Ekran;
            ModelArtikalDetalji.EksternaMemorija = artikal.Specifikacije.EksternaMemorija;
            ModelArtikalDetalji.Garancija = artikal.Garancija;
            ModelArtikalDetalji.JezgreProcesora = artikal.Specifikacije.JezgreProcesora;
            ModelArtikalDetalji.Model = artikal.Model;
            ModelArtikalDetalji.OperativniSistem = artikal.Specifikacije.OperativniSistem;
            ModelArtikalDetalji.Povezivanje = artikal.Specifikacije.Povezivanje;
            ModelArtikalDetalji.RAM = artikal.Specifikacije.RAM;
            ModelArtikalDetalji.Kamera = artikal.Specifikacije.Kamera;
            ModelArtikalDetalji.Rezolucija = artikal.Specifikacije.Rezolucija;
            ModelArtikalDetalji.Slika = artikal.Slika;
            ModelArtikalDetalji.VrstaEkrana = artikal.Specifikacije.VrstaEkrana;
            ModelArtikalDetalji.pronadjenarijec = searchTerm2;
            if (!string.IsNullOrEmpty(searchTerm2))
                ModelArtikalDetalji.artikalUporedi = ctx.Artikli.Where(x => x.Model.Contains(searchTerm2)).FirstOrDefault();
            return View("Uporedi",ModelArtikalDetalji);
        } 

        public JsonResult GetStudents(string term)
        {
            HomeIndexVM ModelHomeIndex = new HomeIndexVM();
            ModelHomeIndex.searchArtikliString = ctx.Artikli.Where(h => h.Model.Contains(term)).Select(y => y.Model).ToList();
            return Json(ModelHomeIndex.searchArtikliString, JsonRequestBehavior.AllowGet);
        }


    } 
}