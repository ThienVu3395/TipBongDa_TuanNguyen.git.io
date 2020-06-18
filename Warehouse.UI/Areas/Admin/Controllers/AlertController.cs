using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Warehouse.Entities;
using Warehouse.Services.Interface;

namespace Warehouse.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AlertController : Controller
    {
        IAlertService _alertService;

        public AlertController(IAlertService alertService)
        {
            _alertService = alertService;
        }

        #region CRUD
        public ViewResult Index()
        {
            List<Alert> alerts = _alertService.GetAll().OrderByDescending(x => x.Id).ToList();
            return View(alerts);
        }

        public ActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Alert alert = _alertService.GetById(Id.Value);
            if (alert == null)
                return Redirect("/pages/404");

            return View(alert);
        }

        public ViewResult Create()
        {
            return View(new Alert());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Exclude = "ExpirationDate,DateCreated")] Alert alert, string ExpirationDate)
        {
            alert.Author = User.Identity.Name;
            alert.DateCreated = DateTime.Now;

            if(!string.IsNullOrEmpty(ExpirationDate))
            {
                DateTime expirationDateDtime;
                if(DateTime.TryParseExact(ExpirationDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out expirationDateDtime))
                {
                    alert.ExpirationDate = expirationDateDtime;
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _alertService.Add(alert);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(alert);
        }

        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Alert alert = _alertService.GetById(Id.Value);
            if (alert == null)
                return Redirect("/pages/404");

            return View(alert);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Exclude = "Author,ExpirationDate")] Alert alert, string ExpirationDate)
        {
            alert.Author = User.Identity.Name;
            if (!string.IsNullOrEmpty(ExpirationDate))
            {
                DateTime expirationDateDtime;
                if (DateTime.TryParseExact(ExpirationDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out expirationDateDtime))
                {
                    alert.ExpirationDate = expirationDateDtime;
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _alertService.Update(alert);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(alert);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int Id)
        {
            _alertService.Delete(Id);
            return RedirectToAction("Index");
        }

        #endregion

 
    }
}