using Microsoft.AspNet.Identity;
using SanalDenemem.MvcWebUI.Identity;
using SanalDenemem.MvcWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SanalDenemem.MvcWebUI.Controllers
{
    // [Authorize(Roles = "admin")]
    public class AdminManagerController : BaseController
    {
        public ActionResult Index()
        {
            return View(RoleManager.Roles.ToList());
        }

        public ActionResult Edit(string id)
        {
            var role = RoleManager.FindById(id);
            var member = new List<ApplicationUser>();
            var nonmember = new List<ApplicationUser>();

            foreach (var user in UserManager.Users.ToList())
            {
                var list = UserManager.IsInRole(user.Id, role.Name) ? member : nonmember;
                list.Add(user);
            }

            return View(new RoleUpdate()
            {
                Role = role,
                Members = member,
                NonMembers = nonmember
            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoleAdminInfo model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (var userid in model.AddToIds ?? new string[] { })
                {
                    result = UserManager.AddToRole(userid, model.Name);
                    if (!result.Succeeded)
                    {
                        return View("Eror", new string[]
                        {
                            " hata oluştu."
                        });
                    }
                }

                foreach (var userid in model.DeleteToIds ?? new string[] { })
                {
                    result = UserManager.RemoveFromRole(userid, model.Name);

                    if (!result.Succeeded)
                    {
                        return View("Eror", new string[]
                        {
                            " hata oluştu."
                        });
                    }
                }


                return RedirectToAction("Index");
            }
            else
            {
                return View("Eror", new string[]
                {
                    " hata oluştu."
                });
            }
        }

        public ActionResult Delete(string id)
        {
            var user = UserManager.FindById(id);
            var result = UserManager.Delete(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Eror", new string[] { "hata olutu" });
            }
        }
    }
}