using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Warehouse.Entities;
using Warehouse.Services.Interface;

namespace Warehouse.Controllers
{
    public class AlertController : Controller
    {
        readonly IAlertService _alertService;

        public AlertController(IAlertService alertService)
        {
            _alertService = alertService;
        }

        [ChildActionOnly]
        public PartialViewResult _AlertPartial()
        {
            List<Alert> alerts = _alertService.GetToShow();
            return PartialView(alerts);
        }

        [ChildActionOnly]
        public int CountListToShow()
        {
            return _alertService.CountListToShow();
        }

        public ActionResult GetContent(int Id)
        {
            var alert = _alertService.GetById(Id);
            if(alert == null)
            {
                return Content("Không có dữ liệu!");
            }
            return PartialView(alert);
        }
    }
}