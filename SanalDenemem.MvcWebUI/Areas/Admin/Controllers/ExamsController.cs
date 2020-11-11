using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SanalDenemem.MvcWebUI.Areas.Admin.Views.Exams;
using SanalDenemem.MvcWebUI.Entity;

namespace SanalDenemem.MvcWebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
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
            exam.ExamType = db.ExamTypes.Where(x => x.Id == exam.ExamTypeId).FirstOrDefault();
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
        public ActionResult Create([Bind(Include = "Id,Title,ExamTime,ExamTypeId")] Exam exam)
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
        public ActionResult Edit([Bind(Include = "Id,Title,ExamTime,ExamTypeId")] Exam exam)
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
            List<Question> questions = db.Questions.Where(x => x.ExamId == exam.Id).ToList();
            foreach (var item in questions)
            {
                foreach (var item2 in db.Options.Where(x=>x.QuestionId == item.Id))
                {
                    db.Options.Remove(item2);
                }
                db.SaveChanges();
                db.Questions.Remove(item);
            }
            db.SaveChanges();
            foreach (var item in db.MemberExams.Where(x=>x.ExamId == exam.Id).ToList())
            {
                foreach (var item2 in db.SubMemberExams.Where(x => x.MemberExamId == item.Id).ToList())
                {
                    db.SubMemberExams.Remove(item2);
                }
                db.SaveChanges();
                db.MemberExams.Remove(item);
            }
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
            ViewBag.LessonId = new SelectList(db.Lessons, "Id", "LessonName");
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
        public JsonResult GetTopicsByLesson(int lessonId)
        {
            List<Topic> TopicList = db.Topics.Where(x => x.LessonId == lessonId).ToList();
            return Json(TopicList);
        }

        public class QuestionRequestModel
        {
            public int RowNo { get; set; }
            public double Point { get; set; }
            public string Desc { get; set; }
            public string Text { get; set; }
            public string Image { get; set; }
            public int ExamId { get; set; }
            public int LessonId { get; set; }
            public int TopicId { get; set; }
            public string Option1 { get; set; }
            public string Option2 { get; set; }
            public string Option3 { get; set; }
            public string Option4 { get; set; }
            public string Option5 { get; set; }
            public bool Option1State { get; set; }
            public bool Option2State { get; set; }
            public bool Option3State { get; set; }
            public bool Option4State { get; set; }
            public bool Option5State { get; set; }
        }

        [HttpPost]
        public JsonResult InsertQuestion(QuestionRequestModel requestModel)
        {
            Question question = new Question();
            question.RowNo = requestModel.RowNo;
            question.Point = requestModel.Point;
            question.Desc = requestModel.Desc;
            question.Text = requestModel.Text;
            HttpFileCollectionBase files = Request.Files;
            if (files.Count > 0)
            {
                HttpPostedFileBase file = files[0];
                string extension = Path.GetExtension(file.FileName);
                string guid = Guid.NewGuid().ToString() + file.FileName.Split('.')[0] + extension.ToLower();
                string path = Server.MapPath(@"~/Content/Uploads/Questions/");
                bool folderExists = Directory.Exists(path);
                if (!folderExists) { Directory.CreateDirectory(path); }
                file.SaveAs(Path.Combine(path, guid));
                question.Image = Path.Combine(path, guid);
            }
            else
            {
                question.Image = null;
            }
            question.ExamId = requestModel.ExamId;
            question.LessonId = requestModel.LessonId;
            question.TopicId = requestModel.TopicId;
            List<Option> options = new List<Option>();
            Option option1 = new Option();
            option1.OptionText = requestModel.Option1;
            option1.IsCorrect = requestModel.Option1State;
            option1.QuestionId = question.Id;
            Option option2 = new Option();
            option2.OptionText = requestModel.Option2;
            option2.IsCorrect = requestModel.Option2State;
            option2.QuestionId = question.Id;
            Option option3 = new Option();
            option3.OptionText = requestModel.Option3;
            option3.IsCorrect = requestModel.Option3State;
            option3.QuestionId = question.Id;
            Option option4 = new Option();
            option4.OptionText = requestModel.Option4;
            option4.IsCorrect = requestModel.Option4State;
            option4.QuestionId = question.Id;
            Option option5 = new Option();
            option5.OptionText = requestModel.Option5;
            option5.IsCorrect = requestModel.Option5State;
            option5.QuestionId = question.Id;
            options.Add(option1);
            options.Add(option2);
            options.Add(option3);
            options.Add(option4);
            options.Add(option5);
            question.Options = options;
            try
            {
                db.Questions.Add(question);
                db.SaveChanges();
                return Json(new { IsSuccess = true, Message = "Kayıt Başarılı!" });
            }
            catch (Exception e)
            {
                return Json(new { IsSuccess = false, Message = "Kayıt Başarısız!" });
            }
        }

        [HttpPost]
        public JsonResult DeleteQuestion(int questionId)
        {
            Question question = db.Questions.Where(x => x.Id == questionId).FirstOrDefault();
            foreach (var item in db.Options.Where(x => x.QuestionId == questionId).ToList())
            {
                db.Options.Remove(item);
                db.SaveChanges();
            }
            try
            {
                db.Questions.Remove(question);
                db.SaveChanges();
                return Json(new { IsSuccess = true, Message = "Silme Başarılı!" });
            }
            catch (Exception e)
            {
                return Json(new { IsSuccess = false, Message = "Silme Başarısız!" });
            }
        }

        [HttpPost]
        public JsonResult GetQuestionById(int questionId)
        {
            Question question = db.Questions.Where(x => x.Id == questionId).FirstOrDefault();
            QuestionRequestModel questionRequestModel = new QuestionRequestModel();
            questionRequestModel.ExamId = question.ExamId;
            if (question.Image != null)
            {
                questionRequestModel.Image = question.Image.Split('\\').Last();
            }
            questionRequestModel.LessonId = question.LessonId;
            questionRequestModel.TopicId = question.TopicId;
            questionRequestModel.Desc = question.Desc;
            questionRequestModel.Text = question.Text;
            questionRequestModel.RowNo = question.RowNo;
            questionRequestModel.Point = question.Point;
            var options = question.Options.OrderBy(x => x.Id).ToList();
            questionRequestModel.Option1 = options[0].OptionText;
            questionRequestModel.Option1State = options[0].IsCorrect;
            questionRequestModel.Option2 = options[1].OptionText;
            questionRequestModel.Option2State = options[1].IsCorrect;
            questionRequestModel.Option3 = options[2].OptionText;
            questionRequestModel.Option3State = options[2].IsCorrect;
            questionRequestModel.Option4 = options[3].OptionText;
            questionRequestModel.Option4State = options[3].IsCorrect;
            questionRequestModel.Option5 = options[4].OptionText;
            questionRequestModel.Option5State = options[4].IsCorrect;
            return Json(new { Question = questionRequestModel });
        }

        public class UpdQuestionRequestModel
        {
            public int QuesId { get; set; }
            public int RowNo { get; set; }
            public int Point { get; set; }
            public string Desc { get; set; }
            public string Text { get; set; }
            public string Image { get; set; }
            public int ExamId { get; set; }
            public int LessonId { get; set; }
            public int TopicId { get; set; }
            public string Option1 { get; set; }
            public string Option2 { get; set; }
            public string Option3 { get; set; }
            public string Option4 { get; set; }
            public string Option5 { get; set; }
            public bool Option1State { get; set; }
            public bool Option2State { get; set; }
            public bool Option3State { get; set; }
            public bool Option4State { get; set; }
            public bool Option5State { get; set; }
        }

        [HttpPost]
        public JsonResult UpdateQuestion(UpdQuestionRequestModel requestModel)
        {
            Question question = db.Questions.Where(x => x.Id == requestModel.QuesId).FirstOrDefault();
            question.RowNo = requestModel.RowNo;
            question.Point = requestModel.Point;
            question.Desc = requestModel.Desc;
            question.Text = requestModel.Text;
            HttpFileCollectionBase files = Request.Files;
            if (files.Count > 0)
            {
                HttpPostedFileBase file = files[0];
                string extension = Path.GetExtension(file.FileName);
                string guid = Guid.NewGuid().ToString() + file.FileName.Split('.')[0] + extension.ToLower();
                string path = Server.MapPath(@"~/Content/Uploads/Questions/");
                bool folderExists = Directory.Exists(path);
                if (!folderExists) { Directory.CreateDirectory(path); }
                file.SaveAs(Path.Combine(path, guid));
                question.Image = Path.Combine(path, guid);
            }
            else
            {
                question.Image = null;
            }
            question.ExamId = requestModel.ExamId;
            question.LessonId = requestModel.LessonId;
            question.TopicId = requestModel.TopicId;
            List<Option> options = db.Options.Where(x => x.QuestionId == question.Id).OrderBy(x => x.Id).ToList();
            Option option1 = options[0];
            option1.OptionText = requestModel.Option1;
            option1.IsCorrect = requestModel.Option1State;
            option1.QuestionId = question.Id;
            Option option2 = options[1];
            option2.OptionText = requestModel.Option2;
            option2.IsCorrect = requestModel.Option2State;
            option2.QuestionId = question.Id;
            Option option3 = options[2];
            option3.OptionText = requestModel.Option3;
            option3.IsCorrect = requestModel.Option3State;
            option3.QuestionId = question.Id;
            Option option4 = options[3];
            option4.OptionText = requestModel.Option4;
            option4.IsCorrect = requestModel.Option4State;
            option4.QuestionId = question.Id;
            Option option5 = options[4];
            option5.OptionText = requestModel.Option5;
            option5.IsCorrect = requestModel.Option5State;
            option5.QuestionId = question.Id;
            options.Add(option1);
            options.Add(option2);
            options.Add(option3);
            options.Add(option4);
            options.Add(option5);
            question.Options = options;
            try
            {
                db.SaveChanges();
                return Json(new { IsSuccess = true, Message = "Kayıt Başarılı!" });
            }
            catch (Exception e)
            {
                return Json(new { IsSuccess = false, Message = "Kayıt Başarısız!" });
            }
        }
    }
}