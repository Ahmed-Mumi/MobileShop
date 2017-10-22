using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RS1_P120_MobitelShop.Helper;

namespace RS1_P120_MobitelShop.Models
{
    public class Servis:IEntity
    {
        public int Id { get; set; }
        public DateTime DatumPrijave { get; set; }
        public DateTime DatumZavrsetka { get; set; }
        public string Opis { get; set; }
        public double Cijena { get; set; }

        public Administrator Administrator { get; set; }
        public int AdministratorId { get; set; }

        public Klijent Klijent { get; set; }
        public int KlijentId { get; set; }

        public TipServisa TipServisa { get; set; }
        public int TipServisaId { get; set; }
    }
}