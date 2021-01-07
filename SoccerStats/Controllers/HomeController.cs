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
            List<Result> allResults = new List<Result>();

            List<string> teams = results.Select(r => r.HomeTeam).Distinct().ToList();

            foreach (var team in teams)
            {
                allResults.AddRange(results.Where(r => r.HomeTeam == team || r.AwayTeam == team).ToList());
            }            

            return View(allResults);
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