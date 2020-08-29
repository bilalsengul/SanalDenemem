using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SanalDenemem.MvcWebUI.Models
{
    public class Register
    {
        [Required]
        [DisplayName("İsim")]
        public String Name { get; set; }
        [Required]
        [DisplayName("Soyad")]
        public String Surname { get; set; }
        [Required]
        [DisplayName("kullanıcı adınız")]
        public String Username { get; set; }
        [Required]
        [DisplayName("Email Adresi")]
        [EmailAddress(ErrorMessage ="lütfen gecerli bir email heabı giriniz.")]
        public String Email { get; set; }
        [Required]
        [DisplayName("Şifre")]
        [StringLength(255, ErrorMessage = "şifre 6 ve 255 karakter arasında olmalı.", MinimumLength = 6)]
        public String Password { get; set; }
        [Required]
        [DisplayName("Şifre Tekrar")]
        [Compare("Password" ,ErrorMessage ="şifre aynı değil.")]
        public String RePassword { get; set; }

      
    }
}