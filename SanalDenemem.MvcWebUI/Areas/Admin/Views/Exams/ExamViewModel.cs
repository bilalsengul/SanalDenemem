using SanalDenemem.MvcWebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SanalDenemem.MvcWebUI.Areas.Admin.Views.Exams
{
    public class ExamViewModel
    {
        public Exam Exam { get; set; }
        public List<Question> QuestionList { get; set; }
    }
}