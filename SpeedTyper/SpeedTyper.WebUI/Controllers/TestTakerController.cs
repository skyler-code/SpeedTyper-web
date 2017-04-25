using System.Web.Mvc;

namespace SpeedTyper.WebUI.Controllers
{

    [ChildActionOnly]
    public class TestTakerController : Controller
    {
        // GET: TestTaker
        public PartialViewResult DisplayTest()
        {
            return PartialView();
        }
    }
}