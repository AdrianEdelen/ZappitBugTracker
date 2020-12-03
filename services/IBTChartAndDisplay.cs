using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZappitBugTracker.services
{
    public interface IBTChartAndDisplay
    {
        public int TicketCount();
        public int YourTickets();
        public int UrgentTickets();
        public int OpenTickets();
        public int TicketsToday();
    }
}
