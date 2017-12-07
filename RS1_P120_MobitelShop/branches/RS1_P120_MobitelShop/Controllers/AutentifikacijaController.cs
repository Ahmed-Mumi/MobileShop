using RS1_P120_MobitelShop.DAL;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RS1_P120_MobitelShop.Models;
using RS1_P120_MobitelShop.Helper;
using RS1_P120_MobitelShop.ViewModel;

namespace RS1_P120_MobitelShop.Controllers
{
    public class AutentifikacijaController : Controller
    {
        // GET: Autentifikacija
        MojContext ctx = new MojContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Provjera(string username,string password,string zapamti)
        {
            Korisnik korisnik = ctx.Korisnici
                .Include(x => x.Administrator)
                .Include(x => x.Klijent)
                .SingleOrDefault(x => x.Login.Username == username && x.Login.Password == password);

                if(korisnik == null)
                {
                    return RedirectToAction("Index");
                }

            Autentifikacija.PokreniNovuSesiju(korisnik,HttpContext,(zapamti =="on"));

            Korisnik k = Autentifikacija.GetLogiraniKorisnik(HttpContext);

            if (k.Administrator != null  && k.Klijent == null)
            {
                Autentifikacija.PokreniNovuSesiju(k, HttpContext, (zapamti == "on"));
                return Redirect("/ModulAdministracija/AdminHome");
            }

            //else if (k.Administrator == null && k.Klijent == null)
            //{
            //    Autentifikacija.PokreniNovuSesiju(k, HttpContext, (zapamti == "on"));
            //    return Redirect("/PrikazPonude");
            //}

            else if (k.Administrator == null && k.Klijent != null)
            {
                Autentifikacija.PokreniNovuSesiju(k, HttpContext, (zapamti == "on"));
                return Redirect("/ModulKlijenti/Korpa/Index");
            }

            return RedirectToAction("Index","Home");          
        }
        public ActionResult Logout()
        {
            Autentifikacija.PokreniNovuSesiju(null, HttpContext, true);
            return RedirectToAction("Index");
        }

        public ActionResult Dodaj()
        {
            Klijent klijent;
            klijent = new Klijent();
            klijent.Korisnik = new Korisnik();
            klijent.Korisnik.Login = new Login();

            RegistrationVM Model = new RegistrationVM();
            Model.Klijent = klijent;
            Model.gradoviStavke = ucitajGradove();
            return View("Registration",Model);
        }

        private List<SelectListItem> ucitajGradove()
        {
            var gradovi = new List<SelectListItem>();
            gradovi.Add(new SelectListItem { Value = null, Text = "Izaberite grad" });
            gradovi.AddRange(ctx.Gradovi.Select(x=> new SelectListItem{Value = x.Id.ToString(),Text = x.Naziv}));
            return gradovi;
        }

        public ActionResult Registration(RegistrationVM vm)
        {
            Klijent klijent = new Klijent();
            klijent.Korisnik = new Korisnik();
            klijent.Korisnik.Login = new Login();
            ctx.Klijenti.Add(klijent);

            klijent.Korisnik.Ime = vm.Ime;
            klijent.Korisnik.Login.Username = vm.Username;
            klijent.Korisnik.Login.Password = vm.Password;
            klijent.Korisnik.Prezime = vm.Prezime;
            klijent.Korisnik.Telefon = vm.Telefon;
            klijent.Korisnik.Adresa = vm.Adresa;
            klijent.Korisnik.DatumRodjenja = Convert.ToDateTime(vm.DatumRodjenja);
            klijent.Korisnik.Email = vm.Email;
            klijent.Korisnik.GradId = vm.GradId;
            ctx.SaveChanges();
            return Redirect("/Autentifikacija");
        }

    }
}