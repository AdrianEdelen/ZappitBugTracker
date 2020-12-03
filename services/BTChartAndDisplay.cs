using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZappitBugTracker.Data;

namespace ZappitBugTracker.services
{
    public class BTChartAndDisplay : IBTChartAndDisplay
    {
        private readonly ApplicationDbContext _context;

        public BTChartAndDisplay(ApplicationDbContext context)
        {
            _context = context;
        }

        public int OpenTickets()
        {
            return _context.Tickets.Where(t => t.TicketStatus.Id != 3).Count();
            
        }

        public int TicketCount()
        {
            return _context.Tickets.Count();
        }

        public int UrgentTickets()
        {
            throw new NotImplementedException();
        }

        public int YourTickets()
        {
            throw new NotImplementedException();
        }
    }
}
