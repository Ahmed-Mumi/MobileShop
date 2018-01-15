﻿using RS1_P120_MobitelShop.Areas.ModulKlijenti.ViewModel;
using RS1_P120_MobitelShop.DAL;
using RS1_P120_MobitelShop.Helper;
using RS1_P120_MobitelShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;

namespace RS1_P120_MobitelShop.Areas.ModulKlijenti.Controllers
{
    public class ProfileController : Controller
    {
        MojContext ctx = new MojContext();
        public ActionResult Index()
        {
            ProfilPodaciVM Model = new ProfilPodaciVM();
            Korisnik Korisnik = Autentifikacija.GetLogiraniKorisnik(HttpContext);
            Model.Korisnik =Korisnik;
            Model.gradoviStavke = ucitajGradove(Korisnik);
            Model.Email = Korisnik.Email;
            Model.Ime = Korisnik.Ime;
            Model.Prezime = Korisnik.Prezime;
            Model.Adresa = Korisnik.Adresa;
            Model.Telefon = Korisnik.Telefon;
            if(Korisnik.GradId!=null && Korisnik.GradId!=0)
                 Model.GradNaziv = Korisnik.Grad.Naziv;
            return View("Index",Model);
        }

        private List<SelectListItem> ucitajGradove(Korisnik Korisnik)
        {
            var gradovi = new List<SelectListItem>();
            if (Korisnik.GradId == 0 || Korisnik.GradId==null)
            {
                gradovi.Add(new SelectListItem { Value = null, Text = "Izaberite grad" });
                gradovi.AddRange(ctx.Gradovi.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Naziv
                }));
            }
            else
            {
                gradovi.AddRange(ctx.Gradovi.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Naziv,
                    Selected = x.Id == Korisnik.GradId ? true : false
                }));
            }
            return gradovi;
        }

        public ActionResult Snimi(ProfilPodaciVM vm)
        {  
            if (!ModelState.IsValid)
            {
                Korisnik korisnik= ctx.Korisnici.Find(Autentifikacija.GetLogiraniKorisnik(HttpContext).Id);
                vm.gradoviStavke = ucitajGradove(korisnik);
                vm.GradNaziv = korisnik.Grad.Naziv;
                return View("Index", vm);
            }
            Korisnik Korisnik = ctx.Korisnici.Find(Autentifikacija.GetLogiraniKorisnik(HttpContext).Id);
            Korisnik.Ime = vm.Ime; 
            Korisnik.Prezime = vm.Prezime;
            Korisnik.Telefon = vm.Telefon;
            Korisnik.Adresa = vm.Adresa;
            Korisnik.GradId = vm.GradId; 
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}