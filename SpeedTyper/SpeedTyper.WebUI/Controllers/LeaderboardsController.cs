using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using SpeedTyper.DataObjects;
using SpeedTyper.LogicLayer;
using SpeedTyper.WebUI.Models;

namespace SpeedTyper.WebUI.Controllers
{
#if !DEBUG
    [RequireHttps]
#endif
    public class LeaderboardsController : Controller
    {
        ITestManager _testManager;
        IUserManager _userManager;
        IRankManager _rankManager;

        public LeaderboardsController(ITestManager testManager, IUserManager userManager, IRankManager rankManager)
        {
            _testManager = testManager;
            _userManager = userManager;
            _rankManager = rankManager;
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
            var topResults = new LeaderboardViewModels.TestResultsModel()
            {
                TopTestResults = _testManager.GetAllTopTestResults(),
                Ranks = cachedRanks()
            };
            return PartialView("TestResults", topResults);
        }

        public PartialViewResult Top30DaysResults()
        {
            var topResults = new LeaderboardViewModels.TestResultsModel()
            {
                TopTestResults = _testManager.GetTop30DaysResults(),
                Ranks = cachedRanks()
            };
            return PartialView("TestResults", topResults);
        }

        public PartialViewResult Top90DaysResults()
        {
            var topResults = new LeaderboardViewModels.TestResultsModel()
            {
                TopTestResults = _testManager.GetTop90DaysResults(),
                Ranks = cachedRanks()
            };
            return PartialView("TestResults", topResults);
        }

        public PartialViewResult TodaysResults()
        {
            var topResults = new LeaderboardViewModels.TestResultsModel()
            {
                TopTestResults = _testManager.GetTodaysResults(),
                Ranks = cachedRanks()
            };
            return PartialView("TestResults", topResults);
        }

        public PartialViewResult HighestRankingMembers()
        {
            var topResults = new LeaderboardViewModels.HighestRankedPlayersModel
            {
                TopPlayers = _userManager.RetrieveHighestRankingMembers(),
                Ranks = cachedRanks()
            };
            return PartialView("HighestRankingMembers", topResults);
        }

        private List<Rank> cachedRanks()
        {
            if(System.Web.HttpContext.Current.Cache["stRanks"] == null)
            {
                System.Web.HttpContext.Current.Cache["stRanks"] = _rankManager.RetrieveUserRanks();
            }
            return (List<Rank>)System.Web.HttpContext.Current.Cache["stRanks"];
        }
    }
}
