using SoccerStats.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoccerStats.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Result> results = Helper.SoccerStatsWorker.LoadPage();

            results = results.Where(r => r.HomeTeam == "Liverpool" || r.AwayTeam == "Liverpool").ToList();

            return View(results);
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