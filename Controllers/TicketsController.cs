using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IBTProjectService _projectService;
        private readonly IBTRolesService _rolesService;
        private readonly IBTAttachmentService _attachmentService;
        #endregion
        #region Constructors
        public TicketsController(ApplicationDbContext context, UserManager<BTUser> userManager, IBTHistoryService historyService, IBTProjectService projectService, IBTRolesService rolesService, IBTAttachmentService attachmentService)
        {
            _context = context;
            _userManager = userManager;
            _historyService = historyService;
            _projectService = projectService;
            _rolesService = rolesService;
            _attachmentService = attachmentService;
        }
        #endregion

        //GET AllTickets
        //#region Get/post all tickets
        //[Authorize(Roles = "Admin,ProjectManager,Developer")]
        //public async Task<IActionResult> AllTickets()
        //{
        //    var applicationDbContext = _context.Tickets
        //        .Include(t => t.DeveloperUser)
        //        .Include(t => t.OwnerUser)
        //        .Include(t => t.Project)
        //        .Include(t => t.TicketPriority)
        //        .Include(t => t.TicketStatus)
        //        .Include(t => t.TicketType);
        //    return View(await applicationDbContext.ToListAsync());
        //}
        ////POST AllTickets
        ////[HttpPost]
        ////[Authorize(Roles = "Admin,ProjectManager")]
        ////public async Task<IActionResult> AllTickets()
        ////{
        ////    return View();
        ////}
        //#endregion
        #region CreateAttachments
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeAttachment([Bind("FormFile, Description, TicketId")] TicketAttachment ticketAttachment)
        {
            
            if (ModelState.IsValid)
            {
               
                ticketAttachment.ContentType = ticketAttachment.FormFile.ContentType;
                ticketAttachment.FileData = await _attachmentService.ConvertFileToByteArrayAsync(ticketAttachment.FormFile);
                ticketAttachment.FileName = ticketAttachment.FormFile.FileName;
                ticketAttachment.Created = DateTimeOffset.Now;
                ticketAttachment.UserId = _userManager.GetUserId(User);

                _context.Add(ticketAttachment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Tickets", new { id = ticketAttachment.TicketId });

            }
            
            return RedirectToAction("Details", "Tickets", new { id = ticketAttachment.TicketId });
        }
        #endregion
        
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
        //#region GET Index
        //// GET: Tickets
        //[Authorize]
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Tickets
        //        .Include(t => t.DeveloperUser)
        //        .Include(t => t.OwnerUser)
        //        .Include(t => t.Project)
        //        .Include(t => t.TicketPriority)
        //        .Include(t => t.TicketStatus)
        //        .Include(t => t.TicketType);
        //    return View(await applicationDbContext.ToListAsync());
        //}
        //#endregion
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
                .Include(t => t.Attachments)
                .Include(t => t.Histories)
                .ThenInclude(tc => tc.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }
        #endregion
        #region POST Create
        
        // POST: Tickets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ProjectManager,Submitter,Developer")]
        public async Task<IActionResult> Create([Bind("Id,ProjectId,Title,Description,TicketTypeId")] Ticket ticket)
        {

            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                ticket.Created = DateTime.Now;
                ticket.TicketPriorityId = (await _context.TicketPriority.FirstOrDefaultAsync(p => p.Name == "New")).Id;
                ticket.TicketStatusId = (await _context.TicketStatus.FirstOrDefaultAsync(s => s.Name == "New")).Id;
                ticket.OwnerUserId = _userManager.GetUserId(User);

                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Tickets", new { id = ticket.Id });
                //add a sweet alert on succesful ticket submission
            }

            ViewData["DeveloperUserId"] = new SelectList(_context.Users, "Id", "Id", ticket.DeveloperUserId);
            ViewData["OwnerUserId"] = new SelectList(_context.Users, "Id", "Id", ticket.OwnerUserId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", ticket.ProjectId);
            ViewData["TicketPriorityId"] = new SelectList(_context.Set<TicketPriority>(), "Id", "Id", ticket.TicketPriorityId);
            ViewData["TicketStatusId"] = new SelectList(_context.Set<TicketStatus>(), "Id", "Id", ticket.TicketStatusId);
            ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Id", ticket.TicketTypeId);
            return View();
            
        }
        #endregion
        #region GET/POST Edit
        // GET: Tickets/Edit/5
        [Authorize(Roles = "Admin,ProjectManager,Developer,Submitter")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.DeveloperUser)
                .Include(t => t.TicketStatus)
                .Include(t => t.TicketPriority)
                .Include(t => t.OwnerUser)
                .Include(t => t.TicketType)
                .FirstOrDefaultAsync(i => i.Id == id);
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
            
            return View(ticket);
        }
        
        // POST: Tickets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ProjectManager,Developer")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,DeveloperUserId,OwnerUserId,Created")] Ticket ticket)
        {
            if (!User.IsInRole("Demo"))
            {
                if (id != ticket.Id)
                {
                    return NotFound();
                }
                Ticket oldTicket = await _context.Tickets
                            .AsNoTracking()
                            .Include(t => t.DeveloperUser)
                            .FirstOrDefaultAsync(t => t.Id == ticket.Id);
                if (ModelState.IsValid)
                {
                    try
                    {
                        ticket.DeveloperUser = await _context.Users.FindAsync(ticket.DeveloperUserId);
                        ticket.Updated = DateTimeOffset.Now;
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
                    return RedirectToAction("Details", "Tickets", new { id = ticket.Id });
                }
                ViewData["DeveloperUserId"] = new SelectList(_context.Users, "Id", "Id", ticket.DeveloperUserId);
                ViewData["OwnerUserId"] = new SelectList(_context.Users, "Id", "Id", ticket.OwnerUserId);
                ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", ticket.ProjectId);
                ViewData["TicketPriorityId"] = new SelectList(_context.Set<TicketPriority>(), "Id", "Id", ticket.TicketPriorityId);
                ViewData["TicketStatusId"] = new SelectList(_context.Set<TicketStatus>(), "Id", "Id", ticket.TicketStatusId);
                ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Id", ticket.TicketTypeId);
                return View(ticket);
            }
            else
            {
                TempData["DemoLockout"] = "Your Changes have not been saved. To make changes to the database please log in as a full user.";
                return RedirectToAction("Details", "Tickets", new { id = ticket.Id });
            }

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
        #region CreateAttachment
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

        public async Task<FileResult> DownloadFile (int id)
        {
            TicketAttachment attachment = await _context.TicketAttachments.FindAsync(id);
            return File(attachment.FileData, attachment.ContentType);
        }
    }


}
