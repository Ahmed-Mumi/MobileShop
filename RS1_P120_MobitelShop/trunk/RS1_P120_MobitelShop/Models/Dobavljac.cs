using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RS1_P120_MobitelShop.Helper;

namespace RS1_P120_MobitelShop.Models
{
    public class Dobavljac:IEntity
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Adresa { get; set; }

        public Skladiste Skladiste { get; set; }
        public int SkladisteId { get; set; }

        public Grad Grad { get; set; }
        public int GradId { get; set; }
    }
}