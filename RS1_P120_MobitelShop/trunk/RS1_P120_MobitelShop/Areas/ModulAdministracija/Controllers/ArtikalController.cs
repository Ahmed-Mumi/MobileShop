using RS1_P120_MobitelShop.Areas.ModulAdministracija.Models;
using RS1_P120_MobitelShop.DAL;
using RS1_P120_MobitelShop.Helper;
using RS1_P120_MobitelShop.Models;
using System;
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
            ArtikalEditVM Model = new ArtikalEditVM()
            {
                //popust = ucitajPopust()
            };
            return View("Edit", Model);
        }
        public ActionResult Snimi(ArtikalEditVM vm)
        {
            Artikal artikal;
            if (vm.ArtikalId == 0)
            {
                artikal = new Artikal();

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
            artikal.Sifra = vm.sifraArtikla;
            artikal.Garancija = vm.garancija;
            //artikal.SpecifikacijeId = vm.SpecifikacijeId;
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