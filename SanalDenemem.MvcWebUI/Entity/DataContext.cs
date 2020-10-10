using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
namespace SanalDenemem.MvcWebUI.Entity
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DataConnection")
        {
            //initializer clasını global.asax ekledim.
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exam>()
            .HasRequired(i => i.ExamType)
            .WithMany(u => u.Exams)
            .HasForeignKey(i => i.ExamTypeId)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
            .HasRequired(i => i.Exam)
            .WithMany(u => u.Questions)
            .HasForeignKey(i => i.ExamId)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Option>()
            .HasRequired(i => i.Question)
            .WithMany(u => u.Options)
            .HasForeignKey(i => i.QuestionId)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Topic>()
            .HasRequired(i => i.Lesson)
            .WithMany(u => u.Topics)
            .HasForeignKey(i => i.LessonId)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<SubMemberExam>()
            .HasRequired(i => i.MemberExam)
            .WithMany(u => u.SubMemberExams)
            .HasForeignKey(i => i.MemberExamId)
            .WillCascadeOnDelete(false);
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
        public DbSet<Task> Tasks { get; set; }
        public DbSet<SubMemberExam> SubMemberExams { get; set; }

        public DbSet<Contact>Contacts { get; set; }
    }
}