using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Warehouse.Common;
using System.Configuration;
using Warehouse.Services.Interface;
using Warehouse.Entities;

namespace Warehouse.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LeagueController : Controller
    {
        string APIKey = ConfigurationManager.AppSettings["APIKeyFootball"].ToString();
        string API_League = ConfigurationManager.AppSettings["API_League"].ToString();
        readonly List<string> ImageExtensions = ConfigurationManager.AppSettings["ImageExtensions"].ToString().Split('|').ToList();

        private ILeagueService _leagueService;

        public LeagueController(ILeagueService leagueService)
        {
            _leagueService = leagueService;
        }

        // GET: Admin/Leaguage
        public ActionResult Index(int? country_id)
        {
            ViewBag.CountryId = country_id ?? 41; // Default country is England
            return View();
        }

        public PartialViewResult GetLeagueByCountryId(int countryId)
        {
            var leagues = _leagueService.GetLeaguesByCountryID(countryId);
            ViewBag.CountryId = countryId;
            return PartialView(leagues);
        }

        public JsonResult ChangeStatus(int countryID, int league_id)
        {
            try
            {
                var league = _leagueService.GetLeagueByIdFromDatabase(league_id);
                if (league == null)
                {
                    league = _leagueService.GetLeagueByIdFromAPI(countryID, league_id);
                }
                league.status = league.status == true ? false : true;
                _leagueService.AddOrUpdate(league);
                return Json(new { status = 1, content = league.status  }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { status = 0, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Edit(int country_id, int league_id)
        {
            var league = _leagueService.GetLeague(country_id, league_id);
            if (league == null)
                return Redirect("/pages/404");
            return View(league);
        }

        [HttpPost]
        public ActionResult Edit(League league, HttpPostedFileBase file)
        {
            string extend = string.Empty;
            if (file != null && file.ContentLength > 0)
            {
                extend = System.IO.Path.GetExtension(file.FileName).ToLower();
                if (ImageExtensions.Contains(extend) == false)
                {
                    ModelState.AddModelError("Logo", "File mở rộng của logo không hợp lệ!");
                }
                else
                {
                    league.logo = DateTime.Now.Ticks.ToString() + extend;
                }
            }
            if(league.sort_order <= 0)
            {
                ModelState.AddModelError("sort_order", "Số thứ tự phải > 0.");
            }
            if(ModelState.IsValid)
            {
                if(extend != string.Empty)
                    file.SaveAs(Server.MapPath("~/Photos/Leagues/" + league.logo));
                _leagueService.AddOrUpdate(league);
                return RedirectToAction("Index",new { country_id = league.country_id });
            }
            return View(league);
        }
    }
}