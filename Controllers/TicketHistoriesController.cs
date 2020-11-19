using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZappitBugTracker.Data;
using ZappitBugTracker.Models;
using ZappitBugTracker.Models.ViewModels;

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
