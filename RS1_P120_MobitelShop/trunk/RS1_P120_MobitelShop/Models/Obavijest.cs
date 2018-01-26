using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS1_P120_MobitelShop.Models
{
    public class Obavijest
    {
        public int Id { get; set; }
        public string TekstObavijesti { get; set; }
        public string NaslovObavijesti { get; set; }
        public DateTime DatumObavijesti { get; set; }
        public string Slika { get; set; }
        public virtual Administrator Administrator { get; set; }
        public int AdministratorId { get; set; }
    }
}