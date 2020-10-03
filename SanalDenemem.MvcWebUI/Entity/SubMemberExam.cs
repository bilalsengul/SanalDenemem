using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SanalDenemem.MvcWebUI.Entity
{
    public class SubMemberExam
    {
        public int Id { get; set; }
        public int MemberExamId { get; set; }
        public int QuestionId { get; set; }
        public int CorrectOptionId { get; set; }
        public int SelectedOptionId { get; set; }
    }
}