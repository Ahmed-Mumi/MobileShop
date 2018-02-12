using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RS1_P120_MobitelShop.Helper;
using System.Data.Entity;
using RS1_P120_MobitelShop.DAL;
using RS1_P120_MobitelShop.Models;
using RS1_P120_MobitelShop.ViewModel;
using PagedList;
using RS1_P120_MobitelShop.Areas.ModulKlijenti.ViewModel;
using PagedList.Mvc;


namespace RS1_P120_MobitelShop.Controllers
{
    public class HomeController : Controller
    {
        MojContext ctx = new MojContext();
        PocetnaIndexVM ModelHomeIndex = new PocetnaIndexVM();
        ArtikliSpecifikacijeVM ModelArtikalDetalji = new ArtikliSpecifikacijeVM();
        public ActionResult Index(int? ArtikalId, int? page, string searchTerm, PocetnaIndexVM spec)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            ModelHomeIndex.listaNajnovijihArtikala = ctx.Popusti.OrderByDescending(x => x.Artikal.DatumObjave).Select(p => new PocetnaIndexRow()
            {
                Slika = p.Artikal.Slika,
                Model = p.Artikal.Model,
                Marka = p.Artikal.Marka,
                Cijena = p.Artikal.Cijena,
                ArtikalId = p.Artikal.Id,
                Ekran = p.Artikal.Specifikacije.Ekran,
                Kamera = p.Artikal.Specifikacije.Kamera,
                OperativniSistem = p.Artikal.Specifikacije.OperativniSistem,
                Popust = p.IznosPopusta,
                VrstaEkrana = p.Artikal.Specifikacije.VrstaEkrana,
                CijenaSaPopustom = ((100 - p.IznosPopusta) * (int)Math.Round(p.Artikal.Cijena)) / 100
            }).Take(4).ToList();
            if (spec.specifikacijeList != null)
            {
                List<Artikal> temp = RezultatiFiltera(spec);
                ModelHomeIndex.listaArtikalaPoSearch = temp.OrderBy(x => x.Id).ToPagedList(pageNumber, pageSize);
            }
            else if (string.IsNullOrEmpty(searchTerm))
            {
                ModelHomeIndex.listaArtikalaPoSearch = ctx.Artikli.OrderBy(x => x.Id).ToPagedList(pageNumber, pageSize);
            }
            else
            {
                ModelHomeIndex.listaArtikalaPoSearch = ctx.Artikli.Where(x => x.Model.Contains(searchTerm)).OrderBy(x => x.Id).ToPagedList(pageNumber, pageSize);
            }
            //ovo dodo samo nista vise
            if (ModelHomeIndex.listaArtikalaPoSearch.Count == 1)
            {
                return RedirectToAction("Detalji", "Uporedi", new { area = "ModulKlijenti", artikalId = ModelHomeIndex.listaArtikalaPoSearch.FirstOrDefault().Id });
            }
            Korisnik k = Autentifikacija.GetLogiraniKorisnik(HttpContext);
            if (k != null)
                ModelHomeIndex.BrojArtikalaUKorpi = ctx.Korpe.Count(x => x.KlijentId == k.Id);
            ModelHomeIndex.Korisnik = Autentifikacija.GetLogiraniKorisnik(HttpContext);
            ModelHomeIndex.specifikacijeList = IspisiSpecifikacije();


            ModelHomeIndex.cijenaOd = spec.cijenaOd;
            ModelHomeIndex.cijenaDo = spec.cijenaDo;
            ModelHomeIndex.Obavijesti = ctx.Obavijesti.OrderByDescending(x => x.DatumObavijesti).Take(6).ToList();
            return View("Index", ModelHomeIndex);
        }

