using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RS1_P120_MobitelShop.Helper;
using System.Data.Entity;
using RS1_P120_MobitelShop.DAL;
using RS1_P120_MobitelShop.Models;

namespace RS1_P120_MobitelShop.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        MojContext ctx = new MojContext();
        
         public ActionResult Index()
        {
            Korisnik k = Autentifikacija.GetLogiraniKorisnik(HttpContext);

            if (k == null)
                return RedirectToAction("Logout", "Autentifikacija", new { area = "" });

            if (k.Administrator != null)
                return RedirectToAction("Index", "Home", new { area = "ModulAdministracija" });

            if (k.Klijent != null)
                return RedirectToAction("Index", "Home", new { area = "ModulKlijenti" });

            return RedirectToAction("Logout", "Autenficikacija", new { area = "" });
        }  
    }
}