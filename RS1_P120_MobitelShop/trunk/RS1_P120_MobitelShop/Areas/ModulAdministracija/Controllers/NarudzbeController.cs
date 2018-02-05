﻿using RS1_P120_MobitelShop.Areas.ModulAdministracija.Models;
using RS1_P120_MobitelShop.DAL;
using RS1_P120_MobitelShop.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View("Detalji", Model);
        }
        
    }
}