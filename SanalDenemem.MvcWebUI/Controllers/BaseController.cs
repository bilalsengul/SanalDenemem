using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SanalDenemem.MvcWebUI.Entity;
using SanalDenemem.MvcWebUI.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SanalDenemem.MvcWebUI.Controllers
{
    public class BaseController : Controller
    {
        protected DataContext db { get; private set; }
        protected UserManager<ApplicationUser> UserManager { get; private set; }
        protected RoleManager<ApplicationRole> RoleManager { get; private set; }


        public BaseController()
        {
            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            RoleStore<ApplicationRole> roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            db = new DataContext();
            UserManager = new UserManager<ApplicationUser>(userStore);
            RoleManager = new RoleManager<ApplicationRole>(roleStore);

        }

       /* protected Member GetCurrentUser()
        {
            return (Member)Session["LogonUser"];
        }*/
    }
}