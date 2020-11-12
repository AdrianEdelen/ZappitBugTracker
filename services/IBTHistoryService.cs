using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZappitBugTracker.Models;

namespace ZappitBugTracker.services
{
    public interface IBTHistoryService
    {
        Task AddHistory(Ticket oldTicket, Ticket newTicket, string userId);

    }
}
