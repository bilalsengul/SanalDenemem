using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SanalDenemem.MvcWebUI.Entity;
using SanalDenemem.MvcWebUI.Identity;
using SanalDenemem.MvcWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SanalDenemem.MvcWebUI.Controllers
{
    public class AccountController : BaseController
    {
        [HttpGet]
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // GET: Account
        public ActionResult Register(Register model)
        {

            if (ModelState.IsValid)
            {
                //kişiyi kayıt etme işlemleri ve üye atama işlemleri hallettim.
                ApplicationUser user = new ApplicationUser();
                Member member = new Member();

                user.Name = model.Name;
                member.Name = model.Name;

                user.Surname = model.Surname;
                member.Surname = model.Surname;

                user.Email = model.Email;
                user.UserName = model.Username;
                member.UserName = model.Username;
                var result = UserManager.Create(user, model.Password);

                if (result.Succeeded)
                {
                    member.UserId = user.Id;
                    if (RoleManager.RoleExists("freeMember"))
                    {
                        UserManager.AddToRole(user.Id, "freeMember");
                    }

                    memberAdd(member);
                    return RedirectToAction("listedeneme", "Account");
                }
                else
                {
                    ModelState.AddModelError("UserCreateError", "kullanıcı adı sistemde mevcut lütfen farklı bir kullanıcı adı seçin");
                }
            }
            return View(model);
        }

        [HttpGet]
        // GET: Account
        public ActionResult Login()
        {

            if (Request.IsAuthenticated)
            {
                return View("Error", new string[] { "yetkisiz giriş" });
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // GET: Account
        public ActionResult Login(Login model,string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var user =UserManager.Find(model.Username, model.Password);
                if (user != null)
                {
                    //TODO:systemweb paketini eklemeyi unutma
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    //owin clasındaki oluşturdugum keyi verdim ismini degiştimeyi unutmaaaa.
                    var identityclaims = UserManager.CreateIdentity(user, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties()
                    {
                        //beni hatırla butonu icin.
                        IsPersistent = model.RememberMe
                    };
                    authManager.SignIn(authProperties, identityclaims);
                    if (!String.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    return RedirectToAction("index");//giriş olunca indexse yolladım.
                }
                else
                {
                    ModelState.AddModelError("LoginError", "şifre veya kullanıcı adı hatalı");
                }
            }
            else
            {
                ModelState.AddModelError("LoginError", "giriş yaparken hata oluştu.");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Login");
        }

        //TODO:deneme amaclı oluşturdum işim bitince view ve bu action Delete
        [Authorize(Roles ="admin")]
        public ActionResult listedeneme()
        {
            
           
            var users = UserManager.Users.ToList();
           
            return View(users);
        }

        public void memberAdd(Member member)
        {
            db.Members.Add(member);
            db.SaveChanges();
        }
    }
}