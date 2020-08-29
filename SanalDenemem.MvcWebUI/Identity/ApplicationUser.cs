using Microsoft.AspNet.Identity.EntityFramework;
using SanalDenemem.MvcWebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SanalDenemem.MvcWebUI.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }

    } 
}