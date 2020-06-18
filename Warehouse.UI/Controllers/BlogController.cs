using System.Linq;
using System.Web.Mvc;
using Warehouse.Models;
using Warehouse.Services.Interface;
using PagedList;
using Warehouse.Entities;
using System;

namespace Warehouse.Controllers
{
    [RoutePrefix("blog")]
    public class BlogController : Controller
    {
        readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [Route("")]
        public ActionResult Index(int? page)
        {
            ViewBag.ActiveBog = "active";
            return View(_blogService.GetListByDisplay(true).OrderByDescending(x => x.Id).ToPagedList(page ?? 1, 3));
        }

        [Route("{alias}")]
        public ActionResult Detail(string alias)
        {
            ViewBag.ActiveBog = "active";
            Blog blog = _blogService.GetByAlias(alias, true);

            if (blog == null)
                return Redirect("/pages/404");

            return View(blog);
        }

        [Route("demo/{alias}")]
        public ActionResult DetailDemo(string alias)
        {
            ViewBag.ActiveBog = "active";
            Blog blog = _blogService.GetByAlias(alias);

            if (blog == null)
                return Redirect("/pages/404");

            return View("Detail",blog);
        }

        [ChildActionOnly]
        public PartialViewResult _BlogSidebarPartial()
        {
            ViewBag.ArticlesHightLight = _blogService.GetListHighLight();
            return PartialView();
        }

        [Route("tim-kiem")]
        public ActionResult Search()
        {
            return RedirectToAction("Index");
        }

        [Route("tim-kiem")]
        [HttpPost]
        public ActionResult Search(string keyword)
        {
            ViewBag.ActiveBog = "active";
            return View(_blogService.GetListByDisplay(true).Where(x => x.Title.ToLower().Contains(keyword.ToLower()) || keyword == "").ToList());
        }


    }
}