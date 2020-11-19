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

namespace ZappitBugTracker.Controllers
{
    public class TicketPrioritiesController : Controller
    {
        #region Private Members
        private readonly ApplicationDbContext _context;
        #endregion
        #region Constructors
        public TicketPrioritiesController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion
        #region GET Index
        // GET: TicketPriorities
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.TicketPriority.ToListAsync());
        }
        #endregion
        #region GET/POST Create
        // GET: TicketPriorities/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TicketPriorities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name")] TicketPriority ticketPriority)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketPriority);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticketPriority);
        }
        #endregion
        #region GET/POST Delete
        // GET: TicketPriorities/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketPriority = await _context.TicketPriority
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketPriority == null)
            {
                return NotFound();
            }

            return View(ticketPriority);
        }

        // POST: TicketPriorities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketPriority = await _context.TicketPriority.FindAsync(id);
            _context.TicketPriority.Remove(ticketPriority);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        private bool TicketPriorityExists(int id)
        {
            return _context.TicketPriority.Any(e => e.Id == id);
        }
    }
}
