using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RS1_P120_MobitelShop.Models;
using RS1_P120_MobitelShop.Helper;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RS1_P120_MobitelShop.ViewModel
{
    public class RegistrationVM
    {
        public Klijent Klijent { get; set; }
        public string Ime { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        //dodano
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string PasswordConfirm { get; set; }
        public string Prezime { get; set; }
        public string Spol { get; set; }
        public string Telefon { get; set; }
        public string Adresa { get; set; }
        public string Email { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime DatumRodjenja { get; set; }
        public List<SelectListItem> gradoviStavke { get; set; }
        public int GradId { get; set; }
    }
}