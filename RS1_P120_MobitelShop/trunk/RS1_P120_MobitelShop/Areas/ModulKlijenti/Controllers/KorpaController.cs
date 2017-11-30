using RS1_P120_MobitelShop.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RS1_P120_MobitelShop.Areas.ModulKlijenti.Controllers
{
    public class KorpaController : Controller
    {
        // GET: ModulKlijenti/Korpa
        [Autorizacija(KorisnickeUloge.Klijent)]
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}