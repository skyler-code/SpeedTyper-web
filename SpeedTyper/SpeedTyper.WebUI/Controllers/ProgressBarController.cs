using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpeedTyper.LogicLayer;
using Microsoft.AspNet.Identity;
using SpeedTyper.WebUI.Models;

namespace SpeedTyper.WebUI.Controllers
{
    [Authorize]
    [ChildActionOnly]
    public class ProgressBarController : Controller
    {
        IUserManager _userManager;
        
        public ProgressBarController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        // GET: ProgressBar
        public PartialViewResult ProgressBar(int currentXP, int xpToLevel, int previousLevelXPToLevel)
        {
            var progressBarModel = new ProgressBarViewModel()
            {
                CurrentXP = currentXP,
                XPToLevel = xpToLevel,
                XPString = Infrastructure.DisplayHelpers.ProgressBarXP(currentXP, xpToLevel, previousLevelXPToLevel),
                WidthPercentString = Infrastructure.DisplayHelpers.ProgressBarWidthPercent(currentXP, xpToLevel, previousLevelXPToLevel)
            };
            return PartialView(progressBarModel);
        }
    }
}