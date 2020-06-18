using System.Web.Mvc;

namespace Warehouse.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PlayerController : Controller
    {
        // GET: Admin/Games
        public ActionResult Index()
        {
            return View();
        }
    }
}