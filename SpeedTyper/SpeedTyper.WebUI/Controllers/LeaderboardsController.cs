using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpeedTyper.DataObjects;
using SpeedTyper.WebUI.Models;
using SpeedTyper.LogicLayer;

namespace SpeedTyper.WebUI.Controllers
{
#if !DEBUG
    [RequireHttps]
#endif
    public class LeaderboardsController : Controller
    {
        TestManager testManager = new TestManager();
        UserManager userManager = new UserManager();

        // GET: TestResults
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch
            {
                return HttpNotFound();
            }
        }

        public PartialViewResult AllTopResults()
        {
            return PartialView("TestResults", testManager.GetAllTopTestResults());
        }

        public PartialViewResult Top30DaysResults()
        {
            return PartialView("TestResults", testManager.GetTop30DaysResults());
        }

        public PartialViewResult Top90DaysResults()
        {
            return PartialView("TestResults", testManager.GetTop90DaysResults());
        }

        public PartialViewResult TodaysResults()
        {
            return PartialView("TestResults", testManager.GetTodaysResults());
        }

        public PartialViewResult HighestRankingMembers()
        {
            return PartialView("HighestRankingMembers", userManager.RetrieveHighestRankingMembers());
        }
    }
}
