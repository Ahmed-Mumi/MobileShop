using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RS1_P120_MobitelShop.Areas.ModulAdministracija.Models
{
    public class PopustEditVM
    {
        public int Id { get; set; }
        public int ArtikalId { get; set; }
        public List<SelectListItem> artikli { get; set; }
        public DateTime datumOd { get; set; }
        public DateTime datumDo { get; set; }
        public int iznosPopusta { get; set; }
        public int PopustId { get; set; }
    }
}