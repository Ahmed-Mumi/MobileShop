using RS1_P120_MobitelShop.Helper;
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
            List<SpecifikacijeVM> specifikacijeLista = new List<SpecifikacijeVM>();
            foreach (var x in ctx.Artikli)
            {
                if (specifikacijeLista.Count == 0)
                    specifikacijeLista.Add(new SpecifikacijeVM { RamId = x.Id, RamNaziv = x.Specifikacije.RAM, isChecked = false });
                else
                {
                    bool ima=false;
                    foreach (var p in specifikacijeLista.ToList())
                    {
                        if (p.RamNaziv == x.Specifikacije.RAM)
                            ima = true;  
                    }
                    if(!ima)
                        specifikacijeLista.Add(new SpecifikacijeVM { RamId = x.Id, RamNaziv = x.Specifikacije.RAM, isChecked = false }); 
                }
            }
            //specifikacijeLista.Add(new SpecifikacijeVM { RamId = 1, RamNaziv = "3 GB", isChecked = false });
            //specifikacijeLista.Add(new SpecifikacijeVM { RamId = 2, RamNaziv = "4 GB", isChecked = false });
            SpecifikacijeList listaspec = new SpecifikacijeList();
            listaspec.specifikacije = specifikacijeLista;
            return View("Specifikacije",listaspec);
        }
        [HttpPost]
        public ActionResult IspisiSpecifikacije(SpecifikacijeList spec)
        { 
            SpecifikacijeList Model = new SpecifikacijeList();
            foreach (var item in spec.specifikacije)
            { 
                if (item.isChecked)
                { 
                    Model.listaArtikala.AddRange(ctx.Artikli.Where(x => x.Specifikacije.RAM == item.RamNaziv).ToList()); 
                } 

            }
            return View("IspisPoFilteru", Model);
        }
    } 
} 
 