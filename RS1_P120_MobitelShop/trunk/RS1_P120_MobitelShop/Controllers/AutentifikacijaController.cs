using RS1_P120_MobitelShop.DAL;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RS1_P120_MobitelShop.Models;
using RS1_P120_MobitelShop.Helper;
using RS1_P120_MobitelShop.ViewModel;
using System.Net.Mail;
using System.Text;
using System.Web.Hosting;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Google.Authenticator;

namespace RS1_P120_MobitelShop.Controllers
{
    public class AutentifikacijaController : Controller
    { 
        MojContext ctx = new MojContext(); 
        [HttpGet]
        public ActionResult Index()
        {
             return View();
        }
       
        public ActionResult Provjera(string email, string password,string zapamti)
        {
            Korisnik korisnik = ctx.Korisnici
                .Include(x => x.Administrator)
                .Include(x => x.Klijent)
                .SingleOrDefault(x => x.Email == email && x.Login.Password == password);

                if(korisnik == null && korisnik.Klijent.Korisnik.isBanned == true)
                {
                return RedirectToAction("Index", "Home", new { area = "" }); 
            }

            Autentifikacija.PokreniNovuSesiju(korisnik,HttpContext,(zapamti =="on"));

            Korisnik k = Autentifikacija.GetLogiraniKorisnik(HttpContext);

            if (k.Administrator != null  && k.Klijent == null)
            {
                Autentifikacija.PokreniNovuSesiju(k, HttpContext, (zapamti == "on"));
                return Redirect("/ModulAdministracija/AdminHome");
            } 
            else if (k.Administrator == null && k.Klijent != null)
            {
                Logout(); 
                if (k.Login.IsValid == true)
                {
                    //mozda da ne pokrece dok se ne verifikuje
                    string key = System.Guid.NewGuid().ToString().Substring(0, 12);
                    string message = "";
                    bool status = false;
                    KorisnikVM kor = new KorisnikVM()
                    {
                        Korisnik = korisnik,
                        Zapamti = zapamti,
                        Email = korisnik.Email,
                        Password = korisnik.Login.Password,
                        Url = ControllerContext.HttpContext.Request.UrlReferrer.ToString()
                    };
                    status = true;
                    message = "2FA Verification";
                    Session["Email"] = k.Email;
                    TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                    string UserUniqueKey = k.Email + key;
                    Session["UserUniqueKey"] = UserUniqueKey;
                    var setupInfo = tfa.GenerateSetupCode("MobiShop", k.Email, UserUniqueKey, 300, 300);
                    ViewBag.BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
                    ViewBag.SetupCode = setupInfo.ManualEntryKey;
                    ViewBag.Message = message;
                    ViewBag.Status = status;
                    return View("TwoFactorAuth", kor);
                    //return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());

                }
                else if(!k.Login.IsValid)
                {
                    return RedirectToAction("Index", "Home", new { area = "" }); 
                }
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }


        public ActionResult Verfiy2FA(KorisnikVM k)
        {
            var token = Request["passcode"];
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            string UserUniqueKey = Session["UserUniqueKey"].ToString();
            bool isValid = tfa.ValidateTwoFactorPIN(UserUniqueKey, token);
            if (isValid)
            {
                Korisnik korisnik = ctx.Korisnici
                .Include(x => x.Administrator)
                .Include(x => x.Klijent)
                .SingleOrDefault(x => x.Email == k.Email && x.Login.Password == k.Password);
                Autentifikacija.PokreniNovuSesiju(korisnik, HttpContext, (k.Zapamti == "on")); 
                Session["IsValid2FA"] = true;
                return Redirect(k.Url);
                //return RedirectToAction("Index", "Home", new { area = "" });

            }
            return RedirectToAction("Index", "Home", new { area = "" });        
        }

        public ActionResult Logout()
        {
            Autentifikacija.PokreniNovuSesiju(null, HttpContext, true);
            return RedirectToAction("Index","Home");
        } 
        public ActionResult Dodaj()
        {
            Klijent klijent;
            klijent = new Klijent();
            klijent.Korisnik = new Korisnik();
            klijent.Korisnik.Login = new Login();

            RegistrationVM Model = new RegistrationVM();
            Model.Klijent = klijent;
            return View("Registration",Model);
        } 
        private List<SelectListItem> ucitajGradove()
        {
            var gradovi = new List<SelectListItem>();
            gradovi.Add(new SelectListItem { Value = null, Text = "Izaberite grad" });
            gradovi.AddRange(ctx.Gradovi.Select(x=> new SelectListItem{Value = x.Id.ToString(),Text = x.Naziv}));
            return gradovi;
        } 
        public ActionResult Registration(RegistrationVM vm)
        {
            var response = Request["g-recaptcha-response"];
            string secretKey = "6LcfskAUAAAAAMQeZJc_3mXI7rtcWZjeNTr5a9j8";//OVDJE SECRET KEY
            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");  
            if (!ModelState.IsValid || !status)
            {
                return View("Registration", vm);
            } 
            Korisnik korisnik = ctx.Korisnici.Where(x => x.Email == vm.Email).FirstOrDefault();
            if (korisnik == null) {
                Klijent klijent = new Klijent();
                klijent.Korisnik = new Korisnik();
                klijent.Korisnik.Login = new Login();
                ctx.Klijenti.Add(klijent);   
                klijent.Korisnik.Login.Password = vm.Password;
                klijent.Korisnik.Login.IsValid = false;
                klijent.Korisnik.Email = vm.Email;
                ctx.SaveChanges();
                BuildEmailTemplate(klijent.Korisnik.LoginId); 
            }
            else
            {
                ViewData["Message"] = "Success";
                return View("Registration", vm);
            }
            KorisnikVM Kor = new KorisnikVM() { Korisnik = korisnik };
            return View("Index",Kor);
        } 
        public ActionResult Confirm(int loginId)
        {
            ViewBag.loginId = loginId;
            return View();
        } 
        public JsonResult RegisterConfirm(int loginId)
        {
            Login Login = ctx.Logini.Where(x => x.Id == loginId).FirstOrDefault();
            Login.IsValid = true;
            ctx.SaveChanges();
            var msg = "Your login is verified";
            return Json(msg, JsonRequestBehavior.AllowGet);
        } 
        public void BuildEmailTemplate(int loginId)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "Text" + ".cshtml");
            var regInfo = ctx.Korisnici.Where(x => x.LoginId == loginId).FirstOrDefault();
            var url = "http://localhost:53235/" + "Autentifikacija/Confirm?loginId=" + loginId;
            body = body.Replace("@ViewBag.ConfirmationLink", url);
            body = body.ToString();
            PosaljiEmail.BuildEmailTemplate("Your account is successfully created", body, regInfo.Email);
        }
    }
}