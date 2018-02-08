using RS1_P120_MobitelShop.Areas.ModulAdministracija.Models;
using RS1_P120_MobitelShop.Areas.ModulAdministracija.ViewModels;
using RS1_P120_MobitelShop.DAL;
using RS1_P120_MobitelShop.Helper;
using RS1_P120_MobitelShop.Models;
using RS1_P120_MobitelShop.ViewModel;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RS1_P120_MobitelShop.Areas.ModulAdministracija.Controllers
{
    [Autorizacija(KorisnickeUloge.Administrator)]
    public class KlijentiController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulAdministracija/Klijenti
        public ActionResult Index()
        {
            return View("Index");
        }
        public ActionResult Prikazi()
        {
            KlijentiPrikaziVM Model = new KlijentiPrikaziVM
            {
                klijentStavke = ctx.Klijenti
                .Select(x => new KlijentiPrikaziVM.KlijentInfo()
                {
                    KlijentId = x.Id,
                    Ime = x.Korisnik.Ime,
                    Prezime = x.Korisnik.Prezime,
                    DatumRodjenja = x.Korisnik.DatumRodjenja.Value,
                    Email = x.Korisnik.Email,
                    Telefon = x.Korisnik.Telefon,
                    Adresa = x.Korisnik.Adresa,
                    KorisnickoIme = x.Korisnik.Login.Username,
                    Grad = x.Korisnik.Grad.Naziv
                    //GradId = x.Korisnik.GradId.Value
                }).ToList()
            };
            return View("Prikazi", Model);
        }
        

        //public ActionResult Dodaj()
        //{
        //    Klijent klijent;
        //    klijent = new Klijent();
        //    klijent.Korisnik = new Korisnik();
        //    klijent.Korisnik.Login = new Login();
        //    KlijentiEditVM Model = new KlijentiEditVM()
        //    {
        //         Klijent = klijent,
        //        gradovi = ucitajGradove()
        //    };
        //    return View("Uredi",Model);
        //}
        public ActionResult Uredi(int id)
        {
            Klijent k = ctx.Klijenti.Where(y => y.Korisnik.Id == id).FirstOrDefault();
            KlijentiEditVM Model = new KlijentiEditVM()
            {
                Ime = k.Korisnik.Ime,
                Prezime = k.Korisnik.Prezime,
                DatumRodjenja = k.Korisnik.DatumRodjenja.Value,
                Email = k.Korisnik.Email,
                Telefon = k.Korisnik.Telefon,
                Adresa = k.Korisnik.Adresa,
                KorisnickoIme = k.Korisnik.Login.Username,
                gradovi = ucitajGradove(),
                LoginId = k.Korisnik.LoginId,
                GradId = k.Korisnik.GradId.Value,
                KlijentId = k.Korisnik.Klijent.Id
            };
            return View("Uredi", Model);
        }

        private List<SelectListItem> ucitajGradove()
        {
            var grad = new List<SelectListItem>();
            grad.AddRange(ctx.Gradovi.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }));
            return grad;
        }
        public ActionResult Obrisi(int id)
        {
            Klijent k = ctx.Klijenti.Find(id);
            ctx.Klijenti.Remove(k);
            ctx.SaveChanges();
            return RedirectToAction("Prikazi");
        }
        public ActionResult Snimi(KlijentiEditVM vm)
        {
            Klijent klijent = new Klijent();
            //if(vm.KlijentId==0)
            //{
            //    klijent = new Klijent();
            //    klijent.Korisnik = new Korisnik();
            //    klijent.Korisnik.Login = new Login();
            //    ctx.Klijenti.Add(klijent);
            //}
            //else
            //{
            //}

            klijent = ctx.Klijenti.Where(x => x.Id == vm.KlijentId).Include(x => x.Korisnik).Include(x => x.Korisnik.Login).FirstOrDefault();
            klijent.Korisnik.Ime = vm.Ime;
            klijent.Korisnik.Login.Username = vm.KorisnickoIme;
            klijent.Korisnik.Prezime = vm.Prezime;
            klijent.Korisnik.Telefon = vm.Telefon;
            klijent.Korisnik.Adresa = vm.Adresa;
            klijent.Korisnik.DatumRodjenja = Convert.ToDateTime(vm.DatumRodjenja);
            klijent.Korisnik.Email = vm.Email;
            klijent.Korisnik.GradId = vm.GradId;
           
            ctx.SaveChanges();
            return RedirectToAction("Prikazi");
        }

    }
}