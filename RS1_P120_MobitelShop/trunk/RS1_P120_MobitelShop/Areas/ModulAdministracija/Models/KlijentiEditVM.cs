using RS1_P120_MobitelShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace RS1_P120_MobitelShop.Areas.ModulAdministracija.Models
{
    public class KlijentiEditVM
    {
        public int Id { get; set; }
        public Klijent Klijent { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Adresa { get; set; }
        public string KorisnickoIme { get; set; }
        public List<SelectListItem> gradovi { get; set; }
        public string GradNaziv { get; set; }
        public int GradId { get; set; }
        public int LoginId { get; set; }
        public int KlijentId { get; set; }
    }
}