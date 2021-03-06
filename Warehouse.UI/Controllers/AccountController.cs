﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using Warehouse.Models;
using Facebook;
using System.IO;
using System.Data.Entity.Validation;
using Warehouse.Data.Interface;
using Warehouse.Services.Interface;
using Warehouse.Services.Services;
using Warehouse.Data.Data;
using Warehouse.Entities;
using DevTrends.MvcDonutCaching;

namespace Warehouse.Controllers
{
    [Authorize]
    [RoutePrefix("tai-khoan")]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IProvinceService _provinceService;
        private IDistrictService _districtService;
        private IWardService _wardService;

        public AccountController(IProvinceService provinceService, IDistrictService districtService, IWardService wardService)
        {
            _provinceService = provinceService;
            _districtService = districtService;
            _wardService = wardService;
        }
        public AccountController(ApplicationSignInManager signInManager, ApplicationUserManager userManager)
        {
            SignInManager = signInManager;
            UserManager = userManager;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeAvatar(string userId, string base64String)
        {
            if (!string.IsNullOrEmpty(base64String))
            {
                var model = UserManager.FindById(userId);
                try
                {
                    base64String = base64String.Substring(base64String.IndexOf(',') + 1);
                    string newAvatar = DateTime.Now.Ticks.ToString() + ".jpg";
                    Functions.SaveFileFromBase64(Server.MapPath("~/Photos/ThanhVien/" + newAvatar), base64String);
                    model.Avatar = newAvatar;
                }
                catch
                {
                    return RedirectToAction("Manage");
                }
                UserManager.Update(model);
            }

            return RedirectToAction("Manage");
        }

        [ChildActionOnly]
        public PartialViewResult _InfoPartial()
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ApplicationUser user = UserManager.FindByName(User.Identity.Name);
            return PartialView(user);
        }


        public ViewResult SetPassword()
        {
            ViewBag.ActiveAccount = "active";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            ModelState state = ModelState["OldPassword"];
            if (state != null)
            {
                state.Errors.Clear();
            }

            if (ModelState.IsValid)
            {
                IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                else
                {
                    AddErrors(result);
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        [Route("dang-nhap")]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.ActiveAccount = "active";
            return View();
        }


        //
        [HttpPost]
        [AllowAnonymous]
        [Route("dang-nhap")]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, false, shouldLockout: true);

            switch (result)
            {
                case SignInStatus.Success:
                    {
                        var user = await UserManager.FindByNameAsync(model.UserName);
                        if (user != null)
                        {
                            //if (DateTime.Compare(user.LockoutEndDateUtc ?? DateTime.Today.AddDays(-1), DateTime.Today) > 0)
                            //{
                            //    return Redirect("/pages/locked");
                            //}
                            user.IP = Request.UserHostAddress;
                            await UserManager.UpdateAsync(user);
                        }
                        var cacheManager = new OutputCacheManager();
                        cacheManager.RemoveItems();
                        //return RedirectToAction("Index","Manage");
                        if(!string.IsNullOrEmpty(returnUrl))
                            return Redirect(returnUrl);
                        return RedirectToAction("Index", "Manage");
                    }
                case SignInStatus.LockedOut:
                    return Redirect("/pages/locked");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", Warehouse.Language.Account.Index.WrongAccountOrPassword);
                    return View(model);
            }
        }

