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

namespace Warehouse.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GameController : Controller
    {
        readonly IGoldenGoalGameService _goldenGoalGameService;
        readonly IProphet1x2GameService _prophet1x2GameService;
        readonly IProphet1x2GameDetailService _prophet1x2GameDetailService;
        readonly IPlayGoldenGoalGameService _playGoldenGoalGameService;
        readonly IPlayProphet1x2GameService _playProphet1x2GameService;

        readonly string APIKeyFootball = ConfigurationManager.AppSettings["APIKeyFootball"].ToString();
        readonly string API_League = ConfigurationManager.AppSettings["API_League"].ToString();
        readonly string API_TeamInfo = ConfigurationManager.AppSettings["API_TeamInfo"].ToString();

        public GameController(IPlayProphet1x2GameService playProphet1x2GameService, IProphet1x2GameDetailService prophet1x2GameDetailService, IProphet1x2GameService prophet1x2GameService, IGoldenGoalGameService goldenGoalGameService, IPlayGoldenGoalGameService playGoldenGoalGameService)
        {
            _playProphet1x2GameService = playProphet1x2GameService;
            _prophet1x2GameDetailService = prophet1x2GameDetailService;
            _prophet1x2GameService = prophet1x2GameService;
            _goldenGoalGameService = goldenGoalGameService;
            _playGoldenGoalGameService = playGoldenGoalGameService;
        }

        public ActionResult GoldenGoalGame()
        {
            var data = _goldenGoalGameService.GetAll().OrderByDescending(x => x.Round).ToList();
            return View(data);
        }

        [HttpPost]
        public JsonResult AddForGoldenGoalGame(FormCollection frm)
        {
            try
            {
                var golenGoalGameLast = _goldenGoalGameService.GetGoldenGoalGameLastRound();
                if(golenGoalGameLast != null && (golenGoalGameLast.match_status == "Chưa đá" || golenGoalGameLast.match_status == ""))
                {
                    return Json(new { status = 0, message = "Không thể cài đặt trận tiếp theo khi trận hiện tại ở giải bàn thắng vàng chưa kết thúc" });
                }
                var goldenGoal = new GoldenGoalGame()
                {
                    match_id = int.Parse(frm["match_id"]),
                    league_id = int.Parse(frm["league_id"]),
                    league_name = frm["league_name"].ToString(),
                    match_date = DateTime.ParseExact(frm["match_date"].ToString(),"yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None).ToString("dd/MM/yyyy"),
                    match_status = frm["match_status"].ToString(),
                    match_time = frm["match_time"].ToString(),
                    match_hometeam_name = frm["match_hometeam_name"].ToString(),
                    match_awayteam_name = frm["match_awayteam_name"].ToString(),
                    match_hometeam_id = long.Parse(frm["match_hometeam_id"]),
                    match_awayteam_id = long.Parse(frm["match_awayteam_id"]),
                    match_hometeam_score = frm["match_hometeam_score"],
                    match_awayteam_score = frm["match_awayteam_score"],
                    CreatedDate = DateTime.Now,
                    Round = _goldenGoalGameService.GetNewRound(),
                };
                List<dynamic> teams = API.GetListAPI<dynamic>(API_TeamInfo + "&league_id=" + goldenGoal.league_id + "&APIkey=" + APIKeyFootball);
                dynamic hometeam = teams.FirstOrDefault(x => x.team_key == goldenGoal.match_hometeam_id);
                dynamic awayteam = teams.FirstOrDefault(x => x.team_key == goldenGoal.match_awayteam_id);
                goldenGoal.match_hometeam_logo = hometeam.team_badge;
                goldenGoal.match_awayteam_logo = awayteam.team_badge;
                _goldenGoalGameService.Add(goldenGoal);
                return Json(new { status = 1, message = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, message = ex.Message });
            }
        }

        public ActionResult _EditGoldenGoalGameModal(int Id)
        {
            var goldenGoal = _goldenGoalGameService.GetGoldenGoalGameById(Id);
            if (goldenGoal == null)
            {
                return Content("<p>Dữ liệu không tồn tại.</p>");
            }
            List<dynamic> players = Functions.GetFullTeam(goldenGoal.league_id, goldenGoal.match_hometeam_id, goldenGoal.match_awayteam_id);
            ViewBag.player_id_result = new SelectList(players, "player_key", "player_name", goldenGoal.player_id_result);
            return PartialView(goldenGoal);
        }

        [HttpPost]
        public ActionResult _EditGoldenGoalGameModal(int Id, FormCollection frm)
        {
            var goldenGoal = _goldenGoalGameService.GetGoldenGoalGameById(Id);
            goldenGoal.match_status = frm["match_status"].ToString();
            if (frm["player_id_result"] != null)
            {
                goldenGoal.player_id_result = long.Parse(frm["player_id_result"].ToString());
                List<dynamic> players = Functions.GetFullTeam(goldenGoal.league_id, goldenGoal.match_hometeam_id, goldenGoal.match_awayteam_id);
                goldenGoal.player_name_result = players.First(x => x.player_key == goldenGoal.player_id_result).player_name;
            }
            goldenGoal.match_hometeam_score = frm["match_hometeam_score"].ToString();
            goldenGoal.match_awayteam_score = frm["match_awayteam_score"].ToString();
            goldenGoal.minute_goalscorer = frm["minute_goalscorer"].ToString();
            _goldenGoalGameService.Update(goldenGoal);
            return RedirectToAction("GoldenGoalGame");
        }

        public ActionResult ViewListPlayerGoldenGoalGame(int Id)
        {
            var goldenGoalGame = _goldenGoalGameService.GetGoldenGoalGameById(Id);
            if (goldenGoalGame == null)
                return Redirect("/pages/404");
            var playGoldenGoalGames = _playGoldenGoalGameService.GetByGoldenGoalGameId(Id);
            ViewBag.PlayersWinner = new List<PlayGoldenGoalGame>();
            ViewBag.PlayersWinner = playGoldenGoalGames.Where(x => x.player_id_result == goldenGoalGame.player_id_result && x.minute_goalscorer == goldenGoalGame.minute_goalscorer).ToList();
            ViewBag.GoldenGoalGame = goldenGoalGame;
            return View(playGoldenGoalGames);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AwardedGoldenGoalGame(int Id)
        {
            _goldenGoalGameService.Awarded(Id);
            return RedirectToAction("ViewListPlayer", new { Id = Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePlayGoldenGoalGame(int GoldenGoalGameID, int[] check)
        {
            foreach(int Id in check)
            {
                var playGoldenGoalGame = _playGoldenGoalGameService.GetById(Id);
                if(playGoldenGoalGame != null)
                {
                    _playGoldenGoalGameService.Delete(playGoldenGoalGame);
                }
            }
            return RedirectToAction("ViewListPlayer", new { Id = GoldenGoalGameID });
        }

        [HttpPost]
        public JsonResult AddForProphet1x2Game(FormCollection frm)
        {
            try
            {
                var prophet1x2GameActive = _prophet1x2GameService.GetProphet1x2GameActive();
                if (prophet1x2GameActive == null)
                {
                    return Json(new { status = 0, message = "Chưa có vòng đấu nào được kích hoạt!" });
                }
                if(frm["match_status"] != string.Empty)
                {
                    return Json(new { status = 0, message = "Trận này đã kết thúc. Không thể chọn trận này!" });
                }
                if (prophet1x2GameActive.Prophet1x2GameDetails != null && prophet1x2GameActive.Prophet1x2GameDetails.Count == 0)
                {
                    return Json(new { status = 0, message = "Vòng này đã đủ 10 trận. Bạn không thể chọn thêm." });
                }
                var prophet1x2GameDetail = new Prophet1x2GameDetail()
                {
                    match_id = int.Parse(frm["match_id"]),
                    league_id = int.Parse(frm["league_id"]),
                    league_name = frm["league_name"].ToString(),
                    match_date = DateTime.ParseExact(frm["match_date"].ToString(), "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None).ToString("dd/MM/yyyy"),
                    match_time = frm["match_time"].ToString(),
                    match_hometeam_name = frm["match_hometeam_name"].ToString(),
                    match_awayteam_name = frm["match_awayteam_name"].ToString(),
                    match_hometeam_id = long.Parse(frm["match_hometeam_id"]),
                    match_awayteam_id = long.Parse(frm["match_awayteam_id"]),
                    CreatedDate = DateTime.Now,
                    CreatedUser = User.Identity.Name,
                    SortOrder = prophet1x2GameActive.Prophet1x2GameDetails == null || prophet1x2GameActive.Prophet1x2GameDetails.Count == 0 ? 1 : prophet1x2GameActive.Prophet1x2GameDetails.Max(x => (x.SortOrder ?? 0)) + 1,
                    Prophet1x2GameId = prophet1x2GameActive.Id,
                };
                List<dynamic> teams = API.GetListAPI<dynamic>(API_TeamInfo + "&league_id=" + prophet1x2GameDetail.league_id + "&APIkey=" + APIKeyFootball);
                dynamic hometeam = teams.FirstOrDefault(x => x.team_key == prophet1x2GameDetail.match_hometeam_id);
                dynamic awayteam = teams.FirstOrDefault(x => x.team_key == prophet1x2GameDetail.match_awayteam_id);
                prophet1x2GameDetail.match_hometeam_logo = hometeam.team_badge;
                prophet1x2GameDetail.match_awayteam_logo = awayteam.team_badge;
                if (_prophet1x2GameDetailService.GetByProphet1x2GameId(prophet1x2GameActive.Id).Any(x => x.match_id == prophet1x2GameDetail.match_id) == false)
                {
                    _prophet1x2GameDetailService.Add(prophet1x2GameDetail);
                }
                return Json(new { status = 1, message = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, message = ex.Message });
            }
        }

        public ActionResult _EditProphet1x2GameDetailModal(int Id)
        {
            var prophet1x2GameDetail = _prophet1x2GameDetailService.GetById(Id);
            if(prophet1x2GameDetail == null)
            {
                return Content("<p>Dữ liệu không tồn tại.</p>");
            }
            return PartialView(prophet1x2GameDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _EditProphet1x2GameDetailModal(int Id, int? match_hometeam_score, int? match_awayteam_score)
        {
            try
            {
                var prophet1x2GameDetail = _prophet1x2GameDetailService.GetById(Id);
                prophet1x2GameDetail.match_hometeam_score = match_hometeam_score;
                prophet1x2GameDetail.match_awayteam_score = match_awayteam_score;
                if(prophet1x2GameDetail.match_hometeam_score == prophet1x2GameDetail.match_awayteam_score)
                {
                    prophet1x2GameDetail.match_result = 0;
                }
                else if(prophet1x2GameDetail.match_hometeam_score > prophet1x2GameDetail.match_awayteam_score)
                {
                    prophet1x2GameDetail.match_result = 1;
                }
                else
                {
                    prophet1x2GameDetail.match_result = 2;
                }
                _prophet1x2GameDetailService.Update(prophet1x2GameDetail);
                return Json(new { status = 1 });
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, message = ex.Message });
            }
        }

        public ActionResult Prophet1x2Game()
        {
            var data = _prophet1x2GameService.GetAll().OrderByDescending(x => x.Round).ToList();
            return View(data);
        }

        public PartialViewResult _CreateProphet1x2GameModal()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _CreateProphet1x2GameModal(Prophet1x2Game prophet1x2Game)
        {
            prophet1x2Game.CreatedDated = DateTime.Now;
            prophet1x2Game.CreatedUser = User.Identity.Name;
            prophet1x2Game.Awarded = false;
            if(_prophet1x2GameService.GetProphet1x2GameByRound(prophet1x2Game.Round) != null)
            {
                ModelState.AddModelError("Round", "Vòng đấu này đã tồn tại. Bạn hãy kiểm tra lại!");
            }
            if(ModelState.IsValid)
            {
                _prophet1x2GameService.Add(prophet1x2Game);
                if(prophet1x2Game.Active)
                {
                    _prophet1x2GameService.Active(prophet1x2Game);
                }
                return Json(new { status = 1, message = "Thêm thành công" });
            }
            return Json(new { status = 0, message = Functions.GetAllErrorsPage(ModelState) });
        }


        public ActionResult ViewProphet1x2GameDetail(int Id)
        {
            var prophet1x2Game = _prophet1x2GameService.GetById(Id);
            if (prophet1x2Game == null)
            {
                return Redirect("/pages/404");
            }
            ViewBag.Round = prophet1x2Game.Round;
            var prophet1x2sGameDetail = _prophet1x2GameDetailService.GetByProphet1x2GameId(Id);      
            return View(prophet1x2sGameDetail);
        }

        public ActionResult _EditProphet1x2GameModal(int Id)
        {
            var prophet1x2Game = _prophet1x2GameService.GetById(Id);
            if (prophet1x2Game == null)
            {
                return Content("<p>Dữ liệu không tồn tại.</p>");
            }
            return PartialView(prophet1x2Game);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _EditProphet1x2GameModal(int Id, bool Active, bool Awarded)
        {
            try
            {
                var prophet1x2Game = _prophet1x2GameService.GetById(Id);
                if (Active == true)
                {
                    _prophet1x2GameService.Active(prophet1x2Game);
                }
                else
                {
                    prophet1x2Game.Active = Active;
                }
                prophet1x2Game.Awarded = Awarded;
                _prophet1x2GameService.Update(prophet1x2Game);
                return Json(new { status = 1 });
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProphet1x2GameDetailModal(int Id)
        {
            var Prophet1x2GameDetail = _prophet1x2GameDetailService.GetById(Id);
            int Prophet1x2GameId = Prophet1x2GameDetail.Prophet1x2GameId;
            _prophet1x2GameDetailService.Delete(Prophet1x2GameDetail);
            return RedirectToAction("ViewProphet1x2GameDetail", new { Id = Prophet1x2GameId });
        }

        public ActionResult ViewListPlayerProphet1x2Game(int prophet1x2GameId)
        {
            var prophet1x2Game = _prophet1x2GameService.GetById(prophet1x2GameId);
            if (prophet1x2Game == null)
            {
                return Redirect("/pages/404");
            }
            ViewBag.Round = prophet1x2Game.Round;
            ViewBag.Prophet1x2GameId = prophet1x2GameId;
            var listUserPlayprophet1x2GameDetail = _playProphet1x2GameService.GetByProphet1x2GameId(prophet1x2Game.Id);
            Dictionary<string, List<PlayProphet1x2Game>> usersPlayProphet1x2Game = listUserPlayprophet1x2GameDetail.GroupBy(x => x.UserName).Select(x => new { UserName = x.Key, ListPlayProphet1x2Game = x.ToList() }).ToDictionary(x => x.UserName, x=> x.ListPlayProphet1x2Game);
            ViewBag.Prophet1x2GameDetails = _prophet1x2GameDetailService.GetByProphet1x2GameId(prophet1x2GameId).ToDictionary(x => x.Id, x => x);
            return View(usersPlayProphet1x2Game);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProphet1x2Game(int Id)
        {
            var prophet1x2Game = _prophet1x2GameService.GetById(Id);
            if (prophet1x2Game == null)
            {
                return Redirect("/pages/404");
            }
            _prophet1x2GameService.Delete(prophet1x2Game);
            return RedirectToAction("Prophet1x2Game");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePlayerProphet1x2GameByUserName(int prophet1x2GameId, string userName)
        {
            _playProphet1x2GameService.DeleteRangeByUserName(userName);
            return RedirectToAction("ViewListPlayerProphet1x2Game", new { prophet1x2GameId = prophet1x2GameId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeletePlayProphet1x2GameDetailById(int Id)
        {
            try
            {
                var playProphet1x2Game = _playProphet1x2GameService.GetById(Id);
                _playProphet1x2GameService.Delete(playProphet1x2Game);
                return Json(new { status = 1, id = Id });
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, message = ex.Message });
            }
        }

    }
}