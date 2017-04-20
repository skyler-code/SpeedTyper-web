using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpeedTyper.LogicLayer;
using Microsoft.AspNet.Identity;

namespace SpeedTyper.WebUI.Controllers
{
    public class ProgressBarController : Controller
    {
        IUserManager _userManager;
        
        public ProgressBarController(IUserManager userManager)
        {
            _userManager = userManager;
        }
        // GET: ProgressBar
        public PartialViewResult ProgressBar()
        {
            var user = _userManager.RetrieveUserByUsername(User.Identity.GetUserName());
            return PartialView(user);
        }
    }
}