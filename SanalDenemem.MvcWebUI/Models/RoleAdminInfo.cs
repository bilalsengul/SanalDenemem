using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SanalDenemem.MvcWebUI.Models
{
    public class RoleAdminInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string[] AddToIds { get; set; }
        public string[] DeleteToIds { get; set; }
    }
}