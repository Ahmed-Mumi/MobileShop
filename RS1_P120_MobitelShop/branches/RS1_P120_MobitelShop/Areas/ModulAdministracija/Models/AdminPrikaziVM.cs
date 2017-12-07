using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS1_P120_MobitelShop.Areas.ModulAdministracija.Models
{
    public class AdminPrikaziVM
    {
        public class AdminInfo
        {
            public int Id { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public DateTime DatumRodjenja { get; set; }
            public string Email { get; set; }
            public string Telefon { get; set; }
            public string Adresa { get; set; }
            public string KorisnickoIme { get; set; }
            public string password { get; set; }
            public string Grad { get; set; }
            public int GradId { get; set; }
            public int LoginId { get; set; }
            public int AdministratorId { get; set; }
        }
        public List<AdminInfo> adminStavke { get; set; }
    }
}