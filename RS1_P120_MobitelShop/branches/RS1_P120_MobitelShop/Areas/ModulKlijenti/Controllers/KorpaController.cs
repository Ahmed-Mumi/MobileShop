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

        public ActionResult PretraziPoRamu(string CourseLevel)
        {
            List<Artikal> premaraum = new List<Artikal>();

            return View("Artikli");
        }
    } 
}