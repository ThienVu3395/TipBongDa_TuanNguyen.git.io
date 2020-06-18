using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Warehouse.Common;
using Warehouse.Entities;
using Warehouse.Models;
using Warehouse.Services.Interface;

namespace Warehouse.Controllers
{
    public class GameController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        readonly IGoldenGoalGameService _goldenGoalGameService;
        readonly IPlayGoldenGoalGameService _playGoldenGoalGameService;
        readonly IInfoWebService _infoWebService;
        readonly IProphet1x2GameService _prophet1x2GameService;
        readonly IProphet1x2GameDetailService _prophet1x2GameDetailService;
        readonly IPlayProphet1x2GameService _playProphet1x2GameService;

        readonly string APIKeyFootball = ConfigurationManager.AppSettings["APIKeyFootball"].ToString();
        readonly string API_TeamInfo = ConfigurationManager.AppSettings["API_TeamInfo"].ToString();

        public GameController()
        {
        }

        public GameController(ApplicationSignInManager signInManager, ApplicationUserManager userManager)
        {
            SignInManager = signInManager;
            UserManager = userManager;
        }

        public GameController(IPlayProphet1x2GameService playProphet1x2GameService, IProphet1x2GameDetailService prophet1x2GameDetailService, IProphet1x2GameService prophet1x2GameService, IGoldenGoalGameService goldenGoalGameService, IInfoWebService infoWebService, IPlayGoldenGoalGameService playGoldenGoalGameService)
        {
            _goldenGoalGameService = goldenGoalGameService;
            _infoWebService = infoWebService;
            _playGoldenGoalGameService = playGoldenGoalGameService;
            _prophet1x2GameService = prophet1x2GameService;
            _prophet1x2GameDetailService = prophet1x2GameDetailService;
            _playProphet1x2GameService = playProphet1x2GameService;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult ChangeRoundGoldedGoalGame(int Round)
        {
            var goldenGoalGame = _goldenGoalGameService.GetGoldenGoalGameByRound(Round);
            if (goldenGoalGame == null)
                return Content("<p>Không có dữ liệu.</p>");
            List<dynamic> teams = API.GetListAPI<dynamic>(API_TeamInfo + "&league_id=" + goldenGoalGame.league_id + "&APIkey=" + APIKeyFootball);
            dynamic hometeam = teams.FirstOrDefault(x => x.team_key == goldenGoalGame.match_hometeam_id);
            dynamic awayteam = teams.FirstOrDefault(x => x.team_key == goldenGoalGame.match_awayteam_id);
            ViewBag.PlayersHomeTeam = hometeam.players;
            ViewBag.PlayersAwayTeam = awayteam.players;
            ViewBag.ListRound = new SelectList(_goldenGoalGameService.GetAll().Select(x => new { Id = x.Round, Round = "Vòng " + x.Round }).OrderByDescending(x => x.Round).ToList(), "Id", "Round", goldenGoalGame.Round);
            if (User.Identity.IsAuthenticated)
            {
                var userCurrent = UserManager.FindByName(User.Identity.Name);
                ViewBag.CheckHavePlayed = _goldenGoalGameService.CheckUserHavePlayed(goldenGoalGame.Id, User.Identity.Name) || _goldenGoalGameService.CheckUserHavePlayed(goldenGoalGame.Id, userCurrent.IP);
            }
            else
            {
                ViewBag.CheckHavePlayed = false;
            }
            ViewBag.PlayersWinner = goldenGoalGame.PlayGoldenGoalGames.Where(x => x.player_id_result == goldenGoalGame.player_id_result && x.minute_goalscorer == goldenGoalGame.minute_goalscorer).ToList();
            ViewBag.InfoWeb = _infoWebService.GetFirst();
            return PartialView(goldenGoalGame);
        }

        [Route("tranh-tai-mien-phi/ban-thang-vang")]
        public ActionResult BanThangVang()
        {
            ViewBag.ActiveGame = "active";
            var goldenGoalGame = _goldenGoalGameService.GetGoldenGoalGameLastRound();
            ViewBag.InfoWeb = _infoWebService.GetFirst();
            return View(goldenGoalGame);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("tranh-tai-mien-phi/ban-thang-vang")]
        public JsonResult BanThangVang(FormCollection frm)
        {
            try
            {
                var goldenGoalGame = _goldenGoalGameService.GetGoldenGoalGameLastRound();
                if(goldenGoalGame.match_status != "" && goldenGoalGame.match_status != "Chưa đá")
                {
                    return Json(new { status = 0, message = "Bạn chỉ có thể tham gia dự đoán trận đấu chưa kết thúc!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    DateTime timeNow = DateTime.Now;
                    DateTime matchDate = DateTime.ParseExact(goldenGoalGame.match_time + " " + goldenGoalGame.match_date, "HH:mm dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None);
                    if ((matchDate - timeNow).TotalHours <= 1.0)
                    {
                        return Json(new { status = 0, message = "Không thể dự đoán bây giờ vì trận đấu sắp diễn ra 1 tiếng nữa!" }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (frm["PlayersHomeTeam"].ToString() == "0" && frm["PlayersAwayTeam"].ToString() == "0")
                {
                    return Json(new { status = 0, message = "Bạn chưa chọn cầu thủ ghi bàn đầu tiên!" }, JsonRequestBehavior.AllowGet);
                }
                long player_id_result = long.Parse(frm["PlayersHomeTeam"].ToString() == "0" ? frm["PlayersAwayTeam"].ToString() : frm["PlayersHomeTeam"].ToString());
                if (frm["minute_goalscorer"].ToString() == string.Empty)
                {
                    return Json(new { status = 0, message = "Bạn chưa nhập số phút bàn thắng đầu tiên!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int minute_goalscorer;
                    if(int.TryParse(frm["minute_goalscorer"].ToString(), out minute_goalscorer) == false)
                    {
                        return Json(new { status = 0, message = "Số phút phải là số!" }, JsonRequestBehavior.AllowGet);
                    }
                    else if(minute_goalscorer < 1 || minute_goalscorer > 120)
                    {
                        return Json(new { status = 0, message = "Số phút phải trong khoảng từ 1 đến 120!" }, JsonRequestBehavior.AllowGet);
                    }
                }
                var userCurrent = UserManager.FindByName(User.Identity.Name);
                bool checkHavePlayed = _goldenGoalGameService.CheckUserHavePlayed(goldenGoalGame.Id, User.Identity.Name) || _goldenGoalGameService.CheckIPHavePlayed(goldenGoalGame.Id, userCurrent.IP);
                if (checkHavePlayed)
                {
                    return Json(new { status = 0, message = "Bạn đã dự đoán trận này trước đó rồi!" }, JsonRequestBehavior.AllowGet);
                }
                else if(userCurrent.Point < 50)
                {
                    return Json(new { status = 0, message = "Bạn không đủ 50 điểm để tham gia dự đoán!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    List<dynamic> players = Functions.GetFullTeam(goldenGoalGame.league_id, goldenGoalGame.match_hometeam_id, goldenGoalGame.match_awayteam_id);
                    string player_name_result = players.First(x => x.player_key == player_id_result).player_name;

                    PlayGoldenGoalGame playGoldenGoalGame = new PlayGoldenGoalGame()
                    {
                        GoldenGoalGameId = goldenGoalGame.Id,
                        CreatedDate = DateTime.Now,
                        LastChanged = DateTime.Now,
                        IP = userCurrent.IP,
                        minute_goalscorer = frm["minute_goalscorer"].ToString(),
                        player_id_result = player_id_result,
                        player_name_result = player_name_result,
                        UserName = User.Identity.Name
                    };
                    _playGoldenGoalGameService.Add(playGoldenGoalGame);

                    userCurrent.Point -= 50;
                    UserManager.Update(userCurrent);

                    return Json(new { status = 1, message = "Dự đoán thành công." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [Route("tranh-tai-mien-phi/nha-tien-tri-1x2")]
        public ActionResult NhaTienTri1x2()
        {
            ViewBag.ActiveGame = "active";
            var prophet1x2Game = _prophet1x2GameService.GetProphet1x2GameActive();
            ViewBag.InfoWeb = _infoWebService.GetFirst();
            return View(prophet1x2Game);
        }

        public ActionResult DoiVongNhaTienTri(int Round)
        {
            var prophet1x2Game = _prophet1x2GameService.GetProphet1x2GameByRound(Round);
            if (prophet1x2Game == null)
                return Content("<p>Không có dữ liệu.</p>");
           
            ViewBag.Prophet1x2GameDetails = _prophet1x2GameDetailService.GetByProphet1x2GameId(prophet1x2Game.Id);
            ViewBag.Point = 0;
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.PlayProphet1x2Games = _playProphet1x2GameService.GetByUserName(User.Identity.Name).Where(x => (ViewBag.Prophet1x2GameDetails as List<Prophet1x2GameDetail>).Any(y => y.Id == x.Prophet1x2GameDetailId));
                ViewBag.Point = _playProphet1x2GameService.CountAnswerCorrect(prophet1x2Game.Id);
            }
            
            ViewBag.InfoWeb = _infoWebService.GetFirst();
            ViewBag.ListRound = new SelectList(_prophet1x2GameService.GetAll().Select(x => new { Id = x.Round, Round = "Vòng " + x.Round }).OrderByDescending(x => x.Round).ToList(), "Id", "Round", prophet1x2Game.Round);
            return PartialView(prophet1x2Game);
        }
    }
}