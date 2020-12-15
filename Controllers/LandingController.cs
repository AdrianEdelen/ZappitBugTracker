using Microsoft.AspNetCore.Mvc;

namespace ZappitBugTracker.Controllers
{
    public class LandingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
