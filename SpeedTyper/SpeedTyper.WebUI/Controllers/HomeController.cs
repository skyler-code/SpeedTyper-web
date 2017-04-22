using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpeedTyper.LogicLayer;
using Microsoft.AspNet.Identity;

namespace SpeedTyper.WebUI.Controllers
{
    #if !DEBUG
    [RequireHttps]
    #endif
    public class HomeController : Controller
    {
        IUserManager userManager;
        ILevelManager levelManager;
        public HomeController(IUserManager _userManager, ILevelManager _levelManager)
        {
            userManager = _userManager;
            levelManager = _levelManager;
        }
        public ActionResult Index()
        {
            DataObjects.User _user;
            if (User.Identity.IsAuthenticated)
            {
                _user = userManager.RetrieveUserByUsername(User.Identity.GetUserName());
            }
            else
            {
                _user = userManager.CreateGuestUser();
            }
            var playerRank = Infrastructure.CacheManager.CachedRanks().Find(r => r.RankID == _user.RankID).RankName;
            int previousLevelXPToLevel;
            string greeting = "";
            if (!_user.IsGuest)
            {
                greeting = playerRank + " " + _user.DisplayName;
                previousLevelXPToLevel = levelManager.RetrieveXPForLevel(_user.Level);
            }
            else
            {
                greeting = playerRank;
                previousLevelXPToLevel = 0;
            }

            var homeViewModel = new Models.HomeViewModel()
            {
                User = _user,
                Greeting = greeting,
                PreviousLevelXPToLevel = previousLevelXPToLevel
            };
            return View(homeViewModel);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}