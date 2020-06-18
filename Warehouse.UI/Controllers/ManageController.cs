using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Warehouse.Entities;
using Warehouse.Models;
using Warehouse.Services.Interface;

namespace Warehouse.Controllers
{
    [Authorize]
    [RoutePrefix("quan-ly-tai-khoan")]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IProvinceService _provinceService;
        private IDistrictService _districtService;
        private IWardService _wardService;
        readonly IPlayGoldenGoalGameService _playGoldenGoalGameService;

        public ManageController()
        {
        }

        public ManageController(IPlayGoldenGoalGameService playGoldenGoalGameService, IProvinceService provinceService, IDistrictService districtService, IWardService wardService)
        {
            _provinceService = provinceService;
            _districtService = districtService;
            _wardService = wardService;
            _playGoldenGoalGameService = playGoldenGoalGameService;
        }

        public string UserId
        {
            get
            {
                return User.Identity.GetUserId();
            }
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

        [NonAction]
        bool CeckCanEditPlayGoldenGoalGame(int Id)
        {
            var playGoldenGoalGameService = _playGoldenGoalGameService.GetById(Id);
            var goldenGoalGame = playGoldenGoalGameService.GoldenGoalGame;
            bool check = true;
            if (goldenGoalGame.match_status != "" && goldenGoalGame.match_status != "Chưa đá")
            {
                check = false;
            }
            else
            {
                DateTime timeNow = DateTime.Now;
                DateTime matchDate = DateTime.ParseExact(goldenGoalGame.match_time + " " + goldenGoalGame.match_date, "HH:mm dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None);
                if ((matchDate - timeNow).TotalHours <= 1.0)
                {
                    check = false;
                }
            }
            return check;
        }

        [Route("giai-dau/ban-thang-vang")]
        public ActionResult MyGoldenGoldGame()
        {
            var data = _playGoldenGoalGameService.GetByUserName(User.Identity.Name);
            Dictionary<int, bool> listCheckCanEditPlayGoldenGoalGame = new Dictionary<int, bool>();
            foreach(var item in data)
            {
                listCheckCanEditPlayGoldenGoalGame.Add(item.Id, CeckCanEditPlayGoldenGoalGame(item.Id));
            }
            ViewBag.ListCheckCanEditPlayGoldenGoalGame = listCheckCanEditPlayGoldenGoalGame;
            return View(_playGoldenGoalGameService.GetByUserName(User.Identity.Name));
        }

        public ActionResult _EditMyGoldenGoldGameModal(int Id)
        {
            var data = _playGoldenGoalGameService.GetById(Id);
            if(data == null)
            {
                return Content("<p>Không có dữ liệu.</p>");
            }
            var goldenGoalGame = data.GoldenGoalGame;
            if (goldenGoalGame.match_status != "" && goldenGoalGame.match_status != "Chưa đá")
            {
                return Json(new { status = 0, message = "Bạn chỉ có thể tham gia dự đoán trận đấu chưa kết thúc!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                DateTime timeNow = DateTime.Now;
                DateTime matchDate = DateTime.ParseExact(goldenGoalGame.match_time + " " + goldenGoalGame.match_date, "HH:mm dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None);
                if ((matchDate - timeNow).TotalHours <= 1.0)
                {
                    return Json(new { status = 0, message = "Không thể chỉnh sửa dự đoán bây giờ vì trận đấu sắp diễn ra!" }, JsonRequestBehavior.AllowGet);
                }
            }
            List<dynamic> players = Functions.GetFullTeam(goldenGoalGame.league_id, goldenGoalGame.match_hometeam_id, goldenGoalGame.match_awayteam_id);
            ViewBag.player_id_result = new SelectList(players, "player_key", "player_name", data.player_id_result);
            return PartialView(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _EditMyGoldenGoldGameModal(int Id, FormCollection frm)
        {
            if(frm["player_id_result"].ToString() == string.Empty)
            {
                return Json(new { status = 0, message = "Bạn chưa chọn cầu thủ ghi bàn đầu tiên" }, JsonRequestBehavior.AllowGet);
            }
            if (frm["minute_goalscorer"].ToString() == string.Empty)
            {
                return Json(new { status = 0, message = "Bạn chưa nhập số phút bàn thắng đầu tiên!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int minute_goalscorer;
                if (int.TryParse(frm["minute_goalscorer"].ToString(), out minute_goalscorer) == false)
                {
                    return Json(new { status = 0, message = "Số phút phải là số!" }, JsonRequestBehavior.AllowGet);
                }
                else if (minute_goalscorer < 1 || minute_goalscorer > 120)
                {
                    return Json(new { status = 0, message = "Số phút phải trong khoảng từ 1 đến 120!" }, JsonRequestBehavior.AllowGet);
                }
            }
            var data = _playGoldenGoalGameService.GetById(Id);
            if (data == null)
            {
                return Json(new { status = 0, message = "Không có dữ liệu" }, JsonRequestBehavior.AllowGet);
            }
            var goldenGoalGame = data.GoldenGoalGame;
            if (goldenGoalGame.match_status != "" && goldenGoalGame.match_status != "Chưa đá")
            {
                return Json(new { status = 0, message = "Bạn chỉ có thể tham gia dự đoán trận đấu chưa kết thúc!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                DateTime timeNow = DateTime.Now;
                DateTime matchDate = DateTime.ParseExact(goldenGoalGame.match_time + " " + goldenGoalGame.match_date, "HH:mm dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None);
                if ((matchDate - timeNow).TotalHours <= 1.0)
                {
                    return Json(new { status = 0, message = "Không thể chỉnh sửa dự đoán bây giờ vì trận đấu sắp diễn ra!" }, JsonRequestBehavior.AllowGet);
                }
            }
            try
            {
                data.player_id_result = long.Parse(frm["player_id_result"]);
                data.minute_goalscorer = frm["minute_goalscorer"];
                data.LastChanged = DateTime.Now;
                _playGoldenGoalGameService.Update(data);
                return Json(new { status = 1, message = "Sửa thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [Route("cap-nhat-thong-tin")]
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            switch (message)
            {
                case ManageMessageId.UpdateInfoSuccess:
                    {
                        ViewBag.SuccessMessage = "Cập nhật thông tin thành công";
                        break;
                    }
                case ManageMessageId.ChangePasswordSuccess:
                    {
                        ViewBag.SuccessMessage = "Đổi mật khẩu thành công";
                        break;
                    }
                case ManageMessageId.SendLinkEmailSuccess:
                    {
                        ViewBag.SuccessMessage = "Kiểm tra email của bạn để tiếp tục thực hiện việc xác thực.";
                        break;
                    }
                default: break;
            }
            ViewBag.CountUser = UserManager.Users.Count();
            ApplicationUser applicationUser = await UserManager.FindByNameAsync(User.Identity.Name);
            return View(applicationUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("cap-nhat-thong-tin")]

        public async Task<ActionResult> Index(UpdateInfoViewModel updateInfoViewModel)
        {
            ApplicationUser applicationUser = await UserManager.FindByNameAsync(User.Identity.Name);
            try
            {
                if (ModelState.IsValid)
                {
                    applicationUser.FullName = updateInfoViewModel.FullName;
                    applicationUser.Address = updateInfoViewModel.Address;
                    if (!string.IsNullOrEmpty(updateInfoViewModel.sBirthDate))
                        applicationUser.BirthDate = DateTime.ParseExact(updateInfoViewModel.sBirthDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None);
                    await UserManager.UpdateAsync(applicationUser);
                    return RedirectToAction("Index", new { message = ManageMessageId.UpdateInfoSuccess });
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            ViewBag.CountUser = UserManager.Users.Count();
            return View("Index", applicationUser);
        }

        public PartialViewResult _TopInfoUserPartial()
        {
            ViewBag.User = UserManager.FindByName(User.Identity.Name);
            return PartialView();
        }

        public PartialViewResult _LeftSidebarInfoUserPartial()
        {
            ViewBag.User = UserManager.FindByName(User.Identity.Name);
            return PartialView();
        }

        [Route("doi-mat-khau")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        public async Task<ActionResult> ConfirmEmail()
        {
            string code = await UserManager.GenerateEmailConfirmationTokenAsync(User.Identity.GetUserId());
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = User.Identity.GetUserId(), code = code }, protocol: Request.Url.Scheme);
            await UserManager.SendEmailAsync(User.Identity.GetUserId(), "Xác thực email của bạn", "Vui lòng click vào <a href=\"" + callbackUrl + "\">đây</a> để thực hiện việc xác thực.");
            return RedirectToAction("Index", new
            {
                message = ManageMessageId.SendLinkEmailSuccess
            });
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("doi-mat-khau")]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (!ModelState.IsValid)
            {
                ViewBag.CountUser = UserManager.Users.Count();
                return View("Index", user);
            }
            var result = await UserManager.ChangePasswordAsync(UserId, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            ViewBag.CountUser = UserManager.Users.Count();
            return View("Index", user);
        }

        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }



        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }


        [Route("dat-mat-khau")]
        public ActionResult SetPassword()
        {
            return View();
        }

        [Route("dat-mat-khau")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangeAvatar(string base64String)
        {
            ApplicationUser applicationUser = UserManager.FindById(User.Identity.GetUserId());
            if (!string.IsNullOrEmpty(base64String))
            {
                var model = UserManager.FindById(User.Identity.GetUserId());
                try
                {
                    base64String = base64String.Substring(base64String.IndexOf(',') + 1);
                    string newAvatar = model.UserName + DateTime.Now.Ticks.ToString() + ".jpg";
                    Functions.SaveFileFromBase64(Server.MapPath("~/Photos/Users/" + newAvatar), base64String);
                    model.Avatar = newAvatar;
                    UserManager.Update(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Avatar", ex.Message);
                }
            }

            ViewBag.CountUser = UserManager.Users.Count();
            return View("Index", applicationUser);
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            UpdateInfoSuccess,
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            SendLinkEmailSuccess,
            Error
        }

        #endregion
    }
}