        public JsonResult GetStudents(string term)
        {
            ModelHomeIndex.searchArtikliString = ctx.Artikli.Where(h => h.Model.Contains(term)).Select(y => y.Model).ToList();
            return Json(ModelHomeIndex.searchArtikliString, JsonRequestBehavior.AllowGet);
        }
        public List<SpecVM> IspisiSpecifikacije()
        {
            List<SpecVM> specifikacijeList = new List<SpecVM>();
            foreach (var x in ctx.Artikli)
            {
                if (specifikacijeList.Count == 0)
                {
                    specifikacijeList.Add(new SpecVM { RamId = x.Id, RamNaziv = x.Specifikacije.RAM, isRamChecked = false, isRam = true });
                    specifikacijeList.Add(new SpecVM { KameraId = x.Id, KameraNaziv = x.Specifikacije.Kamera, isKameraChecked = false, isKamera = true });
                    specifikacijeList.Add(new SpecVM { MarkaId = x.Id, MarkaNaziv = x.Marka, isMarkaChecked = false, isMarka = true });
                    specifikacijeList.Add(new SpecVM { EkranId = x.Id, EkranNaziv = x.Specifikacije.Ekran, isEkranChecked = false, isEkran = true });
                }
                else
                {
                    bool sameRam = false, sameKamera = false, sameMarka = false, sameEkran = false;
                    foreach (var p in specifikacijeList.ToList())
                    {
                        if (p.RamNaziv == x.Specifikacije.RAM)
                            sameRam = true;
                        if (p.KameraNaziv == x.Specifikacije.Kamera)
                            sameKamera = true;
                        if (p.MarkaNaziv == x.Marka)
                            sameMarka = true;
                        if (p.EkranNaziv == x.Specifikacije.Ekran)
                            sameEkran = true;
                    }
                    if (!sameRam)
                        specifikacijeList.Add(new SpecVM { RamId = x.Id, RamNaziv = x.Specifikacije.RAM, isRamChecked = false, isRam = true });
                    if (!sameKamera)
                        specifikacijeList.Add(new SpecVM { KameraId = x.Id, KameraNaziv = x.Specifikacije.Kamera, isKameraChecked = false, isKamera = true });
                    if (!sameMarka)
                        specifikacijeList.Add(new SpecVM { MarkaId = x.Id, MarkaNaziv = x.Marka, isMarkaChecked = false, isMarka = true });
                    if (!sameEkran)
                        specifikacijeList.Add(new SpecVM { EkranId = x.Id, EkranNaziv = x.Specifikacije.Ekran, isEkranChecked = false, isEkran = true });
                }
            }
            return specifikacijeList;
        }
        [HttpPost]
        public List<Artikal> RezultatiFiltera(PocetnaIndexVM spec)
        {
            List<Artikal> tempList = new List<Artikal>();
            List<Artikal> tempList2 = new List<Artikal>();
            List<Artikal> tempList3 = new List<Artikal>();
            tempList3 = ctx.Artikli.ToList();
            tempList = ctx.Artikli.ToList();
            string ram = null, kamera = null, ekran = null, marka = null;
            foreach (var x in spec.specifikacijeList.ToList())
            {
                tempList2 = tempList3;
                if (x.isRamChecked)
                {
                    if (ram == null)
                    {
                        tempList = tempList.Where(p => p.Specifikacije.RAM == x.RamNaziv).ToList();
                        ram = "ram";
                    }
                    else
                    {
                        tempList2 = tempList2.Where(p => p.Specifikacije.RAM == x.RamNaziv).ToList();
                        tempList2.AddRange(tempList);
                        tempList = VisestrukaPodKategorija(tempList2, spec, ram);
                    }
                }
                else if (x.isKameraChecked)
                {
                    if (kamera == null)
                    {
                        tempList = tempList.Where(p => p.Specifikacije.Kamera == x.KameraNaziv).ToList();
                        kamera = "kamera";
                    }
                    else
                    {
                        tempList2 = tempList2.Where(p => p.Specifikacije.Kamera == x.KameraNaziv).ToList();
                        tempList2.AddRange(tempList);
                        tempList = VisestrukaPodKategorija(tempList2, spec, kamera);
                    }
                }
                else if (x.isEkranChecked)
                {
                    if (ekran == null)
                    {
                        tempList = tempList.Where(p => p.Specifikacije.Ekran == x.EkranNaziv).ToList();
                        ekran = "ekran";
                    }
                    else
                    {
                        tempList2 = tempList2.Where(p => p.Specifikacije.Ekran == x.EkranNaziv).ToList();
                        tempList2.AddRange(tempList);
                        tempList = VisestrukaPodKategorija(tempList2, spec, ekran);
                    }
                }
                else if (x.isMarkaChecked)
                {
                    if (marka == null)
                    {
                        tempList = tempList.Where(p => p.Marka == x.MarkaNaziv).ToList();
                        marka = "marka";
                    }
                    else
                    {
                        tempList2 = tempList2.Where(p => p.Marka == x.MarkaNaziv).ToList();
                        tempList2.AddRange(tempList);
                        tempList = VisestrukaPodKategorija(tempList2, spec, marka);
                    }
                }
            }

            //dodano
            return tempList.Where(x => x.Cijena > spec.cijenaOd && x.Cijena < spec.cijenaDo).ToList();
        }

        public List<Artikal> VisestrukaPodKategorija(List<Artikal> tempList2, PocetnaIndexVM spec, string specifikacija)
        {
            foreach (var x in spec.specifikacijeList.ToList())
            {
                if (x.isRamChecked)
                {
                    if (specifikacija != "ram")
                        tempList2 = tempList2.Where(p => p.Specifikacije.RAM == x.RamNaziv).ToList();
                }
                else if (x.isKameraChecked)
                {
                    if (specifikacija != "kamera")
                        tempList2 = tempList2.Where(p => p.Specifikacije.Kamera == x.KameraNaziv).ToList();
                }
                else if (x.isEkranChecked)
                {
                    if (specifikacija != "ekran")
                        tempList2 = tempList2.Where(p => p.Specifikacije.Ekran == x.EkranNaziv).ToList();
                }
                else if (x.isMarkaChecked)
                {
                    if (specifikacija != "marka")
                        tempList2 = tempList2.Where(p => p.Marka == x.MarkaNaziv).ToList();
                }
            }
            return tempList2;
        }
    }
}