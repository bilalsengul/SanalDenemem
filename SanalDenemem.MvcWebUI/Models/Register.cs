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
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [Display(Name = "Password")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", 
            ErrorMessage = "Şifreler en az 8 karakter olmalı ve şunların 3 / 4'ünü içermelidir: büyük harf (A-Z), küçük harf (a-z), sayı (0-9) ve özel karakter (ör.! @ # $% ^ & *)")]
        //[StringLength(255, ErrorMessage = "şifre 6 ve 255 karakter arasında olmalı.", MinimumLength = 6)]
        public String Password { get; set; }
        [Required]
        [DisplayName("Şifre Tekrar")]
        [Compare("Password" ,ErrorMessage ="şifre aynı değil.")]
        public String RePassword { get; set; }

      
    }
}