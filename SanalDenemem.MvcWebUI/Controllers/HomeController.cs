using SanalDenemem.MvcWebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SanalDenemem.MvcWebUI.Controllers
{
    public class HomeController : BaseController
    {

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Pricing()
        {
            return View();
        }

        public ActionResult ExamTypes()
        {
            return View(db.ExamTypes.ToList());
        }

        public ActionResult Exams(int? id)
        {
            if (id == null)
            {
                return View(@"~/Views/Shared/Error.cshtml", new string[] { "Hatalı Sayfa" });
            }
            return View(db.Exams.Where(x => x.ExamTypeId == id).ToList());
        }

        public ActionResult Blog()
        {
            return View();
        }
    }
}