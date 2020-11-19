using System.Threading.Tasks;
using ZappitBugTracker.Models;

namespace ZappitBugTracker.services
{
    public interface IBTHistoryService
    {
        Task AddHistory(Ticket oldTicket, Ticket newTicket, string userId);

    }
}
