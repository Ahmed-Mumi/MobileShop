using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RS1_P120_MobitelShop.Areas.ModulAdministracija.Models
{
    public class ArtikalPrikaziVM
    {
        public class ArtikalInfo
        {
            public int Id { get; set; }
            public string sifraArtikla { get; set; }
            public string slika { get; set; }
            public string model { get; set; }
            public string marka { get; set; }
            public double cijena { get; set; }
            public string garancija { get; set; }
            public int SpecifikacijeId { get; set; }
        }
        public List<ArtikalInfo> artikal { get; set; }
        public int SpecifikacijeId { get; set; }

        public int PopustId { get; set; }
        public List<SelectListItem> popusti { get; set; }


    }
}