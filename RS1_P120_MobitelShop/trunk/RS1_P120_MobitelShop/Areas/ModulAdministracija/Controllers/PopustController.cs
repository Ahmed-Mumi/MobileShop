using RS1_P120_MobitelShop.Areas.ModulAdministracija.Models;
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
    public class PopustController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulAdministracija/Popust
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Prikazi(int id)
        {
            PopustPrikaziVM Model = new PopustPrikaziVM()
            {
                popustNovi = ctx.Popusti.Where(x=> x.ArtikalId == id)
                .Select(x => new PopustPrikaziVM.PopustInfo()
                {
                    Id = x.ArtikalId,
                    iznosPopusta = x.IznosPopusta
                }).ToList()
            };
            
            return View("Prikazi", Model);
        }
    }
}