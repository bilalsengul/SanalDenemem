using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        [ForeignKey("Lesson")]
        public int LessonId { get; set; }
        [ForeignKey("Topic")]
        public int TopicId { get; set; }
        public Exam Exam { get; set; }
        public Topic Topic { get; set; }
        public Lesson Lesson { get; set; }
        public virtual List<Option> Options { get; set; }

    }
}