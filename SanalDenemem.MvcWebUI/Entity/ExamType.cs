using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SanalDenemem.MvcWebUI.Entity
{
    public class ExamType
    {
        //kpss ygs gibi
        public int Id { get; set; }
        [DisplayName("Sınav Tipi İsmi")]
        public string ExamTypeName { get; set; }
        [DisplayName("Açıklama")]
        public string Desc { get; set; }
        public virtual List<Exam> Exams { get; set; }
    }
}