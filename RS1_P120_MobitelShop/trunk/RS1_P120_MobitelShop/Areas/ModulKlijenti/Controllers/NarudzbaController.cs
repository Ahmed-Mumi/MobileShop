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
                    ArtikalId=p.ArtikalId,
                    Popust= ctx.Popusti.Where(pop => pop.ArtikalId == p.ArtikalId).Select(s => s.IznosPopusta).FirstOrDefault(),
                    CijenaSaPopustom = ((100 - ctx.Popusti.Where(pop => pop.ArtikalId == p.ArtikalId).Select(s => s.IznosPopusta).FirstOrDefault()) * p.Artikal.Cijena) / 100,
                    KorisnikId=p.Klijent.Korisnik.Id

                }).ToList()
        };
            Korisnik k = Autentifikacija.GetLogiraniKorisnik(HttpContext); 
            Model.BrojArtikalaUKorpi = ctx.Korpe.Count(x => x.KlijentId == k.Id);

            return View("Index",Model);
        }
        [Autorizacija(KorisnickeUloge.Klijent)] 
        public ActionResult Snimi(ProfilPodaciVM vm)
        {
            if (!ModelState.IsValid)
            {
                Korisnik k = ctx.Korisnici.Find(Autentifikacija.GetLogiraniKorisnik(HttpContext).Id);
                vm.gradoviStavke = ucitajGradove(k);
                vm.GradNaziv = k.Grad.Naziv;
                return View("~/Areas/ModulKlijenti/Views/Profile/Index.cshtml", vm);
            }
            Korisnik Korisnik = ctx.Korisnici.Find(Autentifikacija.GetLogiraniKorisnik(HttpContext).Id);
            Korisnik.Ime = vm.Ime;
            Korisnik.Prezime = vm.Prezime;
            Korisnik.Telefon = vm.Telefon;
            Korisnik.Adresa = vm.Adresa;
            Korisnik.GradId = vm.GradId;
            ctx.SaveChanges();
            Korpa Korpa = ctx.Korpe.Where(x => x.Id == vm.KorpaId).FirstOrDefault();
            int KlijentId = Korpa.KlijentId;
            ctx.Korpe.Remove(Korpa);
            DetaljiNarudzbe DetaljiNarudzbe = new DetaljiNarudzbe();
            DetaljiNarudzbe.Narudzba = new Narudzba();
            DetaljiNarudzbe.Artikal = ctx.Artikli.Where(x => x.Id == vm.ArtikalId).FirstOrDefault();  
            DetaljiNarudzbe.Narudzba.DatumNarudzbe = DateTime.Now; 
            DetaljiNarudzbe.Narudzba.Isporuka = new Isporuka(); 
            DetaljiNarudzbe.Narudzba.Klijent = ctx.Klijenti.Where(x => x.Id == KlijentId).FirstOrDefault();
            ctx.DetaljiNarudzbi.Add(DetaljiNarudzbe);
            ctx.SaveChanges();
            BuildEmailTemplate(Korisnik.Id, DetaljiNarudzbe.NarudzbaId);
            return RedirectToAction("Index");
        }

        private List<SelectListItem> ucitajGradove(Korisnik Korisnik)
        {
            var gradovi = new List<SelectListItem>();
            if (Korisnik.GradId == 0 || Korisnik.GradId == null)
            {
                gradovi.Add(new SelectListItem { Value = null, Text = "Izaberite grad" });
                gradovi.AddRange(ctx.Gradovi.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Naziv
                }));
            }
            else
            {
                gradovi.AddRange(ctx.Gradovi.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Naziv,
                    Selected = x.Id == Korisnik.GradId ? true : false
                }));
            }
            return gradovi;
        }
        [Autorizacija(KorisnickeUloge.Klijent)] 
        public ActionResult Ukloni(int id)
        {
            Korpa Korpa = ctx.Korpe.Where(x => x.Id == id).FirstOrDefault();
            ctx.Korpe.Remove(Korpa);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public void BuildEmailTemplate(int korisnikId, int narudzbaId)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/Areas/ModulKlijenti/Views/Narudzba/") + "Text2" + ".cshtml");
            var regInfo = ctx.Korisnici.Where(x => x.Id == korisnikId).FirstOrDefault();
            body = body.ToString();
            body = body.Replace("@ViewBag.KorisnickoIme", regInfo.Ime + " " + regInfo.Prezime);
            body = body.Replace("@ViewBag.KorisnickoAdresa", regInfo.Adresa);
            body = body.Replace("@ViewBag.KorisnickiGrad", regInfo.Grad.Naziv);
            var narudzba = ctx.DetaljiNarudzbi.Where(x => x.NarudzbaId == narudzbaId).FirstOrDefault();
            body = body.Replace("@ViewBag.NarudzbaModel", narudzba.Artikal.Marka + " " + narudzba.Artikal.Model);
            body = body.Replace("@ViewBag.NarudzbaCijena", narudzba.Artikal.Cijena.ToString());
            var popust = ctx.Popusti.Where(x => x.ArtikalId == narudzba.ArtikalId).FirstOrDefault();
            if (popust != null)
            {
                body = body.Replace("@ViewBag.NarudzbaPopust", popust.IznosPopusta.ToString());
                double cijenasapopustom = ((100 - popust.IznosPopusta) * narudzba.Artikal.Cijena) / 100;
                body = body.Replace("@ViewBag.SaPopustom", cijenasapopustom.ToString());
            }
            else
            {
                body = body.Replace("@ViewBag.NarudzbaPopust", "---"); 
                body = body.Replace("@ViewBag.SaPopustom", narudzba.Artikal.Cijena.ToString());
            }
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