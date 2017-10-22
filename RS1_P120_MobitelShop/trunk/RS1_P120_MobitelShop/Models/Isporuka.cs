using RS1_P120_MobitelShop.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS1_P120_MobitelShop.Models
{
    public class Isporuka : IEntity
    {
        public string _Naziv { get; set; }
        public string _Tip { get; set; }
        public string _Kontakt { get; set; }
        public int Id { get; set; }

    }
}