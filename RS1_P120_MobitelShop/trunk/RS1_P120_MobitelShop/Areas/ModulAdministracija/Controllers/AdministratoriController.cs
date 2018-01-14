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
    public class AdministratoriController : Controller
    {
        // GET: ModulAdministracija/Administratori
        MojContext ctx = new MojContext();
        public ActionResult Index()
        {
            return View("Index");
        }
        public ActionResult Prikazi()
        {
            AdminPrikaziVM Model = new AdminPrikaziVM
            {
                adminStavke = ctx.Administratori
                .Select(x => new AdminPrikaziVM.AdminInfo()
                {
                    AdministratorId = x.Id,
                    Ime = x.Korisnik.Ime,
                    Prezime = x.Korisnik.Prezime,
                    DatumRodjenja = x.Korisnik.DatumRodjenja.Value,
                    Email = x.Korisnik.Email,
                    Telefon = x.Korisnik.Telefon,
                    Adresa = x.Korisnik.Adresa,
                    KorisnickoIme = x.Korisnik.Login.Username,
                    Grad = x.Korisnik.Grad.Naziv,
                    GradId = x.Korisnik.GradId.Value
                }).ToList()
            };
            return View("Prikazi", Model);
        }

        public ActionResult Dodaj()
        {
            Administrator admin;
            admin = new Administrator();
            admin.Korisnik = new Korisnik();
            admin.Korisnik.Login = new Login();
            AdminEditVM Model = new AdminEditVM()
            {
                Admin = admin,
                gradovi = ucitajGradove()
            };
            return View("Uredi", Model);
        }
        public ActionResult Uredi(int id)
        {
            Administrator k = ctx.Administratori.Where(y => y.Korisnik.Id == id).FirstOrDefault();
            AdminEditVM Model = new AdminEditVM()
            {
                Ime = k.Korisnik.Ime,
                Prezime = k.Korisnik.Prezime,
                DatumRodjenja = k.Korisnik.DatumRodjenja.Value,
                Email = k.Korisnik.Email,
                Telefon = k.Korisnik.Telefon,
                Adresa = k.Korisnik.Adresa,
                KorisnickoIme = k.Korisnik.Login.Username,
                password = k.Korisnik.Login.Password,
                gradovi = ucitajGradove(),
                LoginId = k.Korisnik.LoginId,
                GradId = k.Korisnik.GradId.Value,
                AdministratorId = k.Korisnik.Administrator.Id
            };
            return View("Uredi", Model);
        }
        public ActionResult Obrisi(int id)
        {
            Administrator admin = ctx.Administratori.Find(id);
            ctx.Administratori.Remove(admin);
            ctx.SaveChanges();
            return RedirectToAction("Prikazi");
        }
        public ActionResult Snimi(AdminEditVM vm)
        {
            Administrator admin;
            if(vm.Id == null)
            {
                admin = new Administrator();
                admin.Korisnik = new Korisnik();
                admin.Korisnik.Login = new Login();
                ctx.Administratori.Add(admin);
            }
            else
            {
            admin = ctx.Administratori.Where(x => x.Id == vm.AdministratorId).Include(x => x.Korisnik).Include(x => x.Korisnik.Login).FirstOrDefault();
            }

            admin.Korisnik.Ime = vm.Ime;
            admin.Korisnik.Login.Username = vm.KorisnickoIme;
            admin.Korisnik.Login.Password = vm.password;
            admin.Korisnik.Prezime = vm.Prezime;
            admin.Korisnik.Telefon = vm.Telefon;
            admin.Korisnik.Adresa = vm.Adresa;
            admin.Korisnik.DatumRodjenja = Convert.ToDateTime(vm.DatumRodjenja);
            admin.Korisnik.Email = vm.Email;
            admin.Korisnik.GradId = vm.GradId;

            ctx.SaveChanges();
            return RedirectToAction("Prikazi");
        }
        private List<SelectListItem> ucitajGradove()
        {
            var grad = new List<SelectListItem>();
            grad.AddRange(ctx.Gradovi.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }));
            return grad;
        }
    }
}