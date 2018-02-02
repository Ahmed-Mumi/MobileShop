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

            ModelHomeIndex.listaNajnovijihArtikala=ctx.Popusti.OrderByDescending(x=>x.Artikal.DatumObjave).Select(p => new PocetnaIndexRow()
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
                CijenaSaPopustom = ((100-p.IznosPopusta)*p.Artikal.Cijena)/100
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
            Korisnik k = Autentifikacija.GetLogiraniKorisnik(HttpContext);
            if(k!=null)
                ModelHomeIndex.BrojArtikalaUKorpi = ctx.Korpe.Count(x => x.KlijentId == k.Id);
            ModelHomeIndex.Korisnik = Autentifikacija.GetLogiraniKorisnik(HttpContext);
            ModelHomeIndex.specifikacijeList = IspisiSpecifikacije();

            ModelHomeIndex.Obavijesti = ctx.Obavijesti.OrderByDescending(x => x.DatumObavijesti).Take(5).ToList();
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
                    specifikacijeList.Add(new SpecVM { EksternaMemorijaId = x.Id, EksternaMemorijaNaziv = x.Specifikacije.EksternaMemorija, isEksternaMemorijaChecked = false, isEksternaMemorija = true });
                    specifikacijeList.Add(new SpecVM { OperativniSistemId = x.Id, OperativniSistemNaziv = x.Specifikacije.OperativniSistem, isOperativniSistemChecked = false, isOperativniSistem = true });
                    specifikacijeList.Add(new SpecVM { EkranId = x.Id, EkranNaziv = x.Specifikacije.Ekran, isEkranChecked = false, isEkran = true });
                }
                else
                {
                    bool sameRam = false, sameMemory = false, sameOS = false, sameEkran = false;
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
                        specifikacijeList.Add(new SpecVM { RamId = x.Id, RamNaziv = x.Specifikacije.RAM, isRamChecked = false, isRam = true });
                    if (!sameMemory)
                        specifikacijeList.Add(new SpecVM { EksternaMemorijaId = x.Id, EksternaMemorijaNaziv = x.Specifikacije.EksternaMemorija, isEksternaMemorijaChecked = false, isEksternaMemorija = true });
                    if (!sameOS)
                        specifikacijeList.Add(new SpecVM { OperativniSistemId = x.Id, OperativniSistemNaziv = x.Specifikacije.OperativniSistem, isOperativniSistemChecked = false, isOperativniSistem = true });
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
            string ram = null, memorija = null, ekran = null, os = null;
            foreach (var x in spec.specifikacijeList.ToList())
            {
                //Temp2 - ukoliko postoje dva izbora iz iste podkategorije Iphone, Samsung
                //Temp3 - tempList2 se mora ponovo incijalizovati prilikom svake petlje, a to znaci i pristup bazi u svakom krugu petlje, uz temp3
                //        pristupa bazi samo jednom i dodjeljuju temp2 kad je to potrebno
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
                else if (x.isEksternaMemorijaChecked)
                {
                    if (memorija==null)
                    {
                        tempList = tempList.Where(p => p.Specifikacije.EksternaMemorija == x.EksternaMemorijaNaziv).ToList();
                        memorija = "eksternamemorija";
                    }
                    else
                    {
                        tempList2 = tempList2.Where(p => p.Specifikacije.EksternaMemorija == x.EksternaMemorijaNaziv).ToList();
                        tempList2.AddRange(tempList);
                        tempList = VisestrukaPodKategorija(tempList2, spec, memorija); 
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
                        tempList=VisestrukaPodKategorija(tempList2, spec, ekran);
                    }
                }
                else if (x.isOperativniSistemChecked)
                {
                    if (os == null)
                    {
                        tempList = tempList.Where(p => p.Specifikacije.OperativniSistem == x.OperativniSistemNaziv).ToList();
                        os = "operativnisistem";
                    }
                    else
                    {
                        tempList2 = tempList2.Where(p => p.Specifikacije.OperativniSistem == x.OperativniSistemNaziv).ToList();
                        tempList2.AddRange(tempList);
                        tempList = VisestrukaPodKategorija(tempList2, spec, os);
                    }
                } 
            }
            return tempList;
        }

        public List<Artikal> VisestrukaPodKategorija(List<Artikal> tempList2, PocetnaIndexVM spec, string specifikacija)
        {
            foreach(var x in spec.specifikacijeList.ToList())
            {
                if (x.isRamChecked)
                {
                    if (specifikacija != "ram")
                        tempList2 = tempList2.Where(p => p.Specifikacije.RAM == x.RamNaziv).ToList();
                }
                else if(x.isEksternaMemorijaChecked){
                    if (specifikacija != "eksternamemorija")
                        tempList2 = tempList2.Where(p => p.Specifikacije.EksternaMemorija == x.EksternaMemorijaNaziv).ToList();
                }
                else if (x.isEkranChecked)
                {
                    if (specifikacija != "ekran")
                        tempList2 = tempList2.Where(p => p.Specifikacije.Ekran == x.EkranNaziv).ToList();
                }
                else if (x.isOperativniSistemChecked)
                {
                    if (specifikacija != "operativnisistem")
                        tempList2 = tempList2.Where(p => p.Specifikacije.OperativniSistem == x.OperativniSistemNaziv).ToList();
                }
            }
            return tempList2;
        }
    }

}