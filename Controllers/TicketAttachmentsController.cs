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
    public class TicketAttachmentsController : Controller
    {
        #region Private Members
        private readonly ApplicationDbContext _context;
        #endregion
        #region Constructors
        public TicketAttachmentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion
        #region GET Index
        [Authorize]
        // GET: TicketAttachments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TicketAttachments.Include(t => t.Ticket).Include(t => t.User);
            return View(await applicationDbContext.ToListAsync());
        }
        #endregion
        #region GET/POST Create
        // GET: TicketAttachments/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }
        // POST: TicketAttachments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,FilePath,FileData,Description,Created,TicketId,UserId")] TicketAttachment ticketAttachment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketAttachment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description", ticketAttachment.TicketId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ticketAttachment.UserId);
            return View(ticketAttachment);
        }
        #endregion
        private bool TicketAttachmentExists(int id)
        {
            return _context.TicketAttachments.Any(e => e.Id == id);
        }
    }
}
