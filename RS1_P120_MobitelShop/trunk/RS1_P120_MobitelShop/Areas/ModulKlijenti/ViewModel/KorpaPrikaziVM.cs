using RS1_P120_MobitelShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS1_P120_MobitelShop.Areas.ModulKlijenti.ViewModel
{
    public class KorpaPrikaziRow
    {
        public string ModelNaziv { get; set; }
        public double Cijena { get; set; }
        public int KorpaId { get; set; }
        public int ArtikalId { get; set; }
        public int? Popust { get; set; }
        public double CijenaSaPopustom { get; set; }
        public int KorisnikId { get; set; }

    }
    public class KorpaPrikaziVM
    {
        public int BrojArtikalaUKorpi { get; set; }
        public List<KorpaPrikaziRow> ListaKorpa { get; set; }
    }
}