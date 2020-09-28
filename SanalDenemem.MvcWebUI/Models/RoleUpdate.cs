using SanalDenemem.MvcWebUI.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SanalDenemem.MvcWebUI.Models
{
    public class RoleUpdate
    {
        public ApplicationRole Role { get; set; }
        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NonMembers { get; set; }
    }
}