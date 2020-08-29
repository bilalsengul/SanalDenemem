using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SanalDenemem.MvcWebUI.Models
{
    public class Login
    {
        [Required]
        [DisplayName("kullanıcı adınız")]
        public String Username { get; set; }

        [Required]
        [DisplayName("password")]
        public String Password { get; set; }

        [DisplayName("Beni hatırla")]
        public bool RememberMe { get; set; }
    }
}