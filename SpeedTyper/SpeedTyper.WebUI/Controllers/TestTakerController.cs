using SpeedTyper.LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace SpeedTyper.WebUI.Controllers
{

    [ChildActionOnly]
    public class TestTakerController : Controller
    {
        ITestManager _testManager;
        IUserManager _userManager;
        public TestTakerController(ITestManager testManager, IUserManager userManager)
        {
            _testManager = testManager;
            _userManager = userManager;
        }
        
        // GET: TestTaker
        public PartialViewResult DisplayTest()
        {
            return PartialView();
        }
    }
}