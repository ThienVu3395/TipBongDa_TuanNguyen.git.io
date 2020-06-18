using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Warehouse.Data.Data;
using Warehouse.Entities;
using Warehouse.Services.Interface;
using Warehouse.Models;
using System.Data.SqlClient;
using Warehouse.Areas.Admin.Models;

namespace Warehouse.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ForumCategoryController : Controller
    {
        IForumCategoryService _forumCategoryService;

        public ForumCategoryController(IForumCategoryService forumCategoryService)
        {
            _forumCategoryService = forumCategoryService;
        }

        #region Get Alias 

        [HttpPost]
        public ContentResult GetAlias(string Name)
        {
            return Content(Functions.UnicodeToKoDauAndGach(Name));
        }
        #endregion

        public ActionResult Index()
        {
            return View(_forumCategoryService.GetAll());
        }

        public ActionResult _DetailModal(int id)
        {
            ForumCategory forumCategory = _forumCategoryService.GetById(id, null);
            if (forumCategory == null)
            {
                return Content("<p>Dữ liệu không tồn tại trong hệ thống!</p>");
            }
            return PartialView(forumCategory);
        }

        public ActionResult _CreateModal()
        {
            return PartialView(new CreateForumCategoryViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(CreateForumCategoryViewModel createForumCategoryViewModel)
        {
            if(_forumCategoryService.CheckUniqueName(createForumCategoryViewModel.Name) == false)
            {
                ModelState.AddModelError("Name", "Tên forum này đã bị trùng. Bạn hãy nhập tên khác.");
            }
            if (_forumCategoryService.CheckUniqueAlias(createForumCategoryViewModel.Alias) == false)
            {
                ModelState.AddModelError("Alias", "Bí danh forum này đã bị trùng. Bạn hãy nhập bí danh khác.");
            }
            ForumCategory forumCategory = new ForumCategory();
            Random random = new Random();
            int idColor = random.Next(1, 21);
            forumCategory.Name = createForumCategoryViewModel.Name;
            forumCategory.Alias = createForumCategoryViewModel.Alias;
            forumCategory.Display = createForumCategoryViewModel.Display;
            forumCategory.SortOrder = createForumCategoryViewModel.SortOrder;
            forumCategory.DateCreated = DateTime.Now;
            forumCategory.UserCreated = User.Identity.Name;
            forumCategory.ColorCode = "tt-color" + (idColor < 9 ? "0" + idColor.ToString() : idColor.ToString());
            if (ModelState.IsValid)
            {
                _forumCategoryService.Add(forumCategory);
                return Json(new { status = 1  });
            }
            return Json(new { status = 0, message = Functions.GetAllErrorsPage(ModelState) });
        }

        public ActionResult _EditModal(int id)
        {
            ForumCategory forumCategory = _forumCategoryService.GetById(id, null);
            if (forumCategory == null)
            {
                return Content("<p>Dữ liệu không tồn tại trong hệ thống!</p>");
            }
            EditForumCategoryViewModel editForumCategoryViewModel = new EditForumCategoryViewModel()
            {
                Id = forumCategory.Id,
                Name = forumCategory.Name,
                Alias = forumCategory.Alias,
                Display = forumCategory.Display,
                SortOrder = forumCategory.SortOrder
            };
            return PartialView(editForumCategoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(EditForumCategoryViewModel editForumCategoryViewModel,string oldName, string oldAlias)
        {
            ForumCategory forumCategory = _forumCategoryService.GetById(editForumCategoryViewModel.Id, null);
            forumCategory.Name = editForumCategoryViewModel.Name;
            forumCategory.Alias = editForumCategoryViewModel.Alias;
            forumCategory.SortOrder = editForumCategoryViewModel.SortOrder;
            forumCategory.Display = editForumCategoryViewModel.Display;
            if(editForumCategoryViewModel.Name != oldName)
            {
                if(_forumCategoryService.CheckUniqueName(editForumCategoryViewModel.Name) == false)
                {
                    ModelState.AddModelError("Name", "Tên forum này đã bị trùng. Bạn hãy nhập tên khác.");
                }
            }
            if (editForumCategoryViewModel.Alias != oldAlias)
            {
                if (_forumCategoryService.CheckUniqueAlias(editForumCategoryViewModel.Alias) == false)
                {
                    ModelState.AddModelError("Alias", "Bí danh forum này đã bị trùng. Bạn hãy nhập bí danh khác.");
                }
            }
            if (ModelState.IsValid)
            {
                _forumCategoryService.Update(forumCategory);
                return Json(new { status = 1 });
            }
            return Json(new { status = 0, message = Functions.GetAllErrorsPage(ModelState) });
        }

        public ActionResult _DeleteModal(int id)
        {
            ForumCategory forumCategory = _forumCategoryService.GetById(id, null);
            if (forumCategory == null)
            {
                return Content("<p>Dữ liệu không tồn tại trong hệ thống!</p>");
            }
            return PartialView(forumCategory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ForumCategory forumCategory = _forumCategoryService.GetById(id, null);
            if(forumCategory.ForumArticles != null && forumCategory.ForumArticles.Count > 0)
            {
                ModelState.AddModelError("", "Chủ để này đang có bài viết. Không thể xóa.");
            }
            if (ModelState.IsValid)
            {
                _forumCategoryService.Delete(id);
                return Json(new { status = 1 });
            }
            return Json(new { status = 0, message = Functions.GetAllErrorsPage(ModelState) });
        }

    }
}
