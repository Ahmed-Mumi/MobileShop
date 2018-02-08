using RS1_P120_MobitelShop.Areas.ModulAdministracija.Models;
using RS1_P120_MobitelShop.DAL;
using RS1_P120_MobitelShop.Helper;
using RS1_P120_MobitelShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nexmo.Api;

namespace RS1_P120_MobitelShop.Areas.ModulAdministracija.Controllers
{
    [Autorizacija(KorisnickeUloge.Administrator)]
    public class NarudzbeController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulAdministracija/Narudzbe
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Prikazi()
        {
            NarudzbePrikaziVM Model = new NarudzbePrikaziVM
            {
                narudzbeStavke = ctx.Narudzbe.Select(x => new NarudzbePrikaziVM.NarudzbeInfo()
                {
                   Id = x.Id,
                   BrojNarudzbe = x.BrojNarudzbe,
                   DatumNarudzbe = x.DatumNarudzbe,
                   statusNarudzbe = x.StatusNarudzbe,
                   Otkazano = x.Otkazano,
                   IsporukaId = x.IsporukaId,
                   KlijentId = x.KlijentId,
                   isporukaVrsta = x.Isporuka._Naziv,
                   klijentIme = x.Klijent.Korisnik.Ime,
                }).ToList()
            };
            return View("Prikazi",Model);
        }
        [HttpPost]
        public ActionResult Detalji(int id)
        {
            DetaljiNarudzbePrikaziVM Model = new DetaljiNarudzbePrikaziVM
            {
                detaljiStavke = ctx.DetaljiNarudzbi.Where(x => x.NarudzbaId == id)
                .Select(x => new DetaljiNarudzbePrikaziVM.DetaljiInfo()
                {
                    Id = x.Id,
                    ArtikalId = x.ArtikalId,
                    Kolicina = x.Kolicina,
                    artikalNaziv = x.Artikal.Model,
                    CijenaArtikla = x.Artikal.Cijena,
                    Ukupno = x.Artikal.Cijena * x.Kolicina,
                   
                    //NarudzbaId = x.NarudzbaId,
                }).ToList(),
                NarudzbaId = id
            };
            return PartialView("Detalji");
        }
        public ActionResult promjeniStatus(int id)
        {
            Narudzba n = ctx.Narudzbe.Find(id);
            if(n.Otkazano == false)
            {
                n.Otkazano = true;

                //dodao
                var results = SMS.Send(new SMS.SMSRequest
                {
                    from = Configuration.Instance.Settings["appsettings:NEXMO_FROM_NUMBER"],
                    to = n.Klijent.Korisnik.Telefon,
                    text = "Postovani "+n.Klijent.Korisnik.Ime+" "+n.Klijent.Korisnik.Prezime+ ",<br> Vasa narudzba je isporucena. Hvala Vam na ukazanom povjerenju.<br> Vas MobileShop."
                });
            }
            else
            {
                n.Otkazano = false;
            }
            ctx.SaveChanges();
            return RedirectToAction("Prikazi");
        }
    }
}