using Microsoft.AspNetCore.Mvc.Rendering;
using ZappitBugTracker.Data;

namespace ZappitBugTracker.services
{
    public class BTModalService : IBTModalService
    {
        private readonly ApplicationDbContext _context;

        public BTModalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public SelectList DevUserDropDown()
        {
            return new SelectList(_context.Users, "Id", "FullName");
        }

        public SelectList OwnerUserDropDown()
        {
            return new SelectList(_context.Users, "Id", "Name");
        }

        public SelectList PriorityDropDown()
        {
            return new SelectList(_context.TicketPriority, "Id", "Name");
        }
        public SelectList ProjectDropDown()
        {
            return new SelectList(_context.Projects, "Id", "Name");
        }

        public SelectList StatusDropDown()
        {
            return new SelectList(_context.TicketStatus, "Id", "Name");
        }

        public SelectList TypeDropDown()
        {
            return new SelectList(_context.TicketTypes, "Id", "Name");
        }

    }
}
