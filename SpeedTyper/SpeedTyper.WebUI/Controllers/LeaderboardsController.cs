using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using SpeedTyper.DataObjects;
using SpeedTyper.LogicLayer;
using SpeedTyper.WebUI.Models;
using SpeedTyper.WebUI.Infrastructure;

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
                Ranks = CacheManager.CachedRanks()
            };
            return PartialView("TestResults", topResults);
        }

        public PartialViewResult Top30DaysResults()
        {
            var topResults = new LeaderboardViewModels.TestResultsModel()
            {
                TopTestResults = _testManager.GetTop30DaysResults(),
                Ranks = CacheManager.CachedRanks()
            };
            return PartialView("TestResults", topResults);
        }

        public PartialViewResult Top90DaysResults()
        {
            var topResults = new LeaderboardViewModels.TestResultsModel()
            {
                TopTestResults = _testManager.GetTop90DaysResults(),
                Ranks = CacheManager.CachedRanks()
            };
            return PartialView("TestResults", topResults);
        }

        public PartialViewResult TodaysResults()
        {
            var topResults = new LeaderboardViewModels.TestResultsModel()
            {
                TopTestResults = _testManager.GetTodaysResults(),
                Ranks = CacheManager.CachedRanks()
            };
            return PartialView("TestResults", topResults);
        }

        public PartialViewResult HighestRankingMembers()
        {
            var topResults = new LeaderboardViewModels.HighestRankedPlayersModel
            {
                TopPlayers = _userManager.RetrieveHighestRankingMembers(),
                Ranks = CacheManager.CachedRanks()
            };
            return PartialView("HighestRankingMembers", topResults);
        }
    }
}
