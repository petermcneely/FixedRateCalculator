using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CashflowCalculator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "A fixed rate loan cash flow generator.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "MIAC Analytics";

            return View();
        }
    }
}