using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SanalDenemem.MvcWebUI.Entity
{
    public class MemberExam
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public bool State { get; set; }
        public int CorrectCount { get; set; }
        public int FailCount { get; set; }
        public double Score { get; set; }
        public List<SubMemberExam> SubMemberExams { get; set; }
    }
}