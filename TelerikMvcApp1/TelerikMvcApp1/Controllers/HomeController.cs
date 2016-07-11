using DAL.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TelerikMvcApp1.Controllers
{
    public class HomeController : Controller
    {
        [Inject]
        public IDAL Dal { get; set; }

        //[Inject]
        //public ICategoriesDAL CategoriesDal { get; set; }

        //[Inject]
        //public IDocumentsDAL DocumentsDal { get; set; }

        public ActionResult Index()
        {

            ViewBag.Message = Dal.GetAllDocuments().ToList();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region [Dispose]

        protected override void Dispose(bool disposing)
        {            
            Dal.Dispose();            
            base.Dispose(disposing);
        }

        #endregion
    }
}
