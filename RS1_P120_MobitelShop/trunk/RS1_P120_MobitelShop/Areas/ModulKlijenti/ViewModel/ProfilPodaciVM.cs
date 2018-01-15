﻿using RS1_P120_MobitelShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RS1_P120_MobitelShop.Areas.ModulKlijenti.ViewModel
{
    public class ProfilPodaciVM
    { 
        public Korisnik Korisnik { get; set; }
        [Required(ErrorMessage = "Unesite Vaše ime")]
        public string Ime { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } 
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string PasswordConfirm { get; set; }
        public string NoviPassword { get; set; }
        [Required(ErrorMessage = "Unesite Vaše prezime")]
        public string Prezime { get; set; }
        [Required(ErrorMessage = "Unesite Vaš telefonski broj")]
        public string Telefon { get; set; }
        [Required(ErrorMessage = "Unesite Vašu adresu")]
        public string Adresa { get; set; }   
        public string Email { get; set; }
        public List<SelectListItem> gradoviStavke { get; set; }
        //[Required(ErrorMessage = "Unesite grad")]
        public int GradId { get; set; }
        public string GradNaziv { get; set; }
    }
}