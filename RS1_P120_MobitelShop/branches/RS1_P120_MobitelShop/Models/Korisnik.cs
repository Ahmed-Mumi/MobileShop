using RS1_P120_MobitelShop.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS1_P120_MobitelShop.Models
{
    public class Korisnik : IEntity
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Adresa { get; set; }
        public string KorisnickoIme { get; set; }
        public string LozinkaHash { get; set; }
        public string LozinkaSalt { get; set; }

        public virtual Klijent Klijent { get; set; }


        public virtual Administrator Administrator { get; set; }
     

        public virtual Grad Grad { get; set; }
        public int GradId { get; set; }

        public virtual Login Login { get; set; }
        public int LoginId { get; set; }
      
    }
}
