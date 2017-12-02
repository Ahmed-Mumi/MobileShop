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
    public class DobavljaciController : Controller
    {
        // GET: ModulAdministracija/Dobavljaci
        MojContext ctx = new MojContext();
        public ActionResult Index()
        {
            return View("Index");
        }
        public ActionResult Prikazi()
        {
            DobavljaciPrikaziVM Model = new DobavljaciPrikaziVM
            {
                dobavljaciStavke = ctx.Dobavljaci
                .Select(x => new DobavljaciPrikaziVM.DobavljaciInfo()
                {
                    Id = x.Id,
                    Naziv = x.Naziv,
                    Email = x.Email,
                    Telefon = x.Telefon,
                    Adresa = x.Adresa,
                    Grad = x.Grad.Naziv,
                    skladista = x.Skladiste.Naziv,            
                   SkladisteId = x.SkladisteId,
                   GradId = x.GradId
                }).ToList()
            };
            return View("Prikazi", Model);
        }
        public ActionResult Dodaj()
        {
            Dobavljac dobavljac;
            dobavljac = new Dobavljac();
            DobavljaciEditVM Model = new DobavljaciEditVM()
            {
                Dobavljac = dobavljac,
                gradovi = ucitajGradove(),
                skladiste = ucitajSkladista()
            };
            return View("Uredi", Model);
        }
        public ActionResult Snimi(DobavljaciEditVM vm)
        {
            Dobavljac dobavljac;
            if (vm.Id == 0)
            {
                dobavljac = new Dobavljac();
                
                ctx.Dobavljaci.Add(dobavljac);
            }
            else
            {
                dobavljac = ctx.Dobavljaci.Where(x => x.Id == vm.Id).FirstOrDefault();
            }

            dobavljac.Naziv = vm.Naziv;
            dobavljac.Email = vm.Email;
            dobavljac.Telefon = vm.Telefon;
            dobavljac.Adresa = vm.Adresa;
            dobavljac.GradId = vm.GradId;
            dobavljac.SkladisteId = vm.SkladisteId;
            ctx.SaveChanges();
            return RedirectToAction("Prikazi");
        }
        private List<SelectListItem> ucitajSkladista()
        {
            var skladista = new List<SelectListItem>();
            skladista.AddRange(ctx.Skladista.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }));
            return skladista;
        }
        private List<SelectListItem> ucitajGradove()
        {
            var grad = new List<SelectListItem>();
            grad.AddRange(ctx.Gradovi.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }));
            return grad;
        }
    }
}