using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SanalDenemem.MvcWebUI.Entity
{
    public class Topic
    {
        public int Id { get; set; }
        public string TopicName { get; set; }
        public string Desc { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public List<Question> Questions { get; set; }
    }
}