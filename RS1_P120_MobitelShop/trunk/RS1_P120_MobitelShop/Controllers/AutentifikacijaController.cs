using RS1_P120_MobitelShop.DAL;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RS1_P120_MobitelShop.Models;
using RS1_P120_MobitelShop.Helper;

namespace RS1_P120_MobitelShop.Controllers
{
    public class AutentifikacijaController : Controller
    {
        // GET: Autentifikacija
        MojContext ctx = new MojContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Provjera(string username,string password,string zapamti)
        {
            Korisnik korisnik = ctx.Korisnici
                .Include(x => x.Administrator)
                .Include(x => x.Klijent)
                .SingleOrDefault(x => x.Login.Username == username && x.Login.Password == password);

                if(korisnik == null)
                {
                    return RedirectToAction("Index");
                }

            Autentifikacija.PokreniNovuSesiju(korisnik,HttpContext,(zapamti =="on"));

            return RedirectToAction("Index","Home");          
        }
        public ActionResult Logout()
        {
            Autentifikacija.PokreniNovuSesiju(null, HttpContext, true);
            return RedirectToAction("Index");
        }
    }
}