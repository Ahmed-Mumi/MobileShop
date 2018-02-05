using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS1_P120_MobitelShop.Areas.ModulAdministracija.Models
{
    public class DetaljiNarudzbePrikaziVM
    {
        public class DetaljiInfo
        {
            public int Id { get; set; }
            public int Kolicina { get; set; }
            public int ArtikalId { get; set; }
            public string artikalNaziv { get; set; }
            public double? Ukupno { get; set; }
            public double? CijenaArtikla { get; set; }
            public int NarudzbaId { get; set; }
          
            public double? CijenaSaPopustom { get; set; }
         
        }
        public List<DetaljiInfo> detaljiStavke { get; set; }
        public int NarudzbaId { get; set; }
    }
}