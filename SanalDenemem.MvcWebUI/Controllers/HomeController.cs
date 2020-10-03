using Microsoft.AspNet.Identity;
using SanalDenemem.MvcWebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SanalDenemem.MvcWebUI.Controllers
{
    public class HomeController : BaseController
    {

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Pricing()
        {
            return View();
        }

        public ActionResult ExamTypes()
        {
            return View(db.ExamTypes.ToList());
        }

        public ActionResult Exams(int? id)
        {
            if (id == null)
            {
                return View(@"~/Views/Shared/Error.cshtml", new string[] { "Hatalı Sayfa" });
            }
            return View(db.Exams.Where(x => x.ExamTypeId == id).ToList());
        }

        public ActionResult ExamQuestionTransition(int? id)
        {
            if (id == null)
            {
                return View(@"~/Views/Shared/Error.cshtml", new string[] { "Hatalı Sayfa" });
            }
            return View(db.Exams.Where(x => x.Id == id).FirstOrDefault());
        }

        public ActionResult ExamQuestion(int? id)
        {
            if (id == null)
            {
                return View(@"~/Views/Shared/Error.cshtml", new string[] { "Hatalı Sayfa" });
            }
            var list = db.Questions.Where(x => x.ExamId == id).ToList();
            foreach (var item in list)
            {
                item.Exam = db.Exams.Where(x => x.Id == id).FirstOrDefault();
                if (item.Image != null)
                {
                    item.Image = item.Image.Split('\\').Last();
                }
            }
            int member = db.Members.Where(x => x.UserName == User.Identity.Name).FirstOrDefault().Id;

            return View(Tuple.Create<int, List<Question>>(member, list));
        }

        public class SubResult
        {
            public int examId { get; set; }
            public int quesId { get; set; }
            public int optId { get; set; }
            public int memberId { get; set; }
        }

        public ActionResult ExamResult(List<SubResult> dizi)
        {
            int correctCount = 0;
            int falseCount = 0;
            foreach (SubResult item in dizi)
            {
                bool isCorrect = db.Options.Where(x => x.Id == item.optId && x.QuestionId == item.quesId).FirstOrDefault().IsCorrect;
                if (isCorrect) { correctCount++; }
                else { falseCount++; }

            }


            return View();
        }

        public ActionResult Blog()
        {
            return View();
        }
    }
}