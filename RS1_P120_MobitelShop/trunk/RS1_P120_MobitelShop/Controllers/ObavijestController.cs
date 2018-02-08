using RS1_P120_MobitelShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RS1_P120_MobitelShop.Models;

namespace RS1_P120_MobitelShop.Areas.ModulKlijenti.Controllers
{
    public class ObavijestController : Controller
    {
        MojContext ctx = new MojContext();
        public ActionResult Index(int ObavijestId)
        {
            Obavijest Obavijest = ctx.Obavijesti.Where(x => x.Id == ObavijestId).FirstOrDefault();
            return View("Obavijest", Obavijest);
        }
    }
}