using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Warehouse.Entities;
using Warehouse.Models;
using Warehouse.Services.Interface;

namespace Warehouse.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InfoWebController : Controller
    {
        private IInfoWebService _InfoWebService;

        readonly List<string> ImageExtensions = ConfigurationManager.AppSettings["ImageExtensions"].ToString().Split('|').ToList();

        public InfoWebController(IInfoWebService InfoWebService)
        {
            _InfoWebService = InfoWebService;
        }

        #region CRUD
        public ActionResult Index()
        {
            return View(_InfoWebService.GetList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InfoWeb InfoWeb = _InfoWebService.GetFirst();
            if (InfoWeb == null)
            {
                return Redirect("/pages/404");
            }
            return View(InfoWeb);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InfoWeb InfoWeb = _InfoWebService.GetFirst();
            if (InfoWeb == null)
            {
                return Redirect("/pages/404");
            }
            return View(InfoWeb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(InfoWeb InfoWeb, HttpPostedFileBase file)
        {
            if(file != null && file.ContentLength > 0)
            {
                InfoWeb.Logo =  DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(file.FileName);
                string fileName = Server.MapPath("~/images/" + InfoWeb.Logo);
                Functions.UpLoadImage(fileName, file, this.ModelState, "Logo");
            }
            if (ModelState.IsValid)
            {
                _InfoWebService.Update(InfoWeb);
                Session["InfoWeb"] = InfoWeb;
                return RedirectToAction("Index");
            }
            return View(InfoWeb);
        }

        #endregion
    }
}
