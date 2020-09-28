using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SanalDenemem.MvcWebUI.Entity
{
    public class Lesson
    {
        public int Id { get; set; }
        [DisplayName("Ders İsmi")]
        public string LessonName { get; set; }
        public virtual List<Topic> Topics { get; set; }
        public virtual List<Question> Questions { get; set; }
    }
}