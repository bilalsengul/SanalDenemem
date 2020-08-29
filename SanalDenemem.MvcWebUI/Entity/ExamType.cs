using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SanalDenemem.MvcWebUI.Entity
{
    public class ExamType
    {
        //kpss ygs gibi
        public int Id { get; set; }
        public string ExamTypeName { get; set; }
        public string Desc { get; set; }
        public List<Exam> Exams { get; set; }
    }
}