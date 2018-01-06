using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RS1_P120_MobitelShop.Areas.ModulAdministracija.Models
{
    public class NarudzbePrikaziVM
    {
        public class NarudzbeInfo
        {
            public int Id { get; set; }
            public int BrojNarudzbe { get; set; }
            public DateTime DatumNarudzbe { get; set; }
            public string statusNarudzbe { get; set; }
            public bool Otkazano { get; set; }
            public List<SelectListItem> isporuke { get; set; }
            public int IsporukaId { get; set; }
            public List<SelectListItem> klijenti { get; set; }
            public int KlijentId { get; set; }
            public string isporukaVrsta { get; set; }
            public string klijentIme { get; set; }
        }
        public List<NarudzbeInfo> narudzbeStavke { get; set; }
    }
}