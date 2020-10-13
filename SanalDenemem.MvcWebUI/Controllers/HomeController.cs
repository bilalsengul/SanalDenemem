using Microsoft.AspNet.Identity;
using SanalDenemem.MvcWebUI.Entity;
using SanalDenemem.MvcWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        [HttpPost]
        public JsonResult GetExamsByExamType(int examTypeId)
        {
            List<Exam> ExamList = db.Exams.Where(x => x.ExamTypeId == examTypeId).ToList();
            return Json(ExamList);
        }

        public ActionResult ExamsShowResult()
        {
            var list = db.ExamTypes.ToList();

            ViewBag.ExamTypeList = new SelectList(list, "Id", "ExamTypeName");
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

        [Authorize(Roles = "premiumMember")]
        public ActionResult ExamQuestionTransition(int? id)
        {
            if (id == null)
            {
                return View(@"~/Views/Shared/Error.cshtml", new string[] { "Hatalı Sayfa" });
            }
            int memberId = db.Members.Where(x => x.UserName == User.Identity.Name).FirstOrDefault().Id;
            if (db.MemberExams.Where(x => x.MemberId == memberId && x.ExamId == id).FirstOrDefault()!=null)
            {
                return View(@"~/Views/Shared/Error.cshtml", new string[] { "Sınava daha önce giriş yaptınız." });
            }
            return View(db.Exams.Where(x => x.Id == id).FirstOrDefault());
        }

        [Authorize(Roles = "premiumMember")]
        public ActionResult ExamQuestion(int? id)
        {
            int memberId = db.Members.Where(x => x.UserName == User.Identity.Name).FirstOrDefault().Id;
            if (db.MemberExams.Where(x => x.MemberId == memberId && x.ExamId == id).FirstOrDefault() != null)
            {
                return View(@"~/Views/Shared/Error.cshtml", new string[] { "Sınava daha önce giriş yaptınız." });
            }
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

        public JsonResult ExamResult(List<SubResult> dizi)
        {
            MemberExam memberExam = new MemberExam();
            List<SubMemberExam> subMemberExams = new List<SubMemberExam>();
            int correctCount = 0;
            int failCount = 0;
            memberExam.State = true;
            memberExam.SubMemberExams = subMemberExams;
            memberExam.MemberId = dizi[0].memberId;
            memberExam.ExamId = dizi[0].examId;
            memberExam.CorrectCount = correctCount;
            memberExam.FailCount = failCount;
            memberExam.Score = correctCount - failCount * 0.25;
            db.MemberExams.Add(memberExam);
            foreach (SubResult item in dizi)
            {
                bool isCorrect = db.Options.Where(x => x.Id == item.optId && x.QuestionId == item.quesId).FirstOrDefault().IsCorrect;
                if (isCorrect) { correctCount++; }
                else { failCount++; }

                SubMemberExam subMemberExam = new SubMemberExam();
                subMemberExam.MemberExamId = memberExam.Id;
                subMemberExam.QuestionId = item.quesId;
                subMemberExam.SelectedOptionId = item.optId;
                subMemberExam.CorrectOptionId = db.Options.Where(x => x.QuestionId == item.quesId && x.IsCorrect == true).FirstOrDefault().Id;
                db.SubMemberExams.Add(subMemberExam);
                subMemberExams.Add(subMemberExam);
                db.SaveChanges();
            }
            var a = db.MemberExams.Where(x => x.Id == memberExam.Id).FirstOrDefault();
            a.FailCount = failCount;
            a.CorrectCount = correctCount;
            a.Score = correctCount - failCount * 0.25;
            db.SaveChanges();

            return Json(new { IsSuccess = true, Message = "Kayıt Başarılı!" });
        }


        public ActionResult ExamResutlDetails(int? id)
        {
            Member member = null;
            List<ExamResultDetails> examResults = new List<ExamResultDetails>();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            foreach (var examResult in db.MemberExams.Where(i => i.ExamId == id).ToList())
            {
                member = db.Members.FirstOrDefault(i => i.Id == examResult.MemberId);
                examResults.Add(new ExamResultDetails()
                {
                    Name = member.Name,
                    Surname = member.Surname,
                    UserName = member.UserName,
                    Score = examResult.Score
                });
            }
            return View(examResults.OrderByDescending(i=>i.Score).ToList());
        }
        
        public ActionResult Blog()
        {
            return View();
        }
    }
}