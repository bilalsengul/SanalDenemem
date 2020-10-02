using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SanalDenemem.MvcWebUI.Entity
{
    public class Member
    {
       /* public Member()
        {
            this.Exames=new HashSet<Exam>();
        }*/
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        List<MemberExam>MemberExams { get; set; }
        List<Task>Tasks { get; set; }
        // public virtual ICollection<Exam> Exames { get; set; }
        //public string Email { get; set; }
        //public string Password { get; set; }
        public string ProfileImageName { get; set; }
        //public System.DateTime AddedDate { get; set; }
    }
}