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
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Zaboravili ste unijeti lozinku")]
        [StringLength(255, MinimumLength = 8,ErrorMessage ="Lozinka mora imati minimalno osam karaktera!")]
        //[RegularExpression("(?!^[0-9]*$)(?!^[a-zA-Z]*$)$", ErrorMessage = "Lozinka mora sadržavati brojeve i slova")]
        public string Password { get; set; }
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Lozinka nije ista. Pokušajte ponovo!")]
        public string PasswordConfirm { get; set; }
        public string Prezime { get; set; }
        public string Spol { get; set; }
        public string Telefon { get; set; }
        public string Adresa { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Neispravan format.")]
        [Required(ErrorMessage = "Zaboravili ste unijeti email adresu")]
        public string Email { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime DatumRodjenja { get; set; }
        public List<SelectListItem> gradoviStavke { get; set; }
        public int BrojArtikalaUKorpi { get; set; }

        public int GradId { get; set; }
    }
}