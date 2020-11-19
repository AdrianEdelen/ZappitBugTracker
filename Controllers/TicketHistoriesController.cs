using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ZappitBugTracker.Data;

namespace ZappitBugTracker.Controllers
{
    public class TicketHistoriesController : Controller
    {
        #region Private Members
        private readonly ApplicationDbContext _context;
        #endregion
        #region Constructors
        public TicketHistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion
        #region GET Index
        // GET: TicketHistories
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TicketHistories.Include(t => t.Ticket).Include(t => t.User);
            //TicketHistoriesIdToNameViewModel vm = new TicketHistoriesIdToNameViewModel();


            //vm.OldValLabel = _context.Tick
            //return View(vm);
            return View(await applicationDbContext.ToListAsync());
        }
        #endregion
        private bool TicketHistoryExists(int id)
        {
            return _context.TicketHistories.Any(e => e.Id == id);
        }
    }
}
