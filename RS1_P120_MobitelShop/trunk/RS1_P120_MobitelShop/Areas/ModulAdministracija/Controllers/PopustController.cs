using RS1_P120_MobitelShop.Areas.ModulAdministracija.Models;
using RS1_P120_MobitelShop.DAL;
using RS1_P120_MobitelShop.Helper;
using RS1_P120_MobitelShop.Models;
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
                    PopustId = x.Id,
                    modelName = x.Artikal.Model,
                    iznosPopusta = x.IznosPopusta,
                }).ToList(),
                ArtikalId = id
            };          
            return View("Prikazi", Model);
        }
        public ActionResult Dodaj()
        {
           
            PopustEditVM Model = new PopustEditVM();
            //Popust p = ctx.Popusti.Where(x=> x.ArtikalId == Model.ArtikalId).FirstOrDefault();
          
            Model.artikli = ucitajArtikle();       
            Model.datumOd = DateTime.Now;
            return View("Edit", Model);
        }
        public ActionResult Obrisi(int id)
        {
            Popust p = ctx.Popusti.Find(id);
            ctx.Popusti.Remove(p);
            ctx.SaveChanges();
            return RedirectToAction("Prikazi","Artikal",new { area = "ModulAdministracija" });
        }
        public ActionResult Snimi(PopustEditVM vm)
        {
            Popust popust;

            if (vm.Id == 0)
            {
                popust = new Popust();

                ctx.Popusti.Add(popust);
            }
            else
            {
                popust = ctx.Popusti.Where(x => x.Id == vm.ArtikalId).FirstOrDefault();
            }
           
            popust.ArtikalId = vm.ArtikalId;
            popust.StartDate = vm.datumOd;
            popust.EndDate = vm.datumDo;
            popust.IznosPopusta = vm.iznosPopusta;
            ctx.SaveChanges();
            return RedirectToAction("Prikazi", "Artikal", new { area = "ModulAdministracija" , id = vm.ArtikalId });
        }
        private List<SelectListItem> ucitajArtikle()
        {
            var artikli = new List<SelectListItem>();
            artikli.AddRange(ctx.Artikli.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Model }));
            return artikli;
        }
    }
}