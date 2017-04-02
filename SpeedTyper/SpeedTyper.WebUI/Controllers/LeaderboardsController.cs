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
        ITestManager _testManager;
        IUserManager _userManager;

        public LeaderboardsController(ITestManager testManager, IUserManager userManager)
        {
            _testManager = testManager;
            _userManager = userManager;
        }

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
            return PartialView("TestResults", _testManager.GetAllTopTestResults());
        }

        public PartialViewResult Top30DaysResults()
        {
            return PartialView("TestResults", _testManager.GetTop30DaysResults());
        }

        public PartialViewResult Top90DaysResults()
        {
            return PartialView("TestResults", _testManager.GetTop90DaysResults());
        }

        public PartialViewResult TodaysResults()
        {
            return PartialView("TestResults", _testManager.GetTodaysResults());
        }

        public PartialViewResult HighestRankingMembers()
        {
            return PartialView("HighestRankingMembers", _userManager.RetrieveHighestRankingMembers());
        }
    }
}
