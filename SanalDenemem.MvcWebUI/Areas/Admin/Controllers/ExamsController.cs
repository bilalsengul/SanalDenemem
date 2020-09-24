using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SanalDenemem.MvcWebUI.Areas.Admin.Views.Exams;
using SanalDenemem.MvcWebUI.Entity;

namespace SanalDenemem.MvcWebUI.Areas.Admin.Controllers
{
    public class ExamsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Admin/Exams
        public ActionResult Index()
        {
            var exams = db.Exams.Include(e => e.ExamType);
            return View(exams.ToList());
        }

        // GET: Admin/Exams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = db.Exams.Find(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        // GET: Admin/Exams/Create
        public ActionResult Create()
        {
            ViewBag.ExamTypeId = new SelectList(db.ExamTypes, "Id", "ExamTypeName");
            return View();
        }

        // POST: Admin/Exams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,ExamTypeId")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                db.Exams.Add(exam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExamTypeId = new SelectList(db.ExamTypes, "Id", "ExamTypeName", exam.ExamTypeId);
            return View(exam);
        }

        // GET: Admin/Exams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = db.Exams.Find(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExamTypeId = new SelectList(db.ExamTypes, "Id", "ExamTypeName", exam.ExamTypeId);
            return View(exam);
        }

        // POST: Admin/Exams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,ExamTypeId")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExamTypeId = new SelectList(db.ExamTypes, "Id", "ExamTypeName", exam.ExamTypeId);
            return View(exam);
        }

        // GET: Admin/Exams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = db.Exams.Find(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        // POST: Admin/Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exam exam = db.Exams.Find(id);
            db.Exams.Remove(exam);
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

        // GET: Admin/Exams/Questions/5
        public ActionResult Questions(int? id)
        {
            ExamViewModel evm = new ExamViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evm.QuestionList = db.Questions.Where(x => x.ExamId == id).ToList();
            foreach (var item in evm.QuestionList)
            {
                item.Topic = db.Topics.Where(x => x.Id == item.TopicId).FirstOrDefault();
                item.Topic.Lesson = db.Lessons.Where(x => x.Id == item.Topic.LessonId).FirstOrDefault();
            }
            evm.Exam = db.Exams.Where(x => x.Id == id).FirstOrDefault();
            return View(evm);
        }

        // GET: Admin/Exams/Questions/5
        [HttpPost]
        public ActionResult CreateQuestion(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Question> questionlist = db.Questions.Where(x => x.ExamId == int.Parse(id)).ToList();
            return View(questionlist);
        }

        [HttpPost]
        public JsonResult GetLessons()
        {
            List<Lesson> LessonList = db.Lessons.ToList();
            return Json(LessonList);
        }

        [HttpPost]
        public JsonResult GetTopicsByLesson(int lessonId)
        {
            List<Topic> TopicList = db.Topics.Where(x => x.LessonId == lessonId).ToList();
            return Json(TopicList);
        }

        //[HttpPost]
        //public ActionResult InsertQuestion()
        //{
        //    //if (id == null)
        //    //{
        //    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    //}
        //    //List<Question> questionlist = db.Questions.Where(x => x.ExamId == int.Parse(id)).ToList();
        //    //return View(questionlist);
        //}

    }
}
