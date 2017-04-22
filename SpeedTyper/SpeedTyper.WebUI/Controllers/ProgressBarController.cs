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
    public class ProgressBarController : Controller
    {
        IUserManager _userManager;
        
        public ProgressBarController(IUserManager userManager)
        {
            _userManager = userManager;
        }
        // GET: ProgressBar
        public PartialViewResult ProgressBar(int currentXP, int xpToLevel)
        {

            double widthPercent = ((double)currentXP / (double)xpToLevel) * 100;
            string widthPercentString;
            string xpString;
            if (widthPercent < 0 || widthPercent > 100)
            {
                widthPercentString = "100%";
            }
            else
            {
                widthPercentString = widthPercent + "%";
            }
            if (xpToLevel > 0)
            {
                xpString = currentXP + " / " + xpToLevel;
            }
            else
            {
                xpString = "MAX";
            }

            var progressBarModel = new ProgressBarViewModel()
            {
                CurrentXP = currentXP,
                XPToLevel = xpToLevel,
                XPString = xpString,
                WidthPercentString = widthPercentString
            };
            return PartialView(progressBarModel);
        }
    }
}