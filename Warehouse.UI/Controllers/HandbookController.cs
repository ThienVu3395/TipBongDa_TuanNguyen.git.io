using System.Linq;
using System.Web.Mvc;
using Warehouse.Services.Interface;
using PagedList;
using Warehouse.Entities;

namespace Warehouse.Controllers
{
    [RoutePrefix("cam-nang")]
    public class HandbookController : Controller
    {
        readonly IHandbookService _handbookService;

        public HandbookController(IHandbookService handbookService)
        {
            _handbookService = handbookService;
        }

        [Route("")]
        public ActionResult Index(int? page)
        {
            ViewBag.ActiveCamNang = "active";
            return View(_handbookService.GetListByDisplay(true).OrderByDescending(x => x.Id).ToPagedList(page ?? 1, 3));
        }

        [Route("{alias}")]
        public ActionResult Detail(string alias)
        {
            ViewBag.ActiveCamNang = "active";
            Handbook handbook = _handbookService.GetByAlias(alias, true);

            if (handbook == null)
                return Redirect("/pages/404");

            return View(handbook);
        }

        [Route("demo/{alias}")]
        public ActionResult DetailDemo(string alias)
        {
            ViewBag.ActiveCamNang = "active";
            Handbook handbook = _handbookService.GetByAlias(alias);

            if (handbook == null)
                return Redirect("/pages/404");

            return View("Detail", handbook);
        }

        [ChildActionOnly]
        public PartialViewResult _HandbookSidebarPartial()
        {
            ViewBag.ArticlesHightLight = _handbookService.GetListHighLight();
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
            ViewBag.ActiveCamNang = "active";
            return View(_handbookService.GetListByDisplay(true).Where(x => x.Title.ToLower().Contains(keyword.ToLower()) || keyword == "").ToList());
        }


    }
}