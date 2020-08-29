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
    public class DataInitializer: CreateDatabaseIfNotExists<DbContext>
    {
        protected override void Seed(DbContext context)
        {
            //burda classlarımı liste olarak oluştur ve verileri ekle ardından foreach ile gez contexdeki listeye add yap
            //ve foreach dan cıkınca saveChange yap. her tablo icin ayrı ayrı yap bunu. ardından classı contexde ctorda belirt.
            
            base.Seed(context);
        }
    }
}