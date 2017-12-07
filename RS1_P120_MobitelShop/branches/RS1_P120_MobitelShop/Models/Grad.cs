using RS1_P120_MobitelShop.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS1_P120_MobitelShop.Models
{
    public class Grad : IEntity
    {
        public string Naziv { get; set; }
        public string PostanskiBroj { get; set; }
        public int Id { get; set; }
        public virtual Kanton Kanton { get; set; }
        public int KantonId { get; set; }
    }
}
        