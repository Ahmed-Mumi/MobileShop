using RS1_P120_MobitelShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS1_P120_MobitelShop.ViewModel
{
    public class KorisnikVM
    {
        public Korisnik Korisnik { get; set; }
        public string Zapamti { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
    }
}