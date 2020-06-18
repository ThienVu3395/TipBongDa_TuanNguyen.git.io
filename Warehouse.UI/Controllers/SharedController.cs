using DevTrends.MvcDonutCaching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Warehouse.Services.Interface;

namespace Warehouse.Controllers
{
    public class SharedController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ILanguageService _languageService;
        private readonly IBlogService _blogService;
        private readonly IHandbookService _handbookService;

        public SharedController(IHandbookService handbookService, IBlogService blogService, IArticleService articleService, ILanguageService languageService)
        {
            _articleService = articleService;
            _languageService = languageService;
            _blogService = blogService;
            _handbookService = handbookService;
        }

        [ChildActionOnly]
        public PartialViewResult _HeaderPartial()
        {
            return PartialView(Session["InfoWeb"]);  // truyền thêm Session lưu thông tin của shop
        }

        [ChildActionOnly]
        public PartialViewResult _FooterPartial()
        {
            ViewBag.Blogs = _blogService.GetNewBlogs(5);
            ViewBag.Articles = _articleService.GetByArticleCategoryId(4, true);
            return PartialView(Session["InfoWeb"]);
        }

        [ChildActionOnly]
        public PartialViewResult _MenuPartial()
        {
            return PartialView(Session["InfoWeb"]);
        }


        [HttpPost]
        public ContentResult ChangeLanguage()
        {
            var cacheManager = new OutputCacheManager();
            cacheManager.RemoveItems();
            return Content("success");
        }

    }
}