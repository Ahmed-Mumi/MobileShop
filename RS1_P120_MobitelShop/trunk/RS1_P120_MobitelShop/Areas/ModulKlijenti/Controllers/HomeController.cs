using RS1_P120_MobitelShop.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RS1_P120_MobitelShop.DAL;
using RS1_P120_MobitelShop.Models;
using PagedList.Mvc;
using PagedList;
using RS1_P120_MobitelShop.Areas.ModulKlijenti.ViewModel;

namespace RS1_P120_MobitelShop.Areas.ModulKlijenti.Controllers
{
    public class HomeController : Controller
    {
        MojContext ctx = new MojContext();
        HomeIndexVM ModelHomeIndex = new HomeIndexVM();

        public ActionResult Index(int? ArtikalId, int? page, string searchTerm)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);

            ModelHomeIndex.listaNajnovijihArtikala = ctx.Popusti.OrderByDescending(x => x.Artikal.DatumObjave).Select(p => new HomeIndexRow()
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
                CijenaSaPopustom = ((100 - p.IznosPopusta) * p.Artikal.Cijena) / 100
            }).Take(4).ToList(); 
            if (string.IsNullOrEmpty(searchTerm))
            {
                ModelHomeIndex.listaArtikalaPoSearch = ctx.Artikli.OrderBy(x => x.Id).ToPagedList(pageNumber, pageSize);
            }
            else
            {
                ModelHomeIndex.listaArtikalaPoSearch = ctx.Artikli.Where(x => x.Model.Contains(searchTerm)).OrderBy(x => x.Id).ToPagedList(pageNumber, pageSize);
            }
            Korisnik k = Autentifikacija.GetLogiraniKorisnik(HttpContext);
            if (k != null)
                ModelHomeIndex.BrojArtikalaUKorpi = ctx.Korpe.Count(x => x.KlijentId == k.Id);
            ModelHomeIndex.Korisnik = Autentifikacija.GetLogiraniKorisnik(HttpContext);
            return View("Index", ModelHomeIndex);
        }  
        public JsonResult GetStudents(string term)
        {
            ModelHomeIndex.searchArtikliString = ctx.Artikli.Where(h => h.Model.Contains(term)).Select(y => y.Model).ToList();
            return Json(ModelHomeIndex.searchArtikliString, JsonRequestBehavior.AllowGet);
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
            return View("Specifikacije", listaspec);
        }
        [HttpPost]
        public ActionResult IspisiSpecifikacije(SpecifikacijeList spec)
        {
            SpecifikacijeList Model = new SpecifikacijeList();
            Model.listaArtikala = new List<Artikal>();
            List<Artikal> tempList = new List<Artikal>();
            List<Artikal> temptempList = new List<Artikal>();
            //for (int i = 0; i < spec.specifikacijeList.ToList().Count; i++)
            //{
            //    if(spec.specifikacijeList[i].isEkranChecked || spec.specifikacijeList[i].isEksternaMemorijaChecked || spec.specifikacijeList[i].isRamChecked || spec.specifikacijeList[i].isOperativniSistemChecked){

            //        for (int j = i+1; j < spec.specifikacijeList.ToList().Count; j++)
            //        {

            //        }

            //    }
            //}
            //foreach (var item in spec.specifikacijeList.ToList())
            //{
            //    if (item.isEkranChecked || item.isEksternaMemorijaChecked || item.isRamChecked || item.isOperativniSistemChecked)
            //    {
            //        tempList = ctx.Artikli.Where(x => x.Specifikacije.RAM == item.RamNaziv).ToList();
            //        Model.listaArtikala.AddRange(tempList);
            //    }  
            //}
            string ram=null, memorija=null, ekran=null, operat=null;
            foreach(var x in spec.specifikacijeList.ToList())
            {
                if (x.isEkranChecked)
                    ekran = x.EkranNaziv;
                else if (x.isEksternaMemorijaChecked)
                    memorija = x.EksternaMemorijaNaziv;
                else if (x.isRamChecked)
                    ram = x.RamNaziv;
                else
                    operat = x.OperativniSistemNaziv;
            }
            Model.listaArtikala = ctx.Artikli.Where(x => (x.Specifikacije.Ekran == ekran && ekran!=null) && (x.Specifikacije.EksternaMemorija==memorija && memorija!=null)
                                                && (x.Specifikacije.RAM==ram && ram!=null) && (x.Specifikacije.OperativniSistem==operat && operat!=null)).ToList();
            return View("IspisPoFilteru", Model);
        }
    }
}