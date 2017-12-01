using RS1_P120_MobitelShop.Areas.ModulAdministracija.ViewModels;
using RS1_P120_MobitelShop.DAL;
using RS1_P120_MobitelShop.Helper;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RS1_P120_MobitelShop.Areas.ModulAdministracija.Controllers
{
        [Autorizacija(KorisnickeUloge.Administrator)]
    public class AdminHomeController : Controller
    {
        // GET: ModulAdministracija/AdminHome

        public static int Administratori { get; set; }
        public static int Klijenti { get; set; }
        public static int Dobavljaci { get; set; }
        public static int Artikli { get; set; }

        MojContext ctx = new MojContext();
        public ActionResult Index()
        {
            Administratori = ctx.Administratori.Count();
            Klijenti = ctx.Klijenti.Count();
            Dobavljaci = ctx.Dobavljaci.Count();
            Artikli = ctx.Artikli.Count();

            ViewBag.Administratori = Administratori;
            ViewBag.Klijenti = Klijenti;
            ViewBag.Dobavljaci = Dobavljaci;
            ViewBag.Artikli = Artikli;
            return View("Index");
        }
        public ActionResult UpravljackaPloca()
        {
            return View();
        }
    }
}