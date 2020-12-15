using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using ZappitBugTracker.Data;
using ZappitBugTracker.Models;
using ZappitBugTracker.Models.ViewModels;

namespace ZappitBugTracker.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        #region Constructors
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        #endregion
        #region Index
        [Authorize]
        public async Task<IActionResult> Index()
        {
            List<TicketsAndProjectsViewModel> model = new List<TicketsAndProjectsViewModel>();
            List<Ticket> Tickets = await _context.Tickets.ToListAsync();
            List<Project> Projects = await _context.Projects.ToListAsync();

            foreach (var ticket in Tickets)
            {
                TicketsAndProjectsViewModel vm = new TicketsAndProjectsViewModel();
                vm.Ticket = ticket;
                model.Add(vm);

            }
            foreach (var project in Projects)
            {
                TicketsAndProjectsViewModel vm = new TicketsAndProjectsViewModel();
                vm.Project = project;
                model.Add(vm);
            }
            return View(model);

            //var applicationDbContext = _context.Tickets
            //    .Include(t => t.DeveloperUser)
            //    .Include(t => t.OwnerUser)
            //    .Include(t => t.Project)
            //    .Include(t => t.TicketPriority)
            //    .Include(t => t.TicketStatus)
            //    .Include(t => t.TicketType);
            //return View(await applicationDbContext.ToListAsync());
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
