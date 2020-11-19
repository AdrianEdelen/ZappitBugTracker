using System.Threading.Tasks;

namespace ZappitBugTracker.services
{
    public interface IBTAccessService
    {
        public Task<bool> CanInteractProject(string userId, int projectId, string roleName);
        public Task<bool> CanInteractTicket(string userId, int ticketId, string roleName);
    }
}
