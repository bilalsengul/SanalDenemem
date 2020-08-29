using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SanalDenemem.MvcWebUI.Entity
{
    public class Exam
    {
        /*public Exam()
        {
            this.Members = new HashSet<Member>();
        }*/
        public int Id { get; set; }
        public string Title { get; set; }
        public List<MemberExam> MemberExams { get; set; }
        //public virtual ICollection<Member> Members { get; set; }
        public List<Question> Questions { get; set; }
        public int ExamTypeId { get; set; }
        public ExamType ExamType { get; set; }
       
    }
}