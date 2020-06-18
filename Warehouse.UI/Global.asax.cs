using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Warehouse.Data.Data;
using Warehouse.Data.Interface;
using Warehouse.Models;
using Warehouse.Services.Interface;
using Warehouse.Services.Services;

namespace Warehouse
{
    public class MvcApplication : System.Web.HttpApplication
    {
       
        protected void Application_Start()
        {
            UnityConfig.RegisterComponents();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Application["WidthImageProduct"] = ConfigurationManager.AppSettings["WidthImageProduct"].ToString();
            Application["HeightImageProduct"] = ConfigurationManager.AppSettings["HeightImageProduct"].ToString();
            Application["WidthImageBlog"] = ConfigurationManager.AppSettings["WidthImageBlog"].ToString();
            Application["HeightImageBlog"] = ConfigurationManager.AppSettings["HeightImageBlog"].ToString();
        }

        protected void Session_Start()
        {
            InfoWebDal infoWebDal = new InfoWebDal();
            InfoWebService infoWebService = new InfoWebService(infoWebDal);
            Session["InfoWeb"] = infoWebService.GetFirst();
            //var lang = "vi"; // Ngôn ngữ mặc định

            //if (HttpContext.Current.Request.Cookies["lang"] != null)
            //{
            //    lang = HttpContext.Current.Request.Cookies["lang"].Value;
            //}

            //else
            //{
            //    HttpCookie cookie = new HttpCookie("lang", lang);
            //    cookie.Expires = DateTime.Today.AddDays(30);
            //    HttpContext.Current.Response.SetCookie(cookie);
            //}

            //var culture = new CultureInfo(lang);

            //System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            //System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //var lang = "vi"; // Ngôn ngữ mặc định

            //if (HttpContext.Current.Request.Cookies["lang"] != null)
            //{
            //    lang = HttpContext.Current.Request.Cookies["lang"].Value;
            //}

            //else
            //{
            //    HttpCookie cookie = new HttpCookie("lang", lang);
            //    cookie.Expires = DateTime.Today.AddDays(30);
            //    HttpContext.Current.Response.SetCookie(cookie);
            //}

            //var culture = new CultureInfo(lang);

            //System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            //System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

        }
    }
}
