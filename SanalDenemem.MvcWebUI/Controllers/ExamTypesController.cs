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
    public class ExamTypesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: ExamTypes
        public ActionResult Index()
        {
            return View(db.ExamTypes.ToList());
        }

        // GET: ExamTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamType examType = db.ExamTypes.Find(id);
            if (examType == null)
            {
                return HttpNotFound();
            }
            return View(examType);
        }

        // GET: ExamTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExamTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ExamTypeName,Desc")] ExamType examType)
        {
            if (ModelState.IsValid)
            {
                db.ExamTypes.Add(examType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(examType);
        }

        // GET: ExamTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamType examType = db.ExamTypes.Find(id);
            if (examType == null)
            {
                return HttpNotFound();
            }
            return View(examType);
        }

        // POST: ExamTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ExamTypeName,Desc")] ExamType examType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(examType);
        }

        // GET: ExamTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamType examType = db.ExamTypes.Find(id);
            if (examType == null)
            {
                return HttpNotFound();
            }
            return View(examType);
        }

        // POST: ExamTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExamType examType = db.ExamTypes.Find(id);
            db.ExamTypes.Remove(examType);
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
    }
}
