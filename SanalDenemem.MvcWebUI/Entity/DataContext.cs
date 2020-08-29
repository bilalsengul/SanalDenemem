using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
namespace SanalDenemem.MvcWebUI.Entity
{
    public class DataContext:DbContext
    {
        public DataContext():base("DataConnection")
        {
            //initializer clasını global.asax ekledim.
        }

        // public DbSet<class ismi> MyProperty { get; set; }  bu şekild tabloları ekle.
        public DbSet<Member> Members { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamType> ExamTypes { get; set; }
        public DbSet<MemberExam> MemberExams { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
    }
}