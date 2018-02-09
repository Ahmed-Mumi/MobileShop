using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS1_P120_MobitelShop.Areas.ModulAdministracija.ViewModels
{
    public class KlijentiPrikaziVM
    {
        public class KlijentInfo
        {
            public int Id { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public DateTime? DatumRodjenja { get; set; }
            public string Email { get; set; }
            public string Telefon { get; set; }
            public string Adresa { get; set; }
            public string KorisnickoIme { get; set; }
            public string Grad { get; set; }
            public int GradId { get; set; }
            public int LoginId { get; set; }
            public int KlijentId { get; set; }
            public bool IsBanned { get; set; }
        }
        public List<KlijentInfo> klijentStavke { get; set; }
    }
}