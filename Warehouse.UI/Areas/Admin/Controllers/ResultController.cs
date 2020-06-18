using System.Web.Mvc;

namespace Warehouse.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ResultController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}