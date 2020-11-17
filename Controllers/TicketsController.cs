using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZappitBugTracker.Data;
using ZappitBugTracker.Models;
using ZappitBugTracker.services;

namespace ZappitBugTracker.Controllers
{
    public class TicketsController : Controller
    {
        #region Private Members
        private readonly ApplicationDbContext _context;
        private readonly UserManager<BTUser> _userManager;
        private readonly IBTHistoryService _historyService;
        #endregion
        #region Constructors
        public TicketsController(ApplicationDbContext context, UserManager<BTUser> userManager, IBTHistoryService historyService)
        {
            _context = context;
            _userManager = userManager;
            _historyService = historyService;
        }
        #endregion

        //public async Task<IActionResult> ViewMyTickets()
        //{
            
        //}




        #region GET Project Tickets
        //GET Project Tickets
        [Authorize]
        public async Task<IActionResult> ProjectTickets(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["ProjectName"] = project.Name;
            ViewData["ProjectId"] = project.Id;

            var projectTickets = await _context.Tickets
                .Include(t => t.DeveloperUser)
                .Include(t => t.OwnerUser)
                .Include(t => t.Project)
                .Include(t => t.TicketPriority)
                .Include(t => t.TicketStatus)
                .Include(t => t.TicketType)
                .Where(prop => prop.ProjectId == id)
                .ToListAsync();
            return View(projectTickets);

        }
        #endregion
        #region GET Index
        // GET: Tickets
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tickets
                .Include(t => t.DeveloperUser)
                .Include(t => t.OwnerUser)
                .Include(t => t.Project)
                .Include(t => t.TicketPriority)
                .Include(t => t.TicketStatus)
                .Include(t => t.TicketType);
            return View(await applicationDbContext.ToListAsync());
        }
        #endregion
        #region GET Details
        // GET: Tickets/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.DeveloperUser)
                .Include(t => t.OwnerUser)
                .Include(t => t.Project)
                .Include(t => t.TicketPriority)
                .Include(t => t.TicketStatus)
                .Include(t => t.TicketType)
                .Include(t => t.Comments)
                .ThenInclude(tc => tc.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }
        #endregion
        #region GET/POST Create
        // GET: Tickets/Create
        [Authorize(Roles = "Admin,ProjectManager,Submitter,Developer")]
        public IActionResult Create(int? id)
        {
            if (id != null)
            {
                Ticket currentTicket = new Ticket();
                currentTicket.ProjectId = (int)id;
                ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Name");
                return View(currentTicket);
            }
            else
            {
                return RedirectToAction("Index", "Projects");
            }
        }

        // POST: Tickets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ProjectManager,Submitter,Developer")]
        public async Task<IActionResult> Create([Bind("ProjectId,Title,Description,TicketTypeId")] Ticket ticket)
        {

            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                ticket.Created = DateTime.Now;
                ticket.TicketPriorityId = 1;
                ticket.TicketStatusId = 1;
                ticket.OwnerUser = await _userManager.GetUserAsync(User);

                await _context.SaveChangesAsync();
                return RedirectToAction("ProjectTickets", "Tickets", new { id = ticket.ProjectId });

            }

            ViewData["DeveloperUserId"] = new SelectList(_context.Users, "Id", "Id", ticket.DeveloperUserId);
            ViewData["OwnerUserId"] = new SelectList(_context.Users, "Id", "Id", ticket.OwnerUserId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", ticket.ProjectId);
            ViewData["TicketPriorityId"] = new SelectList(_context.Set<TicketPriority>(), "Id", "Id", ticket.TicketPriorityId);
            ViewData["TicketStatusId"] = new SelectList(_context.Set<TicketStatus>(), "Id", "Id", ticket.TicketStatusId);
            ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Id", ticket.TicketTypeId);
            return View(ticket);
        }
        #endregion
        #region GET/POST Edit
        // GET: Tickets/Edit/5
        [Authorize(Roles = "Admin,ProjectManager,Developer")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ticket.DeveloperUser = await _context.Users.FindAsync(ticket.DeveloperUserId);
            ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Name");
            ViewData["DeveloperUserId"] = new SelectList(_context.Users, "Id", "FullName", ticket.DeveloperUserId);
            ViewData["OwnerUserId"] = new SelectList(_context.Users, "Id", "Name", ticket.OwnerUserId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", ticket.ProjectId);
            ViewData["TicketPriorityId"] = new SelectList(_context.Set<TicketPriority>(), "Id", "Name", ticket.TicketPriorityId);
            ViewData["TicketStatusId"] = new SelectList(_context.Set<TicketStatus>(), "Id", "Name", ticket.TicketStatusId);
            //ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Id", ticket.TicketTypeId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ProjectManager,Developer")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,DeveloperUserId")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }
            ticket.DeveloperUser = await _context.Users.FindAsync(ticket.DeveloperUserId);
            Ticket oldTicket = await _context.Tickets
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == ticket.Id);
            //ticket.DeveloperUser = _context.Tickets.Include(t => t.DeveloperUser);
            

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
                //Add History
                string userId = _userManager.GetUserId(User);
                await _historyService.AddHistory(oldTicket, ticket, userId);

                ticket.Updated = DateTime.Now;
                return RedirectToAction("ProjectTickets", "Tickets", new { id = ticket.ProjectId });
                //return RedirectToAction(nameof(Index));
            }
            ViewData["DeveloperUserId"] = new SelectList(_context.Users, "Id", "Id", ticket.DeveloperUserId);
            ViewData["OwnerUserId"] = new SelectList(_context.Users, "Id", "Id", ticket.OwnerUserId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", ticket.ProjectId);
            ViewData["TicketPriorityId"] = new SelectList(_context.Set<TicketPriority>(), "Id", "Id", ticket.TicketPriorityId);
            ViewData["TicketStatusId"] = new SelectList(_context.Set<TicketStatus>(), "Id", "Id", ticket.TicketStatusId);
            ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Id", ticket.TicketTypeId);
            return View(ticket);
        }
        #endregion 
        #region GET/POST Delete
        // GET: Tickets/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.DeveloperUser)
                .Include(t => t.OwnerUser)
                .Include(t => t.Project)
                .Include(t => t.TicketPriority)
                .Include(t => t.TicketStatus)
                .Include(t => t.TicketType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Ticket Attachments
        [HttpPost]
        public async Task<IActionResult> CreateAttachment(int ticketId, string description, IFormFile attachment)
        {
            TicketAttachment ticketAttachment = new TicketAttachment
            {
                Description = description,
                TicketId = ticketId,
                //FileData = Encoder(attachment.FileName),
                //FilePath = Encoder(attachment.FileName),
                Created = DateTimeOffset.Now,
                UserId = _userManager.GetUserId(User)
            };
            _context.TicketAttachments.Add(ticketAttachment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Tickets", new { id = ticketId });
        }
        #endregion
        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
