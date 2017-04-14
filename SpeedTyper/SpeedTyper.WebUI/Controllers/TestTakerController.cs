using SpeedTyper.LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpeedTyper.WebUI.Controllers
{
    public class TestTakerController : Controller
    {
        ITestManager _testManager;
        public TestTakerController(ITestManager testManager)
        {
            _testManager = testManager;
        }

        // GET: TestTaker
        public ActionResult DisplayTest()
        {
            return View();
        }
    }
}