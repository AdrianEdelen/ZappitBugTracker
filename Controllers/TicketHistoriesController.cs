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
        #region GET Details
        // GET: TicketHistories/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketHistory = await _context.TicketHistories
                .Include(t => t.Ticket)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketHistory == null)
            {
                return NotFound();
            }

            return View(ticketHistory);
        }
        #endregion
        #region GET/POST Create
        // GET: TicketHistories/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: TicketHistories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,TicketId,Property,OldValue,NewValue,Created,UserId")] TicketHistory ticketHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description", ticketHistory.TicketId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ticketHistory.UserId);
            return View(ticketHistory);
        }
        #endregion
        #region GET/POST Edit
        // GET: TicketHistories/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketHistory = await _context.TicketHistories.FindAsync(id);
            if (ticketHistory == null)
            {
                return NotFound();
            }
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description", ticketHistory.TicketId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ticketHistory.UserId);
            return View(ticketHistory);
        }

        // POST: TicketHistories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TicketId,Property,OldValue,NewValue,Created,UserId")] TicketHistory ticketHistory)
        {
            if (id != ticketHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketHistoryExists(ticketHistory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description", ticketHistory.TicketId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ticketHistory.UserId);
            return View(ticketHistory);
        }
        #endregion
        #region GET/POST Delete
        // GET: TicketHistories/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketHistory = await _context.TicketHistories
                .Include(t => t.Ticket)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketHistory == null)
            {
                return NotFound();
            }

            return View(ticketHistory);
        }

        // POST: TicketHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketHistory = await _context.TicketHistories.FindAsync(id);
            _context.TicketHistories.Remove(ticketHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        private bool TicketHistoryExists(int id)
        {
            return _context.TicketHistories.Any(e => e.Id == id);
        }
    }
}
