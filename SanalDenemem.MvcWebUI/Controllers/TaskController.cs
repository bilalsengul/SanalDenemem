using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SanalDenemem.MvcWebUI.Entity;

namespace SanalDenemem.MvcWebUI.Controllers
{
    public class TaskController : BaseController
    {
        

        // GET: Task/Create
        public ActionResult Create(int id)
        {
            ViewBag.MemberId =id;
            return View();
        }

        // POST: Task/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Task task,int id)
        {
            task.MemberId = id;
            var member = db.Members.FirstOrDefault(i => i.Id == id);
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Profile", "Members",new { id=member.UserId} );
            }

            ViewBag.MemberId = id;
            return View(task);
        }

        // GET: Task/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            ViewBag.userId = task.MemberId;
            if (task == null)
            {
                return HttpNotFound();
            }
           // ViewBag.MemberId = new SelectList(db.Members, "Id", "UserId", task.MemberId);
            return View(task);
        }

        // POST: Task/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Task task)
        {
            if (ModelState.IsValid)
            {
                var member = db.Members.FirstOrDefault(i => i.Id == task.MemberId);
              
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Profile", "Members", new { id = member.UserId });
            }
            ViewBag.userId = task.MemberId;
            return View(task);
        }

        // GET: Task/Delete/5
      

        // POST: Task/Delete/5
        
   
        public ActionResult Delete(int id)
        {          
            Task task = db.Tasks.Find(id);
            
           var userId = task.MemberId;
           var member = db.Members.FirstOrDefault(i => i.Id == userId);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Profile","Members",new {id=member.UserId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
