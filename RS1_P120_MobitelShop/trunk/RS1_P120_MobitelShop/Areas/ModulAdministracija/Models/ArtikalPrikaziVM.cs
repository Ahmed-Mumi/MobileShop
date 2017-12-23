using PagedList;
using RS1_P120_MobitelShop.Models;
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

            public string model { get; set; }
            public string slika { get; set; }
            public string marka { get; set; }
            public string garancija { get; set; }
            public double Cijena { get; set; }
            public double CijenaSaPopustom { get; set; }
        }
       
        public double cijena { get; set; }     
        public string model { get; set; }
        public int Id { get; set; }
        public IPagedList<Artikal> ArtikalPageList { get; set; }
        public List<ArtikalInfo> spisakMobitela { get; set; }
        public int SpecifikacijeId { get; set; }
        public int ArtikalId { get; set; }
        public string searchString { get; set; }
        public string searchStringModel { get; set; }
        public double? CijenaMin { get; set; }
        public double? CijenaMax { get; set; }

        public int PopustId { get; set; }
        public List<SelectListItem> popusti { get; set; }

      
    }
}