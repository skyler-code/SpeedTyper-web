using System.Web.Mvc;
using SpeedTyper.LogicLayer;
using SpeedTyper.WebUI.Models;
using SpeedTyper.WebUI.Infrastructure;
using Microsoft.AspNet.Identity;

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

        [ChildActionOnly]
        public PartialViewResult AllTopResults()
        {
            var topResults = new LeaderboardViewModels.TestResultsModel()
            {
                TopTestResults = _testManager.GetAllTopTestResults(),
                Ranks = CacheManager.CachedRanks()
            };
            return PartialView("TestResults", topResults);
        }

        [ChildActionOnly]
        public PartialViewResult Top30DaysResults()
        {
            var topResults = new LeaderboardViewModels.TestResultsModel()
            {
                TopTestResults = _testManager.GetTop30DaysResults(),
                Ranks = CacheManager.CachedRanks()
            };
            return PartialView("TestResults", topResults);
        }

        [ChildActionOnly]
        public PartialViewResult Top90DaysResults()
        {
            var topResults = new LeaderboardViewModels.TestResultsModel()
            {
                TopTestResults = _testManager.GetTop90DaysResults(),
                Ranks = CacheManager.CachedRanks()
            };
            return PartialView("TestResults", topResults);
        }

        [ChildActionOnly]
        public PartialViewResult TodaysResults()
        {
            var topResults = new LeaderboardViewModels.TestResultsModel()
            {
                TopTestResults = _testManager.GetTodaysResults(),
                Ranks = CacheManager.CachedRanks()
            };
            return PartialView("TestResults", topResults);
        }

        [ChildActionOnly]
        public PartialViewResult HighestRankingMembers()
        {
            var topResults = new LeaderboardViewModels.HighestRankedPlayersModel
            {
                TopPlayers = _userManager.RetrieveHighestRankingMembers(),
                Ranks = CacheManager.CachedRanks()
            };
            return PartialView("HighestRankingMembers", topResults);
        }

        [ChildActionOnly]
        public PartialViewResult Top10Results()
        {
            var topResults = new LeaderboardViewModels.TestResultsModel()
            {
                TopTestResults = _testManager.GetTop10TestResults(),
                Ranks = CacheManager.CachedRanks()
            };
            return PartialView("TestResults", topResults);
        }

        [ChildActionOnly]
        public PartialViewResult Last10Results(int userID)
        {
            var topResults = new LeaderboardViewModels.TestResultsModel()
            {
                TopTestResults = _testManager.GetUserLast10TestResults(userID)
            };
            return PartialView("Last10Results", topResults);
        }
    }
}
