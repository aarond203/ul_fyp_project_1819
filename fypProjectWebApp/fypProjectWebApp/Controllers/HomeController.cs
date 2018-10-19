using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fypProjectWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Transactions()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Topup()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Checkout()
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