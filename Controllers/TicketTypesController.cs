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
    public class TicketTypesController : Controller
    {
        #region Private Members
        private readonly ApplicationDbContext _context;
        #endregion
        #region Constructors
        public TicketTypesController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion
        #region GET Index
        // GET: TicketTypes
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.TicketTypes.ToListAsync());
        }
        #endregion
        #region GET Details
        // GET: TicketTypes/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketType = await _context.TicketTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketType == null)
            {
                return NotFound();
            }

            return View(ticketType);
        }
        #endregion
        #region GET/POST Create
        // GET: TicketTypes/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TicketTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name")] TicketType ticketType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticketType);
        }
        #endregion
        #region GET/POST Edit
        // GET: TicketTypes/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketType = await _context.TicketTypes.FindAsync(id);
            if (ticketType == null)
            {
                return NotFound();
            }
            return View(ticketType);
        }

        // POST: TicketTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TicketType ticketType)
        {
            if (id != ticketType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketTypeExists(ticketType.Id))
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
            return View(ticketType);
        }
        #endregion
        #region GET/POST Delete
        // GET: TicketTypes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketType = await _context.TicketTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketType == null)
            {
                return NotFound();
            }

            return View(ticketType);
        }

        // POST: TicketTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketType = await _context.TicketTypes.FindAsync(id);
            _context.TicketTypes.Remove(ticketType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        private bool TicketTypeExists(int id)
        {
            return _context.TicketTypes.Any(e => e.Id == id);
        }
    }
}
