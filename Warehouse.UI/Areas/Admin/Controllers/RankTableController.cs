using System.Web.Mvc;

namespace Warehouse.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RankTableController : Controller
    {
        // GET: Admin/RankTable
        public ActionResult Index()
        {
            return View();
        }
    }
}