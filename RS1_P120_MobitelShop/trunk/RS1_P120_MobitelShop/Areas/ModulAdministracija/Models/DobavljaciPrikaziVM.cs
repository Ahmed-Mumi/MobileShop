using RS1_P120_MobitelShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RS1_P120_MobitelShop.Areas.ModulAdministracija.Models
{
    public class DobavljaciPrikaziVM
    {
        public class DobavljaciInfo
        {
            public int Id { get; set; }
            public string Naziv { get; set; }
            public string Email { get; set; }         
            public string Telefon { get; set; }
            public string Adresa { get; set; }
            public string skladista { get; set; }
            public List<SelectListItem> skladiste { get; set; }
            public int SkladisteId { get; set; }
            public List<SelectListItem> gradovi { get; set; }
            public string Grad { get; set; }
            public int GradId { get; set; }      
        }
        public List<DobavljaciInfo> dobavljaciStavke { get; set; }
    }
}