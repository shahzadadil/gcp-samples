namespace Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Google.Apis.Auth.OAuth2;
    using Google.Cloud.Storage.V1;

    using log4net;

    public class HomeController : Controller
    {
        private ILog _Logger = LogManager.GetLogger(typeof(HomeController));

        public ActionResult Index()
        {
           
            _Logger.Info("Executing Index method");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}