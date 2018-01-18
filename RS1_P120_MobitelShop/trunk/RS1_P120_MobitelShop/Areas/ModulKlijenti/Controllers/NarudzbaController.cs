using RS1_P120_MobitelShop.Areas.ModulKlijenti.ViewModel;
using RS1_P120_MobitelShop.DAL;
using RS1_P120_MobitelShop.Helper;
using RS1_P120_MobitelShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace RS1_P120_MobitelShop.Areas.ModulKlijenti.Controllers
{
    [Autorizacija(KorisnickeUloge.Klijent)]
    public class NarudzbaController : Controller
    {
        MojContext ctx = new MojContext();
        public ActionResult Index()
        {
            Korisnik Korisnik = Autentifikacija.GetLogiraniKorisnik(HttpContext);
            KorpaPrikaziVM Model = new KorpaPrikaziVM()
            {
                ListaKorpa = ctx.Korpe.Where(x => x.Klijent.Korisnik.Id == Korisnik.Id).Select(p => new KorpaPrikaziRow()
                {
                    ModelNaziv=p.Artikal.Marka+" "+p.Artikal.Model,
                    Cijena=p.Artikal.Cijena,
                    KorpaId=p.Id,
                    ArtikalId=p.ArtikalId
                }).ToList(),
                
            };
            return View("Index",Model);
        }

        public ActionResult Ukloni(int id)
        {
            Korpa Korpa = ctx.Korpe.Where(x => x.Id == id).FirstOrDefault();
            ctx.Korpe.Remove(Korpa);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Naruci(int Artikalid,int KorpaId)
        {
            Korpa Korpa = ctx.Korpe.Where(x => x.Id == KorpaId).FirstOrDefault();
            int KlijentId = Korpa.KlijentId;
            ctx.Korpe.Remove(Korpa);
            DetaljiNarudzbe DetaljiNarudzbe = new DetaljiNarudzbe();
            DetaljiNarudzbe.Narudzba = new Narudzba();
            DetaljiNarudzbe.ArtikalId = Artikalid;
            DetaljiNarudzbe.Narudzba.DatumNarudzbe = DateTime.Now;
            DetaljiNarudzbe.Narudzba.Isporuka = new Isporuka();
            DetaljiNarudzbe.Narudzba.Klijent = new Klijent();
            DetaljiNarudzbe.Narudzba.Klijent.Korisnik = new Korisnik();
            DetaljiNarudzbe.Narudzba.Klijent.Korisnik.Login = new Login();
            DetaljiNarudzbe.Narudzba.Klijent.Id = KlijentId;
            Korisnik korisnik = Autentifikacija.GetLogiraniKorisnik(HttpContext);
            DetaljiNarudzbe.Narudzba.Klijent.Korisnik.Id = korisnik.Id;
            DetaljiNarudzbe.Narudzba.Klijent.Korisnik.Login.Id = korisnik.Login.Id;
            ctx.DetaljiNarudzbi.Add(DetaljiNarudzbe);
            ctx.SaveChanges();
            BuildEmailTemplate(korisnik.Id,DetaljiNarudzbe.Narudzba.Id); 
            return RedirectToAction("Index");
        }

        public void BuildEmailTemplate(int korisnikId,int narudzbaId)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/Areas/ModulKlijenti/Views/Narudzba/") + "Text2" + ".cshtml");
            var regInfo = ctx.Korisnici.Where(x => x.Id == korisnikId).FirstOrDefault();  
            body = body.ToString();
            body = body.Replace("@ViewBag.KorisnickoIme", regInfo.Ime+" "+regInfo.Prezime);
            body = body.Replace("@ViewBag.KorisnickoAdresa", regInfo.Adresa);
            body = body.Replace("@ViewBag.KorisnickiGrad", regInfo.Grad.Naziv);
            var narudzba = ctx.DetaljiNarudzbi.Where(x => x.NarudzbaId == narudzbaId).FirstOrDefault();
            body = body.Replace("@ViewBag.NarudzbaModel", narudzba.Artikal.Marka+ " " + narudzba.Artikal.Model);
            body = body.Replace("@ViewBag.NarudzbaCijena", narudzba.Artikal.Cijena.ToString()); 
            BuildEmailTemplate("Your order has been successfully processed", body, regInfo.Email);
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