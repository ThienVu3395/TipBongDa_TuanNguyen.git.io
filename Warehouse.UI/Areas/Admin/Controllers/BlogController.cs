using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Warehouse.Entities;
using Warehouse.Models;
using Warehouse.Services.Interface;

namespace Warehouse.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        #region Get Alias 
        public ContentResult GetAlias(string Title)
        {
            return Content(Functions.UnicodeToKoDauAndGach(Title));
        }
        #endregion

        #region CRUD
        public ViewResult Index()
        {
            List<Blog> blogs = _blogService.GetAll();
            return View(blogs);
        }

        public ActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = _blogService.GetById(Id.Value);
            if (blog == null)
                return Redirect("/pages/404");

            return View(blog);
        }

        public ViewResult Create()
        {
            return View(new Blog());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Blog blog, HttpPostedFileBase file)
        {
            blog.Author = User.Identity.Name;
            blog.DateCreated = DateTime.Now;
            if(file != null && file.ContentLength > 0)
            {
                string newAvatar = blog.Alias + ".jpg";
                Functions.UpLoadImage(Server.MapPath("~/Photos/Blogs/" + newAvatar), file, this.ModelState, "Image");
                blog.Image = newAvatar;
            }
            else
            {
                ModelState.AddModelError("Image", "Bạn chưa chọn hình!");
            }
            if(_blogService.CheckUniqueTitle(blog.Title) == false)
            {
                ModelState.AddModelError("Title", "Tiêu đề bị trùng bài viết khác. Bạn hãy nhập tiêu đề khác!");
            }
            if (_blogService.CheckUniqueAlias(blog.Alias) == false)
            {
                ModelState.AddModelError("Alias", "Bí danh bị trùng bài viết khác. Bạn hãy nhập bí danh khác!");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _blogService.Add(blog);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(blog);
        }

        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = _blogService.GetById(Id.Value);
            if (blog == null)
                return Redirect("/pages/404");
            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Blog blog, string OldTitle, string OldAlias, HttpPostedFileBase file)
        {
            blog.Author = User.Identity.Name;
            blog.LastModified = DateTime.Now;
            if (file != null && file.ContentLength > 0)
            {
                string newAvatar = blog.Alias + DateTime.Now.Ticks.ToString() + ".jpg";
                Functions.UpLoadImage(Server.MapPath("~/Photos/Blogs/" + newAvatar), file, this.ModelState, "Image");
                blog.Image = newAvatar;
            }
            if (blog.Title != OldTitle && _blogService.CheckUniqueTitle(blog.Title) == false)
            {
                ModelState.AddModelError("Title", "Tiêu đề bị trùng bài viết khác. Bạn hãy nhập tiêu đề khác!");
            }
            if (blog.Alias != OldAlias &&  _blogService.CheckUniqueAlias(blog.Alias) == false)
            {
                ModelState.AddModelError("Alias", "Bí danh bị trùng bài viết khác. Bạn hãy nhập bí danh khác!");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _blogService.Update(blog);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                
            }
            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int Id)
        {
            _blogService.Delete(Id);
            return RedirectToAction("Index");
        }

        #endregion
 
        #region Count 
        [HttpPost]
        public ContentResult CountDisplay()
        {
            return Content(_blogService.CountDisplay().ToString());
        }
        [HttpPost]
        public ContentResult CountHide()
        {
            return Content(_blogService.CountHide().ToString());
        }
        #endregion
 
    }
}