using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RS1_P120_MobitelShop.Areas.ModulKlijenti.Controllers
{
    public class NarudzbaController : Controller
    {
        // GET: ModulKlijenti/Narudzba
        public ActionResult Index()
        {

            return View("Index");
        }
    }
}