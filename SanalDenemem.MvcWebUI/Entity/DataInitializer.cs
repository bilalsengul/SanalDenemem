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

            ////SINAV TİPLERİ
            //var examTypes = new List<ExamType>{
            //    new ExamType{ExamTypeName="KPSS-1", Desc="Kamu Personeli Seçme Sınavı - 1"},
            //    new ExamType{ExamTypeName="KPSS-2", Desc="Kamu Personeli Seçme Sınavı - 2"},
            //    new ExamType{ExamTypeName="KPSS-3", Desc="Kamu Personeli Seçme Sınavı - 3"},
            //    new ExamType{ExamTypeName="KPSS-4", Desc="Kamu Personeli Seçme Sınavı - 4"},
            //    new ExamType{ExamTypeName="YKS", Desc="Yükseköğretim Kurumları Sınavı"},
            //    new ExamType{ExamTypeName="AYT", Desc="Alan Yeterlilik Testi"},
            //    new ExamType{ExamTypeName="TYT", Desc="Temel Yeterlilik Testi"},
            //    new ExamType{ExamTypeName="DHPT", Desc="Din Hizmetleri Alan Bilgisi Testi"}
            //};
            //examTypes.ForEach(s => context.ExamTypes.Add(s));
            //context.SaveChanges();

            ////SINAVLAR
            //var exams = new List<Exam>{
            //    new Exam{ExamTypeId=1,Title="A Denemesi", ExamTime=20},
            //    new Exam{ExamTypeId=1,Title="B Denemesi", ExamTime=50},
            //    new Exam{ExamTypeId=2,Title="C Denemesi", ExamTime=40},
            //    new Exam{ExamTypeId=2,Title="D Denemesi", ExamTime=50},
            //    new Exam{ExamTypeId=3,Title="E Denemesi", ExamTime=60},
            //    new Exam{ExamTypeId=3,Title="F Denemesi", ExamTime=60},
            //    new Exam{ExamTypeId=4,Title="G Denemesi", ExamTime=30},
            //    new Exam{ExamTypeId=4,Title="H Denemesi", ExamTime=10},
            //    new Exam{ExamTypeId=5,Title="J Denemesi", ExamTime=30},
            //    new Exam{ExamTypeId=5,Title="K Denemesi", ExamTime=10},
            //    new Exam{ExamTypeId=6,Title="L Denemesi", ExamTime=30},
            //    new Exam{ExamTypeId=6,Title="M Denemesi", ExamTime=10},
            //    new Exam{ExamTypeId=7,Title="N Denemesi", ExamTime=30},
            //    new Exam{ExamTypeId=7,Title="O Denemesi", ExamTime=10},
            //    new Exam{ExamTypeId=8,Title="P Denemesi", ExamTime=30},
            //    new Exam{ExamTypeId=8,Title="R Denemesi", ExamTime=10}
            //};
            //exams.ForEach(s => context.Exams.Add(s));
            //context.SaveChanges();

            ////DERSLER
            //var lessons = new List<Lesson>{
            //    new Lesson{LessonName="Türkçe"},
            //    new Lesson{LessonName="Matematik"},
            //    new Lesson{LessonName="Tarih"},
            //    new Lesson{LessonName="Din Bilgisi"},
            //    new Lesson{LessonName="Fizik"},
            //    new Lesson{LessonName="Biyoloji"},
            //    new Lesson{LessonName="Kimya"},
            //    new Lesson{LessonName="Geometri"}
            //};
            //lessons.ForEach(s => context.Lessons.Add(s));
            //context.SaveChanges();

            ////KONULAR
            //var topics = new List<Topic>{
            //    new Topic{LessonId=1, TopicName="-", Desc="-"},
            //    new Topic{LessonId=1, TopicName="Yazım Kuralları", Desc="Yazım Kuralları"},
            //    new Topic{LessonId=2, TopicName="-", Desc="-"},
            //    new Topic{LessonId=2, TopicName="Rasyonel Sayılar", Desc="Rasyonel Sayılar"},
            //    new Topic{LessonId=2, TopicName="İntegral", Desc="İntegral"},
            //    new Topic{LessonId=2, TopicName="Türev", Desc="Türev"},
            //    new Topic{LessonId=3, TopicName="-", Desc="-"},
            //    new Topic{LessonId=4, TopicName="-", Desc="-"},
            //    new Topic{LessonId=5, TopicName="-", Desc="-"},
            //    new Topic{LessonId=6, TopicName="-", Desc="-"},
            //    new Topic{LessonId=7, TopicName="-", Desc="-"},
            //    new Topic{LessonId=8, TopicName="-", Desc="-"},
            //    new Topic{LessonId=8, TopicName="-", Desc="Üçgen"},
            //    new Topic{LessonId=8, TopicName="-", Desc="Dörtgen"}
            //};
            //topics.ForEach(s => context.Topics.Add(s));
            //context.SaveChanges();

            ////SORULAR
            //var questions = new List<Question>{
            //    new Question{Desc="Bilal uzun biridir.", Text="Bilalin boyu kaçtır?", ExamId=1, LessonId=1, TopicId=2, RowNo=1},
            //    new Question{Desc="Yunus uzun biridir.", Text="Yunusun boyu kaçtır?", ExamId=1, LessonId=1, TopicId=2, RowNo=2},
            //    new Question{Desc="Ümit uzun biridir.", Text="Ümitin boyu kaçtır?", ExamId=1, LessonId=1, TopicId=2, RowNo=3},
            //    new Question{Desc="Alfabenin 2. harfidir.", Text="A'dan sonra gelen harf?", ExamId=9, LessonId=1, TopicId=2, RowNo=1},
            //    new Question{Desc="Alfabenin 3. harfidir.", Text="B'dan sonra gelen harf?", ExamId=9, LessonId=1, TopicId=2, RowNo=2},
            //    new Question{Desc="Alfabenin 4. harfidir.", Text="C'dan sonra gelen harf?", ExamId=9, LessonId=8, TopicId=2, RowNo=3},
            //    new Question{Desc="Alfabenin 5. harfidir.", Text="D'dan sonra gelen harf?", ExamId=9, LessonId=8, TopicId=2, RowNo=4}
            //};
            //questions.ForEach(s => context.Questions.Add(s));
            //context.SaveChanges();

            ////SEÇENEKLER
            //var options = new List<Option>{
            //    new Option{OptionText="180cm", QuestionId=1, IsCorrect=false},
            //    new Option{OptionText="185cm", QuestionId=1, IsCorrect=false},
            //    new Option{OptionText="190cm", QuestionId=1, IsCorrect=true},
            //    new Option{OptionText="195cm", QuestionId=1, IsCorrect=false},
            //    new Option{OptionText="200cm", QuestionId=1, IsCorrect=false},
            //    new Option{OptionText="180cm", QuestionId=2, IsCorrect=false},
            //    new Option{OptionText="185cm", QuestionId=2, IsCorrect=true},
            //    new Option{OptionText="190cm", QuestionId=2, IsCorrect=false},
            //    new Option{OptionText="195cm", QuestionId=2, IsCorrect=false},
            //    new Option{OptionText="200cm", QuestionId=2, IsCorrect=false},
            //    new Option{OptionText="180cm", QuestionId=3, IsCorrect=false},
            //    new Option{OptionText="185cm", QuestionId=3, IsCorrect=false},
            //    new Option{OptionText="190cm", QuestionId=3, IsCorrect=false},
            //    new Option{OptionText="195cm", QuestionId=3, IsCorrect=true},
            //    new Option{OptionText="200cm", QuestionId=3, IsCorrect=false},
            //    new Option{OptionText="A", QuestionId=4, IsCorrect=false},
            //    new Option{OptionText="B", QuestionId=4, IsCorrect=true},
            //    new Option{OptionText="C", QuestionId=4, IsCorrect=false},
            //    new Option{OptionText="D", QuestionId=4, IsCorrect=false},
            //    new Option{OptionText="E", QuestionId=4, IsCorrect=false},
            //    new Option{OptionText="A", QuestionId=5, IsCorrect=false},
            //    new Option{OptionText="B", QuestionId=5, IsCorrect=false},
            //    new Option{OptionText="C", QuestionId=5, IsCorrect=true},
            //    new Option{OptionText="D", QuestionId=5, IsCorrect=false},
            //    new Option{OptionText="E", QuestionId=5, IsCorrect=false},
            //    new Option{OptionText="A", QuestionId=6, IsCorrect=false},
            //    new Option{OptionText="B", QuestionId=6, IsCorrect=false},
            //    new Option{OptionText="C", QuestionId=6, IsCorrect=false},
            //    new Option{OptionText="Ç", QuestionId=6, IsCorrect=true},
            //    new Option{OptionText="D", QuestionId=6, IsCorrect=false},
            //    new Option{OptionText="A", QuestionId=7, IsCorrect=false},
            //    new Option{OptionText="B", QuestionId=7, IsCorrect=false},
            //    new Option{OptionText="C", QuestionId=7, IsCorrect=false},
            //    new Option{OptionText="D", QuestionId=7, IsCorrect=true},
            //    new Option{OptionText="E", QuestionId=7, IsCorrect=false}
            //};
            //options.ForEach(s => context.Options.Add(s));
            //context.SaveChanges();

            base.Seed(context);
        }
    }
}