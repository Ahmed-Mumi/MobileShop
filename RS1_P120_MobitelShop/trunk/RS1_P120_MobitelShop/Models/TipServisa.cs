using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RS1_P120_MobitelShop.Helper;
namespace RS1_P120_MobitelShop.Models
{
    public class TipServisa:IEntity
    {
        public int Id { get; set; }
        public string Tip { get; set; }
        public int Cijena { get; set; }
        public virtual Artikal Artikal { get; set; }
        public int? ArtikalId { get; set; }

        
    }
}