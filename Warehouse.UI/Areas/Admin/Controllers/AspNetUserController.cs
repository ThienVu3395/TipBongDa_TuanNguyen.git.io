using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Warehouse.Models;
using PagedList.Mvc;
using PagedList;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet;
using System.IO;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Warehouse.Data.Interface;
using Warehouse.Services.Interface;

namespace Warehouse.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AspNetUserController : Controller
    {
        readonly List<string> ImageExtensions = ConfigurationManager.AppSettings["ImageExtensions"].ToString().Split('|').ToList();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IProvinceService _provinceService;
        private IDistrictService _districtService;
        private IWardService _wardService;

        public AspNetUserController()
        {
           
        }

        public AspNetUserController(ApplicationSignInManager signInManager, ApplicationUserManager userManager)
        {
            SignInManager = signInManager;
            UserManager = userManager;
        }

        public AspNetUserController(IProvinceService provinceService, IDistrictService districtService, IWardService wardService)
        {
            _provinceService = provinceService;
            _districtService = districtService;
            _wardService = wardService;
        }

        #region Public Property 
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

        #endregion

        public PartialViewResult _UserLoggedPartial()
        {
            ApplicationUser applicationUser = UserManager.FindById(UserId);
            IList<string> userRoles = applicationUser.Roles.Select(r => r.RoleId).ToList();
            ViewBag.UserRole = userRoles.First();
            return PartialView(applicationUser);
        }

        public PartialViewResult _UserPanelPartial()
        {
            ApplicationUser applicationUser = UserManager.FindById(UserId);
            return PartialView(applicationUser);
        }

        public async Task<ViewResult> ProfileUser()
        {
            ApplicationUser model = await UserManager.FindByIdAsync(UserId);
            ViewBag.RoleId = model.Roles.FirstOrDefault()?.RoleId;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ProfileUser(ApplicationUser updateInfoViewModel, string sBirthDate)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if(string.IsNullOrEmpty(updateInfoViewModel.Email))
            {
                ModelState.AddModelError("Email", "Bạn chưa nhập Email.");
            }
            else if (Functions.IsValidEmail(updateInfoViewModel.Email) == false)
            {
                ModelState.AddModelError("Email", "Email không hợp lệ.");
            }
            else if (user.Email != updateInfoViewModel.Email)
            {
                if (UserManager.Users.Any(x => x.Email == updateInfoViewModel.Email))
                {
                    ModelState.AddModelError("Email", "Email này đã tồn tại trong hệ thống. Bạn hãy nhập email khác.");
                }
            }
            if (string.IsNullOrEmpty(updateInfoViewModel.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "Bạn chưa nhập Số điện thoại.");
            }
            else if (user.PhoneNumber != updateInfoViewModel.PhoneNumber)
            {
                if (UserManager.Users.Any(x => x.PhoneNumber == updateInfoViewModel.PhoneNumber))
                {
                    ModelState.AddModelError("PhoneNumber", "Số điện thoại này đã tồn tại trong hệ thống. Bạn hãy nhập số điện thoại khác.");
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    user.Email = updateInfoViewModel.Email;
                    user.FullName = updateInfoViewModel.FullName;
                    user.Address = updateInfoViewModel.Address;
                    user.PhoneNumber = updateInfoViewModel.PhoneNumber;
                    if(!string.IsNullOrEmpty(sBirthDate))
                    {
                        user.BirthDate = DateTime.ParseExact(sBirthDate, "dd/MM/yyyy", null);
                    }
                    UserManager.Update(user);
                    return Json(new { status = 1, message = "Cập nhật thông tin thành công." });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return Json(new { status = 0, message = Functions.GetAllErrorsPage(this.ModelState) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeAvatar(string base64String)
        {
            if (!string.IsNullOrEmpty(base64String))
            {
                var model = UserManager.FindById(User.Identity.GetUserId());
                try
                {
                    base64String = base64String.Substring(base64String.IndexOf(',') + 1);
                    string newAvatar = model.UserName + DateTime.Now.Ticks.ToString() + ".jpg";
                    Functions.SaveFileFromBase64(Server.MapPath("~/Photos/Users/" + newAvatar), base64String);
                    model.Avatar = newAvatar;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    ViewBag.RoleId = model.Roles.FirstOrDefault()?.RoleId;
                    ViewBag.Title = "Hồ sơ cá nhân";
                    return View("ProfileUser", model);
                }
                UserManager.Update(model);
            }

            return RedirectToAction("ProfileUser");
        }

        /// <summary>
        /// Danh sách Admin
        /// </summary>
        /// <param name="afterInsert">Vừa hoàn thành thao tác thêm?</param>
        /// <returns></returns>
        public async Task<ActionResult> ListAdmin(int? afterInsert)
        {
            List<ApplicationUser> applicationUsers = await UserManager.Users.ToListAsync();
            applicationUsers = applicationUsers.Where(u => u.Roles != null && u.Roles.FirstOrDefault(r=>r.RoleId == "Admin") != null ).ToList();
            return View(applicationUsers);
        }

        /// <summary>
        /// Danh sách khách hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Members()
        {
            List<ApplicationUser> applicationUsers = UserManager.Users.ToList().Where(x => x.Roles == null || x.Roles.Count == 0).ToList();
            return View(applicationUsers);
        }

        /// <summary>
        /// Thêm Member
        /// </summary>
        /// <returns></returns>
        public ActionResult _CreateModal()
        {
            return PartialView(new CreateUserViewModel());
        }

        /// <summary>
        /// Thêm Quản trị viên
        /// </summary>
        /// <param name="createUserViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create(CreateUserViewModel createUserViewModel)
        {
            if(createUserViewModel.Point < 0)
            {
                ModelState.AddModelError("Point", "Điểm số phải >= 0.");
            }
            if (UserManager.Users.Any(x => x.UserName == createUserViewModel.UserName))
            {
                ModelState.AddModelError("UserName", "Tài khoản này đã tồn tại trong hệ thống. Bạn hãy nhập tài khoản khác.");
            }

            if (UserManager.Users.Any(x => x.Email == createUserViewModel.Email))
            {
                ModelState.AddModelError("Email", "Email này đã tồn tại trong hệ thống. Bạn hãy nhập email khác.");
            }

            if (UserManager.Users.Any(x => x.PhoneNumber == createUserViewModel.PhoneNumber))
            {
                ModelState.AddModelError("Phone", "Số điện thoại này đã tồn tại trong hệ thống. Bạn hãy nhập số điện thoại khác.");
            }
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = createUserViewModel.UserName,
                    Email = createUserViewModel.Email,
                    FullName = createUserViewModel.FullName,
                    Avatar = "user.png",
                    DateRegister = DateTime.Now,
                    EmailConfirmed = true,
                    PhoneNumber = createUserViewModel.PhoneNumber,
                    PhoneNumberConfirmed = true,
                    Point = createUserViewModel.Point,
                    IP = Request.UserHostAddress
                };
                try
                {
                    IdentityResult result = await UserManager.CreateAsync(user, createUserViewModel.Password);
                    if (createUserViewModel.RoleId == "Admin")
                    {
                        user.Roles.Add(new IdentityUserRole() { RoleId = "Admin", UserId = user.Id });
                        result = UserManager.Update(user);
                    }
                    if (result.Succeeded)
                    {
                        return Json(new { status = 1, message = "Thêm thành công" });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return Json(new { status = 0, message = Functions.GetAllErrorsPage(ModelState) });
        }

        public ActionResult _EditMemberModal(string Id)
        {
            ApplicationUser applicationUser = UserManager.FindById(Id);
            if (applicationUser == null)
            {
                return Content("<p>Dữ liệu không tồn tại trong hệ thống!</p>");
            }
            return PartialView(applicationUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditMember(ApplicationUser applicationUser, bool Status, string sBirthDate)
        {
            ApplicationUser user = UserManager.FindById(applicationUser.Id);

            if (applicationUser.Point < 0)
            {
                ModelState.AddModelError("Point", "Điểm số phải >= 0.");
            }
            if(string.IsNullOrEmpty(applicationUser.Email))
            {
                ModelState.AddModelError("Email", "Bạn chưa nhập Email.");
            }
            else if (Functions.IsValidEmail(applicationUser.Email) == false)
            {
                ModelState.AddModelError("Email", "Email không hợp lệ.");
            }
            else if(user.Email != applicationUser.Email)
            {
                if(UserManager.Users.Any(x=>x.Email == applicationUser.Email))
                {
                    ModelState.AddModelError("Email", "Email này đã tồn tại trong hệ thống. Bạn hãy nhập email khác.");
                }
            }
            if (string.IsNullOrEmpty(applicationUser.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "Bạn chưa nhập Số điện thoại.");
            }
            else if (user.PhoneNumber != applicationUser.PhoneNumber)
            {
                if (UserManager.Users.Any(x => x.PhoneNumber == applicationUser.PhoneNumber))
                {
                    ModelState.AddModelError("PhoneNumber", "Số điện thoại này đã tồn tại trong hệ thống. Bạn hãy nhập số điện thoại khác.");
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    user.FullName = applicationUser.FullName;
                    user.PhoneNumber = applicationUser.PhoneNumber;
                    user.Address = applicationUser.Address;
                    user.Email = applicationUser.Email;
                    if (!string.IsNullOrEmpty(sBirthDate))
                    {
                        user.BirthDate = DateTime.ParseExact(sBirthDate,"dd/MM/yyyy", null);
                    }
                    user.Point = applicationUser.Point;
                    user.LockoutEndDateUtc = Status == true ? DateTime.Today.AddDays(-1) : DateTime.Today.AddYears(200);
                    UserManager.Update(user);
                    return Json(new { status = 1, message = "Update success." }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return Json(new { status = 0, message = Functions.GetAllErrorsPage(this.ModelState) });
        }

       

        public ActionResult _DeleteModal(string Id)
        {
            ApplicationUser applicationUser = UserManager.FindById(Id);
            if (applicationUser == null)
            {
                return Content("<p>Dữ liệu không tồn tại trong hệ thống!</p>");
            }
            return PartialView(applicationUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Delete(string Id)
        {
            try
            {
                ApplicationUser user = await UserManager.FindByIdAsync(Id);
                await UserManager.DeleteAsync(user);
                return Json(new { status = 1, message = "Update success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return Json(new { status = 0, message = Functions.GetAllErrorsPage(this.ModelState) }, JsonRequestBehavior.AllowGet);
        }


        public ViewResult Lock(string Id)
        {
            ApplicationUser aspNetUser = UserManager.FindById(Id);
            return View(aspNetUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Lock")]
        public ActionResult Locked(string Id, string LockoutEndDateUtc)
        {
            try
            {
                ApplicationUser aspNetUser = UserManager.FindById(Id);
                aspNetUser.LockoutEndDateUtc = DateTime.Parse(LockoutEndDateUtc);
                UserManager.Update(aspNetUser);
            }
            catch (FormatException)
            {
                object thongbao = "Thời gian khoá có định dạng không hợp lệ!";
                return View("_ThongBaoLoi", thongbao);
            }
            catch (Exception ex)
            {
                object thongbao = "Xảy ra lỗi " + ex.Message;
                return View("_ThongBaoLoi", thongbao);
            }
            return RedirectToAction("Index");
        }

        public ViewResult UnLocked(string Id)
        {
            ApplicationUser aspNetUser = UserManager.FindById(Id);
            return View(aspNetUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("UnLocked")]
        public ActionResult UnLockedConfirmed(string Id)
        {
            if (Session["Revalidate"] == null)
            {
                object thongbao = "Bạn chưa xác thực mật khẩu lần 2 để thực hiện thao tác xóa này!";
                return View("_ThongBaoLoi", thongbao);
            }
            try
            {
                ApplicationUser applicationUser  = UserManager.FindById(Id);
                applicationUser.LockoutEndDateUtc = DateTime.Today;
                UserManager.Update(applicationUser);
            }
            catch (Exception ex)
            {
                object thongbao = "Xảy ra lỗi " + ex.Message;
                return View("_ThongBaoLoi", thongbao);
            }
            return RedirectToAction("Index");
        }

        public ViewResult ChangePermission(string Id)
        {
            ApplicationUser applicationUser = UserManager.FindById(Id);
            //ViewBag.Roles = new SelectList(UserManager.Role , "Id", "Name", aspNetUser.AspNetRoles.Count > 0 ? aspNetUser.AspNetRoles.First().Id : null);

            return View(applicationUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePermission(string UserId, string Role)
        {
            ApplicationUser user = UserManager.FindById(UserId);
            UserManager.AddToRole(UserId, Role);
            return RedirectToAction("Index");
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = UserManager.ChangePassword(UserId, changePasswordViewModel.OldPassword, changePasswordViewModel.NewPassword);
                if (result.Succeeded)
                {
                    var user = UserManager.FindById(UserId);
                    if (user != null)
                    {
                        SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                    }
                    return Json(new { status = 1, message = "Đổi mật khẩu thành công." }, JsonRequestBehavior.AllowGet);
                }
                AddErrors(result);
            }  
            return Json(new { status = 0, message = Functions.GetAllErrorsPage(ModelState)}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SetPassword(SetPasswordViewModel setPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = UserManager.AddPassword(UserId, setPasswordViewModel.NewPassword);
                if (result.Succeeded)
                {
                    var user = UserManager.FindById(UserId);
                    if (user != null)
                    {
                        SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                    }
                    return Json(new { status = 1, message = "Đặt lại mật khẩu thành công." });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return Json(new { status = 0, message = Functions.GetAllErrorsPage(ModelState) });
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}
