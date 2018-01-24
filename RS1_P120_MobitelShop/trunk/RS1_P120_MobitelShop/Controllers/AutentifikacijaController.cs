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
namespace RS1_P120_MobitelShop.Controllers
{
    public class AutentifikacijaController : Controller
    { 
        MojContext ctx = new MojContext();
        bool Poruka = false;
        [HttpGet]
        public ActionResult Index(string poruka)
        {
            if (Poruka)
            {
                ViewData["Confirm"] = "Confirm";
                Poruka = false;
            }

            return View();
        }
       
        public ActionResult Provjera(string email, string password,string zapamti)
        {
            Korisnik korisnik = ctx.Korisnici
                .Include(x => x.Administrator)
                .Include(x => x.Klijent)
                .SingleOrDefault(x => x.Email == email && x.Login.Password == password);

                if(korisnik == null)
                {
                    return RedirectToAction("Index");
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
                    Autentifikacija.PokreniNovuSesiju(k, HttpContext, (zapamti == "on"));
                    return RedirectToAction("Index", "Home");
                  

                }
                else if(!k.Login.IsValid)
                {
                    return View();
           
                } 
            }
            return RedirectToAction("Index", "Home");
       

           
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
            //Model.gradoviStavke = ucitajGradove();
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
                //klijent.Korisnik.Ime = vm.Ime;
                klijent.Korisnik.Login.Username = vm.Username;
                klijent.Korisnik.Login.Password = vm.Password;
                //ovaj dio dodajem
                klijent.Korisnik.Login.IsValid = false;
                //klijent.Korisnik.Prezime = vm.Prezime;
                //klijent.Korisnik.Telefon = vm.Telefon;
                //klijent.Korisnik.Adresa = vm.Adresa;
                //klijent.Korisnik.DatumRodjenja = Convert.ToDateTime(vm.DatumRodjenja);
                klijent.Korisnik.Email = vm.Email;
                //klijent.Korisnik.GradId = vm.GradId;
                ctx.SaveChanges();
                //ovaj dio dodajem
                BuildEmailTemplate(klijent.Korisnik.LoginId);
                Poruka = true;
            }
            else
            {
                ViewData["Message"] = "Success";
                return View("Registration", vm);
            }

            return Redirect("/Autentifikacija");
        }

        //ovaj dio dodajem
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
            BuildEmailTemplate("Your account is successfully created", body, regInfo.Email);
        }

        private void BuildEmailTemplate(string subjectText, string bodyText, string sendTo)
        {
            string from, to, bcc, cc, subject, body;
            from = "mobishopcenter@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = subjectText;
            StringBuilder sb = new StringBuilder();
            sb.Append(bodyText);
            body = sb.ToString();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }
            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SendEmail(mail);
        }
        public static void SendEmail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("mobishopcenter@gmail.com", "MobiShopCenter123");
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}