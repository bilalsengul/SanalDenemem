using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SanalDenemem.MvcWebUI.Entity
{
    public class Contact
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Lütfen gecerli E-mail adresi giriniz"),Required]
        public string  Email { get; set; }
        [Required]
        public string Content { get; set; }
        public string  Subject { get; set; }
    }
}