using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Raven.Client;

namespace Yarb.Web.Controllers
{
    public class HomeController : Controller
    {
        public IDocumentSession session { get; set; }

        public HomeController(IDocumentSession Session)
        {
            session = Session;
        }
        public ActionResult Index()
        {
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