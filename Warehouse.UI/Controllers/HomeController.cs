using DevTrends.MvcDonutCaching;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Warehouse.Entities;
using Warehouse.Models;
using Warehouse.Services.Interface;

namespace Warehouse.Controllers
{
    public class HomeController : Controller
    {
        IArticleService _articleService;
        IArticleCategoryService _articleCategoryService;
        ISlideService _slideService;
        IInfoWebService _infoWebService;
        IBlogService _blogService;
        IHandbookService _handbookService;

        public HomeController(IHandbookService handbookService, IBlogService blogService, IInfoWebService infoWebService, ISlideService slideService, IArticleService articleService, IArticleCategoryService articleCategoryService)
        {
            _articleService = articleService;
            _articleCategoryService = articleCategoryService;
            _slideService = slideService;
            _infoWebService = infoWebService;
            _blogService = blogService;
            _handbookService = handbookService;
        }

        public HomeController()
        {

        }


        // [DonutOutputCache(Duration = 86400, Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult Index(int? page)
        {
            ViewBag.Mess1 = "active";
            ViewBag.Mess2 = "";
            ViewBag.Mess3 = "";
            ViewBag.Mess4 = "";
            ViewBag.Mess5 = "";
            List<Article> newArticles = _articleService.GetByArticleCategoryId(1, true);
            ViewBag.Slides = _slideService.GetByStatus(true);
            ViewBag.InfoWeb = _infoWebService.GetFirst();
            ViewBag.Blogs = _blogService.GetNewBlogs(6);
            ViewBag.Handbooks = _handbookService.GetNewHandbooks(6);
            return View(newArticles.Take(6).ToList());
        }

        //[Route("lich-thi-dau")]
        //public ActionResult Fixtures()
        //{
        //    ViewBag.Mess1 = "";
        //    ViewBag.Mess2 = "active";
        //    ViewBag.Mess3 = "";
        //    ViewBag.Mess4 = "";
        //    ViewBag.Mess5 = "";
        //    return View();
        //}

        //[Route("ket-qua")]
        //public ActionResult KetQua()
        //{
        //    ViewBag.Mess1 = "";
        //    ViewBag.Mess2 = "";
        //    ViewBag.Mess3 = "active";
        //    ViewBag.Mess4 = "";
        //    ViewBag.Mess5 = "";

        //    return View();
        //}

        //[Route("bang-diem")]
        //public ActionResult BangDiem()
        //{
        //    ViewBag.Mess1 = "";
        //    ViewBag.Mess2 = "";
        //    ViewBag.Mess3 = "";
        //    ViewBag.Mess4 = "active";
        //    ViewBag.Mess5 = "";

        //    return View();
        //}

        public ViewResult About()
        {
            ViewBag.InfoWeb = _infoWebService.GetFirst();
            ViewBag.Slides = _slideService.GetByStatus(true);
            ViewBag.Mess1 = "";
            ViewBag.Mess2 = "";
            ViewBag.Mess3 = "active";
            ViewBag.Mess4 = "";
            ViewBag.Mess5 = "";
            return View();
        }

    }
}