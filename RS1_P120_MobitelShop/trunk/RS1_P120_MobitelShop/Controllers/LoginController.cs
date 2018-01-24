using PagedList;
using RS1_P120_MobitelShop.Areas.ModulKlijenti.ViewModel;
using RS1_P120_MobitelShop.DAL;
using RS1_P120_MobitelShop.Helper;
using RS1_P120_MobitelShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RS1_P120_MobitelShop.Controllers
{
    public class LoginController : Controller
    {
        // GET: ModulKlijenti/Korpa
        MojContext ctx = new MojContext();

        public ActionResult Index()
        {
            Korisnik k = Autentifikacija.GetLogiraniKorisnik(HttpContext);

            if (k == null)
                return RedirectToAction("Logout", "Autentifikacija", new { area = "" });

            if (k.Administrator != null && k.Login.IsValid == true)
                return RedirectToAction("Index", "Home", new { area = "ModulAdministracija" });

            if (k.Klijent != null && k.Login.IsValid == true)
                return RedirectToAction("Index", "Home", new { area = "" });

            return RedirectToAction("Logout", "Autentifikacija", new { area = "" });
        }
    }
}