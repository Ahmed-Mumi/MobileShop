using RS1_P120_MobitelShop.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS1_P120_MobitelShop.Models
{
    public class Administrator : IEntity
    {
        public int Id { get; set; }
        public virtual Korisnik Korisnik { get; set; }    
    }
}