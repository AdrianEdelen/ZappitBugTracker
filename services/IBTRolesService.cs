using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZappitBugTracker.Models;

namespace ZappitBugTracker.services
{
    public interface IBTRolesService
    {
        public Task<bool> AddUserToRole(BTUser user, string roleName);

        public Task<bool> IsUserInRole(BTUser user, string roleName);

        public Task<IEnumerable<string>> ListUserRoles(BTUser user);

        public Task<bool> RemoveUserFromRole(BTUser user, string roleName);

        public Task<ICollection<BTUser>> UsersInRole(string roleName);

        public Task<ICollection<BTUser>> UsersNotInRole(IdentityRole role);
    }

}
