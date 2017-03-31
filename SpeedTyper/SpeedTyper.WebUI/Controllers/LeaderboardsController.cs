using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpeedTyper.DataObjects;
using SpeedTyper.WebUI.Models;
using SpeedTyper.LogicLayer;

namespace SpeedTyper.WebUI.Controllers
{
#if !DEBUG
    [RequireHttps]
#endif
    public class LeaderboardsController : Controller
    {
        TestManager testManager = new TestManager();

        // GET: TestResults
        public ActionResult Index()
        {
            try
            {
                return View(testManager.GetAllTopTestResults());
            }
            catch
            {
                return HttpNotFound();
            }
        }

        // GET: TestResults/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResult testResult = testManager.GetTop10TestResults()[0];
            if (testResult == null)
            {
                return HttpNotFound();
            }
            return View(testResult);
        }
    }
}
