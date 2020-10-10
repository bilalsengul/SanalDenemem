using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SanalDenemem.MvcWebUI.Entity;
namespace SanalDenemem.MvcWebUI.Models
{
    public class ExamResultSub
    {
        public Member member { get; set; }
        public List<MemberExam> memberExams { get; set; }
        public List<Exam> exams  { get; set; }
        public List<ExamType> ExamTypes  { get; set; }
    }
}