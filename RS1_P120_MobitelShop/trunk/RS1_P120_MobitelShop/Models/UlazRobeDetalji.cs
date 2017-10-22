using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RS1_P120_MobitelShop.Helper;
namespace RS1_P120_MobitelShop.Models
{
    public class UlazRobeDetalji:IEntity
    {
        public int Id { get; set; }
        public int Kolicina { get; set; }
        public DateTime Datum { get; set; }

        public Filijala Filijala { get; set; }
        public int FilijalaId { get; set; }

        public Dobavljac Dovabljac { get; set; }
        public int DobavljacId { get; set; }
    }
}