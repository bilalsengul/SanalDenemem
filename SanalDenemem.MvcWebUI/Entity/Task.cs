using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SanalDenemem.MvcWebUI.Entity
{
    public class Task
    {
        public int Id { get; set; }
        [DisplayName("Başlık")]
        public string Title { get; set; }
        [DisplayName("Acıklama")]
        public string  Description { get; set; }
        [DisplayName("Tamamlandı mı ?")]
        public bool Done { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
    }
}