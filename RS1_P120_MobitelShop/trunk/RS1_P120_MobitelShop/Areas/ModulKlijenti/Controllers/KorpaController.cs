﻿using RS1_P120_MobitelShop.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RS1_P120_MobitelShop.DAL;
using RS1_P120_MobitelShop.Areas.ModulKlijenti.ViewModel;
using RS1_P120_MobitelShop.Models;

namespace RS1_P120_MobitelShop.Areas.ModulKlijenti.Controllers
{
    public class KorpaController : Controller
    {
        // GET: ModulKlijenti/Korpa
        MojContext ctx = new MojContext();
        [Autorizacija(KorisnickeUloge.Klijent)]
        public ActionResult Index()
        {
            HomeIndexVM Model = new HomeIndexVM()
            {
                listaNajnovijihArtikala = ctx.Artikli.OrderByDescending(x => x.DatumObjave).Select(p => new HomeIndexRow()
                {
                    Slika=p.Slika,
                    Model=p.Model,
                    Marka=p.Marka,
                    Cijena=p.Cijena,
                    ArtikalId=p.Id
                }).Take(2).ToList()
            };
            return View("Index",Model);
        } 
        public ActionResult IspisiSpecifikacije()
        { 
            List<SpecifikacijeVM> specifikacijeList = new List<SpecifikacijeVM>();
            foreach (var x in ctx.Artikli)
            {
                if (specifikacijeList.Count == 0)
                {
                    //specifikacijeList.Add(new SpecifikacijeVM { RamId = x.Id, RamNaziv = x.Specifikacije.RAM, isRamChecked = false,isRam=true });
                    //specifikacijeList.Add(new SpecifikacijeVM { EksternaMemorijaId = x.Id, EksternaMemorijaNaziv = x.Specifikacije.EksternaMemorija, isEksternaMemorijaChecked = false,isEksternaMemorija=true });
                    //specifikacijeList.Add(new SpecifikacijeVM { OperativniSistemId = x.Id, OperativniSistemNaziv = x.Specifikacije.OperativniSistem, isOperativniSistemChecked = false,isOperativniSistem=true });
                    //specifikacijeList.Add(new SpecifikacijeVM { EkranId = x.Id, EkranNaziv = x.Specifikacije.Ekran, isEkranChecked = false,isEkran=true });


                    specifikacijeList.Add(new SpecifikacijeVM { RamId = x.Id, RamNaziv = x.Specifikacije.RAM, ISCHECKEDUAAAAAAAAAAAAAAAAAAAAA = false, isRam = true });
                    specifikacijeList.Add(new SpecifikacijeVM { EksternaMemorijaId = x.Id, EksternaMemorijaNaziv = x.Specifikacije.EksternaMemorija, ISCHECKEDUAAAAAAAAAAAAAAAAAAAAA = false, isEksternaMemorija = true });
                    specifikacijeList.Add(new SpecifikacijeVM { OperativniSistemId = x.Id, OperativniSistemNaziv = x.Specifikacije.OperativniSistem, ISCHECKEDUAAAAAAAAAAAAAAAAAAAAA = false, isOperativniSistem = true });
                    specifikacijeList.Add(new SpecifikacijeVM { EkranId = x.Id, EkranNaziv = x.Specifikacije.Ekran, ISCHECKEDUAAAAAAAAAAAAAAAAAAAAA = false, isEkran = true });
                }
                else
                {
                    bool sameRam = false, sameMemory=false, sameOS=false, sameEkran=false;
                    foreach (var p in specifikacijeList.ToList())
                    {
                        if (p.RamNaziv == x.Specifikacije.RAM)
                            sameRam = true;
                        if (p.EksternaMemorijaNaziv == x.Specifikacije.EksternaMemorija)
                            sameMemory = true;
                        if (p.OperativniSistemNaziv == x.Specifikacije.OperativniSistem)
                            sameOS = true;
                        if (p.EkranNaziv == x.Specifikacije.Ekran)
                            sameEkran = true;
                    }
                    if (!sameRam)
                        specifikacijeList.Add(new SpecifikacijeVM { RamId = x.Id, RamNaziv = x.Specifikacije.RAM, isRamChecked = false, isRam = true });
                    if (!sameMemory)
                        specifikacijeList.Add(new SpecifikacijeVM { EksternaMemorijaId = x.Id, EksternaMemorijaNaziv = x.Specifikacije.EksternaMemorija, isEksternaMemorijaChecked = false, isEksternaMemorija = true });
                    if (!sameOS)
                        specifikacijeList.Add(new SpecifikacijeVM { OperativniSistemId = x.Id, OperativniSistemNaziv = x.Specifikacije.OperativniSistem, isOperativniSistemChecked = false, isOperativniSistem = true });
                    if (!sameEkran)
                        specifikacijeList.Add(new SpecifikacijeVM { EkranId = x.Id, EkranNaziv = x.Specifikacije.Ekran, isEkranChecked = false, isEkran = true });


                    //if (!sameRam)
                    //    specifikacijeList.Add(new SpecifikacijeVM { RamId = x.Id, RamNaziv = x.Specifikacije.RAM, ISCHECKEDUAAAAAAAAAAAAAAAAAAAAA = false, isRam = true });
                    //if (!sameMemory)
                    //    specifikacijeList.Add(new SpecifikacijeVM { EksternaMemorijaId = x.Id, EksternaMemorijaNaziv = x.Specifikacije.EksternaMemorija, ISCHECKEDUAAAAAAAAAAAAAAAAAAAAA = false, isEksternaMemorija = true });
                    //if (!sameOS)
                    //    specifikacijeList.Add(new SpecifikacijeVM { OperativniSistemId = x.Id, OperativniSistemNaziv = x.Specifikacije.OperativniSistem, ISCHECKEDUAAAAAAAAAAAAAAAAAAAAA = false, isOperativniSistem = true });
                    //if (!sameEkran)
                    //    specifikacijeList.Add(new SpecifikacijeVM { EkranId = x.Id, EkranNaziv = x.Specifikacije.Ekran, ISCHECKEDUAAAAAAAAAAAAAAAAAAAAA = false, isEkran = true });
                }
            } 
            SpecifikacijeList listaspec = new SpecifikacijeList(); 
            listaspec.specifikacijeList = specifikacijeList;
            return View("Specifikacije",listaspec);
        }
        [HttpPost]
        public ActionResult IspisiSpecifikacije(SpecifikacijeList spec)
        { 
            SpecifikacijeList Model = new SpecifikacijeList();
            Model.listaArtikala = new List<Artikal>();
            List<Artikal> tempList = new List<Artikal>();
            foreach (var item in spec.specifikacijeList.ToList())
            { 
                if (item.isEkranChecked || item.isEksternaMemorijaChecked || item.isRamChecked && item.isOperativniSistemChecked)
                {
                    tempList=ctx.Artikli.Where(x=>x.Specifikacije.RAM==item.RamNaziv).ToList();
                    Model.listaArtikala.AddRange(tempList);
                } 
            } 
            return View("IspisPoFilteru", Model);
        }
    } 
} 