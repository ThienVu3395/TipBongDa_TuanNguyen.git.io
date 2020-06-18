using DevTrends.MvcDonutCaching;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Warehouse.Entities;
using Warehouse.Models;
using Warehouse.Services.Interface;

namespace Warehouse.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ArticleController : Controller
    {
        IArticleService _articleService;
        IArticleCategoryService _articleCategoryService;
        readonly List<string> ImageExtensions = ConfigurationManager.AppSettings["ImageExtensions"].ToString().Split('|').ToList();

        public ArticleController(IArticleService articleService, IArticleCategoryService articleCategoryService)
        {
            _articleService = articleService;
            _articleCategoryService = articleCategoryService;
        }

        #region Get Alias 

        [HttpPost]
        public ContentResult GetAlias(string Title)
        {
            return Content(Functions.UnicodeToKoDauAndGach(Title));
        }
        #endregion

        public PartialViewResult _MenuArticlePartial()
        {
            return PartialView(_articleCategoryService.GetAll().OrderBy(x=>x.SortOrder).ToList());
        }

        #region CRUD
        public ViewResult Index(int articleCategoryId)
        {
            List<Article> articles = new List<Article>();
            articles = _articleService.GetByArticleCategoryId(articleCategoryId, null);
            var articleCategory = _articleCategoryService.GetById(articleCategoryId);
            ViewBag.Title = articleCategory.Name.ToUpperInvariant();          
            return View(articles);
        }

        public ActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Article Article = _articleService.GetById(Id.Value);
            if (Article == null)
                return Redirect("/pages/404");

            return View(Article);
        }

        public ActionResult Create(int articleCategoryId)
        {
            Article article = new Article()
            {
                Title = "Tiêu đề bản nháp",
                Display = false,
                Image = "no_photo.gif",
                Alias = "tieu-de-ban-nhap-" + DateTime.Now.Ticks.ToString(),
                DateCreated = DateTime.Now,
                Content = "Nội dung bản nháp",
                UserCreated = User.Identity.Name,
                Hot = false,
                ArticleCategoryId = articleCategoryId
            };
            _articleService.Add(article);
            return RedirectToAction("Edit", new { Id = article.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Article article, HttpPostedFileBase file)
        {
            article.DateCreated = DateTime.Now;
            article.UserCreated = User.Identity.Name;
            if(file != null && file.ContentLength > 0)
            {
                string extendFile = System.IO.Path.GetExtension(file.FileName).ToLower();
                if(ImageExtensions.Contains(extendFile) == false)
                {
                    ModelState.AddModelError("Image", "Đuôi mở rộng của file hình không được cho phép.");
                }
                else
                {
                    article.Image = DateTime.Now.Ticks.ToString() + extendFile;
                }
            }
            //else
            //{
            //    ModelState.AddModelError("Image", "Bạn chưa chọn hình bài viết.");
            //}
            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        file.SaveAs(Server.MapPath("~/Photos/Articles/" + article.Image));
                    }
                    _articleService.Add(article);
                    return RedirectToAction("Index", new { articleCategoryId = article.ArticleCategoryId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            ViewBag.ArticleCategoryId = new SelectList(_articleCategoryService.GetAll(), "Id", "Name", article.ArticleCategoryId);
            return View(article);
        }

        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Article article = _articleService.GetById(Id.Value);
            if (article == null)
                return Redirect("/pages/404");

            ViewBag.ArticleCategoryId = new SelectList(_articleCategoryService.GetAll(), "Id", "Name", article.ArticleCategoryId);
            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Article article, HttpPostedFileBase file)
        {
            article.UserCreated = User.Identity.Name;
            bool changeImage = false;
            if (file != null && file.ContentLength > 0)
            {
                string extendFile = System.IO.Path.GetExtension(file.FileName).ToLower();
                if (ImageExtensions.Contains(extendFile) == false)
                {
                    ModelState.AddModelError("Image", "Đuôi mở rộng của file hình không được cho phép.");
                }
                else
                {
                    article.Image = DateTime.Now.Ticks.ToString() + extendFile;
                    changeImage = true;
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _articleService.Update(article);
                    if(changeImage)
                    {
                        file.SaveAs(Server.MapPath("~/Photos/Articles/" + article.Image));
                    }
                    return RedirectToAction("Index",new { articleCategoryId = article.ArticleCategoryId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                
            }
            ViewBag.ArticleCategoryId = new SelectList(_articleCategoryService.GetAll(), "Id", "Name", article.ArticleCategoryId);
            return View(article);
        }

        [HttpPost]
        [ValidateInput(false)]
        public string UpdateContent(int Id, string Content)
        {
            Article article = _articleService.GetById(Id);
            article.Content = Content;
            if (ModelState.IsValid)
            {
                try
                {
                    _articleService.Update(article);
                    return "1";
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }

            }
            return Functions.GetAllErrorsPage(this.ModelState);
        }

        [HttpPost]
        public ActionResult Delete(int Id, int articleCategoryId)
        {
            _articleService.Delete(Id);
            return RedirectToAction("Index", new { articleCategoryId = articleCategoryId });
        }

        #endregion


    }
}