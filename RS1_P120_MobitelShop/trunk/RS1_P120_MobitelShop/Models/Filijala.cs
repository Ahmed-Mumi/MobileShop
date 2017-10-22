using RS1_P120_MobitelShop.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS1_P120_MobitelShop.Models
{
    public class Filijala : IEntity
    {
        public string Naziv { get; set; }
        public int Id { get; set; }

        public Grad Grad { get; set; }
        public int GradId { get; set; }

    }
}