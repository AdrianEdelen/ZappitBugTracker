using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ZappitBugTracker.Models;

namespace ZappitBugTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        #region Constructors
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        #endregion
        #region Index
        public IActionResult Index()
        {
            return View();
        }
        #endregion
        #region Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}
