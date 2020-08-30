using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SanalDenemem.MvcWebUI.Entity
{
    public class Topic
    {
        public int Id { get; set; }
        [DisplayName("Konu İsmi")]
        public string TopicName { get; set; }
        [DisplayName("Açıklama")]
        public string Desc { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public List<Question> Questions { get; set; }
    }
}