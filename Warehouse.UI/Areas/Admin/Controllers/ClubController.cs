using System.Web.Mvc;

namespace Warehouse.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClubController : Controller
    {
        // GET: Admin/Club
        public ActionResult Index()
        {
            return View();
        }
    }
}