        [AllowAnonymous]
        [Route("~/AdminLogin")]
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("~/AdminLogin")]
        public async Task<ActionResult> AdminLogin(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, false, shouldLockout: true);

            if (result == SignInStatus.Success)
            {
                if (UserManager.FindByName(model.UserName).Roles == null || UserManager.FindByName(model.UserName).Roles.Any(x => x.RoleId == "Admin") == false)
                {
                    result = SignInStatus.Failure;
                }
            }

            switch (result)
            {
                case SignInStatus.Success:
                    {
                        return Redirect("/AdminArea");
                    }
                case SignInStatus.LockedOut:
                    return Redirect("/pages/locked");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = Url.Action("Index", "Home", new { area = "Admin" }), RememberMe = false });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", Warehouse.Language.Account.Index.WrongAccountOrPassword);
                    return View(model);
            }
        }


        [AllowAnonymous]
        [Route("dang-ky")]
        public ActionResult Register()
        {
            ViewBag.ActiveAccount = "active";
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("dang-ky")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            string EncodedResponse = Request.Form["g-Recaptcha-Response"];
            bool IsCaptchaValid = (ReCaptchaClass.Validate(EncodedResponse) == "true" ? true : false);

            if (!IsCaptchaValid)
            {
                ModelState.AddModelError("Recaptcha", "Mã xác nhận không hợp lệ!");
            }

            if (UserManager.Users.Any(x => x.UserName == model.UserName))
            {
                ModelState.AddModelError("UserName", "Tài khoản này đã tồn tại trong hệ thống. Bạn hãy nhập tài khoản khác.");
            }

            if (UserManager.Users.Any(x => x.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email này đã tồn tại trong hệ thống. Bạn hãy nhập email khác.");
            }

            if (UserManager.Users.Any(x => x.PhoneNumber == model.Phone))
            {
                ModelState.AddModelError("Phone", "Số điện thoại này đã tồn tại trong hệ thống. Bạn hãy nhập số điện thoại khác.");
            }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName,
                    PhoneNumber = model.Phone,
                    DateRegister = DateTime.Now,
                    Avatar = "user.png",
                    Point = 1000,
                    IP = Request.UserHostAddress,
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false
                };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            IdentityResult result = await UserManager.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {
                return View("ConfirmEmail");
            }
            else
            {
                AddErrors(result);
                return View();
            }
        }

        [AllowAnonymous]
        [Route("quen-mat-khau")]
        public ActionResult ForgotPassword()
        {
            ViewBag.ActiveAccount = "active";
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("quen-mat-khau")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    return RedirectToAction("ForgotPasswordConfirmation", "Account");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Đặt lại mật khẩu", "Đặt lại mật khẩu cho website bằng cách click  <a href=\"" + callbackUrl + "\">vào đây</a>.");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            ViewBag.ActiveAccount = "active";
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            ViewBag.ActiveAccount = "active";
            if (code == null)
            {
                return View("Error");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", Warehouse.Language.Account.Index.NotExistEmail);
                    return View();
                }
                IdentityResult result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation", "Account");
                }
                else
                {
                    AddErrors(result);
                    return View();
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                await SignInAsync(user, isPersistent: false);
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.ActiveAccount = "active";
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Change password success."
                : message == ManageMessageId.SetPasswordSuccess ? "Set password success."
                : message == ManageMessageId.RemoveLoginSuccess ? "Remove login success."
                : message == ManageMessageId.Error ? "An error occurred while processing."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            ApplicationUser userCurrent = UserManager.FindByName(User.Identity.Name);
            return View(userCurrent);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                if (DateTime.Compare(user.LockoutEndDateUtc ?? DateTime.Today.AddDays(-1), DateTime.Today) > 0)
                {
                    return Redirect("/pages/locked");
                }
                await SignInAsync(user, isPersistent: false);

                user.IP = Request.UserHostAddress;
                await UserManager.UpdateAsync(user);

                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { FullName = loginInfo?.ExternalIdentity?.Name, Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    user.IP = Request.UserHostAddress;
                    await UserManager.UpdateAsync(user);
                }
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    user.IP = Request.UserHostAddress;
                    await UserManager.UpdateAsync(user);
                }
                return RedirectToAction("Index", "Manage");
            }

            if (UserManager.Users.Any(x => x.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email này đã tồn tại trong hệ thống. Bạn hãy nhập email khác.");
            }

            if (UserManager.Users.Any(x => x.PhoneNumber == model.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "Số điện thoại này đã tồn tại trong hệ thống. Bạn hãy nhập số điện thoại khác.");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                    Point = 1000,
                    DateRegister = DateTime.Now,
                    Avatar = "user.png",
                    IP = Request.UserHostAddress
                };
                IdentityResult result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);

                        return RedirectToAction("Index", "Home");
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;

            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            //return RedirectToAction("Index", "Home");
            return Redirect(Request.UrlReferrer.ToString());
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
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

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
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
            return UserManager.HasPassword(User.Identity.GetUserId());
        }

        private void SendEmail(string email, string callbackUrl, string subject, string message)
        {
            // For information on sending mail, please visit http://go.microsoft.com/fwlink/?LinkID=320771
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}