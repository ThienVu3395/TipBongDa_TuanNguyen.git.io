using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Warehouse.Entities;
using Warehouse.Models;
using Warehouse.Services.Interface;

namespace Warehouse.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HandbookController : Controller
    {
        IHandbookService _handBookService;
        readonly int WidthImageBlog = int.Parse(ConfigurationManager.AppSettings["WidthImageBlog"].ToString());
        readonly int HeightImageBlog = int.Parse(ConfigurationManager.AppSettings["HeightImageBlog"].ToString());

        public HandbookController(IHandbookService handBookService)
        {
            _handBookService = handBookService;
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
            List<Handbook> handBooks = _handBookService.GetAll();
            return View(handBooks);
        }

        public ActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Handbook handbook = _handBookService.GetById(Id.Value);
            if (handbook == null)
                return Redirect("/pages/404");

            return View(handbook);
        }

        public ViewResult Create()
        {
            return View(new Handbook());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Handbook handbook, HttpPostedFileBase file)
        {
            string newAvatar = string.Empty;
            handbook.Author = User.Identity.Name;
            handbook.DateCreated = DateTime.Now;
            if (file != null && file.ContentLength > 0)
            {
                newAvatar = handbook.Alias + System.IO.Path.GetExtension(file.FileName);
                handbook.Image = newAvatar;
            }
            else
            {
                ModelState.AddModelError("Image", "Bạn chưa chọn hình!");
            }
            if (_handBookService.CheckUniqueTitle(handbook.Title) == false)
            {
                ModelState.AddModelError("Title", "Tiêu đề bị trùng bài viết khác. Bạn hãy nhập tiêu đề khác!");
            }
            if (_handBookService.CheckUniqueAlias(handbook.Alias) == false)
            {
                ModelState.AddModelError("Alias", "Bí danh bị trùng bài viết khác. Bạn hãy nhập bí danh khác!");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if(newAvatar != string.Empty)
                    {
                        Functions.UpLoadImage(Server.MapPath("~/Photos/Handbooks/" + newAvatar), file, this.ModelState, "Image");
                    }
                    _handBookService.Add(handbook);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(handbook);
        }

        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var handbook = _handBookService.GetById(Id.Value);
            if (handbook == null)
                return Redirect("/pages/404");
            return View(handbook);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Handbook handbook, string OldTitle, string OldAlias, HttpPostedFileBase file)
        {
            handbook.Author = User.Identity.Name;
            handbook.LastModified = DateTime.Now;
            if (file != null && file.ContentLength > 0)
            {
                string newAvatar = handbook.Alias + DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(file.FileName);
                handbook.Image = newAvatar;
            }
            if (handbook.Title != OldTitle && _handBookService.CheckUniqueTitle(handbook.Title) == false)
            {
                ModelState.AddModelError("Title", "Tiêu đề bị trùng bài viết khác. Bạn hãy nhập tiêu đề khác!");
            }
            if (handbook.Alias != OldAlias && _handBookService.CheckUniqueAlias(handbook.Alias) == false)
            {
                ModelState.AddModelError("Alias", "Bí danh bị trùng bài viết khác. Bạn hãy nhập bí danh khác!");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if(handbook.Image != null)
                    {
                        Functions.UpLoadImage(Server.MapPath("~/Photos/Handbooks/" + handbook.Image), file, this.ModelState, "Image");
                    }
                    _handBookService.Update(handbook);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                
            }
            return View(handbook);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int Id)
        {
            _handBookService.Delete(Id);
            return RedirectToAction("Index");
        }

        #endregion

 
        #region Count 
        [HttpPost]
        public ContentResult CountDisplay()
        {
            return Content(_handBookService.CountDisplay().ToString());
        }
        [HttpPost]
        public ContentResult CountHide()
        {
            return Content(_handBookService.CountHide().ToString());
        }
        #endregion
 
    }
}