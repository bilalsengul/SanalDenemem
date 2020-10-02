using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SanalDenemem.MvcWebUI.Entity;

namespace SanalDenemem.MvcWebUI.Controllers
{
    public class MembersController : BaseController
    {
 
        public ActionResult Profile(string id)
        {
           
            if (id == null && !Request.IsAuthenticated || id == null)
            {
                return View("Error", new string[] { "yetkisiz veya hatalı giriş" });
            }
           
            var member = db.Members.FirstOrDefault(i => i.UserName == id || i.UserId == id);
            if (member == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(member);
        }

        [ChildActionOnly]
        public PartialViewResult ExamResult()
        {
          
            return PartialView("_ExamResult");
        }

        [ChildActionOnly]
        public PartialViewResult Tasks(int currentId)
        {
            var tasks = db.Tasks.Where(i => i.MemberId == currentId).ToList();
            ViewBag.count = tasks.Count;
            ViewBag.id = currentId;
            return PartialView("_taskList",tasks);
        }

        [HttpGet]
        // GET: Home
        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, string id)
        {
            if (file != null && file.ContentLength > 0)
            {
                var extension = Path.GetExtension(file.FileName);
                if (extension == ".jpg" || extension == ".png")
                {
                    //farklı isimde kayıt ettim
                    var folder = Server.MapPath("~/Server/img");
                    var randomfilename = Path.GetRandomFileName();
                    var filename = Path.ChangeExtension(randomfilename, ".jpg");
                    var path = Path.Combine(folder, filename);

                    file.SaveAs(path);

                    //veri tabanına resmi kayıt ettim.
                    var member = db.Members.FirstOrDefault(i => i.UserId == id);
                    member.ProfileImageName = filename;
                    db.SaveChanges();

                }
                else
                {
                    ViewData["message"] = "resim dosyası seciniz seciniz.";
                    return View();
                }
            }
            else
            {
                ViewData["message"] = "bir dosya seciniz.";
                return View();
            }
            return RedirectToAction("Profile", new { id = id });
        }


        #region bu kısımlara karışmayın en son ben(yunus) publis etmeden önce silecegim veya düzeltecegim.
        /*
        // GET: Members
        public ActionResult Index(string id)
        {
            if (!Request.IsAuthenticated)
            {
                return View("Error", new string[] { "yetkisiz giriş" });
            }
            var member = db.Members.FirstOrDefault(i => i.UserName == id ||i.UserId==id);
            

            return View(member);
        }

        // GET: Members/Details/5
        public ActionResult Details(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.FirstOrDefault(i => i.UserId == id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,UserName,Name,Surname,ProfileImageName")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }

        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,UserName,Name,Surname,ProfileImageName")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        */
        #endregion


    }
}
