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
    public class TicketStatusController : Controller
    {
        #region Private Member
        private readonly ApplicationDbContext _context;
        #endregion
        #region Constructors
        public TicketStatusController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion
        #region GET Index
        // GET: TicketStatus
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.TicketStatus.ToListAsync());
        }
        #endregion
        #region GET Details
        // GET: TicketStatus/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketStatus = await _context.TicketStatus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketStatus == null)
            {
                return NotFound();
            }

            return View(ticketStatus);
        }
        #endregion
        #region GET/POST Create
        // GET: TicketStatus/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TicketStatus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name")] TicketStatus ticketStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticketStatus);
        }
        #endregion
        #region GET/POST Edit
        // GET: TicketStatus/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketStatus = await _context.TicketStatus.FindAsync(id);
            if (ticketStatus == null)
            {
                return NotFound();
            }
            return View(ticketStatus);
        }

        // POST: TicketStatus/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TicketStatus ticketStatus)
        {
            if (id != ticketStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketStatusExists(ticketStatus.Id))
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
            return View(ticketStatus);
        }
        #endregion
        #region GET/POST Delete
        // GET: TicketStatus/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketStatus = await _context.TicketStatus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketStatus == null)
            {
                return NotFound();
            }

            return View(ticketStatus);
        }

        // POST: TicketStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketStatus = await _context.TicketStatus.FindAsync(id);
            _context.TicketStatus.Remove(ticketStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        private bool TicketStatusExists(int id)
        {
            return _context.TicketStatus.Any(e => e.Id == id);
        }
    }
}
