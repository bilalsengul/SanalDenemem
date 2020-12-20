﻿using Microsoft.AspNet.Identity;
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
        public ActionResult SingleBlog()
        {
            return View();
        }
        public ActionResult Result()
        {
            return View();
        }
        public ActionResult Payment()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Register", "Account");
            }
            Member member = db.Members.FirstOrDefault(i => i.UserName == User.Identity.Name);
            return View(member);
        }

        public class IndexViewModel
        {
            public int SelectedExamTypeId { get; set; }
            public int SelectedExamId { get; set; }
        }

        public ActionResult ExamsShowResult()
        {
            ViewBag.ExamTypesData = new SelectList(db.ExamTypes, "Id", "ExamTypeName");
            ViewBag.ExamsData = new SelectList(db.Exams, "Id", "Title");
            IndexViewModel model = new IndexViewModel()
            {
                SelectedExamTypeId = 1,
                SelectedExamId = 1
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult ExamsShowResult(IndexViewModel model)
        {
            ViewBag.ExamTypesData = new SelectList(db.ExamTypes, "Id", "ExamTypeName");
            ViewBag.ExamsData = new SelectList(db.Exams, "Id", "Title");

            return View(model);
        }

        public class AlternativeExamModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int ExamTypeId { get; set; }
        }

        public JsonResult GetExamsByExamType(int id)
        {
            List<AlternativeExamModel> exams = new List<AlternativeExamModel>();
            foreach (var item in db.Exams.Where(x => x.ExamTypeId == id))
            {
                exams.Add(new AlternativeExamModel() { ExamTypeId = item.ExamTypeId, Id = item.Id, Title = item.Title });
            }
            return Json(exams, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult ExamResultDetails(int? id)
        {
            Member member = null;
            List<ExamResultDetails> examResults = new List<ExamResultDetails>();
            foreach (var examResult in db.MemberExams.Where(i => i.ExamId == id).ToList())
            {
                member = db.Members.FirstOrDefault(i => i.Id == examResult.MemberId);
                examResults.Add(new ExamResultDetails()
                {
                    Name = member.Name,
                    Surname = member.Surname,
                    UserName = member.UserName,
                    //Score = examResult.Score
                    TotalPoint = examResult.Total
                });
            }
            //return PartialView(@"~/Views/Shared/_ExamResultDetails.cshtml", examResults.OrderByDescending(i => i.Score).ToList());
            return PartialView(@"~/Views/Shared/_ExamResultDetails.cshtml", examResults.OrderByDescending(i => i.TotalPoint).ToList());
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
            var exams = db.Exams.Where(x => x.ExamTypeId == id && x.Questions.Count() > 0).ToList();
            foreach (var item in exams) { item.ExamType = db.ExamTypes.Where(x => x.Id == item.ExamTypeId).FirstOrDefault(); }
            return View(exams);
        }

        //[Authorize(Roles = "premiumMember")]
        public ActionResult ExamQuestionTransition(int? id)
        {
            if (id == null)
            {
                return View(@"~/Views/Shared/Error.cshtml", new string[] { "Hatalı Sayfa" });
            }
            int memberId = db.Members.Where(x => x.UserName == User.Identity.Name).FirstOrDefault().Id;
            if (db.MemberExams.Where(x => x.MemberId == memberId && x.ExamId == id).FirstOrDefault() != null)
            {
                return View(@"~/Views/Shared/Error.cshtml", new string[] { "Sınava daha önce giriş yaptınız." });
            }
            return View(db.Exams.Where(x => x.Id == id).FirstOrDefault());
        }

        //[Authorize(Roles = "premiumMember")]
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

        [HttpPost]
        public JsonResult ExamResult(List<SubResult> dizi)
        {
            MemberExam memberExam = new MemberExam();
            List<SubMemberExam> subMemberExams = new List<SubMemberExam>();
            int correctCount = 0;
            int failCount = 0;
            double total = 0;
            memberExam.State = true;
            memberExam.SubMemberExams = subMemberExams;
            memberExam.MemberId = dizi[0].memberId;
            memberExam.ExamId = dizi[0].examId;
            memberExam.CorrectCount = correctCount;
            memberExam.FailCount = failCount;
            memberExam.Score = correctCount - failCount * 0.25;
            memberExam.Total = total;
            if (db.MemberExams.Where(x => x.MemberId == memberExam.MemberId && x.ExamId == memberExam.ExamId).FirstOrDefault() == null)
            {
                db.MemberExams.Add(memberExam);
                foreach (SubResult item in dizi)
                {
                    bool isCorrect = db.Options.Where(x => x.Id == item.optId && x.QuestionId == item.quesId).FirstOrDefault().IsCorrect;
                    double point = db.Questions.Where(x => x.Id == item.quesId).FirstOrDefault().Point;
                    if (isCorrect)
                    {
                        correctCount++;
                        total = total + point;
                    }
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
                a.Total = total;
                db.SaveChanges();
            }

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
            return View(examResults.OrderByDescending(i => i.Score).ToList());
        }

        public ActionResult Blog()
        {
            return View();
        }
    }
}