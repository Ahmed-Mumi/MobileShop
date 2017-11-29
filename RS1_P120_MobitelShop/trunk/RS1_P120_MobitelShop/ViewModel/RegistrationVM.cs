using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RS1_P120_MobitelShop.Models;
using RS1_P120_MobitelShop.Helper;

namespace RS1_P120_MobitelShop.ViewModel
{
    public class RegistrationVM
    {
        public Klijent Klijent { get; set; }
        public string Ime { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Prezime { get; set; }
        public string Spol { get; set; }
        public string Telefon { get; set; }
        public string Adresa { get; set; }
        public string Email { get; set; }
        public DateTime DatumRodjenja { get; set; }
    }
}