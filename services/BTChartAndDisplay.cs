using System;
using System.Linq;
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
            return _context.Tickets.Where(t => t.TicketStatusId != 3).Count();

        }

        public int TicketCount()
        {
            return _context.Tickets.Count();
        }

        public int TicketsToday()
        {
            int count = 0;
            var tickets = _context.Tickets.ToList();
            foreach (var tic in tickets)
            {
                var yesterday = DateTimeOffset.Now.AddDays(-1);
                var result = DateTimeOffset.Compare(tic.Created, yesterday);
                if (result > 0)
                {
                    count++;
                }

            }
            return count;
        }

        public int UrgentTickets()
        {
            return _context.Tickets.Where(t => t.TicketPriorityId == 4 && t.TicketStatusId != 3).Count();
        }

        public int YourTickets()
        {
            throw new NotImplementedException();
        }
    }
}
