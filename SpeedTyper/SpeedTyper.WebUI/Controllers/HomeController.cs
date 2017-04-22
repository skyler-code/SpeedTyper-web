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
        public HomeController(IUserManager _userManager)
        {
            userManager = _userManager;
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
            string greeting = "";
            if (!_user.IsGuest)
            {
                greeting = playerRank + " " + _user.DisplayName;
            } else
            {
                greeting = playerRank;
            }

            var homeViewModel = new Models.HomeViewModel()
            {
                User = _user,
                Greeting = greeting
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