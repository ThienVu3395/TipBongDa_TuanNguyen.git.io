using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Warehouse.Controllers
{
    public class PartialViewController : Controller
    {
        // GET: PartialView
        public PartialViewResult Index()
        {
            return PartialView();
        }

        public PartialViewResult Pages()
        {
            return PartialView();
        }
    }
}