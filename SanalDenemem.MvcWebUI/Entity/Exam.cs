using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SanalDenemem.MvcWebUI.Entity
{
    public class Exam
    {
        public int Id { get; set; }
        [DisplayName("Sınav İsmi")]
        public string Title { get; set; }
        [DisplayName("Sınav Süresi")]
        public int ExamTime { get; set; }
        public virtual List<MemberExam> MemberExams { get; set; }
        public virtual List<Question> Questions { get; set; }
        public int ExamTypeId { get; set; }
        public ExamType ExamType { get; set; }
    }
}