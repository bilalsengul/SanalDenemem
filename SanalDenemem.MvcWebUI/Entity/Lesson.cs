using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SanalDenemem.MvcWebUI.Entity
{
    public class Lesson
    {
        public int Id { get; set; }
        public string LessonName { get; set; }
        public List<Topic> Topics { get; set; }
    }
}