using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Warehouse.Entities;
using Warehouse.Services.Interface;
using Warehouse.Models;
using PagedList;

namespace Warehouse.Controllers
{
    [RoutePrefix("tin-tuc")]
    public class ArticleController : Controller
    {
        readonly IArticleService _articleService;
        readonly IArticleCategoryService _articleCategoryService;

        public ArticleController(IArticleService articleService, IArticleCategoryService articleCategoryService)
        {
            _articleService = articleService;
            _articleCategoryService = articleCategoryService;
        }

        [Route("{alias}")]
        public ActionResult Index(string alias, int? page)
        {
            ViewBag.Category = _articleCategoryService.GetByAlias(alias);
            if(ViewBag.Category == null)
            {
                return Redirect("/pages/404");
            }
            List<Article> articles = _articleService.GetByArticleCategoryId(((ArticleCategory)ViewBag.Category).Id, true);
            ViewBag.hotArticles = _articleService.GetByHot(((ArticleCategory)ViewBag.Category).Id).ToPagedList(page ?? 1, 5);
            return View(articles.ToPagedList(page ?? 1, 1));
        }

        [Route("xem-tin/{alias}.html")]
        public ActionResult Details(string alias)
        {
            Article article = _articleService.GetByAlias(alias, true);
            if (article == null)
            {
                return Redirect("/pages/404");
            }
            ViewBag.hotArticles = _articleService.GetByHot(article.ArticleCategoryId).ToPagedList(1, 5);
            return View(article);
        }


    }
}