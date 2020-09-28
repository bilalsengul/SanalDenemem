using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SanalDenemem.MvcWebUI.Entity
{
    //eger data base modelinde degişiklik yaptıysam altaki satırdan kalıtım al.
    //DropCreateDatabaseIfModelChanges<DbContext>
    //CreateDatabaseIfNotExists<DbContext>
    public class DataInitializer : CreateDatabaseIfNotExists<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            //burda classlarımı liste olarak oluştur ve verileri ekle ardından foreach ile gez contexdeki listeye add yap
            //ve foreach dan cıkınca saveChange yap. her tablo icin ayrı ayrı yap bunu. ardından classı contexde ctorda belirt.

            //SINAV TİPLERİ
            var examTypes = new List<ExamType>{
                new ExamType{ExamTypeName="KPSS-1", Desc="Kamu Personeli Seçme Sınavı - 1"},
                new ExamType{ExamTypeName="KPSS-2", Desc="Kamu Personeli Seçme Sınavı - 2"},
                new ExamType{ExamTypeName="KPSS-3", Desc="Kamu Personeli Seçme Sınavı - 3"},
                new ExamType{ExamTypeName="KPSS-4", Desc="Kamu Personeli Seçme Sınavı - 4"}
            };
            examTypes.ForEach(s => context.ExamTypes.Add(s));
            context.SaveChanges();

            //SINAVLAR
            var exams = new List<Exam>{
                new Exam{ExamTypeId=1,Title="A Denemesi"},
                new Exam{ExamTypeId=1,Title="B Denemesi"},
                new Exam{ExamTypeId=2,Title="C Denemesi"},
                new Exam{ExamTypeId=2,Title="D Denemesi"},
                new Exam{ExamTypeId=3,Title="E Denemesi"},
                new Exam{ExamTypeId=3,Title="F Denemesi"},
                new Exam{ExamTypeId=4,Title="G Denemesi"},
                new Exam{ExamTypeId=4,Title="H Denemesi"}
            };
            exams.ForEach(s => context.Exams.Add(s));
            context.SaveChanges();

            //DERSLER
            var lessons = new List<Lesson>{
                new Lesson{LessonName="Türkçe"},
                new Lesson{LessonName="Matematik"},
                new Lesson{LessonName="Tarih"},
                new Lesson{LessonName="Din Bilgisi"},
                new Lesson{LessonName="Fizik"}
            };
            lessons.ForEach(s => context.Lessons.Add(s));
            context.SaveChanges();

            //KONULAR
            var topics = new List<Topic>{
                new Topic{LessonId=1, TopicName="-", Desc="-"},
                new Topic{LessonId=1, TopicName="Yazım Kuralları", Desc="Yazım Kuralları"},
                new Topic{LessonId=2, TopicName="-", Desc="-"},
                new Topic{LessonId=2, TopicName="Rasyonel Sayılar", Desc="Rasyonel Sayılar"},
                new Topic{LessonId=2, TopicName="İntegral", Desc="İntegral"},
                new Topic{LessonId=2, TopicName="Türev", Desc="Türev"},
                new Topic{LessonId=3, TopicName="-", Desc="-"},
                new Topic{LessonId=4, TopicName="-", Desc="-"},
                new Topic{LessonId=5, TopicName="-", Desc="-"}
            };
            topics.ForEach(s => context.Topics.Add(s));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}