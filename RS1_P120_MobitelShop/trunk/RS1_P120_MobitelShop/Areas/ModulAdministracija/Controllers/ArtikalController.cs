using RS1_P120_MobitelShop.Areas.ModulAdministracija.Models;
using RS1_P120_MobitelShop.DAL;
using RS1_P120_MobitelShop.Helper;
using RS1_P120_MobitelShop.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RS1_P120_MobitelShop.Areas.ModulAdministracija.Controllers
{
    [Autorizacija(KorisnickeUloge.Administrator)]
    public class ArtikalController : Controller
    {
        // GET: ModulAdministracija/Artikal
        MojContext ctx = new MojContext();
        public ActionResult Index()
        {
            return View("Index");
        }
        public ActionResult Prikazi()
        {
            ArtikalPrikaziVM Model = new ArtikalPrikaziVM
            {
                artikal = ctx.Artikli.Select(x => new ArtikalPrikaziVM.ArtikalInfo()
                {
                    Id = x.Id,
                    sifraArtikla = x.Sifra,
                    slika = x.Slika,
                    model = x.Model,
                    marka = x.Marka,
                    cijena = x.Cijena,
                    garancija = x.Garancija               
                }).ToList()                                  
            };
            return View("Prikazi", Model);
        }  
        public ActionResult Dodaj()
        {
            Artikal artikal;
            artikal = new Artikal();
            artikal.Specifikacije = new Specifikacije();
            ArtikalEditVM Model = new ArtikalEditVM();
            Model.datumObjave = DateTime.Now;
            return View("Edit", Model);
        }
        public ActionResult Detalji(int id)
        {
            ArtikalEditVM Model = new ArtikalEditVM();
            Artikal artikal = ctx.Artikli.Where(y => y.Id == id).FirstOrDefault();
            Model.ArtikalId = artikal.Id;
            Model.operativniSistem = artikal.Specifikacije.OperativniSistem;
            Model.rezolucija = artikal.Specifikacije.Rezolucija;
            Model.vrstaEkrana = artikal.Specifikacije.VrstaEkrana;
            Model.jezgreProcesora = artikal.Specifikacije.JezgreProcesora;
            Model.kamera = artikal.Specifikacije.Kamera;
            Model.ekran = artikal.Specifikacije.Ekran;
            Model.eksternaMemorija = artikal.Specifikacije.EksternaMemorija;
            Model.RAM = artikal.Specifikacije.RAM;
            Model.povezivanje = artikal.Specifikacije.Povezivanje;
            return View("Detalji", Model);
        }
        public ActionResult Uredi(int id)
        {
            ArtikalEditVM Model = new ArtikalEditVM();
            Artikal artikal = ctx.Artikli.Where(y => y.Id == id).FirstOrDefault();
            Model.ArtikalId = artikal.Id;
            Model.SpecifikacijeId = artikal.Specifikacije.Id;
            Model.datumObjave = artikal.DatumObjave;
            Model.marka = artikal.Marka;
            Model.model = artikal.Model;
            Model.cijena = artikal.Cijena;
            Model.slika = artikal.Slika;
            Model.garancija = artikal.Garancija;
            Model.sifraArtikla = artikal.Sifra;
            Model.operativniSistem = artikal.Specifikacije.OperativniSistem;
            Model.rezolucija = artikal.Specifikacije.Rezolucija;
            Model.vrstaEkrana = artikal.Specifikacije.VrstaEkrana;
            Model.jezgreProcesora = artikal.Specifikacije.JezgreProcesora;
            Model.kamera = artikal.Specifikacije.Kamera;
            Model.ekran = artikal.Specifikacije.Ekran;
            Model.eksternaMemorija = artikal.Specifikacije.EksternaMemorija;
            Model.RAM = artikal.Specifikacije.RAM;
            Model.povezivanje = artikal.Specifikacije.Povezivanje;
            return View("Edit", Model);
        }
        public ActionResult Obrisi(int id)
        {
            Artikal artikal = ctx.Artikli.Find(id);
            ctx.Artikli.Remove(artikal);
            ctx.SaveChanges();
            return RedirectToAction("Prikazi");
        }
        public ActionResult Snimi(ArtikalEditVM vm)
        {
            Artikal artikal;
            if (vm.ArtikalId == 0)
            {
                artikal = new Artikal();
                artikal.Specifikacije = new Specifikacije();
                ctx.Artikli.Add(artikal);           
            }
            else
            {
                artikal = ctx.Artikli.Where(x => x.Id == vm.ArtikalId).FirstOrDefault();
            }
            artikal.Marka = vm.marka;
            artikal.Model = vm.model;
            artikal.Cijena = vm.cijena;
            artikal.Slika = vm.slika;
            artikal.DatumObjave = vm.datumObjave;
            artikal.Sifra = vm.sifraArtikla;
            artikal.Garancija = vm.garancija;
            artikal.Specifikacije.Ekran = vm.ekran;
            artikal.Specifikacije.EksternaMemorija = vm.eksternaMemorija;
            artikal.Specifikacije.JezgreProcesora = vm.jezgreProcesora;
            artikal.Specifikacije.Kamera = vm.kamera;
            artikal.Specifikacije.OperativniSistem = vm.operativniSistem;
            artikal.Specifikacije.Povezivanje = vm.povezivanje;
            artikal.Specifikacije.RAM = vm.RAM;
            artikal.Specifikacije.Rezolucija = vm.rezolucija;
            artikal.Specifikacije.VrstaEkrana = vm.vrstaEkrana;
            artikal.SpecifikacijeId = vm.SpecifikacijeId;   
            ctx.SaveChanges();
            return RedirectToAction("Prikazi");
        }
        private List<SelectListItem> ucitajPopust()
        {
            var popust = new List<SelectListItem>();
            popust.Add(new SelectListItem { Value = null, Text = "Dodaj popust" });
            popust.AddRange(ctx.Popusti.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.IznosPopusta.ToString() }));
            return popust;
        }
    }
}