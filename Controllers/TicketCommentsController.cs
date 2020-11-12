using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZappitBugTracker.Data;
using ZappitBugTracker.Models;

namespace ZappitBugTracker.Controllers
{
    public class TicketCommentsController : Controller
    {
        #region Private Members
        private readonly ApplicationDbContext _context;
        private readonly UserManager<BTUser> _userManager;
        #endregion
        #region Constructors
        public TicketCommentsController(ApplicationDbContext context, UserManager<BTUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        #endregion
        #region GET Index
        // GET: TicketComments
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TicketComments.Include(t => t.Ticket).Include(t => t.User);
            return View(await applicationDbContext.ToListAsync());
        }
        #endregion
        #region GET Comments For Tickets
        // GET: CommentsForTickets
        [Authorize]
        public async Task<IActionResult> CommentsForTicket(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //not needed
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            
            var ticketComments = await _context.TicketComments
                
                .Include(t => t.Ticket)
                .Include(t => t.User)
                .Where(t => t.TicketId == id)
                .ToListAsync();
            return View(ticketComments);
        }
        #endregion
        #region GET Details
        // GET: TicketComments/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketComment = await _context.TicketComments
                .Include(t => t.Ticket)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketComment == null)
            {
                return NotFound();
            }

            return View(ticketComment);
        }
        #endregion
        #region GET/POST Create
        // GET: TicketComments/Create
        [Authorize]
        public IActionResult Create(int? id)
        {
            TicketComment currentTicket = new TicketComment();
            currentTicket.TicketId = (int)id;
            return View(currentTicket);
        }

        // POST: TicketComments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Comment,TicketId")] TicketComment ticketComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketComment);
                ticketComment.Created = DateTime.Now;
                ticketComment.UserId = _userManager.GetUserId(User);

                
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Tickets", new { id = ticketComment.TicketId });

            }
            return RedirectToAction("Details", "Tickets", new { id = ticketComment.TicketId});
        }
        #endregion
        #region GET/POST Edit
        // GET: TicketComments/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketComment = await _context.TicketComments.FindAsync(id);
            if (ticketComment == null)
            {
                return NotFound();
            }
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description", ticketComment.TicketId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ticketComment.UserId);
            return View(ticketComment);
        }

        // POST: TicketComments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Comment,Created,TicketId,UserId")] TicketComment ticketComment)
        {
            if (id != ticketComment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketCommentExists(ticketComment.Id))
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
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description", ticketComment.TicketId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ticketComment.UserId);
            return View(ticketComment);
        }
        #endregion
        #region GET/POST Delete
        // GET: TicketComments/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketComment = await _context.TicketComments
                .Include(t => t.Ticket)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketComment == null)
            {
                return NotFound();
            }

            return View(ticketComment);
        }

        // POST: TicketComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketComment = await _context.TicketComments.FindAsync(id);
            _context.TicketComments.Remove(ticketComment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        private bool TicketCommentExists(int id)
        {
            return _context.TicketComments.Any(e => e.Id == id);
        }
    }
}
