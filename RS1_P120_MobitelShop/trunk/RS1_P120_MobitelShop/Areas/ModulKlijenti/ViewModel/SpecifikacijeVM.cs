using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RS1_P120_MobitelShop.Models;

namespace RS1_P120_MobitelShop.Areas.ModulKlijenti.ViewModel
{


    public class SpecifikacijeVM
    {
        public bool ISCHECKEDUAAAAAAAAAAAAAAAAAAAAA { get; set; }

        public int RamId { get; set; }
        public string RamNaziv { get; set; }
        public bool isRamChecked { get; set; }
        public bool isRam { get; set; } 
        public int EksternaMemorijaId { get; set; }
        public string EksternaMemorijaNaziv { get; set; }
        public bool isEksternaMemorijaChecked { get; set; }
        public bool isEksternaMemorija { get; set; }  
        public int OperativniSistemId { get; set; }
        public string OperativniSistemNaziv { get; set; }
        public bool isOperativniSistemChecked { get; set; }
        public bool isOperativniSistem { get; set; } 
        public int EkranId { get; set; }
        public string EkranNaziv { get; set; }
        public bool isEkranChecked { get; set; }
        public bool isEkran { get; set; } 
    }
    public class SpecifikacijeList
    {
        public List<SpecifikacijeVM> specifikacijeList { get; set; } 
        public List<Artikal> listaArtikala { get; set; }
    }
}

