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
            Artikal art = ctx.Artikli.FirstOrDefault();
            return View(art);
        }
	}
}