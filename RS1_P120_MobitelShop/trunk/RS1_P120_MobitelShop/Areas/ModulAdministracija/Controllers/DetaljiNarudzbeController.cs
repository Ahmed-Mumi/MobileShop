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
    public class DetaljiNarudzbeController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulAdministracija/DetaljiNarudzbe
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Prikazi(int id)
        {
            DetaljiNarudzbePrikaziVM Model = new DetaljiNarudzbePrikaziVM
            {
                detaljiStavke = ctx.DetaljiNarudzbi.Where(x=> x.NarudzbaId == id)
                .Select(x=> new DetaljiNarudzbePrikaziVM.DetaljiInfo()
                {
                    Id = x.Id,
                    ArtikalId = x.ArtikalId,
                    Kolicina = x.Kolicina,
                    artikalNaziv = x.Artikal.Model,
                    //NarudzbaId = x.NarudzbaId,
                }).ToList(),
                NarudzbaId = id               
        };
            return View("Prikazi",Model);
        }
        //public ActionResult Detalji(int id)
        //{
        //    return View("Detalji", Model);
        //}
    }
}