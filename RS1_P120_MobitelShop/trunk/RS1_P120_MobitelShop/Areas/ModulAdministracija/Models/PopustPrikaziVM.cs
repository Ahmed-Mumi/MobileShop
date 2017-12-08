using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS1_P120_MobitelShop.Areas.ModulAdministracija.Models
{
    public class PopustPrikaziVM
    {
       public class PopustInfo
       {
        public int? Id { get; set; }
        public int iznosPopusta { get; set; }
        public int PopustId { get; set; }
        public string modelName { get; set; }
       }
       public List<PopustInfo> popustNovi { get; set; }
       public int ArtikalId { get; set;}
    }
}