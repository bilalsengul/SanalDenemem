using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SanalDenemem.MvcWebUI.Entity
{
    public class Question
    {
        public int Id { get; set; }
        public int RowNo { get; set; }
        public string Desc { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public int ExamId { get; set; }
        public int TopicId { get; set; }
        public Exam Exam { get; set; }
        public Topic Topic { get; set; }
        public List<Option> Options { get; set; }

    }
}