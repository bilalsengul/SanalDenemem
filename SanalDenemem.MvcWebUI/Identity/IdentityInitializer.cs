using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SanalDenemem.MvcWebUI.Identity
{
    //CreateDatabaseIfNotExists<IdentityDataContext>
    //DropCreateDatabaseIfModelChanges<IdentityDataContext>
    public class IdentityInitializer: CreateDatabaseIfNotExists<IdentityDataContext>
    {
        protected override void Seed(IdentityDataContext context)
        {

            if (!context.Roles.Any(i => i.Name == "admin"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                //TODO:sorun cıkarsa cstt kullanma direkt obje icinde oluştur.
                var role = new ApplicationRole()
                {
                    Name="admin",
                    Description="yönetici"
                };
                manager.Create(role);
            }
            if (!context.Roles.Any(i => i.Name == "freeMember"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole()
                {
                    Name = "freeMember",
                    Description = "normal üye"
                };
                manager.Create(role);
            }
            if (!context.Roles.Any(i => i.Name == "premiumMember"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole()
                {
                    Name = "premiumMember",
                    Description = "paralı üye."
                };
                manager.Create(role);
            }

            if (!context.Users.Any(i => i.Name == "Yunussen"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager  = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Name = "Yunus",
                    Surname = "Şen",
                    UserName = "Yunussen",
                    Email = "Yunussen272@gmail.com"
                };

                manager.Create(user,"1234567");
                manager.AddToRole(user.Id, "admin");
                manager.AddToRole(user.Id, "freeMember");
                manager.AddToRole(user.Id, "premiumMember");
            }

            if (!context.Users.Any(i => i.Name == "bilalsengul"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Name = "Bilal",
                    Surname = "Şengül",
                    UserName = "bilalsengul",
                    Email = "bilalsengul@gmail.com"
                };

                manager.Create(user, "1234567");
                manager.AddToRole(user.Id, "admin");
                manager.AddToRole(user.Id, "freeMember");
                manager.AddToRole(user.Id, "premiumMember");
            }

            if (!context.Users.Any(i => i.Name == "uogut"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Name = "Ümit Recep",
                    Surname = "ÖĞÜT",
                    UserName = "uogut",
                    Email = "umitrecepogut@gmail.com"
                };

                manager.Create(user, "12341234");
                manager.AddToRole(user.Id, "admin");
                manager.AddToRole(user.Id, "freeMember");
                manager.AddToRole(user.Id, "premiumMember");
            }

            base.Seed(context);
        }
    }
}