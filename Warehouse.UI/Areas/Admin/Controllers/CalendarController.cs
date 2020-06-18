using Newtonsoft.Json;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Warehouse.Services.Interface;

namespace Warehouse.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CalendarController : Controller
    {
        readonly IGoldenGoalGameService _goldenGoalGameService;
        readonly IProphet1x2GameDetailService _prophet1x2GameDetailService;

        readonly string APIKeyFootball = ConfigurationManager.AppSettings["APIKeyFootball"].ToString();
        readonly string API_League = ConfigurationManager.AppSettings["API_League"].ToString();
        readonly string API_TeamInfo = ConfigurationManager.AppSettings["API_TeamInfo"].ToString();

        public CalendarController(IGoldenGoalGameService goldenGoalGameService, IProphet1x2GameDetailService prophet1x2GameDetailService)
        {
            _goldenGoalGameService = goldenGoalGameService;
            _prophet1x2GameDetailService = prophet1x2GameDetailService;
        }

        // GET: Admin/Calendar
        public ActionResult Index()
        {
            ViewBag.GoldenGoalGamesMatchId = JsonConvert.SerializeObject(_goldenGoalGameService.GetAll().Select(x => x.match_id));
            ViewBag.Prophet1x2GameDetailsMatchId = JsonConvert.SerializeObject(_prophet1x2GameDetailService.GetAll().Select(x => x.match_id));
            return View();
        }

       
    }
}