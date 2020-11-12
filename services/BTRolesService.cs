using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZappitBugTracker.Models;


namespace ZappitBugTracker.services
{
    public class BTRolesService : IBTRolesService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BTUser> _userManager;

        public BTRolesService(RoleManager<IdentityRole> roleManager, UserManager<BTUser> usermanager)
        {
            _roleManager = roleManager;
            _userManager = usermanager;
        }

        public RoleManager<IdentityRole> RoleManager { get; }

        public async Task<bool> AddUserToRole(BTUser user, string roleName)
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded;
        }

        public async Task<bool> IsUserInRole(BTUser user, string roleName)
        {
            var result = await _userManager.IsInRoleAsync(user, roleName);
            return result;

        }

        public async Task<IEnumerable<string>> ListUserRoles(BTUser user)
        {
            var result = await _userManager.GetRolesAsync(user);
            return result;
        }

        public async Task<bool> RemoveUserFromRole(BTUser user, string roleName)
        {
            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            return result.Succeeded;

        }

        public async Task<ICollection<BTUser>> UsersInRole(string roleName)
        {
            var result = await _userManager.GetUsersInRoleAsync(roleName);
            return result;
        }

        public async Task<ICollection<BTUser>> UsersNotInRole(IdentityRole role)
        {
            //var roleId = await _roleManager.GetRoleIdAsync(role);
            var result = await _userManager.Users.Where(u => IsUserInRole(u, role.Name).Result == false).ToListAsync();
            return result;

        }
    }
}
