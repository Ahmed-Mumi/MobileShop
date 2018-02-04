using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RS1_P120_MobitelShop.DAL;
using RS1_P120_MobitelShop.Models;
using RS1_P120_MobitelShop.Areas.ModulKlijenti.ViewModel; 
using RS1_P120_MobitelShop.Helper;

namespace RS1_P120_MobitelShop.Areas.ModulKlijenti.Controllers
{
    public class UporediController : Controller
    {
        MojContext ctx = new MojContext();
        ArtikliDetaljiVM ModelArtikalDetalji = new ArtikliDetaljiVM(); 
        public ActionResult Index(int artikalId,int? klijentId, string searchTerm2)
        {
            Artikal artikal = ctx.Artikli.Find(artikalId);
            ModelArtikalDetalji.ArtikalId = artikal.Id;
            if(klijentId!=null)
                ModelArtikalDetalji.KlijentId = klijentId.Value; 
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
        [Autorizacija(KorisnickeUloge.Klijent)] 
        public ActionResult Staviukorpi(int ArtikalId, int KlijentId)
        {
            Korisnik k = Autentifikacija.GetLogiraniKorisnik(HttpContext); 
            Korpa korpa = new Korpa();
            ctx.Korpe.Add(korpa);
            korpa.KlijentId = k.Klijent.Id;
            korpa.ArtikalId = ArtikalId;
            if (k != null)
                ctx.SaveChanges();
            //return RedirectToAction("Detalji", new { artikalId = ArtikalId });
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());

        }

        public ActionResult Detalji(int artikalId)
        {
            Korisnik k = Autentifikacija.GetLogiraniKorisnik(HttpContext); 
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
            if (k != null)
            {
                ModelArtikalDetalji.KlijentId = k.Id;
                ModelArtikalDetalji.BrojArtikalaUKorpi = ctx.Korpe.Count(x => x.KlijentId == k.Id);
            }
            ModelArtikalDetalji.GalerijaSlika = ctx.Galerije.Where(x => x.ArtikalId == artikalId).ToList();
            return View("Detalji", ModelArtikalDetalji);
        }

        public JsonResult GetStudents(string term)
        {
            HomeIndexVM ModelHomeIndex = new HomeIndexVM();
            ModelHomeIndex.searchArtikliString = ctx.Artikli.Where(h => h.Model.Contains(term)).Select(y => y.Model).ToList();
            return Json(ModelHomeIndex.searchArtikliString, JsonRequestBehavior.AllowGet);
        }
    } 
}